using System;
using System.Windows.Forms;
using MyLibrary.Common.Presenters;
using MyLibrary.Common.Views;
using MyLibrary.DL.DomainModel;

namespace MyLibrary.DL
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
