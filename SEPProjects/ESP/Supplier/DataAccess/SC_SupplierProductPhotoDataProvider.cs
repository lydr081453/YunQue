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
    public class SC_SupplierProductPhotoDataProvider
    {
        public SC_SupplierProductPhotoDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SC_SupplierProductPhoto");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierProductPhoto model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierProductPhoto(");
            strSql.Append("SupplierId,ProductId,ShowTxt,PhotoUrl,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status)");
            strSql.Append(" values (");
            strSql.Append("@SupplierId,@ProductId,@ShowTxt,@PhotoUrl,@CreatedIP,@CreatTime,@LastModifiedIP,@LastUpdateTime,@Type,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.Int,4),
					new SqlParameter("@ShowTxt", SqlDbType.NVarChar,1000),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.SupplierId;
            parameters[1].Value = model.ProductId;
            parameters[2].Value = model.ShowTxt;
            parameters[3].Value = model.PhotoUrl;
            parameters[4].Value = model.CreatedIP;
            parameters[5].Value = model.CreatTime;
            parameters[6].Value = model.LastModifiedIP;
            parameters[7].Value = model.LastUpdateTime;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.Status;

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
        public void Update(SC_SupplierProductPhoto model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_SupplierProductPhoto set ");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("ProductId=@ProductId,");
            strSql.Append("ShowTxt=@ShowTxt,");
            strSql.Append("PhotoUrl=@PhotoUrl,");
            strSql.Append("CreatedIP=@CreatedIP,");
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("LastModifiedIP=@LastModifiedIP,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.Int,4),
					new SqlParameter("@ShowTxt", SqlDbType.NVarChar,1000),
					new SqlParameter("@PhotoUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.SupplierId;
            parameters[2].Value = model.ProductId;
            parameters[3].Value = model.ShowTxt;
            parameters[4].Value = model.PhotoUrl;
            parameters[5].Value = model.CreatedIP;
            parameters[6].Value = model.CreatTime;
            parameters[7].Value = model.LastModifiedIP;
            parameters[8].Value = model.LastUpdateTime;
            parameters[9].Value = model.Type;
            parameters[10].Value = model.Status;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_SupplierProductPhoto ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_SupplierProductPhoto GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,SupplierId,ProductId,ShowTxt,PhotoUrl,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status from SC_SupplierProductPhoto ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            SC_SupplierProductPhoto model = new SC_SupplierProductPhoto();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(ds.Tables[0].Rows[0]["SupplierId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductId"].ToString() != "")
                {
                    model.ProductId = int.Parse(ds.Tables[0].Rows[0]["ProductId"].ToString());
                }
                model.ShowTxt = ds.Tables[0].Rows[0]["ShowTxt"].ToString();
                model.PhotoUrl = ds.Tables[0].Rows[0]["PhotoUrl"].ToString();
                model.CreatedIP = ds.Tables[0].Rows[0]["CreatedIP"].ToString();
                if (ds.Tables[0].Rows[0]["CreatTime"].ToString() != "")
                {
                    model.CreatTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
                }
                model.LastModifiedIP = ds.Tables[0].Rows[0]["LastModifiedIP"].ToString();
                if (ds.Tables[0].Rows[0]["LastUpdateTime"].ToString() != "")
                {
                    model.LastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
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
            strSql.Append("select Id,SupplierId,ProductId,ShowTxt,PhotoUrl,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status ");
            strSql.Append(" FROM SC_SupplierProductPhoto ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,SupplierId,ProductId,ShowTxt,PhotoUrl,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status ");
            strSql.Append(" FROM SC_SupplierProductPhoto ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "SC_SupplierProductPhoto";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  成员方法
    }
}
