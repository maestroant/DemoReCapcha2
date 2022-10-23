using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DemoReCaptcha2;

namespace DemoReCaptcha2
{
    internal class XevilReCaptcha2
    {
        public string Host { get; set; }
        public string Googlekey { get; set; }
        public string Url { get; set; }
        public int Timeout { get; set; }
        public bool Visible { get; set; }

        // host - The server on which Xevil is installed, url - the site on which we pass the captcha
        public XevilReCaptcha2(string host, string googlekey, string url, bool visible = true, int timeout = 60000)
        {
            Host = host;
            Googlekey = googlekey;
            Url = url;
            Timeout = timeout;
            Visible = visible;
        }

        public string Get()
        {
            string url = string.Format("http://{0}/in.php?method=userrecaptcha&googlekey={1}&pageurl={2}={3}",
                Host, Googlekey, Url, Convert.ToInt32(Visible));

            CookieContainer cookieContainer = new CookieContainer();
            GetRequest getRequest = new GetRequest(url);

            getRequest.Accept = "*/*";
            getRequest.Host = Host;
            getRequest.Run(cookieContainer);

            if (string.IsNullOrEmpty(getRequest.Response))
            {
                // Xevil connection error
                return null;
            }

            if ((getRequest.Response[0] != 'O') || (getRequest.Response[1] != 'K'))
            {
                // error with Xevil connection error name in getRequest.Response
                return null;
            }

            string id = TextParse.SubString(getRequest.Response, "|");

            // formation of a query string. Xevil has no documentation for its API.
            url = string.Format("http://{0}/res.php?action=get&id={1}", Host, id);
            GetRequest getRequest2 = new GetRequest(url);
            getRequest2.Accept = "*/*";
            getRequest2.Host = Host;
            getRequest2.Run(cookieContainer);

            int i = 0;
            while (getRequest2.Response == "CAPCHA_NOT_READY")
            {
                Thread.Sleep(5000);
                getRequest2.Run(cookieContainer);
                if (i > Timeout) break;
                i += 5000;
            }

            if ((getRequest.Response[0] != 'O') || (getRequest.Response[1] != 'K'))
            {
                // Xevil connection error
                return null;
            }

            return TextParse.SubString(getRequest2.Response, "OK|");
        }
    }
}
