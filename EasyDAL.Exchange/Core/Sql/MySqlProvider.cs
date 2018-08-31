﻿
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using EasyDAL.Exchange.Extensions;
using EasyDAL.Exchange.DynamicParameter;
using EasyDAL.Exchange.Core.Sql;
using System.Data;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.AdoNet;
using Rainbow.Core;

namespace EasyDAL.Exchange.Core
{
    internal class MySqlProvider
    {
        private MySqlProvider() { }

        internal MySqlProvider(DbContext dc)
        {
            DC = dc;
            DC.SqlProvider = this;
        }

        internal DbContext DC { get; private set; }

        private string LikeStrHandle(DicModel dic)
        {
            var name = dic.Param;
            var value = dic.Value;
            if (!value.Contains("%")
                && !value.Contains("_"))
            {
                return $"CONCAT('%',@{name},'%')";
            }
            else if ((value.Contains("%") || value.Contains("_"))
                && !value.Contains("/%")
                && !value.Contains("/_"))
            {
                return $"@{name}";
            }
            else if (value.Contains("/%")
                || value.Contains("/_"))
            {
                return $"@{name} escape '/' ";
            }

            throw new Exception(value);
        }

        internal string GetJoins()
        {
            var str = string.Empty;

            foreach (var item in DC.Conditions)
            {
                if (item.Crud != CrudTypeEnum.Join)
                {
                    continue;
                }

                switch (item.Action)
                {
                    case ActionEnum.From:
                        str += $" `{item.TableOne}` as {item.AliasOne} ";
                        break;
                    case ActionEnum.InnerJoin:
                    case ActionEnum.LeftJoin:
                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} `{item.TableOne}` as {item.AliasOne} ";
                        break;
                    case ActionEnum.On:
                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} {item.AliasOne}.`{item.KeyOne}`={item.AliasTwo}.`{item.KeyTwo}` ";
                        break;
                }
            }

