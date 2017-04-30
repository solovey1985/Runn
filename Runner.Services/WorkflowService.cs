using Runner.Services.Models;
using Runner.Services.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Services
{
    public class WorkflowService : IWorkflowService
    {
        private ConfigurationService _configService;
        public WorkflowService() { }

        public void Run(Workflow workflow)
        {
            if (workflow.Validate())
            {
                var workflowsteps = workflow.Steps.OrderBy(x => x.Order).ToList();
                foreach (WorkflowStep step in workflowsteps)
                {
                    Handle(step);
                }
            }
        }

        private void Handle(WorkflowStep step)
        {
            if (step != null && step.Validate())
            {
                IBaseService service = GetServiceByName(step);
                service.Run(step.Task);
            }
        }

        private IBaseService GetServiceByName(WorkflowStep step)
        {
            IBaseService taskRunner;
            switch (step.Task.PathToUtil)
            {
                case "cmd.exe": { taskRunner = new SimpleTaskService(); break; }
                case "powershell.exe": { taskRunner = new PowerShellService(); break; }
                case "git.exe":
                    {
                        taskRunner = new GitService();
                        ((GitService)taskRunner).CurrentTask = _configService.GetTaskById<GitTask>(step.Task.Id);
                        
                        break;
                    }
                default: { taskRunner = new SimpleTaskService(); break; }
              }
            return taskRunner;
        }
    }

   
}
