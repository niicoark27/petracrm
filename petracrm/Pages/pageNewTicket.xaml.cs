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

namespace petracrm.Pages
{
    /// <summary>
    /// Interaction logic for pageNewTicket.xaml
    /// </summary>
    public partial class pageNewTicket : Window
    {
        public pageNewTicket()
        {
            InitializeComponent();
        }

        petraCRMDBDataContext crm_database = new petraCRMDBDataContext();
        petraMicrogenDataContext microgen_database = new petraMicrogenDataContext();

        crmControl crm_control = new crmControl();
        microgenControl microgen_control = new microgenControl();
        private bool isCompany = false;


        private void search_for_customer()
        {
            if (txtSearch.Text != string.Empty)
            {
                if (rdPetraID.IsChecked == true) { dgFoundRecords.ItemsSource = microgen_control.search_customer_by_petra_id(txtSearch.Text); isCompany = false; }
                else if (rdCustomerName.IsChecked == true) { dgFoundRecords.ItemsSource = microgen_control.search_customer_by_name(txtSearch.Text); isCompany = false; }
                else if (rdSSNITNo.IsChecked == true) { dgFoundRecords.ItemsSource = microgen_control.search_customer_by_ssnit_no(txtSearch.Text); isCompany = false; }
                else if (rdCompanyName.IsChecked == true) { dgFoundRecords.ItemsSource = microgen_control.search_companies_by_name(txtSearch.Text); isCompany = true; }

                tbSearchResults.IsSelected = true;
                lblRecordsFound.Content = dgFoundRecords.Items.Count.ToString()+" Records Found.";
            }
            else
            {
                MessageBox.Show("Please specify a search value.");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            search_for_customer();
        }

        private void get_company_details(string petra_id)
        {
            var comp_info = microgen_control.get_company_by_petra_id(petra_id);
            try 
            {
                txtCompanyPetraID.Text = comp_info.petra_id;
                txtPetraID.Text = comp_info.petra_id;
                txtCompanyName.Text = comp_info.company_name;
                txtCustomerName.Text = comp_info.company_name;
                txtCompanyRegNo.Text = comp_info.bus_reg_num;
                

            
            }
            catch(Exception)
            {
                MessageBox.Show("Unable to load company details.");
            }
        }

        private void get_customer_details(string petra_id)
        {
            var cust_info = microgen_control.get_customer_by_petra_id(petra_id);
            var cust_contact = microgen_control.get_customer_contact_by_id(cust_info.Entity_ID);
            var cust_employers = microgen_control.get_customer_employers(cust_info.Entity_ID);

            try
            {
                
                txtPetraID.Text = cust_info.Petra_ID;
                txtCustomerPetraID.Text = cust_info.Petra_ID;
                txtSSN.Text = cust_info.SSNIT_No;
                txtFirstName.Text = cust_info.First_Name;
                txtMiddleNames.Text = cust_info.Second_Name;
                txtSurname.Text = cust_info.Last_Name;
                txtCustomerName.Text = cust_info.First_Name+" "+cust_info.Second_Name+" "+cust_info.Last_Name;

             }
             catch (Exception)
             {
               MessageBox.Show("Unable to load customer details.");
             }

            try
            {
                txtEmail.Text = cust_contact.email;
                txtContactNo.Text = cust_contact.phone;
            }
            catch(Exception)
            {
                MessageBox.Show("No contact details found.");
            }

            try
            { 
                cmbEmployers.Items.Clear();

                foreach (var emp in cust_employers)
                {
                    cmbEmployers.Items.Add(new KeyValuePair<string, string>(emp.Employer_Name, emp.Employer_Name));
                }
                if (cmbEmployers.Items.Count > 0) { cmbEmployers.SelectedIndex = 0; }
                
            
            }
            catch(Exception)
            {
                MessageBox.Show("Employer details not found.");
            }
 
            
        }

        private void dgFoundRecords_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try 
            {
                if (!isCompany) { crmCustomerDetails cust = (crmCustomerDetails)dgFoundRecords.SelectedItem; get_customer_details(cust.Petra_ID); tbCustomertInfo.IsSelected = true; }
                else { crmCompanyList comp = (crmCompanyList)dgFoundRecords.SelectedItem; get_company_details(comp.petra_id); tbCompanyInfo.IsSelected = true; }    
            }
            catch(Exception)
            {
                //
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            tbSeach.IsSelected = true;
        }

        private void btnNewTicket_Click(object sender, RoutedEventArgs e)
        {
            tbTicketInfo.IsSelected = true;
        }

        private void btnBackResults_Click(object sender, RoutedEventArgs e)
        {
            tbSearchResults.IsSelected = true;
        }

        private void load_categories()
        {
            cmbTicketCategory.Items.Clear();
            var cats = crm_control.get_Categories();

            foreach (var inicat in cats)
            {
                cmbTicketCategory.Items.Add(new KeyValuePair<string, int>(inicat.Name, inicat.Id));
            }
        }

        private void load_correspondences(int cat_id)
        {
            cmbTicketCorrespondence.Items.Clear();
            var corres = crm_control.get_Correspondence_Filter_By_Category(cat_id);

            foreach (var inicorres in corres)
            {
                cmbTicketCorrespondence.Items.Add(new KeyValuePair<string, int>(inicorres.Name, inicorres.Id));
            }

        }

        private void load_sub_correspondences(int corres_id)
        {
            cmbTicketSubCorrespondence.Items.Clear();
            var sub_corres = crm_control.get_Sub_Correspondence_Filter_By_Correspondence(corres_id);

            foreach (var inicorres in sub_corres)
            {
                cmbTicketSubCorrespondence.Items.Add(new KeyValuePair<string, int>(inicorres.Name, inicorres.Id));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load_categories();
        }

        private void cmbTicketCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                if (cmbTicketCategory.SelectedIndex >= 0)
                {
                    load_correspondences(int.Parse(cmbTicketCategory.SelectedValue.ToString()));
                }
                else
                { 
                    cmbTicketCorrespondence.Items.Clear();
                }
            }
            catch(Exception)
            {
                cmbTicketCorrespondence.Items.Clear();
            }
        }

