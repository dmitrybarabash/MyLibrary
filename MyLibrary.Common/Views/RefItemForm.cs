using System;
using System.Windows.Forms;

namespace MyLibrary.Common.Views
{
    public partial class RefItemForm : Form
    {
        public RefItemForm()
        {
            InitializeComponent();
        }

        public string GroupBoxText { set { groupBoxItem.Text = value; } }

        public string LabelText { set { labelItem.Text = value; } }

        public string ItemText
        {
            get { return textBoxItem.Text.Trim(); }
            set { textBoxItem.Text = value; }
        }

        private void textBoxItem_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = ItemText.Length > 0;
        }
    }
}
