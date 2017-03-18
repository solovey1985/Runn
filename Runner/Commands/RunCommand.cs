using Runner.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runner.Services;
namespace Runner.Commands
{
    public class RunCommand:BaseCommand
    {
        private SimpleTaskService taskRunner;
        public string  CommandLine { get; set; }
        public TaskConfiguration TaskConfiguration { get; set; }
        public RunCommand()
        {
            taskRunner = new SimpleTaskService();
        }
        public override void Execute(object parameter)
        {
            TaskConfiguration current = (TaskConfiguration)parameter;
            if (current != null)
            taskRunner.Run(current);
        }
    }
}
