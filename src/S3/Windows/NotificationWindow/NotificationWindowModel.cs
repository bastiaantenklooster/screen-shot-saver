using S3.Commands;
using System.IO;
using System.Windows.Input;

namespace S3.Windows.NotificationWindow
{
    public class NotificationWindowModel
    {

        public FileInfo SavedFile { get; }

        public ICommand ShowFileCommand { get; }

        public NotificationWindowModel(FileInfo savedFile)
        {
            SavedFile = savedFile;
            ShowFileCommand = new ShowFileCommand(SavedFile);
        }

    }
}
