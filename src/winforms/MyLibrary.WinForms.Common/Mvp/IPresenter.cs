namespace MyLibrary.WinForms.Common.Mvp;

public interface IPresenter<T> where T : IView
{
    T View { get; set; }
}
