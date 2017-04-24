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
            var settings = new JsonSerializerSettings()
            {
                                //Converters = new List<JsonConverter> { new TaskCreationConverter() }
            };
            return JsonConvert.DeserializeObject<List<TaskConfig>>(configStr, settings);
        }

        public T GetTaskById<T>(int id) where T : TaskConfig
            {
            string configStr = File.ReadAllText(Path);
            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                TypeNameHandling = TypeNameHandling.All,
                //Converters = new List<JsonConverter> { new TaskCreationConverter() }
            };
            List<T> configs =  JsonConvert.DeserializeObject<List<T>>(configStr, settings);
            return (T)configs.FirstOrDefault(c => c.Id==id);
        }

        public void Save(IEnumerable<TaskConfig> configs)
        {
            var settings = new JsonSerializerSettings() {
                Formatting = Formatting.Indented,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                TypeNameHandling = TypeNameHandling.Auto,
               // Converters = new List<JsonConverter> { new TaskCreationConverter() }
            };
           var configString = JsonConvert.SerializeObject(configs.ToList(), settings);
           File.WriteAllText(Path, configString);
        }
    }
    public abstract class JsonBaseConverter<T> : JsonConverter
    {
        public abstract T Create(Type objectType, JObject jObject);
        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            T result;
            if (reader.TokenType == JsonToken.StartArray)
            {
                return serializer.Deserialize<List<T>>(reader);
            }
            else
            {
                var jsonObject = JObject.Load(reader);
                result = Create(objectType, jsonObject);
                serializer.Populate(reader, result);
                return result;
            }
            
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class TaskCreationConverter : JsonBaseConverter<TaskConfig>
    {
        public override TaskConfig Create(Type objectType, JObject jsonObject)
        {
            var typeName = jsonObject["Type"].ToString();
            TaskType type;
            Enum.TryParse<TaskType>(typeName, out type);
            switch (type)
            {
                case TaskType.Git:
                    return new GitTask();
                case TaskType.CommandLine:
                case TaskType.Executable:
                case TaskType.PowerShell:
                    return new TaskConfig();
                default:
                    return null;
            }
        }
    }
}
