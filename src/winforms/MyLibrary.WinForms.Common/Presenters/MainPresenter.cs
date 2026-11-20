using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Utilities;
using MyLibrary.WinForms.Common.Mvp;
using MyLibrary.WinForms.Common.Views;

namespace MyLibrary.WinForms.Common.Presenters;

public class MainPresenter : BasePresenter<IMainView>
{
    private IMyLibraryDomainModel Model { get; set; }
    private IViewFactory ViewFactory { get; set; }
    // Книги в том же порядке, в каком они показаны в представлении
    private List<Book> Books { get; set; } = [];

    public MainPresenter(IMyLibraryDomainModel domainModel, IMainView mainView, IViewFactory viewFactory)
    {
        // Сохраняем внедряемые ссылки на модель, представление и фабрику представлений
        Model = domainModel;
        View = mainView;
        ViewFactory = viewFactory;
        // Подписываемся на события представления
        View.Loaded += OnLoaded;
        View.AddBook += OnAddBook;
        View.EditBook += OnEditBook;
        View.DeleteBook += OnDeleteBook;
        View.EditAuthors += OnEditAuthors;
        View.EditPresses += OnEditPresses;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        // Считываем список книг
        UpdateView();
    }

    private void OnAddBook(object sender, EventArgs e)
    {
        // Таблица Authors не должна быть пустой, поскольку внешний ключ AuthorFk таблицы Books не может быть NULL
        if (Model.AuthorRepository.GetAll().ToList().Count == 0)
        {
            View.ShowError("Справочник \"Авторы\" пуст. Необходимо, чтобы он содержал хотя бы одного автора.");
            return;
        }
        // Таблица Presses не должна быть пустой, поскольку внешний ключ PressFk таблицы Books не может быть NULL
        if (Model.PressRepository.GetAll().ToList().Count == 0)
        {
            View.ShowError("Справочник \"Издательства\" пуст. Необходимо, чтобы он содержал хотя бы одно издательство.");
            return;
        }

        var bookPresenter = new BookPresenter(Model, ViewFactory.CreateBookView())
        {
            Title = "Новая книга",
            AuthorFk = 0,
            PressFk = 0
        };
        if (bookPresenter.View.ShowDialog())
        {
            try
            {
                Model.BookRepository.Add(new Book
                {
                    Id = 0,
                    AuthorFk = bookPresenter.AuthorFk,
                    Name = bookPresenter.View.BookName,
                    Pages = bookPresenter.View.Pages,
                    Price = bookPresenter.View.Price,
                    PressFk = bookPresenter.PressFk
                });
            }
            catch (Exception ex)
            {
                View.ShowError("Произошла ошибка при добавлении новой книги.");
                Utils.Log.Trace(ex);
            }

            // Пересчитываем список книг
            UpdateView();

            // Выделяем добавленную (последнюю в списке) книгу
            if (Books.Count > 0)
                View.SelectBooks([Books.Count - 1]);
        }
        else
        {
            // Восстанавливаем фокус и выделение книг
            View.FocusBookList();
        }
    }

    private void OnEditBook(object sender, EventArgs e)
    {
        // Индексы выделенных элементов в списке книг. Это копия, поэтому она
        // остается актуальной и после того, как UpdateView заново заполнит список.
        IReadOnlyList<int> selectedIndices = View.SelectedBookIndices;

        // Проверяем, есть ли хотя бы один выделенный элемент в списке книг
        if (selectedIndices.Count == 0)
            return;

        // Если выделенных элементов несколько, то берем первый из них
        Book book = Books[selectedIndices[0]];

        var bookPresenter = new BookPresenter(Model, ViewFactory.CreateBookView())
        {
            Title = "Изменение существующей книги",
            AuthorFk = book.AuthorFk,
            PressFk = book.PressFk
        };
        bookPresenter.View.BookName = book.Name;
        bookPresenter.View.Pages = book.Pages;
        bookPresenter.View.Price = book.Price;
        if (bookPresenter.View.ShowDialog())
        {
            try
            {
                book.AuthorFk = bookPresenter.AuthorFk;
                book.Name = bookPresenter.View.BookName;
                book.Pages = bookPresenter.View.Pages;
                book.Price = bookPresenter.View.Price;
                book.PressFk = bookPresenter.PressFk;
                Model.BookRepository.Update(book);
            }
            catch (Exception ex)
            {
                View.ShowError("Произошла ошибка при изменении информации о книге.");
                Utils.Log.Trace(ex);
            }

            // Пересчитываем список книг
            UpdateView();
        }

        // Восстанавливаем выделение
        View.SelectBooks(selectedIndices);
    }

