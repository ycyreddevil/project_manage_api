﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
 using project_manage_api.Infrastructure;

 namespace project_manage_api.Model
{
    public class SugarDBContext<T> where T : class, new()
    {
        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<T> SimpleDb { get { return new SimpleClient<T>(Db); } }//用来处理T表的常用操作

        public SugarDBContext()
        {
            // var expMethods = new List<SqlFuncExternal>();
            // expMethods.Add(new SqlFuncExternal()
            // {
            //     UniqueMethodName = "IsEqualEngAndChinese",
            //     MethodValue = (expInfo, dbType, expContext) =>
            //     {
            //         if (dbType == DbType.MySql)
            //             return StringTools.IsEqualEngAndChinese(expInfo.Args[0].MemberName);
            //         throw new Exception("未实现");
            //     }
            // });
            
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=39.108.249.230;Port=35550;user id=user;password=AllUser;"
                + "database=project_manage;pooling=true;Convert Zero Datetime=True;charset=utf8;",
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了,
                // ConfigureExternalServices = new ConfigureExternalServices()
                // {
                //     SqlFuncServices = expMethods
                // }
            });

            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
        }

        public virtual List<T> GetList()
        {
            return SimpleDb.GetList();
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleDb.GetList(whereExpression);
        }

        public virtual bool Insert(T obj)
        {
            return SimpleDb.Insert(obj);
        }

        public virtual bool BatchInsert(List<T> objs)
        {
            //if()
            //for(int i=0;i<objs.Count;i++)
            //{


            //}
            return SimpleDb.InsertRange(objs);
        }

        public virtual bool Delete(dynamic id)
        {
            return SimpleDb.Delete(id);
        }

        public virtual bool Delete(dynamic[] id)
        {
            return SimpleDb.DeleteByIds(id);
        }

        public virtual bool Update(T obj)
        {
            return SimpleDb.Update(obj);
        }

        public virtual bool UpdateRange(List<T> obj)
        {
            return SimpleDb.UpdateRange(obj);
        }
    }
}
