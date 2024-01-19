using FreelancerHR.DTO;
using FreelancerHR.Modules.Hiring.Controllers;
using FreelancerHR.Service.Contract;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;


namespace FreelancerHR.Modules.Hiring.ViewModel
{
    [Export]
    public class ContractsViewModel : BindableBase
    {
       private ObservableCollection<HiringOfferDTO> hiringOffersCollection;

        public DelegateCommand<string> EditCommand { get; private set; }

        public ObservableCollection<HiringOfferDTO> HiringOffersCollection
        {
            get { return hiringOffersCollection; }
        }
        private ICollectionView offersView;

        private IRegionManager regionManager;

        private IHiringOfferService hiringOfferService;

        public IHiringOfferService HiringOfferService
        {
            get { return hiringOfferService; }
            set
            {
                this.HiringOffersCollection.Clear();
                hiringOfferService = value;
                foreach (var hiringOfferdto in hiringOfferService.GetAllHiringOffers())
                {
                    // this.hiringOffersCollection.Add(HiringOfferDTO);
                    Application.Current.Dispatcher.Invoke(new Action(() => this.HiringOffersCollection.Add(hiringOfferdto)));
                    this.offersView.MoveCurrentToFirst();
                }
            }
        }

        [ImportingConstructor]
        public ContractsViewModel( IRegionManager regionManager)
        {
            EditCommand = new DelegateCommand<string>(OnEditExecuted);

            this.hiringOffersCollection = new ObservableCollection<HiringOfferDTO>();
            this.offersView = CollectionViewSource.GetDefaultView(this.hiringOffersCollection);// new ListCollectionView(this.hiringOffersCollection);
            this.offersView.CurrentChanged += OffersViewCurrentChanged;
        }

        private HiringOfferDTO selectedHiringOffer;

        public HiringOfferDTO SelectedHiringOffer
        {
            get { return selectedHiringOffer; }
            set
            {
                SetProperty(ref selectedHiringOffer, value);
            }
        }

        void OffersViewCurrentChanged(object sender, EventArgs e)
        {
            SelectedHiringOffer = this.offersView.CurrentItem as HiringOfferDTO;
        }


        public ICollectionView Offers
        {
            get { return this.offersView; }
        }

        void OnEditExecuted(string parameter)
        {
            var parameters = new NavigationParameters();
            parameters.Add("CompagnyID", this.offersView.CurrentItem);

            this.regionManager.RequestNavigate(
                "CompagnyViewRegion",
                new Uri("EditCompanyView", UriKind.Relative), parameters);
        }
    }
}
