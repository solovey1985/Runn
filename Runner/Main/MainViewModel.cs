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
    public partial class MainViewModel:BaseViewModel
    {
        IConfigurationService _configService;
        private BaseService taskRunner;
        public MainViewModel(IConfigurationService configService)
        {
            _configService = configService;
            Configurations = _configService.ReadConfigurationFromFile("config.json");
        }

        private TaskConfig CurrentTask { get; set; }
        
        public List<Services.Models.TaskConfig> Configurations { get; set; }
        
        
    
    }
}
