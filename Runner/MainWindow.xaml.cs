using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Runner.ViewModels;
using Runner.Services.Models;

namespace Runner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public event  EventHandler<string> TaskRunning;
        public bool IsHandlerAdded { get { return TaskRunning != null; } }
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            MainViewModel _viewModel = viewModel; 
            DataContext = _viewModel;
        }
        public bool ProcessCommandLineArgs(IList<string> args)
        {
            if (args == null || args.Count == 0)
                return true;
            if ((args.Count > 1))
            {
                string taskName = args.Skip(1).Aggregate((i, j)=> i + " " + j).Trim();
                OnTaskRunning(taskName);
                
            }

            return true;
        }

        private void OnTaskRunning(string taskName)
        {
            TaskRunning(this, taskName);
        }

    }
}
