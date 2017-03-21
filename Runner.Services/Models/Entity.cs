using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Services.Models
{
    public abstract class Entity
    {
    }

    public class TaskConfiguration : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathToFile { get; set; }
        public string PathToUtil { get; set; }
        public string[] Parameters { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; internal set; }
    }
}
