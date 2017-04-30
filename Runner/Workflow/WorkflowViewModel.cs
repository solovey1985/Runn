using Runner.Services;
using Runner.Services.Models;
using Runner.ViewModels;
using Runner.Services.Workflows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow = Runner.Services.Workflows.Workflow;
namespace Runner.Workflows
{
    public class WorkflowViewModel: BaseViewModel
    {
        IConfigurationService configService;
        public WorkflowViewModel(IConfigurationService _configurationService) {
            configService = _configurationService;
            Tasks = new ObservableCollection<TaskConfig>(configService.ReadConfigurationFromFile("config.json"));
            Workflow = new Workflow();
            Workflow.Name = "Daily routine";
            Workflow.Steps = new List<WorkflowStep>();
            Workflow.Id = 1;
        }

        public string Name { get; set; }
        public ObservableCollection<TaskConfig> Tasks { get; set; }
        public TaskConfig SelectedTask { get; set; }
        public ObservableCollection<TaskConfig> WorkflowTasks { get; set; }
        public Workflow Workflow { get; set; }
        public void OnTaskAddedHandler (object sender, TaskConfig addedTask)
        {
            if (addedTask != null)
            {
                int currentOrder = 0;
                if (Workflow.Steps.Any())
                { currentOrder = Workflow.Steps.OrderBy(x => x.Order).Last().Order + 1; }
                Workflow.Steps.Add(new WorkflowStep() { Task = addedTask, Order = currentOrder });
                configService.SaveWorkflow(Workflow.Name, Workflow);
            }
        }
    }
}
