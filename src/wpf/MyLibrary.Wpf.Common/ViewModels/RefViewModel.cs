using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyLibrary.Common.DomainModel;
using MyLibrary.Wpf.Common.Mvvm;
using MyLibrary.Wpf.Common.Services;

namespace MyLibrary.Wpf.Common.ViewModels;

// Общая часть моделей представления справочников (авторов и издательств). Именно с ней
// работает окно справочника RefWindow (аналог интерфейса IRefView в WinForms-варианте).
public abstract class RefViewModel : DialogViewModel
{
    protected IMyLibraryDomainModel Model { get; }
    protected IDialogService DialogService { get; }

    protected RefViewModel(IMyLibraryDomainModel domainModel, IDialogService dialogService)
    {
        // Сохраняем внедряемые ссылки на модель и службу диалогов
        Model = domainModel;
        DialogService = dialogService;
        // Создаем команды. При загрузке окна считываем список элементов справочника.
        LoadedCommand = new RelayCommand(UpdateItems);
        AddCommand = new RelayCommand(OnAdd);
        EditCommand = new RelayCommand(OnEdit, HasSelectedItems);
        DeleteCommand = new RelayCommand(OnDelete, HasSelectedItems);
        // Доступность команд изменения и удаления зависит от того, выделено ли что-нибудь в списке
        SelectedItems.CollectionChanged += (_, _) =>
        {
            EditCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
        };
    }

    // Заголовок столбца списка
    public string ColumnName { get; protected init; }

    // Элементы справочника в том порядке, в каком они показаны в представлении
    public ObservableCollection<RefListItem> Items { get; } = [];
    // Выделенные в списке элементы (синхронизируются со списком при помощи SelectedItemsBehavior)
    public ObservableCollection<RefListItem> SelectedItems { get; } = [];

    public RelayCommand LoadedCommand { get; }
    public RelayCommand AddCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand DeleteCommand { get; }

    // Событие для оповещения подписчиков о том, что какой-либо элемент справочника был отредактирован
    // или удален. Нужно для немедленного обновления списка книг в главном окне.
    public event EventHandler Modified;

    protected void OnModified() =>
        Modified?.Invoke(this, EventArgs.Empty);

    // Заново считывает список из модели и показывает его в представлении
    protected abstract void UpdateItems();

    protected abstract void OnAdd();
    protected abstract void OnEdit();
    protected abstract void OnDelete();

    private bool HasSelectedItems() =>
        SelectedItems.Count > 0;

    // Выделенные элементы в порядке их следования в списке
    protected List<RefListItem> GetSelectedItems() =>
        Items.Where(SelectedItems.Contains).ToList();

    // Показывает в представлении новый список элементов
    protected void ShowItems(IEnumerable<RefListItem> items)
    {
        SelectedItems.Clear();
        Items.Clear();
        foreach (RefListItem item in items)
            Items.Add(item);
    }

    // Выделяет указанные элементы (список при этом получает фокус и прокручивается к ним)
    protected void SelectItems(IEnumerable<RefListItem> items)
    {
        SelectedItems.Clear();
        foreach (RefListItem item in items)
            SelectedItems.Add(item);
    }

    protected void RemoveItem(RefListItem item)
    {
        SelectedItems.Remove(item);
        Items.Remove(item);
    }
}
