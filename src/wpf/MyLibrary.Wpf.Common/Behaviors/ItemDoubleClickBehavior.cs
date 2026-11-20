using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyLibrary.Wpf.Common.Behaviors;

// Присоединенное поведение, которое выполняет команду модели представления при двойном щелчке
// левой кнопкой мыши по элементу списка (но не по пустому месту и не по заголовку столбца).
//
// Пример: <ListView behaviors:ItemDoubleClickBehavior.Command="{Binding EditCommand}" ...>
public static class ItemDoubleClickBehavior
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(ItemDoubleClickBehavior),
            new PropertyMetadata(null, OnCommandChanged));

    public static ICommand GetCommand(DependencyObject element) =>
        (ICommand)element.GetValue(CommandProperty);

    public static void SetCommand(DependencyObject element, ICommand value) =>
        element.SetValue(CommandProperty, value);

    private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ItemsControl itemsControl)
            return;

        itemsControl.MouseDoubleClick -= OnMouseDoubleClick;
        if (e.NewValue is not null)
            itemsControl.MouseDoubleClick += OnMouseDoubleClick;
    }

    private static void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var itemsControl = (ItemsControl)sender;

        // Проверяем, что щелкнули левой кнопкой и именно по элементу списка
        if (e.ChangedButton != MouseButton.Left ||
            e.OriginalSource is not DependencyObject source ||
            ItemsControl.ContainerFromElement(itemsControl, source) is null)
            return;

        ICommand command = GetCommand(itemsControl);
        if (command?.CanExecute(null) == true)
        {
            command.Execute(null);
            e.Handled = true;
        }
    }
}
