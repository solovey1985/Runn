using Runner.Services;
using Runner.Services.Models;
using Runner.ViewModels;
using Runner.Services.Workflows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WF = Runner.Services.Workflows.Workflow;
using System.Text;
using System.Threading.Tasks;
using Runner.Commands;

namespace Runner.Workflows
{
    public partial class WorkflowViewModel: BaseViewModel
    {
        IConfigurationService configService;
        public WorkflowViewModel(IConfigurationService _configurationService) {
            configService = _configurationService;
            Tasks = new ObservableCollection<TaskConfig>(configService.ReadConfigurationFromFile("config.json"));
            Workflow = configService.GetWorkflow(configService.GetAllWorkflows().FirstOrDefault());
            WorkflowsList = configService.GetAllWorkflows();
            OnPropertyChanged("Workflow");
            OnPropertyChanged("Workflow.Steps");
        }
        
        #region Properties
        public string Name { get; set; }
        public ObservableCollection<TaskConfig> Tasks { get; set; }
        private ObservableCollection<WorkflowStep> steps;
        public ObservableCollection<WorkflowStep> Steps { get { return steps ?? (steps = new ObservableCollection<WorkflowStep>(Workflow.Steps)); } }
        public TaskConfig SelectedTask { get; set; }
        public ObservableCollection<TaskConfig> WorkflowTasks { get; set; }
        public WF Workflow { get; set; }
        public List<string> WorkflowsList { get; set; }
        #endregion
              
        public void OnWorkflowChanged(object sender, EventArgs args)
        {
            configService.GetWorkflow(args.ToString());
        }
        public void OnTaskAddedHandler (object sender, TaskConfig addedTask)
        {
            if (addedTask != null)
            {
                int currentOrder = 0;
                if (steps.Any())
                { currentOrder = steps.OrderBy(x => x.Order).Last().Order + 1; }
                steps.Add(new WorkflowStep() { Task = addedTask, Order = currentOrder });
                OnPropertyChanged("Steps");
            }
        }
        internal void OnTaskRemovedHandler(object sender, TaskConfig e)
        {
            WorkflowStep step = Workflow.Steps.Where(x => x.Task == e).FirstOrDefault();
            if (step == null) return;
            Workflow.Steps.Remove(step);
            OnPropertyChanged("Workflow");
        }

        #region Private
        private int GetNewWorkflowId()
        {
            return Workflow.Id + 1;
        }
        #endregion
    }
}