        private void cmbTicketSubCorrespondence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbTicketSubCorrespondence.SelectedIndex >= 0)
                {
                    var sub_corres = crm_control.get_Sub_Correspondence(int.Parse(cmbTicketSubCorrespondence.SelectedValue.ToString()));
                    var sla = crm_control.get_SLAs_By_Name_View(sub_corres.SLA);
                    txtAssocaitedSLA.Text = sla.Name + " TIMERS [Pre Escalate : " + sla.code + "] [Escalate : " + sla.Escalated + "]";
                }
                else
                { 
                    txtAssocaitedSLA.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                txtAssocaitedSLA.Text = string.Empty;
            }
        }

        private void cmbTicketCorrespondence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbTicketCorrespondence.SelectedIndex >= 0)
                {
                    load_sub_correspondences(int.Parse(cmbTicketCorrespondence.SelectedValue.ToString()));
                }
                else
                { 
                    cmbTicketSubCorrespondence.Items.Clear();
                }
            }
            catch (Exception)
            {
                cmbTicketSubCorrespondence.Items.Clear();
            }
        }

        private bool validate()
        {
            try
            {
                if (txtPetraID.Text == string.Empty) { MessageBox.Show("No Petra ID found for this ticket. Ticket can not be created without an ID.", "No Petra ID", MessageBoxButton.OK, MessageBoxImage.Exclamation); return false; }
                else if (txtPetraID.Text == string.Empty) { MessageBox.Show("No Petra ID found for this ticket. Ticket can not be created without an ID.", "No Petra ID", MessageBoxButton.OK, MessageBoxImage.Exclamation); return false; }
                else if (txtTicketSubject.Text == string.Empty) { MessageBox.Show("No subject found for this ticket. Ticket can not be created without a subject.", "No Subject", MessageBoxButton.OK, MessageBoxImage.Exclamation); return false; }          
                else if (cmbTicketCategory.SelectedIndex < 0) { MessageBox.Show("Please select a category for this ticket. Ticket can not be created without a category.", "No Category", MessageBoxButton.OK, MessageBoxImage.Exclamation); return false; }
                else if (cmbTicketCorrespondence.SelectedIndex  < 0) { MessageBox.Show("Please select a correspondence for this ticket. Ticket can not be created without a correspondence.", "No Correspondence", MessageBoxButton.OK, MessageBoxImage.Exclamation); return false; }
                else if (cmbTicketSubCorrespondence.SelectedIndex < 0) { MessageBox.Show("Please select a [sub correspondence for this ticket. Ticket can not be created without a sub correspondence.", "No Correspondence", MessageBoxButton.OK, MessageBoxImage.Exclamation); return false; }           
                else { return true; }
            }
            catch(Exception)
            {
                return false;
            }
        }

        private void btnCreateTicket_Click(object sender, RoutedEventArgs e)
        {
            if(validate())
            {
                get_ticket_code();
                String conf_msg = "The ticket will be created  with the following details;\n\n Ticket ID : "+txtTicketID.Text+" \n Subject : "+ txtTicketSubject.Text +"";
                if(MessageBox.Show(conf_msg,"Ticket Craeted",MessageBoxButton.OKCancel,MessageBoxImage.Information) == MessageBoxResult.OK){ if(save_ticket()){this.Close();} }

            }
        }

        private int get_priority()
        { 
            if(rdPriorityLow.IsChecked == true){return 0;}
            else if (rdPriorityMedium.IsChecked == true) { return 1; }
            else if (rdPriorityHigh.IsChecked == true) { return 2; }
            else { return -1;  };
        }

        private bool save_ticket()
        {
            try
            {
                petraCRMDBDataContext database = new petraCRMDBDataContext();
                ticket newTicket = new ticket();
                newTicket.ticket_id = txtTicketID.Text;
                newTicket.customer_id = txtPetraID.Text;
                if (isCompany) { newTicket.customer_id_type = 1; } else { newTicket.customer_id_type = 0; }
                newTicket.ticket_priority = get_priority();
                newTicket.subject = txtTicketSubject.Text;
                newTicket.category_id = int.Parse(cmbTicketCategory.SelectedValue.ToString());
                newTicket.correspondence_id = int.Parse(cmbTicketCorrespondence.SelectedValue.ToString());
                newTicket.sub_correspondence_id = int.Parse(cmbTicketSubCorrespondence.SelectedValue.ToString());
                newTicket.notes = txtNotes.Text;
                newTicket.ticket_month = DateTime.Now.Month;
                newTicket.ticket_year = DateTime.Now.Year;
                newTicket.status = 1;
                newTicket.owner = 0;
                newTicket.assigned_to = 1;
                newTicket.created_at = DateTime.Now;
                database.tickets.InsertOnSubmit(newTicket);
                database.SubmitChanges();
                return true;
            }
            catch (Exception newSlaError)
            {
                MessageBox.Show(newSlaError.Message);
                return false;
            }
        }

        private void get_ticket_code()
        {
            var cat_code = crm_control.get_Categories(int.Parse(cmbTicketCategory.SelectedValue.ToString()));
            var corres_code = crm_control.get_Correspondence(int.Parse(cmbTicketCorrespondence.SelectedValue.ToString()));
            var sub_corres_code = crm_control.get_Sub_Correspondence(int.Parse(cmbTicketSubCorrespondence.SelectedValue.ToString()));
           
            string sequence = crm_control.get_ticket_seqence_no(cat_code.Id, corres_code.Id, sub_corres_code.Id, DateTime.Now.Month, DateTime.Now.Year);
            txtTicketID.Text = cat_code.code+corres_code.code+sub_corres_code.code+DateTime.Now.Month.ToString()+DateTime.Now.Year.ToString()+sequence;

        }

        private void btnCancelTicket_Click(object sender, RoutedEventArgs e)
        {
            string msg = "Are you sure you want to cancel this ticket?";
            if (MessageBox.Show(msg) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }



    }
}
