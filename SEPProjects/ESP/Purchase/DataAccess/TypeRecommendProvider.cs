using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace ESP.Purchase.DataAccess
{
    public class TypeRecommendProvider
    {
        public TypeRecommendProvider()
        { }

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(TypeRecommendInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_TypeRecommend(");
            strSql.Append("Level1Id,Level2Id,Level3Id,Level1,Level2,Level3,Orders,TotalPrice,RecYear,RecMonth,CreateDate,UserId,Status)");
            strSql.Append(" values (");
            strSql.Append("@Level1Id,@Level2Id,@Level3Id,@Level1,@Level2,@Level3,@Orders,@TotalPrice,@RecYear,@RecMonth,@CreateDate,@UserId,@Status)");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] parameters = {
					new SqlParameter("@Level1Id", SqlDbType.Int,4),
					new SqlParameter("@Level2Id", SqlDbType.Int,4),
                    new SqlParameter("@Level3Id",SqlDbType.Int,4),
                    new SqlParameter("@Level1",SqlDbType.NVarChar,50),
                    new SqlParameter("@Level2",SqlDbType.NVarChar,50),
                    new SqlParameter("@Level3",SqlDbType.NVarChar,50),
                    new SqlParameter("@Orders",SqlDbType.Int,4),
                    new SqlParameter("@TotalPrice",SqlDbType.Decimal),
                    new SqlParameter("@RecYear",SqlDbType.Int,4),
                                        new SqlParameter("@RecMonth",SqlDbType.Int,4),
                                        new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                        new SqlParameter("@UserId",SqlDbType.Int,4),
                                        new SqlParameter("@Status",SqlDbType.Int,4)
                                        };


            parameters[0].Value = model.Level1Id;
            parameters[1].Value = model.Level2Id;
            parameters[2].Value = model.Level3Id;
            parameters[3].Value = model.Level1;
            parameters[4].Value = model.Level2;
            parameters[5].Value = model.Level3;
            parameters[6].Value = model.Orders;
            parameters[7].Value = model.TotalPrice;
            parameters[8].Value = model.RecYear;
            parameters[9].Value = model.RecMonth;
            parameters[10].Value = model.CreateDate;
            parameters[11].Value = model.UserId;
            parameters[12].Value = model.Status;
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
        public int Update(TypeRecommendInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_TypeRecommend set ");
            strSql.Append("Level1Id = @Level1Id,");
            strSql.Append("Level2Id = @Level2Id,");
            strSql.Append("Level3Id = @Level3Id,");
            strSql.Append("Level1 = @Level1,");
            strSql.Append("Level2 = @Level2,");
            strSql.Append("Level3 = @Level3,");
            strSql.Append("Orders = @Orders,");
            strSql.Append("TotalPrice = @TotalPrice,");
            strSql.Append("RecYear = @RecYear,");
            strSql.Append("RecMonth = @RecMonth,");
            strSql.Append("CreateDate = @CreateDate,");
            strSql.Append("UserId = @UserId,");
            strSql.Append("Status = @Status");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4),
				    new SqlParameter("@Level1Id", SqlDbType.Int,4),
					new SqlParameter("@Level2Id", SqlDbType.Int,4),
                    new SqlParameter("@Level3Id",SqlDbType.Int,4),
                    new SqlParameter("@Level1",SqlDbType.NVarChar,50),
                    new SqlParameter("@Level2",SqlDbType.NVarChar,50),
                    new SqlParameter("@Level3",SqlDbType.NVarChar,50),
                    new SqlParameter("@Orders",SqlDbType.Int,4),
                    new SqlParameter("@TotalPrice",SqlDbType.Decimal),
                    new SqlParameter("@RecYear",SqlDbType.Int,4),
                                        new SqlParameter("@RecMonth",SqlDbType.Int,4),
                                        new SqlParameter("@CreateDate",SqlDbType.DateTime),
                                        new SqlParameter("@UserId",SqlDbType.Int,4),
                                        new SqlParameter("@Status",SqlDbType.Int,4)
                                        };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.Level1Id;
            parameters[2].Value = model.Level2Id;
            parameters[3].Value = model.Level3Id;
            parameters[4].Value = model.Level1;
            parameters[5].Value = model.Level2;
            parameters[6].Value = model.Level3;
            parameters[7].Value = model.Orders;
            parameters[8].Value = model.TotalPrice;
            parameters[9].Value = model.RecYear;

            parameters[10].Value = model.RecMonth;
            parameters[11].Value = model.CreateDate;
            parameters[12].Value = model.UserId;
            parameters[13].Value = model.Status;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="typeid">id</param>
        /// <returns></returns>
        public TypeRecommendInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_TypeRecommend ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return ESP.Finance.Utility.CBO.FillObject<ESP.Purchase.Entity.TypeRecommendInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public List<TypeRecommendInfo> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_TypeRecommend where status=1");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by orders ");
            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.TypeRecommendInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        public List<TypeRecommendInfo> SearchData(string begin, string end)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 10 c1.typeid Level1Id,c2.typeid Level2Id,c3.typeid Level3Id,c1.typename Level1,c2.typename Level2,c3.typename Level3,");
            strSql.Append(" COUNT(*) orders,SUM(a.totalprice) TotalPrice");
            strSql.Append(" from T_GeneralInfo a join T_OrderInfo b on a.id =b.general_id");
            strSql.Append(" join T_Type c3 on b.producttype=c3.typeid");
            strSql.Append(" join T_Type c2 on c3.parentId=c2.typeid");
            strSql.Append(" join T_Type c1 on c2.parentId=c1.typeid");
            strSql.Append(" where a.status not in(-1,0,2,4) ");
            if (begin!=null && end!=null)
            {
                strSql.Append(" and  (a.app_date between '"+begin+"' and '"+end+"') ");
            }
            strSql.Append(" group by c1.typeid,c2.typeid,c3.typeid,c1.typename,c2.typename,c3.typename");
            strSql.Append(" order by orders desc");


            return ESP.Finance.Utility.CBO.FillCollection<ESP.Purchase.Entity.TypeRecommendInfo>(DbHelperSQL.Query(strSql.ToString()));
        }





        #endregion  成员方法
    }
}
