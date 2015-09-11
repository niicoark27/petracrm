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
using petracrm.Pages;
using petracrm.Models;

namespace petracrm.Controls
{
    /// <summary>
    /// Interaction logic for controlOperationsAdmin.xaml
    /// </summary>
    public partial class controlOperationsAdmin : Page
    {
        public controlOperationsAdmin()
        {
            InitializeComponent();
        }

        private void btnNewCategory_Click(object sender, RoutedEventArgs e)
        {
            pageCategories newCategory = new pageCategories();
            newCategory.ShowDialog();
        }

        private void btnNewCorrespondence_Click(object sender, RoutedEventArgs e)
        {
            pageCorrespondence newCorrespondence = new pageCorrespondence();
            newCorrespondence.ShowDialog();
        }

        private void btnNewSubCorresponence_Click(object sender, RoutedEventArgs e)
        {
            pageSubCorresdonce newSubCorrespondence = new pageSubCorresdonce();
            newSubCorrespondence.ShowDialog();
        }

        private void btnNewSLA_Click(object sender, RoutedEventArgs e)
        {
            pageSLAs newSLA = new pageSLAs();
            newSLA.ShowDialog();
        }


    }
}
