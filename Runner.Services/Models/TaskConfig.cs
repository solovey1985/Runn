using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace Runner.Services.Models
{
    [JsonObject]
    public class TaskConfig
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathToFile { get; set; }
        public string PathToUtil { get; set; }
        public string[] Parameters { get; set; }
        public TaskType Type { get; set; }
        public bool Validate()
        {
            bool result = true;
            if (String.IsNullOrEmpty(PathToFile))
            {
                result = false;
            }
            return result;
        }
        public bool Validate(List<TaskConfig> tasksToAdd)
        {
            bool canBeAdded = false;
            canBeAdded = !tasksToAdd.Any(x => x.Name == Name || x.PathToFile == PathToFile);
            return canBeAdded && Validate();
        }
   }

    public enum TaskType
    {
        Executable = 1,
        CommandLine = 2,
        PowerShell = 3 ,
        Git = 4
    }
}
