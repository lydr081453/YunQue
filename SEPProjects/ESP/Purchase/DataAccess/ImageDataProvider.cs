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
    /// 数据访问类ImageDataProvider，为供应商图片类
    /// </summary>
    public class ImageDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageDataProvider"/> class.
        /// </summary>
        public ImageDataProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(ImageInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Image(");
            strSql.Append("supplier_id,imagename,imageurl)");
            strSql.Append(" values (");
            strSql.Append("@supplier_id,@imagename,@imageurl)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@supplier_id", SqlDbType.Int,4),
					new SqlParameter("@imagename", SqlDbType.NVarChar,100),
					new SqlParameter("@imageurl", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.supplier_id;
            parameters[1].Value = model.imagename;
            parameters[2].Value = model.imageurl;

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
        public static void Update(ImageInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Image set ");
            strSql.Append("supplier_id=@supplier_id,");
            strSql.Append("imagename=@imagename,");
            strSql.Append("imageurl=@imageurl");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@supplier_id", SqlDbType.Int,4),
					new SqlParameter("@imagename", SqlDbType.NVarChar,100),
					new SqlParameter("@imageurl", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.supplier_id;
            parameters[2].Value = model.imagename;
            parameters[3].Value = model.imageurl;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public static void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_Image ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ImageInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,supplier_id,imagename,imageurl from T_Image ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            ImageInfo model = new ImageInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["supplier_id"].ToString() != "")
                {
                    model.supplier_id = int.Parse(ds.Tables[0].Rows[0]["supplier_id"].ToString());
                }
                model.imagename = ds.Tables[0].Rows[0]["imagename"].ToString();
                model.imageurl = ds.Tables[0].Rows[0]["imageurl"].ToString();
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
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,supplier_id,imagename,imageurl ");
            strSql.Append(" FROM T_Image ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// Gets the list by supplier ID.
        /// </summary>
        /// <param name="supplierid">The supplierid.</param>
        /// <returns></returns>
        public static List<ImageInfo> GetListBySupplierID(int supplierid)
        {
            List<ImageInfo> list = new List<ImageInfo>();
            string sql = string.Format("select * from T_Image where 1=1 and supplier_id={0} ",supplierid.ToString());
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(sql))
            {
                while (r.Read())
                {
                    ImageInfo c = new ImageInfo();
                    c.PopupData(r);
                    list.Add(c);
                }
                r.Close();
            }
            return list;
        }
        #endregion  成员方法
    }
}