using Microsoft.Win32;
using Runner.Commands;
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
            TaskTypes = new ObservableCollection<ComboData>(Enum.GetValues(typeof(TaskType)).Cast<TaskType>().Select(v => new ComboData {Value = v, Text = v.ToString() }).ToList());
            CurrentTask = new TaskConfig();
            
            
        }


        private BaseCommand saveCommand;
        public BaseCommand SaveCommand {
            get {
                    return saveCommand ??
                    (saveCommand = new BaseCommand(obj => {
                        _confgService.Save(Tasks.ToList());
                    }));
            }
        }
        private BaseCommand addCommand;
        public BaseCommand AddCommand
        {
            get { return addCommand ??
                    (addCommand = new BaseCommand(obj => {
                        if (CurrentTask.Validate(Tasks.ToList()))
                        {
                            Tasks.Add(CurrentTask);
                        }
                    })); }
        }
        private BaseCommand showFileDialog;
        public BaseCommand ShowFileDialog
        {
            get { return showFileDialog ??
                        (showFileDialog = new BaseCommand(obj => {
                            string param = obj.ToString();
                            OpenFileDialog dialog = new OpenFileDialog();
                            if (dialog.ShowDialog() == true)
                            {
                                switch (param)
                                {
                                    case "File": CurrentTask.PathToFile = dialog.FileName; break;
                                    case "Util": CurrentTask.PathToUtil = dialog.FileName; break;
                                }
                            }
                        })); }
        }
        private BaseCommand removeCommand;
        public BaseCommand RemoveCommand { get
            {
                return removeCommand ??
                    (removeCommand = new BaseCommand(obj=> {
                        if(CurrentTask != null)
                        {
                            if (Tasks.Contains(CurrentTask))
                            {
                                Tasks.Remove(CurrentTask);
                                CurrentTask = new TaskConfig();
                            }
                        }
                    }));
            } }

        public List<TaskConfig> Tasks { get; set; } 
        TaskConfig _currentTask { get; set; }
        public TaskConfig CurrentTask { get { return _currentTask; }
        set { _currentTask = value;  OnPropertyChanged("CurrentTask"); } }

        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public ObservableCollection<ComboData> TaskTypes { get; set ;}

        public void OnTaskChanged(TaskConfig config)
        {
            CurrentTask = config;
        }
    }

    public class ComboData
    {
        public TaskType Value { get;set;}
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
