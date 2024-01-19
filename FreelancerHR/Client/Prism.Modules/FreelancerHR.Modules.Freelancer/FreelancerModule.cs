using FreelancerHR.Modules.Freelancer.Views;
using FreelancerHR.Infrastructure;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Modules.Freelancer
{
    [ModuleExport(typeof(FreelancerModule))]
    public class FreelancerModule : IModule
    {
        [Import]
        public IRegionManager RegionManager;

        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(FreelancersView));
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(FreelancerNavigationItemView));
        }
    }
}
