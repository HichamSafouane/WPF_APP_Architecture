using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using FreelancerHR.Infrastructure;
using FreelancerHR.Modules.Compagy.Views;

namespace FreelancerHR.Modules.Compagy
{
    [ModuleExport(typeof(CompagnyModule))]
    public class CompagnyModule : IModule
    {
        [Import]
        public IRegionManager RegionManager;

        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(CompanyMainView));
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(CompaniesNavigationItemView));
            this.RegionManager.RegisterViewWithRegion("CompagnyViewRegion", typeof(CompaniesView));
            this.RegionManager.RegisterViewWithRegion("CompagnyViewRegion", typeof(EditCompanyView));

            this.RegionManager.RequestNavigate("CompagnyViewRegion",new Uri("CompaniesView", UriKind.Relative));
        }
    }
}
