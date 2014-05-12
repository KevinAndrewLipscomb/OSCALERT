using System;

using System.Collections;
using Class_db_role_member_map;
using Class_biz_notifications;
using Class_biz_user;
namespace Class_biz_role_member_map
{
    public class TClass_biz_role_member_map
    {
        private TClass_db_role_member_map db_role_member_map = null;
        private TClass_biz_notifications biz_notifications = null;
        private TClass_biz_user biz_user = null;
        //Constructor  Create()
        public TClass_biz_role_member_map() : base()
        {
            db_role_member_map = new TClass_db_role_member_map();
            biz_notifications = new TClass_biz_notifications();
            biz_user = new TClass_biz_user();
        }
        public bool BePrivilegedToModifyTuple(bool has_config_roles_and_matrices, bool has_assign_roles_to_members, string role_natural_text)
        {
            bool result;
            result = has_config_roles_and_matrices || ((role_natural_text != "Application Administrator") && has_assign_roles_to_members);
            return result;
        }

        public void Bind(string sort_order, bool be_sort_order_descending, object target, out ArrayList crosstab_metadata)
        {
            db_role_member_map.Bind(sort_order, be_sort_order_descending, target, out crosstab_metadata);
        }

        public void BindActuals(string sort_order, bool be_sort_order_ascending, object target)
        {
            db_role_member_map.BindActuals(sort_order, be_sort_order_ascending, target);
        }

        public void BindHolders(string role_name, object target, string sort_order, bool be_sort_order_ascending)
        {
            db_role_member_map.BindHolders(role_name, target, sort_order, be_sort_order_ascending);
        }

        public void Save(string member_id, string role_id, bool be_granted)
        {
            db_role_member_map.Save(member_id, role_id, be_granted);
            biz_notifications.IssueForRoleChange(member_id, role_id, be_granted);
        }

    } // end TClass_biz_role_member_map

}
