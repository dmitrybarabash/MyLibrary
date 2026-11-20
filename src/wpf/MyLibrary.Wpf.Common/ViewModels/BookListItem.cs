using System.Globalization;
using MyLibrary.Common.DomainModel;

namespace MyLibrary.Wpf.Common.ViewModels;

// Строка списка книг в главном окне. Все значения уже подготовлены для отображения.
public class BookListItem(Book book, string author, string press)
{
    // Книга, которую представляет эта строка
    public Book Book { get; } = book;

    public string Author { get; } = author;
    public string Name { get; } = book.Name;
    public string Pages { get; } = book.Pages.ToString();
    public string Price { get; } = decimal.Round(book.Price, 2).ToString("G", CultureInfo.InvariantCulture);
    public string Press { get; } = press;
}
