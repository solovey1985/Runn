using Runner.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                }));
            }
        }
        private BaseCommand cancelCommand;
        public BaseCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new BaseCommand(obj => {
                    Workflow = configService.LoadWorkflow(Workflow.Name);
                }));
            }
        }
        public BaseCommand selectionChangedCommand;

        public BaseCommand SelectionChangedCommand
        {
            get { return selectionChangedCommand ?? ( selectionChangedCommand = new BaseCommand(obj => {
                Workflow = configService.LoadWorkflow(obj.ToString());
                steps = new ObservableCollection<WorkflowStep>( Workflow.Steps);
                OnPropertyChanged("Steps");
                OnPropertyChanged("Workflow");

            })); }
        }

        #endregion
    }
}
