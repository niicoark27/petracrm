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
    /// Interaction logic for pageSubCorresdonce.xaml
    /// </summary>
    public partial class pageSubCorresdonce : Window
    {
        public pageSubCorresdonce()
        {
            InitializeComponent();
        }

        petraCRMDBDataContext database = new petraCRMDBDataContext();
        crmControl control = new crmControl();
        private bool isUpdate = false;

        private void clear_entries()
        {
            try
            {
                isUpdate = false;
                txtID.Text = "Auto";
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtDescription.Text = string.Empty;              
                cmbCorrespondence.SelectedIndex = -1;
                cmbSLA.SelectedIndex = -1;
                txtSlaOnHold.Text = string.Empty;
                txtSlaEscalate.Text = string.Empty;
                
            }
            catch (Exception err) { MessageBox.Show("An error occured while setting up from.\n"+err.Message); }
        }

        private bool validate_entries()
        {
            try
            {
                if (txtName.Text == string.Empty) { MessageBox.Show("Please specify the name of the correspondence you want to create", "No correspondence name", MessageBoxButton.OK, MessageBoxImage.Stop); txtName.Focus(); return false; }
                else if (cmbCorrespondence.SelectedIndex < 0) { MessageBox.Show("Please select the assocaited correspondence of the sub correspondence you want to create", "No correspondence selected", MessageBoxButton.OK, MessageBoxImage.Stop); return false; }
                else if (cmbSLA.SelectedIndex < 0) { MessageBox.Show("Please select the assocaited SLA of the sub correspondence you want to create", "No SLA selected", MessageBoxButton.OK, MessageBoxImage.Stop); return false; }
                else if (txtCode.Text == string.Empty) { MessageBox.Show("Please specify the code of the sub correspondence you want to create", "No sub correspondence code", MessageBoxButton.OK, MessageBoxImage.Stop); txtCode.Focus(); return false; }           
                else { return true; }
            }
            catch (Exception) { return false; }
        }

        private void load_sub_correspondence_details(int id)
        {
            try
            {
                var sub_corres = control.get_Sub_Correspondence(id);
                txtID.Text = sub_corres.Id.ToString();
                txtName.Text = sub_corres.Name;
                txtDescription.Text = sub_corres.description;
                cmbCorrespondence.Text = sub_corres.correspondence;
                cmbSLA.Text = sub_corres.SLA;
                isUpdate = true;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while loading sub correspondence details.");
            }

        }

        private void load_sub_cmbs()
        { 
            //Load Correspondence
            cmbCorrespondence.Items.Clear();
            var corres = control.get_Correspondence();
            foreach (var inic in corres){ cmbCorrespondence.Items.Add(new KeyValuePair<string, int>(inic.Name, inic.Id)); }

            //Load SLAs
            cmbSLA.Items.Clear();
            var slas = control.get_SLAs_View();
            foreach (var inis in slas) { cmbSLA.Items.Add(new KeyValuePair<string, int>(inis.Name, inis.Id)); }

        }

        private void load_sla_details(int id)
        {
            try
            {
                var sla = control.get_SLAs_View(id);
                txtSlaOnHold.Text = sla.Pre_escalate.ToString();
                txtSlaEscalate.Text = sla.Escalated.ToString();
                txtCode.Text = sla.code;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured while loading SLA timers details.");
            }
        }

        private void load_sub_correspondence()
        {
            dgSubCorrespondence.ItemsSource = control.get_Sub_Correspondence();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clear_entries();
            load_sub_cmbs();
            load_sub_correspondence();
        }

        private void cmbSLA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(cmbSLA.SelectedIndex >= 0)
                {
                    load_sla_details(int.Parse(cmbSLA.SelectedValue.ToString()));
                }
            }
            catch(Exception)
            {
                MessageBox.Show("An error occured while loading SLA timers.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit this form?", "Exit this form", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            { this.Close(); }
        }

        private bool create_new_sub_correspondence()
        {
            try
            {

                sub_correspondence newSubCorres = new sub_correspondence();
                newSubCorres.sub_correspondence_name = txtName.Text;
                newSubCorres.code = txtCode.Text;
                newSubCorres.description = txtDescription.Text;
                newSubCorres.correspondence_id = int.Parse(cmbCorrespondence.SelectedValue.ToString());
                newSubCorres.sla_id = int.Parse(cmbSLA.SelectedValue.ToString());
                newSubCorres.owner = 0;
                newSubCorres.created_at = DateTime.Now;
                database.sub_correspondences.InsertOnSubmit(newSubCorres);
                database.SubmitChanges();
                return true;
            }
            catch (Exception newSlaError)
            {
                MessageBox.Show(newSlaError.Message);
                return false;
            }

        }

        private bool update_sub_correspondence()
        {
            try
            {

                var sub_corres = from c in database.sub_correspondences
                             where c.id == int.Parse(txtID.Text)
                             select c;

                foreach (sub_correspondence c in sub_corres)
                {
                    c.sub_correspondence_name = txtName.Text;
                    c.description = txtDescription.Text;
                    c.correspondence_id = int.Parse(cmbCorrespondence.SelectedValue.ToString());
                    c.sla_id = int.Parse(cmbSLA.SelectedValue.ToString());
                    c.updated_at = DateTime.Now;
                    c.code = txtCode.Text;
                    c.modified_by = 0;
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

        private void dgSubCorrespondence_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                crmSubCorrespondenceView sub_corress = (crmSubCorrespondenceView)dgSubCorrespondence.SelectedItem;
                load_sub_correspondence_details(sub_corress.Id);
            }
            catch(Exception)
            {
               // 
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (validate_entries())
            {
                if (!isUpdate)
                {
                    if (create_new_sub_correspondence()) { MessageBox.Show("New Sub Correspondence Created Successfully.", "New Record Added", MessageBoxButton.OK, MessageBoxImage.Information); clear_entries(); load_sub_correspondence(); }
                }
                else
                {
                    if (update_sub_correspondence()) { MessageBox.Show("Sub Correspondence Updated Successfully.", "Record Updated", MessageBoxButton.OK, MessageBoxImage.Information); clear_entries(); load_sub_correspondence(); }
                }
            }
        }


    }
}
