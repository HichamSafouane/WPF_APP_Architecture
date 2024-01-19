using FreelancerHR.Infrastructure;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Modules.Compagy.ViewModel
{
    [Export]
    public class CompaniesNavigationItemViewModel : NavigationItemViewModel
    {

        public CompaniesNavigationItemViewModel()
            : base("CompanyMainView")
        {

        }
    }
}
