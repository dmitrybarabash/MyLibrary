using System;
using System.Windows.Forms;
using MyLibrary.Common.EF.DomainModel;
using MyLibrary.WinForms.Common.Presenters;
using MyLibrary.WinForms.Common.Views;

namespace MyLibrary.WinForms.EF;

static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        using var myLibraryDomainModel = new MyLibraryDomainModel();
        var mainPresenter = new MainPresenter(myLibraryDomainModel, new MainForm());
        Application.Run((Form)mainPresenter.View);
    }
}
