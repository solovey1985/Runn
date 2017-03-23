using Runner.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runner.Services;
using WPFNotification.Services;
using WPFNotification.Model;

namespace Runner.Commands
{
    public class RunCommand:BaseCommand
    {
        private NotificationDialogService _dialogService;
        private BaseService taskRunner;
        private ConfigurationService configService;
        public Task TaskConfiguration { get; set; }
        public RunCommand()
        {
            _dialogService = new NotificationDialogService();
            configService = new ConfigurationService("config.json");
            

        }
        public override void Execute(object parameter)
        {
            Services.Models.TaskConfig current = (Services.Models.TaskConfig)parameter;
            if (current != null)
                switch (current.PathToUtil)
                {
                    case "cmd.exe": {  taskRunner = new SimpleTaskService(); break;}
                    case "powershell.exe": {  taskRunner = new PowerShellService(); break;}
                    case "git.exe": {  taskRunner = new GitService();
                            GitTask gitTask =  configService.GetTaskById<GitTask>(current.Id);
                            taskRunner.Run(gitTask);
                            return;
                            }
                    default: { taskRunner = new SimpleTaskService(); break; }

                }
                taskRunner.Run(current);
                
        }
    }
}
