using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Transactions;
using SysTransaction = System.Transactions.Transaction;
using System.Configuration;
using System.Data;
using System.Data.EntityClient;
using System.Data.Objects;

namespace ESP.Data
{
    class DbConnectionInfo
    {
        public DbConnectionInfo(DbConnection connection, string connectionString)
        {
            ConnectionString = connectionString;
            Connection = connection;
            ObjectContexts = new Dictionary<Type, ObjectContext>();
        }

        public string ConnectionString { get; private set; }
        public DbConnection Connection { get; private set; }
        public Dictionary<Type, ObjectContext> ObjectContexts { get; private set; }

        internal void Dispose()
        {
            Connection.Dispose();
            foreach (var en in this.ObjectContexts)
            {
                en.Value.Dispose();
            }
        }
    }

    class HookCookie : IDisposable
    {
        Dictionary<string, DbConnectionInfo> _transactionConnections;
        string _connectionString;

        public static HookCookie Empty
        {
            get
            {
                return new HookCookie(null, null);
            }
        }

        public HookCookie(Dictionary<string, DbConnectionInfo> transactionConnections, string connectionString)
        {
            _transactionConnections = transactionConnections;
            _connectionString = connectionString;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (_transactionConnections != null)
                _transactionConnections.Remove(_connectionString);
        }

        #endregion
    }



    /// <summary>
    /// 对 DbConnection 的包装。
    /// </summary>
    /// <remarks>
    /// 当在环境事务中创建 DbConnectionHolder 实例时， 将首先检索当前环境事务中是否
    /// 有相同连接字符串的连接存在， 如果有， 则使用现有连接， 否则创建新的连接并存
    /// 储到当前环境事务的连接列表中。 当对实例调用 Dispose 时并不关闭数据库连接，
    /// 而等到当前环境事务结束时， 才关闭连接。
    /// 
    /// 如果不在环境事务中创建实例， 则总是创建新的连接对象。 当对实例调用 Dispose
    /// 时总是释放数据库连接。
    /// </remarks>
    public sealed class DbConnectionHolder : IDisposable
    {
        /// <summary>
        /// 连接列表的字典
        /// </summary>
        private static readonly Dictionary<SysTransaction, Dictionary<string, DbConnectionInfo>> transactionConnections = new Dictionary<SysTransaction, Dictionary<string, DbConnectionInfo>>();

        /// <summary>
        /// 获取当前环境事务中与连接字符串匹配的现有连接对象， 如果没有则创建一个， 如果不前不存在环境事务， 则返回空。 
        /// </summary>
        /// <param name="connectionString">连接字符串。</param>
        /// <returns>如果存在环境事务， 则返回相应的连接对象， 否则返回空。</returns>
        private DbConnectionInfo GetConnection(string connectionString)
        {
            Dictionary<string, DbConnectionInfo> dictionary;
            DbConnectionInfo newOpenConnection;
            SysTransaction current = SysTransaction.Current;
            if (current == null)
            {
                return null;
            }
            lock (transactionConnections)
            {
                if (!transactionConnections.TryGetValue(current, out dictionary))
                {
                    dictionary = new Dictionary<string, DbConnectionInfo>();
                    current.TransactionCompleted += new TransactionCompletedEventHandler(OnTransactionCompleted);
                    transactionConnections.Add(current, dictionary);
                }
            }

            if (dictionary.TryGetValue(connectionString, out newOpenConnection))
                return newOpenConnection;

            DbConnection dbConnection = GetNewConnection(connectionString);
            dbConnection.Open();
            newOpenConnection = new DbConnectionInfo(dbConnection, connectionString);
            dictionary.Add(connectionString, newOpenConnection);
            return newOpenConnection;
        }

        /// <summary>
        /// 环境事务结束事件响应程序， 负责释放与环境事件相关的数据库连接。
        /// </summary>
        /// <param name="sender">事件源。</param>
        /// <param name="e">事件参数。</param>
        private static void OnTransactionCompleted(object sender, TransactionEventArgs e)
        {
            Dictionary<string, DbConnectionInfo> dictionary;
            SysTransaction transaction = e.Transaction;
            lock (transactionConnections)
            {
                if (transactionConnections.TryGetValue(transaction, out dictionary))
                {
                    transactionConnections.Remove(transaction);
                }
                else
                {
                    dictionary = null;
                }
            }
            if (dictionary != null)
            {
                foreach (KeyValuePair<string, DbConnectionInfo> entry in dictionary)
                {
                    DbConnectionInfo info = entry.Value;
                    info.Dispose();
                }
            }
        }


        /// <summary>
        /// 被包装的 DbConnection 对象。
        /// </summary>
        public DbConnection DbConnection { get { return DbConnectionInfo.Connection; } }

