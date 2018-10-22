﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyDAL.Core
{
    internal class XConfig
    {
        internal static bool IsDebug { get; set; } = false;

        /************************************************************************************************************/

        internal static bool IsCodeFirst { get; set; } = false;
        internal static bool IsNeedChangeDb { get; set; } = true;
        internal static string TablesNamespace { get; set; } = string.Empty;

        /************************************************************************************************************/

        public static int CommandTimeout { get; set; } = 10;  // 10s 

        /// <summary>
        /// Default is 4000, any value larger than this field will not have the default value applied.
        /// </summary>
        internal static int StringDefaultLength { get; private set; } = 4000;

        internal static BindingFlags ClassSelfMember { get; private set; } = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;

        /************************************************************************************************************/

        internal static Type Bool { get; private set; } = typeof(bool);
        internal static Type Byte { get; private set; } = typeof(byte);
        internal static Type Char { get; private set; } = typeof(char);
        internal static Type Decimal { get; private set; } = typeof(decimal);
        internal static Type Double { get; private set; } = typeof(double);
        internal static Type Float { get; private set; } = typeof(float);
        internal static Type Int { get; private set; } = typeof(int);
        internal static Type Long { get; private set; } = typeof(long);
        internal static Type Sbyte { get; private set; } = typeof(sbyte);
        internal static Type Short { get; private set; } = typeof(short);
        internal static Type Uint { get; private set; } = typeof(uint);
        internal static Type Ulong { get; private set; } = typeof(ulong);
        internal static Type Ushort { get; private set; } = typeof(ushort);
        internal static Type String { get; private set; } = typeof(string);
        internal static Type DateTime { get; private set; } = typeof(DateTime);
        internal static Type TimeSpan { get; private set; } = typeof(TimeSpan);
        internal static Type Guid { get; private set; } = typeof(Guid);
        internal static Type NullableT { get; private set; } = typeof(Nullable<>);
        internal static Type ListT { get; private set; } = typeof(List<>);

        /************************************************************************************************************/

        internal static Type XTableAttribute = typeof(XTableAttribute);
        internal static string XTableFullName = typeof(XTableAttribute).FullName;

        internal static Type XColumnAttribute = typeof(XColumnAttribute);
        internal static string XColumnFullName = typeof(XColumnAttribute).FullName;

    }
}
