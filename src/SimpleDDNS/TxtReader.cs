using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimpleDDNS
{
    /// <summary>
    /// from http://blog.sina.com.cn/s/blog_6e51df7f0100so8z.html
    /// </summary>
    public class TxtReader
    {
        /// <summary>
        /// 自动判断编码读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Read(string path)
        {
            return File.ReadAllText(path, TxtReader.GetEncoding(path));
        }

        /// <summary>
        /// 自动判断编码读取行文本
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path, TxtReader.GetEncoding(path));
        }


        ///<summary>
        /// C#读取文件时自动判断编码函数
        ///</summary>
        ///<param name="path">需要判断编码方式文件的物理路径如"C:\file\file.txt"</param>
        ///<returns></returns>
        public static System.Text.Encoding GetEncoding(string path)
        {
            FileStream fs =new FileStream(path, FileMode.Open,FileAccess.Read);
            System.Text.Encoding r = GetEncoding(fs);
            fs.Close();
            return r;
        }

        public static System.Text.Encoding GetEncoding(FileStream fs)
        {
            BinaryReader r =new BinaryReader(fs,System.Text.Encoding.Default);
            byte[] ss = r.ReadBytes(4);
            r.Close();
            //编码类型 Coding=编码类型.ASCII;   
            if (ss[0] <=0xEF)
            {
                if (ss[0] ==0xEF&& ss[1] ==0xBB&& ss[2] ==0xBF)
                {
                    return System.Text.Encoding.UTF8;
                }
                else if (ss[0] ==0xFE&& ss[1] ==0xFF)
                {
                    return System.Text.Encoding.BigEndianUnicode;
                }
                else if (ss[0] ==0xFF&& ss[1] ==0xFE)
                {
                    return System.Text.Encoding.Unicode;
                }
                else
                {
                    return System.Text.Encoding.Default;
                }
            }
            else
            {
                return System.Text.Encoding.Default;
            }
        }
    }
}
