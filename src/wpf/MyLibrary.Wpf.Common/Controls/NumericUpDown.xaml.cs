using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyLibrary.Wpf.Common.Controls;

// Поле ввода числа с кнопками увеличения/уменьшения значения. В WPF, в отличие от
// Windows Forms, нет стандартного элемента NumericUpDown, поэтому делаем свой.
//
// Значение всегда находится в диапазоне [Minimum, Maximum]. Допустимое число, введенное
// с клавиатуры, сразу попадает в Value, а при потере фокуса или нажатии Enter текст
// приводится к допустимому значению: обрезается по диапазону либо, если это вообще
// не число, возвращается к последнему допустимому значению.
public partial class NumericUpDown : UserControl
{
    public NumericUpDown()
    {
        InitializeComponent();
        UpdateText();
    }

    #region Dependency properties

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(decimal),
            typeof(NumericUpDown),
            new FrameworkPropertyMetadata(0m, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged, CoerceValue));

    public decimal Value
    {
        get => (decimal)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(
            nameof(Minimum),
            typeof(decimal),
            typeof(NumericUpDown),
            new PropertyMetadata(0m, OnRangeChanged));

    public decimal Minimum
    {
        get => (decimal)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(
            nameof(Maximum),
            typeof(decimal),
            typeof(NumericUpDown),
            new PropertyMetadata(100m, OnRangeChanged));

    public decimal Maximum
    {
        get => (decimal)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public static readonly DependencyProperty IncrementProperty =
        DependencyProperty.Register(
            nameof(Increment),
            typeof(decimal),
            typeof(NumericUpDown),
            new PropertyMetadata(1m));

    public decimal Increment
    {
        get => (decimal)GetValue(IncrementProperty);
        set => SetValue(IncrementProperty, value);
    }

    public static readonly DependencyProperty DecimalPlacesProperty =
        DependencyProperty.Register(
            nameof(DecimalPlaces),
            typeof(int),
            typeof(NumericUpDown),
            new PropertyMetadata(0, OnDecimalPlacesChanged));

    public int DecimalPlaces
    {
        get => (int)GetValue(DecimalPlacesProperty);
        set => SetValue(DecimalPlacesProperty, value);
    }

    private static object CoerceValue(DependencyObject d, object baseValue)
    {
        var control = (NumericUpDown)d;
        return Math.Clamp((decimal)baseValue, control.Minimum, Math.Max(control.Minimum, control.Maximum));
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (NumericUpDown)d;
        // Если значение изменилось из-за того, что пользователь вводит текст, то текст
        // не трогаем, иначе курсор будет прыгать
        if (!control._isTyping)
            control.UpdateText();
    }

    private static void OnRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        d.CoerceValue(ValueProperty);

    private static void OnDecimalPlacesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        ((NumericUpDown)d).UpdateText();

    #endregion

    // Признаки того, что текст меняется программно или что значение меняется из-за ввода текста
    private bool _isUpdatingText;
    private bool _isTyping;

    private void UpdateText()
    {
        _isUpdatingText = true;
        try
        {
            textBox.Text = Value.ToString("F" + DecimalPlaces, CultureInfo.CurrentCulture);
        }
        finally
        {
            _isUpdatingText = false;
        }
    }

    private static bool TryParse(string text, out decimal value) =>
        decimal.TryParse(
            text,
            NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
            CultureInfo.CurrentCulture,
            out value);

    // Округляет до нужного числа знаков после запятой (по-школьному, а не по-банковски)
    private decimal Round(decimal value) =>
        Math.Round(value, DecimalPlaces, MidpointRounding.AwayFromZero);

    // Приводит введенный текст к допустимому значению
    private void CommitText()
    {
        if (TryParse(textBox.Text, out decimal value))
            Value = Round(value);
        UpdateText();
    }

    private void ChangeValue(decimal delta)
    {
        CommitText();
        Value += delta;
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isUpdatingText)
            return;

        // Допустимое число сразу же запоминаем, чтобы оно не потерялось, если пользователь,
        // например, нажмет Enter и тем самым кнопку OK диалога
        if (TryParse(textBox.Text, out decimal value) && value >= Minimum && value <= Maximum)
        {
            _isTyping = true;
            try
            {
                Value = Round(value);
            }
            finally
            {
                _isTyping = false;
            }
        }
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Up:
                ChangeValue(Increment);
                e.Handled = true;
                break;
            case Key.Down:
                ChangeValue(-Increment);
                e.Handled = true;
                break;
            case Key.Enter:
                // Событие не помечаем как обработанное, чтобы сработала кнопка по умолчанию
                CommitText();
                break;
        }
    }

    private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) =>
        CommitText();

    private void ButtonUp_Click(object sender, RoutedEventArgs e) =>
        ChangeValue(Increment);

    private void ButtonDown_Click(object sender, RoutedEventArgs e) =>
        ChangeValue(-Increment);
}
