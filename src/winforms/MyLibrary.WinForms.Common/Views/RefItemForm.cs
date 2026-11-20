using System;
using System.Windows.Forms;
using MyLibrary.WinForms.Common.Mvp;

namespace MyLibrary.WinForms.Common.Views;

public partial class RefItemForm : Form, IRefItemView
{
    public RefItemForm()
    {
        InitializeComponent();
    }

    #region IRefItemView implementation

    public string Title { set => Text = value; }

    public string Heading { set => groupBoxItem.Text = value; }

    public string ItemLabel { set => labelItem.Text = value; }

    public string ItemText
    {
        get => textBoxItem.Text.Trim();
        set => textBoxItem.Text = value;
    }

    public bool CanAccept { set => buttonOk.Enabled = value; }

    bool IDialogView.ShowDialog()
    {
        return ShowDialog() == DialogResult.OK;
    }

    public event EventHandler ItemTextChanged;

    private void textBoxItem_TextChanged(object sender, EventArgs e)
    {
        if (ItemTextChanged is not null)
            ItemTextChanged(this, EventArgs.Empty);
    }

    #endregion
}
