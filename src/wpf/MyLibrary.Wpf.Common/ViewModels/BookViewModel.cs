using System;
using System.Collections.Generic;
using System.Linq;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Utilities;
using MyLibrary.Wpf.Common.Mvvm;
using MyLibrary.Wpf.Common.Services;

namespace MyLibrary.Wpf.Common.ViewModels;

// Модель представления диалога добавления/изменения книги
public class BookViewModel : DialogViewModel
{
    private IMyLibraryDomainModel Model { get; }
    private IDialogService DialogService { get; }

    public BookViewModel(IMyLibraryDomainModel domainModel, IDialogService dialogService)
    {
        // Сохраняем внедряемые ссылки на модель и службу диалогов
        Model = domainModel;
        DialogService = dialogService;
        // Создаем команды
        LoadedCommand = new RelayCommand(OnLoaded);
        OkCommand = new RelayCommand(OnOk, CanAccept);
    }

    // Id автора и издательства книги. Для новой книги — 0 (тогда выбираются первые в списках).
    public long AuthorFk { get; set; }
    public long PressFk { get; set; }

    public string BookName { get; set => SetProperty(ref field, value); } = "";
    public int Pages { get; set => SetProperty(ref field, value); } = 300;
    public decimal Price { get; set => SetProperty(ref field, value); }

    // Содержимое выпадающих списков авторов и издательств
    public IReadOnlyList<Author> Authors { get; private set => SetProperty(ref field, value); } = [];
    public IReadOnlyList<Press> Presses { get; private set => SetProperty(ref field, value); } = [];

    public Author SelectedAuthor
    {
        get;
        set
        {
            // При выборе пользователем из выпадающего списка другого автора, запоминаем его Id.
            // Значение null приходит, когда выпадающий список заполняется заново, — его пропускаем.
            if (SetProperty(ref field, value) && value is not null)
                AuthorFk = value.Id;
        }
    }

    public Press SelectedPress
    {
        get;
        set
        {
            // При выборе пользователем из выпадающего списка другого издательства, запоминаем его Id
            if (SetProperty(ref field, value) && value is not null)
                PressFk = value.Id;
        }
    }

    public RelayCommand LoadedCommand { get; }
    public RelayCommand OkCommand { get; }

    private void OnLoaded()
    {
        try
        {
            // Заполняем выпадающий список авторов и выбираем в нем автора книги
            List<Author> authors = Model.AuthorRepository.GetAll().ToList();
            Authors = authors;
            SelectedAuthor = AuthorFk == 0 ? authors.FirstOrDefault() : authors.Find(a => a.Id == AuthorFk);

            // Заполняем выпадающий список издательств и выбираем в нем издательство книги
            List<Press> presses = Model.PressRepository.GetAll().ToList();
            Presses = presses;
            SelectedPress = PressFk == 0 ? presses.FirstOrDefault() : presses.Find(p => p.Id == PressFk);
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при получении авторов и/или издательств.");
            Utils.Log.Trace(ex);
        }
    }

    // Подтвердить ввод можно, только если название книги не пустое
    private bool CanAccept() =>
        !string.IsNullOrWhiteSpace(BookName);

    private void OnOk()
    {
        BookName = BookName.Trim();
        Close(true);
    }
}
