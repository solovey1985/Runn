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
        IConfigurationService _configService;
       
        public MainViewModel(IConfigurationService configService, RunCommand command)
        {
            _configService = configService;
            Configurations = _configService.ReadConfigurationFromFile("config.json");
            RunCommand = command;
         
        }

        public List<Services.Models.TaskConfig> Configurations { get; set; }
        public RunCommand RunCommand { get; set; } 
        
    
    }
}
