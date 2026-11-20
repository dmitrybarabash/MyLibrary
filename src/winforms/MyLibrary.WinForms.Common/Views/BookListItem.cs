namespace MyLibrary.WinForms.Common.Views;

// Строка списка книг в главном окне. Все значения уже подготовлены презентером для отображения.
public record BookListItem(string Author, string Name, string Pages, string Price, string Press);
