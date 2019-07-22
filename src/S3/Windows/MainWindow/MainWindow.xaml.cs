using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace S3.Windows.MainWindow
{

    public partial class MainWindow : Window
    {

        private List<NotificationWindow.NotificationWindow> Notifications { get; } = new List<NotificationWindow.NotificationWindow>();

        public MainWindow()
        {
            Application.Current.Exit += Application_Exit;

            var context = new MainWindowModel(this);

            context.Screenshot.Saved += Screenshot_Saved;

            DataContext = context;

            InitializeComponent();
        }

        public void HideNotifications()
        {
            Notifications.ToList().ForEach(notification =>
            {
                notification.Hide();
                notification.Close();
            });
        }

        private void Screenshot_Saved(object sender, ScreenshotSavedEventArgs e)
        {
            var notification = new NotificationWindow.NotificationWindow(e.File)
            {
                Owner = this
            };

            Notifications.Add(notification);

            notification.Show();

            notification.Closed += Notification_Closed;
        }

        private void Notification_Closed(object sender, System.EventArgs e)
        {
            var notification = sender as NotificationWindow.NotificationWindow;

            notification.Closed -= Notification_Closed;

            Notifications.Remove(notification);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            (DataContext as MainWindowModel)?.Dispose();
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }

}
