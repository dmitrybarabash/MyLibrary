using System.Windows;

namespace MyLibrary.Wpf.Common.Views;

// Вся логика окна находится в модели представления RefItemViewModel.
// Здесь только ставим фокус на поле ввода и выделяем его текст, чтобы его было удобно заменить.
public partial class RefItemWindow : Window
{
    public RefItemWindow()
    {
        InitializeComponent();
        Loaded += (_, _) =>
        {
            textBoxItem.Focus();
            textBoxItem.SelectAll();
        };
    }
}
