using Class_biz_cad_activity_notification_agent;
using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web;

namespace cad_activity_notification_agent
  {

  public partial class TWebForm_cad_activity_notification_agent: ki_web_ui.page_class
    {

    protected void Page_Load(object sender, EventArgs e)
      {
      var biz_cad_activity_notification_agent = new TClass_biz_cad_activity_notification_agent();
      //
      var today_0026 = DateTime.Today.AddMinutes(26);
      var today_0326 = DateTime.Today.AddHours(3).AddMinutes(26);
      var today_0626 = DateTime.Today.AddHours(6).AddMinutes(26);
      var today_0926 = DateTime.Today.AddHours(9).AddMinutes(26);
      var today_1226 = DateTime.Today.AddHours(12).AddMinutes(26);
      var today_1526 = DateTime.Today.AddHours(15).AddMinutes(26);
      var today_1826 = DateTime.Today.AddHours(18).AddMinutes(26);
      var today_2126 = DateTime.Today.AddHours(21).AddMinutes(26);
      var now = DateTime.Now;
      var datetime_to_quit = (now < today_0026 ? today_0026 : (now < today_0326 ? today_0326 : (now < today_0626 ? today_0626 : (now < today_0926 ? today_0926 : (now < today_1226 ? today_1226 : (now < today_1526 ? today_1526 : (now < today_1826 ? today_1826 : (now < today_2126 ? today_2126 : today_0026.AddDays(1)))))))));
      //
      using var log = new StreamWriter(path:HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["scratch_folder"] + "/cad_activity_notification_agent.log"),append:true);
      log.AutoFlush = true;
      //
      while (DateTime.Now < datetime_to_quit)
        {
        //
        // Start the agent.  It will block until it terminates.
        //
        biz_cad_activity_notification_agent.Work(datetime_to_quit,log);
        //
        // If the agent terminates, wait one minute prior to launching a new one, to make sure the remote site has had time to properly reset itself (since we haven't built a login re-try
        // mechanism), then loop back to start a new agent (if it's not quitting time).
        //
        Thread.Sleep(millisecondsTimeout:60000);
        }
      }

    }

  }
