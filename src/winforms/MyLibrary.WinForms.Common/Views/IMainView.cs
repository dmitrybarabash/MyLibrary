using System;
using System.Collections.Generic;
using MyLibrary.WinForms.Common.Mvp;

namespace MyLibrary.WinForms.Common.Views;

public interface IMainView : IView
{
    // Индексы выделенных книг в порядке возрастания. Возвращается копия,
    // которая не меняется при последующих изменениях списка.
    IReadOnlyList<int> SelectedBookIndices { get; }

    void ShowBooks(IEnumerable<BookListItem> books);
    void RemoveBookAt(int index);
    // Переводит фокус на список книг, выделяет указанные книги и прокручивает список к ним
    void SelectBooks(IEnumerable<int> indices);
    void FocusBookList();

    void ShowError(string message);
    bool Confirm(string message, string caption);

    event EventHandler Loaded;
    event EventHandler AddBook;
    event EventHandler EditBook;
    event EventHandler DeleteBook;
    event EventHandler EditAuthors;
    event EventHandler EditPresses;
}
