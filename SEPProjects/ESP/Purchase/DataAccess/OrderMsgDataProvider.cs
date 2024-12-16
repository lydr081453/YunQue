using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.Entity;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.Common;

namespace ESP.Purchase.DataAccess
{
    public class OrderMsgDataProvider
    {
        public OrderMsgDataProvider()
        { }
        #region  成员方法


        /// <summary>
        /// 添加一条数据
        /// </summary>
        public int Add(OrderMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_OrderMsg ( ");
            strSql.Append("GeneralId,");
            strSql.Append("OrderId,");
            strSql.Append("MsgId,");
            strSql.Append("MsgReturnId,");
            strSql.Append("CreatTime,");
            strSql.Append("CreatUserId,");
            strSql.Append("UpdateTime,");
            strSql.Append("UpdateUserId)");
            strSql.Append(" Values (");
            strSql.Append("@GeneralId,");
            strSql.Append("@OrderId,");
            strSql.Append("@MsgId,");
            strSql.Append("@MsgReturnId,");
            strSql.Append("@CreatTime,");
            strSql.Append("@CreatUserId,");
            strSql.Append("@UpdateTime,");
            strSql.Append("@UpdateUserId");
            strSql.Append(" )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@GeneralId", SqlDbType.Int,4),
                    new SqlParameter("@OrderId", SqlDbType.Int,4),
					new SqlParameter("@MsgId", SqlDbType.NVarChar),
					new SqlParameter("@MsgReturnId", SqlDbType.NVarChar),
					new SqlParameter("@CreatTime", SqlDbType.DateTime),
					new SqlParameter("@CreatUserId", SqlDbType.Int,4),
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUserId", SqlDbType.Int,4)};
            parameters[0].Value = model.GeneralId;
            parameters[1].Value = model.OrderId;
            parameters[2].Value = model.MsgId;
            parameters[3].Value = model.MsgReturnId;
            parameters[4].Value = model.CreatTime;
            parameters[5].Value = model.CreatUserId;
            parameters[6].Value = model.UpdateTime;
            parameters[7].Value = model.UpdateUserId;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
            //DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            //return model.ID;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(OrderMsg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_OrderMsg set ");
            strSql.Append("MsgId=@MsgId,");
            strSql.Append("MsgReturnId=@MsgReturnId,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateUserId=@UpdateUserId");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@MsgId", SqlDbType.NVarChar),
					new SqlParameter("@MsgReturnId", SqlDbType.NVarChar),
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateUserId", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.MsgId;
            parameters[2].Value = model.MsgReturnId;
            parameters[3].Value = model.UpdateTime;
            parameters[4].Value = model.UpdateUserId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_OrderMsg ");
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
        public OrderMsg GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_OrderMsg ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            return ESP.ConfigCommon.CBO.FillObject<OrderMsg>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OrderMsg GetModelByOrderId(int orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_OrderMsg ");
            strSql.Append(" where orderid=@orderid");
            SqlParameter[] parameters = {
					new SqlParameter("@orderid", SqlDbType.Int,4)};
            parameters[0].Value = orderId;
            return ESP.ConfigCommon.CBO.FillObject<OrderMsg>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_OrderMsg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<OrderMsg> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_OrderMsg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<OrderMsg>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        public List<OrderMsg> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<OrderMsg>(GetList(""));
        }

        #endregion  成员方法
    }

}
