using FreelancerHR.DTO;
using FreelancerHR.Service.Contract;
using FreelancerHR.Infrastructure;
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

namespace FreelancerHR.Modules.Freelancer.ViewModel
{
    [Export]
    public class FreelancersViewModel : BindableBase
    {
        private ObservableCollection<FreelancerDTO> freelancersCollection;

        public ObservableCollection<FreelancerDTO> FreelancersCollection
        {
            get { return freelancersCollection; }
        }
        private ICollectionView freelancersView;

        private IRegionManager regionManager;

        private ExportFactory<IFreelancerService> freelancerService;

        [ImportingConstructor]
        public FreelancersViewModel(ExportFactory<IFreelancerService> freelancerService, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.freelancerService = freelancerService;

            this.freelancersCollection = new ObservableCollection<FreelancerDTO>();
            this.freelancersView = CollectionViewSource.GetDefaultView(this.freelancersCollection);// new ListCollectionView(this.freelancersCollection);
            this.freelancersView.CurrentChanged += FreelancersView_CurrentChanged;

            using (var serviceInstance = this.freelancerService.CreateExport())
            {
                foreach (var freelancerdto in serviceInstance.Value.GetAllFreelancers())
                {
                    // this.FreelancersCollection.Add(compagnydto);
                    Application.Current.Dispatcher.Invoke(new Action(() => this.FreelancersCollection.Add(freelancerdto)));
                }
            }



            //Task.Factory.StartNew(new Action(() =>
            //{
            //    foreach (var compagnydto in freelancerService.GetAllFreelancers())
            //    {
            //        Application.Current.Dispatcher.Invoke(new Action(() => this.FreelancersCollection.Add(compagnydto)));
            //    }
            //}
            //));
        }

        void FreelancersView_CurrentChanged(object sender, EventArgs e)
        {
        }

    }
}