using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ESP.Framework.DataAccess.Utilities
{
    /// <summary>
    /// 与数据库无关的数据库查询参数类
    /// </summary>
    public class DbDataParameter
    {
        DbType _DbType;
        string _Name;
        object _Value;

        /// <summary>
        /// 构造查询参数
        /// </summary>
        /// <param name="dbType">数据类型</param>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        public DbDataParameter(DbType dbType, string name, object value)
        {
            _DbType = dbType;
            _Name = name;
            _Value = value;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DbType
        {
            get { return _DbType; }
            set { _DbType = value; }
        }

        /// <summary>
        /// 参数名
        /// </summary>
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}
