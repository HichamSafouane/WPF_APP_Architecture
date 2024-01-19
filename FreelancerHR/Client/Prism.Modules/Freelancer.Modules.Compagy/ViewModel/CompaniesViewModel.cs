using FreelancerHR.DTO;
using FreelancerHR.Service.Contract;
using FreelancerHR.Infrastructure;
using Microsoft.Practices.Prism.Commands;
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
using System.Windows.Threading;

namespace Freelancer.Modules.Compagy.ViewModel
{
    [Export]
    public class CompaniesViewModel
    {
        private ObservableCollection<CompagnyDTO> companiesCollection;

        public DelegateCommand<string> EditCommand { get; private set; }

        public ObservableCollection<CompagnyDTO> CompaniesCollection
        {
            get { return companiesCollection; }
        }
        private ICollectionView companiesView;

        private IRegionManager regionManager;

        private ExportFactory<ICompagnyService> compagnyService;

        [ImportingConstructor]
        public CompaniesViewModel(ExportFactory<ICompagnyService> compagnyService, IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            EditCommand = new DelegateCommand<string>(OnEditExecuted);
            this.compagnyService = compagnyService;

            this.companiesCollection = new ObservableCollection<CompagnyDTO>();
            this.companiesView = CollectionViewSource.GetDefaultView(this.companiesCollection);// new ListCollectionView(this.companiesCollection);
            this.companiesView.CurrentChanged += CompaniesView_CurrentChanged;

            using (var serviceInstance = this.compagnyService.CreateExport())
            {
               foreach (var compagnydto in serviceInstance.Value.GetAllCompagnies())
                {
                    // this.CompaniesCollection.Add(compagnydto);
                    Application.Current.Dispatcher.Invoke(new Action(() => this.CompaniesCollection.Add(compagnydto)));
                }
            }

            //Task.Factory.StartNew(new Action(() =>
            //{
            //    foreach (var compagnydto in compagnyService.GetAllCompagnies())
            //    {
            //        // this.CompaniesCollection.Add(compagnydto);
            //        Application.Current.Dispatcher.Invoke(new Action(() => this.CompaniesCollection.Add(compagnydto)));
            //    }
            //}
            //));
        }

        void CompaniesView_CurrentChanged(object sender, EventArgs e)
        {
        }


        public ICollectionView Companies
        {
            get { return this.companiesView; }
        }

        void OnEditExecuted(string parameter)
        {
            var parameters = new NavigationParameters();
            parameters.Add("CompagnyID", this.companiesView.CurrentItem);

            this.regionManager.RequestNavigate(
                "CompagnyViewRegion",
                new Uri("EditCompanyView", UriKind.Relative), parameters);
        }
    }
}
