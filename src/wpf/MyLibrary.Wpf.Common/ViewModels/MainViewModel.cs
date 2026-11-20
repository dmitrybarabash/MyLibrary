using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Utilities;
using MyLibrary.Wpf.Common.Mvvm;
using MyLibrary.Wpf.Common.Services;

namespace MyLibrary.Wpf.Common.ViewModels;

public class MainViewModel : ViewModelBase
{
    private IMyLibraryDomainModel Model { get; }
    private IDialogService DialogService { get; }

    public MainViewModel(IMyLibraryDomainModel domainModel, IDialogService dialogService)
    {
        // Сохраняем внедряемые ссылки на модель и службу диалогов
        Model = domainModel;
        DialogService = dialogService;
        // Создаем команды. При загрузке окна считываем список книг.
        LoadedCommand = new RelayCommand(UpdateBooks);
        AddBookCommand = new RelayCommand(OnAddBook);
        EditBookCommand = new RelayCommand(OnEditBook, HasSelectedBooks);
        DeleteBookCommand = new RelayCommand(OnDeleteBook, HasSelectedBooks);
        EditAuthorsCommand = new RelayCommand(OnEditAuthors);
        EditPressesCommand = new RelayCommand(OnEditPresses);
        // Доступность команд изменения и удаления зависит от того, выделено ли что-нибудь в списке
        SelectedBooks.CollectionChanged += (_, _) =>
        {
            EditBookCommand.RaiseCanExecuteChanged();
            DeleteBookCommand.RaiseCanExecuteChanged();
        };
    }

    // Книги в том порядке, в каком они показаны в представлении
    public ObservableCollection<BookListItem> Books { get; } = [];
    // Выделенные в списке книги (синхронизируются со списком при помощи SelectedItemsBehavior)
    public ObservableCollection<BookListItem> SelectedBooks { get; } = [];

    public RelayCommand LoadedCommand { get; }
    public RelayCommand AddBookCommand { get; }
    public RelayCommand EditBookCommand { get; }
    public RelayCommand DeleteBookCommand { get; }
    public RelayCommand EditAuthorsCommand { get; }
    public RelayCommand EditPressesCommand { get; }

    private bool HasSelectedBooks() =>
        SelectedBooks.Count > 0;

    private void OnAddBook()
    {
        try
        {
            // Таблица Authors не должна быть пустой, поскольку внешний ключ AuthorFk таблицы Books не может быть NULL
            if (!Model.AuthorRepository.GetAll().Any())
            {
                DialogService.ShowError(
                    "Справочник \"Авторы\" пуст. Необходимо, чтобы он содержал хотя бы одного автора.");
                return;
            }
            // Таблица Presses не должна быть пустой, поскольку внешний ключ PressFk таблицы Books не может быть NULL
            if (!Model.PressRepository.GetAll().Any())
            {
                DialogService.ShowError(
                    "Справочник \"Издательства\" пуст. Необходимо, чтобы он содержал хотя бы одно издательство.");
                return;
            }
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при получении авторов и/или издательств.");
            Utils.Log.Trace(ex);
            return;
        }

        var bookViewModel = new BookViewModel(Model, DialogService)
        {
            Title = "Новая книга",
            AuthorFk = 0,
            PressFk = 0
        };

        if (!DialogService.ShowDialog(bookViewModel))
            return;

        try
        {
            Model.BookRepository.Add(new Book
            {
                Id = 0,
                AuthorFk = bookViewModel.AuthorFk,
                Name = bookViewModel.BookName,
                Pages = bookViewModel.Pages,
                Price = bookViewModel.Price,
                PressFk = bookViewModel.PressFk
            });
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при добавлении новой книги.");
            Utils.Log.Trace(ex);
        }

        // Пересчитываем список книг
        UpdateBooks();

        // Выделяем добавленную (последнюю в списке) книгу
        if (Books.Count > 0)
            SelectBooks([Books[^1]]);
    }

