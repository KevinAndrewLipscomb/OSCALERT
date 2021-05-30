// Derived from KiAspdotnetFramework/component/db/Class~db~template~kicrudhelped~items.cs~template

using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Class_db_cad_records
  {
  public class TClass_db_cad_records: TClass_db
    {
    private class cad_record_summary
      {
      public string id;
      }

    private readonly TClass_db_trail db_trail = null;

    public TClass_db_cad_records() : base()
      {
      db_trail = new TClass_db_trail();
      }

    internal void Augment()
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "insert into cad_record (incident_date,incident_address,call_sign,time_initialized,time_of_alarm,be_augmented)"
        + " select DATE(transmission_datetime) as incident_date"
        + " , address as incident_address"
        + " , unit as call_sign"
        + " , TIME(transmission_datetime) as time_initialized"
        + " , TIME(transmission_datetime) as time_of_alarm"
        + " , TRUE as be_augmented"
        + " from radio_dispatch"
        +   " join capcode_unit_map on (capcode_unit_map.capcode=radio_dispatch.capcode)"
        +   " left join cad_record on (cad_record.incident_address=radio_dispatch.address and cad_record.call_sign=capcode_unit_map.unit)"
        + " where cad_record.id is null"
        +   " and transmission_datetime > GREATEST((select min(TIMESTAMP(incident_date,time_initialized)) from cad_record where be_current),DATE_SUB(NOW(),INTERVAL 3 HOUR))",
        connection
        );
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    public bool Bind(string partial_spec, object target)
      {
      var concat_clause = "concat(IFNULL(incident_date,'-'),'|',IFNULL(incident_num,'-'),'|',IFNULL(incident_address,'-'),'|',IFNULL(call_sign,'-'),'|',IFNULL(time_initialized,'-'),'|',IFNULL(time_of_alarm,'-'),'|',IFNULL(time_enroute,'-'),'|',IFNULL(time_on_scene,'-'),'|',IFNULL(time_transporting,'-'),'|',IFNULL(time_at_hospital,'-'),'|',IFNULL(time_available,'-'),'|',IFNULL(time_downloaded,'-'))";
      Open();
      ((target) as ListControl).Items.Clear();
      using var my_sql_command = new MySqlCommand
        (
        "select id"
        + " , CONVERT(" + concat_clause + " USING utf8) as spec"
        + " from cad_record"
        + " where " + concat_clause + " like '%" + partial_spec.ToUpper() + "%'"
        + " order by spec",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["spec"].ToString(), dr["id"].ToString()));
        }
      dr.Close();
      Close();
      return ((target) as ListControl).Items.Count > 0;
      }

    internal void BindBaseDataList
      (
      #pragma warning disable CA1801 // Remove unused parameter
      string sort_order,
      bool be_sort_order_ascending,
      object target
      #pragma warning restore CA1801 // Remove unused parameter
      )
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select cad_record.id as id"
        + " from cad_record"
        + (sort_order.Length > 0 ? " order by " + sort_order.Replace("%",(be_sort_order_ascending ? "asc" : "desc")) : k.EMPTY),
        connection
        );
      ((target) as BaseDataList).DataSource = my_sql_command.ExecuteReader();
      ((target) as BaseDataList).DataBind();
      Close();
      }

    public void BindDirectToListControl(object target)
      {
      Open();
      ((target) as ListControl).Items.Clear();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT id"
        + " , CONVERT(concat(IFNULL(incident_date,'-'),'|',IFNULL(incident_num,'-'),'|',IFNULL(incident_address,'-'),'|',IFNULL(call_sign,'-'),'|',IFNULL(time_initialized,'-'),'|',IFNULL(time_of_alarm,'-'),'|',IFNULL(time_enroute,'-'),'|',IFNULL(time_on_scene,'-'),'|',IFNULL(time_transporting,'-'),'|',IFNULL(time_at_hospital,'-'),'|',IFNULL(time_available,'-'),'|',IFNULL(time_downloaded,'-')) USING utf8) as spec"
        + " FROM cad_record"
        + " order by spec",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["spec"].ToString(), dr["id"].ToString()));
        }
      dr.Close();
      Close();
      }

    public bool Delete(string id)
      {
      var result = true;
      Open();
      try
        {
        using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from cad_record where id = \"" + id + "\""), connection);
        my_sql_command.ExecuteNonQuery();
        }
      catch(Exception e)
        {
        if (e.Message.StartsWith("Cannot delete or update a parent row: a foreign key constraint fails", true, null))
          {
          result = false;
          }
        else
          {
          throw;
          }
        }
      Close();
      return result;
      }

    public bool Get
      (
      string id,
      out DateTime incident_date,
      out string incident_num,
      out string incident_address,
      out string call_sign,
      out DateTime time_initialized,
      out DateTime time_of_alarm,
      out DateTime time_enroute,
      out DateTime time_on_scene,
      out DateTime time_transporting,
      out DateTime time_at_hospital,
      out DateTime time_available,
      out DateTime time_downloaded
      )
      {
      incident_date = DateTime.MinValue;
      incident_num = k.EMPTY;
      incident_address = k.EMPTY;
      call_sign = k.EMPTY;
      time_initialized = DateTime.MinValue;
      time_of_alarm = DateTime.MinValue;
      time_enroute = DateTime.MinValue;
      time_on_scene = DateTime.MinValue;
      time_transporting = DateTime.MinValue;
      time_at_hospital = DateTime.MinValue;
      time_available = DateTime.MinValue;
      time_downloaded = DateTime.MinValue;
      var result = false;
      //
      Open();
      using var my_sql_command = new MySqlCommand("select * from cad_record where CAST(id AS CHAR) = \"" + id + "\"", connection);
      var dr = my_sql_command.ExecuteReader();
      if (dr.Read())
        {
        incident_date = DateTime.Parse(dr["incident_date"].ToString());
        incident_num = dr["incident_num"].ToString();
        incident_address = dr["incident_address"].ToString();
        call_sign = dr["call_sign"].ToString();
        time_initialized = DateTime.Parse(dr["time_initialized"].ToString());
        time_of_alarm = DateTime.Parse(dr["time_of_alarm"].ToString());
        time_enroute = DateTime.Parse(dr["time_enroute"].ToString());
        time_on_scene = DateTime.Parse(dr["time_on_scene"].ToString());
        time_transporting = DateTime.Parse(dr["time_transporting"].ToString());
        time_at_hospital = DateTime.Parse(dr["time_at_hospital"].ToString());
        time_available = DateTime.Parse(dr["time_available"].ToString());
        time_downloaded = DateTime.Parse(dr["time_downloaded"].ToString());
        result = true;
        }
      dr.Close();
      Close();
      return result;
      }

    public void Set
      (
      string id,
      string incident_date,
      string incident_num,
      string incident_address,
      string call_sign,
      string time_initialized,
      string time_of_alarm,
      string time_enroute,
      string time_on_scene,
      string time_transporting,
      string time_at_hospital,
      string time_available,
      string time_downloaded,
      string nature
      )
      {
      //
      // Perform the Set in the usual fashion.
      //
      var childless_field_assignments_clause = k.EMPTY
      + "incident_date = STR_TO_DATE(NULLIF('" + incident_date + "',''),'%m/%d/%Y')"
      + " , incident_num = NULLIF('" + incident_num + "','')"
      + " , incident_address = NULLIF('" + incident_address + "','')"
      + " , call_sign = NULLIF('" + call_sign + "','')"
      + " , time_initialized = STR_TO_DATE(NULLIF('" + time_initialized + "',''),'%H:%i:%s')"
      + " , time_of_alarm = STR_TO_DATE(NULLIF('" + time_of_alarm + "',''),'%H:%i:%s')"
      + " , time_enroute = STR_TO_DATE(NULLIF('" + time_enroute + "',''),'%H:%i:%s')"
      + " , time_on_scene = STR_TO_DATE(NULLIF('" + time_on_scene + "',''),'%H:%i:%s')"
      + " , time_transporting = STR_TO_DATE(NULLIF('" + time_transporting + "',''),'%H:%i:%s')"
      + " , time_at_hospital = STR_TO_DATE(NULLIF('" + time_at_hospital + "',''),'%H:%i:%s')"
      + " , time_available = STR_TO_DATE(NULLIF('" + time_available + "',''),'%H:%i:%s')"
      + " , time_downloaded = STR_TO_DATE(NULLIF('" + time_downloaded + "',''),'%m/%d/%y %H:%i:%s')"
      + " , nature = NULLIF('" + nature + "','')"
      + k.EMPTY;
      //
      var target_table_name = "cad_record";
      var key_field_name = "id";
      var key_field_value = id;
      var additional_match_condition = " or (incident_num = '" + incident_num + "' and call_sign = '" + call_sign + "')";
      //
      // The following code is adapted from Class_db_trail.MimicTraditionalInsertOnDuplicateKeyUpdate, but does not journal its activity.
      //
      const string DELIMITER = "~";
      var procedure_name = "MTIODKU_" + ConfigurationManager.AppSettings["application_name"] + "_" + DateTime.Now.Ticks.ToString("D19");
      var code = "/* DELIMITER '" + DELIMITER + "' */"
      + " drop procedure if exists " + procedure_name
      + DELIMITER
      + " create procedure " + procedure_name + "() modifies sql data"
      +   " BEGIN"
      +   " start transaction;"
      +   " if (select 1 from " + target_table_name + " where " + key_field_name + " = '" + key_field_value + "'" + (additional_match_condition.Length > 0 ? additional_match_condition : k.EMPTY) + " limit 1 LOCK IN SHARE MODE) is null"
      +   " then"
      +     " insert " + target_table_name + " set " + key_field_name + " = NULLIF('" + key_field_value + "',''), " + childless_field_assignments_clause + ";"
      +   " else"
      +     " update " + target_table_name + " set " + childless_field_assignments_clause + " where " + key_field_name + " = '" + key_field_value + "'" + (additional_match_condition.Length > 0 ? additional_match_condition : k.EMPTY) + ";"
      +   " end if;"
      +   " commit;"
      +   " END"
      + DELIMITER
      + " call " + procedure_name + "()"
      + DELIMITER
      + " drop procedure if exists " + procedure_name;
      var my_sql_script = new MySqlScript();
      my_sql_script.Connection = connection;
      my_sql_script.Delimiter = DELIMITER;
      my_sql_script.Query = code;
      Open();
      ExecuteOneOffProcedureScriptWithTolerance(procedure_name,my_sql_script);
      ////
      //// Determine if a Nature is known for this record.
      ////
      //var be_nature_unknown_after_set = DBNull.Value == using var my_sql_command = new MySqlCommand
      //  ("select nature from cad_record where id = '" + id + "' or (incident_num = '" + incident_num + "' and call_sign = '" + call_sign + "')",connection); my_sql_command.ExecuteScalar();
      ////
      Close();
      ////
      //return be_nature_unknown_after_set;
      }

    internal object Summary(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "SELECT *"
        + " FROM cad_record"
        + " where id = '" + id + "'",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      dr.Read();
      var the_summary = new cad_record_summary()
        {
        id = id
        };
      dr.Close();
      Close();
      return the_summary;
      }

    internal void ValidateAndTrim()
      {
      //try
      //  {

      Open();
      using var my_sql_command = new MySqlCommand
        (
        k.EMPTY
        //
        // On primary data, set be_current to FALSE on each record which is clearly not associated with the unit's latest incident, or where the unit has already gone available.
        //
        + " update cad_record left join"
        +   " ("
        +   " SELECT call_sign"
        +   " , max(incident_num) as max_incident_num"
        +   " FROM cad_record"
        +   " where call_sign not in (select designator from incident_nature)"
        +   " group by call_sign"
        +   " )"
        +   " as valid_record"
        +     " on (valid_record.call_sign=cad_record.call_sign and valid_record.max_incident_num=cad_record.incident_num)"
        + " set be_current = FALSE"
        + " where (valid_record.max_incident_num is null or time_available is not null)"
        +   " and not be_augmented"
        + ";"
        //
        // On augmented data, set be_current to FALSE on each record which is clearly not associated with the unit's latest incident, or which is probably stale.
        //
        + " update cad_record as target"
        +   " left join"
        +     " ("
        +     " SELECT call_sign"
        +     " , max(time_of_alarm) as max_time_of_alarm"
        +     " FROM cad_record"
        +     " where be_augmented"
        +     " group by call_sign"
        +     " )"
        +     " as valid_record"
        +       " on (valid_record.call_sign=target.call_sign and valid_record.max_time_of_alarm=target.time_of_alarm)"
        +   " left join"
        +     " ("
        +     " select incident_num"
        +     " from cad_record"
        +     " where not be_augmented and be_current"
        +     " group by incident_num"
        +     " )"
        +     " as co_response using (incident_num)"
        + " set be_current = FALSE"
        + " where be_augmented"
        +   " and"
        +     " ("
        +       " valid_record.max_time_of_alarm is null"
        +     " or"
        +       " co_response.incident_num is null"
        +         " and time_of_alarm < DATE_SUB(NOW(),INTERVAL 20 MINUTE)"
        +     " or"
        +       " co_response.incident_num is not null"
        +         " and time_of_alarm < DATE_SUB(NOW(),INTERVAL 30 MINUTE)"
        +         " and nature not like '% structure fire'"
        +         " and nature not like '% in distress'"
        +         " and nature <> 'Cardiac arrest'"
        +     " or"
        +       " co_response.incident_num is not null"
        +         " and time_of_alarm < DATE_SUB(NOW(),INTERVAL 60 MINUTE)"
        +     " )"
        + ";"
        //
        // Set be_current to FALSE on each additional record which really is not associated with the unit's latest incident, but which fact could not be determined in the earlier update because of variations on the unit's
        // call_sign.
        //
        + " update cad_record full_table join cad_record invalid on"
        +   " ("
        +     " full_table.be_current and invalid.be_current"
        +   " and"
        +     " (invalid.incident_num < full_table.incident_num)"
        +   " and"
        +     " ("
                  //--
                  //
                  // Unqualified transformation (ie, "120R"->"120", "L01"->"L01") of invalid.call_sign
                  //
        +       " IF(invalid.call_sign REGEXP '^A[[:digit:]]'," // ambulances
        +         " REPLACE("
        +            " REPLACE("
        +               " REPLACE("
        +                  " REPLACE("
        +                    " invalid.call_sign,"
        +                    " 'R',''),"
        +                  " 'S',''),"
        +               " 'P',''),"
        +            " 'D',''),"
        +         " IF(invalid.call_sign REGEXP '(^E[[:digit:]])|(^L[[:digit:]])|(^FR[[:digit:]])|(^T[[:digit:]])'," // engines, ladders, frsqs, tankers
        +            " REPLACE(invalid.call_sign,'P',''),"
        +            " invalid.call_sign"
        +            " )"
        +         " )"
                  //
                  //--
        +     " ="
                  //--
                  //
                  // Unqualified transformation (ie, "120S"->"120", "L01P"->"L01") of full_table.call_sign
                  //
        +       " IF(full_table.call_sign REGEXP '^A[[:digit:]]'," // ambulances
        +         " REPLACE("
        +            " REPLACE("
        +               " REPLACE("
        +                  " REPLACE("
        +                    " full_table.call_sign,"
        +                    " 'R',''),"
        +                 " 'S',''),"
        +               " 'P',''),"
        +            " 'D',''),"
        +         " IF(full_table.call_sign REGEXP '(^E[[:digit:]])|(^L[[:digit:]])|(^FR[[:digit:]])|(^T[[:digit:]])'," // engines, ladders, frsqs, tankers
        +            " REPLACE(full_table.call_sign,'P',''),"
        +            " full_table.call_sign"
        +            " )"
        +         " )"
                  //
                  //--
        +     " )"
        +   " )"
        + " set invalid.be_current = FALSE"
        + ";"
        //
        // Set be_current to FALSE on lingering "hold", EMSALL, EMTALS, and "R#" designators.
        //
        + " update cad_record"
        + " set be_current = FALSE"
        + " where be_current"
        +   " and"
        +     " ("
        +       " call_sign REGEXP '^HOLD[[:digit:]]' or call_sign REGEXP '^HZC[[:digit:]]' or call_sign REGEXP '^R[[:digit:]]'"
        +     " or"
        +       " call_sign in (select designator from ephemeral_dispatch)"
        +     " )"
        +   " and ABS(TIMESTAMPDIFF(MINUTE,ADDTIME(incident_date,time_of_alarm),CURTIME())) > 90"
        + ";"
        //
        // Set be_current to FALSE on the CBNF and COMITF designators.
        //
        + " update cad_record set be_current = FALSE where be_current and call_sign in ('CBNF','COMITF')"
        + ";"
        //
        // Delete records that are most likely inaccessible to us for updating.
        //
        + " delete from cad_record where incident_date < DATE_ADD(CURDATE(),INTERVAL -7 DAY)"
        ,
        connection
        );
      my_sql_command.ExecuteNonQuery();
      Close();

      //  }
      //catch (Exception the_exception)
      //  {
      //  }
      }

    } // end TClass_db_cad_records

  }
