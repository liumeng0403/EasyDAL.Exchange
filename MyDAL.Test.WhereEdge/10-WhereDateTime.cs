﻿using MyDAL.Test.Entities.EasyDal_Exchange;
using System;
using System.Threading.Tasks;
using Xunit;
using Yunyong.DataExchange;

namespace MyDAL.Test.WhereEdge
{
    public class _10_WhereDateTime:TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = "";

            var date = DateTime.Parse("2018-08-16 12:03:47.225916");
            var res1 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd"))
                .QueryListAsync();
            Assert.True(res1.Count == 28619);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /**************************************************************************************************************************************/
            
            var xx2 = "";
            
            var res2 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn.ToString("yyyy-MM") == date.ToString("yyyy-MM"))
                .QueryListAsync();
            Assert.True(res2.Count == 28619);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /**************************************************************************************************************************************/

            var xx3 = "";

            var res3 = await Conn
                .Selecter<Agent>()
                .Where(it => it.CreatedOn.ToString("yyyy") == date.ToString("yyyy"))
                .QueryListAsync();
            Assert.True(res3.Count == 28619);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /**************************************************************************************************************************************/

            var xx4 = "";

            var res4 = await Conn
                .Selecter<Agent>()
                .Where(it => it.ActivedOn!=null&&it.ActivedOn.Value.ToString("yyyy-MM-dd")==DateTime.Parse("2018-08-19 12:05:45.560984").ToString("yyyy-MM-dd"))
                .QueryListAsync();
            Assert.True(res4.Count == 554);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /**************************************************************************************************************************************/

            var xx = "";
        }
    }
}
