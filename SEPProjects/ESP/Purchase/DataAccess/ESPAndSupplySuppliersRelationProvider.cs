using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.Common;

namespace ESP.Purchase.DataAccess
{
    public class ESPAndSupplySuppliersRelationProvider
    {
        public ESPAndSupplySuppliersRelationProvider()
		{}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_ESPAndSupplySuppliersRelation");
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
		public int Add(Entity.ESPAndSupplySuppliersRelation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_ESPAndSupplySuppliersRelation(");
            strSql.Append("ESPSupplierId,SupplySupplierId,CreatedDate,CreatedUserId)");
			strSql.Append(" values (");
            strSql.Append("@ESPTypeId,@SupplyTypeId,@CreatedDate,@CreatedUserId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ESPTypeId", SqlDbType.Int,4),
					new SqlParameter("@SupplyTypeId", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4)};
            parameters[0].Value = model.ESPSupplierId;
            parameters[1].Value = model.SupplySupplierId;
			parameters[2].Value = DateTime.Now;
            parameters[3].Value = model.CreatedUserId;

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
		/// 删除一条数据
		/// </summary>
		public void Delete(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete T_ESPAndSupplySuppliersRelation ");
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
		public Entity.ESPAndSupplySuppliersRelation GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from T_ESPAndSupplySuppliersRelation ");
            strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.ESPAndSupplySuppliersRelation>(DbHelperSQL.Query(strSql.ToString(), parameters));
		}

        public Entity.ESPAndSupplySuppliersRelation GetModelByEid(int ESPId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_ESPAndSupplySuppliersRelation ");
            strSql.Append(" where ESPSupplierId=@ESPSupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@ESPSupplierId", SqlDbType.Int,4)};
            parameters[0].Value = ESPId;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.ESPAndSupplySuppliersRelation>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public Entity.ESPAndSupplySuppliersRelation GetModelBySid(int SupplyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_ESPAndSupplySuppliersRelation ");
            strSql.Append(" where SupplySupplierId=@SupplySupplierId");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplySupplierId", SqlDbType.Int,4)};
            parameters[0].Value = SupplyId;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.ESPAndSupplySuppliersRelation>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Entity.ESPAndSupplySuppliersRelation> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM T_ESPAndSupplySuppliersRelation where 1=1");
			if(strWhere.Trim()!="")
			{
				strSql.Append(strWhere);
			}
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.ESPAndSupplySuppliersRelation>(DbHelperSQL.Query(strSql.ToString()));
		}
    }
}
