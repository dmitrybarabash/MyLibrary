using System;
using System.Windows.Forms;
using MyLibrary.Common.Mvp;

namespace MyLibrary.Common.Views
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