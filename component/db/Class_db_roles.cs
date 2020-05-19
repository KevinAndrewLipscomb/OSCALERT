using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace Class_db_roles
  {
  public class TClass_db_roles: TClass_db
    {

        private readonly TClass_db_trail db_trail = null;

        //Constructor  Create()
        public TClass_db_roles() : base()
        {
            // TODO: Add any constructor code here
            db_trail = new TClass_db_trail();
        }

        public bool Bind(string partial_name, object target)
        {
            bool result;
            MySqlDataReader dr;
            Open();
            ((target) as ListControl).Items.Clear();
            using var my_sql_command = new MySqlCommand("SELECT name FROM role WHERE name like \"" + partial_name + "%\" order by name", connection);
            dr = my_sql_command.ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), dr["name"].ToString()));
            }
            dr.Close();
            Close();
            result = ((target) as ListControl).Items.Count > 0;
            return result;
        }

        public void BindDirectToListControl(object target, bool has_config_roles_and_matrices, string unselected_literal, string selected_value)
        {
            MySqlDataReader dr;
            string where_clause;
            ((target) as ListControl).Items.Clear();
            if (unselected_literal.Length > 0)
            {
                ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
            }
            where_clause = " where name <> \"Member\"";
            if (!has_config_roles_and_matrices)
            {
                where_clause += " and (name <> \"Application Administrator\")";
            }
            Open();
            using var my_sql_command = new MySqlCommand("SELECT id,name FROM role" + where_clause + " order by pecking_order", connection);
            dr = my_sql_command.ExecuteReader();
            while (dr.Read())
            {
                ((target) as ListControl).Items.Add(new ListItem(dr["name"].ToString(), dr["id"].ToString()));
            }
            dr.Close();
            Close();
            if (selected_value.Length > 0)
            {
                ((target) as ListControl).SelectedValue = selected_value;
            }

        }

        public void BindDirectToListControl(object target, bool has_config_roles_and_matrices)
        {
            BindDirectToListControl(target, has_config_roles_and_matrices, "-- Role --");
        }

        public void BindDirectToListControl(object target, bool has_config_roles_and_matrices, string unselected_literal)
        {
            BindDirectToListControl(target, has_config_roles_and_matrices, unselected_literal, k.EMPTY);
        }

        public bool Delete(string name)
        {
            bool result;
            result = true;
            Open();
            try {
                using var my_sql_command = new MySqlCommand(db_trail.Saved("delete from role where name = " + name), connection);
                my_sql_command.ExecuteNonQuery();
            }
            catch(System.Exception e) {
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

        public bool Get(string name, out string soft_hyphenation_text, out string pecking_order)
        {
            bool result;
            MySqlDataReader dr;

            soft_hyphenation_text = k.EMPTY;
            pecking_order = k.EMPTY;
            result = false;
            Open();
            using var my_sql_command = new MySqlCommand("select * from role where CAST(name AS CHAR) = \"" + name + "\"", connection);
            dr = my_sql_command.ExecuteReader();
            if (dr.Read())
            {
                name = dr["name"].ToString();
                soft_hyphenation_text = dr["soft_hyphenation_text"].ToString();
                pecking_order = dr["pecking_order"].ToString();
                result = true;
            }
            dr.Close();
            Close();
            return result;
        }

        public string NameOfId(string id)
        {
            string result;
            Open();
            using var my_sql_command = new MySqlCommand("select name from role where id = \"" + id + "\"", connection);
            result = my_sql_command.ExecuteScalar().ToString();
            Close();
            return result;
        }

        public void Set(string name, string soft_hyphenation_text, string pecking_order)
        {
            var childless_field_assignments_clause = " soft_hyphenation_text = NULLIF('" + soft_hyphenation_text + "','')" + " , pecking_order = NULLIF('" + pecking_order + "','')";
            db_trail.MimicTraditionalInsertOnDuplicateKeyUpdate
              (
              target_table_name:"role",
              key_field_name:"name",
              key_field_value:name,
              childless_field_assignments_clause:childless_field_assignments_clause
              );
        }

    } // end TClass_db_roles

}
