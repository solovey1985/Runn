﻿using Runner.Services.Models;
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
            info.FileName = taskConfig.PathToFile;
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
            process.StartInfo = info;
            process.Start();
            
            //TODO: IN workflows can be useful
            
            process.Close();
            PostRun(taskConfig);
            return false;
        }

        public abstract void PreRun(Models.TaskConfig taskConfig);
        public abstract void PostRun(Models.TaskConfig taskConfig);
    }
}
