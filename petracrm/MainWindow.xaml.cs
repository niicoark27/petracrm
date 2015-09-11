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

namespace petracrm
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

        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
            Controls.controlCategories page = new Controls.controlCategories();
            frame.Navigate(page);
        }

        private void btnSLA_Click(object sender, RoutedEventArgs e)
        {
            Controls.controlSLAs page = new Controls.controlSLAs();
            frame.Navigate(page);
        }

        private void btnCorrespondence_Click(object sender, RoutedEventArgs e)
        {
            Controls.controlCorrespondence page = new Controls.controlCorrespondence();
            frame.Navigate(page);
        }

        private void btnTickets_Click(object sender, RoutedEventArgs e)
        {
            Controls.controlTickets page = new Controls.controlTickets();
            frame.Navigate(page);
        }

        private void btnOperationsAdmin_Click(object sender, RoutedEventArgs e)
        {
            Controls.controlOperationsAdmin page = new Controls.controlOperationsAdmin();
            frame.Navigate(page);
        }



    }
}
