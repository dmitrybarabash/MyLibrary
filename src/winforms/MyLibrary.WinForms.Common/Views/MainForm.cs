using System;
using System.Windows.Forms;

namespace MyLibrary.WinForms.Common.Views
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ScaleListViewColumns()
        {
            // Оставляем место для вертикальной полосы прокрутки
            int width = listView.Width - 30;

            // Масштабируем ширину столбцов, исходя из соотношения (20%, 45%, 10%, 10%, 15%)
            listView.Columns[0].Width = width * 20 / 100;
            listView.Columns[1].Width = width * 45 / 100;
            listView.Columns[2].Width = width * 10 / 100;
            listView.Columns[3].Width = width * 10 / 100;
            listView.Columns[4].Width = width * 15 / 100;
        }

        #region IMainView implementation

        public ListView ListView { get { return listView; } }

        public event EventHandler Loaded;

        private void LoadedHandler(object sender, EventArgs e)
        {
            if (Loaded is not null)
                Loaded(this, EventArgs.Empty);
            ScaleListViewColumns();
        }

        public event EventHandler AddBook;

        private void AddBookHandler(object sender, EventArgs e)
        {
            if (AddBook is not null)
                AddBook(this, EventArgs.Empty);
        }

        public event EventHandler EditBook;

        private void EditBookHandler(object sender, EventArgs e)
        {
            if (EditBook is not null)
                EditBook(this, EventArgs.Empty);
        }

        public event EventHandler DeleteBook;

        private void DeleteBookHandler(object sender, EventArgs e)
        {
            if (DeleteBook is not null)
                DeleteBook(this, EventArgs.Empty);
        }

        public event EventHandler EditAuthors;

        private void EditAuthorsHandler(object sender, EventArgs e)
        {
            if (EditAuthors is not null)
                EditAuthors(this, EventArgs.Empty);
        }

        public event EventHandler EditPresses;

        private void EditPressesHandler(object sender, EventArgs e)
        {
            if (EditPresses is not null)
                EditPresses(this, EventArgs.Empty);
        }

        #endregion


        private void toolStripFileExitItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripHelpAboutItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void listView_Resize(object sender, EventArgs e)
        {
            ScaleListViewColumns();
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditBookHandler(sender, e);
        }
    }
}
