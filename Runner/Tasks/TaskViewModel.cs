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
using Runner.Shared.Credentials;
using Runner.ViewModels;

namespace Runner.Tasks
{
    public partial class TaskViewModel : BaseViewModel
    {
       IConfigurationService _confgService;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<CredentilasInputArgs> CredentialsIputRequired;
        public TaskViewModel(IConfigurationService confgService)
        {
            _confgService = confgService;
            Tasks = new ObservableCollection<TaskConfig>( _confgService.ReadConfigurationFromFile("config.json"));
            TaskTypes = new ObservableCollection<ComboData<TaskType>>(Enum.GetValues(typeof(TaskType)).Cast<TaskType>().Select(v => new ComboData<TaskType> { Value = v, Text = v.ToString() }).ToList());
            GitOperations = new ObservableCollection<ComboData<GitOperation>>(Enum.GetValues(typeof(GitOperation)).Cast<GitOperation>().Select(x => new ComboData<GitOperation> { Value = x, Text = x.ToString() }).ToList());
            CurrentTask = new TaskConfig();


        }
        public void OnCredentialsInputed(object e, CredentilasInputArgs args)
        {
            if (args != null)
            {
                if(CurrentTask.Type == TaskType.Git)
                {

                    IWithCredentials task = CurrentTask as IWithCredentials;
                    if (task == null) return;
                    task.UserName = args.Login;
                    task.Password = CryptoService.Encrypt(args.Password);
                    OnPropertyChanged("CurrentTask");
                    _confgService.Save(Tasks);
                }
            }
        }
       
        
        public ObservableCollection<TaskConfig> Tasks { get; set; }
        TaskConfig _currentTask { get; set; }
        public TaskConfig CurrentTask { get { return _currentTask; }
            set { _currentTask = value; OnPropertyChanged("CurrentTask"); } }
        public CredentialsModel Credentials { get; set; }
        public ObservableCollection<ComboData<TaskType>> TaskTypes { get; set; }
        public ObservableCollection<ComboData<GitOperation>> GitOperations { get; set; }
        public void OnTaskChanged(TaskConfig config)
        {
            CurrentTask = config;
        }
    }
    public class CredentilasInputArgs : EventArgs
    {
        public CredentilasInputArgs()
        {
        }

        public CredentilasInputArgs(string userName)
        {
           Login = userName;
        }

        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class ComboData<T>
    {
        public T Value { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
