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
        private INotificationDialogService _dialogService = new NotificationDialogService();
        private BaseService taskRunner;
        public string  CommandLine { get; set; }
        public TaskConfiguration TaskConfiguration { get; set; }
        public RunCommand()
        {
            
        }
        public override void Execute(object parameter)
        {
            TaskConfiguration current = (TaskConfiguration)parameter;
            if (current != null)
                switch (current.PathToUtil)
                {
                    case "cmd.exe": {  taskRunner = new SimpleTaskService(); break;}
                    case "powershell.exe": {  taskRunner = new PowerShellService(); break;}
                    case "git.exe": {  taskRunner = new GitService(); break;}
                    default: { taskRunner = new SimpleTaskService(); break; }

                }
                taskRunner.Run(current);
                _dialogService.ShowNotificationWindow(new Notification {Title="Task done", Message=current.Description});
        }
    }
}
