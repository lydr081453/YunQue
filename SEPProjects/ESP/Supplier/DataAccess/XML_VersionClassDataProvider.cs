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
    public class XML_VersionClassDataProvider
    {
        public XML_VersionClassDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XML_VersionClass");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XML_VersionClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XML_VersionClass(");
            strSql.Append("Name,ParentID,Level)");
            strSql.Append(" values (");
            strSql.Append("@Name,@ParentID,@Level)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@Level", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.ParentID;
            parameters[2].Value = model.Level;

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
        public void Update(XML_VersionClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XML_VersionClass set ");
            strSql.Append("Name=@Name,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("Level=@Level,");
            strSql.Append("State=@State");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@Level", SqlDbType.Int,4),
                    new SqlParameter("@State", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.ParentID;
            parameters[3].Value = model.Level;
            parameters[4].Value = model.State;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XML_VersionClass ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XML_VersionClass GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Name,ParentID,Level,state from XML_VersionClass ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XML_VersionClass model = new XML_VersionClass();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                if (ds.Tables[0].Rows[0]["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Level"].ToString() != "")
                {
                    model.Level = int.Parse(ds.Tables[0].Rows[0]["Level"].ToString());
                }
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
            strSql.Append("select ID,Name,ParentID,Level ");
            strSql.Append(" FROM XML_VersionClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        [AjaxPro.AjaxMethod]
        public static List<ESP.Supplier.Entity.XML_VersionClass> GetClassByParentIdA(int parentId)
        {
            List<ESP.Supplier.Entity.XML_VersionClass> list = new List<ESP.Supplier.Entity.XML_VersionClass>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from XML_VersionClass ");
            strSql.Append(" where Level=2 and State=1 and parentid="+parentId.ToString());

           DataSet ds= DbHelperSQL.Query(strSql.ToString());
           return DataTableToList(ds.Tables[0]);
        }

        [AjaxPro.AjaxMethod]
        public static List<ESP.Supplier.Entity.XML_VersionList> GetListByParentIdA(int parentId)
        {
            List<ESP.Supplier.Entity.XML_VersionList> list = new List<ESP.Supplier.Entity.XML_VersionList>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from XML_VersionList ");
            strSql.Append(" where State=1 and classid=" + parentId.ToString());

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return DataTableToList2(ds.Tables[0]);
        }

        private static List<XML_VersionList> DataTableToList2(DataTable dt)
        {
            List<XML_VersionList> modelList = new List<XML_VersionList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                XML_VersionList model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new XML_VersionList();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.Name = dt.Rows[n]["Name"].ToString();
                    model.TableName = dt.Rows[n]["TableName"].ToString();
                    model.Url = dt.Rows[n]["Url"].ToString();
                    model.Content = dt.Rows[n]["Content"].ToString();
                    model.Version = dt.Rows[n]["Version"].ToString();
                    model.InsertUser = dt.Rows[n]["InsertUser"].ToString();
                    model.InsertTime = dt.Rows[n]["InsertTime"].ToString();
                    model.InsertIP = dt.Rows[n]["InsertIP"].ToString();
                    model.UpdateUser = dt.Rows[n]["UpdateUser"].ToString();
                    model.UpdateTime = dt.Rows[n]["UpdateTime"].ToString();
                    model.UpdateIP = dt.Rows[n]["UpdateIP"].ToString();

                    model.BJAuditor = dt.Rows[n]["BJAuditor"].ToString();
                    model.SHAuditor = dt.Rows[n]["SHAuditor"].ToString();
                    model.GZAuditor = dt.Rows[n]["GZAuditor"].ToString();
                    if (dt.Rows[n]["ClassID"].ToString() != "")
                    {
                        model.ClassID = int.Parse(dt.Rows[n]["ClassID"].ToString());
                    }
                    if (dt.Rows[n]["BJAuditorId"].ToString() != "")
                    {
                        model.BJAuditorId = int.Parse(dt.Rows[n]["BJAuditorId"].ToString());
                    }
                    if (dt.Rows[n]["SHAuditorId"].ToString() != "")
                    {
                        model.SHAuditorId = int.Parse(dt.Rows[n]["SHAuditorId"].ToString());
                    }
                    if (dt.Rows[n]["GZAuditorId"].ToString() != "")
                    {
                        model.GZAuditorId = int.Parse(dt.Rows[n]["GZAuditorId"].ToString());
                    }
                    model.XML = dt.Rows[n]["XML"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }


        private static List<XML_VersionClass> DataTableToList(DataTable dt)
        {
            List<XML_VersionClass> modelList = new List<XML_VersionClass>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                XML_VersionClass model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new XML_VersionClass();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.Name = dt.Rows[n]["Name"].ToString();
                    if (dt.Rows[n]["ParentID"].ToString() != "")
                    {
                        model.ParentID = int.Parse(dt.Rows[n]["ParentID"].ToString());
                    }
                    if (dt.Rows[n]["Level"].ToString() != "")
                    {
                        model.Level = int.Parse(dt.Rows[n]["Level"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int used)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Name,ParentID,Level ");
            strSql.Append(" FROM XML_VersionClass ");
            strSql.Append(" where state=1 ");

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
            strSql.Append(" ID,Name,ParentID,Level ");
            strSql.Append(" FROM XML_VersionClass ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //只取得supplier已有的所有的1级物料
        public List<XML_VersionClass> GetChooseList(int sid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct vc.* from xml_versionclass vc join sc_suppliertype st on vc.id=st.typeid  ");
            strSql.Append(" where st.typelv=1 and vc.state=1 and st.supplierid=" + sid);

            List<XML_VersionClass> list = new List<XML_VersionClass>();            
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                XML_VersionClass model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new XML_VersionClass();
                    if (ds.Tables[0].Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
                    }
                    model.Name = ds.Tables[0].Rows[n]["Name"].ToString();
                    if (ds.Tables[0].Rows[n]["ParentID"].ToString() != "")
                    {
                        model.ParentID = int.Parse(ds.Tables[0].Rows[n]["ParentID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Level"].ToString() != "")
                    {
                        model.Level = int.Parse(ds.Tables[0].Rows[n]["Level"].ToString());
                    }
                    list.Add(model);
                }
            }
            return list;
            
        }

        //只取得supplier已有的2级物料
        public List<XML_VersionClass> GetChooseList(int sid, int pid)
        {         

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct vc.* from xml_versionclass vc join sc_suppliertype st on vc.id=st.typeid  ");
            strSql.Append(" where st.typelv=2 and vc.parentid="+pid+" and st.supplierid=" + sid);

            List<XML_VersionClass> list = new List<XML_VersionClass>();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                XML_VersionClass model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new XML_VersionClass();
                    if (ds.Tables[0].Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
                    }
                    model.Name = ds.Tables[0].Rows[n]["Name"].ToString();
                    if (ds.Tables[0].Rows[n]["ParentID"].ToString() != "")
                    {
                        model.ParentID = int.Parse(ds.Tables[0].Rows[n]["ParentID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["Level"].ToString() != "")
                    {
                        model.Level = int.Parse(ds.Tables[0].Rows[n]["Level"].ToString());
                    }
                    list.Add(model);
                }
            }
            return list;
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "XML_VersionClass";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  成员方法
    }
}
