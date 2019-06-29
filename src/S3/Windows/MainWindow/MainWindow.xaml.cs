using System.Windows;

namespace S3.Windows.MainWindow
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            Application.Current.Exit += Application_Exit;

            var context = new MainWindowModel();

            context.Screenshot.Saved += Screenshot_Saved;

            DataContext = context;

            InitializeComponent();
        }

        private void Screenshot_Saved(object sender, ScreenshotSavedEventArgs e)
        {
            var notification = new NotificationWindow.NotificationWindow(e.File)
            {
                Owner = this
            };

            notification.Show();
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
