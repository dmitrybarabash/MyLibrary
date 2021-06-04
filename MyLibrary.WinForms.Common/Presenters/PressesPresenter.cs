using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Utilities;
using MyLibrary.WinForms.Common.Mvp;
using MyLibrary.WinForms.Common.Views;

namespace MyLibrary.WinForms.Common.Presenters
{
    public class PressesPresenter : BasePresenter<IRefView>
    {
        private IMyLibraryDomainModel Model { get; set; }

        public PressesPresenter(IMyLibraryDomainModel domainModel, IRefView refView)
        {
            // Сохраняем внедряемые ссылки на модель и представление
            Model = domainModel;
            View = refView;
            // Настраиваем представление
            View.Title = "Издательства";
            View.ListViewTheOnlyColumnName = "Название";
            // Подписываемся на события представления
            View.Loaded += OnLoaded;
            View.Add += OnAdd;
            View.Edit += OnEdit;
            View.Delete += OnDelete;
        }

        // Событие для оповещения подписчиков о том, что какое-либо издательство было отредактировано
        // или удалено. Нужно для немедленного обновления списка книг в главном окне.
        public event EventHandler Modified;

        private void ModifiedHandler()
        {
            if (Modified is not null)
                Modified(this, EventArgs.Empty);
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            // Считываем список издательств
            UpdateView();
        }

        private void OnAdd(object sender, EventArgs e)
        {
            var refItemForm = new RefItemForm();
            refItemForm.Text = "Новое издательство";
            refItemForm.GroupBoxText = "Информация о новом издательстве";
            refItemForm.LabelText = "Издательство:";

            if (refItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var newPress = new Press { Id = 0, Name = refItemForm.ItemText };
                    Model.PressRepository.Add(newPress);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при добавлении нового издательства.", "Что-то пошло не так",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utils.Log.Trace(ex);
                }

                // Пересчитываем список издательств
                UpdateView();

                // Выделяем добавленное (последнее в списке) издательство
                View.ListView.Select();
                View.ListView.Items[View.ListView.Items.Count - 1].Selected = true;
                View.ListView.EnsureVisible(View.ListView.Items.Count - 1);
            }
            else
            {
                // Восстанавливаем фокус и выделение издательств
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
            Press press = (Press)View.ListView.Items[selectedIndex].Tag;

            var refItemForm = new RefItemForm();
            refItemForm.Text = "Изменение существующего издательства";
            refItemForm.GroupBoxText = "Информация об издательстве";
            refItemForm.LabelText = "Издательство:";
            refItemForm.ItemText = press.Name;

            if (refItemForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    press.Name = refItemForm.ItemText;
                    Model.PressRepository.Update(press);
                    View.ListView.Items[selectedIndex].Text = press.Name;

                    // Оповещаем подписчиков о том, что произошли изменения
                    ModifiedHandler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при изменении информации об издательстве.", "Что-то пошло не так",
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

            if (selectedIndices.Count == 0 || MessageBox.Show("Будут удалены не только эти издательства, но и все изданные ими книги.\n\nВы уверены, что хотите это всё удалить?",
                "Удаление издательств и всех их книг", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                // Пробегаемся по списку выделенных элементов снизу вверх и удаляем их из модели и представления.
                // Если пробегаться по списку выделенных элементов сверху вниз, то после первого же удаления
                // все следующие элементы поднимутся вверх и список выделенных элементов станет не актуален.
                for (int i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    Model.PressRepository.Delete(((Press)View.ListView.Items[selectedIndices[i]].Tag).Id);
                    View.ListView.Items.RemoveAt(selectedIndices[i]);
                }

                // Оповещаем подписчиков о том, что произошли изменения
                ModifiedHandler();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении издательств.", "Что-то пошло не так",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }

        }

        // Очищаем список и заполняем его заново из модели
        private void UpdateView()
        {
            try
            {
                IEnumerable<Press> presses = Model.PressRepository.GetAll();

                View.ListView.Items.Clear();
                foreach (var press in presses)
                {
                    ListViewItem item = View.ListView.Items.Add(new ListViewItem());
                    item.Text = press.Name;
                    item.Tag = press;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при получении списка издательств.", "Что-то пошло не так",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utils.Log.Trace(ex);
            }
        }
    }
}