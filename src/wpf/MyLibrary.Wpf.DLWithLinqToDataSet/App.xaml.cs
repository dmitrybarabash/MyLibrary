using System.Windows;
using MyLibrary.Common.DLWithLinqToDataSet.DomainModel;
using MyLibrary.Wpf.Common.Services;
using MyLibrary.Wpf.Common.ViewModels;
using MyLibrary.Wpf.Common.Views;

namespace MyLibrary.Wpf.DLWithLinqToDataSet;

public partial class App : Application
{
    private MyLibraryDomainModel _myLibraryDomainModel;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Создаем модель, модель представления главного окна и само главное окно
        _myLibraryDomainModel = new MyLibraryDomainModel();
        var mainViewModel = new MainViewModel(_myLibraryDomainModel, new WpfDialogService());
        var mainWindow = new MainWindow { DataContext = mainViewModel };
        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _myLibraryDomainModel?.Dispose();
        base.OnExit(e);
    }
}
