﻿using HPC.DAL;
using MyDAL.Test.Entities;
using MyDAL.Test.Enums;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyDAL.Test
{
    public abstract class TestBase
    {
        protected string xx { get; set; }
        protected (List<string>, List<string>, List<string>) tuple { get; set; }

        protected WhereTestModel WhereTest
        {
            get
            {
                return new WhereTestModel
                {
                    CreatedOn = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30),
                    StartTime = Convert.ToDateTime("2018-08-23 13:36:58").AddDays(-30),
                    EndTime = DateTime.Now,
                    AgentLevelNull = null,
                    ContainStr2 = "~00-d-3-",
                    In_Array_枚举 = new AgentLevel?[]
                    {
                        AgentLevel.CityAgent,
                        AgentLevel.DistiAgent
                    },
                    In_Array_String = new string[]
                    {
                        "黄银凤",
                        "刘建芬"
                    }
                };
            }
        }

        /// <summary>
        /// MySQL
        /// </summary>
        protected XConnection Conn
        {
            get
            {
                return GetMySQLConnection();
            }
        }

        /// <summary>
        /// SqlServer 2012SP1+
        /// </summary>
        protected IDbConnection Conn2
        {
            get
            {
                return GetTSQLConnection_2012SP1Plus();
            }
        }

        private static XConnection GetMySQLConnection()
        {
            //
            // Nuget : Package : MySql.Data
            //
            // 不同版本 mysql 连接字符串一般如下：
            // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;"
            // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;"
            // "Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;allowPublicKeyRetrieval=true;"
            //
            var Conn = 
                new XConnection
                (
                    new MySqlConnection
                        ("Server=localhost; Database=MyDAL_TestDB; Uid=SkyUser; Pwd=Sky@4321;SslMode=none;allowPublicKeyRetrieval=true;")
                );
            return Conn;
        }
        private static IDbConnection GetTSQLConnection_2012SP1Plus()
        {
            //
            // Nuget : Package : System.Data.SqlClient
            //
            return
                new SqlConnection("Data Source=127.0.0.1;Initial Catalog=MyDAL_TestDB;User Id=sa;Password=1010;")
                .OpenDebug()  // 全局 debug 配置, 生产环境不要开启 
                              //.OpenAsync()  // 建议 每次新实例并打开,以获得更好的性能体验, 但是 用完要注意手动释放, 防止 连接池 资源耗尽!!!
                ;
        }

        protected Task None()
        {
            return default(Task);
        }
        protected void PrintLog(string msg)
        {
            Console.WriteLine(msg);
        }

        protected int StepProcess<M>(IEnumerable<M> modelList, int stepNum, Func<IEnumerable<M>, int> func)
        {
            //
            var result = 0;
            var total = modelList.Count();
            var limit = default(int);
            if (stepNum <= 0)
            {
                limit = 100;
            }
            else
            {
                limit = stepNum;
            }
            var start = 0;

            //
            do
            {
                var models = modelList.Skip(start).Take(limit);
                if (func != null)
                {
                    result += func(models);
                }
                if (start < (total - limit))
                {
                    start = start + limit;
                }
                else
                {
                    start = total;
                }
            }
            while (start < total);

            //
            return result;
        }
    }

}
