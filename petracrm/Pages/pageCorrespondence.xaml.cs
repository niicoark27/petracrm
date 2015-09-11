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
    /// Interaction logic for pageCorrespondence.xaml
    /// </summary>
    public partial class pageCorrespondence : Window
    {
        public pageCorrespondence()
        {
            this.DataContext = this;
            InitializeComponent();
        }


        crmControl control = new crmControl();
        petraCRMDBDataContext database = new petraCRMDBDataContext();
        private bool isUpdate = false;


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(validate_entries())
            {
                if (!isUpdate)
                {
                    if (create_new_correspondence()) { MessageBox.Show("New Correspondence Created Successfully.","New Record Added",MessageBoxButton.OK,MessageBoxImage.Information); clear_entries(); load_correspondence(); }
                }
                else
                {
                    if (update_correspondence()) { MessageBox.Show("Correspondence Updated Successfully.","Record Updated",MessageBoxButton.OK,MessageBoxImage.Information); clear_entries(); load_correspondence(); }
                }
             }
        }

        private void clear_entries()
        {
            try 
            {
                isUpdate = false;
                txtID.Text = "Auto";
                txtName.Text = string.Empty;
                txtDescription.Text = string.Empty;
                cmbCategories.SelectedIndex = -1;
                txtCode.Text = string.Empty;
                load_categories();
            }
            catch (Exception) { MessageBox.Show("An error occured while setting up from."); }
        }

        private bool validate_entries()
        {
            try
            {
                if (txtName.Text == string.Empty) { MessageBox.Show("Please specify the name of the correspondence you want to create", "No correspondence name", MessageBoxButton.OK, MessageBoxImage.Stop); txtName.Focus(); return false; }
                else if (cmbCategories.SelectedIndex < 0) { MessageBox.Show("Please select the assocaited category of the correspondence you want to create", "No category selected", MessageBoxButton.OK, MessageBoxImage.Stop); return false; }
                else if (txtCode.Text == string.Empty) { MessageBox.Show("Please specify the code of the correspondence you want to create", "No correspondence code", MessageBoxButton.OK, MessageBoxImage.Stop); txtCode.Focus(); return false; }
                else { return true; }
            }
            catch(Exception){return false;}
        }

        private void load_correspondence_details(int id)
        {
            try
            {
                var corres = control.get_Correspondence(id);
                txtID.Text = corres.Id.ToString();
                txtCode.Text = corres.code;
                txtName.Text = corres.Name;
                txtDescription.Text = corres.description;
                isUpdate = true;
                cmbCategories.Text = corres.category;
            }
            catch(Exception)
            {
                MessageBox.Show("An error occured while loading correspondence details.");
            }

        }

        private void load_correspondence()
        {
            dgCorrespondence.ItemsSource = control.get_Correspondence();
        }

        private void load_categories()
        {
            cmbCategories.Items.Clear();
            var cats = control.get_Categories();

            foreach (var inicat in cats)
            {
                cmbCategories.Items.Add(new KeyValuePair<string, int>(inicat.Name, inicat.Id));  
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clear_entries();
            load_correspondence();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit this form?", "Exit this form", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            { this.Close(); }
        }

        private bool create_new_correspondence()
        {
            try
            {

                correspondence newCorres = new correspondence();
                newCorres.correspondence_name = txtName.Text;
                newCorres.description = txtDescription.Text;
                newCorres.category_id = int.Parse(cmbCategories.SelectedValue.ToString());
                newCorres.owner = 0;
                newCorres.code = txtCode.Text;
                newCorres.created_at = DateTime.Now;
                database.correspondences.InsertOnSubmit(newCorres);
                database.SubmitChanges();
                return true;
            }
            catch (Exception newSlaError)
            {
                MessageBox.Show(newSlaError.Message);
                return false;
            }

        }

        private bool update_correspondence()
        {
            try
            {

                var corres = from c in database.correspondences
                                   where c.id == int.Parse(txtID.Text)
                                   select c;

                foreach (correspondence c in corres)
                {
                    c.correspondence_name = txtName.Text;
                    c.category_id = int.Parse(cmbCategories.SelectedValue.ToString());
                    c.description = txtDescription.Text;
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

        private void dgCorrespondence_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                crmCorrespondenceView corress = (crmCorrespondenceView)dgCorrespondence.SelectedItem;
                load_correspondence_details(corress.Id);
            }
            catch(Exception)
            {
            
            }
        }

       
        



    }
}
