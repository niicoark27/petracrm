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
    /// Interaction logic for controlCategories.xaml
    /// </summary>
    public partial class controlCategories : Page
    {
        public controlCategories()
        {
            InitializeComponent();
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            Pages.pageCategories openCategory = new Pages.pageCategories();
            openCategory.ShowDialog();
        }
    }
}
