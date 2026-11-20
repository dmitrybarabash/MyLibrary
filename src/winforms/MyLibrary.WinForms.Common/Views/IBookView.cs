using System;
using System.Collections.Generic;
using MyLibrary.WinForms.Common.Mvp;

namespace MyLibrary.WinForms.Common.Views;

public interface IBookView : IDialogView
{
    string Title { set; }
    string BookName { get; set; }
    int Pages { get; set; }
    decimal Price { get; set; }
    int SelectedAuthorIndex { get; set; }
    int SelectedPressIndex { get; set; }
    bool CanAccept { set; }

    void SetAuthors(IEnumerable<string> authors);
    void SetPresses(IEnumerable<string> presses);

    void ShowError(string message);

    event EventHandler Loaded;
    event EventHandler BookNameChanged;
    event EventHandler AuthorSelected;
    event EventHandler PressSelected;
}
