using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Commands
{
    public class RunCommand:BaseCommand
    {
        public string  CommandLine { get; set; }

        public override void Execute(object parameter)
        {
            base.Execute(parameter);
            CommandLine = (string)parameter;
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processInfo.FileName = "cmd.exe";
            processInfo.WorkingDirectory = Path.GetDirectoryName(CommandLine);
            processInfo.Arguments = "/c START " + Path.GetFileName(CommandLine);
            Process.Start(processInfo);
        }
    }
}
