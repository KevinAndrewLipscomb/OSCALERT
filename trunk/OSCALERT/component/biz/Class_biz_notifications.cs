using Class_biz_members;
using Class_biz_roles;
using Class_biz_user;
using Class_biz_users;
using Class_db_notifications;
using kix;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace Class_biz_notifications
  {

  public class TClass_biz_notifications
    {

    private class Static
      {
      public static char[] BreakChars = new char[3 + 1];
      static Static() // constructor
        {
        BreakChars[1] = Convert.ToChar(k.SPACE);
        BreakChars[2] = Convert.ToChar(k.TAB);
        BreakChars[3] = Convert.ToChar(k.HYPHEN);
        }
      }

    private string application_name = String.Empty;
    private TClass_db_notifications db_notifications = null;
    private string host_domain_name = String.Empty;
    private string runtime_root_fullspec = String.Empty;

    public TClass_biz_notifications() : base()
      {
      application_name = ConfigurationManager.AppSettings["application_name"];
      db_notifications = new TClass_db_notifications();
      host_domain_name = ConfigurationManager.AppSettings["host_domain_name"];
      runtime_root_fullspec = ConfigurationManager.AppSettings["runtime_root_fullspec"];
      }

    public void BindDirectToListControl
      (
      object target,
      string unselected_literal,
      string selected_value
      )
      {
      db_notifications.BindDirectToListControl(target,unselected_literal,selected_value);
      }
    public void BindDirectToListControl(object target, string unselected_literal)
      {
      BindDirectToListControl(target, unselected_literal, selected_value:k.EMPTY);
      }
    public void BindDirectToListControl(object target)
      {
      BindDirectToListControl(target, unselected_literal:"-- Notification --");
      }

    private delegate string IssueForForgottenUsername_Merge(string s);
    public void IssueForForgottenUsername(string email_address, string username, string client_host_name)
      {
      TClass_biz_user biz_user;
      TClass_biz_users biz_users;
      StreamReader template_reader;

      IssueForForgottenUsername_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>", application_name)
          .Replace("<host_domain_name/>", host_domain_name)
          .Replace("<client_host_name/>", client_host_name)
          .Replace("<email_address/>", client_host_name)
          .Replace("<username/>", username);
        };

      biz_user = new TClass_biz_user();
      biz_users = new TClass_biz_users();
      template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/username_reminder.txt"));
      k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], email_address, Merge(template_reader.ReadLine()), Merge(template_reader.ReadToEnd()));
      template_reader.Close();
      }

    private delegate string IssueForMembershipEstablishmentBlocked_Merge(string s);
    public void IssueForMembershipEstablishmentBlocked
      (
      string username,
      string user_id,
      string claimed_role_name,
      string claimed_member_name,
      string claimed_member_id,
      string claimed_member_email_address
      )
      {
      var user_email_address = k.EMPTY;

      IssueForMembershipEstablishmentBlocked_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>",application_name)
          .Replace("<host_domain_name/>",host_domain_name)
          .Replace("<username/>",username)
          .Replace("<user_id/>",user_id)
          .Replace("<user_email_address/>",user_email_address)
          .Replace("<claimed_role_name/>",claimed_role_name)
          .Replace("<claimed_member_name/>",claimed_member_name)
          .Replace("<claimed_member_id/>",claimed_member_id)
          .Replace("<claimed_member_email_address/>",claimed_member_email_address);
        };

      var biz_user = new TClass_biz_user();
      var template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/membership_establishment_blocked.txt"));
      user_email_address = biz_user.EmailAddress();
      k.SmtpMailSend
        (
        ConfigurationManager.AppSettings["sender_email_address"],
        ConfigurationManager.AppSettings["application_name"] + "-appadmin@" + host_domain_name + k.COMMA + ConfigurationManager.AppSettings["sysadmin_sms_address"],
        Merge(template_reader.ReadLine()),
        Merge(template_reader.ReadToEnd()),
        false,
        k.EMPTY,
        k.EMPTY,
        user_email_address
        );
      template_reader.Close();
      }

    private delegate string IssueForMembershipEstablishmentTrouble_Merge(string s);
    public void IssueForMembershipEstablishmentTrouble(string full_name, string explanation)
      {
      string user_email_address = k.EMPTY;
      TClass_biz_user biz_user;
      StreamReader template_reader;

      IssueForMembershipEstablishmentTrouble_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<full_name/>", full_name.ToUpper())
          .Replace("<user_email_address/>", user_email_address)
          .Replace("<application_name/>", application_name)
          .Replace("<explanation/>", k.WrapText(explanation, (k.NEW_LINE + "   "), Static.BreakChars, short.Parse(ConfigurationManager.AppSettings["email_blockquote_maxcol"])))
          .Replace("<host_domain_name/>", host_domain_name);
        };

      biz_user = new TClass_biz_user();
      template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/membership_establishment_trouble.txt"));
      user_email_address = biz_user.EmailAddress();
      k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], ConfigurationManager.AppSettings["membership_establishment_liaison"] + k.COMMA + ConfigurationManager.AppSettings["application_name"] + "-appadmin@" + host_domain_name, Merge(template_reader.ReadLine()), Merge(template_reader.ReadToEnd()));
      template_reader.Close();
      }

    private delegate string IssueForRoleChange_Merge(string s);
    public void IssueForRoleChange(string member_id, string role_id, bool be_granted)
      {
      string actor = k.EMPTY;
      string actor_email_address = k.EMPTY;
      string actor_member_id;
      TClass_biz_members biz_members;
      TClass_biz_roles biz_roles;
      TClass_biz_user biz_user;
      TClass_biz_users biz_users;
      string changed = k.EMPTY;
      string first_name = k.EMPTY;
      string last_name = k.EMPTY;
      string role_name = k.EMPTY;
      StreamReader template_reader;
      string to_or_from = k.EMPTY;

      IssueForRoleChange_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>", application_name)
          .Replace("<host_domain_name/>", host_domain_name)
          .Replace("<actor/>", actor)
          .Replace("<actor_email_address/>", actor_email_address)
          .Replace("<changed/>", changed)
          .Replace("<to_or_from/>", to_or_from)
          .Replace("<first_name/>", first_name)
          .Replace("<last_name/>", last_name)
          .Replace("<role_name/>", role_name)
          .Replace("<runtime_root_fullspec/>", runtime_root_fullspec);
        };

      biz_members = new TClass_biz_members();
      biz_roles = new TClass_biz_roles();
      biz_user = new TClass_biz_user();
      biz_users = new TClass_biz_users();
      actor_member_id = biz_members.IdOfUserId(biz_user.IdNum());
      actor = biz_user.Roles()[0] + k.SPACE + biz_members.FirstNameOfMemberId(actor_member_id) + k.SPACE + biz_members.LastNameOfMemberId(actor_member_id);
      actor_email_address = biz_users.PasswordResetEmailAddressOfId(biz_user.IdNum());
      if (be_granted)
        {
        changed = "granted";
        to_or_from = "to";
        }
      else
        {
        changed = "removed";
        to_or_from = "from";
        }
      first_name = biz_members.FirstNameOfMemberId(member_id);
      last_name = biz_members.LastNameOfMemberId(member_id);
      role_name = biz_roles.NameOfId(role_id);
      template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/role_change.txt"));
      k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], biz_members.EmailAddressOf(member_id) + k.COMMA + actor_email_address + k.COMMA + db_notifications.TargetOf("role-change", member_id), Merge(template_reader.ReadLine()), Merge(template_reader.ReadToEnd()), false, k.EMPTY, k.EMPTY, actor_email_address);
      template_reader.Close();
      }

    private delegate string IssueForTemporaryPassword_Merge(string s);
    public void IssueForTemporaryPassword(string username, string client_host_name, string temporary_password)
      {
      TClass_biz_user biz_user;
      TClass_biz_users biz_users;
      StreamReader template_reader;

      IssueForTemporaryPassword_Merge Merge = delegate (string s)
        {
        return s
          .Replace("<application_name/>", application_name)
          .Replace("<host_domain_name/>", host_domain_name)
          .Replace("<client_host_name/>", client_host_name)
          .Replace("<temporary_password/>", temporary_password);
        };

      biz_user = new TClass_biz_user();
      biz_users = new TClass_biz_users();
      template_reader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("template/notification/temporary_password.txt"));
      k.SmtpMailSend(ConfigurationManager.AppSettings["sender_email_address"], biz_users.PasswordResetEmailAddressOfUsername(username), Merge(template_reader.ReadLine()), Merge(template_reader.ReadToEnd()));
      template_reader.Close();
      }

    } // end TClass_biz_notifications

  }
