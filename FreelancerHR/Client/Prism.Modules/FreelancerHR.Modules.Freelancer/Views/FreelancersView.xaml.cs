using FreelancerHR.Modules.Freelancer.ViewModel;
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

namespace FreelancerHR.Modules.Freelancer.Views
{
    /// <summary>
    /// Interaction logic for FreelancersView.xaml
    /// </summary>
    [Export]
    public partial class FreelancersView : UserControl
    {
        public FreelancersView()
        {
            InitializeComponent();
        }

        [Import]
        public FreelancersViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
