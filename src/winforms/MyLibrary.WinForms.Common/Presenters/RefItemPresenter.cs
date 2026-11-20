using System;
using MyLibrary.WinForms.Common.Mvp;
using MyLibrary.WinForms.Common.Views;

namespace MyLibrary.WinForms.Common.Presenters;

public class RefItemPresenter : BasePresenter<IRefItemView>
{
    public RefItemPresenter(IRefItemView refItemView)
    {
        // Сохраняем внедряемую ссылку на представление
        View = refItemView;
        // Подписываемся на события представления
        View.ItemTextChanged += OnItemTextChanged;
    }

    private void OnItemTextChanged(object sender, EventArgs e)
    {
        // Подтвердить ввод можно, только если значение не пустое
        View.CanAccept = View.ItemText.Length > 0;
    }
}
