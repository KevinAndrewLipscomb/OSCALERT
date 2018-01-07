using Class_biz_cad_records;
using Class_biz_field_situations;
using Class_ss_imagetrendelite;
using kix;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Class_ac_cad_activity_notification_agent
  {

  public class TClass_ac_cad_activity_notification_agent
    {

    public TClass_ac_cad_activity_notification_agent
      (
      DateTime datetime_to_quit,
      StreamWriter log
      )
      {
      log.WriteLine(DateTime.Now.ToString("s") + ">Reached TClass_ac_cad_activity_notification_agent.");
      //
      var saved_meta_surge_alert_timestamp_ems = DateTime.MinValue;
      var saved_meta_surge_alert_timestamp_als = DateTime.MinValue;
      var saved_meta_surge_alert_timestamp_fire = DateTime.MinValue;
      //
      var biz_cad_records = new TClass_biz_cad_records();
      var biz_field_situations = new TClass_biz_field_situations();
      var ss_imagetrendelite = new TClass_ss_imagetrendelite();
      //
      var authorization_token = ss_imagetrendelite.AuthorizationTokenOf
        (
        username:ConfigurationManager.AppSettings["vbemsbridge_username"],
        password:ConfigurationManager.AppSettings["vbemsbridge_password"]
        );
      log.WriteLine(DateTime.Now.ToString("s") + ">From ss_imagetrendelite.AuthorizationTokenOf, got: " + authorization_token);
      while (DateTime.Now < datetime_to_quit)
        {
        var current_ems_cad_list = ss_imagetrendelite.CurrentEmsCadList(authorization_token);
        if (current_ems_cad_list == null)
          {
          log.WriteLine(DateTime.Now.ToString("s") + "***ss_imagetrendelite.CurrentEmsCadList returned null.");
          }
        else
          {
          var rows = current_ems_cad_list.Records;
          //
          var current_incident_num = k.EMPTY;
          var nature = k.EMPTY;
          var saved_incident_num = k.EMPTY;
          //
          for (var i = new k.subtype<int>(0,rows.Count); i.val < i.LAST; i.val++)
            {
            var cells = rows[i.val].Columns;
            if (cells[10].Value.Length > 1) // incident_date/time_initialized
              {
              current_incident_num = cells[1].Value;
              //if (current_incident_num != saved_incident_num)
              //  {
              //  //
              //  // Determine nature, if supported.
              //  //
              //  }
              biz_cad_records.Set
                (
                id:k.EMPTY,
                incident_date:(cells[10].Value.Split())[0],
                incident_num:current_incident_num,
                incident_address:cells[5].Value,
                call_sign:cells[7].Value,
                time_initialized:(cells[10].Value.Split())[1],
                time_of_alarm:(cells[0].Value.Length > 1 ? (cells[0].Value.Split())[1] : k.EMPTY),
                time_enroute:(cells[11].Value.Length > 1 ? (cells[11].Value.Split())[1] : k.EMPTY),
                time_on_scene:(cells[17].Value.Length > 1 ? (cells[17].Value.Split())[1] : k.EMPTY),
                time_transporting:(cells[13].Value.Length > 1 ? (cells[13].Value.Split())[1] : k.EMPTY),
                time_at_hospital:(cells[14].Value.Length > 1 ? (cells[14].Value.Split())[1] : k.EMPTY),
                time_available:(cells[15].Value.Length > 1 ? (cells[15].Value.Split())[1] : k.EMPTY),
                time_downloaded:k.EMPTY,
                nature:nature
                );
              saved_incident_num = current_incident_num;
              }
            }
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
          }
        //
        Thread.Sleep(millisecondsTimeout:int.Parse(ConfigurationManager.AppSettings["vbemsbridge_refresh_rate_in_seconds"])*1000);
        }
      }

    }

  }