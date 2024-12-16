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
    public class XML_VersionListDataProvider
    {
        public XML_VersionListDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XML_VersionList");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XML_VersionList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XML_VersionList(");
            strSql.Append("Name,TableName,Url,Content,Version,InsertUser,InsertTime,InsertIP,UpdateUser,UpdateTime,UpdateIP,ClassID,XML,BJAuditorId,SHAuditorId,GZAuditorId,BJAuditor,SHAuditor,GZAuditor,Type,Status)");
            strSql.Append(" values (");
            strSql.Append("@Name,@TableName,@Url,@Content,@Version,@InsertUser,@InsertTime,@InsertIP,@UpdateUser,@UpdateTime,@UpdateIP,@ClassID,@XML,@BJAuditorId,@SHAuditorId,@GZAuditorId,@BJAuditor,@SHAuditor,@GZAuditor,@Type,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@TableName", SqlDbType.NVarChar,50),
					new SqlParameter("@Url", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@Version", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertUser", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertTime", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertIP", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateIP", SqlDbType.NVarChar,100),
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@XML", SqlDbType.NText),
                    new SqlParameter("@BJAuditorId", SqlDbType.Int,4),
                    new SqlParameter("@SHAuditorId", SqlDbType.Int,4),
                    new SqlParameter("@GZAuditorId", SqlDbType.Int,4),
                    new SqlParameter("@BJAuditor", SqlDbType.NVarChar,50),
                    new SqlParameter("@SHAuditor", SqlDbType.NVarChar,50),
                    new SqlParameter("@GZAuditor", SqlDbType.NVarChar,50),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.TableName;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.Content;
            parameters[4].Value = model.Version;
            parameters[5].Value = model.InsertUser;
            parameters[6].Value = model.InsertTime;
            parameters[7].Value = model.InsertIP;
            parameters[8].Value = model.UpdateUser;
            parameters[9].Value = model.UpdateTime;
            parameters[10].Value = model.UpdateIP;
            parameters[11].Value = model.ClassID;
            parameters[12].Value = model.XML;
            parameters[13].Value = model.BJAuditorId;
            parameters[14].Value = model.SHAuditorId;
            parameters[15].Value = model.GZAuditorId;
            parameters[16].Value = model.BJAuditor;
            parameters[17].Value = model.SHAuditor;
            parameters[18].Value = model.GZAuditor;
            parameters[19].Value = model.Type;
            parameters[20].Value = model.Status;
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
        public void Update(XML_VersionList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XML_VersionList set ");
            strSql.Append("Name=@Name,");
            strSql.Append("TableName=@TableName,");
            strSql.Append("Url=@Url,");
            strSql.Append("Content=@Content,");
            strSql.Append("Version=@Version,");
            strSql.Append("InsertUser=@InsertUser,");
            strSql.Append("InsertTime=@InsertTime,");
            strSql.Append("InsertIP=@InsertIP,");
            strSql.Append("UpdateUser=@UpdateUser,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("UpdateIP=@UpdateIP,");
            strSql.Append("ClassID=@ClassID,");
            strSql.Append("XML=@XML,");
            strSql.Append("BJAuditorId=@BJAuditorId,");
            strSql.Append("SHAuditorId=@SHAuditorId,");
            strSql.Append("GZAuditorId=@GZAuditorId,");
            strSql.Append("BJAuditor=@BJAuditor,");
            strSql.Append("SHAuditor=@SHAuditor,");
            strSql.Append("GZAuditor=@GZAuditor,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("State=@State");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@TableName", SqlDbType.NVarChar,50),
					new SqlParameter("@Url", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@Version", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertUser", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertTime", SqlDbType.NVarChar,100),
					new SqlParameter("@InsertIP", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateUser", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateTime", SqlDbType.NVarChar,100),
					new SqlParameter("@UpdateIP", SqlDbType.NVarChar,100),
					new SqlParameter("@ClassID", SqlDbType.Int,4),
					new SqlParameter("@XML", SqlDbType.NText),
                    new SqlParameter("@BJAuditorId", SqlDbType.Int,4),
                    new SqlParameter("@SHAuditorId", SqlDbType.Int,4),
                    new SqlParameter("@GZAuditorId", SqlDbType.Int,4),
                    new SqlParameter("@BJAuditor", SqlDbType.NVarChar,50),
                    new SqlParameter("@SHAuditor", SqlDbType.NVarChar,50),
                    new SqlParameter("@GZAuditor", SqlDbType.NVarChar,50),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@State", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.TableName;
            parameters[3].Value = model.Url;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.Version;
            parameters[6].Value = model.InsertUser;
            parameters[7].Value = model.InsertTime;
            parameters[8].Value = model.InsertIP;
            parameters[9].Value = model.UpdateUser;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.UpdateIP;
            parameters[12].Value = model.ClassID;
            parameters[13].Value = model.XML;
            parameters[14].Value = model.BJAuditorId;
            parameters[15].Value = model.SHAuditorId;
            parameters[16].Value = model.GZAuditorId;
            parameters[17].Value = model.BJAuditor;
            parameters[18].Value = model.SHAuditor;
            parameters[19].Value = model.GZAuditor;
            parameters[20].Value = model.Type;
            parameters[21].Value = model.Status;
            parameters[22].Value = model.State;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XML_VersionList ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void Delete(int typeid, int level, int updateStatus)
        {
            if (level == 1 || level == 2)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" update XML_VersionClass set ");
                strSql.Append(" state=@state where");
                if (level == 1)
                {
                    strSql.Append(" id=" + typeid + " or parentid=" + typeid);
                }
                if (level == 2)
                {
                    strSql.Append(" id=" + typeid);
                }

                SqlParameter[] parameters = {
                    new SqlParameter("@state",SqlDbType.Int,4)
                };
                parameters[0].Value = updateStatus;
                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append(" update XML_VersionList set ");
            strSql1.Append(" state=@state1 where");
            if (level == 1)
            {
                strSql1.Append(" classid in (select id from XML_VersionClass where parentid = "+typeid+") ");
            }
            if (level == 2)
            {
                strSql1.Append(" classid in ( select id from XML_VersionClass where id="+typeid+")");
            }
            if (level == 3)
            {
                strSql1.Append(" id=" + typeid);
            }
           
            SqlParameter[] parameters1 = {
                    new SqlParameter("@state1",SqlDbType.Int,4)
                };
            parameters1[0].Value = updateStatus;
            DbHelperSQL.ExecuteSql(strSql1.ToString(), parameters1);


            StringBuilder strSqlStype = new StringBuilder();
            strSqlStype.Append(" update SC_SupplierType set ");
            strSqlStype.Append(" state=@stateST where ");

            StringBuilder strSqlVtype = new StringBuilder();
            strSqlVtype.Append(" update SC_VendeeTypeRelation set ");
            strSqlVtype.Append(" state=@stateVT where ");
            if (level == 1)
            {
                strSqlStype.Append(" id in (select id from dbo.SC_SupplierType where typelv=1 and typeid="+ typeid);
                strSqlStype.Append(" union ");
                strSqlStype.Append(" select id from SC_SupplierType where typelv=2 and typeid in ( select id from XML_VersionClass where parentid="+typeid+") ");
                strSqlStype.Append(" union ");
                strSqlStype.Append(" select id from SC_SupplierType where typelv=3 and typeid in( select id from XML_VersionList where classid in ");
                strSqlStype.Append(" ( select id from XML_VersionClass where parentid="+typeid+")))");

                strSqlVtype.Append(" typeid in(select id from XML_VersionClass where parentid=" + typeid + ") ");
            }
            if (level == 2)
            {
                strSqlStype.Append(" id in (select id from SC_SupplierType where typelv=2 and typeid ="+typeid);
                strSqlStype.Append(" union ");
                strSqlStype.Append(" select id from SC_SupplierType where typelv=3 and typeid in( select id from XML_VersionList where classid ="+typeid+" ))");

                strSqlVtype.Append(" typeid ="+typeid);
            }
            if (level == 3)
            {
                strSqlStype.Append(" typelv=3 and typeid="+typeid);
            }

            SqlParameter[] parametersStype = {
                    new SqlParameter("@stateST",SqlDbType.Int,4)
                };
            parametersStype[0].Value = updateStatus;
            DbHelperSQL.ExecuteSql(strSqlStype.ToString(), parametersStype);

            if (level == 1 || level == 2)
            {
                SqlParameter[] parametersVtype = {
                    new SqlParameter("@stateVT",SqlDbType.Int,4)
                };
                parametersVtype[0].Value = updateStatus;
                DbHelperSQL.ExecuteSql(strSqlVtype.ToString(), parametersVtype);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XML_VersionList GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Name,TableName,Url,Content,Version,InsertUser,InsertTime,InsertIP,UpdateUser,UpdateTime,UpdateIP,ClassID,XML,BJAuditorId,BJAuditor,SHAuditorId,SHAuditor,GZAuditorId,GZAuditor,state from XML_VersionList ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XML_VersionList model = new XML_VersionList();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.TableName = ds.Tables[0].Rows[0]["TableName"].ToString();
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                model.Version = ds.Tables[0].Rows[0]["Version"].ToString();
                model.InsertUser = ds.Tables[0].Rows[0]["InsertUser"].ToString();
                model.InsertTime = ds.Tables[0].Rows[0]["InsertTime"].ToString();
                model.InsertIP = ds.Tables[0].Rows[0]["InsertIP"].ToString();
                model.UpdateUser = ds.Tables[0].Rows[0]["UpdateUser"].ToString();
                model.UpdateTime = ds.Tables[0].Rows[0]["UpdateTime"].ToString();
                model.UpdateIP = ds.Tables[0].Rows[0]["UpdateIP"].ToString();
                model.BJAuditorId = int.Parse(ds.Tables[0].Rows[0]["BJAuditorId"].ToString());
                model.BJAuditor = ds.Tables[0].Rows[0]["BJAuditor"].ToString();
                model.SHAuditorId = int.Parse(ds.Tables[0].Rows[0]["SHAuditorId"].ToString());
                model.SHAuditor = ds.Tables[0].Rows[0]["SHAuditor"].ToString();
                model.GZAuditorId = int.Parse(ds.Tables[0].Rows[0]["GZAuditorId"].ToString());
                model.GZAuditor = ds.Tables[0].Rows[0]["GZAuditor"].ToString();
                if (ds.Tables[0].Rows[0]["ClassID"].ToString() != "")
                {
                    model.ClassID = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
                }
                model.XML = ds.Tables[0].Rows[0]["XML"].ToString();
                if (ds.Tables[0].Rows[0]["state"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["state"].ToString());
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
            strSql.Append("select ID,Name,TableName,Url,Content,Version,InsertUser,InsertTime,InsertIP,UpdateUser,UpdateTime,UpdateIP,ClassID,XML,BJAuditorId,BJAuditor,SHAuditorId,SHAuditor,GZAuditorId,GZAuditor ");
            strSql.Append(" FROM XML_VersionList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int used)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Name,TableName,Url,Content,Version,InsertUser,InsertTime,InsertIP,UpdateUser,UpdateTime,UpdateIP,ClassID,XML,BJAuditorId,BJAuditor,SHAuditorId,SHAuditor,GZAuditorId,GZAuditor ");
            strSql.Append(" FROM XML_VersionList ");
            strSql.Append(" where state=1 " );

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
            strSql.Append(" ID,Name,TableName,Url,Content,Version,InsertUser,InsertTime,InsertIP,UpdateUser,UpdateTime,UpdateIP,ClassID,XML ");
            strSql.Append(" FROM XML_VersionList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //只取得supplier已有的所有的3级物料
        public List<XML_VersionList> GetChooseList(int sid)
        {  
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select vl.* from xml_versionlist vl join sc_suppliertype st on vl.id = st.typeid where st.typelv=3 and st.supplierid=" + sid);
                return ESP.ConfigCommon.CBO.FillCollection<XML_VersionList>(DbHelperSQL.Query(strSql.ToString()));
            
        }

        //只取得supplier已有的所有的3级物料
        public List<XML_VersionList> GetChooseList(int sid, int pid)
        {           

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select vl.* from xml_versionlist vl join sc_suppliertype st on vl.id = st.typeid where st.typelv=3 and vl.classid="+pid+" and st.supplierid=" + sid);
            return ESP.ConfigCommon.CBO.FillCollection<XML_VersionList>(DbHelperSQL.Query(strSql.ToString()));
        }

        #endregion  成员方法
    }
}
