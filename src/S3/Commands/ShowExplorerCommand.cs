using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace S3.Commands
{
    public class ShowExplorerCommand : ICommand
    {

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public DirectoryInfo Directory { get; }

        public ShowExplorerCommand(DirectoryInfo directory)
        {
            Directory = directory;
        }

        public bool CanExecute(object parameter)
        {
            return Directory.Exists;
        }

        public void Execute(object parameter)
        {
            Process.Start(Directory.FullName);
        }

    }
}
