using FreelancerHR.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerHR.Service.Contract;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using FreelancerHR.Infrastructure;

namespace FreelancerHR.Modules.Hiring.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FreelancerOfferViewModel : BindableBase
    {

        private HiringOfferDTO currentHiringOffer;

        private IHiringOfferService hiringOfferService;

        private IEventAggregator eventAggregator;

        private static int count;

        [ImportingConstructor]
        public FreelancerOfferViewModel()
        {
            count++;

        }

        public FreelancerDTO FreelancerDto
        {
            get { return freelancerDto; }
            set { SetProperty(ref freelancerDto , value); }
        }

        private FreelancerDTO freelancerDto;

        ~FreelancerOfferViewModel()
        {
            count--;
        }
    }
}
