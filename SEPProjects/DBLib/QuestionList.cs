using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DBLib
{
    public class QuestionList
    {
        /// <summary>
        /// 调用Group列表
        /// </summary>
        /// <param name="ID">ID为0时调用全部数据</param>
        /// <returns></returns>
        public DataSet GroupSelect(int ID)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            string strProcName = "[dbo].[sp_E_Group_Select]";
            DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
            dbObj.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
            DataSet ds = dbObj.ExecuteDataSet(dbCommand);
            dbCommand.Dispose();
            return ds;

        }

        /// <summary>
        /// 根据用户身份读取题库
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataSet GroupSelectByUser(int ID)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            string strProcName = "[dbo].[sp_E_Group_SelectByUser]";
            DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
            dbObj.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
            DataSet ds = dbObj.ExecuteDataSet(dbCommand);
            dbCommand.Dispose();
            return ds;

        }

        /// <summary>
        /// 根据Group读取问题列表
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public DataSet QuestionSelect(int GroupID)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            string strProcName = "[dbo].[sp_Question_Select]";
            DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
            dbObj.AddInParameter(dbCommand, "GroupID", DbType.Int32, GroupID);
            DataSet ds = dbObj.ExecuteDataSet(dbCommand);
            dbCommand.Dispose();
            return ds;

        }

        /// <summary>
        /// 根据问题ID列出相关答案列表
        /// </summary>
        /// <param name="QID"></param>
        /// <returns></returns>
        public DataSet ChoiceSelect(int QID)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            string strProcName = "[dbo].[sp_Choice_Select]";
            DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
            dbObj.AddInParameter(dbCommand, "QID", DbType.Int32, QID);
            DataSet ds = dbObj.ExecuteDataSet(dbCommand);
            dbCommand.Dispose();
            return ds;

        }
      


    }
}
