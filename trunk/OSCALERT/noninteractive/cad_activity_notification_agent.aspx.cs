using Class_ac_cad_activity_notification_agent;
using System;
using System.Threading;

namespace cad_activity_notification_agent
  {

  public partial class TWebForm_cad_activity_notification_agent: System.Web.UI.Page
    {

    protected void Page_Load(object sender, System.EventArgs e)
      {
      var today_0326 = DateTime.Today.AddHours(3).AddMinutes(26);
      var today_1126 = DateTime.Today.AddHours(11).AddMinutes(26);
      var today_1926 = DateTime.Today.AddHours(19).AddMinutes(26);
      var now = DateTime.Now;
      var datetime_to_quit = (now < today_0326 ? today_0326 : (now < today_1126 ? today_1126 : (now < today_1926 ? today_1926 : today_0326.AddDays(1))));
      //
      while (DateTime.Now < datetime_to_quit)
        {
        //
        // Start the agent.  It will block until it terminates.
        //
        // IMPORTANT:  See this app's 000-README regarding the recommendation to establish a dedicated, specially-configured application pool for this app to run in.
        //
        new TClass_ac_cad_activity_notification_agent(datetime_to_quit);
        //
        // If the agent terminates, wait one minute prior to launching a new one, to make sure the remote site has had time to properly reset itself (since we haven't built a login re-try
        // mechanism), then loop back to start a new agent (if it's not quitting time).
        //
        Thread.Sleep(millisecondsTimeout:60000);
        }
      }

    }

  }
