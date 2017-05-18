using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Runner.Services.Models;
using LibGit2Sharp.Handlers;
using LibGit2Sharp.Core;
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
            switch (config.Operation)
            {
                    case GitOperation.Pull: { Pull(config); break;}
                    case GitOperation.Fetch: { Fetch(config); break; }
                    case GitOperation.Commit: { break; }
                    case GitOperation.Push: {Push(config); break; }
            }
            return false;
            
        }

        private void Push(GitTask config)
        {
            using (var repo = new Repository(config.PathToFile))
            {
                PushOptions options = new PushOptions();
                options.CredentialsProvider = new CredentialsHandler( (url, usernameFromUrl, types) =>
                                                                                GetCredentials(config));
                var remote = repo.Network.Remotes["origin"];
                repo.Network.Push(remote, @"refs/heads/master", options);
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
                            Password = CryptoService.Decrypt(taskConfig.Password)
                        });
                Fetch(taskConfig);
                Commands.Pull(repo, new Signature(taskConfig.UserName, taskConfig.UserName, DateTimeOffset.Now), options);
            }
        }

        public void Fetch(GitTask taskConfig)
        {
            using(var repo = new Repository(taskConfig.PathToFile))
            {
                FetchOptions options = new FetchOptions();
                options.CredentialsProvider = new CredentialsHandler((url, usernameFromUrl, types) =>
                    new UsernamePasswordCredentials()
                    {
                        Username = taskConfig.UserName,
                        Password = CryptoService.Decrypt(taskConfig.Password)
                    });

                foreach (Remote remote in repo.Network.Remotes)
                {
                    IEnumerable<string> refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                    Commands.Fetch(repo, remote.Name, refSpecs, null, "");
                }
            }
        }
        public override void PostRun(Models.TaskConfig taskConfig)
        {

        }

        public override void PreRun(Models.TaskConfig taskConfig)
        {

        }

        #region Private methods
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
                    Password = CryptoService.Decrypt(task.Password)
                };
            }
            return new UsernamePasswordCredentials();
        }
        #endregion
        
    }
}
