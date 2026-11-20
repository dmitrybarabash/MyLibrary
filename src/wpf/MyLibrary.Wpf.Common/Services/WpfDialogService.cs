using System;
using System.Linq;
using System.Windows;
using MyLibrary.Wpf.Common.Mvvm;
using MyLibrary.Wpf.Common.ViewModels;
using MyLibrary.Wpf.Common.Views;

namespace MyLibrary.Wpf.Common.Services;

// Реализация службы диалогов на WPF (аналог WinFormsViewFactory в WinForms-варианте).
// Это единственное место, которое знает, какое окно соответствует какой модели представления.
public class WpfDialogService : IDialogService
{
    public bool ShowDialog(DialogViewModel viewModel)
    {
        Window window = viewModel switch
        {
            BookViewModel => new BookWindow(),
            RefViewModel => new RefWindow(),
            RefItemViewModel => new RefItemWindow(),
            _ => throw new ArgumentException(
                $"Для модели представления {viewModel.GetType().Name} нет окна.",
                nameof(viewModel))
        };
        window.DataContext = viewModel;
        window.Owner = GetActiveWindow();

        // Модель представления может сама попросить закрыть окно (например, по кнопке OK)
        void OnCloseRequested(object sender, bool dialogResult) =>
            window.DialogResult = dialogResult;

        viewModel.CloseRequested += OnCloseRequested;
        try
        {
            return window.ShowDialog() == true;
        }
        finally
        {
            viewModel.CloseRequested -= OnCloseRequested;
        }
    }

    public void ShowError(string message) =>
        ShowMessage(message, "Что-то пошло не так", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

    public bool Confirm(string message, string caption) =>
        ShowMessage(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes;

    private static MessageBoxResult ShowMessage(string message, string caption, MessageBoxButton button,
        MessageBoxImage icon, MessageBoxResult defaultResult)
    {
        Window owner = GetActiveWindow();
        return owner is not null
            ? MessageBox.Show(owner, message, caption, button, icon, defaultResult)
            : MessageBox.Show(message, caption, button, icon, defaultResult);
    }

    // Активное окно приложения, которое станет владельцем нового окна
    private static Window GetActiveWindow() =>
        Application.Current?.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
            ?? Application.Current?.MainWindow;
}
