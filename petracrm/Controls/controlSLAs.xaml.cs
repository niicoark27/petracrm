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

namespace petracrm.Controls
{
    /// <summary>
    /// Interaction logic for controlSLAs.xaml
    /// </summary>
    public partial class controlSLAs : Page
    {
        public controlSLAs()
        {
            InitializeComponent();
        }

        private void btnNewSLA_Click(object sender, RoutedEventArgs e)
        {
            Pages.pageSLAs newSLA = new Pages.pageSLAs();
            newSLA.ShowDialog();
            load_data();
        }

        private void load_data()
        { 
            Models.crmControl control = new Models.crmControl();
            dgSLAs.ItemsSource = control.get_SLAs_View();
        }

        private void frmSLAs_Loaded(object sender, RoutedEventArgs e)
        {
            load_data();
        }
    }
}
