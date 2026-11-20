using MyLibrary.Wpf.Common.Mvvm;

namespace MyLibrary.Wpf.Common.ViewModels;

// Строка списка справочника (автор или издательство). Название может меняться
// при редактировании, и представление сразу же это отображает.
public class RefListItem(long id, string name) : ViewModelBase
{
    public long Id { get; } = id;

    public string Name { get; set => SetProperty(ref field, value); } = name;
}
