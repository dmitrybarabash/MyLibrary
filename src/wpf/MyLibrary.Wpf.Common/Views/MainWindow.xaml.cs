using System.Windows;
using System.Windows.Input;

namespace MyLibrary.Wpf.Common.Views;

// Вся логика главного окна находится в модели представления MainViewModel.
// Здесь обрабатываются только пункты меню "Выход" и "О программе", которые относятся
// исключительно к представлению и никак не затрагивают модель.
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e) =>
        Close();

    private void MenuItemAbout_Click(object sender, RoutedEventArgs e) =>
        new AboutWindow { Owner = this }.ShowDialog();
}
