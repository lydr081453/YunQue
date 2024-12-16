using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;


namespace ESP.Purchase.DataAccess
{
    /// <summary>
    /// 数据访问类T_Watch。
    /// </summary>
    public class BaiduDataProvider
    {
        public BaiduDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Watch");
            strSql.Append(" where id= @id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Purchase.Entity.BaiduInfo model,string conn)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                DataSet ds = new DataSet();
                try
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into t_baidu(");
                    strSql.Append("url,title,keywordid,[key],linkPos,urlHash,urlFrom,dTime,uid,nameSite,varTime,mode,zid,doc,ext1,ext2,ext3,ext4,ext5,status,keys,createTime,isAppoint,dataType)");
                    strSql.Append(" values (");
                    strSql.Append("@url,@title,@keywordid,@key,@linkPos,@urlHash,@urlFrom,@dTime,@uid,@nameSite,@varTime,@mode,@zid,@doc,@ext1,@ext2,@ext3,@ext4,@ext5,@status,@keys,@createTime,@isAppoint,@dataType)");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@url", SqlDbType.VarChar,450),
					new SqlParameter("@title", SqlDbType.NVarChar),
					new SqlParameter("@key", SqlDbType.NVarChar),
					new SqlParameter("@linkPos", SqlDbType.SmallInt,2),
					new SqlParameter("@urlHash", SqlDbType.VarChar,50),
					new SqlParameter("@urlFrom", SqlDbType.NVarChar,4000),
					new SqlParameter("@dTime", SqlDbType.SmallDateTime),
					new SqlParameter("@uid", SqlDbType.VarChar,50),
					new SqlParameter("@nameSite", SqlDbType.NVarChar),
					new SqlParameter("@varTime", SqlDbType.VarChar,50),
					new SqlParameter("@mode", SqlDbType.NVarChar),
					new SqlParameter("@zid", SqlDbType.VarChar,100),
					new SqlParameter("@doc", SqlDbType.NVarChar),
					new SqlParameter("@ext1", SqlDbType.VarChar,20),
					new SqlParameter("@ext2", SqlDbType.VarChar,20),
					new SqlParameter("@ext3", SqlDbType.VarChar,20),
					new SqlParameter("@ext4", SqlDbType.VarChar,20),
					new SqlParameter("@ext5", SqlDbType.VarChar,20),
					new SqlParameter("@status", SqlDbType.SmallInt,2),
					new SqlParameter("@keys", SqlDbType.NVarChar),
					new SqlParameter("@createTime", SqlDbType.SmallDateTime),
					new SqlParameter("@isAppoint", SqlDbType.SmallInt,2),
					new SqlParameter("@dataType", SqlDbType.SmallInt,2),
                    new SqlParameter("@keywordid", SqlDbType.SmallInt,2)};
                    parameters[0].Value = model.url;
                    parameters[1].Value = model.title;
                    parameters[2].Value = model.key;
                    parameters[3].Value = model.linkPos;
                    parameters[4].Value = model.urlHash;
                    parameters[5].Value = model.urlFrom;
                    parameters[6].Value = model.dTime;
                    parameters[7].Value = model.uid;
                    parameters[8].Value = model.nameSite;
                    parameters[9].Value = model.varTime;
                    parameters[10].Value = model.mode;
                    parameters[11].Value = model.zid;
                    parameters[12].Value = model.doc;
                    parameters[13].Value = model.ext1;
                    parameters[14].Value = model.ext2;
                    parameters[15].Value = model.ext3;
                    parameters[16].Value = model.ext4;
                    parameters[17].Value = model.ext5;
                    parameters[18].Value = model.status;
                    parameters[19].Value = model.keys;
                    parameters[20].Value = model.createTime;
                    parameters[21].Value = model.isAppoint;
                    parameters[22].Value = model.dataType;
                    parameters[23].Value = model.Keywordid;

                    connection.Open();
                    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(strSql.ToString(), connection);
                    foreach (SqlParameter parameter in parameters)
                    {
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                    int ret = command.ExecuteNonQuery();
                    connection.Close();
                    return ret;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    connection.Close();
                    throw new Exception(ex.Message);
                }
            }

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Purchase.Entity.BaiduInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Watch set ");
            strSql.Append("url=@url,");
            strSql.Append("title=@title,");
            strSql.Append("[key]=@key,");
            strSql.Append("linkPos=@linkPos,");
            strSql.Append("urlHash=@urlHash,");
            strSql.Append("urlFrom=@urlFrom,");
            strSql.Append("dTime=@dTime,");
            strSql.Append("uid=@uid,");
            strSql.Append("nameSite=@nameSite,");
            strSql.Append("varTime=@varTime,");
            strSql.Append("mode=@mode,");
            strSql.Append("zid=@zid,");
            strSql.Append("doc=@doc,");
            strSql.Append("ext1=@ext1,");
            strSql.Append("ext2=@ext2,");
            strSql.Append("ext3=@ext3,");
            strSql.Append("ext4=@ext4,");
            strSql.Append("ext5=@ext5,");
            strSql.Append("status=@status,");
            strSql.Append("keys=@keys,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("isAppoint=@isAppoint,");
            strSql.Append("dataType=@dataType");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@url", SqlDbType.VarChar,450),
					new SqlParameter("@title", SqlDbType.NVarChar),
					new SqlParameter("@key", SqlDbType.NVarChar),
					new SqlParameter("@linkPos", SqlDbType.SmallInt,2),
					new SqlParameter("@urlHash", SqlDbType.VarChar,50),
					new SqlParameter("@urlFrom", SqlDbType.NVarChar),
					new SqlParameter("@dTime", SqlDbType.SmallDateTime),
					new SqlParameter("@uid", SqlDbType.VarChar,50),
					new SqlParameter("@nameSite", SqlDbType.NVarChar),
					new SqlParameter("@varTime", SqlDbType.VarChar,50),
					new SqlParameter("@mode", SqlDbType.NVarChar),
					new SqlParameter("@zid", SqlDbType.VarChar,100),
					new SqlParameter("@doc", SqlDbType.NVarChar),
					new SqlParameter("@ext1", SqlDbType.VarChar,20),
					new SqlParameter("@ext2", SqlDbType.VarChar,20),
					new SqlParameter("@ext3", SqlDbType.VarChar,20),
					new SqlParameter("@ext4", SqlDbType.VarChar,20),
					new SqlParameter("@ext5", SqlDbType.VarChar,20),
					new SqlParameter("@status", SqlDbType.SmallInt,2),
					new SqlParameter("@keys", SqlDbType.NVarChar),
					new SqlParameter("@createTime", SqlDbType.SmallDateTime),
					new SqlParameter("@isAppoint", SqlDbType.SmallInt,2),
					new SqlParameter("@dataType", SqlDbType.SmallInt,2)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.url;
            parameters[2].Value = model.title;
            parameters[3].Value = model.key;
            parameters[4].Value = model.linkPos;
            parameters[5].Value = model.urlHash;
            parameters[6].Value = model.urlFrom;
            parameters[7].Value = model.dTime;
            parameters[8].Value = model.uid;
            parameters[9].Value = model.nameSite;
            parameters[10].Value = model.varTime;
            parameters[11].Value = model.mode;
            parameters[12].Value = model.zid;
            parameters[13].Value = model.doc;
            parameters[14].Value = model.ext1;
            parameters[15].Value = model.ext2;
            parameters[16].Value = model.ext3;
            parameters[17].Value = model.ext4;
            parameters[18].Value = model.ext5;
            parameters[19].Value = model.status;
            parameters[20].Value = model.keys;
            parameters[21].Value = model.createTime;
            parameters[22].Value = model.isAppoint;
            parameters[23].Value = model.dataType;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Watch ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Purchase.Entity.BaiduInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_Watch ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            ESP.Purchase.Entity.BaiduInfo model = new ESP.Purchase.Entity.BaiduInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.id = id;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.url = ds.Tables[0].Rows[0]["url"].ToString();
                model.title = ds.Tables[0].Rows[0]["title"].ToString();
                model.key = ds.Tables[0].Rows[0]["key"].ToString();
                if (ds.Tables[0].Rows[0]["linkPos"].ToString() != "")
                {
                    model.linkPos = int.Parse(ds.Tables[0].Rows[0]["linkPos"].ToString());
                }
                model.urlHash = ds.Tables[0].Rows[0]["urlHash"].ToString();
                model.urlFrom = ds.Tables[0].Rows[0]["urlFrom"].ToString();
                if (ds.Tables[0].Rows[0]["dTime"].ToString() != "")
                {
                    model.dTime = DateTime.Parse(ds.Tables[0].Rows[0]["dTime"].ToString());
                }
                model.uid = ds.Tables[0].Rows[0]["uid"].ToString();
                model.nameSite = ds.Tables[0].Rows[0]["nameSite"].ToString();
                model.varTime = ds.Tables[0].Rows[0]["varTime"].ToString();
                model.mode = ds.Tables[0].Rows[0]["mode"].ToString();
                model.zid = ds.Tables[0].Rows[0]["zid"].ToString();
                model.doc = ds.Tables[0].Rows[0]["doc"].ToString();
                model.ext1 = ds.Tables[0].Rows[0]["ext1"].ToString();
                model.ext2 = ds.Tables[0].Rows[0]["ext2"].ToString();
                model.ext3 = ds.Tables[0].Rows[0]["ext3"].ToString();
                model.ext4 = ds.Tables[0].Rows[0]["ext4"].ToString();
                model.ext5 = ds.Tables[0].Rows[0]["ext5"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                model.keys = ds.Tables[0].Rows[0]["keys"].ToString();
                if (ds.Tables[0].Rows[0]["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(ds.Tables[0].Rows[0]["createTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isAppoint"].ToString() != "")
                {
                    model.isAppoint = int.Parse(ds.Tables[0].Rows[0]["isAppoint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["dataType"].ToString() != "")
                {
                    model.dataType = int.Parse(ds.Tables[0].Rows[0]["dataType"].ToString());
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
            strSql.Append("select [id],[url],[title],[key],[linkPos],[urlHash],[urlFrom],[dTime],[uid],[nameSite],[varTime],[mode],[zid],[doc],[ext1],[ext2],[ext3],[ext4],[ext5],[status],[keys],[createTime],[isAppoint],[dataType] ");
            strSql.Append(" FROM T_Watch ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by createTime desc ");
            return ESP.Purchase.Common.DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetListAdapter(string strWhere, int startRecord, int PageItem)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[url],[title],[key],[linkPos],[urlHash],[urlFrom],[dTime],[uid],[nameSite],[varTime],[mode],[zid],[doc],[ext1],[ext2],[ext3],[ext4],[ext5],[status],[keys],[createTime],[isAppoint],[dataType] ");
            strSql.Append(" FROM T_Watch ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by dTime desc ");
            SqlDataAdapter da = new SqlDataAdapter(strSql.ToString(), DbHelperSQL.connectionString);
            DataSet ds = new DataSet();
            
            da.Fill(ds, startRecord, PageItem, "a");
            return ds;
        }

        public int GetListCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM T_Watch ");
            strSql.Append(" where 1=1 " + strWhere);
            return int.Parse(DbHelperSQL.Query(strSql.ToString()).Tables[0].Rows[0][0].ToString());
        }

        public List<string> getAllUrlList()
        {
            string sql = "SELECT  url FROM T_Watch";
            DataTable dt = ESP.Purchase.Common.DbHelperSQL.Query(sql).Tables[0];
            List<string> urls = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                urls.Add(dt.Rows[i][0].ToString());
            }
            return urls;
        }

        public DataTable getTitleList()
        {
            string sql = "SELECT  id,title FROM T_Watch where status=0 ";
            DataTable dt = DbHelperSQL.Query(sql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 增加一条数据 Watch库
        /// </summary>
        public int AddWatch(ESP.Purchase.Entity.BaiduInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Watch(");
            strSql.Append("url,title,keywordid,[key],linkPos,urlHash,urlFrom,dTime,uid,nameSite,varTime,mode,zid,doc,ext1,ext2,ext3,ext4,ext5,status,createTime, [desc],deleted)");
            strSql.Append(" values (");
            strSql.Append("@url,@title,@keywordid,@key,@linkPos,@urlHash,@urlFrom,@dTime,@uid,@nameSite,@varTime,@mode,@zid,@doc,@ext1,@ext2,@ext3,@ext4,@ext5,@status,@createTime, @desc,@deleted)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@url", SqlDbType.VarChar,450),
					new SqlParameter("@title", SqlDbType.NVarChar),
					new SqlParameter("@key", SqlDbType.NVarChar),
					new SqlParameter("@linkPos", SqlDbType.SmallInt,2),
					new SqlParameter("@urlHash", SqlDbType.VarChar,50),
					new SqlParameter("@urlFrom", SqlDbType.NVarChar),
					new SqlParameter("@dTime", SqlDbType.SmallDateTime),
					new SqlParameter("@uid", SqlDbType.VarChar,50),
					new SqlParameter("@nameSite", SqlDbType.NVarChar),
					new SqlParameter("@varTime", SqlDbType.VarChar,50),
					new SqlParameter("@mode", SqlDbType.NVarChar),
					new SqlParameter("@zid", SqlDbType.VarChar,100),
					new SqlParameter("@doc", SqlDbType.NVarChar),
					new SqlParameter("@ext1", SqlDbType.VarChar,20),
					new SqlParameter("@ext2", SqlDbType.VarChar,20),
					new SqlParameter("@ext3", SqlDbType.VarChar,20),
					new SqlParameter("@ext4", SqlDbType.VarChar,20),
					new SqlParameter("@ext5", SqlDbType.VarChar,20),
					new SqlParameter("@status", SqlDbType.SmallInt,2),
					new SqlParameter("@createTime", SqlDbType.SmallDateTime),
                    new SqlParameter("@desc", SqlDbType.NVarChar),
                    new SqlParameter("@deleted", SqlDbType.NChar),
                    new SqlParameter("@keywordid", SqlDbType.Int)};
            parameters[0].Value = model.url;
            parameters[1].Value = model.title;
            parameters[2].Value = model.key;
            parameters[3].Value = model.linkPos;
            parameters[4].Value = model.urlHash;
            parameters[5].Value = model.urlFrom;
            parameters[6].Value = model.dTime;
            parameters[7].Value = model.uid;
            parameters[8].Value = model.nameSite;
            parameters[9].Value = model.varTime;
            parameters[10].Value = model.mode;
            parameters[11].Value = model.zid;
            parameters[12].Value = model.doc;
            parameters[13].Value = model.ext1;
            parameters[14].Value = model.ext2;
            parameters[15].Value = model.ext3;
            parameters[16].Value = model.ext4;
            parameters[17].Value = model.ext5;
            parameters[18].Value = model.status;
            parameters[19].Value = model.createTime;
            parameters[20].Value = model.desc;
            parameters[21].Value = model.Deleted;
            parameters[22].Value = model.Keywordid;

            object obj = ESP.Purchase.Common.DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public void UpdateWatch(ESP.Purchase.Entity.BaiduInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Watch set ");
            strSql.Append("url=@url,");
            strSql.Append("title=@title,");
            strSql.Append("[key]=@key,");
            strSql.Append("linkPos=@linkPos,");
            strSql.Append("urlHash=@urlHash,");
            strSql.Append("urlFrom=@urlFrom,");
            strSql.Append("dTime=@dTime,");
            strSql.Append("uid=@uid,");
            strSql.Append("nameSite=@nameSite,");
            strSql.Append("varTime=@varTime,");
            strSql.Append("mode=@mode,");
            strSql.Append("zid=@zid,");
            strSql.Append("doc=@doc,");
            strSql.Append("ext1=@ext1,");
            strSql.Append("ext2=@ext2,");
            strSql.Append("ext3=@ext3,");
            strSql.Append("ext4=@ext4,");
            strSql.Append("ext5=@ext5,");
            strSql.Append("status=@status,");
            strSql.Append("createTime=@createTime");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@url", SqlDbType.VarChar,450),
					new SqlParameter("@title", SqlDbType.NVarChar),
					new SqlParameter("@key", SqlDbType.NVarChar),
					new SqlParameter("@linkPos", SqlDbType.SmallInt,2),
					new SqlParameter("@urlHash", SqlDbType.VarChar,50),
					new SqlParameter("@urlFrom", SqlDbType.NVarChar),
					new SqlParameter("@dTime", SqlDbType.SmallDateTime),
					new SqlParameter("@uid", SqlDbType.VarChar,50),
					new SqlParameter("@nameSite", SqlDbType.NVarChar),
					new SqlParameter("@varTime", SqlDbType.VarChar,50),
					new SqlParameter("@mode", SqlDbType.NVarChar),
					new SqlParameter("@zid", SqlDbType.VarChar,100),
					new SqlParameter("@doc", SqlDbType.NVarChar),
					new SqlParameter("@ext1", SqlDbType.VarChar,20),
					new SqlParameter("@ext2", SqlDbType.VarChar,20),
					new SqlParameter("@ext3", SqlDbType.VarChar,20),
					new SqlParameter("@ext4", SqlDbType.VarChar,20),
					new SqlParameter("@ext5", SqlDbType.VarChar,20),
					new SqlParameter("@status", SqlDbType.SmallInt,2),
					new SqlParameter("@createTime", SqlDbType.SmallDateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.url;
            parameters[2].Value = model.title;
            parameters[3].Value = model.key;
            parameters[4].Value = model.linkPos;
            parameters[5].Value = model.urlHash;
            parameters[6].Value = model.urlFrom;
            parameters[7].Value = model.dTime;
            parameters[8].Value = model.uid;
            parameters[9].Value = model.nameSite;
            parameters[10].Value = model.varTime;
            parameters[11].Value = model.mode;
            parameters[12].Value = model.zid;
            parameters[13].Value = model.doc;
            parameters[14].Value = model.ext1;
            parameters[15].Value = model.ext2;
            parameters[16].Value = model.ext3;
            parameters[17].Value = model.ext4;
            parameters[18].Value = model.ext5;
            parameters[19].Value = model.status;
            parameters[20].Value = model.createTime;

            ESP.Purchase.Common.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public DataSet GetWatchList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[url],[title],[key],[linkPos],[urlHash],[urlFrom],[dTime],[uid],[nameSite],[varTime],[mode],[zid],[doc],[ext1],[ext2],[ext3],[ext4],[ext5],[status],[createTime],[desc] ");
            strSql.Append(" FROM T_Watch ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by dTime desc");
            return ESP.Purchase.Common.DbHelperSQL.Query(strSql.ToString());
        }

        public int GetWatchListCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(id) as countid ");
            strSql.Append(" FROM T_Watch ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  " + strWhere);
            }
            DataTable dt = ESP.Purchase.Common.DbHelperSQL.Query(strSql.ToString()).Tables[0];
            int countid = 0;
            countid = int.Parse(dt.Rows[0]["countid"].ToString());
            return countid;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteWatchInfo(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Watch ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            ESP.Purchase.Common.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void DeleteWatchInfo(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Watch ");
            strSql.Append(" where 1=1 ").Append(sqlWhere);
            ESP.Purchase.Common.DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据 Watch库
        /// </summary>
        public int AddSearchEngine(ESP.Purchase.Entity.BaiduInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SearchEngine(");
            strSql.Append("url,title,[key],linkPos,urlHash,urlFrom,dTime,uid,nameSite,varTime,mode,zid,doc,ext1,ext2,ext3,ext4,ext5,status,createTime)");
            strSql.Append(" values (");
            strSql.Append("@url,@title,@key,@linkPos,@urlHash,@urlFrom,@dTime,@uid,@nameSite,@varTime,@mode,@zid,@doc,@ext1,@ext2,@ext3,@ext4,@ext5,@status,@createTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@url", SqlDbType.VarChar,450),
					new SqlParameter("@title", SqlDbType.NVarChar),
					new SqlParameter("@key", SqlDbType.NVarChar),
					new SqlParameter("@linkPos", SqlDbType.SmallInt,2),
					new SqlParameter("@urlHash", SqlDbType.VarChar,50),
					new SqlParameter("@urlFrom", SqlDbType.NVarChar),
					new SqlParameter("@dTime", SqlDbType.SmallDateTime),
					new SqlParameter("@uid", SqlDbType.VarChar,50),
					new SqlParameter("@nameSite", SqlDbType.NVarChar),
					new SqlParameter("@varTime", SqlDbType.VarChar,50),
					new SqlParameter("@mode", SqlDbType.NVarChar),
					new SqlParameter("@zid", SqlDbType.VarChar,100),
					new SqlParameter("@doc", SqlDbType.NVarChar),
					new SqlParameter("@ext1", SqlDbType.VarChar,20),
					new SqlParameter("@ext2", SqlDbType.VarChar,20),
					new SqlParameter("@ext3", SqlDbType.VarChar,20),
					new SqlParameter("@ext4", SqlDbType.VarChar,20),
					new SqlParameter("@ext5", SqlDbType.VarChar,20),
					new SqlParameter("@status", SqlDbType.SmallInt,2),
					new SqlParameter("@createTime", SqlDbType.SmallDateTime)};
            parameters[0].Value = model.url;
            parameters[1].Value = model.title;
            parameters[2].Value = model.key;
            parameters[3].Value = model.linkPos;
            parameters[4].Value = model.urlHash;
            parameters[5].Value = model.urlFrom;
            parameters[6].Value = model.dTime;
            parameters[7].Value = model.uid;
            parameters[8].Value = model.nameSite;
            parameters[9].Value = model.varTime;
            parameters[10].Value = model.mode;
            parameters[11].Value = model.zid;
            parameters[12].Value = model.doc;
            parameters[13].Value = model.ext1;
            parameters[14].Value = model.ext2;
            parameters[15].Value = model.ext3;
            parameters[16].Value = model.ext4;
            parameters[17].Value = model.ext5;
            parameters[18].Value = model.status;
            parameters[19].Value = model.createTime;

            object obj = ESP.Purchase.Common.DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public void UpdateSearchEngine(ESP.Purchase.Entity.BaiduInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SearchEngine set ");
            strSql.Append("url=@url,");
            strSql.Append("title=@title,");
            strSql.Append("[key]=@key,");
            strSql.Append("linkPos=@linkPos,");
            strSql.Append("urlHash=@urlHash,");
            strSql.Append("urlFrom=@urlFrom,");
            strSql.Append("dTime=@dTime,");
            strSql.Append("uid=@uid,");
            strSql.Append("nameSite=@nameSite,");
            strSql.Append("varTime=@varTime,");
            strSql.Append("mode=@mode,");
            strSql.Append("zid=@zid,");
            strSql.Append("doc=@doc,");
            strSql.Append("ext1=@ext1,");
            strSql.Append("ext2=@ext2,");
            strSql.Append("ext3=@ext3,");
            strSql.Append("ext4=@ext4,");
            strSql.Append("ext5=@ext5,");
            strSql.Append("status=@status,");
            strSql.Append("createTime=@createTime");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@url", SqlDbType.VarChar,450),
					new SqlParameter("@title", SqlDbType.NVarChar),
					new SqlParameter("@key", SqlDbType.NVarChar),
					new SqlParameter("@linkPos", SqlDbType.SmallInt,2),
					new SqlParameter("@urlHash", SqlDbType.VarChar,50),
					new SqlParameter("@urlFrom", SqlDbType.NVarChar),
					new SqlParameter("@dTime", SqlDbType.SmallDateTime),
					new SqlParameter("@uid", SqlDbType.VarChar,50),
					new SqlParameter("@nameSite", SqlDbType.NVarChar),
					new SqlParameter("@varTime", SqlDbType.VarChar,50),
					new SqlParameter("@mode", SqlDbType.NVarChar),
					new SqlParameter("@zid", SqlDbType.VarChar,100),
					new SqlParameter("@doc", SqlDbType.NVarChar),
					new SqlParameter("@ext1", SqlDbType.VarChar,20),
					new SqlParameter("@ext2", SqlDbType.VarChar,20),
					new SqlParameter("@ext3", SqlDbType.VarChar,20),
					new SqlParameter("@ext4", SqlDbType.VarChar,20),
					new SqlParameter("@ext5", SqlDbType.VarChar,20),
					new SqlParameter("@status", SqlDbType.SmallInt,2),
					new SqlParameter("@createTime", SqlDbType.SmallDateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.url;
            parameters[2].Value = model.title;
            parameters[3].Value = model.key;
            parameters[4].Value = model.linkPos;
            parameters[5].Value = model.urlHash;
            parameters[6].Value = model.urlFrom;
            parameters[7].Value = model.dTime;
            parameters[8].Value = model.uid;
            parameters[9].Value = model.nameSite;
            parameters[10].Value = model.varTime;
            parameters[11].Value = model.mode;
            parameters[12].Value = model.zid;
            parameters[13].Value = model.doc;
            parameters[14].Value = model.ext1;
            parameters[15].Value = model.ext2;
            parameters[16].Value = model.ext3;
            parameters[17].Value = model.ext4;
            parameters[18].Value = model.ext5;
            parameters[19].Value = model.status;
            parameters[20].Value = model.createTime;

            ESP.Purchase.Common.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public DataSet GetSearchEngineList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[url],[title],[key],[linkPos],[urlHash],[urlFrom],[dTime],[uid],[nameSite],[varTime],[mode],[zid],[doc],[ext1],[ext2],[ext3],[ext4],[ext5],[status],[createTime] ");
            strSql.Append(" FROM T_SearchEngine ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by dTime desc");
            return ESP.Purchase.Common.DbHelperSQL.Query(strSql.ToString());
        }

        public int GetSearchEngineListCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(id) as countid ");
            strSql.Append(" FROM T_SearchEngine ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  " + strWhere);
            }
            DataTable dt = ESP.Purchase.Common.DbHelperSQL.Query(strSql.ToString()).Tables[0];
            int countid = 0;
            countid = int.Parse(dt.Rows[0]["countid"].ToString());
            return countid;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteSearchEngineInfo(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_SearchEngine ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
				};
            parameters[0].Value = id;
            ESP.Purchase.Common.DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public void DeleteSearchEngineInfo(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_SearchEngine ");
            strSql.Append(" where 1=1 ").Append(sqlWhere);
            ESP.Purchase.Common.DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 去除重复值，修改数据的状态，将URL为空，或者描述信息为空的数据删除掉
        /// </summary>
        public void DeleteRepeatWatchInfo()
        {
            string sql = @"delete t_watch where [id] not in(select min([id]) from t_watch group by (url + urlhash));
                           update T_Watch set [status] =1 ;
                           delete t_watch where url is null or url='';
                           delete t_watch where [desc] = '' or [desc] ='-' or [desc] is null;";
            ESP.Purchase.Common.DbHelperSQL.ExecuteSql(sql);
        }
        #endregion  成员方法
    }
}

