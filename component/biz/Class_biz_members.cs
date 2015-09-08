using Class_db_members;
using kix;

namespace Class_biz_members
  {

  public class TClass_biz_members
    {

    private TClass_db_members db_members = null;

    public TClass_biz_members() : base()
      {
      db_members = new TClass_db_members();
      }

    public bool Add()
      {
      var add = false;
      //if (!db_members.BeKnown(first_name,last_name,cad_num))
      //  {
      //  db_members.Add
      //    (
      //    attributes..
      //    );
      //  biz_notifications.IssueForMemberAdded
      //    (
      //    attributes..
      //    );
      //  add = true;
      //  }
      return add;
      }

    internal bool BeRoleHolderBySharedSecret
      (
      string shared_secret,
      out string claimed_role_name,
      out string claimed_member_name,
      out string claimed_member_id,
      out string claimed_member_email_address
      )
      {
      return db_members.BeRoleHolderBySharedSecret
        (
        shared_secret,
        out claimed_role_name,
        out claimed_member_name,
        out claimed_member_id,
        out claimed_member_email_address
        );
      }

    public bool BeValidProfile(string id)
      {
      return db_members.BeValidProfile(id);
      }

    public void BindDirectToListControl
      (
      object target,
      string unselected_literal,
      string selected_value
      )
      {
      db_members.BindDirectToListControl(target, unselected_literal, selected_value);
      }

    public void BindDirectToListControl(object target)
      {
      BindDirectToListControl(target, "-- Member --");
      }

    public void BindDirectToListControl
      (
      object target,
      string unselected_literal
      )
      {
      BindDirectToListControl(target, unselected_literal, k.EMPTY);
      }

    public string EmailAddressOf(string member_id)
      {
      return db_members.EmailAddressOf(member_id);
      }

    public string FirstNameOfMemberId(string member_id)
      {
      return db_members.FirstNameOfMemberId(member_id);
      }

    public string IdOfUserId(string user_id)
      {
      return db_members.IdOfUserId(user_id);
      }

    public string LastNameOfMemberId(string member_id)
      {
      return db_members.LastNameOfMemberId(member_id);
      }

    public string UserIdOf(string member_id)
      {
      return db_members.UserIdOf(member_id);
      }

    } // end TClass_biz_members

  }
