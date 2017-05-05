using Runner.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WF = Runner.Services.Workflows.Workflow;
namespace Runner.Workflows
{
    public partial class WorkflowViewModel
    {
        #region Commands
        private BaseCommand addCommand;
        public BaseCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new BaseCommand(obj => {
                    Workflow = new WF() { Steps = new List<WorkflowStep>(), Name = "New workflow", Description = String.Empty, Id = GetNewWorkflowId() };
                    steps = new ObservableCollection<WorkflowStep>(Workflow.Steps);
                    OnPropertyChanged("Workflow");
                    OnPropertyChanged("Steps");
                }));
            }

        }
        private BaseCommand saveCommand;
        public BaseCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new BaseCommand(obj => {
                    Workflow.Steps = steps.ToList();
                    configService.SaveWorkflow(Workflow.Name, Workflow);
                    WorkflowsList = configService.GetAllWorkflows();
                    OnPropertyChanged("WorkflowsList");
                }));
            }
        }
        private BaseCommand cancelCommand;
        public BaseCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new BaseCommand(obj => {
                    Workflow = configService.GetWorkflow(Workflow.Name);
                }));
            }
        }
        private BaseCommand selectionChangedCommand;
        public BaseCommand SelectionChangedCommand
        {
            get { return selectionChangedCommand ?? ( selectionChangedCommand = new BaseCommand(obj => {
                if (obj == null) return;
                Workflow = configService.GetWorkflow(obj.ToString());
                steps = new ObservableCollection<WorkflowStep>( Workflow.Steps);
                OnPropertyChanged("Steps");
                OnPropertyChanged("Workflow");

            })); }
        }
        private BaseCommand deleteTaskCommand;
        public BaseCommand DeleteTaskCommand
        {
            get
            {
                return deleteTaskCommand ?? (deleteTaskCommand = new BaseCommand(obj => {
                    var task = obj as WorkflowStep;
                    if (task == null) return;
                    steps.Remove(task);
                    OnPropertyChanged("Steps");
                }));
            }
        }
        private BaseCommand deleteCommand;
        public BaseCommand DeleteCommand { get { return deleteCommand ?? (deleteCommand = new BaseCommand(obj => {
            configService.DeleteWorkflow(Workflow);
            WorkflowsList = configService.GetAllWorkflows();
            string firstWorkflow = WorkflowsList.FirstOrDefault();
            if (!string.IsNullOrEmpty(firstWorkflow))
            {
                Workflow = configService.GetWorkflow(firstWorkflow);
                OnPropertyChanged("WorkflowsList");
                OnPropertyChanged("Workflow");
                OnPropertyChanged("Steps");
                
            }
        })); }
        }
        #endregion
    }
}
