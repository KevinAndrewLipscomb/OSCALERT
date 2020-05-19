using Class_db;
using Class_db_trail;
using kix;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace Class_db_members
  {

  public class TClass_db_members: TClass_db
    {

    private readonly TClass_db_trail db_trail = null;

    public TClass_db_members() : base()
      {
      db_trail = new TClass_db_trail();
      }

    // internal void Add
    //   (
    //   attributes..
    //   )
    //   {
    //   }

    // internal bool BeKnown
    //   (
    //   attributes..
    //   )
    //   {
    //   var be_known = true;
    //   return be_known;
    //   }

    internal bool BeRoleHolderBySharedSecret
      (
      string shared_secret,
      out string claimed_role_name,
      out string claimed_member_name,
      out string claimed_member_id,
      out string claimed_member_email_address
      )
      {
      var be_role_holder_by_shared_secret = false;
      claimed_role_name = k.EMPTY;
      claimed_member_name = k.EMPTY;
      claimed_member_id = k.EMPTY;
      claimed_member_email_address = k.EMPTY;
      Open();
      using var my_sql_command = new MySqlCommand
        (
        "select role.name as role_name"
        + " , concat(member.first_name,' ',member.last_name) as member_name"
        + " , member.id as member_id"
        + " , IFNULL(email_address,'') as email_address"
        + " from member"
        +   " join role_member_map on (role_member_map.member_id=member.id)"
        +   " join role on (role.id=role_member_map.role_id)"
        + " where registration_code = '" + shared_secret + "'"
        + " order by role.pecking_order"
        + " limit 1",
        connection
        );
      var dr = my_sql_command.ExecuteReader();
      if (dr.Read())
        {
        claimed_role_name = dr["role_name"].ToString();
        claimed_member_name = dr["member_name"].ToString();
        claimed_member_id = dr["member_id"].ToString();
        claimed_member_email_address = dr["email_address"].ToString();
        be_role_holder_by_shared_secret = true;
        }
      dr.Close();
      Close();
      return be_role_holder_by_shared_secret;
      }

    public bool BeValidProfile(string id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select be_valid_profile from member where id = '" + id + "'", connection);
      var be_valid_profile = ("1" == my_sql_command.ExecuteScalar().ToString());
      Close();
      return be_valid_profile;
      }

    public void BindDirectToListControl
      (
      object target,
      string unselected_literal,
      string selected_value
      )
      {
      ((target) as ListControl).Items.Clear();
      if (unselected_literal.Length > 0)
        {
        ((target) as ListControl).Items.Add(new ListItem(unselected_literal, k.EMPTY));
        }
      Open();
      using var my_sql_command = new MySqlCommand("select member.id as member_id, concat(last_name,', ',first_name) as member_designator from member order by member_designator", connection);
      var dr = my_sql_command.ExecuteReader();
      while (dr.Read())
        {
        ((target) as ListControl).Items.Add(new ListItem(dr["member_designator"].ToString(), dr["member_id"].ToString()));
        }
      dr.Close();
      Close();
      if (selected_value.Length > 0)
        {
        ((target) as ListControl).SelectedValue = selected_value;
        }
      }

    public void BindDirectToListControl
      (
      object target,
      string unselected_literal
      )
      {
      BindDirectToListControl(target, unselected_literal, k.EMPTY);
      }
    public void BindDirectToListControl(object target)
      {
      BindDirectToListControl(target, unselected_literal:"-- Member --");
      }

    public string EmailAddressOf(string member_id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select email_address from member where id = '" + member_id + "'", connection);
      var email_address_obj = my_sql_command.ExecuteScalar().ToString();
      Close();
      return (email_address_obj == null ? k.EMPTY : email_address_obj.ToString());
      }

    public string FirstNameOfMemberId(string member_id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select first_name from member where id = '" + member_id + "'", connection);
      var first_name_of_member_id = my_sql_command.ExecuteScalar().ToString();
      Close();
      return first_name_of_member_id;
      }

    public string IdOfUserId(string user_id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select member_id from user_member_map where user_id = '" + user_id + "'", connection);
      var member_id_obj = my_sql_command.ExecuteScalar();
      Close();
      return (member_id_obj == null ? k.EMPTY : member_id_obj.ToString());
      }

    public string LastNameOfMemberId(string member_id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select last_name from member where id = '" + member_id + "'", connection);
      var last_name_of_member_id = my_sql_command.ExecuteScalar().ToString();
      Close();
      return last_name_of_member_id;
      }

    public void SetEmailAddress(string id, string email_address)
      {
      Open();
      using var my_sql_command = new MySqlCommand(db_trail.Saved("UPDATE member SET email_address = '" + email_address + "' WHERE id = '" + id + "'"), connection);
      my_sql_command.ExecuteNonQuery();
      Close();
      }

    public string UserIdOf(string member_id)
      {
      Open();
      using var my_sql_command = new MySqlCommand("select user_id from user_member_map where member_id = '" + member_id + "'", connection);
      var user_id_obj = my_sql_command.ExecuteScalar();
      Close();
      return (user_id_obj == null ? k.EMPTY : user_id_obj.ToString());
      }

    } // end TClass_db_members

  }
