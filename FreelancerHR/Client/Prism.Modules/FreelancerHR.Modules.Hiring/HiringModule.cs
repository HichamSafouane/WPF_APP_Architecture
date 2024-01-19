using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using FreelancerHR.Infrastructure;
using System.ComponentModel.Composition;
using FreelancerHR.Modules.Hiring.Views;


namespace FreelancerHR.Modules.Hiring
{
    [ModuleExport(typeof(HiringModule))]
    class HiringModule : IModule
    {
        [Import]
        public IRegionManager RegionManager;

        public void Initialize()
        {
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(HiringMainView));

            this.RegionManager.RegisterViewWithRegion(RegionNames.HiringContractDetailsRegion, typeof(MainContractDetailsView));
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(HiringRequestNavigationItemView));

            this.RegionManager.RegisterViewWithRegion(RegionNames.HiringContractsRegion, typeof(ContratsView));
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainContractDetailsRegion, typeof(ContractDetailsView));
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainContractDetailsRegion, typeof(FreelancerDetailsSeparatorView));
            //this.RegionManager.RegisterViewWithRegion(RegionNames.MainContractDetailsRegion,
            //    typeof (FreelancerOfferView));

            //IRegion ordersRegion = this.RegionManager.Regions[RegionNames.MainContractDetailsRegion];
            //  ordersRegion.Add(new ContratsView());
            //  this.RegionManager.RegisterViewWithRegion(RegionNames.MainContractDetailsRegion, typeof(ContratsView));

        }
    }
}
