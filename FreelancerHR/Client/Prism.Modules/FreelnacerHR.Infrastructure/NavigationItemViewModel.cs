using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Infrastructure
{
    public class NavigationItemViewModel : BindableBase, IPartImportsSatisfiedNotification
    {

        [Import]
        public IRegionManager regionManager;


        private bool isChecked;

        private string viewName;

        public NavigationItemViewModel(string viewName)
        {
            this.viewName = viewName;
        }

        public void OnImportsSatisfied()
        {
            IRegion mainContentRegion = this.regionManager.Regions[RegionNames.MainRegion];
            if (mainContentRegion != null && mainContentRegion.NavigationService != null)
            {
                mainContentRegion.NavigationService.Navigated += this.MainRegion_Navigated;
            }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                 if (isChecked == value) return;

                isChecked = value;
                OnPropertyChanged("IsChecked");

                if (isChecked)
                    this.regionManager.RequestNavigate(RegionNames.MainRegion, new Uri(this.viewName, UriKind.Relative));
            }
        }

        public void MainRegion_Navigated(object sender, RegionNavigationEventArgs e)
        {
            this.UpdateNavigationButtonState(e.Uri);
        }

        private void UpdateNavigationButtonState(Uri uri)
        {
            this.IsChecked = (uri == new Uri(this.viewName, UriKind.Relative));
        }
    }
}
