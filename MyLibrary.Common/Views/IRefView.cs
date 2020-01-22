using System;
using System.Windows.Forms;
using MyLibrary.Common.Mvp;

namespace MyLibrary.Common.Views
{
    public interface IRefView : IView
    {
        string Title { set; }
        ListView ListView { get; }
        string ListViewTheOnlyColumnName { set; }

        event EventHandler Loaded;
        event EventHandler Add;
        event EventHandler Edit;
        event EventHandler Delete;
    }

}