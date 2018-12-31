﻿using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Join
{
    public sealed class OnX
        : Operator, IFirstOrDefaultX, IQueryListX, IPagingListX, IPagingListXO, ITopX, IQueryAllX,IExistX
    {

        internal OnX(Context dc)
            : base(dc)
        { }

        public async Task<List<M>> QueryAllAsync<M>()
            where M : class
        {
            return await new QueryAllXImpl(DC).QueryAllAsync<M>();
        }
        public async Task<List<T>> QueryAllAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new QueryAllXImpl(DC).QueryAllAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".FirstOrDefaultAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<M> FirstOrDefaultAsync<M>()
            where M : class
        {
            return await new FirstOrDefaultXImpl(DC).FirstOrDefaultAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".FirstOrDefaultAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new FirstOrDefaultXImpl(DC).FirstOrDefaultAsync<T>(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".ListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> QueryListAsync<M>()
            where M : class
        {
            return await new QueryListXImpl(DC).QueryListAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".ListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new QueryListXImpl(DC).QueryListAsync(columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> PagingListAsync<M>(int pageIndex, int pageSize)
            where M : class
        {
            return await new PagingListXImpl(DC).PagingListAsync<M>(pageIndex, pageSize);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
        {
            return await new PagingListXImpl(DC).PagingListAsync(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingList<M>> PagingListAsync<M>(PagingQueryOption option)
            where M : class
        {
            return await new PagingListXOImpl(DC).PagingListAsync<M>(option);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingList<T>> PagingListAsync<T>(PagingQueryOption option, Expression<Func<T>> columnMapFunc)
        {
            return await new PagingListXOImpl(DC).PagingListAsync(option, columnMapFunc);
        }

        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<M>> TopAsync<M>(int count)
            where M : class
        {
            return await new TopXImpl(DC).TopAsync<M>(count);
        }
        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<T>> columnMapFunc)
        {
            return await new TopXImpl(DC).TopAsync(count, columnMapFunc);
        }

        public async Task<bool> ExistAsync()
        {
            return await new ExistXImpl(DC).ExistAsync();
        }
    }
}
