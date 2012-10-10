using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using log4net;
using SimpleDDNS.Clients.Dnspod;
using System.Net;
using System.IO;
using System.Threading;

namespace SimpleDDNS
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length>0 && args[0] == "help")
            {
                GetIP(10000);
            }
            else
            {
                SetDns();
            }
            
        }

        #region GetIP
        private static string GetIP(int index)
        {
            if (index < 0)
            {
                string strUrl = "http://www.ip138.com/ip2city.asp"; //获得IP的网址了   
                Uri uri = new Uri(strUrl);
                WebRequest wr = WebRequest.Create(uri);
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站的数据   
                int i = all.IndexOf("[") + 1;
                string tempip = all.Substring(i, 15);
                string ip = tempip.Replace("]", "").Replace(" ", "");
                return ip;
            }
            else
            {
                System.Net.IPAddress[] addressList =
                        Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                if (addressList != null && index < addressList.Length)
                {
                    return addressList[index].ToString();
                }
                else
                {
                    for (int i = 0; i < addressList.Length; i++)
                    {
                        Console.WriteLine("{0} {1}", i, addressList[i].ToString());
                    }
                    Console.WriteLine("按任意键继续...");
                    Console.Read();
                    return "127.0.0.1";
                }
            }
        } 
        #endregion

        #region SetDns
        private static void SetDns()
        {
            ILog _log = LogManager.GetLogger(typeof(Program));
            LogManager.Configure("logs\\log.txt", 1024, false);
            int success = 0;
            try
            {
       
                Config cfg = Config.Load("config.json");
                DnspodClient client = new DnspodClient(cfg.email, cfg.password);
                DomainList list = client.GetDomains();
                Dictionary<string, SimpleDDNS.Clients.Dnspod.Domain> domainDict = list.GetDomainDict();
                if (cfg.domains != null)
                {
                    for (int i = 0; i < cfg.domains.Count; i++)
                    {
                        if (cfg.domains[i].records != null && cfg.domains[i].records.Count > 0 &&
                            domainDict.ContainsKey(cfg.domains[i].name))
                        {
                            SimpleDDNS.Clients.Dnspod.Domain d = domainDict[cfg.domains[i].name];
                            RecordList rlist = client.GetRecords(d.id);
                            if (rlist != null && rlist.records != null)
                            {
                                Dictionary<string, SimpleDDNS.Clients.Dnspod.Record> records = rlist.GetRecordDict();

                                for (int j = 0; j < cfg.domains[i].records.Count; j++)
                                {
                                    Record r = cfg.domains[i].records[j];
                                    if (records.ContainsKey(r.name))
                                    {
                                        string value = GetIP(r.index);
                                        if (r.ip == IPType.internet)
                                        {
                                            value = GetIP(-1);
                                        }
                                        try
                                        {
                                            RecordUpdate ru = client.UpdateRecord(d.id, records[r.name].id, r.name, value);
                                            if (ru != null && ru.status != null && ru.status.code == "1")
                                            {
                                                string msg = string.Format("成功解析{0}.{1} -> {2}", r.name, d.name, value);
                                                Console.WriteLine(msg);
                                                _log.Info(msg);
                                                success++;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            _log.Error(ex);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            Console.WriteLine("成功解析{0}个域名！ 3秒钟后退出...", success);
            Thread.Sleep(3000);
        } 
        #endregion

        #region Test
        private static void Test()
        {
            DnspodClient client = new DnspodClient("shootsoft@qq.com", "");
            fastJSON.JSON.Instance.Parameters.EnableAnonymousTypes = true;
            Debug.WriteLine(fastJSON.JSON.Instance.ToJSON(client.GetDomains()));
            Debug.WriteLine(fastJSON.JSON.Instance.ToJSON(client.GetRecords(0)));
        } 
        #endregion
    }
}
