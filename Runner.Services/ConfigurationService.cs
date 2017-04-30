using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Runner.Services.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Runner.Services.Workflows;

namespace Runner.Services
{
    public class ConfigurationService : BaseService, IDisposable, IConfigurationService
    {
        string _path;
   
        public string Path
        {
            get{ return _path; }
            set{ _path = value; }
        }

        public ConfigurationService(){}
   

        public void Dispose()
        {
           
        }

        public override void PostRun(Models.TaskConfig taskConfig)
        {
            
        }

        public override void PreRun(Models.TaskConfig taskConfig)
        {
           
        }

        public List<Models.TaskConfig> ReadConfigurationFromFile(string path)
        {
            Path = path;
            string configStr = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<TaskConfig>>(configStr, GetSerializingSetting());
        }

        public T GetTaskById<T>(int id) where T : TaskConfig
        {
            string configStr = File.ReadAllText(Path);
           List<T> configs =  JsonConvert.DeserializeObject<List<T>>(configStr, GetSerializingSetting());
            return (T)configs.FirstOrDefault(c => c.Id==id);
        }

        public void Save(IEnumerable<TaskConfig> configs)
        {
           var configString = JsonConvert.SerializeObject(configs.ToList(), GetSerializingSetting());
           File.WriteAllText(Path, configString);
        }

        private JsonSerializerSettings GetSerializingSetting()
        {
            return new JsonSerializerSettings(){
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All
            };
        }
        public void SaveWorkflow(string name, Workflow workflow)
        {
            var configString = JsonConvert.SerializeObject(workflow, GetSerializingSetting());
            File.WriteAllText($"{workflow.Name}.json", configString);
        }   
    } 
}
