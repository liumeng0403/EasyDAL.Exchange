﻿using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Join
{
    public sealed class WhereX
        : Operator, IQueryFirstOrDefaultX, IQueryListX, IQueryPagingListX, IQueryPagingListXO, ICountX
    {

        internal WhereX(Context dc)
            : base(dc)
        { }

        public async Task<long> CountAsync()
        {
            return await new CountXImpl(DC).CountAsync();
        }
        public async Task<long> CountAsync<F>(Expression<Func<F>> propertyFunc)
        {
            return await new CountXImpl(DC).CountAsync(propertyFunc);
        }

        /// <summary>
        /// 多表单条数据查询
        /// </summary>
        public async Task<M> QueryFirstOrDefaultAsync<M>()
            where M:class
        {
            return await new QueryFirstOrDefaultXImpl(DC).QueryFirstOrDefaultAsync<M>();
        }
        /// <summary>
        /// 多表单条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<VM>> columnMapFunc)
            where VM:class
        {
            return await new QueryFirstOrDefaultXImpl(DC).QueryFirstOrDefaultAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<M>> QueryListAsync<M>()
            where M:class
        {
            return await new QueryListXImpl(DC).QueryListAsync<M>();
        }
        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> columnMapFunc)
            where VM:class
        {
            return await new QueryListXImpl(DC).QueryListAsync<VM>(columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> QueryPagingListAsync<M>(int pageIndex, int pageSize)
            where M:class
        {
            return await new QueryPagingListXImpl(DC).QueryPagingListAsync<M>(pageIndex, pageSize);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<VM>> columnMapFunc)
            where VM:class
        {
            return await new QueryPagingListXImpl(DC).QueryPagingListAsync<VM>(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> QueryPagingListAsync<M>(PagingQueryOption option)
            where M:class
        {
            return await new QueryPagingListXOImpl(DC).QueryPagingListAsync<M>(option);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<VM>> columnMapFunc)
            where VM:class
        {
            return await new QueryPagingListXOImpl(DC).QueryPagingListAsync<VM>(option, columnMapFunc);
        }

    }
}
