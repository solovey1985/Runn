using Runner.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Services.Framework
{
    public class ProcessHelper
    {
        public bool Run(TaskConfig taskConfig)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = taskConfig.PathToUtil;
            if (!string.IsNullOrEmpty(taskConfig.PathToFile))
            {
                info.Arguments = $"/c START {taskConfig.PathToFile}";
                info.WorkingDirectory = Path.GetDirectoryName(taskConfig.PathToFile);
            }
            Debug.Write(info.WorkingDirectory);
            //Appereance
            info.WindowStyle = ProcessWindowStyle.Normal;
            info.UseShellExecute = true;
            info.CreateNoWindow = false;
            info.RedirectStandardError = false;
            info.RedirectStandardOutput = false;

            Process process = new Process();
            process.EnableRaisingEvents = true;
            process.StartInfo = info;
            process.Start();

            //TODO: IN workflows can be useful
            //process.WaitForExit(); 

            process.Close();
            return false;
        }
    }
}
