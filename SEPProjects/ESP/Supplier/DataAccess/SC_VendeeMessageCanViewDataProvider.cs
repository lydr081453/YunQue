using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_VendeeMessageCanViewDataProvider
    {
        public SC_VendeeMessageCanViewDataProvider()
        { }


        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_VendeeMessageCanView model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_VendeeMessageCanView ( ");
            strSql.Append("MessageID,");
            strSql.Append("VendeeID,");
            strSql.Append("CreatedDate,");
            strSql.Append("IsUserselfSelected)");
            strSql.Append(" Values (");
            strSql.Append("@MessageID,");
            strSql.Append("@VendeeID,");
            strSql.Append("@CreatedDate,");
            strSql.Append("@IsUserselfSelected");
            strSql.Append(" )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    //new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@MessageID", SqlDbType.Int,4),
                    new SqlParameter("@VendeeID", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@IsUserselfSelected", SqlDbType.Bit)};
            //parameters[0].Value = model.ID;
            parameters[0].Value = model.MessageID;
            parameters[1].Value = model.VendeeID;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = model.IsUserselfSelected;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_VendeeMessageCanView ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_VendeeMessageCanView GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_VendeeMessageCanView ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            return ESP.ConfigCommon.CBO.FillObject<SC_VendeeMessageCanView>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_VendeeMessageCanView ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_VendeeMessageCanView> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_VendeeMessageCanView ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_VendeeMessageCanView>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        public List<SC_VendeeMessageCanView> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_VendeeMessageCanView>(GetList(""));
        }

        #endregion  成员方法
    }
}
