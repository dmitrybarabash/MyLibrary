namespace MyLibrary.WinForms.Common.Mvp;

public abstract class BasePresenter<T> : IPresenter<T> where T : IView
{
    public T View { get; set; }
}
