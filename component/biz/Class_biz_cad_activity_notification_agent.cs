using Class_biz_cad_records;
using Class_biz_field_situations;
using Class_ss_imagetrendelite;
using kix;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Class_biz_cad_activity_notification_agent
  {

  public class TClass_biz_cad_activity_notification_agent
    {

    internal void Work
      (
      DateTime datetime_to_quit,
      StreamWriter log
      )
      {
      //
      var biz_cad_records = new TClass_biz_cad_records();
      var biz_field_situations = new TClass_biz_field_situations();
      var ss_imagetrendelite = new TClass_ss_imagetrendelite();
      //
      var address = k.EMPTY;
      var current_incident_num = k.EMPTY;
      var incident_date_time_initialized = k.EMPTY;
      var nature = k.EMPTY;
      //var saved_incident_num = k.EMPTY; // for use managing nature
      var saved_meta_surge_alert_timestamp_ems = DateTime.MinValue;
      var saved_meta_surge_alert_timestamp_als = DateTime.MinValue;
      var saved_meta_surge_alert_timestamp_fire = DateTime.MinValue;
      //
      TClass_ss_imagetrendelite.EmsCadList current_ems_cad_list;
      //
      var authorization_token = ss_imagetrendelite.AuthorizationTokenOf
        (
        username:ConfigurationManager.AppSettings["vbemsbridge_username"],
        password:ConfigurationManager.AppSettings["vbemsbridge_password"]
        );
      var datetime_of_last_nudge = DateTime.Now;
      var request_identifier = Guid.NewGuid().ToString();
      while (DateTime.Now < datetime_to_quit)
        {
        if (DateTime.Now > datetime_of_last_nudge.AddMinutes(double.Parse(ConfigurationManager.AppSettings["nudge_interval_minutes"])))
          {
          ss_imagetrendelite.Nudge(authorization_token);
          datetime_of_last_nudge = DateTime.Now;
          }
        current_ems_cad_list = ss_imagetrendelite.CurrentEmsCadList(authorization_token,request_identifier,log);
        if (current_ems_cad_list == null)
          {
          log.WriteLine(DateTime.Now.ToString("s") + "***ss_imagetrendelite.CurrentEmsCadList returned null.");
          }
        else
          {
          request_identifier = current_ems_cad_list.RequestIdentifier;
          var rows = current_ems_cad_list.Records;
          for (var i = new k.subtype<int>(0,rows.Count); i.val < i.LAST; i.val++)
            {
            var cells = rows[i.val].Columns;
            //
            // Remove the IncidentNumberNotValue cell if it exists in this record.  It only appears in some records.
            //
            if (cells.Count == 19)
              {
              cells.RemoveAt(2);
              }
            //
            address = cells[4].Value;
            current_incident_num = cells[1].Value;
            incident_date_time_initialized = cells[9].Value;
            if(
                (incident_date_time_initialized.Length > 1) // there is an incident_date/time_initialized
              &&
                (address.Length > 1) // there is an address
              &&
                (
                  (current_incident_num[0] == 'E') // this is an EMS incident whose number starts with EMS
                ||
                  (current_incident_num[0] == 'F') // this if a fire incident whose number starts with FD
                )
              )
              {
              //if (current_incident_num != saved_incident_num)
              //  {
              //  //
              //  // Determine nature, if supported.
              //  //
              //  }
              biz_cad_records.Set
                (
                id:k.EMPTY,
                incident_date:(incident_date_time_initialized.Split())[0],
                incident_num:current_incident_num,
                incident_address:address,
                call_sign:cells[6].Value,
                time_initialized:(incident_date_time_initialized.Split())[1],
                time_of_alarm:(cells[0].Value.Contains(k.SPACE) ? (cells[0].Value.Split())[1] : k.EMPTY),
                time_enroute:(cells[10].Value.Contains(k.SPACE) ? (cells[10].Value.Split())[1] : k.EMPTY),
                time_on_scene:(cells[16].Value.Contains(k.SPACE) ? (cells[16].Value.Split())[1] : k.EMPTY),
                time_transporting:(cells[12].Value.Contains(k.SPACE) ? (cells[12].Value.Split())[1] : k.EMPTY),
                time_at_hospital:(cells[13].Value.Contains(k.SPACE) ? (cells[13].Value.Split())[1] : k.EMPTY),
                time_available:(cells[14].Value.Contains(k.SPACE) ? (cells[14].Value.Split())[1] : k.EMPTY),
                time_downloaded:k.EMPTY,
                nature:nature
                );
              //saved_incident_num = current_incident_num; // for use managing nature
              }
            cells.Clear();
            }
          rows.Clear();
          //
          // Validate and trim the cad_records.
          //
          biz_cad_records.ValidateAndTrim();
          //
          // Notify members as appropriate.
          //
          biz_field_situations.DetectAndNotify
            (
            saved_multambholds_alert_timestamp:ref saved_meta_surge_alert_timestamp_ems,
            saved_multalsholds_alert_timestamp:ref saved_meta_surge_alert_timestamp_als,
            saved_firesurge_alert_timestamp:ref saved_meta_surge_alert_timestamp_fire
            );
          //
          }
        //
        Thread.Sleep(millisecondsTimeout:int.Parse(ConfigurationManager.AppSettings["vbemsbridge_refresh_rate_in_seconds"])*1000);
        }
      }

    }

  }
