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
    public class SC_SupplierEvaluationDataProvider
    {
        public SC_SupplierEvaluationDataProvider()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SC_SupplierEvaluation");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SC_SupplierEvaluation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SC_SupplierEvaluation(");
			strSql.Append("supplierid,questionid,questionnum,Evaluation,createtime,creator,createip,lastupdatetime,lastupdateman,lastupdateip,type,status)");
			strSql.Append(" values (");
			strSql.Append("@supplierid,@questionid,@questionnum,@Evaluation,@createtime,@creator,@createip,@lastupdatetime,@lastupdateman,@lastupdateip,@type,@status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@supplierid", SqlDbType.Int,4),
					new SqlParameter("@questionid", SqlDbType.Int,4),
					new SqlParameter("@questionnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Evaluation", SqlDbType.NVarChar,4000),
					new SqlParameter("@createtime", SqlDbType.DateTime),
					new SqlParameter("@creator", SqlDbType.NVarChar,200),
					new SqlParameter("@createip", SqlDbType.NVarChar,200),
					new SqlParameter("@lastupdatetime", SqlDbType.DateTime),
					new SqlParameter("@lastupdateman", SqlDbType.NVarChar,200),
					new SqlParameter("@lastupdateip", SqlDbType.NVarChar,200),
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4)};
			parameters[0].Value = model.supplierid;
			parameters[1].Value = model.questionid;
			parameters[2].Value = model.questionnum;
			parameters[3].Value = model.Evaluation;
			parameters[4].Value = model.createtime;
			parameters[5].Value = model.creator;
			parameters[6].Value = model.createip;
			parameters[7].Value = model.lastupdatetime;
			parameters[8].Value = model.lastupdateman;
			parameters[9].Value = model.lastupdateip;
			parameters[10].Value = model.type;
			parameters[11].Value = model.status;

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
        /// 增加一条数据(带事务)
        /// </summary>
        public int Add(SC_SupplierEvaluation model, SqlTransaction trans, SqlConnection conn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierEvaluation(");
            strSql.Append("supplierid,questionid,questionnum,Evaluation,createtime,creator,createip,lastupdatetime,lastupdateman,lastupdateip,type,status)");
            strSql.Append(" values (");
            strSql.Append("@supplierid,@questionid,@questionnum,@Evaluation,@createtime,@creator,@createip,@lastupdatetime,@lastupdateman,@lastupdateip,@type,@status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@supplierid", SqlDbType.Int,4),
					new SqlParameter("@questionid", SqlDbType.Int,4),
					new SqlParameter("@questionnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Evaluation", SqlDbType.NVarChar,4000),
					new SqlParameter("@createtime", SqlDbType.DateTime),
					new SqlParameter("@creator", SqlDbType.NVarChar,200),
					new SqlParameter("@createip", SqlDbType.NVarChar,200),
					new SqlParameter("@lastupdatetime", SqlDbType.DateTime),
					new SqlParameter("@lastupdateman", SqlDbType.NVarChar,200),
					new SqlParameter("@lastupdateip", SqlDbType.NVarChar,200),
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = model.supplierid;
            parameters[1].Value = model.questionid;
            parameters[2].Value = model.questionnum;
            parameters[3].Value = model.Evaluation;
            parameters[4].Value = model.createtime;
            parameters[5].Value = model.creator;
            parameters[6].Value = model.createip;
            parameters[7].Value = model.lastupdatetime;
            parameters[8].Value = model.lastupdateman;
            parameters[9].Value = model.lastupdateip;
            parameters[10].Value = model.type;
            parameters[11].Value = model.status;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), conn, trans, parameters);
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
		public void Update(SC_SupplierEvaluation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SC_SupplierEvaluation set ");
			strSql.Append("supplierid=@supplierid,");
			strSql.Append("questionid=@questionid,");
			strSql.Append("questionnum=@questionnum,");
			strSql.Append("Evaluation=@Evaluation,");
			strSql.Append("createtime=@createtime,");
			strSql.Append("creator=@creator,");
			strSql.Append("createip=@createip,");
			strSql.Append("lastupdatetime=@lastupdatetime,");
			strSql.Append("lastupdateman=@lastupdateman,");
			strSql.Append("lastupdateip=@lastupdateip,");
			strSql.Append("type=@type,");
			strSql.Append("status=@status");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@supplierid", SqlDbType.Int,4),
					new SqlParameter("@questionid", SqlDbType.Int,4),
					new SqlParameter("@questionnum", SqlDbType.NVarChar,50),
					new SqlParameter("@Evaluation", SqlDbType.NVarChar,4000),
					new SqlParameter("@createtime", SqlDbType.DateTime),
					new SqlParameter("@creator", SqlDbType.NVarChar,200),
					new SqlParameter("@createip", SqlDbType.NVarChar,200),
					new SqlParameter("@lastupdatetime", SqlDbType.DateTime),
					new SqlParameter("@lastupdateman", SqlDbType.NVarChar,200),
					new SqlParameter("@lastupdateip", SqlDbType.NVarChar,200),
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.supplierid;
			parameters[2].Value = model.questionid;
			parameters[3].Value = model.questionnum;
			parameters[4].Value = model.Evaluation;
			parameters[5].Value = model.createtime;
			parameters[6].Value = model.creator;
			parameters[7].Value = model.createip;
			parameters[8].Value = model.lastupdatetime;
			parameters[9].Value = model.lastupdateman;
			parameters[10].Value = model.lastupdateip;
			parameters[11].Value = model.type;
			parameters[12].Value = model.status;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SC_SupplierEvaluation ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 根据用户ID删除一条采购部意见备注
        /// </summary>
        public void DeleteBySupplierId(int supplierid, SqlTransaction trans, SqlConnection conn)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_SupplierEvaluation ");
            strSql.Append(" where supplierid=@supplierid and type=1 ");
            SqlParameter[] parameters = {
					new SqlParameter("@supplierid", SqlDbType.Int,4)};
            parameters[0].Value = supplierid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SC_SupplierEvaluation GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,supplierid,questionid,questionnum,Evaluation,createtime,creator,createip,lastupdatetime,lastupdateman,lastupdateip,type,status from SC_SupplierEvaluation ");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			SC_SupplierEvaluation model=new SC_SupplierEvaluation();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["supplierid"].ToString()!="")
				{
					model.supplierid=int.Parse(ds.Tables[0].Rows[0]["supplierid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["questionid"].ToString()!="")
				{
					model.questionid=int.Parse(ds.Tables[0].Rows[0]["questionid"].ToString());
				}
				model.questionnum=ds.Tables[0].Rows[0]["questionnum"].ToString();
				model.Evaluation=ds.Tables[0].Rows[0]["Evaluation"].ToString();
				if(ds.Tables[0].Rows[0]["createtime"].ToString()!="")
				{
					model.createtime=DateTime.Parse(ds.Tables[0].Rows[0]["createtime"].ToString());
				}
				model.creator=ds.Tables[0].Rows[0]["creator"].ToString();
				model.createip=ds.Tables[0].Rows[0]["createip"].ToString();
				if(ds.Tables[0].Rows[0]["lastupdatetime"].ToString()!="")
				{
					model.lastupdatetime=DateTime.Parse(ds.Tables[0].Rows[0]["lastupdatetime"].ToString());
				}
				model.lastupdateman=ds.Tables[0].Rows[0]["lastupdateman"].ToString();
				model.lastupdateip=ds.Tables[0].Rows[0]["lastupdateip"].ToString();
				if(ds.Tables[0].Rows[0]["type"].ToString()!="")
				{
					model.type=int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
				}
				if(ds.Tables[0].Rows[0]["status"].ToString()!="")
				{
					model.status=int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_SupplierEvaluation GetModelBySupplierId(int supplierid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,supplierid,questionid,questionnum,Evaluation,createtime,creator,createip,lastupdatetime,lastupdateman,lastupdateip,type,status from SC_SupplierEvaluation ");
            strSql.Append(" where supplierid=@supplierid ");
            SqlParameter[] parameters = {
					new SqlParameter("@supplierid", SqlDbType.Int,4)};
            parameters[0].Value = supplierid;

            SC_SupplierEvaluation model = new SC_SupplierEvaluation();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplierid"].ToString() != "")
                {
                    model.supplierid = int.Parse(ds.Tables[0].Rows[0]["supplierid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["questionid"].ToString() != "")
                {
                    model.questionid = int.Parse(ds.Tables[0].Rows[0]["questionid"].ToString());
                }
                model.questionnum = ds.Tables[0].Rows[0]["questionnum"].ToString();
                model.Evaluation = ds.Tables[0].Rows[0]["Evaluation"].ToString();
                if (ds.Tables[0].Rows[0]["createtime"].ToString() != "")
                {
                    model.createtime = DateTime.Parse(ds.Tables[0].Rows[0]["createtime"].ToString());
                }
                model.creator = ds.Tables[0].Rows[0]["creator"].ToString();
                model.createip = ds.Tables[0].Rows[0]["createip"].ToString();
                if (ds.Tables[0].Rows[0]["lastupdatetime"].ToString() != "")
                {
                    model.lastupdatetime = DateTime.Parse(ds.Tables[0].Rows[0]["lastupdatetime"].ToString());
                }
                model.lastupdateman = ds.Tables[0].Rows[0]["lastupdateman"].ToString();
                model.lastupdateip = ds.Tables[0].Rows[0]["lastupdateip"].ToString();
                if (ds.Tables[0].Rows[0]["type"].ToString() != "")
                {
                    model.type = int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,supplierid,questionid,questionnum,Evaluation,createtime,creator,createip,lastupdatetime,lastupdateman,lastupdateip,type,status ");
			strSql.Append(" FROM SC_SupplierEvaluation ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,supplierid,questionid,questionnum,Evaluation,createtime,creator,createip,lastupdatetime,lastupdateman,lastupdateip,type,status ");
			strSql.Append(" FROM SC_SupplierEvaluation ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}
