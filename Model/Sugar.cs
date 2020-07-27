﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
 using NPinyin;
 using project_manage_api.Infrastructure;

 namespace project_manage_api.Model
{
    class Sugar
    {
        public static SqlSugarClient Db = new SqlSugarClient(
        new ConnectionConfig()
        {
            ConnectionString = "server=39.108.249.230;Port=35550;user id=user;password=AllUser;"
            +"database=yelioa;pooling=true;Convert Zero Datetime=True;charset=utf8;",
            DbType = DbType.MySql,//设置数据库类型
            IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
            InitKeyType = InitKeyType.Attribute, //从实体特性中读取主键自增列信息
            ConfigureExternalServices = new ConfigureExternalServices()
            {
                SqlFuncServices = getIdentifyMethod() //set ext method
            }
        });
        
        public static List<SqlFuncExternal> getIdentifyMethod()
        {
            var expMethods = new List<SqlFuncExternal>();
            expMethods.Add(new SqlFuncExternal()
            {
                UniqueMethodName = "getPinYinFirstLetter",
                MethodValue = (expInfo, dbType, expContext) =>
                {
                    if (dbType == DbType.MySql)
                        return expInfo.Args[0].MemberValue.ToString();
                    // return string.Format("CAST({0} AS VARCHAR(MAX))", expInfo.Args[0].MemberName);
                    else
                        throw new Exception("未实现");
                }
            });
            return expMethods;
        }
        
        public static string getPinYinFirstLetter(string str)
        {
            return Pinyin.GetInitials(str);
        }
    }
}
