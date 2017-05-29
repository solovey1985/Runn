using Runner.Framework;
using Runner.Main;
using Runner.Services;
using Runner.Services.Models;
using Runner.Shared.Credentials;
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

namespace Runner.Tasks
{
    /// <summary>
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        public event EventHandler<CredentilasInputArgs> CredentialsInputed;

        TaskViewModel viewModel;
        public TasksPage(IConfigurationService _service)
        {
            viewModel = new TaskViewModel(_service);
            DataContext = viewModel;
            viewModel.CredentialsIputRequired += OnCredentialsInput;
            CredentialsInputed += viewModel.OnCredentialsInputed;
            InitializeComponent();
            
        }

        private void lsbxTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.CurrentTask = (TaskConfig)lsbxTasks.SelectedItem;
        }
        
        public void OnCredentialsInput(object e, CredentilasInputArgs args) {
            CredentialsWindow credentialsWindow;
            if (args != null)            {
                credentialsWindow = new CredentialsWindow(new CredentialsModel() { Login = args.Login });
            }
            else {
                credentialsWindow = new CredentialsWindow();
            }

            if (credentialsWindow.ShowDialog() == true) {
                var credentials = (CredentialsModel)credentialsWindow.DataContext;
                if (CredentialsInputed != null)
                {
                    CredentialsInputed(this, new CredentilasInputArgs() { Login = credentials.Login, Password = credentials.Password });
                }
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }

    
}
