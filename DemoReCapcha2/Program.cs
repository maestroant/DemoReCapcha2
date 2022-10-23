using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DemoReCaptcha2;
using static System.Net.WebRequestMethods;

namespace DemoReCaptcha2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // get demo recaptcha page
            CookieContainer cookieContainer = new CookieContainer();
            GetRequest getRequest = new GetRequest("https://www.google.com/recaptcha/api2/demo");
            getRequest.Accept = "*/*";
            getRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
            getRequest.Host = "www.google.com";
            //getRequest.Proxy = new WebProxy("127.0.0.1:8888");    // debug with Fiddler 4
            getRequest.Run(cookieContainer);

            // sending site key and link to xevil server
            string k = TextParse.SubString(getRequest.Response, "data-sitekey=\"", "\"");

            XevilReCaptcha2 xe = new XevilReCaptcha2("179.143.32.93", k, "https://www.google.com/recaptcha/api2/demo");
            string captchaResponse = xe.Get();
  
            PostRequest postRequest = new PostRequest("https://www.google.com/recaptcha/api2/demo");
            postRequest.Accept = "*/*";
            postRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
            postRequest.Host = "www.google.com";
            postRequest.Data = "g-recaptcha-response=" + captchaResponse;
            //postRequest.Proxy = new WebProxy("127.0.0.1:8888");   // debug with Fiddler 4
            postRequest.Run(cookieContainer);

            // result output. If we see "Verification Success... Hooray!" it means everything is ok! )
            Console.WriteLine(postRequest.Response);
            Console.ReadKey();
        }
    }
}
