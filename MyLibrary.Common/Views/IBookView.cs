using System;
using System.Windows.Forms;
using MyLibrary.Common.Mvp;

namespace MyLibrary.Common.Views
{
    public interface IBookView : IView
    {
        ComboBox AuthorComboBox { get; }
        string BookName { get; set; }
        int Pages { get; set; }
        decimal Price { get; set; }
        ComboBox PressComboBox { get; }

        event EventHandler Loaded;
        event EventHandler AuthorSelected;
        event EventHandler PressSelected;
    }
}