    private void OnEditBook()
    {
        // Выделенные книги в порядке их следования в списке
        List<BookListItem> selectedItems = Books.Where(SelectedBooks.Contains).ToList();

        // Проверяем, есть ли хотя бы одна выделенная книга
        if (selectedItems.Count == 0)
            return;

        // Если выделенных книг несколько, то берем первую из них
        Book book = selectedItems[0].Book;

        var bookViewModel = new BookViewModel(Model, DialogService)
        {
            Title = "Изменение существующей книги",
            AuthorFk = book.AuthorFk,
            PressFk = book.PressFk,
            BookName = book.Name,
            Pages = book.Pages,
            Price = book.Price
        };

        if (!DialogService.ShowDialog(bookViewModel))
            return;

        try
        {
            book.AuthorFk = bookViewModel.AuthorFk;
            book.Name = bookViewModel.BookName;
            book.Pages = bookViewModel.Pages;
            book.Price = bookViewModel.Price;
            book.PressFk = bookViewModel.PressFk;
            Model.BookRepository.Update(book);
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при изменении информации о книге.");
            Utils.Log.Trace(ex);
        }

        // Пересчитываем список книг
        UpdateBooks();

        // Восстанавливаем выделение. После пересчета строки списка новые, поэтому ищем их по Id книг.
        var selectedIds = selectedItems.Select(item => item.Book.Id).ToHashSet();
        SelectBooks(Books.Where(item => selectedIds.Contains(item.Book.Id)));
    }

    private void OnDeleteBook()
    {
        // Выделенные книги в порядке их следования в списке
        List<BookListItem> selectedItems = Books.Where(SelectedBooks.Contains).ToList();

        if (selectedItems.Count == 0 ||
            !DialogService.Confirm("Вы действительно хотите удалить эту книгу?", "Удаление информации о книге"))
            return;

        try
        {
            foreach (BookListItem item in selectedItems)
            {
                Model.BookRepository.Delete(item.Book.Id);
                SelectedBooks.Remove(item);
                Books.Remove(item);
            }
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при удалении книг.");
            Utils.Log.Trace(ex);
        }
    }

    private void OnEditAuthors()
    {
        var authorsViewModel = new AuthorsViewModel(Model, DialogService);
        authorsViewModel.Modified += OnModified;
        DialogService.ShowDialog(authorsViewModel);
        authorsViewModel.Modified -= OnModified;
    }

    private void OnEditPresses()
    {
        var pressesViewModel = new PressesViewModel(Model, DialogService);
        pressesViewModel.Modified += OnModified;
        DialogService.ShowDialog(pressesViewModel);
        pressesViewModel.Modified -= OnModified;
    }

    private void OnModified(object sender, EventArgs e)
    {
        // Пересчитываем список книг
        UpdateBooks();
    }

    // Заново считываем список книг из модели и показываем его в представлении
    private void UpdateBooks()
    {
        try
        {
            List<Author> authors = Model.AuthorRepository.GetAll().ToList();
            List<Press> presses = Model.PressRepository.GetAll().ToList();

            // Готовим строки полностью до того, как что-либо менять, чтобы при ошибке
            // список книг в представлении остался прежним
            List<BookListItem> items = Model.BookRepository.GetAll().Select(book => new BookListItem(
                book,
                authors.Find(a => a.Id == book.AuthorFk).Name,
                presses.Find(p => p.Id == book.PressFk).Name)).ToList();

            SelectedBooks.Clear();
            Books.Clear();
            foreach (BookListItem item in items)
                Books.Add(item);
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при получении списка книг.");
            Utils.Log.Trace(ex);
        }
    }

    // Выделяет указанные книги (список при этом получает фокус и прокручивается к ним)
    private void SelectBooks(IEnumerable<BookListItem> books)
    {
        SelectedBooks.Clear();
        foreach (BookListItem book in books)
            SelectedBooks.Add(book);
    }
}
