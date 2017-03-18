using Runner.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Services
{
    public abstract class BaseService
    {
        public virtual bool Run(TaskConfiguration taskConfig)
        {
            PreRun(taskConfig);
            ProcessStartInfo process = new ProcessStartInfo();
            Process.Start(taskConfig.PathToFile);
            PostRun(taskConfig);
            return false;
        }

        public abstract void PreRun(TaskConfiguration taskConfig);
        public abstract void PostRun(TaskConfiguration taskConfig);
    }
}
