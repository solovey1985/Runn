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
        string _path;
        public ConfigurationService (string path)
        {
            _path = path;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void PostRun(Models.TaskConfig taskConfig)
        {
            throw new NotImplementedException();
        }

        public override void PreRun(Models.TaskConfig taskConfig)
        {
            throw new NotImplementedException();
        }

        public List<Models.TaskConfig> ReadConfigurationFromFile()
        {
            string configStr = File.ReadAllText(_path);
            return JsonConvert.DeserializeObject<List<TaskConfig>>(configStr);
        }

        public T GetTaskById<T>(int id) where T : TaskConfig
            {
            string configStr = File.ReadAllText(_path);
            List<T> configs =  JsonConvert.DeserializeObject<List<T>>(configStr);
            return (T)configs.FirstOrDefault(c => c.Id==id);
        }
    }
}
