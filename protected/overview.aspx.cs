using Class_biz_members;
using Class_biz_user;
using Class_biz_users;
using kix;
using System;
using System.Configuration;
using UserControl_establish_membership;
using UserControl_member_binder;

namespace overview
  {

  public partial class TWebForm_overview: ki_web_ui.page_class
    {

    //--
    //
    // PRIVATE
    //
    //--

    private struct p_type
      {
      public TClass_biz_user biz_user;
      public TClass_biz_users biz_users;
      public TClass_biz_members biz_members;
      }

    private p_type p;

    // / <summary>
    // / Required method for Designer support -- do not modify
    // / the contents of this method with the code editor.
    // / </summary>
    private void InitializeComponent()
      {
      PreRender += TWebForm_overview_PreRender;
      }

    private void TWebForm_overview_PreRender(object sender, System.EventArgs e)
      {
      SessionSet(InstanceId() + ".p", p);
      }

    //--
    //
    // PROTECTED
    //
    //--

    protected override void OnInit(EventArgs e)
      {
      // Required for Designer support
      InitializeComponent();
      base.OnInit(e);
      //
      if (NatureOfLanding(InstanceId() + ".p") == nature_of_visit_type.VISIT_POSTBACK_STANDARD)
        {
        p = (p_type)(Session[InstanceId() + ".p"]);
        }
      else if (NatureOfLanding(InstanceId() + ".p") == nature_of_visit_type.VISIT_INITIAL)
        {
        p.biz_user = new TClass_biz_user();
        p.biz_users = new TClass_biz_users();
        p.biz_members = new TClass_biz_members();
        //
        BeginBreadCrumbTrail();
        if (p.biz_users.BeStalePassword(p.biz_user.IdNum()))
          {
          DropCrumbAndTransferTo("change_password.aspx");
          }
        SessionSet("privilege_array", p.biz_user.Privileges());
        if ((Session["privilege_array"] != null))
          {
          SessionSet("member_id", p.biz_members.IdOfUserId(Session["user_id"].ToString()));
          }
        }
      //
      if (p.biz_members.IdOfUserId(p.biz_user.IdNum()) == k.EMPTY)
        {
        // Display controls appropriate ONLY to nonmembers.
        AddIdentifiedControlToPlaceHolder(((TWebUserControl_establish_membership)(LoadControl("~/usercontrol/app/UserControl_establish_membership.ascx"))), "UserControl_establish_membership", PlaceHolder_establish_membership);
        }
      else
        {
        AddIdentifiedControlToPlaceHolder(((TWebUserControl_member_binder)(LoadControl("~/usercontrol/app/UserControl_member_binder.ascx"))), "UserControl_member_binder", PlaceHolder_member_binder);
        }
      }

    protected void Page_Load(object sender, System.EventArgs e)
      {
      if (!IsPostBack)
        {
        Title.Text = ConfigurationManager.AppSettings["application_name"] + " - overview";
        }
//
// ToolkitScriptManager.GetCurrent(Page).EnablePartialRendering = false;
//
      }

    } // end TWebForm_overview

  }
