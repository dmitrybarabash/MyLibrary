using System;
using System.Windows.Forms;
using MyLibrary.WinForms.Common.Presenters;
using MyLibrary.WinForms.Common.Views;
using MyLibrary.WinForms.CL.DomainModel;

namespace MyLibrary.WinForms.CL
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (MyLibraryDomainModel myLibraryDomainModel = new MyLibraryDomainModel())
            {
                var mainPresenter = new MainPresenter(myLibraryDomainModel, new MainForm());
                Application.Run((Form)mainPresenter.View);
            }
        }
    }
}
