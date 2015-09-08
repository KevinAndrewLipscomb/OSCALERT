using Class_db;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Class_db_notifications
  {

  public class TClass_db_notifications: TClass_db
    {

    private string tier_2_match_field = k.EMPTY;
    private string tier_3_match_field = k.EMPTY;

    //Constructor  Create()
    public TClass_db_notifications() : base()
      {
      tier_2_match_field = ConfigurationManager.AppSettings["tier_2_match_field"];
      tier_3_match_field = ConfigurationManager.AppSettings["tier_3_match_field"];
      }

    public void BindDirectToListControl
      (
      object target,
      string unselected_literal,
      string selected_value
      )
      {
      ((target) as ListControl).Items.Clear();
      if (unselected_literal != k.EMPTY)
        {
        ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
        }
      Open();
      var dr = new MySqlCommand("select notification.id as notification_id" + " , name as notification_name" + " from notification" + " order by notification_name",connection).ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["notification_name"].ToString(), dr["notification_id"].ToString()));
        }
      dr.Close();
      Close();
      if (selected_value != k.EMPTY)
        {
        ((target) as ListControl).SelectedValue = selected_value;
        }
      }
    public void BindDirectToListControl(object target, string unselected_literal)
      {
      BindDirectToListControl(target,unselected_literal,selected_value:k.EMPTY);
      }
    public void BindDirectToListControl(object target)
      {
      BindDirectToListControl(target,unselected_literal:"-- Notification --");
      }

    public string TargetOf
      (
      string name,
      string member_id
      )
      {
      // tier_2_match_value: string;
      // tier_3_match_value: string;
      var target_of = k.EMPTY;
      Open();
      // //
      // // Get tier 2 and 3 associations of target member.
      // //
      // dr := MySqlCommand.Create
      // ('select ' + tier_2_match_field + k.COMMA + tier_3_match_field + ' from member where id = "' + member_id + '"',connection).ExecuteReader();
      // dr.Read();
      // tier_2_match_value := dr[tier_2_match_field].ToString();
      // tier_3_match_value := dr[tier_3_match_field].ToString();
      // dr.Close();
      // Tier 1 stakeholders
      // + ' where tier_id = 1'
      var dr = new MySqlCommand("select email_address" + " from member" + " join role_member_map on (role_member_map.member_id=member.id)" + " join role_notification_map on (role_notification_map.role_id=role_member_map.role_id)" + " join role on (role.id=role_member_map.role_id)" + " join notification on (notification.id=role_notification_map.notification_id)" + " and notification.name = \"" + name + "\"",connection).ExecuteReader();
      if (dr != null)
        {
        while (dr.Read())
          {
          target_of = target_of + dr["email_address"].ToString() + k.COMMA;
          }
        }
      dr.Close();
      // //
      // // Tier 2 stakeholders
      // //
      // dr := MySqlCommand.Create
      // (
      // 'select email_address'
      // + ' from member'
      // +   ' join role_member_map on (role_member_map.member_id=member.id)'
      // +   ' join role_notification_map on (role_notification_map.role_id=role_member_map.role_id)'
      // +   ' join role on (role.id=role_member_map.role_id)'
      // +   ' join notification on (notification.id=role_notification_map.notification_id)'
      // + ' where tier_id = 2'
      // +   ' and ' + tier_2_match_field + ' = ' + tier_2_match_value
      // +   ' and notification.name = "' + name + '"',
      // connection
      // )
      // .ExecuteReader();
      // if dr <> nil then begin
      // while dr.Read do begin
      // target_of := target_of + dr['email_address'].ToString() + k.COMMA;
      // end;
      // end;
      // dr.Close();
      // //
      // // Tier 3 stakeholders
      // //
      // dr := MySqlCommand.Create
      // (
      // 'select email_address'
      // + ' from member'
      // +   ' join role_member_map on (role_member_map.member_id=member.id)'
      // +   ' join role_notification_map on (role_notification_map.role_id=role_member_map.role_id)'
      // +   ' join role on (role.id=role_member_map.role_id)'
      // +   ' join notification on (notification.id=role_notification_map.notification_id)'
      // + ' where tier_id = 3'
      // +   ' and ' + tier_2_match_field + ' = ' + tier_2_match_value
      // +   ' and ' + tier_3_match_field + ' = ' + tier_3_match_value
      // +   ' and notification.name = "' + name + '"',
      // connection
      // )
      // .ExecuteReader();
      // if dr <> nil then begin
      // while dr.Read do begin
      // target_of := target_of + dr['email_address'].ToString() + k.COMMA;
      // end;
      // end;
      // dr.Close();
      Close();
      return (target_of.Length > 0 ? target_of.Substring(0, target_of.Length - 1) : k.EMPTY);
      }

    internal string TargetOfOscalert(string description)
      {
      var target_of_oscalert = k.EMPTY;
      var condition_clause = k.EMPTY;
      if (new ArrayList() {"AlsNeeded","CardiacArrestAlsNeeded","MultAlsHolds"}.Contains(description))
        {
        condition_clause = " min_oscalert_peck_order_als <= (select pecking_order from field_situation_impression where description = '" + description + "')";
        }
      else if (description == "Trap")
        {
        condition_clause = " do_oscalert_for_trap";
        }
      else if (description == "AirportAlert")
        {
        condition_clause = " do_oscalert_for_airport_alert";
        }
      else if (description == "MrtCall")
        {
        condition_clause = " do_oscalert_for_mrt";
        }
      else if (description == "SarCall")
        {
        condition_clause = " do_oscalert_for_sart";
        }
      else if (new ArrayList() {"StabbingOrGsw","CardiacArrest","WorkingFire"}.Contains(description))
        {
        condition_clause = " FALSE"; // Suppress notifications.  Logging takes place anyway, because it is of interest on the Active Case Board.
        }
      else
        {
        condition_clause = " min_oscalert_peck_order_general <= (select pecking_order from field_situation_impression where description = '" + description + "')";
        }
      Open();
      var dr = new MySqlCommand("select CONCAT(phone_num,'@',hostname) as sms_target from member join sms_gateway on (sms_gateway.id=member.phone_service_id) where " + condition_clause,connection).ExecuteReader();
      while (dr.Read())
        {
        target_of_oscalert += dr["sms_target"].ToString() + k.COMMA;
        }
      dr.Close();
      Close();
      return target_of_oscalert.TrimEnd(new char[] {Convert.ToChar(k.COMMA)});
      }

    } // end TClass_db_notifications

  }
