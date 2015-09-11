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
using System.Windows.Shapes;
using petracrm.Models;

namespace petracrm.Pages
{
    /// <summary>
    /// Interaction logic for pageTicketThread.xaml
    /// </summary>
    public partial class pageTicketThread : Window
    {
        public pageTicketThread(string ticketID)
        {
            InitializeComponent();
            load_ticket_details(ticketID);
        }

        crmControl crm_control = new crmControl();
        microgenControl microgen_control = new microgenControl();
        petraCRMDBDataContext database = new petraCRMDBDataContext();

        private void load_ticket_comments()
        {
            var comms = crm_control.get_ticket_comments(txtTicketID.Text);
            foreach(var comm in comms)
            {
                txtHistory.Text += "Date: " + comm.comment_date + "   Comment By: " + comm.owner + "\n " + comm.comment + "\n\n";
            }
           
        }

        private void load_ticket_details(string ticket_id)
        {
            var ticket_data = crm_control.get_ticket_details(ticket_id);
            txtTicketID.Text = ticket_data.ticket_id;
            txtSubject.Text = ticket_data.subject;
            txtDate.Text = ticket_data.ticket_date;
            txtDescription.Text = ticket_data.description;
            if(ticket_data.customer_type == 0)
            {
                var microgen_data = microgen_control.search_cust_by_petra_id(ticket_data.petra_id);
                txtCustomerName.Text = microgen_data.Customer_Name;
            }
            else if (ticket_data.customer_type == 1)
            {
                var microgen_data = microgen_control.get_company_by_petra_id(ticket_data.petra_id);
                txtCustomerName.Text = microgen_data.company_name;
            }
            load_ticket_comments();

        }

        private bool validate()
        {
            if (txtComment.Text == string.Empty) { MessageBox.Show("Please add your comment.", "comment Required", MessageBoxButton.OK, MessageBoxImage.Exclamation); txtComment.Focus(); return false; }
            else { return true; }
        }

        private bool update_ticket(int status)
        {
            try
            {

                var tic = from t in database.tickets
                          where t.ticket_id == txtTicketID.Text
                          select t;

                foreach (ticket t in tic)
                {
                    t.status = status;
                    t.updated_at = DateTime.Now;
                    t.modified_by = 0;
                    database.SubmitChanges();
                }
                return true;
            }
            catch (Exception updateError)
            {
                MessageBox.Show(updateError.Message);
                return false;
            }
        }

        private void post_comment(int status)
        {
            try
            {
                if(validate())
                {
                    if (update_ticket(status))
                    {
                        ticket_comment newComment = new ticket_comment();
                        newComment.ticket_id = txtTicketID.Text;
                        newComment.comment_date = DateTime.Now;
                        newComment.status = "1".ToString();
                        newComment.owner = 0;
                        newComment.comment = txtComment.Text;
                        database.ticket_comments.InsertOnSubmit(newComment);
                        database.SubmitChanges();
                        MessageBox.Show("Your comment has been posted.");
                        this.Close();
                    }
                    else
                    { MessageBox.Show("Your comment was not posted."); }
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }


        }

        private void btnPostComment_Click(object sender, RoutedEventArgs e)
        {
            post_comment(1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnResolved_Click(object sender, RoutedEventArgs e)
        {
            post_comment(4);
        }

        private void btnOnHold_Click(object sender, RoutedEventArgs e)
        {
            post_comment(2);
        }

    }
}
