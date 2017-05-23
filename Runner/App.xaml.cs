using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Runner.App_Start;
using Microsoft.Practices.Unity;
using Runner.Main;
using System.Windows.Shell;
using Runner.Services;
using Runner.Services.Models;
using System.Runtime.Remoting;
using Runner.Framework;

namespace Runner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {

        [STAThread]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance("Runner"))
            {
                var application = new App();
                application.Init();
                application.Run();

                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            }
        }

        public void Init()
        {
            this.InitializeComponent();
        }

        private IUnityContainer container;
        protected override void OnStartup(StartupEventArgs e)
        {
            container = UnityConfig.Configure();
            var mainWindow = container.Resolve<MainWindow>();
            Application.Current.MainWindow = mainWindow;
            CreateJumpList();
            mainWindow.Content = container.Resolve<MainPage>();
            mainWindow.ShowsNavigationUI = true;
            Application.Current.MainWindow.Show();
        }

        private void CreateJumpList()
        {
            JumpList jumpList = new JumpList();
            JumpList.SetJumpList(Application.Current, jumpList);
            IConfigurationService service = container.Resolve<IConfigurationService>();
            if (service != null)
            {
                List<TaskConfig> configs = service.ReadConfigurationFromFile("config.json");
                foreach (TaskConfig taskConfig in configs)
                {
                    JumpTask t = new JumpTask();
                    t.Title = taskConfig.Name;
                    t.ApplicationPath = Assembly.GetEntryAssembly().Location;
                    t.Arguments = taskConfig.Name;
                    jumpList.JumpItems.Add(t);
                }
            }
            jumpList.Apply();
        }

    
        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            var window = MainWindow as MainWindow;
            if (window == null) return false;
            if (!window.IsHandlerAdded)
            {
                window.TaskRunning += Window_TaskRunning;
            }
            return window.ProcessCommandLineArgs(args);
        }

        private void Window_TaskRunning(object sender, string e)
        {
            IConfigurationService service = container.Resolve<IConfigurationService>();
            var task = service.ReadConfigurationFromFile("config.json").FirstOrDefault(x=>x.Name == e);
            if (task == null) return;
            BaseService taskRunner;
            switch (task.Type)
            {
                case TaskType.Executable:
                case TaskType.CommandLine:
                    { taskRunner = new SimpleTaskService(); break; }
                case TaskType.PowerShell: { taskRunner = new PowerShellService(); break; }
                case TaskType.Git:
                    {
                        taskRunner = new GitService();
                        GitTask gitTask = service.GetTaskById<GitTask>(task.Id);
                        taskRunner.Run(gitTask);
                        return;
                    }
                default: { taskRunner = new SimpleTaskService(); break; }

            }
            taskRunner.Run(task);

        }
    }
}
