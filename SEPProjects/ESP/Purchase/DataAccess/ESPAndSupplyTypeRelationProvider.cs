using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using System.Data;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
	/// 数据访问类ESPAndSupplyTypeRelationProvider。
	/// </summary>
	public class ESPAndSupplyTypeRelationDataProvider
	{
        public ESPAndSupplyTypeRelationDataProvider()
		{}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_ESPAndSupplyTypeRelation");
			strSql.Append(" where id= @id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Entity.ESPAndSupplyTypeRelationInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_ESPAndSupplyTypeRelation(");
            strSql.Append("ESPTypeId,SupplyTypeId,CreatedDate,CreatedUserId,TypeLevel)");
			strSql.Append(" values (");
            strSql.Append("@ESPTypeId,@SupplyTypeId,@CreatedDate,@CreatedUserId,@TypeLevel)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ESPTypeId", SqlDbType.Int,4),
					new SqlParameter("@SupplyTypeId", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@TypeLevel",SqlDbType.Int,4)};
            parameters[0].Value = model.ESPTypeId;
            parameters[1].Value = model.SupplyTypeId;
			parameters[2].Value = DateTime.Now;
            parameters[3].Value = model.CreatedUserId;
            parameters[4].Value = model.TypeLevel;

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
		public void Update(Entity.ESPAndSupplyTypeRelationInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_ESPAndSupplyTypeRelation set ");
            strSql.Append("ESPTypeId=@ESPTypeId,");
            strSql.Append("SupplyTypeId=@SupplyTypeId,");
            strSql.Append("TypeLevel=@TypeLevel");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ESPTypeId", SqlDbType.Int,4),
					new SqlParameter("@SupplyTypeId", SqlDbType.Int,4),
					new SqlParameter("@TypeLevel", SqlDbType.Int,4)};
			parameters[0].Value = model.ID;
            parameters[1].Value = model.ESPTypeId;
            parameters[2].Value = model.SupplyTypeId;
            parameters[3].Value = model.TypeLevel;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete T_ESPAndSupplyTypeRelation ");
            strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Entity.ESPAndSupplyTypeRelationInfo GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from T_ESPAndSupplyTypeRelation ");
            strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.ESPAndSupplyTypeRelationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

        public Entity.ESPAndSupplyTypeRelationInfo GetModelByEid(int ESPTypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_ESPAndSupplyTypeRelation ");
            strSql.Append(" where ESPTypeId=@ESPTypeId");
            SqlParameter[] parameters = {
					new SqlParameter("@ESPTypeId", SqlDbType.Int,4)};
            parameters[0].Value = ESPTypeId;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.ESPAndSupplyTypeRelationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public Entity.ESPAndSupplyTypeRelationInfo GetModelBySid(int SupplyTypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_ESPAndSupplyTypeRelation ");
            strSql.Append(" where SupplyTypeId=@SupplyTypeId");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplyTypeId", SqlDbType.Int,4)};
            parameters[0].Value = SupplyTypeId;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.ESPAndSupplyTypeRelationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Entity.ESPAndSupplyTypeRelationInfo> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM T_ESPAndSupplyTypeRelation where 1=1");
			if(strWhere.Trim()!="")
			{
				strSql.Append(strWhere);
			}
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.ESPAndSupplyTypeRelationInfo>(DbHelperSQL.Query(strSql.ToString()));
		}
    }
}
