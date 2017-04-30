using Microsoft.Practices.Unity;
using Runner.Configuration;
using Runner.Tasks;
using Runner.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Runner.Main
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public IUnityContainer _container;
        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(IUnityContainer container):this()
        {
            _container = container;
        }



        private void Configuration_Navigate(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);

            nav.Navigate(new ConfigurationPage(), new object());
        }
        
        private void Worfklows_Navigate(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            var page = _container.Resolve<WorkflowsPage>();
            nav.Navigate(page, new object());
        }

        private void Tasks_Navigate(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            var page = _container.Resolve<TasksPage>();
            nav.Navigate(page, new object());
        }

    }
}
