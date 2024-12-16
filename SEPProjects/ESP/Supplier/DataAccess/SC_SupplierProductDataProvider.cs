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
    public class SC_SupplierProductDataProvider
    {
        public SC_SupplierProductDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_SupplierProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierProduct(");
            strSql.Append("Version,SupplierId,SN,Name,UsedBeginTime,UsedEndTime,PayDays,ReceiveType,Receiver,Description,Unit,Class,Price,ProductTypeid,ProductTypeName,ProductContentSheet,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status,productPic,ProductBatchId)");
            strSql.Append(" values (");
            strSql.Append("@Version,@SupplierId,@SN,@Name,@UsedBeginTime,@UsedEndTime,@PayDays,@ReceiveType,@Receiver,@Description,@Unit,@Class,@Price,@ProductTypeid,@ProductTypeName,@ProductContentSheet,@CreatedIP,@CreatTime,@LastModifiedIP,@LastUpdateTime,@Type,@Status,@productPic,@ProductBatchId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Version", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SN", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.VarChar,1000),
					new SqlParameter("@UsedBeginTime", SqlDbType.SmallDateTime),
					new SqlParameter("@UsedEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@PayDays", SqlDbType.Int,4),
					new SqlParameter("@ReceiveType", SqlDbType.Int,4),
					new SqlParameter("@Receiver", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Unit", SqlDbType.VarChar,100),
					new SqlParameter("@Class", SqlDbType.VarChar,200),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@ProductTypeid", SqlDbType.Int,4),
					new SqlParameter("@ProductTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductContentSheet", SqlDbType.Text),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@productPic", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductBatchId", SqlDbType.Int,4)};
            parameters[0].Value = model.Version;
            parameters[1].Value = model.SupplierId;
            parameters[2].Value = model.SN;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.UsedBeginTime;
            parameters[5].Value = model.UsedEndTime;
            parameters[6].Value = model.PayDays;
            parameters[7].Value = model.ReceiveType;
            parameters[8].Value = model.Receiver;
            parameters[9].Value = model.Description;
            parameters[10].Value = model.Unit;
            parameters[11].Value = model.Class;
            parameters[12].Value = model.Price;
            parameters[13].Value = model.ProductTypeid;
            parameters[14].Value = model.ProductTypeName;
            parameters[15].Value = model.ProductContentSheet;
            parameters[16].Value = model.CreatedIP;
            parameters[17].Value = model.CreatTime;
            parameters[18].Value = model.LastModifiedIP;
            parameters[19].Value = model.LastUpdateTime;
            parameters[20].Value = model.Type;
            parameters[21].Value = model.Status;
            parameters[22].Value = model.productPic;
            parameters[23].Value = model.ProductBatchId;

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
        public void Update(SC_SupplierProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_SupplierProduct set ");
            strSql.Append("Version=@Version,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("SN=@SN,");
            strSql.Append("Name=@Name,");
            strSql.Append("UsedBeginTime=@UsedBeginTime,");
            strSql.Append("UsedEndTime=@UsedEndTime,");
            strSql.Append("PayDays=@PayDays,");
            strSql.Append("ReceiveType=@ReceiveType,");
            strSql.Append("Receiver=@Receiver,");
            strSql.Append("Description=@Description,");
            strSql.Append("Unit=@Unit,");
            strSql.Append("Class=@Class,");
            strSql.Append("Price=@Price,");
            strSql.Append("ProductTypeid=@ProductTypeid,");
            strSql.Append("ProductTypeName=@ProductTypeName,");
            strSql.Append("ProductContentSheet=@ProductContentSheet,");
            strSql.Append("CreatedIP=@CreatedIP,");
            strSql.Append("CreatTime=@CreatTime,");
            strSql.Append("LastModifiedIP=@LastModifiedIP,");
            strSql.Append("LastUpdateTime=@LastUpdateTime,");
            strSql.Append("Type=@Type,");
            strSql.Append("Status=@Status,");
            strSql.Append("productPic=@productPic,");
            strSql.Append("ProductBatchId=@ProductBatchId");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@Version", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SN", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.VarChar,1000),
					new SqlParameter("@UsedBeginTime", SqlDbType.SmallDateTime),
					new SqlParameter("@UsedEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@PayDays", SqlDbType.Int,4),
					new SqlParameter("@ReceiveType", SqlDbType.Int,4),
					new SqlParameter("@Receiver", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,1000),
					new SqlParameter("@Unit", SqlDbType.VarChar,100),
					new SqlParameter("@Class", SqlDbType.VarChar,200),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@ProductTypeid", SqlDbType.Int,4),
					new SqlParameter("@ProductTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductContentSheet", SqlDbType.Text),
					new SqlParameter("@CreatedIP", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatTime", SqlDbType.SmallDateTime),
					new SqlParameter("@LastModifiedIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LastUpdateTime", SqlDbType.SmallDateTime),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@productPic", SqlDbType.NVarChar,200),
					new SqlParameter("@ProductBatchId", SqlDbType.Int,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.Version;
            parameters[2].Value = model.SupplierId;
            parameters[3].Value = model.SN;
            parameters[4].Value = model.Name;
            parameters[5].Value = model.UsedBeginTime;
            parameters[6].Value = model.UsedEndTime;
            parameters[7].Value = model.PayDays;
            parameters[8].Value = model.ReceiveType;
            parameters[9].Value = model.Receiver;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.Unit;
            parameters[12].Value = model.Class;
            parameters[13].Value = model.Price;
            parameters[14].Value = model.ProductTypeid;
            parameters[15].Value = model.ProductTypeName;
            parameters[16].Value = model.ProductContentSheet;
            parameters[17].Value = model.CreatedIP;
            parameters[18].Value = model.CreatTime;
            parameters[19].Value = model.LastModifiedIP;
            parameters[20].Value = model.LastUpdateTime;
            parameters[21].Value = model.Type;
            parameters[22].Value = model.Status;
            parameters[23].Value = model.productPic;
            parameters[24].Value = model.ProductBatchId;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SC_SupplierProduct ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_SupplierProduct GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,Version,SupplierId,SN,Name,UsedBeginTime,UsedEndTime,PayDays,ReceiveType,Receiver,Description,Unit,Class,Price,ProductTypeid,ProductTypeName,ProductContentSheet,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status,productPic,ProductBatchId from SC_SupplierProduct ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            SC_SupplierProduct model = new SC_SupplierProduct();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Version"].ToString() != "")
                {
                    model.Version = int.Parse(ds.Tables[0].Rows[0]["Version"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(ds.Tables[0].Rows[0]["SupplierId"].ToString());
                }
                model.SN = ds.Tables[0].Rows[0]["SN"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                if (ds.Tables[0].Rows[0]["UsedBeginTime"].ToString() != "")
                {
                    model.UsedBeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["UsedBeginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UsedEndTime"].ToString() != "")
                {
                    model.UsedEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["UsedEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayDays"].ToString() != "")
                {
                    model.PayDays = int.Parse(ds.Tables[0].Rows[0]["PayDays"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReceiveType"].ToString() != "")
                {
                    model.ReceiveType = int.Parse(ds.Tables[0].Rows[0]["ReceiveType"].ToString());
                }
                model.Receiver = ds.Tables[0].Rows[0]["Receiver"].ToString();
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                model.Class = ds.Tables[0].Rows[0]["Class"].ToString();
                if (ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductTypeid"].ToString() != "")
                {
                    model.ProductTypeid = int.Parse(ds.Tables[0].Rows[0]["ProductTypeid"].ToString());
                }
                model.ProductTypeName = ds.Tables[0].Rows[0]["ProductTypeName"].ToString();
                model.ProductContentSheet = ds.Tables[0].Rows[0]["ProductContentSheet"].ToString();
                model.CreatedIP = ds.Tables[0].Rows[0]["CreatedIP"].ToString();
                if (ds.Tables[0].Rows[0]["CreatTime"].ToString() != "")
                {
                    model.CreatTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatTime"].ToString());
                }
                model.LastModifiedIP = ds.Tables[0].Rows[0]["LastModifiedIP"].ToString();
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
                model.productPic = ds.Tables[0].Rows[0]["productPic"].ToString();
                if (ds.Tables[0].Rows[0]["ProductBatchId"].ToString() != "")
                {
                    model.ProductBatchId = int.Parse(ds.Tables[0].Rows[0]["ProductBatchId"].ToString());
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
            strSql.Append("select id,Version,SupplierId,SN,Name,UsedBeginTime,UsedEndTime,PayDays,ReceiveType,Receiver,Description,Unit,Class,Price,ProductTypeid,ProductTypeName,ProductContentSheet,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status,productPic,ProductBatchId ");
            strSql.Append(" FROM SC_SupplierProduct ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
            strSql.Append(" id,Version,SupplierId,SN,Name,UsedBeginTime,UsedEndTime,PayDays,ReceiveType,Receiver,Description,Unit,Class,Price,ProductTypeid,ProductTypeName,ProductContentSheet,CreatedIP,CreatTime,LastModifiedIP,LastUpdateTime,Type,Status,productPic,ProductBatchId ");
            strSql.Append(" FROM SC_SupplierProduct ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

       
        #endregion  成员方法
    }
}
