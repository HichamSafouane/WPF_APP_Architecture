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
    /// Interaction logic for HiringRequestNavigationItemView.xaml
    /// </summary>
    [Export]
    [ViewSortHint("03")]
    public partial class HiringRequestNavigationItemView : UserControl
    {
        public HiringRequestNavigationItemView()
        {
            InitializeComponent();
        }

        [Import]
        HiringRequestsNavigationItemViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
