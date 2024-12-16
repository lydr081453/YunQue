using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    public class SupplierRecommendProvider
    {
        public SupplierRecommendProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(SupplierRecommendInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SupplierRecommend(");
            strSql.Append("RecommendId,SupplierId,SupplyId,SupplierName,Contacter,Tel,Mobile,Position,EMail,DeptName,Remark,Image1,Image2,Image3,Status,CreateDate,UserId,Orders,TotalPrice,BuildTime,CompanyType,RegCapital,CompanyScale,Address,City,BizModel,MainProduct,sort)");
            strSql.Append(" values (");
            strSql.Append("@RecommendId,@SupplierId,@SupplyId,@SupplierName,@Contacter,@Tel,@Mobile,@Position,@EMail,@DeptName,@Remark,@Image1,@Image2,@Image3,@Status,@CreateDate,@UserId,@Orders,@TotalPrice,@BuildTime,@CompanyType,@RegCapital,@CompanyScale,@Address,@City,@BizModel,@MainProduct,@sort)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] parameters = {
					new SqlParameter("@RecommendId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
                    new SqlParameter("@SupplyId",SqlDbType.Int,4),
                    new SqlParameter("@SupplierName",SqlDbType.NVarChar,500),
                    new SqlParameter("@Contacter",SqlDbType.NVarChar,50),
                    new SqlParameter("@Tel",SqlDbType.NVarChar,50),
                    new SqlParameter("@Mobile",SqlDbType.NVarChar,50),
                    new SqlParameter("@Position",SqlDbType.NVarChar,50),
                    new SqlParameter("@EMail",SqlDbType.NVarChar,50),
                                        new SqlParameter("@DeptName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Remark",SqlDbType.NText),
                                        new SqlParameter("@Image1",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Image3",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Status",SqlDbType.Int,4),
                                        new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                        new SqlParameter("@UserId",SqlDbType.Int,4),
                                        new SqlParameter("@Orders",SqlDbType.Int,4),
                                        new SqlParameter("@TotalPrice",SqlDbType.Decimal),
                                        new SqlParameter("@BuildTime",SqlDbType.NVarChar,50),
                                         new SqlParameter("@CompanyType",SqlDbType.NVarChar,50),
                                          new SqlParameter("@RegCapital",SqlDbType.NVarChar,50),
                                           new SqlParameter("@CompanyScale",SqlDbType.NVarChar,50),
                                            new SqlParameter("@Address",SqlDbType.NVarChar,50),
                                             new SqlParameter("@City",SqlDbType.NVarChar,50),
                                              new SqlParameter("@BizModel",SqlDbType.NVarChar,50),
                                               new SqlParameter("@MainProduct",SqlDbType.NVarChar,50),
                                                new SqlParameter("@sort",SqlDbType.Int,4)

                                        };


            parameters[0].Value = model.RecommendId;
            parameters[1].Value = model.SupplierId;
            parameters[2].Value = model.SupplyId;
            parameters[3].Value = model.SupplierName;
            parameters[4].Value = model.Contacter;
            parameters[5].Value = model.Tel;
            parameters[6].Value = model.Mobile;
            parameters[7].Value = model.Position;
            parameters[8].Value = model.EMail;
            parameters[9].Value = model.DeptName;
            parameters[10].Value = model.Remark;

            parameters[11].Value = model.Image1;
            parameters[12].Value = model.Image2;
            parameters[13].Value = model.Image3;
            parameters[14].Value = model.Status;
            parameters[15].Value = model.CreateDate;
            parameters[16].Value = model.UserId;
             parameters[17].Value = model.Orders;
             parameters[18].Value = model.TotalPrice;

             parameters[19].Value = model.BuildTime;
             parameters[20].Value = model.CompanyType;
             parameters[21].Value = model.RegCapital;
             parameters[22].Value = model.CompanyScale;
             parameters[23].Value = model.Address;
             parameters[24].Value = model.City;
             parameters[25].Value = model.BizModel;
             parameters[26].Value = model.MainProduct;
             parameters[27].Value = model.Sort;

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
        public int Update(SupplierRecommendInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SupplierRecommend set ");
            strSql.Append("RecommendId = @RecommendId");
            strSql.Append(",SupplierId = @SupplierId");
            strSql.Append(",SupplyId = @SupplyId");
            strSql.Append(",SupplierName = @SupplierName");
            strSql.Append(",Contacter = @Contacter");
            strSql.Append(",Tel = @Tel");
            strSql.Append(",Mobile = @Mobile");
            strSql.Append(",Position = @Position");
            strSql.Append(",EMail = @EMail");
            strSql.Append(",DeptName = @DeptName");
            strSql.Append(",Remark = @Remark");
            strSql.Append(",Image1 = @Image1");
            strSql.Append(",Image2 = @Image2");
            strSql.Append(",Image3 = @Image3");
            strSql.Append(",Status = @Status");
            strSql.Append(",CreateDate = @CreateDate");
            strSql.Append(",UserId = @UserId,Orders=@Orders,TotalPrice=@TotalPrice");
            strSql.Append(",BuildTime=@BuildTime,CompanyType=@CompanyType,RegCapital=@RegCapital,CompanyScale=@CompanyScale,Address=@Address,City=@City,BizModel=@BizModel,MainProduct=@MainProduct");
            strSql.Append(",sort=@sort where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
				   	new SqlParameter("@RecommendId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
                    new SqlParameter("@SupplyId",SqlDbType.Int,4),
                    new SqlParameter("@SupplierName",SqlDbType.NVarChar,500),
                    new SqlParameter("@Contacter",SqlDbType.NVarChar,50),
                    new SqlParameter("@Tel",SqlDbType.NVarChar,50),
                    new SqlParameter("@Mobile",SqlDbType.NVarChar,50),
                    new SqlParameter("@Position",SqlDbType.NVarChar,50),
                    new SqlParameter("@EMail",SqlDbType.NVarChar,50),
                                        new SqlParameter("@DeptName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Remark",SqlDbType.NText),
                                        new SqlParameter("@Image1",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Image3",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Status",SqlDbType.Int,4),
                                        new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                        new SqlParameter("@UserId",SqlDbType.Int,4),
                                        new SqlParameter("@Orders",SqlDbType.Int,4),
                                        new SqlParameter("@TotalPrice",SqlDbType.Decimal),
                                         new SqlParameter("@BuildTime",SqlDbType.NVarChar,50),
                                         new SqlParameter("@CompanyType",SqlDbType.NVarChar,50),
                                          new SqlParameter("@RegCapital",SqlDbType.NVarChar,50),
                                           new SqlParameter("@CompanyScale",SqlDbType.NVarChar,50),
                                            new SqlParameter("@Address",SqlDbType.NVarChar,50),
                                             new SqlParameter("@City",SqlDbType.NVarChar,50),
                                              new SqlParameter("@BizModel",SqlDbType.NVarChar,50),
                                               new SqlParameter("@MainProduct",SqlDbType.NVarChar,50),
                                                new SqlParameter("@sort",SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.RecommendId;
            parameters[2].Value = model.SupplierId;
            parameters[3].Value = model.SupplyId;
            parameters[4].Value = model.SupplierName;
            parameters[5].Value = model.Contacter;
            parameters[6].Value = model.Tel;
            parameters[7].Value = model.Mobile;
            parameters[8].Value = model.Position;
            parameters[9].Value = model.EMail;
            parameters[10].Value = model.DeptName;
            parameters[11].Value = model.Remark;

            parameters[12].Value = model.Image1;
            parameters[13].Value = model.Image2;
            parameters[14].Value = model.Image3;
            parameters[15].Value = model.Status;
            parameters[16].Value = model.CreateDate;
            parameters[17].Value = model.UserId;
            parameters[18].Value = model.Orders;
            parameters[19].Value = model.TotalPrice;

            parameters[20].Value = model.BuildTime;
            parameters[21].Value = model.CompanyType;
            parameters[22].Value = model.RegCapital;
            parameters[23].Value = model.CompanyScale;
            parameters[24].Value = model.Address;
            parameters[25].Value = model.City;
            parameters[26].Value = model.BizModel;
            parameters[27].Value = model.MainProduct;
            parameters[28].Value = model.Sort;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="typeid">id</param>
        /// <returns></returns>
        public SupplierRecommendInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_SupplierRecommend ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.SupplierRecommendInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public List<SupplierRecommendInfo> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_SupplierRecommend where status=1");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by orders ");
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.SupplierRecommendInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public List<SupplierRecommendInfo> SearchData(int typeId, string begin, string end)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 10 a.supplier_name SupplierName, ");
            strSql.Append("COUNT(*) orders,SUM(a.totalprice) TotalPrice ");
            strSql.Append("from T_GeneralInfo a join T_OrderInfo b on a.id =b.general_id ");
            strSql.Append("where a.status not in(-1,0,2,4) and b.producttype = " + typeId.ToString());
            if (begin != null && end != null)
            {
                strSql.Append(" and  (a.app_date between '" + begin + "' and '" + end + "') ");
            }
            strSql.Append("group by a.supplier_name ");
            strSql.Append("order by orders desc ");


            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.SupplierRecommendInfo>(DbHelperSQL.Query(strSql.ToString()));
        }





        #endregion  成员方法
    }
}
