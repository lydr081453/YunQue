using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Common;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Entity;

namespace ESP.Administrative.DataAccess
{
    /// <summary>
    /// 门卡信息数据操作类
    /// </summary>
    public class CardNODataProvider
    {
        public CardNODataProvider()
		{}
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_CardNO");
            strSql.Append(" where ID= @ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CardNOInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_CardNO(");
            strSql.Append("CardNo,UserID,UserCode,UserName,DepartmentID,DepartmentName,ISEnable,EnableTime,UnEnableTime,Deleted,CreateTime,UpdateTime,OperateorID,OperateorDept,Sort)");
            strSql.Append(" values (");
            strSql.Append("@CardNo,@UserID,@UserCode,@UserName,@DepartmentID,@DepartmentName,@ISEnable,@EnableTime,@UnEnableTime,@Deleted,@CreateTime,@UpdateTime,@OperateorID,@OperateorDept,@Sort)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@DepartmentID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@ISEnable", SqlDbType.Bit,1),
					new SqlParameter("@EnableTime", SqlDbType.DateTime),
					new SqlParameter("@UnEnableTime", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.DepartmentID;
            parameters[5].Value = model.DepartmentName;
            parameters[6].Value = model.ISEnable;
            parameters[7].Value = model.EnableTime;
            parameters[8].Value = model.UnEnableTime;
            parameters[9].Value = model.Deleted;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.OperateorID;
            parameters[13].Value = model.OperateorDept;
            parameters[14].Value = model.Sort;

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
        /// 更新一条数据
        /// </summary>
        public void Update(CardNOInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_CardNO set ");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("DepartmentID=@DepartmentID,");
            strSql.Append("DepartmentName=@DepartmentName,");
            strSql.Append("ISEnable=@ISEnable,");
            strSql.Append("EnableTime=@EnableTime,");
            strSql.Append("UnEnableTime=@UnEnableTime,");
            strSql.Append("Deleted=@Deleted,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("OperateorID=@OperateorID,");
            strSql.Append("OperateorDept=@OperateorDept,");
            strSql.Append("Sort=@Sort");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.NVarChar),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@DepartmentID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@ISEnable", SqlDbType.Bit,1),
					new SqlParameter("@EnableTime", SqlDbType.DateTime),
					new SqlParameter("@UnEnableTime", SqlDbType.DateTime),
					new SqlParameter("@Deleted", SqlDbType.Bit,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@OperateorID", SqlDbType.Int,4),
					new SqlParameter("@OperateorDept", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CardNo;
            parameters[2].Value = model.UserID;
            parameters[3].Value = model.UserCode;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.DepartmentID;
            parameters[6].Value = model.DepartmentName;
            parameters[7].Value = model.ISEnable;
            parameters[8].Value = model.EnableTime;
            parameters[9].Value = model.UnEnableTime;
            parameters[10].Value = model.Deleted;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.UpdateTime;
            parameters[13].Value = model.OperateorID;
            parameters[14].Value = model.OperateorDept;
            parameters[15].Value = model.Sort;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_CardNO ");
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
        public CardNOInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AD_CardNO ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            CardNOInfo model = new CardNOInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.ID = ID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM AD_CardNO ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  成员方法
    }
}