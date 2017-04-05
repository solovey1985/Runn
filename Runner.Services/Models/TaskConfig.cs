using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Services.Models
{
    
    public class TaskConfig
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathToFile { get; set; }
        public string PathToUtil { get; set; }
        public string[] Parameters { get; set; }

        public bool Validate()
        {
            bool result = true;
            if (String.IsNullOrEmpty(PathToFile) || String.IsNullOrEmpty(PathToUtil))
            {
                result = false;
            }
            return result;
        }
   }
}
