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
using Runner.Services.Framework;

namespace Runner.Services
{
    public class ConfigurationService : BaseService, IDisposable, IConfigurationService
    {
        private const string workflowDir = "workflows";
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
           List<TaskConfig> configs =  JsonConvert.DeserializeObject<List<TaskConfig>>(configStr, GetSerializingSetting());
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
            FileSystemHelper.ExistOrCreateDirectory(workflowDir);
            var configString = JsonConvert.SerializeObject(workflow, GetSerializingSetting());
            File.WriteAllText($"{workflowDir}\\{workflow.Name}.json", configString);
        }

        public Workflow GetWorkflow(string name)
        {
            var workflowString = File.ReadAllText($"{workflowDir}\\{name}.json");
            return JsonConvert.DeserializeObject<Workflow>(workflowString);
        }

        public List<string> GetAllWorkflows()
        {
            return Directory.GetFiles(workflowDir).Select(x => x.Replace(".json", string.Empty).Replace($"{workflowDir}\\", string.Empty)).ToList();
        }

        public void DeleteWorkflow(Workflow workflow)
        {
            if (workflow == null) return;
            string pathToDelete = System.IO.Path.Combine(workflowDir, $"{workflow.Name}.json");
            if (File.Exists(System.IO.Path.Combine(pathToDelete)))
            {
                File.Delete(pathToDelete);
            }
        }

        #region Private Methods

        #endregion
    }
}
