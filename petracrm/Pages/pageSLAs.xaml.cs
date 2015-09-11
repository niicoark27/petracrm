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
    /// Interaction logic for pageSLAs.xaml
    /// </summary>
    public partial class pageSLAs : Window
    {
        public pageSLAs()
        {
            InitializeComponent();
        }

        petraCRMDBDataContext database = new petraCRMDBDataContext();
        crmControl control = new crmControl();
        private bool isUpdate = false;
        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (validate_entries())
            {
                if (!isUpdate)
                {
                    if (create_new_sla()) { MessageBox.Show("New SLA Created Successfully.", "New Record Added", MessageBoxButton.OK, MessageBoxImage.Information); clear_entries(); load_slas(); }
                }
                else
                {
                    if (update_sla()) { MessageBox.Show("SLA Updated Successfully.", "Record Updated", MessageBoxButton.OK, MessageBoxImage.Information); clear_entries(); load_slas(); }
                }
            }
        }

        public  bool create_new_sla()
        {
            try
            {
                petraCRMDBDataContext database = new petraCRMDBDataContext();
                sla_timer newSLA = new sla_timer();
                newSLA.sla_name = txtSLAName.Text;
                newSLA.code = txtCode.Text;
                newSLA.pre_escalate = int.Parse(txtSLAPreEscalate.Text);
                newSLA.escalate = int.Parse(txtSLAEscalated.Text);
                newSLA.owner = 0;
                newSLA.created_at = DateTime.Now;
                database.sla_timers.InsertOnSubmit(newSLA);
                database.SubmitChanges();
                return true;
            }
            catch(Exception newSlaError)
            {
                MessageBox.Show(newSlaError.Message);
                return false;
            }

        }

        private bool update_sla()
        {
            try
            {

                var sal = from s in database.sla_timers
                          where s.ID == int.Parse(txtSLAId.Text)
                          select s;

                foreach (sla_timer s in sal)
                {
                    s.sla_name = txtSLAName.Text;
                    s.escalate = int.Parse(txtSLAEscalated.Text);
                    s.pre_escalate = int.Parse(txtSLAPreEscalate.Text);
                    s.code = txtCode.Text;
                    s.updated_at = DateTime.Now;
                    s.modified_by = 0;
                    database.SubmitChanges();
                }
                return true;
            }
            catch (Exception newSlaError)
            {
                MessageBox.Show(newSlaError.Message);
                return false;
            }
        }

        private void clear_entries()
        {
            try
            {
                isUpdate = false;
                txtSLAId.Text= "Auto";
                txtSLAName.Text = string.Empty;
                txtCode.Text = string.Empty;
                txtSLAPreEscalate.Text = string.Empty;
                txtSLAEscalated.Text = string.Empty;
            }
            catch (Exception) { MessageBox.Show("An error occured while setting up from."); }
        }

        private bool validate_entries()
        {
            try
            {
                if (txtSLAName.Text == string.Empty) { MessageBox.Show("Please specify the name of the SLA you want to create", "No SLA name", MessageBoxButton.OK, MessageBoxImage.Stop); txtSLAName.Focus(); return false; }
                else if (int.Parse(txtSLAPreEscalate.Text) < 0) { MessageBox.Show("Please specify SLA pre escalate timer in hours", "Invalid Pre Escalate Value", MessageBoxButton.OK, MessageBoxImage.Stop); txtSLAPreEscalate.Focus(); return false; }
                else if (int.Parse(txtSLAEscalated.Text) < 0) { MessageBox.Show("Please specify SLA escalate timer in hours", "Invalid Escalate Value", MessageBoxButton.OK, MessageBoxImage.Stop); txtSLAEscalated.Focus(); return false; }
                else if (txtCode.Text == string.Empty) { MessageBox.Show("Please specify a unique code for this SLA", "No SLA Code Specified", MessageBoxButton.OK, MessageBoxImage.Stop); txtSLAEscalated.Focus(); return false; }
             
                else { return true; }
            }
            catch (Exception) { return false; }
        }

        private void load_sla_details(int id)
        {
            try
            {
                var sla = control.get_SLAs_View(id);
                txtSLAId.Text = sla.Id.ToString();
                txtSLAName.Text = sla.Name;
                txtCode.Text = sla.code;
                txtSLAPreEscalate.Text = sla.Pre_escalate.ToString();
                txtSLAEscalated.Text = sla.Escalated.ToString();
                isUpdate = true;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while loading SLA details.");
            }
        }

        private void load_slas()
        {
            dgSLAs.ItemsSource = control.get_SLAs_View();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clear_entries();
            load_slas();

        }

        private void dgSLAs_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                crmSLAView sla = (crmSLAView)dgSLAs.SelectedItem;
                load_sla_details(sla.Id);
            }
            catch(Exception)
            {
                //
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit this form?", "Exit this form", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            { this.Close(); }
        }
    }

}
