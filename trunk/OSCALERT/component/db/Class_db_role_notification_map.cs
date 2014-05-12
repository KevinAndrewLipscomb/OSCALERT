using MySql.Data.MySqlClient;
using kix;
using System;


using System.Collections;
using System.Web.UI.WebControls;
using Class_db;
using Class_db_trail;
using Class_db_roles;
namespace Class_db_role_notification_map
{
    public class TClass_db_role_notification_map: TClass_db
    {
        private TClass_db_trail db_trail = null;
        //Constructor  Create()
        public TClass_db_role_notification_map() : base()
        {
            db_trail = new TClass_db_trail();
        }
        public void Bind(string sort_order, bool be_sort_order_descending, object target, out ArrayList crosstab_metadata_rec_arraylist)
        {
            crosstab_metadata_rec_type crosstab_metadata_rec;
            string crosstab_sql;
            MySqlDataReader dr;
            string where_clause;
            crosstab_metadata_rec.index = 1;
            // init to index of last non-dependent column
            crosstab_metadata_rec_arraylist = new ArrayList();
            crosstab_sql = k.EMPTY;
            this.Open();
            dr = new MySqlCommand("select id,name,soft_hyphenation_text from role where name <> \"Member\"", this.connection).ExecuteReader();
            while (dr.Read())
            {
                crosstab_metadata_rec.index = crosstab_metadata_rec.index + 1;
                crosstab_metadata_rec.id = dr["id"].ToString();
                crosstab_metadata_rec.natural_text = dr["name"].ToString();
                crosstab_metadata_rec.soft_hyphenation_text = dr["soft_hyphenation_text"].ToString();
                crosstab_metadata_rec.sql_name = k.Safe(crosstab_metadata_rec.natural_text, k.safe_hint_type.ECMASCRIPT_WORD);
                crosstab_sql = crosstab_sql + k.COMMA_SPACE + "IFNULL((select 1 from role_notification_map where role_id = \"" + dr["id"].ToString() + "\" and notification_id = notification.id),0) as " + crosstab_metadata_rec.sql_name;
                crosstab_metadata_rec_arraylist.Add(crosstab_metadata_rec);
            }
            dr.Close();
            // if filter = k.EMPTY then begin
            where_clause = k.EMPTY;
            // end else begin
            // // where_clause := ' where agency_id = "' + filter + '"';
            // end;
            if (be_sort_order_descending)
            {
                sort_order = sort_order.Replace("%", " desc");
            }
            else
            {
                sort_order = sort_order.Replace("%", " asc");
            }
            ((target) as GridView).DataSource = new MySqlCommand("select notification.id as notification_id" + " , notification.name as notification_name" + crosstab_sql + " from notification" + " left outer join role_notification_map on (role_notification_map.notification_id=notification.id)" + " left outer join role on (role.id=role_notification_map.role_id)" + where_clause + " group by notification.id" + " order by " + sort_order, this.connection).ExecuteReader();
            ((target) as GridView).DataBind();
            this.Close();

        }

        public void BindActuals(string sort_order, bool be_sort_order_ascending, object target)
        {
            if (be_sort_order_ascending)
            {
                sort_order = sort_order.Replace("%", " asc");
            }
            else
            {
                sort_order = sort_order.Replace("%", " desc");
            }
            this.Open();
            ((target) as GridView).DataSource = new MySqlCommand("select role_id" + " , pecking_order as role_pecking_order" + " , role.name as role_name" + " , notification.name as notification_name" + " , notification_id" + " from role_notification_map" + " join notification on (notification.id=role_notification_map.notification_id)" + " join role on (role.id=role_notification_map.role_id)" + " order by " + sort_order, this.connection).ExecuteReader();
            ((target) as GridView).DataBind();
            this.Close();

        }

        public void Save(string notification_id, string role_id, bool be_granted)
        {
            this.Open();
            if (be_granted)
            {
                new MySqlCommand(db_trail.Saved("insert ignore role_notification_map set notification_id = \"" + notification_id + "\", role_id = \"" + role_id + "\""), this.connection).ExecuteNonQuery();
            }
            else
            {
                new MySqlCommand(db_trail.Saved("delete from role_notification_map where notification_id = \"" + notification_id + "\" and role_id = \"" + role_id + "\""), this.connection).ExecuteNonQuery();
            }
            this.Close();
        }

    } // end TClass_db_role_notification_map

}

namespace Class_db_role_notification_map.Units
{
    public class Class_db_role_notification_map
    {
        public const int CI_NOTIFICATION_ID = 0;
        public const int CI_NOTIFICATION_NAME = 1;
        public const int CI_FIRST_CROSSTAB = 2;
    } // end Class_db_role_notification_map

}

