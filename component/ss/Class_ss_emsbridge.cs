using Class_ss;
using kix;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Web;

namespace Class_ss_emsbridge
  {
  public class TClass_ss_emsbridge : TClass_ss
    {

    private static class Static
      {
      }

    public TClass_ss_emsbridge() : base()
      {      
      }

    private bool Request_vbems_emsbridge_com_ResourceAppsCaddispatchCaddispatchhistorydetail
      (
      CookieContainer cookie_container,
      string incident_id,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vbems.emsbridge.com/resource/apps/caddispatch/cad_dispatch_history_detail.cfm?IncidentID=" + incident_id);
        request.CookieContainer = cookie_container;
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

		    request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
    }

    internal string NatureOf
      (
      string incident_id,
      CookieContainer cookie_container
      )
      {
      var nature_of = k.EMPTY;
      HttpWebResponse response;
      if (Request_vbems_emsbridge_com_ResourceAppsCaddispatchCaddispatchhistorydetail(cookie_container, incident_id, out response))
        {
        try
          {
          var document_node = HtmlDocumentOf(ConsumedStreamOf(response)).DocumentNode;
          nature_of = document_node.SelectSingleNode("/html/center/body/table/tr[9]/td[2]").InnerText.Trim();
          }
        catch
          {
          nature_of = "-SCRAPE-ERROR-";
          }
        }
      return nature_of;
      }

    }
  }
