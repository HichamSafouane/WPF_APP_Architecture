using FreelancerHR.DTO;
using FreelancerHR.Modules.Hiring.ViewModel;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FreelancerHR.Modules.Hiring.Views
{
    /// <summary>
    /// Interaction logic for ContractDetailsView.xaml
    /// </summary>
    [Export]
    public partial class ContractDetailsView : UserControl
    {
        public ContractDetailsView()
        {
            InitializeComponent();

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e)
                                                            =>
                                                            ViewModel.CurrentHiringOffer =
                                                            RegionContext.GetObservableContext(this).Value
                                                            as HiringOfferDTO;
        }

        [Import]
        ContractDetailsViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
            get { return this.DataContext as ContractDetailsViewModel; }
        }
    }
}
