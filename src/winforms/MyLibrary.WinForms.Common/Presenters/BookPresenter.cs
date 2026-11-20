using System;
using System.Collections.Generic;
using System.Linq;
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

    public BookPresenter(IMyLibraryDomainModel domainModel, IBookView bookView)
    {
        // Сохраняем внедряемые ссылки на модель и представление
        Model = domainModel;
        View = bookView;
        // Подписываемся на события представления
        View.Loaded += OnLoaded;
        View.BookNameChanged += OnBookNameChanged;
        View.AuthorSelected += OnAuthorSelected;
        View.PressSelected += OnPressSelected;
    }

    public string Title { set => View.Title = value; }
    public long AuthorFk { get; set; }
    public long PressFk { get; set; }

    private void OnLoaded(object sender, EventArgs e)
    {
        try
        {
            // Заполняем в представлении выпадающий список авторов
            Authors = Model.AuthorRepository.GetAll().ToList();
            View.SetAuthors(Authors.Select(a => a.Name));
            View.SelectedAuthorIndex = AuthorFk == 0 ? 0 : Authors.FindIndex(a => a.Id == AuthorFk);

            // Заполняем в представлении выпадающий список издательств
            Presses = Model.PressRepository.GetAll().ToList();
            View.SetPresses(Presses.Select(p => p.Name));
            View.SelectedPressIndex = PressFk == 0 ? 0 : Presses.FindIndex(a => a.Id == PressFk);
        }
        catch (Exception ex)
        {
            View.ShowError("Произошла ошибка при получении авторов и/или издательств.");
            Utils.Log.Trace(ex);
        }
    }

    private void OnBookNameChanged(object sender, EventArgs e)
    {
        // Подтвердить ввод можно, только если название книги не пустое
        View.CanAccept = View.BookName.Length > 0;
    }

    private void OnAuthorSelected(object sender, EventArgs e)
    {
        // При выборе пользователем из выпадающего списка другого автора, запоминаем его Id
        AuthorFk = Authors[View.SelectedAuthorIndex].Id;
    }

    private void OnPressSelected(object sender, EventArgs e)
    {
        // При выборе пользователем из выпадающего списка другого издательства, запоминаем его Id
        PressFk = Presses[View.SelectedPressIndex].Id;
    }
}
