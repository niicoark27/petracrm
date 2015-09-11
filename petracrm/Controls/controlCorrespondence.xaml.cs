using System;
using System.Collections.Generic;
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
using petracrm.Models;

namespace petracrm.Controls
{
    /// <summary>
    /// Interaction logic for controlCorrespondence.xaml
    /// </summary>
    public partial class controlCorrespondence : Page
    {
        public controlCorrespondence()
        {
            InitializeComponent();
        }

        Models.crmControl control = new Models.crmControl();
        petraCRMDBDataContext database = new petraCRMDBDataContext();

        private void btnNewCorrespondence_Click(object sender, RoutedEventArgs e)
        {
            Pages.pageCorrespondence newCorrespondence = new Pages.pageCorrespondence();
            newCorrespondence.ShowDialog();
            load_correspondence();
        }

        private void load_correspondence()
        {
            dgCorrrespondence.ItemsSource = control.get_Correspondence();
        }

    }
}
