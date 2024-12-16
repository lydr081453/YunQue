using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;

namespace AdminForm.Data
{
    public static class TransactionScopeConnections
    {
        private static readonly Dictionary<Transaction, Dictionary<string, DbConnection>> transactionConnections = new Dictionary<Transaction, Dictionary<string, DbConnection>>();

        public static DbConnection GetConnection(string connectionString)
        {
            Dictionary<string, DbConnection> dictionary;
            DbConnection newOpenConnection;
            Transaction current = Transaction.Current;
            if (current == null)
            {
                return null;
            }
            transactionConnections.TryGetValue(current, out dictionary);
            if (dictionary == null)
            {
                current.TransactionCompleted += new TransactionCompletedEventHandler(TransactionScopeConnections.OnTransactionCompleted);
                dictionary = new Dictionary<string, DbConnection>();
                lock (transactionConnections)
                {
                    transactionConnections.Add(current, dictionary);
                }
            }
            else
            {
                dictionary.TryGetValue(connectionString, out newOpenConnection);
                if (newOpenConnection != null)
                {
                    return newOpenConnection;
                }
            }

            newOpenConnection = DatabaseHelper.GetNewOpenConnection();
            dictionary.Add(connectionString, newOpenConnection);
            return newOpenConnection;
        }



        private static void OnTransactionCompleted(object sender, TransactionEventArgs e)
        {
            Dictionary<string, DbConnection> dictionary;
            transactionConnections.TryGetValue(e.Transaction, out dictionary);
            if (dictionary != null)
            {
                lock (transactionConnections)
                {
                    transactionConnections.Remove(e.Transaction);
                }
                foreach (DbConnection connection in dictionary.Values)
                {
                    connection.Dispose();
                }
            }
        }
    }



}