            return str;
        }

        internal string GetWheres()
        {
            if (!DC.Conditions.Any(it => it.Action == ActionEnum.Where)
                && !DC.Conditions.Any(it => it.Action == ActionEnum.And)
                && !DC.Conditions.Any(it => it.Action == ActionEnum.Or))
            {
                throw new Exception("没有设置任何条件!");
            }

            var str = string.Empty;

            foreach (var item in DC.Conditions)
            {
                switch (item.Action)
                {
                    case ActionEnum.Where:
                    case ActionEnum.And:
                    case ActionEnum.Or:
                        switch (item.Option)
                        {
                            case OptionEnum.Equal:
                            case OptionEnum.LessThan:
                            case OptionEnum.LessThanOrEqual:
                            case OptionEnum.GreaterThan:
                            case OptionEnum.GreaterThanOrEqual:
                                switch (item.Crud)
                                {
                                    case CrudTypeEnum.Join:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} {item.AliasOne}.`{item.KeyOne}`{item.Option.ToEnumDesc<OptionEnum>()}@{item.Param} ";
                                        break;
                                    case CrudTypeEnum.Delete:
                                    case CrudTypeEnum.Update:
                                    case CrudTypeEnum.Query:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} `{item.KeyOne}`{item.Option.ToEnumDesc<OptionEnum>()}@{item.Param} ";
                                        break;
                                }
                                break;
                            case OptionEnum.Like:
                                switch (item.Crud)
                                {
                                    case CrudTypeEnum.Join:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} {item.AliasOne}.`{item.KeyOne}`{item.Option.ToEnumDesc<OptionEnum>()}{LikeStrHandle(item)} ";
                                        break;
                                    case CrudTypeEnum.Delete:
                                    case CrudTypeEnum.Update:
                                    case CrudTypeEnum.Query:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} `{item.KeyOne}`{item.Option.ToEnumDesc<OptionEnum>()}{LikeStrHandle(item)} ";
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }

            return str;
        }

        internal string GetUpdates()
        {
            if (!DC.Conditions.Any(it => it.Action == ActionEnum.Set)
                && !DC.Conditions.Any(it => it.Action == ActionEnum.Change))
            {
                throw new Exception("没有设置任何要更新的字段!");
            }

            var list = new List<string>();

            foreach (var item in DC.Conditions)
            {
                switch (item.Action)
                {
                    case ActionEnum.Set:
                    case ActionEnum.Change:
                        switch (item.Option)
                        {
                            case OptionEnum.ChangeAdd:
                            case OptionEnum.ChangeMinus:
                                list.Add($" `{item.KeyOne}`=`{item.KeyOne}`{item.Option.ToEnumDesc<OptionEnum>()}@{item.Param} ");
                                break;
                            case OptionEnum.Set:
                                list.Add($" `{item.KeyOne}`{item.Option.ToEnumDesc<OptionEnum>()}@{item.Param} ");
                                break;
                        }
                        break;
                }
            }

            return string.Join(",", list);
        }

        internal string GetColumns()
        {
            return $" ({ string.Join(",", DC.Conditions.Select(it => $"`{it.KeyOne}`"))}) ";
        }
        internal string GetValues()
        {
            return $" ({ string.Join(",", DC.Conditions.Select(it => $"@{it.Param}"))}) ";
        }

        internal string GetTableName<M>(M m)
        {

            var tableName = DC.AH.GetPropertyValue<M, TableAttribute>(m, a => a.Name);
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new Exception("DB Entity 缺少 TableAttribute 指定的表名!");
            }

            return tableName;

        }
        internal bool TryGetTableName<M>(out string tableName)
        {
            tableName = DC.AH.GetPropertyValue<M, TableAttribute>(Activator.CreateInstance<M>(), a => a.Name);
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new Exception("DB Entity 缺少 TableAttribute 指定的表名!");
            }

            return true;

        }

        internal OptionEnum GetChangeOption(ChangeEnum change)
        {
            switch (change)
            {
                case ChangeEnum.Add:
                    return OptionEnum.ChangeAdd;
                case ChangeEnum.Minus:
                    return OptionEnum.ChangeMinus;
                default:
                    return OptionEnum.ChangeAdd;
            }
        }

        internal DynamicParameters GetParameters()
        {
            var paras = new DynamicParameters();
            foreach (var item in DC.Conditions)
            {
                switch (item.Action)
                {
                    case ActionEnum.Insert:
                    case ActionEnum.Set:
                    case ActionEnum.Change:
                    case ActionEnum.Where:
                    case ActionEnum.And:
                    case ActionEnum.Or:
                        paras.Add(item.Param, item.Value);
                        break;
                }
            }
            return paras;
        }

        internal List<string> GetSQL<M>(SqlTypeEnum type, PagingList<M> pager = default(PagingList<M>))
        {
            var list = new List<string>();

            //
            var tableName = string.Empty;
            if (type != SqlTypeEnum.JoinQueryListAsync)
            {
                TryGetTableName<M>(out tableName);
            }
            switch (type)
            {
                case SqlTypeEnum.CreateAsync:
                    list.Add($" insert into `{tableName}` {DC.SqlProvider.GetColumns()} values {DC.SqlProvider.GetValues()} ;");
                    break;
                case SqlTypeEnum.DeleteAsync:
                    list.Add($" delete from `{tableName}` where {GetWheres()} ; ");
                    break;
                case SqlTypeEnum.UpdateAsync:
                    list.Add($" update `{tableName}` set {DC.SqlProvider.GetUpdates()} where {GetWheres()} ;");
                    break;
                case SqlTypeEnum.QueryFirstOrDefaultAsync:
                    list.Add($"SELECT * FROM `{tableName}` WHERE {GetWheres()} ; ");
                    break;
                case SqlTypeEnum.QueryListAsync:
                    list.Add($"SELECT * FROM `{tableName}` WHERE {GetWheres()} ; ");
                    break;
                case SqlTypeEnum.QueryPagingListAsync:
                    var wherePart = GetWheres();
                    list.Add($"SELECT count(*) FROM `{tableName}` WHERE {wherePart} ; ");
                    list.Add($"SELECT * FROM `{tableName}` WHERE {wherePart} limit {(pager.PageIndex - 1) * pager.PageSize},{pager.PageIndex * pager.PageSize}  ; ");
                    break;
                case SqlTypeEnum.JoinQueryListAsync:
                    list.Add($" select * from {GetJoins()} where {GetWheres()} ; ");
                    break;
            }

            //
            if (Hints.Hint)
            {
                Hints.SQL = list;
                Hints.Parameters = DC.Conditions.Select(it => $"key:【{it.Param}】;val:【{it.Value}】.").ToList();
            }

            //
            return list;
        }
    }
}