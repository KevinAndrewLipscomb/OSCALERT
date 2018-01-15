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
    // BEGIN code generated by pasting the received JSON data into the converter at http://json2csharp.com/ , and renaming RootObject to EmsCadList, and relaxing the accessibilities from private to internal.
    //
    //--
    //
    internal class Column
      {
      public string Name { get; set; }
      public string Value { get; set; }
      public string DataType { get; set; }
      }
    //
    internal class Record
      {
      public List<Column> Columns { get; set; }
      }
    //
    internal class EmsCadList
      {
      public object ErrorMessage { get; set; }
      public int TotalRecordCount { get; set; }
      public List<Record> Records { get; set; }
      public object ChildView { get; set; }
      public string RequestIdentifier { get; set; }
      }
    //
    //--
    //
    // END
    //
    //--

    private bool Request_www_imagetrendelite_com_Signin
      (
      string username,
      string password,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.imagetrendelite.com/Elite/AuthAPI/Authenticate?organizationId=VBEMS");
        Normalize(request);

		    request.Accept = "*/*";
		    request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
		    request.ContentType = "application/json";
		    request.Referer = "https://www.imagetrendelite.com/Elite/?organizationId=VBEMS";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
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
	    catch (Exception)
	    {
		    if(response != null) response.Close();
		    return false;
	    }

	    return true;
      }

    private bool Request_www_imagetrendelite_com_Load
      (
      string authorization_token,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	    {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.imagetrendelite.com/Elite/Organizationvbems/Agency00404/DynamicListAPIController/LoadDynamicListRecords?skip=0&pageSize=300&search=&comparisonType=STARTSWITH&sortColumn=UnitNotifiedByDispatch&sortAscending=false&viewID=910a358f-b03d-489a-bbe0-39d64ebc08cb&includeTotalRecordCount=false");
          // The following addition GET parameter appears not to be strictly necessary:  &RequestIdentifier=8513f56b-e453-4ae7-8ffd-be1a46975429
        Normalize(request);

		    request.Accept = "*/*";
		    request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
		    request.Headers.Set(HttpRequestHeader.Authorization, authorization_token);
		    request.Referer = "https://www.imagetrendelite.com/Elite/Organizationvbems/Agency00404/RunForm/CadList?startingFilter=ems";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";

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

    internal string AuthorizationTokenOf
      (
      string username,
      string password
      )
      {
      HttpWebResponse response;
      if(!Request_www_imagetrendelite_com_Signin
          (
          username:ConfigurationManager.AppSettings["vbemsbridge_username"],
          password:ConfigurationManager.AppSettings["vbemsbridge_password"],
          response:out response
          )
        )
        {
        throw new Exception("Request_www_imagetrendelite_com_Signin() returned FALSE.");
        }
      var authorization_token = response.Headers.Get("Authorization");
      HtmlDocumentOf(ConsumedStreamOf(response));
      return authorization_token;
      }
    
    internal EmsCadList CurrentEmsCadList
      (
      string authorization_token,
      StreamWriter log
      )
      {
      HttpWebResponse response;
      if(!Request_www_imagetrendelite_com_Load
          (
          authorization_token:authorization_token,
          response:out response
          )
        )
        {
        throw new Exception("Request_www_imagetrendelite_com_Load() returned FALSE.");
        }
      //
      EmsCadList current_ems_cad_list = null;
      var text = HtmlDocumentOf(ConsumedStreamOf(response)).DocumentNode.InnerText;
      if (!text.Contains("Server Error") || !text.Contains("Site Temporarily Offline"))
        {
        try
          {
          current_ems_cad_list = new JavaScriptSerializer(). Deserialize<EmsCadList>(text);
          }
        catch (Exception the_exception)
          {
          log.WriteLine(DateTime.Now.ToString("s") + "***From TClass_ss_imagetrendelite.Request_www_imagetrendelite_com_Load, got: " + text);
          throw the_exception;
          }
        }
      return current_ems_cad_list;
      }

    }
  }