using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyLibrary.WinForms.Common.Mvp;

namespace MyLibrary.WinForms.Common.Views;

public partial class RefForm : Form, IRefView
{
    public RefForm()
    {
        InitializeComponent();
    }

    #region IRefView implementation

    public string Title { set => Text = value; }

    public string ColumnName { set => columnName.Text = value; }

    public IReadOnlyList<int> SelectedItemIndices => [.. listView.SelectedIndices.Cast<int>()];

    public void ShowItems(IEnumerable<string> items)
    {
        listView.Items.Clear();
        foreach (string item in items)
            listView.Items.Add(item);
    }

    public void UpdateItemAt(int index, string text)
    {
        listView.Items[index].Text = text;
    }

    public void RemoveItemAt(int index)
    {
        listView.Items.RemoveAt(index);
    }

    public void SelectItems(IEnumerable<int> indices)
    {
        listView.Select();
        foreach (int index in indices)
        {
            listView.Items[index].Selected = true;
            listView.EnsureVisible(index);
        }
    }

    public void FocusItemList()
    {
        listView.Select();
    }

    bool IDialogView.ShowDialog()
    {
        return ShowDialog() == DialogResult.OK;
    }

    public void ShowError(string message)
    {
        MessageBoxHelper.ShowError(message);
    }

    public bool Confirm(string message, string caption)
    {
        return MessageBoxHelper.Confirm(message, caption);
    }

    public event EventHandler Loaded;

    private void LoadedHandler(object sender, EventArgs e)
    {
        if (Loaded is not null)
            Loaded(this, EventArgs.Empty);
    }

    public event EventHandler Add;

    private void AddHandler(object sender, EventArgs e)
    {
        if (Add is not null)
            Add(this, EventArgs.Empty);
    }

    public event EventHandler Edit;

    private void EditHandler(object sender, EventArgs e)
    {
        if (Edit is not null)
            Edit(this, EventArgs.Empty);
    }

    public event EventHandler Delete;

    private void DeleteHandler(object sender, EventArgs e)
    {
        if (Delete is not null)
            Delete(this, EventArgs.Empty);
    }

    #endregion

    private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        EditHandler(sender, e);
    }

    private void buttonClose_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }
}
