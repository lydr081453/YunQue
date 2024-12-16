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
    public class SC_ProductBatchDataProvider
    {
    
		public SC_ProductBatchDataProvider()
		{}
		#region  成员方法


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SC_ProductBatch model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SC_ProductBatch(");
			strSql.Append("batchName,batchDes,supplierId,typeId,beginTime,endTime,createDate,createIp,lastUpdateDate,lastUpdateIp,status,remark)");
			strSql.Append(" values (");
			strSql.Append("@batchName,@batchDes,@supplierId,@typeId,@beginTime,@endTime,@createDate,@createIp,@lastUpdateDate,@lastUpdateIp,@status,@remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@batchName", SqlDbType.NVarChar,100),
					new SqlParameter("@batchDes", SqlDbType.NVarChar,1000),
					new SqlParameter("@supplierId", SqlDbType.Int,4),
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@beginTime", SqlDbType.DateTime),
					new SqlParameter("@endTime", SqlDbType.DateTime),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@createIp", SqlDbType.VarChar,10),
					new SqlParameter("@lastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@lastUpdateIp", SqlDbType.VarChar,50),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,4000)};
			parameters[0].Value = model.batchName;
			parameters[1].Value = model.batchDes;
			parameters[2].Value = model.supplierId;
			parameters[3].Value = model.typeId;
			parameters[4].Value = model.beginTime;
			parameters[5].Value = model.endTime;
			parameters[6].Value = model.createDate;
			parameters[7].Value = model.createIp;
			parameters[8].Value = model.lastUpdateDate;
			parameters[9].Value = model.lastUpdateIp;
			parameters[10].Value = model.status;
			parameters[11].Value = model.remark;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public void Update(SC_ProductBatch model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SC_ProductBatch set ");
			strSql.Append("batchName=@batchName,");
			strSql.Append("batchDes=@batchDes,");
			strSql.Append("supplierId=@supplierId,");
			strSql.Append("typeId=@typeId,");
			strSql.Append("beginTime=@beginTime,");
			strSql.Append("endTime=@endTime,");
			strSql.Append("createDate=@createDate,");
			strSql.Append("createIp=@createIp,");
			strSql.Append("lastUpdateDate=@lastUpdateDate,");
			strSql.Append("lastUpdateIp=@lastUpdateIp,");
			strSql.Append("status=@status,");
			strSql.Append("remark=@remark");
			strSql.Append(" where batchId=@batchId ");
			SqlParameter[] parameters = {
					new SqlParameter("@batchId", SqlDbType.Int,4),
					new SqlParameter("@batchName", SqlDbType.NVarChar,100),
					new SqlParameter("@batchDes", SqlDbType.NVarChar,1000),
					new SqlParameter("@supplierId", SqlDbType.Int,4),
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@beginTime", SqlDbType.DateTime),
					new SqlParameter("@endTime", SqlDbType.DateTime),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@createIp", SqlDbType.VarChar,10),
					new SqlParameter("@lastUpdateDate", SqlDbType.DateTime),
					new SqlParameter("@lastUpdateIp", SqlDbType.VarChar,50),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,4000)};
			parameters[0].Value = model.batchId;
			parameters[1].Value = model.batchName;
			parameters[2].Value = model.batchDes;
			parameters[3].Value = model.supplierId;
			parameters[4].Value = model.typeId;
			parameters[5].Value = model.beginTime;
			parameters[6].Value = model.endTime;
			parameters[7].Value = model.createDate;
			parameters[8].Value = model.createIp;
			parameters[9].Value = model.lastUpdateDate;
			parameters[10].Value = model.lastUpdateIp;
			parameters[11].Value = model.status;
			parameters[12].Value = model.remark;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int batchId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SC_ProductBatch ");
			strSql.Append(" where batchId=@batchId ");
			SqlParameter[] parameters = {
					new SqlParameter("@batchId", SqlDbType.Int,4)};
			parameters[0].Value = batchId;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SC_ProductBatch GetModel(int batchId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 batchId,batchName,batchDes,supplierId,typeId,beginTime,endTime,createDate,createIp,lastUpdateDate,lastUpdateIp,status,remark from SC_ProductBatch ");
			strSql.Append(" where batchId=@batchId ");
			SqlParameter[] parameters = {
					new SqlParameter("@batchId", SqlDbType.Int,4)};
			parameters[0].Value = batchId;

			SC_ProductBatch model=new SC_ProductBatch();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["batchId"].ToString()!="")
				{
					model.batchId=int.Parse(ds.Tables[0].Rows[0]["batchId"].ToString());
				}
				model.batchName=ds.Tables[0].Rows[0]["batchName"].ToString();
				model.batchDes=ds.Tables[0].Rows[0]["batchDes"].ToString();
				if(ds.Tables[0].Rows[0]["supplierId"].ToString()!="")
				{
					model.supplierId=int.Parse(ds.Tables[0].Rows[0]["supplierId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["typeId"].ToString()!="")
				{
					model.typeId=int.Parse(ds.Tables[0].Rows[0]["typeId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["beginTime"].ToString()!="")
				{
					model.beginTime=DateTime.Parse(ds.Tables[0].Rows[0]["beginTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["endTime"].ToString()!="")
				{
					model.endTime=DateTime.Parse(ds.Tables[0].Rows[0]["endTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["createDate"].ToString()!="")
				{
					model.createDate=DateTime.Parse(ds.Tables[0].Rows[0]["createDate"].ToString());
				}
				model.createIp=ds.Tables[0].Rows[0]["createIp"].ToString();
				if(ds.Tables[0].Rows[0]["lastUpdateDate"].ToString()!="")
				{
					model.lastUpdateDate=DateTime.Parse(ds.Tables[0].Rows[0]["lastUpdateDate"].ToString());
				}
				model.lastUpdateIp=ds.Tables[0].Rows[0]["lastUpdateIp"].ToString();
				if(ds.Tables[0].Rows[0]["status"].ToString()!="")
				{
					model.status=int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
				}
				model.remark=ds.Tables[0].Rows[0]["remark"].ToString();
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
            strSql.Append("select batchId,batchName,batchDes,supplierId,typeId,beginTime,endTime,createDate,createIp,lastUpdateDate,lastUpdateIp,status,remark ");
            strSql.Append(" FROM SC_ProductBatch ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
		#endregion  成员方法
    }
}
