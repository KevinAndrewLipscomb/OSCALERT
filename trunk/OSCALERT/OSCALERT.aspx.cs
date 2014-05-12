using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Class_biz_user;
using Class_db_user;
using Class_db;
using Default;
using Global;
using salogin;
using change_email_address;
using change_password;
using login;
using exception;
using milestone_sweep;
using Class_biz_milestones;
using Class_db_milestones;
using Class_db_trail;
using Class_biz_scheduled_tasks;
using daily_tasks;
using Class_biz_users;
using Class_db_users;
using Class_biz_notifications;
using new_user_registration;
using remind_username;
using overview;
using Class_biz_members;
using Class_db_members;
using UserControl_establish_membership;
using establish_membership_trouble;
using UserControl_precontent;
using UserControl_postcontent;
using Class_db_notifications;
using test_report;
using UserControl_member_binder;
using UserControl_about;
using UserControl_user;
using UserControl_drop_down_date;
using UserControl_config_binder;
using UserControl_config_welcome;
using UserControl_print_div;
using UserControl_bread_crumb_trail;
using UserControl_common_header_bar;
using Class_biz_roles;
using Class_biz_role_member_map;
using Class_db_roles;
using Class_db_role_member_map;
using UserControl_role_member_matrix;
using Class_biz_role_privilege_map;
using Class_biz_privileges;
using Class_biz_role_notification_map;
using Class_db_role_privilege_map;
using Class_db_privileges;
using Class_db_role_notification_map;
using UserControl_roles_and_matrices_binder;
using UserControl_role_notification_matrix;
using UserControl_role_privilege_matrix;
using UserControl_role;
using UserControl_update_progress_blocker;
using UserControl_role_member_mapping;
using UserControl_role_notification_mapping;
using UserControl_role_privilege_mapping;
using Class_db_user_member_map;
using Class_biz_user_member_map;
using UserControl_user_member_mapping;
using UserControl_users_and_mapping_binder;
using UserControl_session_interrupted;
using UserControl_about_by;
using UserControl_about_tools;
namespace OSCALERT.Units
{
    public class OSCALERT
    {
        // Default.TWebForm_Default: ki_web_ui.page_class
        // Global.TGlobal: System.Web.HttpApplication
        // salogin.TWebForm_salogin: ki_web_ui.page_class
        // change_email_address.TWebForm_change_email_address: ki_web_ui.page_class
        // change_password.TWebForm_change_password: ki_web_ui.page_class
        // login.TWebForm_login: ki_web_ui.page_class
        // exception.TWebForm_exception: ki_web_ui.page_class
        // milestone_sweep.TWebForm_milestone_sweep: system.web.ui.page
        // daily_tasks.TWebForm1: system.web.ui.page
        // new_user_registration.TWebForm_new_user_registration: ki_web_ui.page_class
        // remind_username.TWebForm_remind_username: ki_web_ui.page_class
        // overview.TWebForm_overview: ki_web_ui.page_class
        // UserControl_establish_membership.TWebUserControl_establish_membership: ki_web_ui.usercontrol_class
        // establish_membership_trouble.TWebForm_establish_membership_trouble: ki_web_ui.page_class
        // UserControl_precontent.TWebUserControl_precontent: ki_web_ui.usercontrol_class
        // UserControl_postcontent.TWebUserControl_postcontent: ki_web_ui.usercontrol_class
        // test_report.TWebForm_test_report: system.web.ui.page
        // UserControl_member_binder.TWebUserControl_member_binder: ki_web_ui.usercontrol_class
        // UserControl_about.TWebUserControl_about: ki_web_ui.usercontrol_class
        // UserControl_user.TWebUserControl_user: ki_web_ui.usercontrol_class
        // UserControl_drop_down_date.TWebUserControl_drop_down_date: ki_web_ui.usercontrol_class
        // UserControl_config_binder.TWebUserControl_config_binder: ki_web_ui.usercontrol_class
        // UserControl_print_div.TWebUserControl_print_div: ki_web_ui.usercontrol_class
        // UserControl_bread_crumb_trail.TWebUserControl_bread_crumb_trail: ki_web_ui.usercontrol_class
        // UserControl_common_header_bar.TWebUserControl_common_header_bar: ki_web_ui.usercontrol_class
        // UserControl_role_member_matrix.TWebUserControl_role_member_matrix: ki_web_ui.usercontrol_class
        // UserControl_roles_and_matrices_binder.TWebUserControl_roles_and_matrices_binder: ki_web_ui.usercontrol_class
        // UserControl_role_notification_matrix.TWebUserControl_role_notification_matrix: ki_web_ui.usercontrol_class
        // UserControl_role_privilege_matrix.TWebUserControl_role_privilege_matrix: ki_web_ui.usercontrol_class
        // UserControl_role.TWebUserControl_role: ki_web_ui.usercontrol_class
        // UserControl_update_progress_blocker.TWebUserControl_update_progress_blocker: ki_web_ui.usercontrol_class
        // UserControl_role_member_mapping.TWebUserControl_role_member_mapping: ki_web_ui.usercontrol_class
        // UserControl_role_notification_mapping.TWebUserControl_role_notification_mapping: ki_web_ui.usercontrol_class
        // UserControl_role_privilege_mapping.TWebUserControl_role_privilege_mapping: ki_web_ui.usercontrol_class
        // UserControl_user_member_mapping.TWebUserControl_user_member_mapping: ki_web_ui.usercontrol_class
        // UserControl_users_and_mapping_binder.TWebUserControl_users_and_mapping_binder: ki_web_ui.usercontrol_class
        // UserControl_about_by.TWebUserControl_about_by: ki_web_ui.usercontrol_class
        // UserControl_about_tools.TWebUserControl_about_tools: ki_web_ui.usercontrol_class
        // General Information about an assembly is controlled through the following
        // set of attributes. Change these attribute values to modify the information
        // associated with an assembly.
        // [assembly: AssemblyDescription('')]
        // [assembly: AssemblyConfiguration('')]
        // [assembly: AssemblyCompany('')]
        // [assembly: AssemblyProduct('')]
        // [assembly: AssemblyCopyright('')]
        // [assembly: AssemblyTrademark('')]
        // [assembly: AssemblyCulture('')]
        // The Delphi compiler controls the AssemblyTitleAttribute via the ExeDescription.
        // You can set this in the IDE via the Project Options.
        // Manually setting the AssemblyTitle attribute below will override the IDE
        // setting.
        // [assembly: AssemblyTitle('')]
        // Version information for an assembly consists of the following four values:
        // Major Version
        // Minor Version
        // Build Number
        // Revision
        // You can specify all the values or you can default the Revision and Build Numbers
        // by using the '*' as shown below:
        // [assembly: AssemblyVersion('1.0.*')]
        // In order to sign your assembly you must specify a key to use. Refer to the
        // Microsoft .NET Framework documentation for more information on assembly signing.
        // Use the attributes below to control which key is used for signing.
        // Notes:
        // (*) If no key is specified, the assembly is not signed.
        // (*) KeyName refers to a key that has been installed in the Crypto kind1
        // Provider (CSP) on your machine. KeyFile refers to a file which contains
        // a key.
        // (*) If the KeyFile and the KeyName values are both specified, the
        // following processing occurs:
        // (1) If the KeyName can be found in the CSP, that key is used.
        // (2) If the KeyName does not exist and the KeyFile does exist, the key
        // in the KeyFile is installed into the CSP and used.
        // (*) In order to create a KeyFile, you can use the sn.exe (Strong Name) utility.
        // When specifying the KeyFile, the location of the KeyFile should be
        // relative to the project output directory which is
        // %Project Directory%\bin\<configuration>. For example, if your KeyFile is
        // located in the project directory, you would specify the AssemblyKeyFile
        // attribute as [assembly: AssemblyKeyFile('..\\..\\mykey.snk')]
        // (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
        // documentation for more information on this.
        // [assembly: AssemblyDelaySign(false)]
        // [assembly: AssemblyKeyFile('')]
        // [assembly: AssemblyKeyName('')]
        public static void MainEntry(string[] args)
        {
        }

    } // end OSCALERT

}

