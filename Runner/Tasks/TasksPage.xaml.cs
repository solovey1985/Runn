using Runner.Services;
using Runner.Services.Models;
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
        TaskViewModel viewModel;
        public TasksPage(IConfigurationService _service)
        {
            viewModel = new TaskViewModel(_service);
            DataContext = viewModel;
            InitializeComponent();
            
        }

        private void lsbxTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.CurrentTask = (TaskConfig)lsbxTasks.SelectedItem;
        }
    }
}
