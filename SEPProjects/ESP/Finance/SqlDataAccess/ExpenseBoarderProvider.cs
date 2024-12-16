using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
  internal  class ExpenseBoarderProvider:ESP.Finance.IDataAccess.IExpenseBoarderProvider
    {

        #region IExpenseBoarderProvider 成员

      public bool Exists(string CardNo,int userId)
      {
          StringBuilder strSql = new StringBuilder();
          strSql.Append("select count(1) from F_ExpenseBoarder");
          strSql.Append(" where CardNo=@CardNo and UserId=@UserId ");
          SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserId", SqlDbType.Int,4)
                                      };
          parameters[0].Value = CardNo;
          parameters[1].Value = userId;
          return DbHelperSQL.Exists(strSql.ToString(), parameters);
      }

        public int Add(ExpenseBoarderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpenseBoarder(");
            strSql.Append("UserId,Boarder,Mobile,CardNo,CardType)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@Boarder,@Mobile,@CardNo,@CardType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Boarder", SqlDbType.NVarChar,50),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
					new SqlParameter("@CardNo",SqlDbType.NVarChar,50),
                    new SqlParameter("@CardType",SqlDbType.NVarChar,50)
			                          };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.Boarder;
            parameters[2].Value = model.Mobile;
            parameters[3].Value = model.CardNo;
            parameters[4].Value = model.CardType;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Update(ExpenseBoarderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpenseBoarder set ");
            strSql.Append("Boarder=@Boarder,");
            strSql.Append("Mobile=@Mobile,CardType=@CardType");
            strSql.Append(" where CardNo=@CardNo and UserId=@UserId");
            SqlParameter[] parameters = {
					new SqlParameter("@Boarder", SqlDbType.NVarChar,50),
					new SqlParameter("@Mobile", SqlDbType.NVarChar,50),
                    new SqlParameter("@CardType", SqlDbType.NVarChar,50),
					new SqlParameter("@CardNo",SqlDbType.NVarChar,50),
                    new SqlParameter("@UserId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.Boarder;
            parameters[1].Value = model.Mobile;
            parameters[2].Value = model.CardType;
            parameters[3].Value = model.CardNo;
            parameters[4].Value = model.UserId;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpenseBoarder ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public ExpenseBoarderInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from F_ExpenseBoarder ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return CBO.FillObject<ExpenseBoarderInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<ExpenseBoarderInfo> GetList(string term)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * ");
            strSql.Append(" FROM F_ExpenseBoarder ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<ExpenseBoarderInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        #endregion
    }
}
