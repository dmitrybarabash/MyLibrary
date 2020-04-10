using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Mvp;
using MyLibrary.Common.Utilities;
using MyLibrary.Common.Views;

namespace MyLibrary.Common.Presenters
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        private IMyLibraryDomainModel Model { get; set; }

        public MainPresenter(IMyLibraryDomainModel domainModel, IMainView mainView)
        {
            // Сохраняем внедряемые ссылки на модель и представление
            Model = domainModel;
            View = mainView;
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
                MessageBox.Show("Справочник \"Авторы\" пуст. Необходимо, чтобы он содержал хотя бы одного автора.",
                    "Что-то пошло не так", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Таблица Presses не должна быть пустой, поскольку внешний ключ PressFk таблицы Books не может быть NULL
            if (Model.PressRepository.GetAll().ToList().Count == 0)
            {
                MessageBox.Show("Справочник \"Издательства\" пуст. Необходимо, чтобы он содержал хотя бы одно издательство.",
                    "Что-то пошло не так", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var bookPresenter = new BookPresenter(Model, new BookForm());
            bookPresenter.Title = "Новая книга";
            bookPresenter.AuthorFk = 0;
            bookPresenter.PressFk = 0;
            if (((Form)bookPresenter.View).ShowDialog() == DialogResult.OK)
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
                    MessageBox.Show("Произошла ошибка при добавлении новой книги.", "Что-то пошло не так",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }

                // Пересчитываем список книг
                UpdateView();

                // Выделяем добавленную (последнюю в списке) книгу
                View.ListView.Select();
                View.ListView.Items[View.ListView.Items.Count - 1].Selected = true;
                View.ListView.EnsureVisible(View.ListView.Items.Count - 1);
            }
            else
            {
                // Восстанавливаем фокус и выделение книг
                View.ListView.Select();
            }
        }

        private void OnEditBook(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы один выделенный элемент в списке книг
            if (View.ListView.SelectedItems.Count == 0)
                return;

            // Коллекция выделенных элементов в списке книг представления.
            // Делаем копию, потому что View.ListView.SelectedIndices возвращает ссылку
            // на актуальную коллекцию, а она будет очищена при вызове UpdateView.
            var selectedIndices = new int[View.ListView.SelectedIndices.Count];
            View.ListView.SelectedIndices.CopyTo(selectedIndices, 0);

            // Если выделенных элементов несколько, то берем первый из них
            int selectedIndex = selectedIndices[0];
            Book book = (Book)View.ListView.Items[selectedIndex].Tag;

            var bookPresenter = new BookPresenter(Model, new BookForm());
            bookPresenter.Title = "Изменение существующей книги";
            bookPresenter.AuthorFk = book.AuthorFk;
            bookPresenter.View.BookName = book.Name;
            bookPresenter.View.Pages = book.Pages;
            bookPresenter.View.Price = book.Price;
            bookPresenter.PressFk = book.PressFk;
            if (((Form)bookPresenter.View).ShowDialog() == DialogResult.OK)
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
                    MessageBox.Show("Произошла ошибка при изменении информации о книге.", "Что-то пошло не так",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }

                // Пересчитываем список книг
                UpdateView();
            }

            // Восстанавливаем выделение
            View.ListView.Select();
            foreach (int index in selectedIndices)
            {
                View.ListView.Items[index].Selected = true;
                View.ListView.EnsureVisible(index);
            }
        }

        private void OnDeleteBook(object sender, EventArgs e)
        {
            // Восстанавливаем фокус
            View.ListView.Focus();

            // Коллекция выделенных элементов в списке книг представления
            ListView.SelectedIndexCollection selectedIndices = View.ListView.SelectedIndices;

            if (selectedIndices.Count == 0 || MessageBox.Show("Вы действительно хотите удалить эту книгу?",
                "Удаление информации о книге", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                // Пробегаемся по списку выделенных элементов снизу вверх и удаляем их из модели и представления.
                // Если пробегаться по списку выделенных элементов сверху вниз, то после первого же удаления
                // все следующие элементы поднимутся вверх и список выделенных элементов станет не актуален.
                for (int i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    Model.BookRepository.Delete(((Book)View.ListView.Items[selectedIndices[i]].Tag).Id);
                    View.ListView.Items.RemoveAt(selectedIndices[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении книг.", "Что-то пошло не так",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }

        // Очищаем список книг в представлении и заполняем его заново из модели
        private void UpdateView()
        {
            try
            {
                List<Author> authors = Model.AuthorRepository.GetAll().ToList();
                List<Press> presses = Model.PressRepository.GetAll().ToList();
                IEnumerable<Book> books = Model.BookRepository.GetAll();

                View.ListView.Items.Clear();
                foreach (var book in books)
                {
                    ListViewItem item = View.ListView.Items.Add(new ListViewItem());
                    item.Text = authors.Find(a => a.Id == book.AuthorFk).Name;
                    item.SubItems.Add(book.Name);
                    item.SubItems.Add(book.Pages.ToString());
                    item.SubItems.Add(decimal.Round(book.Price, 2).ToString("G", CultureInfo.InvariantCulture));
                    item.SubItems.Add(presses.Find(p => p.Id == book.PressFk).Name);
                    item.Tag = book;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при получении списка книг.", "Что-то пошло не так",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }

        private void OnEditAuthors(object sender, EventArgs e)
        {
            var authorsPresenter = new AuthorsPresenter(Model, new RefForm());
            authorsPresenter.Modified += OnModified;
            ((Form)authorsPresenter.View).ShowDialog();
            // Эта отписка здесь, видимо, лишняя. Но пусть пока будет.
            authorsPresenter.Modified -= OnModified;
        }

        private void OnEditPresses(object sender, EventArgs e)
        {
            var pressesPresenter = new PressesPresenter(Model, new RefForm());
            pressesPresenter.Modified += OnModified;
            ((Form)pressesPresenter.View).ShowDialog();
            // Эта отписка здесь, видимо, лишняя. Но пусть пока будет.
            pressesPresenter.Modified -= OnModified;
        }

        private void OnModified(object sender, EventArgs e)
        {
            // Пересчитываем список книг
            UpdateView();
        }
    }
}