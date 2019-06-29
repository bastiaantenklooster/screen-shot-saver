using System;
using System.IO;
using System.Windows;

namespace S3.Windows.NotificationWindow
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public NotificationWindow(FileInfo savedFile)
        {
            DataContext = new NotificationWindowModel(savedFile);

            InitializeComponent();
        }

        private void FadeOutComplete(object sender, EventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var desktopWorkingArea = SystemParameters.WorkArea;
                Left = desktopWorkingArea.Right - Width;
                Top = desktopWorkingArea.Bottom - Height;
            }
            finally { }
        }
    }
}
