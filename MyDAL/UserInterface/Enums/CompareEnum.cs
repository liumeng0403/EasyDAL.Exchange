﻿namespace MyDAL
{
    /// <summary>
    /// 用于 Paging Option 以指定属性对应 column 的比较作用
    /// </summary>
    public enum CompareEnum
    {
        /// <summary>
        /// " "
        /// </summary>
        None,

        /// <summary>
        /// =
        /// </summary>
        Equal,

        /// <summary>
        /// !=
        /// </summary>
        NotEqual,

        /// <summary>
        /// &lt;
        /// </summary>
        LessThan,

        /// <summary>
        /// &lt;=
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// &gt;
        /// </summary>
        GreaterThan,

        /// <summary>
        /// &gt;=
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// " like "
        /// </summary>
        Like,
        /// <summary>
        /// 
        /// </summary>
        Like_StartsWith,
        /// <summary>
        /// 
        /// </summary>
        Like_EndsWith,

        /// <summary>
        /// " in "
        /// </summary>
        In,

        /// <summary>
        /// " not in "
        /// </summary>
        NotIn        
    }
}
