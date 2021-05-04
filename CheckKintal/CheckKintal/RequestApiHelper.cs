using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace CheckKintal.Services
{
    public class RequestApiHelper : IDisposable
    {
        private HttpRequest httpRequest;

        private string strDomain;

        private RequestParams keyValues;

        public RequestApiHelper()
        {
            Init();
        }

        public RequestApiHelper(string _strDomain)
        {
            Init();
            SetBaseDomain(_strDomain);
        }

        private void Init()
        {
            httpRequest = new HttpRequest();
            keyValues = new RequestParams();
        }

        public void SetBaseDomain(string _strDomain)
        {
            this.strDomain = _strDomain;
            httpRequest.BaseAddress = new Uri(strDomain);
        }

        public HttpRequest AddDefaultRequestHeader()
        {
            httpRequest.AddHeader(HttpHeader.Accept, "application/json, text/plain, */*");
            httpRequest.AddHeader(HttpHeader.AcceptLanguage, "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
            httpRequest.AddHeader("locale", "vi");

            return httpRequest;
        }

        public HttpRequest SetApiToken(string token)
        {
            httpRequest.AddHeader("Authorization", string.Format("Bearer {0}", token));
            httpRequest.Authorization = string.Format("Bearer {0}", token);

            return httpRequest;
        }

        public HttpRequest SetUserAgent(string userAgent)
        {
            string strUserAgent = string.IsNullOrEmpty(userAgent) ? Http.ChromeUserAgent() : userAgent;
            httpRequest.AddHeader(HttpHeader.UserAgent, strUserAgent);
            httpRequest.UserAgent = strUserAgent;

            return httpRequest;
        }

        public HttpRequest SetCookieHeader(string strCookie)
        {
            httpRequest.AddHeader("cookie", strCookie);

            return httpRequest;
        }

        private Uri GetUrl(string _strUrl)
        {
            Uri url = new Uri(new Uri(strDomain), _strUrl);
            return url;
        }

        public RequestParams AddRequestParam(string key, string value)
        {
            keyValues.Add(new KeyValuePair<string, string>(key, value));
            //httpRequest.AddParam(key, value);

            return keyValues;
        }

        public HttpRequest AddParam(string key, string value)
        {
            httpRequest.AddParam(key, value);

            return httpRequest;
        }

        private HttpResponse RunApi(HttpMethod method, string strUrl, RequestParams pairs = null)
        {
            switch(method)
            {
                case HttpMethod.GET:
                    return httpRequest.Get(GetUrl(strUrl), keyValues);
                case HttpMethod.POST:
                    return httpRequest.Post(GetUrl(strUrl), keyValues);
            }
            return httpRequest.Raw(method, strUrl);
        }

        public HttpResponse Get(string strUrl)
        {
            return RunApi(HttpMethod.GET, strUrl, keyValues);
        }

        public HttpResponse Post(string strUrl)
        {
            return RunApi(HttpMethod.POST, strUrl, keyValues);
        }

        private string UppercaseFirst(string s)
        {
            string str = s.ToLower();
            // Check for empty string.
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public void Dispose()
        {
            httpRequest.Dispose();
        }
    }
}
