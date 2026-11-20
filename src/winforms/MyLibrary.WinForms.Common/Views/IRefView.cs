using System;
using System.Collections.Generic;
using MyLibrary.WinForms.Common.Mvp;

namespace MyLibrary.WinForms.Common.Views;

public interface IRefView : IDialogView
{
    string Title { set; }
    string ColumnName { set; }

    // Индексы выделенных элементов в порядке возрастания. Возвращается копия,
    // которая не меняется при последующих изменениях списка.
    IReadOnlyList<int> SelectedItemIndices { get; }

    void ShowItems(IEnumerable<string> items);
    void UpdateItemAt(int index, string text);
    void RemoveItemAt(int index);
    // Переводит фокус на список, выделяет указанные элементы и прокручивает список к ним
    void SelectItems(IEnumerable<int> indices);
    void FocusItemList();

    void ShowError(string message);
    bool Confirm(string message, string caption);

    event EventHandler Loaded;
    event EventHandler Add;
    event EventHandler Edit;
    event EventHandler Delete;
}
