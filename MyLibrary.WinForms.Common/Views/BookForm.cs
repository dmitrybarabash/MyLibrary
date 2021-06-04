using System;
using System.Windows.Forms;

namespace MyLibrary.WinForms.Common.Views
{
    public partial class BookForm : Form, IBookView
    {
        public BookForm()
        {
            InitializeComponent();
        }

        #region IBookView implementation

        public ComboBox AuthorComboBox { get { return comboBoxAuthor; } }

        public string BookName
        {
            get { return textBoxName.Text.Trim(); }
            set { textBoxName.Text = value; }
        }

        public int Pages
        {
            get { return Convert.ToInt32(numericUpDownPages.Value); }
            set { numericUpDownPages.Value = value; }
        }

        public decimal Price
        {
            get { return numericUpDownPrice.Value; }
            set { numericUpDownPrice.Value = value; }
        }

        public ComboBox PressComboBox { get { return comboBoxPress; } }

        public event EventHandler Loaded;

        private void LoadedHandler(object sender, EventArgs e)
        {
            if (Loaded is not null)
                Loaded(this, EventArgs.Empty);
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

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = BookName.Length > 0;
        }
    }
}
