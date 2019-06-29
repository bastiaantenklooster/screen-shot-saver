using S3.Keyboard;
using S3.Properties;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace S3
{

    public class Screenshot : IDisposable
    {

        private readonly object fileNameLock = new object();

        protected DirectoryInfo StorageDirectory { get; }

        protected GlobalKeyboardHook KeyboardHook { get; }

        public event EventHandler<ScreenshotSavedEventArgs> Saved;

        public Screenshot(DirectoryInfo storageDirectory)
        {
            StorageDirectory = storageDirectory;
            KeyboardHook = new GlobalKeyboardHook();

            KeyboardHook.KeyboardPressed += KeyboardHook_KeyboardPressed;
        }

        private void KeyboardHook_KeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.VkSnapshot)
                return;

            if (e.KeyboardState != KeyboardState.KeyUp)
                return;

            HandlePrintScreen();
        }

        private void HandlePrintScreen()
        {
            var sc = new ScreenCapture.ScreenCapture();
            var file = GetUniqueFile($"{ Resources.Filename.ToString() } ({{0}}).jpg");

            Directory.CreateDirectory(file.DirectoryName);

            sc.CaptureScreenToFile(file.FullName, ImageFormat.Png);

            Saved?.Invoke(this, new ScreenshotSavedEventArgs(file));
        }

        private FileInfo GetUniqueFile(string fileNameTemplate)
        {
            string filename;

            lock (fileNameLock)
            {
                for (var i = 0; ; ++i)
                {
                    filename = Path.Combine(StorageDirectory.FullName, string.Format(fileNameTemplate, i));

                    if (!File.Exists(filename))
                        return new FileInfo(filename);
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                KeyboardHook?.Dispose();

                disposedValue = true;
            }
        }

        ~Screenshot()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }

}
