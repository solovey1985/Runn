using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows;
using Microsoft.Practices.Unity;
using Runner.Services;
using Runner.Tasks;
using Runner.Main;

namespace Runner.App_Start
{
    public class UnityConfig
    {
        public static IUnityContainer Configure()
        { 
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IGitService, GitService>();
            container.RegisterType<ISimpleTaskService, SimpleTaskService>();
            container.RegisterType<IConfigurationService, ConfigurationService>();
            container.RegisterType<IPowerShellService, PowerShellService>();
            container.RegisterType<IWorkflowService, WorkflowService>();

            return container;
        }
    }
}
