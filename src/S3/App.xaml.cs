using System.Reflection;
using System.Windows;

namespace S3
{

    public partial class App : Application
    {

        public App()
        {
            RegisterRunOnStartup();
        }

        void RegisterRunOnStartup()
        {
            try
            {
                var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                var curAssembly = Assembly.GetExecutingAssembly();
                key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
            }
            finally { }
        }

    }

}
