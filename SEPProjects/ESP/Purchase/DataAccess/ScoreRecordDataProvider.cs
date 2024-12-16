using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Purchase.Common;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
	/// 数据访问类ScoreRecordDataProvider。
	/// </summary>
	public class ScoreRecordDataProvider
	{
		public ScoreRecordDataProvider()
		{}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int RecordID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_ScoreRecord");
			strSql.Append(" where RecordID= @RecordID");
			SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.Int,4)
				};
			parameters[0].Value = RecordID;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Entity.ScoreRecordInfo model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_ScoreRecord(");
            strSql.Append("PRID,PRNO,SupplierID,SupplierName,ScoreContentID,ScoreContent,ScoreID,ScoreName,Scores,Remark)");
			strSql.Append(" values (");
            strSql.Append("@PRID,@PRNO,@SupplierID,@SupplierName,@ScoreContentID,@ScoreContent,@ScoreID,@ScoreName,@Scores,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@PRNO", SqlDbType.NVarChar),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar),
					new SqlParameter("@ScoreContentID", SqlDbType.Int,4),
					new SqlParameter("@ScoreContent", SqlDbType.NVarChar),
					new SqlParameter("@ScoreID", SqlDbType.Int,4),
					new SqlParameter("@ScoreName", SqlDbType.NVarChar),
					new SqlParameter("@Scores", SqlDbType.Int,4),
                    new SqlParameter("@Remark",SqlDbType.NVarChar)};
			parameters[0].Value = model.PRID;
			parameters[1].Value = model.PRNO;
			parameters[2].Value = model.SupplierID;
			parameters[3].Value = model.SupplierName;
			parameters[4].Value = model.ScoreContentID;
			parameters[5].Value = model.ScoreContent;
			parameters[6].Value = model.ScoreID;
			parameters[7].Value = model.ScoreName;
			parameters[8].Value = model.Scores;
            parameters[9].Value = model.Remark;

            object obj = null;
            if(trans == null)
			    obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(),trans.Connection,trans, parameters);
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
		public void Update(Entity.ScoreRecordInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_ScoreRecord set ");
			strSql.Append("PRID=@PRID,");
			strSql.Append("PRNO=@PRNO,");
			strSql.Append("SupplierID=@SupplierID,");
			strSql.Append("SupplierName=@SupplierName,");
			strSql.Append("ScoreContentID=@ScoreContentID,");
			strSql.Append("ScoreContent=@ScoreContent,");
			strSql.Append("ScoreID=@ScoreID,");
			strSql.Append("ScoreName=@ScoreName,");
			strSql.Append("Scores=@Scores,Remark=@Remark");
			strSql.Append(" where RecordID=@RecordID");
			SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.Int,4),
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@PRNO", SqlDbType.NVarChar),
					new SqlParameter("@SupplierID", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar),
					new SqlParameter("@ScoreContentID", SqlDbType.Int,4),
					new SqlParameter("@ScoreContent", SqlDbType.NVarChar),
					new SqlParameter("@ScoreID", SqlDbType.Int,4),
					new SqlParameter("@ScoreName", SqlDbType.NVarChar),
					new SqlParameter("@Scores", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar)};
			parameters[0].Value = model.RecordID;
			parameters[1].Value = model.PRID;
			parameters[2].Value = model.PRNO;
			parameters[3].Value = model.SupplierID;
			parameters[4].Value = model.SupplierName;
			parameters[5].Value = model.ScoreContentID;
			parameters[6].Value = model.ScoreContent;
			parameters[7].Value = model.ScoreID;
			parameters[8].Value = model.ScoreName;
			parameters[9].Value = model.Scores;
            parameters[10].Value = model.Remark;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int RecordID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete T_ScoreRecord ");
			strSql.Append(" where RecordID=@RecordID");
			SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.Int,4)
				};
			parameters[0].Value = RecordID;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Entity.ScoreRecordInfo GetModel(int RecordID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from T_ScoreRecord ");
			strSql.Append(" where RecordID=@RecordID");
			SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.Int,4)};
			parameters[0].Value = RecordID;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.ScoreRecordInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Entity.ScoreRecordInfo> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM T_ScoreRecord  where 1=1");
			if(strWhere.Trim()!="")
			{
				strSql.Append(strWhere);
			}
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.ScoreRecordInfo>(DbHelperSQL.Query(strSql.ToString()));
		}
    }
}
