using MyLibrary.Wpf.Common.Mvvm;

namespace MyLibrary.Wpf.Common.ViewModels;

// Модель представления диалога добавления/изменения элемента справочника (автора или издательства)
public class RefItemViewModel : DialogViewModel
{
    public RefItemViewModel()
    {
        OkCommand = new RelayCommand(OnOk, CanAccept);
    }

    public string Heading { get; init; }
    public string ItemLabel { get; init; }

    public string ItemText { get; set => SetProperty(ref field, value); } = string.Empty;

    public RelayCommand OkCommand { get; }

    // Подтвердить ввод можно, только если значение не пустое
    private bool CanAccept() =>
        !string.IsNullOrWhiteSpace(ItemText);

    private void OnOk()
    {
        ItemText = ItemText.Trim();
        Close(true);
    }
}
