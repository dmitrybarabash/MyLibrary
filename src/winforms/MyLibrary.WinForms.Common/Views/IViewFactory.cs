namespace MyLibrary.WinForms.Common.Views;

// Создает представления, которые открываются из презентеров,
// чтобы презентеры не зависели от конкретных классов форм
public interface IViewFactory
{
    IBookView CreateBookView();
    IRefView CreateRefView();
    IRefItemView CreateRefItemView();
}
