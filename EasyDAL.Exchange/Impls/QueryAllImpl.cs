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
    internal class QueryAllImpl<M>
        : Impler, IQueryAll<M>
        where M : class
    {
        internal QueryAllImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> QueryAllAsync()
        {
            return (await DC.DS.ExecuteReaderMultiRowAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryAllAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions))).ToList();
        }

        public async Task<List<VM>> QueryAllAsync<VM>()
            where VM : class
        {
            return (await DC.DS.ExecuteReaderMultiRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryAllAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions))).ToList();
        }

        public async Task<List<F>> QueryAllAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.AddConditions(DC.EH.FuncMFExpression(propertyFunc)[0]);
            DC.DH.UiToDbCopy();
            return (await DC.DS.ExecuteReaderSingleColumnAsync<M,F>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryAllAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions),
                propertyFunc.Compile())).ToList();
        }
    }
}
