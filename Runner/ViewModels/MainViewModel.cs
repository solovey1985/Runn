using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runner.Commands;
using Runner.Services.Models;
using Runner.Services;

namespace Runner.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        ConfigurationService configService;
       
        public MainViewModel()
        {
            configService = new ConfigurationService();
            Configurations = configService.ReadConfigurationFromFile("config.json");
            RunCommand = new RunCommand();
         
        }

        public List<TaskConfiguration> Configurations { get; set; }
        public TaskConfiguration CurrentCommand { get; set; }
        public RunCommand RunCommand { get; set; } 
        
    
    }
}
