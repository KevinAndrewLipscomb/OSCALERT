using CefSharp;
using CefSharp.OffScreen;
using Class_biz_cad_records;
using Class_biz_field_situations;
using Class_ss;
using Class_ss_emsbridge;
using kix;
using System;
using System.Configuration;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Class_ss_imagetrendelite
  {

  public class TClass_ss_imagetrendelite : TClass_ss
    {

    //--
    //
    // PRIVATE
    //
    //--

    private static ChromiumWebBrowser master_browser;

    private TClass_biz_cad_records biz_cad_records = null;
    private TClass_biz_field_situations biz_field_situations = null;
    private EventHandler<LoadingStateChangedEventArgs> master_browser_LoadingStateChanged_event_handler;
    private System.Timers.Timer master_browser_timer;
    private ElapsedEventHandler master_browser_timer_Elapsed_event_handler;
    private k.int_nonnegative master_navigation_counter = new k.int_nonnegative();
    private DateTime saved_datetime_to_quit;
    private DateTime saved_meta_surge_alert_timestamp_ems;
    private DateTime saved_meta_surge_alert_timestamp_als;
    private DateTime saved_meta_surge_alert_timestamp_fire;
    private TClass_ss_emsbridge ss_emsbridge;

    private void ajax_container_PropertyChange
      (
      object sender,
      EventArgs e
      )
      {
      master_browser_timer.Stop();
      //
      master_browser.EvaluateScriptAsync("dom.doc = document;").Wait();
      //
      var rows = dom.doc.GetElementById("ajax_container").GetElementsByTagName("tr");
      //
      var current_incident_num = k.EMPTY;
      var nature = k.EMPTY;
      var saved_incident_num = k.EMPTY;
      //
      for (var i = new k.subtype<int>(2,rows.Count); i.val < i.LAST; i.val++)
        {
        var cells = rows[i.val].GetElementsByTagName("td");
        if(
            (cells.Count == 17)
          &&
            (cells[0].InnerText != null)
          &&
            (cells[1].InnerText != null)
          &&
            (cells[4].InnerText != null)
          &&
            (cells[5].InnerText != null) // I don't know why the remote site sometimes provides a cell[5] with a null InnerText, but it does.
          &&
            (cells[8].InnerText != null)
          &&
            (cells[9].InnerText != null)
          &&
            (cells[10].InnerText != null)
          &&
            (cells[11].InnerText != null)
          &&
            (cells[12].InnerText != null)
          &&
            (cells[13].InnerText != null)
          &&
            (cells[14].InnerText != null)
          &&
            (cells[16].InnerText != null)
          )
        //then
          {
          //
          current_incident_num = k.Safe(cells[1].InnerText.Trim(),k.safe_hint_type.NUM);
          if (current_incident_num != saved_incident_num)
            {
            var row = rows[i.val];
            var part_array = row.OuterHtml.Split
              (
              separator: new string[] { "cadWindow('", "')" },
              options: StringSplitOptions.None
              );
            nature = biz_cad_records.LocalRenditionOf
              (
              ss_emsbridge.NatureOf
                (
                incident_id:k.Safe(part_array[1], k.safe_hint_type.NUM),
                cookie_container:TClass_ss_emsbridge.GetUriCookieContainer(dom.doc.Url)
                )
              );
            }
          biz_cad_records.Set
            (
            id:k.EMPTY,
            incident_date:k.Safe(cells[0].InnerText.Trim(),k.safe_hint_type.DATE_TIME),
            incident_num:current_incident_num,
            incident_address:k.Safe(cells[4].InnerText.Trim(),k.safe_hint_type.MAKE_MODEL),
            call_sign:k.Safe(cells[5].InnerText.Trim(),k.safe_hint_type.ALPHANUM),
            time_initialized:k.Safe(cells[8].InnerText.Trim(),k.safe_hint_type.DATE_TIME),
            time_of_alarm:k.Safe(cells[9].InnerText.Trim(),k.safe_hint_type.DATE_TIME),
            time_enroute:k.Safe(cells[10].InnerText.Trim(),k.safe_hint_type.DATE_TIME),
            time_on_scene:k.Safe(cells[11].InnerText.Trim(),k.safe_hint_type.DATE_TIME),
            time_transporting:k.Safe(cells[12].InnerText.Trim(),k.safe_hint_type.DATE_TIME),
            time_at_hospital:k.Safe(cells[13].InnerText.Trim(),k.safe_hint_type.DATE_TIME),
            time_available:k.Safe(cells[14].InnerText.Trim(),k.safe_hint_type.DATE_TIME),
            time_downloaded:k.Safe(cells[16].InnerText.Trim(),k.safe_hint_type.DATE_TIME),
            nature:nature
            );
          saved_incident_num = current_incident_num;
          }
        }
      //
      // Validate and trim the cad_records.
      //
      biz_cad_records.ValidateAndTrim();
      //
      // Notify members as appropriate.
      //
      biz_field_situations.DetectAndNotify
        (
        saved_multambholds_alert_timestamp:ref saved_meta_surge_alert_timestamp_ems,
        saved_multalsholds_alert_timestamp:ref saved_meta_surge_alert_timestamp_als,
        saved_firesurge_alert_timestamp:ref saved_meta_surge_alert_timestamp_fire
        );
      //
      Thread.Sleep(millisecondsTimeout:int.Parse(ConfigurationManager.AppSettings["vbemsbridge_refresh_rate_in_seconds"])*1000);
      //
      if (DateTime.Now < saved_datetime_to_quit)
        {
        //
        // Update the date span (to account for crossing into the next day at midnight) and click the Refresh button.
        //
        dom.doc.GetElementById("DateFrom").SetAttribute("value",DateTime.Today.AddDays(-2).ToString("d"));
        dom.doc.GetElementById("DateTo").SetAttribute("value",DateTime.Today.ToString("d"));
        dom.doc.GetElementsByTagName("input")[4].InvokeMember("click");
        master_browser_timer.Start();
        }
      else
        {
        Dispose();
        }
      }

    private void master_browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
      {
      if (e.IsLoading)
        {
        master_navigation_counter.val++;
        }
      else
        {
        try // Because this code only runs in the master_browser_thread, any exceptions will not bubble up to TGlobal.Application_Error(), so we must catch and escalate them within master_browser_thread (I think).
          {
          master_browser_timer.Stop();
          //
          master_browser.EvaluateScriptAsync("dom.doc = document;").Wait();
          //
          if (master_navigation_counter.val == 1)
            {
            //
            // Log in.
            //
            dom.doc.GetElementById("UserId").SetAttribute("value", ConfigurationManager.AppSettings["vbemsbridge_username"]);
            dom.doc.GetElementById("Password").SetAttribute("value", ConfigurationManager.AppSettings["vbemsbridge_password"]);
            dom.doc.All["submit"].InvokeMember("click");
            master_browser_timer.Start();
            }
          else if (master_navigation_counter.val == 2)
            {
            //
            // Acknowledge the Data Privacy Statement.
            //
            dom.doc.GetElementById("acc_yes").InvokeMember("click");
            master_browser_timer.Start();
            }
          else if (master_navigation_counter.val == 4)
            {
            //
            // Click the "Dispatch" link.
            //
            dom.doc.Links[1].InvokeMember("click");
            master_browser_timer.Start();
            }
          else if (master_navigation_counter.val == 6)
            {
            //
            // Navigate to the source of the target iframe.
            //
            master_browser.Load(url:"https://vbems.emsbridge.com/resource/apps/caddispatch/cad_dispatch_pages.cfm?item=Dispatch&noLayout");
            master_browser_timer.Start();
            }
          else if (master_navigation_counter.val == 7)
            {
            //
            // The site does not trigger the Navigating or DocumentCompleted events past this point, so set up an event handler to run when the target control is updated by ServiceBridge's AJAX code.
            //
            dom.doc.GetElementById("ajax_container").AttachEventHandler("onpropertychange",new EventHandler(ajax_container_PropertyChange));
            //
            // Maybe it will lighten the load on the remote site if we contract the date span from the default, which is at least a week.
            //
            dom.doc.GetElementById("DateFrom").SetAttribute("value",DateTime.Today.AddDays(-2).ToString("d"));
            //
            // Set the "Update every" dropdown to 15 minutes.  We'll be using the Refresh link for updates instead of the supplied timer, to prevent the site from considering us idle.
            //
            var update_every_dropdown = dom.doc.GetElementById("RunTime");
            update_every_dropdown.Children[1].SetAttribute("selected", "");
            update_every_dropdown.Children[4].SetAttribute("selected", "selected");
            update_every_dropdown.InvokeMember("onChange");
            //
            // Set the "Records per page" dropdown to 300.
            //
            var records_per_page_dropdown = dom.doc.GetElementById("nblock");
            records_per_page_dropdown.Children[0].SetAttribute("selected", "");
            records_per_page_dropdown.Children[4].SetAttribute("selected", "selected");
            records_per_page_dropdown.InvokeMember("onChange");
            master_browser_timer.Start();
            }
          //else if (navigation_counter.val > 7)
          //  {
          //  var cells = doc.GetElementsByTagName("TD");
          //  var incident_number = k.Safe(cells[4].InnerText,k.safe_hint_type.NUM);
          //  var nature = k.Safe(cells[16].InnerText,k.safe_hint_type.ALPHA_WORDS);
          //  //
          //  browser.GoBack();
          //  }
          }
        catch (Exception the_exception)
          {
          k.EscalatedException
            (
            the_exception:the_exception,
            user_identity_name:dom.doc.ActiveElement.InnerHtml
            );
          throw;
          }
        }
      }

    private void master_browser_timer_Elapsed(object sender, ElapsedEventArgs e)
      {
      k.EscalatedException(new Exception("TClass_ss_imagetrendelite.master_browser_timer_Elapsed"));
      Dispose();
      }

    //--
    //
    // PUBLIC
    //
    //--

    public class TClass_dom
      {
      public HtmlDocument doc {get; set;}
      }

    public TClass_dom dom;

    public TClass_ss_imagetrendelite(DateTime datetime_to_quit)
      {
      saved_datetime_to_quit = datetime_to_quit;
      //
      biz_cad_records = new TClass_biz_cad_records();
      biz_field_situations = new TClass_biz_field_situations();
      ss_emsbridge = new TClass_ss_emsbridge();
      //
      master_browser_timer = new System.Timers.Timer(interval:120000); // 2 minutes
      master_browser_timer.AutoReset = false;
      master_browser_timer.Elapsed += master_browser_timer_Elapsed_event_handler = new ElapsedEventHandler(master_browser_timer_Elapsed);
      //
      Cef.Initialize // Perform dependency check to make sure all relevant resources are in our output directory.
        (
        cefSettings:new CefSettings(),
        performDependencyCheck:true,
        browserProcessHandler:null
        );
      master_browser = new ChromiumWebBrowser();
      master_browser.RegisterJsObject("dom", new TClass_dom());
      master_browser.LoadingStateChanged += master_browser_LoadingStateChanged_event_handler = new EventHandler<LoadingStateChangedEventArgs>(master_browser_LoadingStateChanged);
      master_browser.Load(url:"https://vbems.emsbridge.com");
      master_browser_timer.Start();
      //
      master_browser_timer.Elapsed -= master_browser_timer_Elapsed_event_handler;
      }

    public void Dispose()
      {
      master_browser.LoadingStateChanged -= master_browser_LoadingStateChanged_event_handler;
      master_browser.Dispose();
      Cef.Shutdown();
      }

    }

  }
