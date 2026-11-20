using System.Windows;
using System.Windows.Input;

namespace MyLibrary.Wpf.Common.Behaviors;

// Присоединенное поведение, которое выполняет команду модели представления, когда элемент
// (обычно окно) загружен. Аналог события Loaded представлений в WinForms-варианте.
//
// Пример: <Window behaviors:LoadedBehavior.Command="{Binding LoadedCommand}" ...>
public static class LoadedBehavior
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(LoadedBehavior),
            new PropertyMetadata(null, OnCommandChanged));

    public static ICommand GetCommand(DependencyObject element) =>
        (ICommand)element.GetValue(CommandProperty);

    public static void SetCommand(DependencyObject element, ICommand value) =>
        element.SetValue(CommandProperty, value);

    private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element)
            return;

        element.Loaded -= OnLoaded;
        if (e.NewValue is null)
            return;

        // Привязка может установить команду уже после того, как элемент загружен
        // (такое бывает, например, при первом показе окна). Тогда выполняем ее сразу.
        if (element.IsLoaded)
            Execute(element);
        else
            element.Loaded += OnLoaded;
    }

    private static void OnLoaded(object sender, RoutedEventArgs e) =>
        Execute((FrameworkElement)sender);

    private static void Execute(FrameworkElement element)
    {
        ICommand command = GetCommand(element);
        if (command?.CanExecute(null) == true)
            command.Execute(null);
    }
}
