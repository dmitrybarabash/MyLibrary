using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Utilities;
using MyLibrary.WinForms.Common.Mvp;
using MyLibrary.WinForms.Common.Views;

namespace MyLibrary.WinForms.Common.Presenters;

public class BookPresenter : BasePresenter<IBookView>
{
    private IMyLibraryDomainModel Model { get; set; }
    private List<Author> Authors { get; set; }
    private List<Press> Presses { get; set; }

    public BookPresenter(IMyLibraryDomainModel domainModel, IBookView mainView)
    {
        // Сохраняем внедряемые ссылки на модель и представление
        Model = domainModel;
        View = mainView;
        // Подписываемся на события представления
        View.Loaded += OnLoaded;
        View.AuthorSelected += OnAuthorSelected;
        View.PressSelected += OnPressSelected;
    }

    public string Title { set => ((Form)View).Text = value; }
    public long AuthorFk { get; set; }
    public long PressFk { get; set; }

    private void OnLoaded(object sender, EventArgs e)
    {
        try
        {
            // Заполняем в представлении выпадающий список авторов
            Authors = Model.AuthorRepository.GetAll().ToList();
            foreach (var author in Authors)
                View.AuthorComboBox.Items.Add(author.Name);
            View.AuthorComboBox.SelectedIndex = AuthorFk == 0 ? 0 : Authors.FindIndex(a => a.Id == AuthorFk);

            // Заполняем в представлении выпадающий список издательств
            Presses = Model.PressRepository.GetAll().ToList();
            foreach (var press in Presses)
                View.PressComboBox.Items.Add(press.Name);
            View.PressComboBox.SelectedIndex = PressFk == 0 ? 0 : Presses.FindIndex(a => a.Id == PressFk);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Произошла ошибка при получении авторов и/или издательств.", "Что-то пошло не так",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            Utils.Log.Trace(ex);
        }
    }

    private void OnAuthorSelected(object sender, EventArgs e)
    {
        // При выборе пользователем из выпадающего списка другого автора, запоминаем его Id
        AuthorFk = Authors[View.AuthorComboBox.SelectedIndex].Id;
    }

    private void OnPressSelected(object sender, EventArgs e)
    {
        // При выборе пользователем из выпадающего списка другого издательства, запоминаем его Id
        PressFk = Presses[View.PressComboBox.SelectedIndex].Id;
    }
}
