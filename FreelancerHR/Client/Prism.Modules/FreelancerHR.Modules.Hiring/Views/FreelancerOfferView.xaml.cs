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
using FreelancerHR.Modules.Hiring.ViewModel;
using FreelancerHR.DTO;
using Microsoft.Practices.Prism.Regions;

namespace FreelancerHR.Modules.Hiring.Views
{
    /// <summary>
    /// Interaction logic for FreelancerOfferView.xaml
    /// </summary>
    [Export]
    public partial class FreelancerOfferView : UserControl
    {
        public FreelancerOfferView()
        {
            InitializeComponent();

            //RegionContext.GetObservableContext(this).PropertyChanged += (s, e)
            //                                    =>
            //                                    ViewModel.CurrentHiringOffer =
            //                                    RegionContext.GetObservableContext(this).Value
            //                                    as HiringOfferDTO;
        }

         
        //[Import]
        //FreelancerOfferViewModel ViewModel
        //{
        //    set
        //    {
        //        this.DataContext = value;
        //    }
        //    get { return this.DataContext as FreelancerOfferViewModel; }
        //}
        
    }
}
