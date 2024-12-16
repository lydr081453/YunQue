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
    public class SC_TypeDataProvider
    {
        public SC_TypeDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SC_Type");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Type model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_Type(");
            strSql.Append("TypeName,TypeShortName,Path,parentId,typelevel,CreatTime,LastUpdateTime,Type,Status,BJAuditorId,BJAuditor,SHAuditorId,SHAuditor,GZAuditorId,GZAuditor)");
            strSql.Append(" values (");
            strSql.Append("@TypeName,@TypeShortName,@Path,@parentId,@typelevel,@CreatTime,@LastUpdateTime,@Type,@Status,@BJAuditorId,@BJAuditor,@SHAuditorId,@SHAuditor,@GZAuditorId,@GZAuditor)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,200),
					new SqlParameter("@TypeShortName", SqlDbType.NVarChar,10),
					new SqlParameter("@Path", SqlDbType.NVarChar,500),
					new SqlParameter("@parentId", SqlDbType.Int,4),
					new SqlParameter("@typelevel", SqlDbType.Int,4),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@BJAuditorId", SqlDbType.Int,4),
					new SqlParameter("@BJAuditor", SqlDbType.NVarChar,50),
					new SqlParameter("@SHAuditorId", SqlDbType.Int,4),
					new SqlParameter("@SHAuditor", SqlDbType.NVarChar,50),
					new SqlParameter("@GZAuditorId", SqlDbType.Int,4),
					new SqlParameter("@GZAuditor", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.TypeName;
            parameters[1].Value = model.TypeShortName;
            parameters[2].Value = model.Path;
            parameters[3].Value = model.parentId;
            parameters[4].Value = model.typelevel;
            parameters[5].Value = model.CreatTime;
            parameters[6].Value = model.LastUpdateTime;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.BJAuditorId;
            parameters[10].Value = model.BJAuditor;
            parameters[11].Value = model.SHAuditorId;
            parameters[12].Value = model.SHAuditor;
            parameters[13].Value = model.GZAuditorId;
            parameters[14].Value = model.GZAuditor;

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
        public void Update(SC_Type model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_Type set ");
            strSql.Append("TypeName=@TypeName,");
            strSql.Append("TypeShortName=@TypeShortName,");
            strSql.Append("Path=@Path,");
            strSql.Append("parentId=@parentId,");
            strSql.Append("typelevel=@typelevel,");
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("BJAuditorId=@BJAuditorId,");
            strSql.Append("BJAuditor=@BJAuditor,");
            strSql.Append("SHAuditorId=@SHAuditorId,");
            strSql.Append("SHAuditor=@SHAuditor,");
            strSql.Append("GZAuditorId=@GZAuditorId,");
            strSql.Append("GZAuditor=@GZAuditor");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,200),
					new SqlParameter("@TypeShortName", SqlDbType.NVarChar,10),
					new SqlParameter("@Path", SqlDbType.NVarChar,500),
					new SqlParameter("@parentId", SqlDbType.Int,4),
					new SqlParameter("@typelevel", SqlDbType.Int,4),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@BJAuditorId", SqlDbType.Int,4),
					new SqlParameter("@BJAuditor", SqlDbType.NVarChar,50),
					new SqlParameter("@SHAuditorId", SqlDbType.Int,4),
					new SqlParameter("@SHAuditor", SqlDbType.NVarChar,50),
					new SqlParameter("@GZAuditorId", SqlDbType.Int,4),
					new SqlParameter("@GZAuditor", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.TypeName;
            parameters[2].Value = model.TypeShortName;
            parameters[3].Value = model.Path;
            parameters[4].Value = model.parentId;
            parameters[5].Value = model.typelevel;
            parameters[6].Value = model.CreatTime;
            parameters[7].Value = model.LastUpdateTime;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.BJAuditorId;
            parameters[11].Value = model.BJAuditor;
            parameters[12].Value = model.SHAuditorId;
            parameters[13].Value = model.SHAuditor;
            parameters[14].Value = model.GZAuditorId;
            parameters[15].Value = model.GZAuditor;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_Type ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Type GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,TypeName,TypeShortName,Path,parentId,typelevel,CreatTime,LastUpdateTime,Type,Status,BJAuditorId,BJAuditor,SHAuditorId,SHAuditor,GZAuditorId,GZAuditor from SC_Type ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            SC_Type model = new SC_Type();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.TypeName = ds.Tables[0].Rows[0]["TypeName"].ToString();
                model.TypeShortName = ds.Tables[0].Rows[0]["TypeShortName"].ToString();
                model.Path = ds.Tables[0].Rows[0]["Path"].ToString();
                if (ds.Tables[0].Rows[0]["parentId"].ToString() != "")
                {
                    model.parentId = int.Parse(ds.Tables[0].Rows[0]["parentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["typelevel"].ToString() != "")
                {
                    model.typelevel = int.Parse(ds.Tables[0].Rows[0]["typelevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatTime"].ToString() != "")
                {
                    model.CreatTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
                }
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
                if (ds.Tables[0].Rows[0]["BJAuditorId"].ToString() != "")
                {
                    model.BJAuditorId = int.Parse(ds.Tables[0].Rows[0]["BJAuditorId"].ToString());
                }
                model.BJAuditor = ds.Tables[0].Rows[0]["BJAuditor"].ToString();
                if (ds.Tables[0].Rows[0]["SHAuditorId"].ToString() != "")
                {
                    model.SHAuditorId = int.Parse(ds.Tables[0].Rows[0]["SHAuditorId"].ToString());
                }
                model.SHAuditor = ds.Tables[0].Rows[0]["SHAuditor"].ToString();
                if (ds.Tables[0].Rows[0]["GZAuditorId"].ToString() != "")
                {
                    model.GZAuditorId = int.Parse(ds.Tables[0].Rows[0]["GZAuditorId"].ToString());
                }
                model.GZAuditor = ds.Tables[0].Rows[0]["GZAuditor"].ToString();
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
            strSql.Append("select Id,TypeName,TypeShortName,Path,parentId,typelevel,CreatTime,LastUpdateTime,Type,Status,BJAuditorId,BJAuditor,SHAuditorId,SHAuditor,GZAuditorId,GZAuditor ");
            strSql.Append(" FROM SC_Type ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_Type> GetAllL2Lists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_Type>(GetList(" typelevel=2"));

        }

        //只取得supplier已有的2级物料
        public static List<SC_Type> GetSupplierL2TypeListBySupplierId(int sid, int tid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.* from SC_Type as a inner join SC_SupplierType as b on a.id=b.typeid where b.supplierid=@supplierid and a.parentid=@parentid ");

            SqlParameter[] parameters = {
                                            new SqlParameter("@supplierid", SqlDbType.Int, 4),
                                            new SqlParameter("@parentid", SqlDbType.Int, 4) 
                                        };
            parameters[0].Value = sid;
            parameters[1].Value = tid;


            return ESP.ConfigCommon.CBO.FillCollection<SC_Type>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public static List<SC_Type> GetListByParentId(int pid)
        {
            string strWhere = string.Empty;
            strWhere += " parentId=@parentId";
            SqlParameter[] parameters = { new SqlParameter("@parentId", SqlDbType.Int, 4) };
            parameters[0].Value = pid;

            return GetList(strWhere, parameters);
        }

        public static List<SC_Type> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_Type ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_Type>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        //只取得supplier已有的所有的3级物料
        public DataSet GetSupplierL3TypeListBySupplierId(int sid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select t.* from sc_type t join SC_SupplierType st on t.id = st.typeid ");
            strSql.Append(" where t.typelevel=3 and st.SupplierId=@sid");
            SqlParameter[] parameters = {
					new SqlParameter("@sid", SqlDbType.Int,4)};
            parameters[0].Value = sid;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public static List<SC_Type> GetLevel1List()
        {
            string strWhere = string.Empty;
            strWhere += " typelevel=@typelevel";

            SqlParameter[] parameters = {
					new SqlParameter("@typelevel", SqlDbType.Int,4)};
            parameters[0].Value = 1;

            return GetList(strWhere, parameters);
        }
        #endregion  成员方法
    }
}
