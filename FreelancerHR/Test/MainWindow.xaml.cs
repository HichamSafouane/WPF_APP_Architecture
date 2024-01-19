using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (FreelancerHiringEntities DBContext = new FreelancerHiringEntities())
            {
                OpenFileDialog dlg = new OpenFileDialog();

                dlg.Title = "Open Image";
                dlg.FileName = ""; // Default file name
                dlg.DefaultExt = ".jpg"; // Default file extension
                dlg.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    MemoryStream ms = new MemoryStream();
                    FileStream stream = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                    ms.SetLength(stream.Length);
                    stream.Read(ms.GetBuffer(), 0, (int)stream.Length);

                    ms.Flush();
                    byte[] b = ms.GetBuffer();

                    DBContext.Freelancer.First().Photo = b;
                    DBContext.SaveChanges();
                 //   Empl.Photo = b;
                    stream.Close();
                }
            }
        }
    }


}
