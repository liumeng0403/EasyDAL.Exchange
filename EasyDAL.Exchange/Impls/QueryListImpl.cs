﻿using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class QueryListImpl<M>
        : Impler, IQueryList<M>
        where M : class
    {
        internal QueryListImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> QueryListAsync()
        {
            return (await DC.DS.ExecuteReaderMultiRowAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryListAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions))).ToList();
        }

        public async Task<List<VM>> QueryListAsync<VM>()
            where VM:class
        {
            SelectMHandle<M, VM>();
            DC.DH.UiToDbCopy();
            return (await DC.DS.ExecuteReaderMultiRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryListAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions))).ToList();
        }

        public async Task<List<VM>> QueryListAsync<VM>(Expression<Func<M, VM>> columnMapFunc)
            where VM:class
        {
            SelectMHandle(columnMapFunc);
            DC.DH.UiToDbCopy();
            return (await DC.DS.ExecuteReaderMultiRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryListAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions))).ToList();
        }

        public async Task<List<M>> QueryListAsync(int topCount)
        {
            return await new TopImpl<M>(DC).TopAsync(topCount);
        }

        public async Task<List<VM>> QueryListAsync<VM>(int topCount) 
            where VM : class
        {
            return await new TopImpl<M>(DC).TopAsync<VM>(topCount);
        }

        public async Task<List<VM>> QueryListAsync<VM>(int topCount, Expression<Func<M, VM>> columnMapFunc) 
            where VM : class
        {
            return await new TopImpl<M>(DC).TopAsync<VM>(topCount, columnMapFunc);
        }
    }

    internal class QueryListXImpl
        : Impler, IQueryListX
    {
        internal QueryListXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> QueryListAsync<M>()
            where M:class
        {
            SelectMHandle<M>();
            DC.DH.UiToDbCopy();
            return (await DC.DS.ExecuteReaderMultiRowAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryListAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions))).ToList();
        }

        public async Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> columnMapFunc)
            where VM:class
        {
            SelectMHandle(columnMapFunc);
            DC.DH.UiToDbCopy();
            return (await DC.DS.ExecuteReaderMultiRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryListAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions))).ToList();
        }

        public async Task<List<M>> QueryListAsync<M>(int topCount) 
            where M : class
        {
            return await new TopXImpl(DC).TopAsync<M>(topCount);
        }

        public async Task<List<VM>> QueryListAsync<VM>(int topCount, Expression<Func<VM>> columnMapFunc) 
            where VM : class
        {
            return await new TopXImpl(DC).TopAsync<VM>(topCount, columnMapFunc);
        }
    }
}
