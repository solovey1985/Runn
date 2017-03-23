using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Services.Models
{
    public class GitTask : TaskConfig
    {
        public GitOperations Operation { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Branch { get; set; }
        public string RemoteRepo { get; set; }
    }

    public enum GitOperations
    {
        Fetch,
        Pull,
        Push,
        Commit,
        Merge
    }
}
