using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Runner.Services.Models;
using LibGit2Sharp.Handlers;

namespace Runner.Services
{
    public class GitService : BaseService
    {
        public override bool Run(TaskConfiguration taskConfig)
        {
            using (var repo = new Repository(taskConfig.PathToFile))
            {
                LibGit2Sharp.PushOptions options = new LibGit2Sharp.PushOptions();
               
                options.CredentialsProvider = new CredentialsHandler(
                    (url, usernameFromUrl, types) =>
                        new UsernamePasswordCredentials()
                        {
                            Username = taskConfig.UserName,
                            Password = taskConfig.Password
                        });
                var remote = repo.Network.Remotes["origin"];
                repo.Network.Push(remote, @"refs/heads/master", options);
                return true;
            }
            
        }

        public override void PostRun(TaskConfiguration taskConfig)
        {
            
        }

        public override void PreRun(TaskConfiguration taskConfig)
        {
            
        }
    }
}
