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
      string cookie,
      string incident_id,
      out HttpWebResponse response
      )
    {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vbems.emsbridge.com/resource/apps/caddispatch/cad_dispatch_history_detail.cfm?IncidentID=" + incident_id);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

		    request.Accept = "text/html, application/xhtml+xml, */*";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Add("DNT", "1");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
		    request.Headers.Set(HttpRequestHeader.Cookie,cookie);

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

    internal string NatureOf(string incident_id, string cookie)
      {
      var nature_of = k.EMPTY;
      HttpWebResponse response;
      if (Request_vbems_emsbridge_com_ResourceAppsCaddispatchCaddispatchhistorydetail(cookie, incident_id, out response))
        {
        nature_of = HtmlDocumentOf(ConsumedStreamOf(response)).DocumentNode.SelectSingleNode("/html/center/body/table/tr[9]/td[2]").InnerText.Trim();
        }
      return nature_of;
      }

    }
  }
