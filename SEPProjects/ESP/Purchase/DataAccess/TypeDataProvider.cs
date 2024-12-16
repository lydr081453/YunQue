using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类TypeDataProvider
    /// </summary>
    public class TypeDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeDataProvider"/> class.
        /// </summary>
        public TypeDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(TypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Type(");
            strSql.Append("typename,auditorid,parentId,typelevel,shauditorid,gzauditorid,status,operationflow,IsNeedHQCheck,BJPaymentUserID,SHPaymentUserID,GZPaymentUserID,Sort)");
            strSql.Append(" values (");
            strSql.Append("@typename,@auditorid,@parentId,@typelevel,@shauditorid,@gzauditorid,@status,@operationflow,@IsNeedHQCheck,@BJPaymentUserID,@SHPaymentUserID,@GZPaymentUserID,@Sort)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] parameters = {
					new SqlParameter("@typename", SqlDbType.NVarChar,400),
					new SqlParameter("@auditorid", SqlDbType.NChar,20),
                    new SqlParameter("@parentId",SqlDbType.Int,4),
                    new SqlParameter("@typelevel",SqlDbType.Int,4),
                    new SqlParameter("@shauditorid",SqlDbType.Int,4),
                    new SqlParameter("@gzauditorid",SqlDbType.Int,4),
                    new SqlParameter("@status",SqlDbType.Int,4),
                    new SqlParameter("@operationflow",SqlDbType.Int,4),
                    new SqlParameter("@IsNeedHQCheck",SqlDbType.Int,4),
                                        new SqlParameter("@BJPaymentUserID",SqlDbType.Int,4),
                                        new SqlParameter("@SHPaymentUserID",SqlDbType.Int,4),
                                        new SqlParameter("@GZPaymentUserID",SqlDbType.Int,4),
                                         new SqlParameter("@Sort",SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.typename;
            parameters[1].Value = model.auditorid;
            parameters[2].Value = model.parentId;
            parameters[3].Value = model.typelevel;
            parameters[4].Value = model.shauditorid;
            parameters[5].Value = model.gzauditorid;
            parameters[6].Value = State.typestatus_used;
            parameters[7].Value = State.typeoperationflow_Mormal;
            parameters[8].Value = State.typeHQCheck_No;
            parameters[9].Value = model.BJPaymentUserID;
            parameters[10].Value = model.SHPaymentUserID;
            parameters[11].Value = model.GZPaymentUserID;
            parameters[12].Value = model.Sort;
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
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Update(TypeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Type set ");
            strSql.Append("typename=@typename,");
            strSql.Append("auditorid=@auditorid,");
            strSql.Append("parentId=@parentId,");
            strSql.Append("typelevel=@typelevel,");
            strSql.Append("shauditorid=@shauditorid,");
            strSql.Append("gzauditorid=@gzauditorid,");
            strSql.Append("BJPaymentUserID=@BJPaymentUserID,");
            strSql.Append("SHPaymentUserID=@SHPaymentUserID,");
            strSql.Append("GZPaymentUserID=@GZPaymentUserID,");
            strSql.Append("Sort=@Sort");
            strSql.Append(" where typeid=@typeid");
            SqlParameter[] parameters = {
					new SqlParameter("@typeid", SqlDbType.Int,4),
					new SqlParameter("@typename", SqlDbType.NVarChar,400),
					new SqlParameter("@auditorid", SqlDbType.NChar,20),
                    new SqlParameter("@parentId",SqlDbType.Int,4),
                    new SqlParameter("@typelevel",SqlDbType.Int,4),
                    new SqlParameter("@shauditorid",SqlDbType.Int,4),
                    new SqlParameter("@gzauditorid",SqlDbType.Int,4),
                          new SqlParameter("@BJPaymentUserID",SqlDbType.Int,4),
                                        new SqlParameter("@SHPaymentUserID",SqlDbType.Int,4),
                                        new SqlParameter("@GZPaymentUserID",SqlDbType.Int,4),
                                        new SqlParameter("@Sort",SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.typeid;
            parameters[1].Value = model.typename;
            parameters[2].Value = model.auditorid;
            parameters[3].Value = model.parentId;
            parameters[4].Value = model.typelevel;
            parameters[5].Value = model.shauditorid;
            parameters[6].Value = model.gzauditorid;
            parameters[7].Value = model.BJPaymentUserID;
            parameters[8].Value = model.SHPaymentUserID;
            parameters[9].Value = model.GZPaymentUserID;
            parameters[10].Value = model.Sort;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="typeid">The typeid.</param>
        public void Delete(int typeid,int level, int updateStatus)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("delete T_Type ");
            strSql.Append(" update T_Type set ");
            strSql.Append(" status=@status where");
            if (level == 1)
            {
                strSql.Append(" parentid in ( select typeid from t_type where parentid=" + typeid + ") " + " or ");
            }
            if ( level == 1 || level == 2)
            {
                strSql.Append(" parentid=" + typeid + " or ");
            }
            strSql.Append(" typeid=" + typeid);
            
            SqlParameter[] parameters = {
                    new SqlParameter("@status",SqlDbType.Int,4)
                };
            parameters[0].Value = updateStatus;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="typeid">The typeid.</param>
        /// <returns></returns>
        public TypeInfo GetModel(int typeid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Type ");
            strSql.Append(" where typeid=@typeid");
            SqlParameter[] parameters = {
					new SqlParameter("@typeid", SqlDbType.Int,4)};
            parameters[0].Value = typeid;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.TypeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
       
        }

        public TypeInfo GetModelByBTBId(string ptbId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Type ");
            strSql.Append(" where PTBId=@PTBId");
            SqlParameter[] parameters = {
					new SqlParameter("@PTBId", SqlDbType.VarChar)};
            parameters[0].Value = ptbId;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.TypeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// Gets the name of the model by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public TypeInfo GetModelByName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Type ");
            strSql.Append(" where typename=@typename");
            SqlParameter[] parameters = {
					new SqlParameter("@typename", SqlDbType.VarChar)};
            parameters[0].Value = name;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.TypeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 根据业务流向确定物料类别
        /// </summary>
        /// <param name="operationflow">The operationflow.</param>
        /// <returns></returns>
        public TypeInfo GetModelByOperationFlow(int operationflow)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Type ");
            strSql.Append(" where operationflow=@operationflow");
            SqlParameter[] parameters = {
					new SqlParameter("@operationflow", SqlDbType.Int,4)};
            parameters[0].Value = operationflow;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.TypeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
       
        }

        /// <summary>
        /// 模糊查询第三级类型
        /// </summary>
        /// <param name="likeKey">The like key.</param>
        /// <param name="isLike">if set to <c>true</c> [is like].</param>
        /// <returns></returns>
        public List<TypeInfo> GetTypeListByLike(string likeKey, bool isLike)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select c.* from t_type c inner join (
                                select a.typeid from t_type a inner join (
                                select typeid from t_type where parentid = 0 ) b on a.parentid = b.typeid ) d
                                on c.parentid = d.typeid ");
            if (isLike)
            {
                strSql.Append(" where c.typename like '%'+@likeKey+'%' and status <>@status");
            }
            else
            {
                strSql.Append(" where c.typename = @likeKey and status <>@status");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@likeKey", SqlDbType.VarChar),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = likeKey;
            parameters[1].Value = State.typestatus_block;
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.TypeInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [typeid],[typename],[auditorid],[parentId],[status],BJPaymentUserID,SHPaymentUserID,GZPaymentUserID,Sort ");
            strSql.Append(" FROM T_Type where status<>" + State.typestatus_block.ToString());
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by sort asc ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<TypeInfo> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_Type where status<>" + State.typestatus_block.ToString());
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by sort asc ");
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.TypeInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 根据parentId获得列表
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        /// <returns></returns>
        public List<TypeInfo> GetListForBase(int parentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Type ");
            strSql.Append(" where parentid=@parentid and (status=@status or status=@status1)");
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@parentid", parentId));
            parms.Add(new SqlParameter("@status", State.typestatus_block));
            parms.Add(new SqlParameter("@status1", State.typestatus_used));

            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.TypeInfo>(DbHelperSQL.Query(strSql.ToString(), parms.ToArray()));
     
        }

        /// <summary>
        /// 根据parentId获得列表
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        /// <param name="typeid">The typeid.</param>
        /// <param name="typeids">The typeids.</param>
        /// <returns></returns>
        public List<TypeInfo> GetListByParentId(int parentId,int typeid,string typeids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Type ");
            strSql.Append(" where parentid=@parentid and status<>@status");
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@parentid", parentId));
            parms.Add(new SqlParameter("@status", State.typestatus_block));
            if (typeid > 0)
            {
                strSql.Append(" and typeid = @typeid");
                parms.Add(new SqlParameter("@typeid", typeid));
            }
            if (typeids != "")
            {
                strSql.Append(" and typeid in ("+typeids+")");
            }
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.TypeInfo>(DbHelperSQL.Query(strSql.ToString(), parms.ToArray()));
     
        }

        /// <summary>
        /// 获得同级Type列表
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <returns></returns>
        public List<TypeInfo> GetListByTypeId(int typeId)
        {
            string sql = @"SELECT typeid, typename, auditorid, parentId, typelevel,shauditorid,gzauditorid,status,operationflow,IsNeedHQCheck,BJPaymentUserID,SHPaymentUserID,GZPaymentUserID,Sort FROM T_Type
                                WHERE  (parentId = (SELECT parentId FROM T_Type AS T_Type_1 WHERE (typeid = @typeId))) and status<>@status";
            SqlParameter[] parameters = {
					new SqlParameter("@typeId", SqlDbType.VarChar),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = typeId;
            parameters[1].Value = State.typestatus_block;
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.TypeInfo>(DbHelperSQL.Query(sql, parameters));
     
        }

        /// <summary>
        /// Gets the list by parent id A.
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public static List<TypeInfo> GetListByParentIdA(int parentId)
        {
            List<TypeInfo> list = new List<TypeInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Type ");
            strSql.Append(" where parentid=@parentid and status<>@status");
            SqlParameter[] parameters = {
					new SqlParameter("@parentid", SqlDbType.VarChar),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = parentId;
            parameters[1].Value = State.typestatus_block;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return getModel(ds);
        }

        /// <summary>
        /// 根据供应商ID获得一级产品类型列表
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <returns></returns>
        public static List<TypeInfo> getLevel1ListBySupplierId(int supplierId)
        {
            List<TypeInfo> list = new List<TypeInfo>();
            string strSql = @"SELECT DISTINCT d.typeid, d.typename, d.auditorid, d.parentId,d.typelevel,d.shauditorid,d.gzauditorid,d.status,d.operationflow,d.IsNeedHQCheck,d.BJPaymentUserID,d.SHPaymentUserID,d.GZPaymentUserID
                                FROM T_Type AS d INNER JOIN
                                (SELECT DISTINCT a.parentId
                                    FROM T_Type AS a INNER JOIN
                                    (SELECT b.parentId FROM T_Product AS a INNER JOIN
                                    T_Type AS b ON a.productType = b.typeid and b.status <> @status WHERE (a.supplierId = @supplierId)) AS c ON c.parentId = a.typeid) AS e ON e.parentId = d.typeid and d.status<>@status";
            SqlParameter[] parms = { 
                    new SqlParameter("@supplierId",supplierId),
                    new SqlParameter("@status",State.typestatus_block)
                                   };
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parms);
            return getModel(ds);
        }

        /// <summary>
        /// 根据供应商ID和一级产品类型ID获得二级产品类型列表
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <param name="level1Id">The level1 id.</param>
        /// <returns></returns>
        public static List<TypeInfo> getLevel2ListBySupplierIdAndLevel1ID(int supplierId, int level1Id)
        {
            List<TypeInfo> list = new List<TypeInfo>();
            string strSql = @"select DISTINCT d.* from t_type as d inner join (SELECT DISTINCT a.typeid, a.typename, a.auditorid, a.parentId,a.shauditorid,a.gzauditorid,a.status,a.BJPaymentUserID,a.SHPaymentUserID,a.GZPaymentUserID
                                    FROM         T_Type AS a INNER JOIN
                                                              (SELECT     b.typeid
                                                                FROM          T_Product AS a INNER JOIN
                                                                                       T_Type AS b ON a.productType = b.typeid and b.status<>@status
                                                                WHERE      (a.supplierId = @supplierId)) AS c ON c.typeid = a.typeid
                                    ) as e on e.parentid=d.typeid
                                    where d.parentid=@level1Id and d.status<>@status";
            SqlParameter[] parms = { 
                    new SqlParameter("@supplierId",supplierId),
                    new SqlParameter("@level1Id",level1Id),
                    new SqlParameter("@status",State.typestatus_block) 
                                   };
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parms);
            return getModel(ds);
        }

        /// <summary>
        /// 根据供应商ID和二级产品类型ID获得三级产品类型列表
        /// </summary>
        /// <param name="supplierId">The supplier id.</param>
        /// <param name="level2Id">The level2 id.</param>
        /// <returns></returns>
        public static List<TypeInfo> getLevel3ListBySupplierIdAndLevel2ID(int supplierId, int level2Id)
        {
            List<TypeInfo> list = new List<TypeInfo>();
            string strSql = @"SELECT  distinct   a.typeid, a.typename, a.auditorid, a.parentId,a.typelevel,a.shauditorid,a.gzauditorid,a.status,a.operationflow,a.IsNeedHQCheck,a.BJPaymentUserID,a.SHPaymentUserID,a.GZPaymentUserID
                                    FROM         T_Type AS a INNER JOIN
                                                          T_Product AS b ON b.productType = a.typeid
                                    where b.supplierId=@supplierId and a.parentId = @level2Id and a.status<>@status";
            SqlParameter[] parms = { 
                    new SqlParameter("@supplierId",supplierId),
                    new SqlParameter("@level2Id",level2Id),
                    new SqlParameter("@status",State.typestatus_block)
                                   };
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parms);
            return getModel(ds);
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns></returns>
        private static List<TypeInfo> getModel(DataSet ds)
        {
            List<TypeInfo> list = new List<TypeInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TypeInfo model = new TypeInfo();
                    model.typeid = int.Parse(ds.Tables[0].Rows[i]["typeid"].ToString());
                    model.auditorid = ds.Tables[0].Rows[i]["auditorid"].ToString();
                    model.typename = ds.Tables[0].Rows[i]["typename"].ToString();
                    model.parentId = ds.Tables[0].Rows[i]["parentId"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[i]["parentId"].ToString());
                    model.typelevel = ds.Tables[0].Rows[i]["typelevel"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[i]["typelevel"].ToString());
                    model.shauditorid = ds.Tables[0].Rows[i]["shauditorid"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[i]["shauditorid"].ToString());
                    model.gzauditorid = ds.Tables[0].Rows[i]["gzauditorid"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[i]["gzauditorid"].ToString());
                    model.status = ds.Tables[0].Rows[i]["status"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[i]["status"].ToString());
                    model.operationflow = ds.Tables[0].Rows[i]["operationflow"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[i]["operationflow"].ToString());

                    model.BJPaymentUserID = ds.Tables[0].Rows[i]["BJPaymentUserID"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[i]["BJPaymentUserID"].ToString());
                    model.SHPaymentUserID = ds.Tables[0].Rows[i]["SHPaymentUserID"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[i]["SHPaymentUserID"].ToString());
                    model.GZPaymentUserID = ds.Tables[0].Rows[i]["GZPaymentUserID"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[i]["GZPaymentUserID"].ToString());
                    model.Sort = ds.Tables[0].Rows[i]["Sort"] == DBNull.Value ? 0 : int.Parse(ds.Tables[0].Rows[i]["Sort"].ToString());
                    list.Add(model);
                }
            }
            return list;
        }

        //取得所有上海分公司的物料审核人
        /// <summary>
        /// Gets the SH auditor.
        /// </summary>
        /// <returns></returns>
        public static string getSHAuditor()
        {
            string strSql = @"SELECT  distinct shauditorid FROM T_Type where shauditorid is not null and shauditorid >0 ";
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), null);
            string auditorlist = string.Empty;
            if(null != ds)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    auditorlist += dr["shauditorid"].ToString() + ",";
                }
            }
            if(auditorlist.Length > 0)
            {
                auditorlist = auditorlist.Substring(0, auditorlist.Length - 1);
            }
            return auditorlist;
        }

        //取得所有广州分公司的物料审核人
        /// <summary>
        /// Gets the GZ auditor.
        /// </summary>
        /// <returns></returns>
        public static string getGZAuditor()
        {
            string strSql = @"SELECT  distinct gzauditorid FROM T_Type where gzauditorid is not null and gzauditorid >0 ";
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), null);
            string auditorlist = string.Empty;
            if (null != ds)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    auditorlist += dr["gzauditorid"].ToString() + ",";
                }
            }
            if (auditorlist.Length > 0)
            {
                auditorlist = auditorlist.Substring(0, auditorlist.Length - 1);
            }
            return auditorlist;
        }

        public static DataTable getAllPaymentUser()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select paymentUserID from( ");
            strSql.Append(" select distinct bjpaymentuserid as paymentUserID from t_type ");
            strSql.Append(" union ");
            strSql.Append(" select distinct shpaymentuserid as paymentUserID from t_type ");
            strSql.Append(" union ");
            strSql.Append(" select distinct gzpaymentuserid as paymentUserID from t_type ");
            strSql.Append(" ) as a where a.paymentUserID<>0" );
            DataTable newDt = new DataTable();
            DataColumn dc1 = new DataColumn("paymentUserId");
            DataColumn dc2 = new DataColumn("paymentUserName");
            newDt.Columns.Add(dc1);
            newDt.Columns.Add(dc2);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                DataRow newDr = newDt.NewRow();
                newDr[0] = dr["paymentuserId"];
                newDr[1] = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(dr["paymentuserId"].ToString())).FullNameCN;
                newDt.Rows.Add(newDr);
            }
            return newDt;
        }
        #endregion  成员方法
    }
}