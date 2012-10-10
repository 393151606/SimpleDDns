SimpleDDns
==========

Simple Dynamic DNS Client for DNSPod

简单Dnspod动态DNS客户端

* 支持本地内网多IP解析
* 支持外网IP解析
* 支持多个域名以及子域名同时解析

用法：

config.json是JSON格式的配置文件，例如：

    {
        "email": "",
        "password": "",
        "domains": [
            {
                "name": "",
                "records": [
                    {
                        "name": "",
                        "index": 0,
                        "ip": "intranet"
                    }
                ]
            }
        ]
    }

其中
* email和password是在DNSPod登录的用户名和密码。
* domains必须是已经在DNSPod中已经添加的域名，可以有多个。
* 每一条A记录(record)中
** name就是在DNSPod中填写的二级域名的名称
** ip是一个枚举，包括 internet 和 intranet
** 当有多个intranet ip时需要填写index

控制台输入：

    SimpleDDNS.exe help

可以查看本机所绑定的所有内网IP


配置好config.json之后，在控制台输入：

    SimpleDDNS.exe

就会自动解析配置的域名到本机

如果需要设置本机启动或者定时刷新，可以使用Windows的计划任务。

参考：
* DNSPod的 API https://www.dnspod.cn/Support/Api
* Fastjson http://www.codeproject.com/Articles/159450/fastJSON
* C#获取外网IP http://blog.163.com/tech_memo/blog/static/64436842201111292753730/