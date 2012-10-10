using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace SimpleDDNS
{
    public enum IPType 
    {
        internet = 0,
        intranet = 1
    }

    public class Record
    {
        public string name { get; set; }
        public IPType ip { get; set; }
        public int index { get; set; }

        public Record() { }
        public Record(string name)
        {
            this.name = name;
        }
        public Record(string name, IPType ip, int index)
        {
            this.name = name;
            this.index = index;
            this.ip = ip;
        }
    }



    public class Domain
    {
        public string name { get; set; }
        public List<Record> records { get; set; }

        public Domain() 
        {
            records = new List<Record>();
        }

        public Domain(string name)
        {
            this.name = name;
            records = new List<Record>();
        }

    }

   // public class Domain : Dictionary<string, Records> { }

    public class Config
    {
        public string email { get; set; }
        public string password { get; set; }
        public List<Domain> domains { get; set; }
        public Config()
        {
            domains = new List<Domain>();
        }

        public static Config Load(string path)
        {
            Config cfg = null;
            if (File.Exists(path))
            {
                cfg = fastJSON.JSON.Instance.ToObject<Config>(File.ReadAllText(path, Encoding.UTF8));
            }
            if (cfg == null)
            {
                cfg = new Config();
            }
            return cfg;
        }
    }
}
