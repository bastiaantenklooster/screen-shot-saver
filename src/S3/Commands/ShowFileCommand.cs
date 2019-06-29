using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace S3.Commands
{
    public class ShowFileCommand : ICommand
    {

        public FileInfo File { get; }

        public ShowFileCommand(FileInfo file)
        {
            File = file;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return File.Exists;
        }

        public void Execute(object parameter)
        {
            Process.Start(File.FullName);
        }

    }
}
