using System;
using System.Windows.Input;

namespace MyLibrary.Wpf.Common.Mvvm;

// Команда, которая делегирует выполнение и проверку доступности методам модели представления.
// Представление привязывает к таким командам кнопки, пункты меню и сочетания клавиш.
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    // WPF сам перепроверяет доступность команд после ввода с клавиатуры, щелчков мышью,
    // смены фокуса и т.п., поэтому просто подписываем подписчиков на CommandManager.RequerySuggested
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) =>
        _canExecute?.Invoke() ?? true;

    public void Execute(object parameter) =>
        _execute();

    // Принудительно перепроверяет доступность команд. Нужно, когда состояние, от которого
    // зависит доступность, изменилось программно, а не в результате действий пользователя.
    public void RaiseCanExecuteChanged() =>
        CommandManager.InvalidateRequerySuggested();
}
