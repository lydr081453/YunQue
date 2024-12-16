
using System;
using System.Collections.Generic;
using System.Text;
using ESP.Compatible;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using ESP.Media.Access.Utilities;
using ESP.Media.Access;
using ESP.Media.Entity;
namespace ESP.Media.BusinessLogic
{
    public class FacetofaceManager
    {
        public static FacetofaceInfo GetModel(int id)
        {
            FacetofaceInfo face = null;
            face = ESP.Media.DataAccess.FacetofaceDataProvider.Load(id);
            if (face == null) face =new ESP.Media.Entity.FacetofaceInfo();
            return face;
        }

        public static int add(FacetofaceInfo face,  string filename, byte[] filedata, int userid, out string errmsg)
        {

                if (filedata != null && filename != string.Empty)
                {
                    face.Path = CommonManager.SaveFile(ESP.Media.Access.Utilities.ConfigManager.MediaFaceToFacePath, filename, filedata, true);
                }
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = add(face, trans, userid);

                    face.Id = id;
                    trans.Commit();
                    errmsg = "成功";
                    return id;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errmsg = "失败!";
                    return -1;
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        public static bool delete(int faceid)
        {
           return ESP.Media.DataAccess.FacetofaceDataProvider.DeleteInfo(faceid);
        }
        public static int add(FacetofaceInfo face, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int id = add(face, trans, userid);
                    trans.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static int add(FacetofaceInfo face, SqlTransaction trans, int userid)
        {
            int id = 0;
            id = ESP.Media.DataAccess.FacetofaceDataProvider.insertinfo(face, trans);
            return id;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="p">p</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(FacetofaceInfo face, int userid)
        {
            using (SqlConnection conn = new SqlConnection(ESP.Media.Access.Utilities.clsConfigOperate.CustomerSqlConnection()))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    bool result = modify(face, trans, userid);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="trans">trans</param>
        /// <param name="emp">当前登录人</param>
        /// <returns></returns>
        public static bool modify(FacetofaceInfo face, SqlTransaction trans, int userid)
        {
            bool result = false;
            result = ESP.Media.DataAccess.FacetofaceDataProvider.updateInfo(trans, null, face, null, null);
            return result;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllList()
        {
            return ESP.Media.DataAccess.FacetofaceDataProvider.QueryInfo(" and 1=1 order by cycle desc", new Hashtable());
        }

        public static FacetofaceInfo GetNew()
        {
            string sql = "select top 1 * from Media_facetoface order by cycle desc";
            DataTable dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql);
            if (dt == null || dt.Rows.Count == 0) return null;
            int id = dt.Rows[0]["id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["id"]);
            return ESP.Media.DataAccess.FacetofaceDataProvider.Load(id);
        }
    }
}
