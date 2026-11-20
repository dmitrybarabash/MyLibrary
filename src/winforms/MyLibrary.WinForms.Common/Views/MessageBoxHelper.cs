using System.Windows.Forms;

namespace MyLibrary.WinForms.Common.Views;

// Стандартные окна сообщений, общие для всех форм
internal static class MessageBoxHelper
{
    public static void ShowError(string message) =>
        MessageBox.Show(message, "Что-то пошло не так", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public static bool Confirm(string message, string caption) =>
        MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes;
}
