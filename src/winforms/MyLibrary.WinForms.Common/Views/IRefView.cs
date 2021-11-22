using System;
using System.Windows.Forms;
using MyLibrary.WinForms.Common.Mvp;

namespace MyLibrary.WinForms.Common.Views;

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
