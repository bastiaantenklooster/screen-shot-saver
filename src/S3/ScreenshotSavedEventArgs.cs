using System;
using System.IO;

namespace S3
{
    public class ScreenshotSavedEventArgs : EventArgs
    {

        public FileInfo File { get; }

        public ScreenshotSavedEventArgs(FileInfo file) : base()
        {
            File = file;
        }

    }
}
