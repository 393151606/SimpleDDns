using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SimpleDDNS.Clients.Dnspod
{
    public class DnspodClient
    {
        const string USER_AGENT = "SimpleDDNS 1.0 (shootsoft@qq.com)";
        FastJSONClient client;

        public string login_email = string.Empty;
        public string login_password = string.Empty;
        //public string format = "json";
        public string lang = "cn";
        public string error_on_empty = "yes";

        public DnspodClient(string email, string password)
        {
            login_email = email;
            login_password = password;
            client = new FastJSONClient();
            client.UserAgent = USER_AGENT;
        }


        private StringBuilder GetCommonSB()
        {
            StringBuilder sb = new StringBuilder();
            AddParam(sb, "login_email", login_email);
            AddParam(sb, "login_password", login_password);
            AddParam(sb, "format", "json");
            AddParam(sb, "lang", lang);
            AddParam(sb, "error_on_empty", error_on_empty);
            return sb;
        }

        private static void AddParam(StringBuilder sb, string key, object value)
        {
            sb.Append(key);
            sb.Append("=");
            sb.Append(HttpUtility.UrlEncode(value.ToString()));
            sb.Append("&");

        }


        public DomainList GetDomains()
        {  
            StringBuilder sb = GetCommonSB();
            AddParam(sb, "type", "all");
            return client.Post<DomainList>("https://dnsapi.cn/Domain.List", sb.ToString());
        }

        public RecordList GetRecords(int domain_id)
        {
            StringBuilder sb = GetCommonSB();
            AddParam(sb, "domain_id", domain_id);
            return client.Post<RecordList>("https://dnsapi.cn/Record.List", sb.ToString());       
        }

        public void CreateRecord(int domain_id, string sub_domain, string ip)
        { 
            //"https://dnsapi.cn/Record.Create"
            string record_type = "A";
            string record_line="默认";
            string value = "127.0.0.1";
            int ttl = 3600;

        }

        public RecordUpdate UpdateRecord(int domain_id, string record_id, string sub_domain, string ip)
        {             
            StringBuilder sb = GetCommonSB();
            AddParam(sb, "domain_id", domain_id);
            AddParam(sb, "record_id", record_id);
            AddParam(sb, "sub_domain", sub_domain);
            AddParam(sb, "record_type", "A");
            AddParam(sb, "record_line", "默认");
            AddParam(sb, "value", ip);
            AddParam(sb, "ttl", 3600);
            return client.Post<RecordUpdate>("https://dnsapi.cn/Record.Modify", sb.ToString());       

        }
    }
}
