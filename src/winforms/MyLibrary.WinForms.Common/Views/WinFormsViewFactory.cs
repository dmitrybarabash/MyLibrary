namespace MyLibrary.WinForms.Common.Views;

public class WinFormsViewFactory : IViewFactory
{
    public IBookView CreateBookView() => new BookForm();
    public IRefView CreateRefView() => new RefForm();
    public IRefItemView CreateRefItemView() => new RefItemForm();
}
