using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyLibrary.Wpf.Common.Behaviors;

// Присоединенное поведение, которое при изменении размеров ListView с представлением GridView
// распределяет ширину списка между столбцами в заданных пропорциях. Нужно потому, что
// столбцы GridView, в отличие от столбцов Grid, не поддерживают ширину вида "*".
//
// Пример: <ListView behaviors:GridViewColumnsBehavior.WidthProportions="20 45 10 10 15" ...>
public static class GridViewColumnsBehavior
{
    // Запас ширины на рамки и внутренние отступы строк списка (ListViewItem)
    private const double ItemPadding = 12;

    public static readonly DependencyProperty WidthProportionsProperty =
        DependencyProperty.RegisterAttached(
            "WidthProportions",
            typeof(DoubleCollection),
            typeof(GridViewColumnsBehavior),
            new PropertyMetadata(null, OnWidthProportionsChanged));

    public static DoubleCollection GetWidthProportions(DependencyObject element) =>
        (DoubleCollection)element.GetValue(WidthProportionsProperty);

    public static void SetWidthProportions(DependencyObject element, DoubleCollection value) =>
        element.SetValue(WidthProportionsProperty, value);

    private static void OnWidthProportionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ListView listView)
            return;

        listView.SizeChanged -= OnSizeChanged;
        if (e.NewValue is not null)
            listView.SizeChanged += OnSizeChanged;
    }

    private static void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var listView = (ListView)sender;
        DoubleCollection proportions = GetWidthProportions(listView);
        if (listView.View is not GridView gridView || proportions is null)
            return;

        // Оставляем место для рамки списка, вертикальной полосы прокрутки и внутренних
        // отступов строк, чтобы не появлялась горизонтальная полоса прокрутки
        double width = listView.ActualWidth -
            listView.BorderThickness.Left -
            listView.BorderThickness.Right -
            listView.Padding.Left -
            listView.Padding.Right -
            SystemParameters.VerticalScrollBarWidth -
            ItemPadding;
        double total = proportions.Sum();
        if (width <= 0 || total <= 0)
            return;

        for (int i = 0; i < gridView.Columns.Count && i < proportions.Count; i++)
            gridView.Columns[i].Width = width * proportions[i] / total;
    }
}
