using Runner.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Runner.Services.Workflows
{
    public class Workflow {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<WorkflowStep> Steps { get; set; }
        public bool IsRunning { get; set; }

        public bool Validate(){
            bool result = false;
            if (Steps != null && Steps.Any()){
                result = true;
            }
            return result;
        }
    }
}

public class WorkflowStep 
{
    public int Order { get; set; }
    public int TaskId { get { return Task?.Id ?? 0; } }
    public TaskConfig Task { get; set; }

    public bool Validate() {

        return Task != null && Order >= 0;
    }
}
