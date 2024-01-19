using FreelancerHR.DTO;
using FreelancerHR.Service.Contract;
using Microsoft.Practices.Prism.Commands;
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
    public class EditCompagnyViewModel : BindableBase, INavigationAware
    {
        private IRegionManager regionManager;
        private ExportFactory<ICompagnyService> compagnyService;

        public DelegateCommand BackCommand { get; private set; }

        public DelegateCommand SaveCommand { get; private set; }

        private CompagnyDTO compagny;

        public CompagnyDTO Compagny
        {
            get { return compagny; }
            set 
            { 
                compagny = value;
                OnPropertyChanged(() => Compagny);
            }
        }

        [ImportingConstructor]
        public EditCompagnyViewModel(ExportFactory<ICompagnyService> compagnyService, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.compagnyService = compagnyService;

            BackCommand = new DelegateCommand(BackExecuted);
            SaveCommand = new DelegateCommand(SaveExecuted);
        }

        private void BackExecuted()
        {
            this.regionManager.RequestNavigate(
                "CompagnyViewRegion",
                new Uri("CompaniesView", UriKind.Relative));
        }

        private void SaveExecuted()
        {
            using (var serviceInstance = this.compagnyService.CreateExport())
            {
                serviceInstance.Value.Update(Compagny);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var param = navigationContext.Parameters["CompagnyID"];
            CompagnyDTO compagnyDto = param as CompagnyDTO;
            if (compagnyDto != null)
            {
                using (var serviceInstance = this.compagnyService.CreateExport())
                {
                    Compagny = serviceInstance.Value.GetCompagnyByID(compagnyDto.CompagnyID);
                }

            }

            return true;
            
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
    
        }
    }
}
