using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Runner.Services.Models;

namespace Runner.Services
{
    public class ConfigurationService: BaseService, IDisposable
    {
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void PostRun(TaskConfiguration taskConfig)
        {
            throw new NotImplementedException();
        }

        public override void PreRun(TaskConfiguration taskConfig)
        {
            throw new NotImplementedException();
        }

        public List<TaskConfiguration> ReadConfigurationFromFile(string path)
        {
            string configStr = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<TaskConfiguration>>(configStr);
        }
    }
}
