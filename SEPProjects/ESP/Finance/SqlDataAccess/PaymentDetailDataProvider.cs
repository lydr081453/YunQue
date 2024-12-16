using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Utility;
using System.Collections;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_PaymentDetail。
    /// </summary>
    internal class PaymentDetailDataProvider : ESP.Finance.IDataAccess.IPaymentDetailDataProvider
    {
        //        Id
        //PaymentID
        //PaymentPredate
        //PaymentPreAmount
        //PaymentContent
        //Remark
        //CreateDate

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int PaymentDetailID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_PaymentDetail");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = PaymentDetailID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Entity.PaymentDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_PaymentDetail(");
            strSql.Append("PaymentID,PaymentPredate,PaymentPreAmount,PaymentContent,Remark,CreateDate,FileUrl)");
            strSql.Append(" values (");
            strSql.Append("@PaymentID,@PaymentPredate,@PaymentPreAmount,@PaymentContent,@Remark,@CreateDate,@FileUrl)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PaymentID", SqlDbType.Int,4),
					new SqlParameter("@PaymentPredate", SqlDbType.DateTime),
					new SqlParameter("@PaymentPreAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PaymentContent", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@FileUrl", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.PaymentID;
            parameters[1].Value = model.PaymentPredate;
            parameters[2].Value = model.PaymentPreAmount;
            parameters[3].Value = model.PaymentContent;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.FileUrl;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Entity.PaymentDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_PaymentDetail set ");
            strSql.Append("PaymentID=@PaymentID,");
            strSql.Append("PaymentPredate=@PaymentPredate,");
            strSql.Append("PaymentPreAmount=@PaymentPreAmount,");
            strSql.Append("PaymentContent=@PaymentContent,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreateDate=@CreateDate,FileUrl=@FileUrl");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@PaymentID", SqlDbType.Int,4),
					new SqlParameter("@PaymentPredate", SqlDbType.DateTime),
					new SqlParameter("@PaymentPreAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PaymentContent", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
                     new SqlParameter("@FileUrl", SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.PaymentID;
            parameters[2].Value = model.PaymentPredate;
            parameters[3].Value = model.PaymentPreAmount;
            parameters[4].Value = model.PaymentContent;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.FileUrl;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_PaymentDetail ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Entity.PaymentDetailInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_PaymentDetail ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return CBO.FillObject<Entity.PaymentDetailInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<Entity.PaymentDetailInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * ");
            strSql.Append(" FROM F_PaymentDetail ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
           
            return CBO.FillCollection<Entity.PaymentDetailInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }





        #endregion  成员方法
    }
}

