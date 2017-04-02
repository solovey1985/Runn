using Runner.Services.Models;
using System.Collections.Generic;

namespace Runner.Services
{
    public interface IBaseService {
        bool Run(Models.TaskConfig taskConfig);

        void PreRun(Models.TaskConfig taskConfig);

        void PostRun(Models.TaskConfig taskConfig);
    }

    public interface IGitService : IBaseService { }

    public interface ISimpleTaskService : IBaseService { }

    public interface IPowerShellService : IBaseService { }

    public interface IConfigurationService : IBaseService
    {
        string Path { get; set; }

        T GetTaskById<T>(int id) where T : TaskConfig;
        List<TaskConfig> ReadConfigurationFromFile(string path);
    }
}