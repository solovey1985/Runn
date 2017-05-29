using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Runner.ViewModels;

namespace Runner.Framework
{
    public class NavigationHelper
    {
        private NavigationService service;
        string pageName;
        public NavigationHelper()
        {
             
        }
        public NavigationHelper(NavigationService navigationService)
        {
        }

        public void NavigateToPage(Page page, BaseViewModel model)
        {
            service.Navigate(page, model);
        }

        public void NavigateToPage<T>(Uri page, T model)
        {
            service.Navigate(page, model);
        }

        public void AddEntryToJournal(Page page, BaseViewModel model)
        {
            
        }
    }
}
