using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Runner.Services.Models;

namespace Runner.Services
{
   

    public class SimpleTaskService : BaseService, ISimpleTaskService
    {
        public override void PostRun(Models.TaskConfig taskConfig)
        {
            
        }

        public override void PreRun(Models.TaskConfig taskConfig)
        {
            
        }
    }
}
