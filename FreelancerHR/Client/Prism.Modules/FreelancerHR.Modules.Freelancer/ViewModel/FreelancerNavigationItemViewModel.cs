using FreelancerHR.Infrastructure;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Modules.Freelancer.ViewModel
{
    [Export]
    class FreelancerNavigationItemViewModel : NavigationItemViewModel
    {

        public FreelancerNavigationItemViewModel()
            : base(ViewNames.FreelancersView)
        {

        }
    }
}
