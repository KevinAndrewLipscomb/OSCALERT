using Class_ss;
using kix;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Web;

namespace Class_ss_broadcastify
  {
  public class TClass_ss_broadcastify : TClass_ss
    {

    private static class Static
      {
      }

    public TClass_ss_broadcastify() : base()
      {      
      }

    private bool Request_www_broadcastify_com_Login
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
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.broadcastify.com/login/");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

		    request.Accept = "text/html, application/xhtml+xml, */*";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Add("DNT", "1");
		    request.Headers.Set(HttpRequestHeader.Pragma, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"xyzzy=test; __utma=245121750.887757798.1413752601.1428276293.1428451907.139; __utmz=245121750.1418498841.34.2.utmcsr=radioreference.com|utmccn=(referral)|utmcmd=referral|utmcct=/; __qca=P0-248757097-1413752601012; __utmb=245121750.7.10.1428451907; __utmc=245121750; __utmt=1");

		    request.Method = "POST";

		    string postString = @"username=" + HttpUtility.UrlEncode(username) + "&password=" + HttpUtility.UrlEncode(password) + "&redirect=%2Fmanage%2Ffeed%2F14744%23ui-tabs-2&action=auth";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
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

    private bool Request_www_broadcastify_com_ManageFeed_Alert_Doupdatealerts
      (
      CookieContainer cookie_container,
      string alert,
      out HttpWebResponse response
      )
      {
	    response = null;

	    try
	      {
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.broadcastify.com/manage/");
        request.CookieContainer = cookie_container;
        request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

		    request.Accept = "text/html, application/xhtml+xml, */*";
        request.Referer = "http://www.broadcastify.com/manage/feed/14744";
		    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US");
		    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko";
		    request.ContentType = "application/x-www-form-urlencoded";
		    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
		    request.Headers.Add("DNT", "1");
		    request.Headers.Set(HttpRequestHeader.Pragma, "no-cache");
		    //request.Headers.Set(HttpRequestHeader.Cookie, @"__utma=245121750.887757798.1413752601.1428276293.1428451907.139; __utmz=245121750.1418498841.34.2.utmcsr=radioreference.com|utmccn=(referral)|utmcmd=referral|utmcct=/; __qca=P0-248757097-1413752601012; __utmc=245121750; bcfyuser1=ODEwMDA6a2V2aW5hbmRyZXdsaXBzY29tYjo0ZTdhMmM5MzI0YTE1NDJmNTIxZmIzYmVlMGQwZTkxMw%3D%3D");

		    request.Method = "POST";

		    string postString = @"alert=" + HttpUtility.UrlEncode(alert) + "&action=doUpdateAlerts&feedId=14744";
		    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString);
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

    private void Login
      (
      CookieContainer cookie_container
      )
      {
      HttpWebResponse response;
      if(!Request_www_broadcastify_com_Login
          (
          cookie_container:cookie_container,
          username:ConfigurationManager.AppSettings["broadcastify_username"],
          password:ConfigurationManager.AppSettings["broadcastify_password"],
          response:out response
          )
        )
        {
        throw new Exception("Request_www_broadcastify_com() returned FALSE.");
        }
      }

    internal void AddAlert
      (
      string alert
      )
      {
      var cookie_container = new CookieContainer();
      //
      Login(cookie_container:cookie_container);
      //
      HttpWebResponse response;
      if(Request_www_broadcastify_com_ManageFeed_Alert_Doupdatealerts
          (
          cookie_container:cookie_container,
          alert:alert,
          response:out response
          )
        )
      //then
        {
        k.SmtpMailSend
          (
          from:ConfigurationManager.AppSettings["sender_email_address"],
          to:ConfigurationManager.AppSettings["sender_email_address"],
          subject:"Response from Request_www_broadcastify_com_ManageFeed_Alert_Doupdatealerts()",
          message_string:ConsumedStreamOf(response)
          );
        }
      else
        {
        throw new Exception("Request_www_broadcastify_com_Manage_Alert_Doupdatealerts() returned FALSE.");
        }
      response.Close();
      }

    }
  }
