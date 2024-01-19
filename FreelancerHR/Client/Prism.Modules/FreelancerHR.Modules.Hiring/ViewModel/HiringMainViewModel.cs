using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using FreelancerHR.Service.Contract;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;

namespace FreelancerHR.Modules.Hiring.ViewModel
{
    [Export]
    public class HiringMainViewModel : BindableBase, INavigationAware
    {
        public event Action NavigatedTo;

        [ImportingConstructor]
        public HiringMainViewModel()
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
 
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (NavigatedTo != null)
            {
                NavigatedTo();
            }
        }
    }
}
