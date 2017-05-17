using Runner.Services.Models;
using Runner.Services.Workflows;
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

        void Save(IEnumerable<TaskConfig> configs);
        void SaveWorkflow(string name, Workflow workflow);
        List<string> GetAllWorkflows();
        Workflow GetWorkflow(string name);
        void DeleteWorkflow(Workflow workflow);
    }
    public interface IWorkflowService
    {
        void Run(Workflow workflow);
    }
}