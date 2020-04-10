using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Mvp;
using MyLibrary.Common.Utilities;
using MyLibrary.Common.Views;

namespace MyLibrary.Common.Presenters
{
    public class AuthorsPresenter : BasePresenter<IRefView>
    {
        private IMyLibraryDomainModel Model { get; set; }

        public AuthorsPresenter(IMyLibraryDomainModel domainModel, IRefView refView)
        {
            // Сохраняем внедряемые ссылки на модель и представление
            Model = domainModel;
            View = refView;
            // Настраиваем представление
            View.Title = "Авторы";
            View.ListViewTheOnlyColumnName = "Автор";
            // Подписываемся на события представления
            View.Loaded += OnLoaded;
            View.Add += OnAdd;
            View.Edit += OnEdit;
            View.Delete += OnDelete;
        }

        // Событие для оповещения подписчиков о том, что какой-либо автор был отредактирован
        // или удален. Нужно для немедленного обновления списка книг в главном окне.
        public event EventHandler Modified;

        private void ModifiedHandler()
        {
            if (Modified != null)
                Modified(this, EventArgs.Empty);
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            // Считываем список авторов
            UpdateView();
        }

        private void OnAdd(object sender, EventArgs e)
        {
            var refItemForm = new RefItemForm();
            refItemForm.Text = "Новый автор";
            refItemForm.GroupBoxText = "Информация о новом авторе";
            refItemForm.LabelText = "Автор:";

            if (refItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var newAuthor = new Author { Id = 0, Name = refItemForm.ItemText };
                    Model.AuthorRepository.Add(newAuthor);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при добавлении нового автора.", "Что-то пошло не так",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }

                // Пересчитываем список авторов
                UpdateView();

                // Выделяем добавленного (последнего в списке) автора
                View.ListView.Select();
                View.ListView.Items[View.ListView.Items.Count - 1].Selected = true;
                View.ListView.EnsureVisible(View.ListView.Items.Count - 1);
            }
            else
            {
                // Восстанавливаем фокус и выделение авторов
                View.ListView.Select();
            }
        }

        private void OnEdit(object sender, EventArgs e)
        {
            // Проверяем, есть ли хотя бы один выделенный элемент в списке книг
            if (View.ListView.SelectedItems.Count == 0)
                return;

            // Если выделенных элементов несколько, то берем первый из них
            int selectedIndex = View.ListView.SelectedIndices[0];
            Author author = (Author)View.ListView.Items[selectedIndex].Tag;

            var refItemForm = new RefItemForm();
            refItemForm.Text = "Изменение существующего автора";
            refItemForm.GroupBoxText = "Информация об авторе";
            refItemForm.LabelText = "Автор:";
            refItemForm.ItemText = author.Name;

            if (refItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    author.Name = refItemForm.ItemText;
                    Model.AuthorRepository.Update(author);
                    View.ListView.Items[selectedIndex].Text = author.Name;

                    // Оповещаем подписчиков о том, что произошли изменения
                    ModifiedHandler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при изменении информации об авторе.", "Что-то пошло не так",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            // Восстанавливаем фокус
            View.ListView.Focus();

            ListView.SelectedIndexCollection selectedIndices = View.ListView.SelectedIndices;

            if (selectedIndices.Count == 0 || MessageBox.Show("Будут удалены не только эти авторы, но и все их книги.\n\nВы уверены, что хотите это всё удалить?",
                "Удаление авторов и всех их книг", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                // Пробегаемся по списку выделенных элементов снизу вверх и удаляем их из модели и представления.
                // Если пробегаться по списку выделенных элементов сверху вниз, то после первого же удаления
                // все следующие элементы поднимутся вверх и список выделенных элементов станет не актуален.
                for (int i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    Model.AuthorRepository.Delete(((Author)View.ListView.Items[selectedIndices[i]].Tag).Id);
                    View.ListView.Items.RemoveAt(selectedIndices[i]);
                }

                // Оповещаем подписчиков о том, что произошли изменения
                ModifiedHandler();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении авторов.", "Что-то пошло не так",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }

        // Очищаем список и заполняем его заново из модели
        private void UpdateView()
        {
            try
            {
                IEnumerable<Author> authors = Model.AuthorRepository.GetAll();

                View.ListView.Items.Clear();
                foreach (var author in authors)
                {
                    ListViewItem item = View.ListView.Items.Add(new ListViewItem());
                    item.Text = author.Name;
                    item.Tag = author;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при получении списка авторов.", "Что-то пошло не так",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }
    }
}