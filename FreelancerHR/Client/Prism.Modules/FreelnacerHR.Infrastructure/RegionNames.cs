using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Infrastructure
{
    public static class RegionNames
    {
        public const string MainRegion = "MainRegion";
        public const string MainNavigationRegion = "MainNavigationRegion";
        public const string MainContractDetailsRegion = "MainContractDetailsRegion";
        public const string HiringContractsRegion = "HiringContractsRegion";
        public const string HiringContractDetailsRegion = "HiringContractsDetailRegion";
    }

    public static class ViewNames
    {
        public const string CompaniesView = "/CompaniesView";
        public const string CompaniesNavigationItemView = "/CompaniesNavigationItemView";
        public const string FreelancersView = "/FreelancersView";
        public const string HiringRequestsView = "/HiringRequestsView";
        public const string HiringContractsView = "/HiringContractsView";
        public const string MainContractDetailsView = "/MainContractDetailsView";
        public const string HiringMainView = "/HiringMainView";
    }
}
