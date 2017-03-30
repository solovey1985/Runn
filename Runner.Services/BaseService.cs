using Runner.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Services
{
    public abstract class BaseService : IBaseService
    {
        public virtual bool Run(Models.TaskConfig taskConfig)
        {
            PreRun(taskConfig);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = taskConfig.PathToUtil;
            info.Arguments = "/c START " + taskConfig.PathToFile;
            info.WorkingDirectory = Path.GetDirectoryName(taskConfig.PathToFile);
            Debug.Write(info.WorkingDirectory);
            //Appereance
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            
            Process process = new Process();
            process.StartInfo = info;
            process.Start();
            process.WaitForExit();
            process.Close();
            PostRun(taskConfig);
            return false;
        }

        public abstract void PreRun(Models.TaskConfig taskConfig);
        public abstract void PostRun(Models.TaskConfig taskConfig);
    }
}
