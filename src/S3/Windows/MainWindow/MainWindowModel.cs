using S3.Commands;
using S3.Properties;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace S3.Windows.MainWindow
{
    public class MainWindowModel : INotifyPropertyChanged, IDisposable
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private DirectoryInfo Directory { get; }

        public Screenshot Screenshot { get; }

        public ICommand ShowExplorerCommand { get; }

        private MainWindow Window { get; }

        public MainWindowModel(MainWindow window)
        {
            Directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), Resources.Directory));

            Directory.Create();

            ShowExplorerCommand = new ShowExplorerCommand(Directory);

            Screenshot = new Screenshot(Directory);

            Screenshot.BeforePrintScreen += Screenshot_BeforePrintScreen;

            Window = window;
        }

        private void Screenshot_BeforePrintScreen(object sender, EventArgs e)
        {
            Window.HideNotifications();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Screenshot?.Dispose();

                disposedValue = true;
            }
        }

        ~MainWindowModel()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
