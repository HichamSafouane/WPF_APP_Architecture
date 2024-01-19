using FreelancerHR.Modules.Hiring.Controllers;
using FreelancerHR.Modules.Hiring.ViewModel;
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
    /// Interaction logic for HiringMainView.xaml
    /// </summary>
    [Export]
    public partial class HiringMainView : UserControl
    {
        public HiringMainView()
        {
            InitializeComponent();
        }

        [Import]
        private IHiringMainController hiringMainController;

        [Import]
        HiringMainViewModel ViewModel
        {
            set { this.DataContext = value; }
        }
    }
}
