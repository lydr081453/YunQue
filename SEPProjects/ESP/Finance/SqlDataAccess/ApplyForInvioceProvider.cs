using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_ApplyForInvioce。
    /// </summary>
    internal class ApplyForInvioceDataProvider : ESP.Finance.IDataAccess.IApplyForInvioceDataProvider
    {
        
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_ApplyForInvioce");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ApplyForInvioceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ApplyForInvioce(");
            strSql.Append("projectId,remark,status,creatorUserId,createDate,inviocePrice,creatorUserName,SupplierId,FlowTo,InvoiceType,InvoiceTitle,BankName,BankNum,TIN,AddressPhone)");
            strSql.Append(" values (");
            strSql.Append("@projectId,@remark,@status,@creatorUserId,@createDate,@inviocePrice,@creatorUserName,@SupplierId,@FlowTo,@InvoiceType,@InvoiceTitle,@BankName,@BankNum,@TIN,@AddressPhone)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@projectId", SqlDbType.Int),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@status", SqlDbType.Int),
					new SqlParameter("@creatorUserId", SqlDbType.Int),
					new SqlParameter("@createDate", SqlDbType.DateTime),
                    new SqlParameter("@inviocePrice",SqlDbType.Decimal),
                    new SqlParameter("@CreatorUserName", SqlDbType.NVarChar),
                    new SqlParameter("@SupplierId",SqlDbType.NVarChar,20),
                    new SqlParameter("@FlowTo", SqlDbType.Int),
                    new SqlParameter("@InvoiceType", SqlDbType.Int),
                    new SqlParameter("@InvoiceTitle",SqlDbType.NVarChar,100),
                    new SqlParameter("@BankName",SqlDbType.NVarChar,100),
                    new SqlParameter("@BankNum",SqlDbType.NVarChar,100),
                    new SqlParameter("@TIN",SqlDbType.NVarChar,100),
                    new SqlParameter("@AddressPhone",SqlDbType.NVarChar,200),
                                        };
            parameters[0].Value = model.ProjectId;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.Status;
            parameters[3].Value =model.CreatorUserId;
            parameters[4].Value =model.CreateDate;
            parameters[5].Value = model.InviocePrice;
            parameters[6].Value = model.CreatorUserName;
            parameters[7].Value = model.SupplierId;
            parameters[8].Value = model.FlowTo;
            parameters[9].Value = model.InvoiceType;
            parameters[10].Value = model.InvoiceTitle;
            parameters[11].Value = model.BankName;
            parameters[12].Value = model.BankNum;
            parameters[13].Value = model.TIN;
            parameters[14].Value = model.AddressPhone;

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
        public int Update(ESP.Finance.Entity.ApplyForInvioceInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ApplyForInvioce set ");
            strSql.Append("projectId=@projectId,");
            strSql.Append("remark=@remark,");
            strSql.Append("status=@status,");
            strSql.Append("inviocePrice=@inviocePrice,SupplierId=@SupplierId,FlowTo=@FlowTo,InvoiceType=@InvoiceType,InvoiceTitle=@InvoiceTitle,BankName=@BankName,BankNum=@BankNum,TIN=@TIN,AddressPhone=@AddressPhone");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@projectId", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.NVarChar,10),
					new SqlParameter("@status", SqlDbType.NVarChar,50),
                    new SqlParameter("@inviocePrice",SqlDbType.Decimal),
					new SqlParameter("@Id", SqlDbType.NVarChar,100),
                    new SqlParameter("@SupplierId",SqlDbType.NVarChar,20),
                    new SqlParameter("@FlowTo", SqlDbType.Int),                    
                    new SqlParameter("@InvoiceType", SqlDbType.Int),
                    new SqlParameter("@InvoiceTitle",SqlDbType.NVarChar,100),
                    new SqlParameter("@BankName",SqlDbType.NVarChar,100),
                    new SqlParameter("@BankNum",SqlDbType.NVarChar,100),
                    new SqlParameter("@TIN",SqlDbType.NVarChar,100),
                    new SqlParameter("@AddressPhone",SqlDbType.NVarChar,200),
                    };
            parameters[0].Value =model.ProjectId;
            parameters[1].Value =model.Remark;
            parameters[2].Value =model.Status;
            parameters[3].Value = model.InviocePrice;
            parameters[4].Value = model.Id;
            parameters[5].Value = model.SupplierId;
            parameters[6].Value = model.FlowTo;
            parameters[7].Value = model.InvoiceType;
            parameters[8].Value = model.InvoiceTitle;
            parameters[9].Value = model.BankName;
            parameters[10].Value = model.BankNum;
            parameters[11].Value = model.TIN;
            parameters[12].Value = model.AddressPhone;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int UpdateStatus(string Ids, ESP.Finance.Utility.ApplyForInvioceStatus.Status status)
        {
            string sql = @"update F_ApplyForInvioce set Status=" + (int)status + " where id in (" + Ids + ")";
            return DbHelperSQL.ExecuteSql(sql);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ApplyForInvioce ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ApplyForInvioceInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_ApplyForInvioce ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return CBO.FillObject<ApplyForInvioceInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ApplyForInvioceInfo> GetList(string term,List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_ApplyForInvioce ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();
            //    return CBO.FillCollection<F_ApplyForInvioce>(DbHelperSQL.ExecuteReader(strSql.ToString(),ps));
            //}
            return CBO.FillCollection < ApplyForInvioceInfo >( DbHelperSQL.Query(strSql.ToString(),param));
        }


        #endregion  成员方法
    }
}