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

namespace Runner.App_Start
{
    public class UnityConfig
    {
        public static IUnityContainer Configure()
        { 
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IGitService, GitService>();
            container.RegisterType<ISimpleTaskService, SimpleTaskService>();
            foreach (var t in typeof(BaseService).Assembly.GetExportedTypes())
            {
                if (typeof(IBaseService).IsAssignableFrom(t))
                {
                    container.RegisterType(typeof(IBaseService), t, t.FullName);
                }
            }
            return container;
        }
    }
}
