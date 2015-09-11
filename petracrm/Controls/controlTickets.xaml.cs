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
    /// Interaction logic for controlTickets.xaml
    /// </summary>
    public partial class controlTickets : Page
    {
        public controlTickets()
        {
            InitializeComponent();
        }

        crmControl crm_control = new crmControl();
        private void load_tickets()
        {
            try
            {
                dgTickets.ItemsSource = crm_control.get_acitve_tickets();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnNewTicket_Click(object sender, RoutedEventArgs e)
        {
            Pages.pageNewTicket newTicket = new Pages.pageNewTicket();
            newTicket.ShowDialog();
            load_tickets();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            load_tickets();
        }

        private void dgTickets_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                crmTicketsView info = (crmTicketsView)dgTickets.SelectedItem;
                Pages.pageTicketThread openThread = new Pages.pageTicketThread(info.ticket_id);
                openThread.ShowDialog();
                load_tickets();

            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
