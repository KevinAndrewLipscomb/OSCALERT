using Class_ss;
using kix;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Class_ss_imagetrendelite
  {
  public class TClass_ss_imagetrendelite : TClass_ss
    {

    //--
    //
    // BEGIN code generated by pasting the received JSON data into a JSON-to-C# converter (like at https://jsonutils.com/), making sure the root object is named "EmsCadList", and making sure the accessibilities are at least "internal".
    //
    #pragma warning disable CA1034 // Nested types should not be visible
    #pragma warning disable CA2227 // Collection properties should be read only
    //
    //--
    //
    public class Column
      {
      public string Name { get; set; }
      public string Value { get; set; }
      public string DataType { get; set; }
      }

    public class Record
      {
      public IList<Column> Columns { get; set; }
      }

    public class EmsCadList
      {
      public object ErrorMessage { get; set; }
      public int TotalRecordCount { get; set; }
      public IList<Record> Records { get; set; }
      public object ChildView { get; set; }
      public string RequestIdentifier { get; set; }
      public int NewDynamicListEngineUsed { get; set; }
      }
    //
    //--
    //
    #pragma warning restore CA2227 // Collection properties should be read only
    #pragma warning restore CA1034 // Nested types should not be visible
    //
    // END
    //
    //--

    //--
    //
    // BEGIN code generated initially by Fiddler extension "Request to Code"
    //
    #pragma warning disable CA1031 // Do not catch general exception types
    #pragma warning disable CA2234 // Pass system uri objects instead of strings
    //
    //--

    private bool Request_www_imagetrendelite_com_Signin
      (
      CookieContainer cookie_container,
      string username,
      string password,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vbems.imagetrendelite.com/Elite/AuthAPI/Authenticate?organizationId=VBEMS");
            NormalizeWithCookie(request,cookie_container);

		    request.Accept = "*/*";
		    request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
		    request.ContentType = "application/json";
		    request.Referer = "https://vbems.imagetrendelite.com/Elite/?organizationId=VBEMS";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");

		    request.Method = "POST";
		    request.ServicePoint.Expect100Continue = false;

		    string body = k.EMPTY
        + "{"
        + k.QUOTE + "identifier" + k.QUOTE + ":" + k.QUOTE + username + k.QUOTE + ","
        + k.QUOTE + "passkey" + k.QUOTE + ":" + k.QUOTE + password + k.QUOTE + ","
        + k.QUOTE + "organizationId" + k.QUOTE + ":" + k.QUOTE + "vbems" + k.QUOTE
        + "}";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
		    request.ContentLength = postBytes.Length;
		    Stream stream = request.GetRequestStream();
		    stream.Write(postBytes, 0, postBytes.Length);
		    stream.Close();

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
      }

    private bool Request_www_imagetrendelite_com_Get1
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/DynamicListAPIController/GetDynamicListViews?dynamicListViewTypeName=ViewAllEMSCADList");
        NormalizeWithCookie(request,cookie_container);

		    request.Accept = "*/*";
		    request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
		    request.Referer = "https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/RunForm/CadList?startingFilter=ems";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
      }

    private bool Request_www_imagetrendelite_com_Get2
      (
      CookieContainer cookie_container,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/DynamicListAPIController/GetDynamicListViewByID?dynamicListViewModelID=910a358f-b03d-489a-bbe0-39d64ebc08cb");
        NormalizeWithCookie(request,cookie_container);

		    request.Accept = "*/*";
		    request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
		    request.Referer = "https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/RunForm/CadList?startingFilter=ems";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
		    else return false;
	    }
	    catch
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
      }

    private bool Request_www_imagetrendelite_com_Load
      (
      CookieContainer cookie_container,
      string request_identifier,
      StreamWriter log,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/DynamicListAPIController/LoadDynamicListRecords?skip=0&pageSize=200&search=&comparisonType=STARTSWITH&sortColumn=UnitNotifiedByDispatch&sortAscending=false&viewID=910a358f-b03d-489a-bbe0-39d64ebc08cb&includeTotalRecordCount=false&RequestIdentifier=" + request_identifier);
        NormalizeWithCookie(request,cookie_container);

		    request.Accept = "*/*";
		    request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
		    request.Referer = "https://vbems.imagetrendelite.com/Elite/Organizationvbems/Agency00404/RunForm/CadList?startingFilter=ems";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");

        request.Timeout = int.Parse(ConfigurationManager.AppSettings["Request_www_imagetrendelite_com_Load_timeout_milliseconds"]);

		    response = (HttpWebResponse)request.GetResponse();
	    }
	    catch (WebException e)
	    {
		    if (e.Status == WebExceptionStatus.ProtocolError)
          {
          response = (HttpWebResponse)e.Response;
          }
		    else
          {
          log.WriteLine(DateTime.Now.ToString("s") + " TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load encountered a WebException: " + e.ToString() + k.NEW_LINE);
          return false;
          }
	    }
	    catch (Exception e)
	    {
		    if (response != null) response.Close();
        log.WriteLine(DateTime.Now.ToString("s") + " TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load encountered a non-web Exception: " + e.ToString() + k.NEW_LINE);
		    return false;
	    }

	    return true;
      }

    //--
    //
    #pragma warning restore CA1031 // Do not catch general exception types
    #pragma warning restore CA2234 // Pass system uri objects instead of strings
    //
    // END code generated initially by Fiddler extension "Request to Code"
    //
    //--

    internal void Login
      (
      string username,
      string password,
      CookieContainer cookie_container
      )
      {
      HttpWebResponse response;
      if(!Request_www_imagetrendelite_com_Signin
          (
          cookie_container:cookie_container,
          username:username,
          password:password,
          response:out response
          )
        )
        {
        throw new Exception("Request_www_imagetrendelite_com_Signin() returned FALSE.");
        }
      HtmlDocumentOf(ConsumedStreamOf(response));
      }

    internal void Nudge(CookieContainer cookie_container)
      {
      HttpWebResponse response;
      if(!Request_www_imagetrendelite_com_Get1
          (
          cookie_container:cookie_container,
          response:out response
          )
        )
        {
        throw new Exception("Request_www_imagetrendelite_com_Get1() returned FALSE.");
        }
      HtmlDocumentOf(ConsumedStreamOf(response));
      //
      if(!Request_www_imagetrendelite_com_Get2
          (
          cookie_container:cookie_container,
          response:out response
          )
        )
        {
        throw new Exception("Request_www_imagetrendelite_com_Get2() returned FALSE.");
        }
      HtmlDocumentOf(ConsumedStreamOf(response));
      }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types")]
    internal EmsCadList CurrentEmsCadList
      (
      CookieContainer cookie_container,
      string request_identifier,
      StreamWriter log
      )
      {
      HttpWebResponse response;
      if(!Request_www_imagetrendelite_com_Load
          (
          cookie_container:cookie_container as CookieContainer,
          request_identifier:request_identifier,
          log:log,
          response:out response
          )
        )
        {
        throw new Exception("Request_www_imagetrendelite_com_Load() returned FALSE.");
        }
      //
      EmsCadList current_ems_cad_list = null;
      var text = HtmlDocumentOf(ConsumedStreamOf(response)).DocumentNode.InnerText;
      if (text.Contains("Server Error"))
        {
        log.WriteLine(DateTime.Now.ToString("s") + "***From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: SERVER ERROR");
        }
      else if (text.Contains("Site Temporarily Offline"))
        {
        log.WriteLine(DateTime.Now.ToString("s") + "***From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: SITE TEMPORARILY OFFLINE");
        }
      else if (text.Contains("Service Unavailable"))
        {
        log.WriteLine(DateTime.Now.ToString("s") + "***From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: SERVICE UNAVAILABLE");
        }
      else if (text.Contains("Forbidden Access"))
        {
        log.WriteLine(DateTime.Now.ToString("s") + "***From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: FORBIDDEN ACCESS");
        }
      else
        {
        try
          {
          current_ems_cad_list = new JavaScriptSerializer().Deserialize<EmsCadList>(text);
          if (current_ems_cad_list.ErrorMessage != null)
            {
            log.WriteLine(DateTime.Now.ToString("s") + " TClass_ss_imagetrendelite.CurrentEmsCadList got an EmsCadList with ErrorMessage: " + current_ems_cad_list.ErrorMessage.ToString() + k.NEW_LINE);
            }
          }
        catch (Exception the_exception)
          {
          log.WriteLine(DateTime.Now.ToString("s") + "***From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: UNHANDLED " + the_exception.ToString() + k.NEW_LINE + text);
          }
        }
      return current_ems_cad_list;
      }

    }
  }
