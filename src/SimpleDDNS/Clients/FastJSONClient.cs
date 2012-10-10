using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using fastJSON;
using System.IO;

namespace SimpleDDNS
{
    public class FastJSONClient
    {
        const string JSONTYPEUTF8 = "application/x-www-form-urlencoded";       

        HttpWebRequest request;
        CookieContainer cookie;
        public Encoding ContentEncoding { get; set; }
        public string UserAgent { get; set; }

        public FastJSONClient()
        {
            cookie = new CookieContainer();
            ContentEncoding = Encoding.UTF8;
        }

        public T Get<T>(string url)
        {
            T result = default(T);
            try
            {
                CreateRequest(url, "GET");
                WebResponse response = request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), ContentEncoding))
                {
                    result = JSON.Instance.ToObject<T>(sr.ReadToEnd());
                }                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);    
            }
            return result;
        }

        public T Post<T>(string url, string body)
        {
            T result = default(T);
            try
            {
                result = JSON.Instance.ToObject<T>(PostTxt(url, body, false));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }


        public T Post<T>(string url, object arg, bool encoded)
        {
            T result = default(T);
            try
            {
                result = JSON.Instance.ToObject<T>(PostTxt(url, arg, encoded));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }

        public T Post<T>(string url)
        {
            T result = default(T);
            try
            {
                result = JSON.Instance.ToObject<T>(PostTxt(url, string.Empty, false));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }

        public string PostTxt(string url, object arg, bool encoded)
        {
            string result = null;
            try
            {
                CreateRequest(url, "POST");
                string body = encoded ? JSON.Instance.ToJSON(arg) : arg.ToString();
                byte[] buf = ContentEncoding.GetBytes(body);
                request.ContentLength = buf.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(buf, 0, buf.Length);
                }
                //request
                WebResponse response = request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), ContentEncoding))
                {
                    result = sr.ReadToEnd(); 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }

        private void CreateRequest(string url, string method)
        {
            request = WebRequest.Create(url) as HttpWebRequest;
            request.CookieContainer = cookie;
            request.ContentType = JSONTYPEUTF8;
            if (!string.IsNullOrEmpty(UserAgent)) request.UserAgent = UserAgent;
            if (!string.IsNullOrEmpty(method)) request.Method = method;
        }
    
    }
}
