using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

using FreelancerHR.DTO;
using FreelancerHR.Modules.Hiring.ViewModel;
using FreelancerHR.Service.Contract;
using FreelancerHR.Infrastructure;
using Microsoft.Practices.Prism;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Regions;
using FreelancerHR.Modules.Hiring.Views;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FreelancerHR.Modules.Hiring.Controllers
{
    [Export(typeof(IHiringMainController))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class HiringMainController : IHiringMainController
    {
        private MainContractDetailsViewModel mainContractDetailsViewModel;
        private ContractsViewModel contractsViewModel;
        private HiringMainViewModel hiringMainViewModel;
        private ExportFactory<IHiringOfferService> hiringOfferService;
        private IRegionManager regionManager;
        private IEventAggregator eventAggregator;

        [ImportingConstructor]
        public HiringMainController(ExportFactory<IHiringOfferService> hiringOfferService, HiringMainViewModel hiringMainViewModel,
            MainContractDetailsViewModel mainContractDetailsViewModel, ContractsViewModel contractsViewModel, IRegionManager regionManager,
            IEventAggregator eventAggregator)
        {
            this.mainContractDetailsViewModel = mainContractDetailsViewModel;
            this.contractsViewModel = contractsViewModel;
            this.hiringOfferService = hiringOfferService;
            this.hiringMainViewModel = hiringMainViewModel;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;

            this.contractsViewModel.PropertyChanged += ContractsViewModelPropertyChanged;

            this.hiringMainViewModel.NavigatedTo += hiringMainViewModel_NavigatedTo;
        }

        private ExportLifetimeContext<IHiringOfferService> service;

        void hiringMainViewModel_NavigatedTo()
        {
            if (this.service != null)
            {
                service.Dispose();
                GC.Collect();
                GC.WaitForFullGCComplete();
                GC.Collect();
            }

            //create new Service for this page
            this.service = hiringOfferService.CreateExport();
            this.contractsViewModel.HiringOfferService = this.service.Value;
        }

       

        void ContractsViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ContractsViewModel contractsVM = sender as ContractsViewModel;
            if (contractsVM == null) {return;}

            if (e.PropertyName == Utilities.GetPropertyName((() => contractsVM.SelectedHiringOffer)))
            {
                IRegion region = this.regionManager.Regions[RegionNames.MainContractDetailsRegion];

                foreach (var view in region.Views.Where(v => v is FreelancerOfferViewModel))
                {
                    region.Remove(view);
                }

#if DEBUG
                GC.Collect();
                GC.WaitForFullGCComplete();
                GC.Collect();
#endif


                this.mainContractDetailsViewModel.CurrentHiringOffer = this.contractsViewModel.SelectedHiringOffer;

                if (this.mainContractDetailsViewModel.CurrentHiringOffer == null)
                {
                    return;
                }

                this.eventAggregator.GetEvent<FreelancerChangedEvent>().Publish(5);

                foreach (
                    var freelancer in
                        this.service.Value.GetFeelancersInOffer(
                            this.mainContractDetailsViewModel.CurrentHiringOffer.HiredEmployeeIDs.Select(
                                x => x.FreelancerID)))
                {
                    var freelancerOfferViewModel = ServiceLocator.Current.GetInstance<FreelancerOfferViewModel>();
                    freelancerOfferViewModel.FreelancerDto = freelancer;
                    region.Add(freelancerOfferViewModel);
                    region.Activate(freelancerOfferViewModel);
                }
            }
        }
    }
}