    private void OnDeleteBook(object sender, EventArgs e)
    {
        // Восстанавливаем фокус
        View.FocusBookList();

        // Индексы выделенных элементов в списке книг
        IReadOnlyList<int> selectedIndices = View.SelectedBookIndices;

        if (selectedIndices.Count == 0 ||
            !View.Confirm("Вы действительно хотите удалить эту книгу?", "Удаление информации о книге"))
            return;

        try
        {
            // Пробегаемся по списку выделенных элементов снизу вверх и удаляем их из модели и представления.
            // Если пробегаться по списку выделенных элементов сверху вниз, то после первого же удаления
            // все следующие элементы поднимутся вверх и список выделенных элементов станет не актуален.
            for (int i = selectedIndices.Count - 1; i >= 0; i--)
            {
                int index = selectedIndices[i];
                Model.BookRepository.Delete(Books[index].Id);
                Books.RemoveAt(index);
                View.RemoveBookAt(index);
            }
        }
        catch (Exception ex)
        {
            View.ShowError("Произошла ошибка при удалении книг.");
            Utils.Log.Trace(ex);
        }
    }

    // Заново считываем список книг из модели и показываем его в представлении
    private void UpdateView()
    {
        try
        {
            List<Author> authors = Model.AuthorRepository.GetAll().ToList();
            List<Press> presses = Model.PressRepository.GetAll().ToList();
            List<Book> books = Model.BookRepository.GetAll().ToList();

            // Готовим строки полностью до того, как что-либо менять, чтобы при ошибке
            // список книг в презентере и в представлении остался согласованным
            List<BookListItem> items = books.Select(book => new BookListItem(
                authors.Find(a => a.Id == book.AuthorFk).Name,
                book.Name,
                book.Pages.ToString(),
                decimal.Round(book.Price, 2).ToString("G", CultureInfo.InvariantCulture),
                presses.Find(p => p.Id == book.PressFk).Name)).ToList();

            Books = books;
            View.ShowBooks(items);
        }
        catch (Exception ex)
        {
            View.ShowError("Произошла ошибка при получении списка книг.");
            Utils.Log.Trace(ex);
        }
    }

    private void OnEditAuthors(object sender, EventArgs e)
    {
        var authorsPresenter = new AuthorsPresenter(Model, ViewFactory.CreateRefView(), ViewFactory);
        authorsPresenter.Modified += OnModified;
        authorsPresenter.View.ShowDialog();
        // Эта отписка здесь, видимо, лишняя. Но пусть пока будет.
        authorsPresenter.Modified -= OnModified;
    }

    private void OnEditPresses(object sender, EventArgs e)
    {
        var pressesPresenter = new PressesPresenter(Model, ViewFactory.CreateRefView(), ViewFactory);
        pressesPresenter.Modified += OnModified;
        pressesPresenter.View.ShowDialog();
        // Эта отписка здесь, видимо, лишняя. Но пусть пока будет.
        pressesPresenter.Modified -= OnModified;
    }

    private void OnModified(object sender, EventArgs e)
    {
        // Пересчитываем список книг
        UpdateView();
    }
}
