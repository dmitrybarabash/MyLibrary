using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyLibrary.WinForms.Common.Mvp;

namespace MyLibrary.WinForms.Common.Views;

public partial class BookForm : Form, IBookView
{
    public BookForm()
    {
        InitializeComponent();
    }

    #region IBookView implementation

    public string Title { set => Text = value; }

    public string BookName
    {
        get => textBoxName.Text.Trim();
        set => textBoxName.Text = value;
    }

    public int Pages
    {
        get => Convert.ToInt32(numericUpDownPages.Value);
        set => numericUpDownPages.Value = value;
    }

    public decimal Price
    {
        get => numericUpDownPrice.Value;
        set => numericUpDownPrice.Value = value;
    }

    public int SelectedAuthorIndex
    {
        get => comboBoxAuthor.SelectedIndex;
        set => comboBoxAuthor.SelectedIndex = value;
    }

    public int SelectedPressIndex
    {
        get => comboBoxPress.SelectedIndex;
        set => comboBoxPress.SelectedIndex = value;
    }

    public bool CanAccept { set => buttonOK.Enabled = value; }

    public void SetAuthors(IEnumerable<string> authors)
    {
        SetComboBoxItems(comboBoxAuthor, authors);
    }

    public void SetPresses(IEnumerable<string> presses)
    {
        SetComboBoxItems(comboBoxPress, presses);
    }

    bool IDialogView.ShowDialog()
    {
        return ShowDialog() == DialogResult.OK;
    }

    public void ShowError(string message)
    {
        MessageBoxHelper.ShowError(message);
    }

    public event EventHandler Loaded;

    private void LoadedHandler(object sender, EventArgs e)
    {
        if (Loaded is not null)
            Loaded(this, EventArgs.Empty);
    }

    public event EventHandler BookNameChanged;

    private void textBoxName_TextChanged(object sender, EventArgs e)
    {
        if (BookNameChanged is not null)
            BookNameChanged(this, EventArgs.Empty);
    }

    public event EventHandler AuthorSelected;

    private void AuthorSelectedHandler(object sender, EventArgs e)
    {
        if (AuthorSelected is not null)
            AuthorSelected(this, EventArgs.Empty);
    }

    public event EventHandler PressSelected;

    private void PressSelectedHandler(object sender, EventArgs e)
    {
        if (PressSelected is not null)
            PressSelected(this, EventArgs.Empty);
    }

    #endregion

    private static void SetComboBoxItems(ComboBox comboBox, IEnumerable<string> items)
    {
        comboBox.Items.Clear();
        foreach (string item in items)
            comboBox.Items.Add(item);
    }
}
