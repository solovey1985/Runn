using Runner.Commands;
using Runner.Services;
using Runner.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.ViewModels
{
    public partial class MainViewModel
    {
        
        private BaseCommand runCommand;
        public BaseCommand RunCommand
        {
            get
            {
                return runCommand ??
                    (runCommand = new BaseCommand(obj => {
                        Services.Models.TaskConfig current = (Services.Models.TaskConfig)obj;
                        if (current != null)
                            switch (current.Type)
                            {
                                case TaskType.Executable:
                                case TaskType.CommandLine:
                                    { taskRunner = new SimpleTaskService(); break; }
                                case TaskType.PowerShell: { taskRunner = new PowerShellService(); break; }
                                case TaskType.Git:
                                    {
                                        taskRunner = new GitService();
                                        GitTask gitTask = _configService.GetTaskById<GitTask>(current.Id);
                                        taskRunner.Run(gitTask);
                                        return;
                                    }
                                default: { taskRunner = new SimpleTaskService(); break; }

                            }
                        taskRunner.Run(current);
                    }));
            }
        }

        private BaseCommand editCommand;
        public BaseCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new BaseCommand(obj => {
                        #region Command functionality
                        

                        #endregion
                    }));
            }
        }
    }
}
