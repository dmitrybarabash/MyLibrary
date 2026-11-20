using System;
using MyLibrary.WinForms.Common.Mvp;

namespace MyLibrary.WinForms.Common.Views;

public interface IRefItemView : IDialogView
{
    string Title { set; }
    string Heading { set; }
    string ItemLabel { set; }
    string ItemText { get; set; }
    bool CanAccept { set; }

    event EventHandler ItemTextChanged;
}
