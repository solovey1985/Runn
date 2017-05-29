using Runner.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;
[Serializable]
public class RunnerContentState<T> : CustomContentState where T:BaseViewModel
{
    Uri page;
    T viewModel;

    public RunnerContentState(Uri page, T viewModel)
    {
        this.page = page;
        this.viewModel = viewModel;
    }

    public override string JournalEntryName
    {
        get
        {
            return "Journal Entry " + viewModel.Title;
        }
    }

    public override void Replay(NavigationService navigationService, NavigationMode mode)
    {
        navigationService.Navigate(page, viewModel);
    }


}