﻿using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.DataRainbow.MySQL;
using MyDAL.DataRainbow.XCommon.Interfaces;
using System.Data;

namespace MyDAL.Core.Configs
{
    internal sealed class DbTypeConfig
        : IDbTypeConfig
    {
        private static IDbTypeConfig MySql { get; } = new MySqlTypeConfig();

        DbType IDbTypeConfig.IntProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.IntProc(dc, colType);
                default:
                    return DbType.Int32;
            }
        }
        DbType IDbTypeConfig.LongProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.LongProc(dc, colType);
                default:
                    return DbType.Int64;
            }
        }
        DbType IDbTypeConfig.DecimalProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.DecimalProc(dc, colType);
                default:
                    return DbType.Decimal;
            }
        }
        DbType IDbTypeConfig.BoolProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.BoolProc(dc, colType);
                default:
                    return DbType.Boolean;
            }
        }
        DbType IDbTypeConfig.StringProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.StringProc(dc, colType);
                default:
                    return DbType.AnsiString;
            }
        }
        DbType IDbTypeConfig.ListStringProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.ListStringProc(dc, colType);
                default:
                    return DbType.String;
            }
        }
        DbType IDbTypeConfig.DateTimeProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.DateTimeProc(dc, colType);
                default:
                    return DbType.DateTime2;
            }
        }
        DbType IDbTypeConfig.GuidProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.GuidProc(dc, colType);
                default:
                    return DbType.Guid;
            }
        }
        DbType IDbTypeConfig.ByteProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.ByteProc(dc, colType);
                default:
                    return DbType.Byte;
            }
        }
        DbType IDbTypeConfig.ByteArrayProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.ByteArrayProc(dc, colType);
                default:
                    return DbType.Binary;
            }
        }
        DbType IDbTypeConfig.CharProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.CharProc(dc, colType);
                default:
                    return DbType.AnsiString;
            }
        }
        DbType IDbTypeConfig.DoubleProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.DoubleProc(dc, colType);
                default:
                    return DbType.Double;
            }
        }
        DbType IDbTypeConfig.FloatProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.FloatProc(dc, colType);
                default:
                    return DbType.Single;
            }
        }
        DbType IDbTypeConfig.SbyteProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.SbyteProc(dc, colType);
                default:
                    return DbType.SByte;
            }
        }
        DbType IDbTypeConfig.ShortProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.ShortProc(dc, colType);
                default:
                    return DbType.Int16;
            }
        }
        DbType IDbTypeConfig.UintProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.UintProc(dc, colType);
                default:
                    return DbType.UInt32;
            }
        }
        DbType IDbTypeConfig.UlongProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.UlongProc(dc, colType);
                default:
                    return DbType.UInt64;
            }
        }
        DbType IDbTypeConfig.UshortProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.UshortProc(dc, colType);
                default:
                    return DbType.UInt16;
            }
        }
        DbType IDbTypeConfig.TimeSpanProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.TimeSpanProc(dc, colType);
                default:
                    return DbType.Time;
            }
        }
        DbType IDbTypeConfig.DateTimeOffsetProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.DateTimeOffsetProc(dc, colType);
                default:
                    return DbType.DateTimeOffset;
            }
        }
        DbType IDbTypeConfig.ObjectProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.ObjectProc(dc, colType);
                default:
                    return DbType.Object;
            }
        }
    }
}
