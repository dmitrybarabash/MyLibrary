using MyLibrary.Wpf.Common.Mvvm;

namespace MyLibrary.Wpf.Common.Services;

// Служба диалогов. Через нее модели представления открывают модальные окна и окна сообщений,
// не завися от конкретных классов окон (аналог IViewFactory в WinForms-варианте).
public interface IDialogService
{
    // Показывает модальное окно, соответствующее модели представления диалога.
    // Возвращает true, если пользователь подтвердил ввод (нажал OK).
    bool ShowDialog(DialogViewModel viewModel);

    void ShowError(string message);
    bool Confirm(string message, string caption);
}
