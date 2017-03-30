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
}