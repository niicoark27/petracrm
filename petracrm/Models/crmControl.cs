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

using System.Data;
using System.Threading;
using System.Globalization;

namespace petracrm.Models
{
    class crmControl : petraCRMDBDataContext
    {

        #region CRM Control Functions
        public   IEnumerable<crmSLAView> get_SLAs_View()
        {
            return (from sla in sla_timers                   
                    select new crmSLAView() {code = sla.code, Id = sla.ID, Name = sla.sla_name, Pre_escalate = sla.pre_escalate, Escalated = sla.escalate});

        }

        public crmSLAView get_SLAs_View(int id)
        {
            return (from sla in sla_timers
                    where sla.ID == id
                    select new crmSLAView() { code = sla.code,  Id = sla.ID, Name = sla.sla_name, Pre_escalate = sla.pre_escalate, Escalated = sla.escalate }).Single<crmSLAView>();;
        }

        public crmSLAView get_SLAs_By_Name_View(string slaName)
        {
            return (from sla in sla_timers
                    where sla.sla_name == slaName
                    select new crmSLAView() { code = sla.code, Id = sla.ID, Name = sla.sla_name, Pre_escalate = sla.pre_escalate, Escalated = sla.escalate }).Single<crmSLAView>(); ;
        }

        public IEnumerable<crmCorrespondenceView> get_Correspondence()
        {
            return (from corres in correspondences
                    join cats in categories on corres.category_id equals cats.id
                    select new crmCorrespondenceView() { code = corres.code, Id = corres.id, Name = corres.correspondence_name, description = corres.description, category = cats.category_name });

        }

        public IEnumerable<crmCorrespondenceView> get_Correspondence_Filter_By_Category(int cat_id)
        {
            return (from corres in correspondences
                    join cats in categories on corres.category_id equals cats.id
                    where corres.category_id == cat_id
                    select new crmCorrespondenceView() { code = corres.code, Id = corres.id, Name = corres.correspondence_name, description = corres.description, category = cats.category_name });
        }

        public crmCorrespondenceView get_Correspondence(int id)
        {
            return (from corres in correspondences
                    join cats in categories on corres.category_id equals cats.id
                    where corres.id == id
                    select new crmCorrespondenceView() { code = corres.code, Id = corres.id, Name = corres.correspondence_name, description = corres.description, category = cats.category_name }).Single<crmCorrespondenceView>();
        }

        public IEnumerable<crmSubCorrespondenceView> get_Sub_Correspondence()
        {
            return (from subCorres in sub_correspondences
                    join corres in correspondences on subCorres.correspondence_id equals corres.id
                    from sla in sla_timers
                    where subCorres.sla_id == sla.ID
                    select new crmSubCorrespondenceView() {code = subCorres.code, Id = subCorres.id, Name = subCorres.sub_correspondence_name, description = subCorres.description, correspondence = corres.correspondence_name, SLA = sla.sla_name });

        }

        public crmSubCorrespondenceView get_Sub_Correspondence(int id)
        {
            return (from subCorres in sub_correspondences
                    join corres in correspondences on subCorres.correspondence_id equals corres.id
                    from sla in sla_timers
                    where subCorres.sla_id == sla.ID && subCorres.id == id
                    select new crmSubCorrespondenceView() { code = subCorres.code, Id = subCorres.id, Name = subCorres.sub_correspondence_name, description = subCorres.description, correspondence = corres.correspondence_name, SLA = sla.sla_name }).Single<crmSubCorrespondenceView>();
        }

        public IEnumerable<crmSubCorrespondenceView> get_Sub_Correspondence_Filter_By_Correspondence(int corres_id)
        {
            return (from subCorres in sub_correspondences
                    join corres in correspondences on subCorres.correspondence_id equals corres.id
                    where  subCorres.correspondence_id == corres_id
                    select new crmSubCorrespondenceView() {code = subCorres.code, Id = subCorres.id, Name = subCorres.sub_correspondence_name, description = subCorres.description, correspondence = corres.correspondence_name, SLA = "" });

        }

        public IEnumerable<crmCategoryView> get_Categories()
        {
            return (from cat in categories                
                    select new crmCategoryView() { code = cat.code,  Id = cat.id, Name = cat.category_name, description = cat.description });

        }

        public crmCategoryView get_Categories(int id)
        {
            return (from cat in categories                  
                    where cat.id == id
                    select new crmCategoryView(){ code = cat.code, Id = cat.id, Name = cat.category_name, description = cat.description }).Single<crmCategoryView>();
        }

        #endregion

        public string get_ticket_seqence_no(int cat_id, int corres_id, int sub_corres_id, int month, int year)
        {
            return (from ini in tickets
                    where 
                          ini.ticket_month == month &&
                          ini.ticket_year == year
                    select ini).Count().ToString("00000");
       
        }

        public IEnumerable<crmTicketsView> get_acitve_tickets()
        {
            return (from tic in tickets
                    join cat in categories on tic.category_id equals cat.id
                    join corress in correspondences on tic.correspondence_id equals corress.id
                    join sub_corress in sub_correspondences on corress.id equals sub_corress.correspondence_id
                    join tic_status in ticket_statuses on tic.status equals tic_status.id
                    join sla in sla_timers on sub_corress.sla_id equals sla.ID
                    select new crmTicketsView() { ticket_id = tic.ticket_id, category = cat.category_name, correspondence = corress.correspondence_name, subject = tic.subject, status  = tic_status.status_desc, subcorrespondence = sub_corress.sub_correspondence_name, created_at = tic.created_at.ToString(), owner = "John Doe", escation_due = sla.escalate});
   
        }

        public crmTicketDetails get_ticket_details(string ticket_id)
        {
            return (from tic in tickets
                    join sub_corress in sub_correspondences on tic.sub_correspondence_id equals sub_corress.id
                    join sla in sla_timers on sub_corress.sla_id equals sla.ID
                    where tic.ticket_id == ticket_id
                    select new crmTicketDetails() { ticket_id = tic.ticket_id, petra_id = tic.customer_id, description = sub_corress.sub_correspondence_name, esacalation = sla.escalate, ticket_date = tic.created_at.ToString(), subject = tic.subject, customer_type = tic.customer_id_type }
                   ).Single<crmTicketDetails>();
        }

        public IEnumerable<crmTicketsView> get_customer_acitve_tickets(string petra_id)
        {
            return (from tic in tickets
                    join cat in categories on tic.category_id equals cat.id
                    join corress in correspondences on tic.correspondence_id equals corress.id
                    join sub_corress in sub_correspondences on corress.id equals sub_corress.correspondence_id
                    join tic_status in ticket_statuses on tic.status equals tic_status.id
                    join sla in sla_timers on sub_corress.sla_id equals sla.ID
                    where tic.customer_id == petra_id
                    select new crmTicketsView() { ticket_id = tic.ticket_id, category = cat.category_name, correspondence = corress.correspondence_name, subject = tic.subject, status = tic_status.status_desc, subcorrespondence = sub_corress.sub_correspondence_name, created_at = tic.created_at.ToString(), owner = "John Doe", escation_due = sla.escalate });

        }

        public IEnumerable<crmTicketComments> get_ticket_comments(string ticket_id)
        {
            return (from tic in tickets
                   join comm in ticket_comments on tic.ticket_id equals comm.ticket_id
                   where comm.ticket_id == ticket_id
                   select new crmTicketComments() { comment_date = comm.comment_date.ToString(), comment = comm.comment, owner = "John Doe" });

        }


    }
}
