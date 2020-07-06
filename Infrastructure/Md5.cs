﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace project_manage_api.Infrastructure
{
    public class Md5
    {
        public static string Encrypt(string str)
        {

            string pwd = String.Empty;

            MD5 md5 = MD5.Create();

            // 编码UTF8/Unicode　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            // 转换成字符串
            for (int i = 0; i < s.Length; i++)
            {
                //格式后的字符是小写的字母
                //如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X");

            }

            return pwd;
        }

        /// <summary>
        /// 防止SQL注入方法
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string avoidSqlInjection(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return inputString;
            }
            inputString = inputString.Trim();
            inputString = inputString.Replace("'", "");
            inputString = inputString.Replace(";--", "");
            inputString = inputString.Replace("--", "");
            inputString = inputString.Replace("=", "");
            //and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|count|*|%|union 等待关键字过滤
            //不要忘记为你的用户名框，密码框设定 允许输入的最多字符长度 maxlength的值哦，这样他们就无法编写太长的东西来再次拼成第一次过滤掉的关键字 如 oorr一次replace过滤后又成了 or 喔。
            inputString = inputString.Replace("and", "");
            inputString = inputString.Replace("exec", "");
            inputString = inputString.Replace("insert", "");
            inputString = inputString.Replace("select", "");
            inputString = inputString.Replace("delete", "");
            inputString = inputString.Replace("update", "");
            inputString = inputString.Replace("chr", "");
            inputString = inputString.Replace("mid", "");
            inputString = inputString.Replace("master", "");
            inputString = inputString.Replace("or", "");
            inputString = inputString.Replace("truncate", "");
            inputString = inputString.Replace("char", "");
            inputString = inputString.Replace("declare", "");
            inputString = inputString.Replace("join", "");
            inputString = inputString.Replace("count", "");
            inputString = inputString.Replace("*", "");
            inputString = inputString.Replace("%", "");
            inputString = inputString.Replace("union", "");
            return inputString;
        }
    }
}
