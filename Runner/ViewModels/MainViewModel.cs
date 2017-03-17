using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runner.Commands;

namespace Runner.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        public MainViewModel()
        {
            CommandDictionary = new Dictionary<string, string>();
            CommandDictionary.Add("Build", "sdf");   
            CommandDictionary.Add("DbMigration", "DBMigrationPath");   
            CommandDictionary.Add("Depploy", "DeployCommandPath");   
             
            Commands = new List<string>();
            Commands.Add("Build");
            Commands.Add("DbMigration");
            Commands.Add("Depploy");
        }

        public Dictionary <string,string>  CommandDictionary { get; set; }
        public List<string> Commands { get; set; }
        public string CurrentCommand { get; set; }
        
        public RunCommand RunCommand { get; set; } 
        
    
    }
}
