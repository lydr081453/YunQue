using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;


namespace AdminForm.Data
{
    public static partial class DatabaseHelper
    {
        class ConnectionWrapper : IDisposable
        {
            // Fields
            private readonly DbConnection connection;
            private readonly bool disposeConnection;

            // Methods
            public ConnectionWrapper(DbConnection connection, bool disposeConnection)
            {
                this.connection = connection;
                this.disposeConnection = disposeConnection;
            }

            public void Dispose()
            {
                if (this.disposeConnection)
                {
                    this.connection.Dispose();
                }
            }

            // Properties
            public DbConnection Connection
            {
                get
                {
                    return this.connection;
                }
            }
        }

        class ConnectionStringInfo
        {
            public string ConnectionString { get; set; }
            public DbProviderFactory Provider { get; set; }
        }

        private static ConnectionStringInfo ConnectionString;

        static DatabaseHelper()
        {
            //string connectionStringName = ConfigurationManager.ConnectionStringName;

            //if (string.IsNullOrEmpty(connectionStringName))
            //    connectionStringName = BookManager.Common.Configuration.ConfigurationManager.ConnectionStringName;

            //if (string.IsNullOrEmpty(connectionStringName))
            //    return;

            //var connectionStringSettings = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];
            //if (connectionStringSettings == null)
            //    return;

            //Initialize("data source=GABRIELZHAO-PC\\SQLEXPRESS;Initial Catalog=espScan;Integrated Security=True;", "System.Data.SqlClient");
            Initialize("data source=10.1.2.8\\shunya;Initial Catalog=esp_dvp;User ID=sa;Password=1qaz@WSX123", "System.Data.SqlClient");
        }

        public static void Initialize(string connectionStringName)
        {
            //var connectionStringSettings = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];
            //Initialize("data source=GABRIELZHAO-PC\\SQLEXPRESS;Initial Catalog=espScan;Integrated Security=True;", "System.Data.SqlClient");
            Initialize("data source=10.1.2.8\\shunya;Initial Catalog=esp_dvp;User ID=sa;Password=1qaz@WSX123", "System.Data.SqlClient");
        }

        public static void Initialize(string connectionString, string providerName)
        {
            ConnectionString = new ConnectionStringInfo();
            ConnectionString.ConnectionString = connectionString;
            ConnectionString.Provider = DbProviderFactories.GetFactory(providerName);
        }

        private static ConnectionWrapper GetOpenConnection()
        {
            return GetOpenConnection(true);
        }

        private static ConnectionWrapper GetOpenConnection(bool disposeInnerConnection)
        {
            DbConnection connection = TransactionScopeConnections.GetConnection("data source=10.1.2.8\\shunya;Initial Catalog=esp_dvp;User ID=sa;Password=1qaz@WSX123");
            //DbConnection connection = TransactionScopeConnections.GetConnection("data source=GABRIELZHAO-PC\\SQLEXPRESS;Initial Catalog=espScan;Integrated Security=True;");
            if (connection != null)
            {
                return new ConnectionWrapper(connection, false);
            }
            return new ConnectionWrapper(GetNewOpenConnection(), disposeInnerConnection);
        }

        internal static DbConnection GetNewOpenConnection()
        {
            //string connectionString = ConnectionString.ConnectionString;
            //string providerName = ConnectionString.ProviderName;
            DbConnection connection = null;
            try
            {
                connection = ConnectionString.Provider.CreateConnection();
                //connection.ConnectionString = "data source=GABRIELZHAO-PC\\SQLEXPRESS;Initial Catalog=espScan;Integrated Security=True;";
                connection.ConnectionString = "data source=10.1.2.8\\shunya;Initial Catalog=esp_dvp;User ID=sa;Password=1qaz@WSX123";
                connection.Open();

                return connection;
            }
            catch
            {
                if (connection != null)
                    connection.Close();
                throw;
            }
        }

        public static int ExecuteNonQuery(string commandText, CommandType commandType, params System.Data.IDataParameter[] parameters)
        {
            using (ConnectionWrapper connection = GetOpenConnection())
            {
                using (DbCommand cmd = connection.Connection.CreateCommand())
                {
                    cmd.CommandType = commandType;
                    cmd.CommandText = commandText;
                    cmd.Parameters.AddRange(parameters);
                    cmd.Connection = connection.Connection;

                    var ret = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return ret;
                }
            }
        }

        public static DbDataReader ExecuteReader(string commandText, CommandType commandType, params System.Data.IDataParameter[] parameters)
        {
            using (ConnectionWrapper connection = GetOpenConnection(false))
            {
                using (DbCommand cmd = connection.Connection.CreateCommand())
                {
                    cmd.CommandType = commandType;
                    cmd.CommandText = commandText;
                    cmd.Parameters.AddRange(parameters);
                    cmd.Connection = connection.Connection;
                    if (System.Transactions.Transaction.Current != null)
                    {
                        var ret1 = cmd.ExecuteReader(System.Data.CommandBehavior.Default);
                        cmd.Parameters.Clear();
                        return ret1;
                    }

                    DbDataReader ret = null;
                    try
                    {
                        ret = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    }
                    catch
                    {
                        if (ret == null)
                        {
                            connection.Connection.Dispose();
                        }
                        throw;
                    }
                    cmd.Parameters.Clear();
                    return ret;
                }
            }
        }

        public static object ExecuteScalar(string commandText, CommandType commandType, params System.Data.IDataParameter[] parameters)
        {
            using (ConnectionWrapper connection = GetOpenConnection())
            {
                using (DbCommand cmd = connection.Connection.CreateCommand())
                {
                    cmd.CommandType = commandType;
                    cmd.CommandText = commandText;
                    cmd.Parameters.AddRange(parameters);
                    cmd.Connection = connection.Connection;

                    var ret = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return ret;
                }
            }
        }
        
        public static DbParameter CreateInputParameter(string name, object value)
        {
            var p = ConnectionString.Provider.CreateParameter();
            p.ParameterName = name;
            if (value == null)
            {
                p.Value = System.DBNull.Value;
                p.DbType = DbType.AnsiString;
            }
            else
            {
                p.Value = value;
            }

            return p;
        }

        public static DbParameter CreateParameter()
        {
            return ConnectionString.Provider.CreateParameter();
        }
    }
}
