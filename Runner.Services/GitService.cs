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
        public override bool Run(Models.TaskConfig taskConfig)
        {
            GitTask config = taskConfig as GitTask;
            if(config != null)
            using (var repo = new Repository(taskConfig.PathToFile))
            {
                PushOptions options = new PushOptions();
               
                options.CredentialsProvider = new CredentialsHandler(
                    (url, usernameFromUrl, types) =>
                        new UsernamePasswordCredentials()
                        {
                            Username = config.UserName,
                            Password = config.Password
                        });
                var remote = repo.Network.Remotes["origin"];
                repo.Network.Push(remote, @"refs/heads/master", options);
                return true;
            }
            return false;
            
        }
        
        public override void PostRun(Models.TaskConfig taskConfig)
        {
            
        }

        public override void PreRun(Models.TaskConfig taskConfig)
        {
            
        }
    }
}
