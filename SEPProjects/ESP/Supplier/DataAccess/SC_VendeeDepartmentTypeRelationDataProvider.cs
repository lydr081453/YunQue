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
    public class SC_VendeeDepartmentTypeRelationDataProvider
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_VendeeDepartmentTypeRelation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_VendeeDepartmentTypeRelation(");
            strSql.Append("[CompanyId],[CompanyDepartmentID] ,[TypeId],[CreatTime],[CreatIP],[LastUpdateTime],[LastUpdateIP] ,[Type] ,[Status])");
            strSql.Append(" values (");
            strSql.Append("@CompanyId,@CompanyDepartmentID ,@TypeId,@CreatTime,@CreatIP,@LastUpdateTime,@LastUpdateIP ,@Type ,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyId", SqlDbType.Int,4),
					new SqlParameter("@CompanyDepartmentID", SqlDbType.NVarChar),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.CompanyId;
            parameters[1].Value = model.CompanyDepartmentID;
            parameters[2].Value = model.TypeId;
            parameters[3].Value = model.CreatTime;
            parameters[4].Value = model.CreatIP;
            parameters[5].Value = model.LastUpdateTime;
            parameters[6].Value = model.LastUpdateIP;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.Status;

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
        /// 增加一条数据
        /// </summary>
        public int Add(SC_VendeeDepartmentTypeRelation model, SqlTransaction trans, SqlConnection conn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_VendeeDepartmentTypeRelation(");
            strSql.Append("[CompanyId],[CompanyDepartmentID] ,[TypeId],[CreatTime],[CreatIP],[LastUpdateTime],[LastUpdateIP] ,[Type] ,[Status])");
            strSql.Append(" values (");
            strSql.Append("@CompanyId,@CompanyDepartmentID ,@TypeId,@CreatTime,@CreatIP,@LastUpdateTime,@LastUpdateIP ,@Type ,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyId", SqlDbType.Int,4),
					new SqlParameter("@CompanyDepartmentID", SqlDbType.NVarChar),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.CompanyId;
            parameters[1].Value = model.CompanyDepartmentID;
            parameters[2].Value = model.TypeId;
            parameters[3].Value = model.CreatTime;
            parameters[4].Value = model.CreatIP;
            parameters[5].Value = model.LastUpdateTime;
            parameters[6].Value = model.LastUpdateIP;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.Status;

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
        public void Update(SC_VendeeDepartmentTypeRelation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_VendeeDepartmentTypeRelation set ");
            strSql.Append("CompanyId=@CompanyId,");
            strSql.Append("CompanyDepartmentID=@CompanyDepartmentID,");
            strSql.Append("TypeId=@TypeId,");
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("CreatIP=@CreatIP,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("LastUpdateIP=@LastUpdateIP,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@CompanyId", SqlDbType.Int,4),
					new SqlParameter("@CompanyDepartmentID", SqlDbType.NVarChar),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@CreatIP", SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastUpdateIP", SqlDbType.NVarChar),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.CompanyId;
            parameters[2].Value = model.CompanyDepartmentID;
            parameters[3].Value = model.TypeId;
            parameters[4].Value = model.CreatTime;
            parameters[5].Value = model.CreatIP;
            parameters[6].Value = model.LastUpdateTime;
            parameters[7].Value = model.LastUpdateIP;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.Status;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int CompanyDepartmentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_VendeeDepartmentTypeRelation ");
            strSql.Append(" where CompanyDepartmentID=@CompanyDepartmentID");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyDepartmentID", SqlDbType.Int,4)
				};
            parameters[0].Value = CompanyDepartmentID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据供应商ID和得分类型删除一组数据
        /// </summary>
        public void DeleteByCompanyDepartmentID(int CompanyDepartmentID, int TypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_VendeeDepartmentTypeRelation ");
            strSql.Append(" where CompanyDepartmentID=@CompanyDepartmentID and TypeId=@TypeId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CompanyDepartmentID", SqlDbType.Int,4),
                    new SqlParameter("@TypeId",SqlDbType.Int,4)
                };
            parameters[0].Value = CompanyDepartmentID;
            parameters[1].Value = TypeId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据供应商ID和得分类型删除一组数据
        /// </summary>
        //public void DeleteBySupplierId(int supplierid, int scoretype, SqlTransaction trans, SqlConnection conn)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("delete SC_VendeeDepartmentTypeRelation ");
        //    strSql.Append(" where supplierid=@supplierid and scoretype=@scoretype");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@supplierid", SqlDbType.Int,4),
        //            new SqlParameter("@scoretype",SqlDbType.Int,4)
        //        };
        //    parameters[0].Value = supplierid;
        //    parameters[1].Value = scoretype;
        //    DbHelperSQL.ExecuteSql(strSql.ToString(), conn, trans, parameters);
        //}


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_VendeeDepartmentTypeRelation GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_VendeeDepartmentTypeRelation ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;
            SC_VendeeDepartmentTypeRelation model = new SC_VendeeDepartmentTypeRelation();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.Id = Id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CompanyId"].ToString() != "")
                {
                    model.CompanyId = int.Parse(ds.Tables[0].Rows[0]["CompanyId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CompanyDepartmentID"].ToString() != "")
                {
                    model.CompanyDepartmentID = ds.Tables[0].Rows[0]["CompanyDepartmentID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TypeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatTime"].ToString() != "")
                {
                    model.CreatTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
                }
                model.CreatIP = ds.Tables[0].Rows[0]["CreatIP"].ToString();
                if (ds.Tables[0].Rows[0]["LastUpdateTime"].ToString() != "")
                {
                    model.LastUpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateTime"].ToString());
                }
                model.LastUpdateIP = ds.Tables[0].Rows[0]["LastUpdateIP"].ToString();
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
        //public DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select [Id],[CompanyId],[CompanyDepartmentID] ,[TypeId],[CreatTime],[CreatIP],[LastUpdateTime],[LastUpdateIP] ,[Type] ,[Status] ");
        //    strSql.Append(" FROM SC_VendeeDepartmentTypeRelation ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    return DbHelperSQL.Query(strSql.ToString());
        //}

        public List<SC_VendeeDepartmentTypeRelation> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [Id],[CompanyId],[CompanyDepartmentID] ,[TypeId],[CreatTime],[CreatIP],[LastUpdateTime],[LastUpdateIP] ,[Type] ,[Status] ");
            strSql.Append(" FROM SC_VendeeDepartmentTypeRelation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection < SC_VendeeDepartmentTypeRelation > (DbHelperSQL.Query(strSql.ToString()));
        }

        public List<SC_VendeeDepartmentTypeRelation> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_VendeeDepartmentTypeRelation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_VendeeDepartmentTypeRelation>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public List<SC_VendeeDepartmentTypeRelation> GetListByDepartmentId(int did)
        {
            string strWhere = string.Empty;
            strWhere += " [CompanyDepartmentID]=@DepartmentId";
            SqlParameter[] parameters = { new SqlParameter("@DepartmentId", SqlDbType.Int, 4) };
            parameters[0].Value = did;

            return GetList(strWhere, parameters);
        }

        public List<SC_VendeeDepartmentTypeRelation> GetListBySupplierId(int cid, int typeid)
        {
            string strWhere = string.Empty;
            strWhere += " CompanyId=@CompanyId and TypeId=@TypeId";
            SqlParameter[] parameters = { new SqlParameter("@CompanyId", SqlDbType.Int, 4),
                                        new SqlParameter("@TypeId",SqlDbType.Int,4)};
            parameters[0].Value = cid;
            parameters[1].Value = typeid;

            return GetList(strWhere, parameters);
        }

        #endregion  成员方法

    }
}
