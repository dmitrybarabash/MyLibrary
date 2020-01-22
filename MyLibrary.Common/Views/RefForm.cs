using System;
using System.Windows.Forms;

namespace MyLibrary.Common.Views
{
    public partial class RefForm : Form, IRefView
    {
        public RefForm()
        {
            InitializeComponent();
            Modified = false;
        }

        #region IRefView implementation

        public string Title { set { Text = value; } }

        public ListView ListView { get { return listView; } }

        public string ListViewTheOnlyColumnName { set { listView.Columns[0].Text = value; } }

        public bool Modified { get; set; }

        public event EventHandler Loaded;

        private void LoadedHandler(object sender, EventArgs e)
        {
            if (Loaded != null)
                Loaded(this, EventArgs.Empty);
        }

        public event EventHandler Add;

        private void AddHandler(object sender, EventArgs e)
        {
            if (Add != null)
                Add(this, EventArgs.Empty);
        }

        public event EventHandler Edit;

        private void EditHandler(object sender, EventArgs e)
        {
            if (Edit != null)
                Edit(this, EventArgs.Empty);
        }

        public event EventHandler Delete;

        private void DeleteHandler(object sender, EventArgs e)
        {
            if (Delete != null)
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
}