        private DbConnectionInfo DbConnectionInfo { get; set; }

        /// <summary>
        /// 数据源提供程序工厂实例。
        /// </summary>
        public DbProviderFactory Factory { get; private set; }

        /// <summary>
        /// 连接字符串。
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// 数据源提供程序名称。
        /// </summary>
        public string ProviderName { get; private set; }

        /// <summary>
        /// 是否需要在 Dispose 时释放被包装的 DbConnection 连接对象。
        /// </summary>
        private bool _DisposeConnection;

        /// <summary>
        /// 用ESP配置节的 Entity Framework 设置构造实例。总是打开连接，即使没有环境事务。
        /// </summary>
        public DbConnectionHolder()
        {
            Initialize(ESP.Configuration.ConfigurationManager.EntityFrameworkConnectionString, typeof(EntityConnection).Namespace);
        }

        ///// <summary>
        ///// 用ESP配置节的 Entity Framework 设置构造实例。
        ///// </summary>
        ///// <param name="openAlways">总是打开连接，即使没有环境事务。</param>
        //public DbConnectionHolder(bool openAlways)
        //{
        //    Initialize(ESP.Configuration.ConfigurationManager.EntityFrameworkConnectionString, typeof(EntityConnection).Namespace, false);
        //}

        /// <summary>
        /// 用连接字符串和数据提供程序名称构造实例。。总是打开连接，即使没有环境事务。
        /// </summary>
        /// <param name="connectionStringSettingsName">连接字符串配置项名称。</param>
        public DbConnectionHolder(string connectionStringSettingsName)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionStringSettingsName];

