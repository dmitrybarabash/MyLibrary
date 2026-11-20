using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyLibrary.Wpf.Common.Mvvm;

// Базовый класс моделей представления. Реализует интерфейс INotifyPropertyChanged, благодаря
// которому привязки (bindings) WPF узнают об изменении свойств и обновляют представление.
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    // Присваивает полю новое значение и, если оно действительно изменилось, оповещает
    // об этом подписчиков. Возвращает true, если значение изменилось.
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
