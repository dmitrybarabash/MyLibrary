using System;

namespace MyLibrary.Wpf.Common.Mvvm;

// Базовый класс моделей представления модальных диалогов
public abstract class DialogViewModel : ViewModelBase
{
    // Заголовок окна диалога
    public string Title { get; init; }

    // Запрос на закрытие окна диалога с указанным результатом (true — пользователь подтвердил ввод).
    // Модель представления ничего не знает об окне, поэтому само окно закрывает служба диалогов.
    public event EventHandler<bool> CloseRequested;

    protected void Close(bool dialogResult) =>
        CloseRequested?.Invoke(this, dialogResult);
}