            Initialize(settings.ConnectionString, settings.ProviderName);
        }

        /// <summary>
        /// 用连接字符串和数据提供程序名称构造实例。总是打开连接，即使没有环境事务。
        /// </summary>
        /// <param name="connectionString">连接字符串。</param>
        /// <param name="providerName">数据提供程序名称。</param>
        public DbConnectionHolder(string connectionString, string providerName)
        {
            Initialize(connectionString, providerName);
        }


        /// <summary>
        /// 用连接字符串和数据提供程序名称初始化实例，并允许指定是否在 Dispose 时
        /// 释放不在事务中创建的新的 DbConnection 对象。
        /// </summary>
        /// <param name="connectionString">连接字符串。</param>
        /// <param name="providerName">数据提供程序名称。</param>
        private void Initialize(string connectionString, string providerName)
        {
            ProviderName = providerName;
            ConnectionString = connectionString;
            try
            {
                Factory = System.Data.Common.DbProviderFactories.GetFactory(providerName);
            }
            catch (ArgumentException)
            {
                if (providerName == "System.Data.EntityClient")
                    Factory = System.Data.EntityClient.EntityProviderFactory.Instance;
            }
            DbConnectionInfo = GetConnection(connectionString);
            if (DbConnectionInfo == null)
            {
                DbConnection connection = connection = GetNewConnection(connectionString);
                DbConnectionInfo = new DbConnectionInfo(connection, connectionString);
                _DisposeConnection = true;
            }
            else
            {
                _DisposeConnection = false;
            }
        }

        /// <summary>
        /// 创建一个新的数据库连接。
        /// </summary>
        /// <param name="connectionString">连接字符串。</param>
        /// <returns>新创建的数据库连接，其连接字符串已经被设置为 connectionString 参数的值，并已经打开。</returns>
        internal DbConnection GetNewConnection(string connectionString)
        {
            DbConnection connection = Factory.CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        /// <summary>
        /// 创建一个与连接匹配的数据库命令对象。
        /// </summary>
        /// <param name="commandText">命令字符串。</param>
        /// <param name="commandType">命令类型。</param>
        /// <returns>数据库命令对象， 其 Connection 和 CommandText 属性已经设置。</returns>
        public DbCommand CreateCommand(string commandText, CommandType commandType)
        {
            DbCommand cmd = Factory.CreateCommand();
            cmd.Connection = this.DbConnection;
            cmd.CommandType = commandType;
            cmd.CommandText = commandText;

            return cmd;
        }

        /// <summary>
        /// 用 selectCommandText 参数作为 SelectCommand 构造数据适配器对象。
        /// </summary>
        /// <param name="selectCommandText">要创建的数据适配器对象的 SelectCommand 的命令文本。</param>
        /// <param name="commandType">命令类型。</param>
        /// <returns>新创建的数据适配器对象。</returns>
        public DbDataAdapter CreateDataAdapter(string selectCommandText, CommandType commandType)
        {
            DbDataAdapter adapter = Factory.CreateDataAdapter();
            adapter.SelectCommand = CreateCommand(selectCommandText, commandType);

            return adapter;
        }

        /// <summary>
        /// 创建与当前数据库连接匹配的查询参数实例。
        /// </summary>
        /// <returns>新创建的查询参数对象。</returns>
        public DbParameter CreateParameter()
        {
            return Factory.CreateParameter();
        }

        /// <summary>
        /// 创建与当前数据库连接匹配的查询参数实例。
        /// </summary>
        /// <param name="name">参数名称。</param>
        /// <param name="value">参数值。</param>
        /// <returns>新创建的查询参数对象。</returns>
        public DbParameter CreateParameter(string name, object value)
        {
            DbParameter para = Factory.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            if (para.Value == null)
                para.Value = DBNull.Value;
            return para;
        }

        /// <summary>
        /// 创建与当前数据库连接匹配的查询参数实例。
        /// </summary>
        /// <param name="name">参数名称。</param>
        /// <param name="value">参数值。</param>
        /// <param name="direction">参数的传递方向。</param>
        /// <returns>新创建的查询参数对象。</returns>
        public DbParameter CreateParameter(string name, object value, System.Data.ParameterDirection direction)
        {
            DbParameter para = Factory.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            para.Direction = direction;
            if (para.Value == null)
                para.Value = DBNull.Value;

            return para;
        }


        /// <summary>
        /// 创建与当前数据库连接匹配的查询参数实例。
        /// </summary>
        /// <param name="name">参数名称。</param>
        /// <param name="value">参数值。</param>
        /// <param name="direction">参数的传递方向。</param>
        /// <param name="dbType">参数的类型。</param>
        /// <returns>新创建的查询参数对象。</returns>
        public DbParameter CreateParameter(string name, object value, System.Data.ParameterDirection direction, System.Data.DbType dbType)
        {
            DbParameter para = Factory.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            para.Direction = direction;
            para.DbType = dbType;
            if (para.Value == null)
                para.Value = DBNull.Value;

            return para;
        }


        /// <summary>
        /// 创建与当前数据库连接匹配的查询参数实例。
        /// </summary>
        /// <param name="name">参数名称。</param>
        /// <param name="value">参数值。</param>
        /// <param name="direction">参数的传递方向。</param>
        /// <param name="dbType">参数的类型。</param>
        /// <param name="size">参数的长度。</param>
        /// <returns>新创建的查询参数对象。</returns>
        public DbParameter CreateParameter(string name, object value, System.Data.ParameterDirection direction, System.Data.DbType dbType, int size)
        {
            DbParameter para = Factory.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            para.Direction = direction;
            para.DbType = dbType;
            para.Size = size;
            if (para.Value == null)
                para.Value = DBNull.Value;

            return para;
        }

        #region IDisposable 成员

        /// <summary>
        /// 释放当前实例，如果连接不在环境事务中， 则同时释放数据库连接。
        /// </summary>
        public void Dispose()
        {
            if (_DisposeConnection && DbConnectionInfo != null)
                DbConnectionInfo.Dispose();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetObjectContext<T>() where T : ObjectContext
        {
            Type key = typeof(T);
            ObjectContext context;
            Dictionary<Type, ObjectContext> contexts = DbConnectionInfo.ObjectContexts;
            if (!contexts.TryGetValue(key, out context))
            {
                context = (ObjectContext)Activator.CreateInstance(key, DbConnectionInfo.Connection);
                contexts.Add(key, context);
            }
            return (T)context;
        }

        /// <summary>
        /// 把当前对象的连接添加到当前事务的连接列表中。
        /// 如果当前无环境事务， 或列表中已经存在相同连接字符串的连接， 
        /// 则方法引发 System.InvalidOperationException 类型的异常。
        /// </summary>
        /// <returns>
        /// 记录本次操作信息， 用于在事务结束前将连接从事务的连接
        /// 列表中移除。
        /// </returns>
        public IDisposable HookConnection()
        {
            if (this._DisposeConnection == false)
                return HookCookie.Empty;

            SysTransaction current = SysTransaction.Current;
            if (current == null)
                throw new InvalidOperationException();

            string key = DbConnectionInfo.ConnectionString;

            Dictionary<string, DbConnectionInfo> dictionary;
            lock (transactionConnections)
            {
                if (!transactionConnections.TryGetValue(current, out dictionary))
                {
                    dictionary = new Dictionary<string, DbConnectionInfo>();
                    current.TransactionCompleted += new TransactionCompletedEventHandler(OnTransactionCompleted);
                    transactionConnections.Add(current, dictionary);
                }
            }
            if (dictionary.ContainsKey(key))
                throw new InvalidOperationException();

            dictionary.Add(key, DbConnectionInfo);

            return new HookCookie(dictionary, key);
        }
    }
}
