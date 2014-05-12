using Class_db_user;
using Class_db_users;
using System.Web;

namespace Class_biz_user
  {

  public class TClass_biz_user
    {

    private TClass_db_user db_user = null;
    private TClass_db_users db_users = null;

    public TClass_biz_user() : base()
      {
      db_user = new TClass_db_user();
      db_users = new TClass_db_users();
      }

    public void BindNotificationsToBaseDataList(object target)
      {
      db_user.BindNotificationsToBaseDataList(IdNum(),target);
      }

    public void BindPrivilegesToBaseDataList(object target)
      {
      db_user.BindPrivilegesToBaseDataList(IdNum(),target);
      }

    public void BindRolesToBaseDataList(object target)
      {
      db_user.BindRolesToBaseDataList(IdNum(),target);
      }

    public string EmailAddress()
      {
      return db_users.PasswordResetEmailAddressOfId(IdNum());
      }

    public string IdNum()
      {
      return db_users.IdOf(HttpContext.Current.User.Identity.Name);
      }

    public string[] Privileges()
      {
      return db_users.PrivilegesOf(IdNum());
      }

    public string[] Roles()
      {
      return db_user.RolesOf(IdNum());
      }

    } // end TClass_biz_user

  }
