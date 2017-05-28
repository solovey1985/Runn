using Microsoft.Win32;
using Runner.Commands;
using Runner.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Tasks
{
    public partial class TaskViewModel
    {
        private BaseCommand saveCommand;
        public BaseCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                (saveCommand = new BaseCommand(obj => {
                    _confgService.Save(Tasks.ToList());

                }));
            }
        }
        private BaseCommand addCommand;
        public BaseCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new BaseCommand(obj =>
                  {
                      CurrentTask = new TaskConfig() { Id = Tasks.Max(x => x.Id) + 1, Name = "", Type = TaskType.CommandLine };
                      Tasks.Add(CurrentTask);
                      OnPropertyChanged("CurrentTask");
                  }));
            }
        }
        private BaseCommand showFileDialog;
        public BaseCommand ShowFileDialog
        {
            get
            {
                return showFileDialog ??
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
                              OnPropertyChanged("CurrentTask");
                          }
                      }));
            }
        }
        private BaseCommand removeCommand;
        public BaseCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new BaseCommand(obj => {
                        if (CurrentTask != null)
                        {
                            if (Tasks.Contains(CurrentTask))
                            {
                                Tasks.Remove(CurrentTask);
                                CurrentTask = new TaskConfig();
                            }
                        }
                    }));
            }
        }
        private BaseCommand credentialsRequiredCommand;
        public BaseCommand CredentialsRequiredCommand
        {
            get
            {
                return credentialsRequiredCommand ??
                    (credentialsRequiredCommand = new BaseCommand(obj => {
                        IWithCredentials task = CurrentTask as IWithCredentials;
                        if (task == null) return;
                        CredentilasInputArgs credentialsInputArgs = new CredentilasInputArgs(task.UserName);
                        CredentialsIputRequired(this, credentialsInputArgs);
                    }));
            }
        }
    }
}
