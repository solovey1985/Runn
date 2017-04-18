using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Runner.App_Start;
using Microsoft.Practices.Unity;
using Runner.Main;

namespace Runner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            var container = UnityConfig.Configure();
            var mainWindow = container.Resolve<MainWindow>();
            Application.Current.MainWindow = mainWindow;

            mainWindow.Content = container.Resolve<MainPage>();
            mainWindow.ShowsNavigationUI = true;
            Application.Current.MainWindow.Show();
        }
    
        
    }
}
