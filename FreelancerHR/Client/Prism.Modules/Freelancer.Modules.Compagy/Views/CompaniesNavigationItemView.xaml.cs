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
using Microsoft.Practices.Prism.Regions;
using FreelancerHR.Modules.Compagy.ViewModel;

namespace FreelancerHR.Modules.Compagy.Views
{
    /// <summary>
    /// Interaction logic for CompaniesNavigationItemView.xaml
    /// </summary>
    [Export]
    [ViewSortHint("02")]
    public partial class CompaniesNavigationItemView : UserControl
    {
        public CompaniesNavigationItemView()
        {
            InitializeComponent();
        }

        [Import]
        public CompaniesNavigationItemViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
