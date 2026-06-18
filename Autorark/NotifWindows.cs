using System.Windows;

namespace Autorark
{
    public static class ErrorHandler
    {
        public static void ShowError(string message, string title = "Ошибка")
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
    }

    public static class NotificationHandler
    {
        public static void ShowNotification(string message, string title = "Уведомление")
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }
    }
}
