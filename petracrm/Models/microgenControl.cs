using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petracrm.Models
{
    class microgenControl : petraMicrogenDataContext
    {

        #region Microgen Control Functions

        public  IEnumerable<crmCustomerDetails> search_customer_by_petra_id(string p_id)
        {
            return (from ep in EntityPersons
                    join et in Entities on ep.EntityID equals et.EntityID
                    where et.EntityKey.Contains(p_id)
                    select new crmCustomerDetails() { Petra_ID = et.EntityKey, Customer_Name = (ep.FirstName+" "+ep.SecondNames+" "+ep.Surname), SSNIT_No = ep.NationalInsuranceNo });
                    
        }

        public crmCustomerDetails search_cust_by_petra_id(string p_id)
        {
            return (from ep in EntityPersons
                    join et in Entities on ep.EntityID equals et.EntityID
                    where et.EntityKey == p_id
                    select new crmCustomerDetails() { Petra_ID = et.EntityKey, Customer_Name = (ep.FirstName + " " + ep.SecondNames + " " + ep.Surname), SSNIT_No = ep.NationalInsuranceNo }).Single<crmCustomerDetails>();

        }

        public IEnumerable<crmCustomerDetails> search_customer_by_name(string customer_name)
        {
            return (from er in EntityRoles
                    join ep in EntityPersons on er.EntityID equals ep.EntityID
                    join et in Entities on ep.EntityID equals et.EntityID
                    join emp in Associations on er.EntityID equals emp.TargetEntityID
                    join ec in EntityClients on emp.SourceEntityID equals ec.EntityID
                    where er.RoleTypeID == 3 && et.StatusID == 51004 && (new[] { "1001", "1004" }).Contains(emp.RoleTypeID.ToString()) 
                    && (ep.FirstName+ep.SecondNames+ep.Surname).Contains(customer_name)
                    select new crmCustomerDetails() { Petra_ID = et.EntityKey, Customer_Name = (ep.FirstName + " " + ep.SecondNames + " " + ep.Surname), SSNIT_No = ep.NationalInsuranceNo, Employer = ec.FullName });

        }

        public IEnumerable<crmCustomerDetails> search_customer_by_ssnit_no(string ssnit_no)
        {
            return (from ep in EntityPersons
                    join et in Entities on ep.EntityID equals et.EntityID
                    where ep.NationalInsuranceNo == ssnit_no
                    select new crmCustomerDetails() { Petra_ID = et.EntityKey, Customer_Name = (ep.FirstName + " " + ep.SecondNames + " " + ep.Surname), SSNIT_No = ep.NationalInsuranceNo });

        }

        public IEnumerable<crmCustomerEmpoyerDetails> get_customer_employers(int entity_ID)
        {
            return (from emp in Associations
                    join ec in EntityClients on emp.SourceEntityID equals ec.EntityID
                    where (new[] { "1001", "1004"}).Contains(emp.RoleTypeID.ToString()) && emp.TargetEntityID == entity_ID
                    select new crmCustomerEmpoyerDetails() { Employer_Name = ec.FullName });
        }

        public crmCustomerFullDetails get_customer_by_petra_id(string p_id)
        {


            return (from er in EntityRoles
                    join ep in EntityPersons on er.EntityID equals ep.EntityID
                    join et in Entities on ep.EntityID equals et.EntityID               
                    join emp in Associations on er.EntityID equals emp.TargetEntityID
                    join ec in EntityClients on emp.SourceEntityID equals ec.EntityID
                    where er.RoleTypeID == 3 && et.StatusID == 51004 && (new[] { "1001", "1004" }).Contains(emp.RoleTypeID.ToString())
                    && et.EntityKey == p_id              
                    select new crmCustomerFullDetails() { Entity_ID = ep.EntityID, Petra_ID = et.EntityKey, First_Name = ep.FirstName, Second_Name = ep.SecondNames, Last_Name = ep.Surname , SSNIT_No = ep.NationalInsuranceNo, Email = "econs.Email", Telephone = "econs.MobileNo", Employer = "Unknown" }).Single<crmCustomerFullDetails>();

        }

        public crmCustomerContactDetails get_customer_contact_by_id(int entity_id)
        {
            return (from ec in EntityContacts
                    where ec.EntityID == entity_id
                    select new crmCustomerContactDetails() { email = ec.Email, phone = ec.MobileNo }).Single<crmCustomerContactDetails>();
                   
        }

        public IEnumerable<crmCompanyList> search_companies_by_name(string company_name)
        {
            return (from ec in EntityClients
                    join et in Entities on ec.EntityID equals et.EntityID
                    join er in EntityRoles on et.EntityID equals er.EntityID
                    where et.StatusID == 51004 && et.EntityTypeID == 2 && er.RoleTypeID == 1001 && ec.FullName.Contains(company_name)
                    select new crmCompanyList() { petra_id = et.EntityKey, company_name = ec.FullName});

        }

        public crmCompanyDetails get_company_by_petra_id(string petra_id)
        {
            return (from ec in EntityClients 
                    join et in Entities on ec.EntityID equals et.EntityID
                    join er in EntityRoles on et.EntityID equals er.EntityID              
                    where et.StatusID == 51004 &&  et.EntityTypeID == 2 && er.RoleTypeID == 1001 && et.EntityKey == petra_id
                    select new crmCompanyDetails() { petra_id = et.EntityKey, id = et.EntityID, company_name = ec.FullName, bus_reg_num = ec.RegisteredCompanyNum, contact_person = "", email = "", mobile_no = "", telephone_no = "" }).Single<crmCompanyDetails>();

        }

        #endregion

    }
}
