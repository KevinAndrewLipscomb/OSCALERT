using System.Configuration;

using kix;

using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Class_biz_users;
namespace remind_username
{
    public struct p_type
    {
        public TClass_biz_users biz_users;
    } // end p_type

    public partial class TWebForm_remind_username: ki_web_ui.page_class
    {
        private p_type p;
        // / <summary>
        // / Required method for Designer support -- do not modify
        // / the contents of this method with the code editor.
        // / </summary>
        private void InitializeComponent()
        {
            //this.Load += this.Page_Load;
            this.PreRender += this.TWebForm_remind_username_PreRender;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            switch(NatureOfVisit(InstanceId() + ".p"))
            {
                case nature_of_visit_type.VISIT_INITIAL:
                    Title.InnerText = Server.HtmlEncode(ConfigurationManager.AppSettings["application_name"]) + " - remind_username";
                    p.biz_users = new TClass_biz_users();
                    Label_application_name_1.Text = ConfigurationManager.AppSettings["application_name"];
                    Label_application_name_2.Text = ConfigurationManager.AppSettings["application_name"];
                    Label_application_name_3.Text = ConfigurationManager.AppSettings["application_name"];
                    TextBox_email_address.Focus();
                    break;
                case nature_of_visit_type.VISIT_POSTBACK_STANDARD:
                    p = (p_type)(Session[InstanceId() + ".p"]);
                    break;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            // Required for Designer support
            InitializeComponent();
            base.OnInit(e);
        }

        protected void Button_cancel_Click(object sender, System.EventArgs e)
        {
            Server.Transfer("login.aspx");
        }

        protected void LinkButton_return_Click(object sender, System.EventArgs e)
        {
            Server.Transfer("login.aspx");
        }

        protected void Button_submit_Click(object sender, System.EventArgs e)
        {
            string email_address;
            email_address = k.Safe(TextBox_email_address.Text.Trim(), k.safe_hint_type.EMAIL_ADDRESS);
            if (p.biz_users.BeRegisteredEmailAddress(email_address))
            {
                p.biz_users.IssueUsernameReminder(email_address, k.Safe(Request.UserHostName, k.safe_hint_type.HOSTNAME));
                Alert(k.alert_cause_type.LOGIC, k.alert_state_type.NORMAL, "usrnamsnt", "The associated " + ConfigurationManager.AppSettings["application_name"] + " username has been sent to " + email_address + k.PERIOD, true);
                Table_return.Visible = true;
            }
            else
            {
                Alert(k.alert_cause_type.USER, k.alert_state_type.FAILURE, "nosucheml", "No such email address registered", true);
            }
        }

        protected void LinkButton_register_new_user_Click(object sender, System.EventArgs e)
        {
            Server.Transfer("new_user_registration.aspx");
        }

        private void TWebForm_remind_username_PreRender(object sender, System.EventArgs e)
        {
            SessionSet(InstanceId() + ".p", p);
        }

    } // end TWebForm_remind_username

}
