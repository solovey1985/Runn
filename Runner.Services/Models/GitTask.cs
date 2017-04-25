using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
namespace Runner.Services.Models
{
    public class GitTask : TaskConfig, IWithCredentials
    {
        public GitOperations Operation { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Branch { get; set; }
        public string RemoteRepo { get; set; }
    }

    public interface IWithCredentials
    {
        string UserName { get; set; }
        string Password { get; set; }
        
    }

    public enum GitOperations
    {
        Fetch = 0,
        Pull = 1,
        Push = 2,
        Commit = 3,
        Merge = 4
    }
}
