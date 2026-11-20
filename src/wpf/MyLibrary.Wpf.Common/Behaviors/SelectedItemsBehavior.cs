using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MyLibrary.Wpf.Common.Behaviors;

// Присоединенное поведение для двусторонней синхронизации выделенных элементов списка (ListBox, ListView)
// с коллекцией модели представления. Нужно потому, что свойство ListBox.SelectedItems доступно только
// для чтения и не поддерживает привязку.
//
// - Когда пользователь меняет выделение в списке, изменения переносятся в коллекцию модели представления.
// - Когда модель представления меняет коллекцию, изменения переносятся в список. Если при этом элементы
//   добавляются в выделение, то список прокручивается к первому из них, а тот получает фокус.
//
// Пример: <ListView SelectionMode="Extended" behaviors:SelectedItemsBehavior.SelectedItems="{Binding SelectedBooks}" ...>
public static class SelectedItemsBehavior
{
    public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.RegisterAttached(
            "SelectedItems",
            typeof(IList),
            typeof(SelectedItemsBehavior),
            new PropertyMetadata(null, OnSelectedItemsChanged));

    public static IList GetSelectedItems(DependencyObject element) =>
        (IList)element.GetValue(SelectedItemsProperty);

    public static void SetSelectedItems(DependencyObject element, IList value) =>
        element.SetValue(SelectedItemsProperty, value);

    // Объект, который выполняет синхронизацию для конкретного списка
    private static readonly DependencyProperty SynchronizerProperty =
        DependencyProperty.RegisterAttached(
            "Synchronizer",
            typeof(Synchronizer),
            typeof(SelectedItemsBehavior));

    private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ListBox listBox)
            return;

        // Прекращаем синхронизацию со старой коллекцией...
        (listBox.GetValue(SynchronizerProperty) as Synchronizer)?.Detach();
        listBox.ClearValue(SynchronizerProperty);

        // ...и начинаем с новой
        if (e.NewValue is IList selectedItems)
        {
            var synchronizer = new Synchronizer(listBox, selectedItems);
            listBox.SetValue(SynchronizerProperty, synchronizer);
            synchronizer.Attach();
        }
    }

    private sealed class Synchronizer(ListBox listBox, IList selectedItems)
    {
        // Признак того, что идет синхронизация, чтобы изменения не ходили по кругу
        private bool _isSynchronizing;
        // Элемент, который получит фокус, когда модель представления закончит менять выделение
        private object _itemToFocus;

        public void Attach()
        {
            // Переносим в список текущее выделение из модели представления
            CopySelectionToListBox();

            listBox.SelectionChanged += OnListBoxSelectionChanged;
            if (selectedItems is INotifyCollectionChanged collection)
                collection.CollectionChanged += OnCollectionChanged;
        }

        public void Detach()
        {
            listBox.SelectionChanged -= OnListBoxSelectionChanged;
            if (selectedItems is INotifyCollectionChanged collection)
                collection.CollectionChanged -= OnCollectionChanged;
        }

        // Пользователь изменил выделение в списке
        private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // SelectionChanged — всплывающее событие, поэтому пропускаем события вложенных
            // элементов (например, выпадающих списков внутри строк)
            if (_isSynchronizing || e.OriginalSource != listBox)
                return;

            Synchronize(() =>
            {
                foreach (object item in e.RemovedItems)
                    selectedItems.Remove(item);
                foreach (object item in e.AddedItems)
                    if (!selectedItems.Contains(item))
                        selectedItems.Add(item);
            });
        }

        // Модель представления изменила коллекцию выделенных элементов
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_isSynchronizing)
                return;

            Synchronize(() =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (object item in e.NewItems)
                            listBox.SelectedItems.Add(item);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (object item in e.OldItems)
                            listBox.SelectedItems.Remove(item);
                        break;
                    default:
                        CopySelectionToListBox();
                        break;
                }
            });

            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems.Count > 0)
                ScheduleFocus(e.NewItems[0]);
        }

        private void Synchronize(Action action)
        {
            _isSynchronizing = true;
            try
            {
                action();
            }
            finally
            {
                _isSynchronizing = false;
            }
        }

        private void CopySelectionToListBox()
        {
            listBox.SelectedItems.Clear();
            foreach (object item in selectedItems)
                listBox.SelectedItems.Add(item);
        }

        // Модель представления обычно добавляет элементы в выделение по одному, поэтому фокус ставим
        // отложенно: на первый из добавленных подряд элементов и после того, как список обновится
        private void ScheduleFocus(object item)
        {
            if (_itemToFocus is not null)
                return;

            _itemToFocus = item;
            listBox.Dispatcher.InvokeAsync(FocusItem, DispatcherPriority.Loaded);
        }

        private void FocusItem()
        {
            object item = _itemToFocus;
            _itemToFocus = null;

            // За это время выделение могло измениться
            if (!listBox.SelectedItems.Contains(item))
                return;

            // Прокручиваем список к элементу. При виртуализации контейнер элемента может
            // появиться только после этого, поэтому сразу же обновляем разметку.
            listBox.ScrollIntoView(item);
            listBox.UpdateLayout();

            if (listBox.ItemContainerGenerator.ContainerFromItem(item) is ListBoxItem container)
                container.Focus();
            else
                listBox.Focus();
        }
    }
}
