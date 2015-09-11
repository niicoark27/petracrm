using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petracrm.Models
{
    class crmViews
    {
    }

    #region CRM Class Views

    class crmSLAView
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string Name { get; set; }
        public int Pre_escalate { get; set; }
        public int Escalated { get; set; }
        
    }

    class crmCorrespondenceView
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string Name { get; set; }
        public string category{ get; set; }
        public String description { get; set; }
    }

    class crmSubCorrespondenceView
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string Name { get; set; }       
        public String correspondence { get; set; }
        public String SLA { get; set; }
        public String description { get; set; }

    }

    class crmCategoryView
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string Name { get; set; }
        public String description { get; set; }

    }

    class crmTicketCounter
    {
        public int ini_serial { get; set; }
    }

    class crmTicketsView
    {
        public string ticket_id { get; set; }
        public string subject { get; set; }
        public string category { get; set; }
        public string correspondence { get; set; }
        public string subcorrespondence { get; set; }
        public string owner { get; set; }
        public string created_at { get; set; }
        public int escation_due { get; set; }
        public string status { get; set; }
    }

    class crmTicketDetails
    {
        public string ticket_id { get; set; }
        public string petra_id { get; set; }
        public string description { get; set; }
        public string ticket_date { get; set; }
        public int esacalation { get; set; }
        public string subject { get; set; }
        public int customer_type { get; set; }

    }

    class crmTicketComments
    {
        public string comment_date { get; set; }
        public string comment { get; set; }
        public string owner { get; set; }
    }

    #endregion

    #region Microgen Class Views

    class crmCustomerFullDetails
    {
        public int Entity_ID { get; set; }
        public string Petra_ID { get; set; }
        public string First_Name { get; set; }
        public string Second_Name { get; set; }
        public string Last_Name { get; set; }
        public string SSNIT_No { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Employer { get; set; }

    }
    class crmCustomerDetails
    {
        public string Petra_ID { get; set; }
        public string Customer_Name { get; set; }
        public string SSNIT_No { get; set; }
        public string Employer { get; set; }

    }
    class crmCustomerEmpoyerDetails
    {   
        public string Employer_Name { get; set; }
    }
    class crmCustomerContactDetails
    {
        public string phone {set; get;}
        public string email {set; get;}
    }
    class crmCompanyDetails
    {
        public int id { set; get; }
        public string petra_id { set; get; }
        public string company_name { set; get; }
        public string bus_reg_num {set; get;}
        public string contact_person { set; get; }
        public string mobile_no { set; get; }
        public string telephone_no { set; get; }
        public string email { set; get; }

    }
    class crmCompanyList
    {
        public string petra_id { set; get; }
        public string company_name { set; get; }
        
    }
   

    #endregion

}
