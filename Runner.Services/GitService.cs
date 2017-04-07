using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Runner.Services.Models;
using LibGit2Sharp.Handlers;
using Runner.Services;

namespace Runner.Services
{
    

    public class GitService : BaseService, IGitService
    {

        
        public GitTask CurrentTask { get; set; }

  
        public void SetCredentials(string user, string pass)
        {
            
        }

        public override bool Run(TaskConfig taskConfig)
        {
            GitTask config = taskConfig as GitTask;
            if (config == null)
                config = CurrentTask;
            if (config != null)
            {
                Pull(config);
            }
            return false;
            
        }

        private void GitPush(GitTask config)
        {
            using (var repo = new Repository(config.PathToFile))
            {
                PushOptions options = new PushOptions();

                options.CredentialsProvider = new CredentialsHandler(
                                                                     (url, usernameFromUrl, types) =>
                                                                             GetCredentials(config));
                var remote = repo.Network.Remotes["origin"];
                repo.Network.Push(remote, @"refs/heads/develop", options);
                
            }
        }

        public void Pull(GitTask taskConfig)
        {
            using (var repo = new Repository(taskConfig.PathToFile))
            {
                LibGit2Sharp.PullOptions options = new LibGit2Sharp.PullOptions();
                options.FetchOptions = new FetchOptions();
                options.FetchOptions.CredentialsProvider = new CredentialsHandler(
                    (url, usernameFromUrl, types) =>
                        new UsernamePasswordCredentials()
                        {
                            Username = taskConfig.UserName,
                            Password = CryptoService.Decrypt(taskConfig.Password, "q2#r44C&m(%mytp0")
                        });
                repo.Network.Pull(new LibGit2Sharp.Signature(taskConfig.UserName, taskConfig.UserName, new DateTimeOffset(DateTime.Now)), options);
            }
        }
        private PushOptions GetPushOptions()
        {
            return new PushOptions();
        }

        private UsernamePasswordCredentials GetCredentials(GitTask task)
        {
            if (!string.IsNullOrEmpty(task.UserName) && !string.IsNullOrEmpty(task.Password))
            {
                return new UsernamePasswordCredentials()
                {
                    Username = task.UserName,
                    Password = CryptoService.Decrypt(task.Password, "q2#r44C&m(%mytp0")
                };
            }
            return new UsernamePasswordCredentials();
        }

        public override void PostRun(Models.TaskConfig taskConfig)
        {
            
        }

        public override void PreRun(Models.TaskConfig taskConfig)
        {
            
        }
    }
}
