using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runner.Commands;
using Runner.Services.Models;
using Runner.Services;

namespace Runner.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        IConfigurationService _configService;
        private BaseService taskRunner;
        public MainViewModel(IConfigurationService configService, RunCommand command)
        {
            _configService = configService;
            Configurations = _configService.ReadConfigurationFromFile("config.json");
        }

        private TaskConfig CurrentTask { get; set; }

        private BaseCommand runCommand;
        public BaseCommand RunCommand
        { get  {
                return runCommand??
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

        public List<Services.Models.TaskConfig> Configurations { get; set; }
        
        
    
    }
}
