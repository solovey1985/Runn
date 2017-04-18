using Runner.Services;
using Runner.Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Tasks
{
     public class TaskViewModel : INotifyPropertyChanged
    {
        IConfigurationService _confgService;

        public event PropertyChangedEventHandler PropertyChanged;

        public TaskViewModel(IConfigurationService confgService)
        {
            _confgService = confgService;
            Tasks = _confgService.ReadConfigurationFromFile("config.json");
            TaskTypes = new ObservableCollection<string>(Enum.GetValues(typeof(TaskType)).Cast<TaskType>().Select(v => v.ToString()).ToList());
            
        }
        public List<TaskConfig> Tasks { get; set; } 
        TaskConfig _currentTask { get; set; }
        public TaskConfig CurrentTask { get { return _currentTask; }
        set { _currentTask = value;  OnPropertyChanged("CurrentTask"); } }

        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public ObservableCollection<string> TaskTypes { get; set ;}

        public void OnTaskChanged(TaskConfig config)
        {
            CurrentTask = config;
        }
    }
}
