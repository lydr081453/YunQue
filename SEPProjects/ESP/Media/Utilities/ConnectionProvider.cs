///////////////////////////////////////////////////////////////////////////
// Description: Connection Provider class for Database connection sharing  
///////////////////////////////////////////////////////////////////////////
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ESP.Media.Access.Utilities
{
	/// <summary>
	/// 目的：提供一个在数据访问层对象共享的数据库连接对象。
	///       
	/// Purpose: provides a SqlConnection object which can be shared among data-access tier objects
	/// to provide a way to do ADO.NET transaction coding without the hassling with SqlConnection objects
	/// on a high level.
	/// </summary>
	public class ConnectionProvider : IDisposable
	{
		#region Class Member Declarations
			private	SqlConnection		m_scoDBConnection;
			private	bool				m_bIsTransactionPending, m_bIsDisposed;
			private	SqlTransaction		m_stCurrentTransaction;
			private	ArrayList			m_alSavePoints;
		    //private string FConnectionString;
		    private static ConnectionProvider FConnectionProvider;

		public static ConnectionProvider connectionPrivider()
		{
			if (FConnectionProvider == null)
		        FConnectionProvider = new ConnectionProvider();
			return FConnectionProvider;
		}

		public string ConnectionString 
		{
			get
			{
				return   ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection();
			}

		}
		#endregion


		private ConnectionProvider()
		{
			// Init the class
			InitClass();
		}


		/// <summary>
		/// Purpose: Implements the IDispose' method Dispose.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		/// <summary>
		/// Purpose: Implements the Dispose functionality.
		/// </summary>
		protected virtual void Dispose(bool bIsDisposing)
		{
			// Check to see if Dispose has already been called.
			if(!m_bIsDisposed)
			{
				if(bIsDisposing)
				{
					// Dispose managed resources.
					if(m_stCurrentTransaction != null)
					{
						m_stCurrentTransaction.Dispose();
						m_stCurrentTransaction = null;
					}
					if(m_scoDBConnection != null)
					{
						// closing the connection will abort (rollback) any pending transactions
						m_scoDBConnection.Close();
						m_scoDBConnection.Dispose();
						m_scoDBConnection = null;
					}
				}
			}
			m_bIsDisposed = true;
		}


		/// <summary>
		/// Purpose: Initializes class members.
		/// </summary>
		private void InitClass()
		{
			// create all the objects and initialize other members.
			m_scoDBConnection = new SqlConnection();
			//AppSettingsReader m_asrConfigReader = new AppSettingsReader();
           
			//if (FConnectionString == null)
		    //	FConnectionString = m_asrConfigReader.GetValue("Main.ConnectionString", typeof(string)).ToString();
			
			// Set connection string of the sqlconnection object
			m_scoDBConnection.ConnectionString = ConnectionString;//FConnectionString;
						//m_asrConfigReader.GetValue("Main.ConnectionString", typeof(string)).ToString();
			m_bIsDisposed = false;
			m_stCurrentTransaction = null;
			m_bIsTransactionPending = false;
			m_alSavePoints = new ArrayList();
		}


		/// <summary>
		/// Purpose: opens the connection object.
		/// </summary>
		/// <returns>true, if succeeded, otherwise an Exception exception is thrown.</returns>
		public bool OpenConnection()
		{
			try
			{
				if(!((m_scoDBConnection.State & ConnectionState.Open) > 0))
				{
					// it's already open.
					//throw new Exception("OpenConnection::Connection is already open.");
					m_scoDBConnection.Open();
					m_bIsTransactionPending = false;

				} 
			return true;
			}
			catch(Exception ex)
			{
				// bubble exception
				throw ex;
			}
		}


		/// <summary>
		/// Purpose: starts a new ADO.NET transaction using the open connection object of this class.
		/// </summary>
		/// <param name="sTransactionName">Name of the transaction to start</param>
		/// <returns>true, if transaction is started correctly, otherwise an Exception exception is thrown</returns>
		public bool BeginTransaction(string sTransactionName)
		{
			try
			{
				if(m_bIsTransactionPending)
				{
					// no nested transactions allowed.
					throw new Exception("BeginTransaction::Already transaction pending. Nesting not allowed");
				}
				if((m_scoDBConnection.State & ConnectionState.Open) == 0)
				{
					// no open connection
					throw new Exception("BeginTransaction::Connection is not open.");
				}
				// begin the transaction and store the transaction object.
				m_stCurrentTransaction = m_scoDBConnection.BeginTransaction(IsolationLevel.ReadCommitted, sTransactionName);
				m_bIsTransactionPending = true;
				m_alSavePoints.Clear();
				return true;
			}
			catch(Exception ex)
			{
				// bubble error
				throw ex;
			}
		}


		/// <summary>
		/// Purpose: commits a pending transaction on the open connection object of this class.
		/// </summary>
		/// <returns>true, if commit was succesful, or an Exception exception is thrown</returns>
		public bool CommitTransaction()
		{
			try
			{
				if(!m_bIsTransactionPending)
				{
					// no transaction pending
					throw new Exception("CommitTransaction::No transaction pending.");
				}
				if((m_scoDBConnection.State & ConnectionState.Open) == 0)
				{
					// no open connection
					throw new Exception("CommitTransaction::Connection is not open.");
				}
				// commit the transaction
				m_stCurrentTransaction.Commit();
				m_bIsTransactionPending = false;
				m_stCurrentTransaction.Dispose();
				m_stCurrentTransaction = null;
				m_alSavePoints.Clear();
				return true;
			}
			catch(Exception ex)
			{
				// bubble error
				throw ex;
			}
		}


		/// <summary>
		/// Purpose: rolls back a pending transaction on the open connection object of this class, 
		/// or rolls back to the savepoint with the given name. Savepoints are created with SaveTransaction().
		/// </summary>
		/// <param name="sTransactionToRollback">Name of transaction to roll back. Can be name of savepoint</param>
		/// <returns>true, if rollback was succesful, or an Exception exception is thrown</returns>
		public bool RollbackTransaction(string sTransactionToRollback)
		{
			try
			{
				if(!m_bIsTransactionPending)
				{
					// no transaction pending
					throw new Exception("RollbackTransaction::No transaction pending.");
				}
				if((m_scoDBConnection.State & ConnectionState.Open) == 0)
				{
					// no open connection
					throw new Exception("RollbackTransaction::Connection is not open.");
				}
				// rollback the transaction
				m_stCurrentTransaction.Rollback(sTransactionToRollback);
				// if this wasn't a savepoint, we've rolled back the complete transaction, so we
				// can clean it up.
				if(!m_alSavePoints.Contains(sTransactionToRollback))
				{
					// it's not a savepoint
					m_bIsTransactionPending = false;
					m_stCurrentTransaction.Dispose();
					m_stCurrentTransaction = null;
					m_alSavePoints.Clear();
				}
				return true;
			}
			catch(Exception ex)
			{
				// bubble error
				throw ex;
			}
		}


		/// <summary>
		/// Purpose: Saves a pending transaction on the open connection object of this class to a 'savepoint'
		/// with the given name.
		/// When a rollback is issued, the caller can rollback to this savepoint or roll back the complete transaction.
		/// </summary>
		/// <param name="sSavePointName">Name of the savepoint to store the current transaction under.</param>
		/// <returns>true, if save was succesful, or an Exception exception is thrown</returns>
		public bool SaveTransaction(string sSavePointName)
		{
			try
			{
				if(!m_bIsTransactionPending)
				{
					// no transaction pending
					throw new Exception("SaveTransaction::No transaction pending.");
				}
				if((m_scoDBConnection.State & ConnectionState.Open) == 0)
				{
					// no open connection
					throw new Exception("SaveTransaction::Connection is not open.");
				}
				// save the transaction
				m_stCurrentTransaction.Save(sSavePointName);
				// Store the savepoint in the list.
				m_alSavePoints.Add(sSavePointName);
				return true;
			}
			catch(Exception ex)
			{
				// bubble error
				throw ex;
			}
		}


		/// <summary>
		/// Purpose: Closes the open connection. Depending on bCommitPendingTransactions, a pending
		/// transaction is commited, or aborted. 
		/// </summary>
		/// <param name="bCommitPendingTransaction">Flag for what to do when a transaction is still pending. True
		/// will commit the current transaction, false will abort (rollback) the complete current transaction.</param>
		/// <returns>true, if close was succesful, false if connection was already closed, or an Exception exception is thrown when
		/// an error occurs</returns>
		public bool CloseConnection(bool bCommitPendingTransaction)
		{
			try
			{
				if((m_scoDBConnection.State & ConnectionState.Open) == 0)
				{
					// no open connection
					return false;
				}
				if(m_bIsTransactionPending)
				{
					if(bCommitPendingTransaction)
					{
						// commit the pending transaction
						m_stCurrentTransaction.Commit();
					}
					else
					{
						// rollback the pending transaction
						m_stCurrentTransaction.Rollback();
					}
					m_bIsTransactionPending = false;
					m_stCurrentTransaction.Dispose();
					m_stCurrentTransaction = null;
					m_alSavePoints.Clear();
				}
				// close the connection
				m_scoDBConnection.Close();
				return true;
			}
			catch(Exception ex)
			{
				// bubble error
				throw ex;
			}
		}


		#region 类属性声明
		public SqlTransaction stCurrentTransaction
		{
			get
			{
				return m_stCurrentTransaction;
			}
		}


		public bool bIsTransactionPending
		{
			get
			{
				return m_bIsTransactionPending;
			}
		}


		public SqlConnection scoDBConnection
		{
			get
			{
				return m_scoDBConnection;
			}
		}
		#endregion
	}
}
