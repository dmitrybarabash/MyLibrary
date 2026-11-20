namespace MyLibrary.WinForms.Common.Mvp;

public interface IDialogView : IView
{
    // Показывает представление в модальном режиме.
    // Возвращает true, если пользователь подтвердил ввод (нажал OK).
    bool ShowDialog();
}
