using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleDDNS.Clients.Dnspod
{
    public class Status
    {
        public string code { get; set; }
        public string message { get; set; }
        public string created_at { get; set; }

    }

    public class DomainInfo
    {

        public int domain_total { get; set; }
        public int all_total { get; set; }
        public int mine_total { get; set; }
        public int share_total { get; set; }
        public int ismark_total { get; set; }
        public int pause_total { get; set; }
    }

    public class Domain
    {

        public int id { get; set; }
        public string name { get; set; }
        public string grade { get; set; }
        public string status { get; set; }
        public string ext_status { get; set; }
        public string records { get; set; }
        public string group_id { get; set; }
        public string is_mark { get; set; }
        public string remark { get; set; }
        public string is_vip { get; set; }
        public string updated_on { get; set; }
        public string vip_start_at { get; set; }
        public string vip_end_at { get; set; }
    }

    public class DomainList
    {
        public Status status { get; set; }
        public DomainInfo info { get; set; }
        public List<Domain> domains { get; set; }

        public Dictionary<string, Domain> GetDomainDict()
        {
            Dictionary<string, Domain> dict = new Dictionary<string, Domain>();
            if (domains != null && domains.Count > 0)
            {
                for (int i = 0; i < domains.Count; i++)
                {
                    dict[domains[i].name] = domains[i];
                }
            }
            return dict; 
        }

        

    }

    public class RecordInfo
    {
        public string sub_domains;
        public string record_total;
    }

    public class Record
    {
        public string id { get; set; }
        public string name { get; set; }
        public string line { get; set; }
        public string type { get; set; }
        public string ttl { get; set; }
        public string value { get; set; }
        public string mx { get; set; }
        public string enabled { get; set; }
        public string monitor_status { get; set; }
        public string remark { get; set; }
        public string updated_on { get; set; }
    }

    public class RecordList
    {
        public Status status { get; set; }
        public RecordInfo info { get; set; }
        public List<Record> records { get; set; }

        public Dictionary<string, Record> GetRecordDict()
        {
            Dictionary<string, Record> dict = new Dictionary<string, Record>();
            if (records != null && records.Count > 0)
            {
                for (int i = 0; i < records.Count; i++)
                {
                    dict[records[i].name] = records[i];
                }
            }
            return dict;
        }
    }

    public class RecordUpdate
    {
        public Status status { get; set; }
        public RecordX record { get; set; }
    
    }

    public class RecordX
    { 
        
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}
