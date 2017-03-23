using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runner.Services.Models;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;
using System.Diagnostics;

namespace Runner.Services
{
    public class PowerShellService : BaseService
    {
        public override bool Run(Models.TaskConfig taskConfig)
        {
            RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
            Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
            runspace.Open();
            
            RunspaceInvoke scriptInvoker = new RunspaceInvoke(runspace);
            Pipeline pipeline = runspace.CreatePipeline();

            Command restrictionCommand = new Command("Set-ExecutionPolicy -Scope LocalMachine Unrestricted");
            Command myCommand = new Command(taskConfig.PathToFile, true);

            string force = taskConfig.Parameters.FirstOrDefault(p => p.Contains("force"));
            if (force != null)
            {
                CommandParameter param = new CommandParameter("f", "");
                myCommand.Parameters.Add(param);

            }
           // pipeline.Commands.Add(restrictionCommand);
            pipeline.Commands.Add(myCommand);
            
            var results = pipeline.Invoke();
            foreach (var item in results)
            {
                Debug.WriteLine(item.BaseObject.ToString());
            }
            
            return results.Count > 0;
        }

        public override void PostRun(Models.TaskConfig taskConfig)
        {
            throw new NotImplementedException();
        }

        public override void PreRun(Models.TaskConfig taskConfig)
        {
            throw new NotImplementedException();
        }
    }
}
