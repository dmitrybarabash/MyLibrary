using System;
using System.Windows.Forms;
using MyLibrary.WinForms.Common.Mvp;

namespace MyLibrary.WinForms.Common.Views
{
    public interface IMainView : IView
    {
        ListView ListView { get; }

        event EventHandler Loaded;
        event EventHandler AddBook;
        event EventHandler EditBook;
        event EventHandler DeleteBook;
        event EventHandler EditAuthors;
        event EventHandler EditPresses;
    }
}