 
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
    /// 数据访问类F_ExpressCheck。
    /// </summary>
    internal class ExpressCheckProvider : ESP.Finance.IDataAccess.IExpressCheckProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string expressNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_ExpressCheck");
            strSql.Append(" where expressNo=@expressNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@expressNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = expressNo;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ExpressCheckInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_ExpressCheck(");
            strSql.Append("Sort,ExpressNo,Sender,City,SendTime,Weight,ExpPrice,PackPrice,InsureFee,OtherFee,Remark,Status,ExpYear,ExpMonth,DeptId,DeptName,UserCode,UserId,ExpCompany)");
            strSql.Append(" values (");
            strSql.Append("@Sort,@ExpressNo,@Sender,@City,@SendTime,@Weight,@ExpPrice,@PackPrice,@InsureFee,@OtherFee,@Remark,@Status,@ExpYear,@ExpMonth,@DeptId,@DeptName,@UserCode,@UserId,@ExpCompany)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@ExpressNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@Sender", SqlDbType.NVarChar,50),
                    new SqlParameter("@City", SqlDbType.NVarChar,50),
                    new SqlParameter("@SendTime", SqlDbType.DateTime,8),
					new SqlParameter("@Weight", SqlDbType.Decimal,20),
                    new SqlParameter("@ExpPrice", SqlDbType.Decimal,20),
                    new SqlParameter("@PackPrice", SqlDbType.Decimal,20),
                    new SqlParameter("@InsureFee", SqlDbType.Decimal,20),
                    new SqlParameter("@OtherFee", SqlDbType.Decimal,20),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@ExpYear", SqlDbType.Int,4),
					new SqlParameter("@ExpMonth", SqlDbType.Int,4),
                    new SqlParameter("@DeptId", SqlDbType.Int,4),
					new SqlParameter("@DeptName", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@ExpCompany", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = model.Sort;
            parameters[1].Value = model.ExpressNo;
            parameters[2].Value = model.Sender;
            parameters[3].Value = model.City;
            parameters[4].Value = model.SendTime;
            parameters[5].Value = model.Weight;
            parameters[6].Value = model.ExpPrice;
            parameters[7].Value = model.PackPrice;
            parameters[8].Value = model.InsureFee;
            parameters[9].Value = model.OtherFee;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.ExpYear;
            parameters[13].Value = model.ExpMonth;
            parameters[14].Value = model.DeptId;
            parameters[15].Value = model.DeptName;
            parameters[16].Value = model.UserCode;
            parameters[17].Value = model.UserId;
            parameters[18].Value = model.ExpCompany;

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
        public int Update(ESP.Finance.Entity.ExpressCheckInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_ExpressCheck set ");
            strSql.Append("Sort=@Sort,ExpressNo=@ExpressNo,Sender=@Sender,City=@City,SendTime=@SendTime,Weight=@Weight,ExpPrice=@ExpPrice,");
            strSql.Append("PackPrice=@PackPrice,InsureFee=@InsureFee,OtherFee=@OtherFee,Remark=@Remark,Status=@Status,ExpYear=@ExpYear,ExpMonth=@ExpMonth,");
            strSql.Append("DeptId=@DeptId,DeptName=@DeptName,UserCode=@UserCode,UserId=@UserId,ExpCompany=@ExpCompany");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                    new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@ExpressNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@Sender", SqlDbType.NVarChar,50),
                    new SqlParameter("@City", SqlDbType.NVarChar,50),
                    new SqlParameter("@SendTime", SqlDbType.DateTime,8),
					new SqlParameter("@Weight", SqlDbType.Decimal,20),
                    new SqlParameter("@ExpPrice", SqlDbType.Decimal,20),
                    new SqlParameter("@PackPrice", SqlDbType.Decimal,20),
                    new SqlParameter("@InsureFee", SqlDbType.Decimal,20),
                    new SqlParameter("@OtherFee", SqlDbType.Decimal,20),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@ExpYear", SqlDbType.Int,4),
					new SqlParameter("@ExpMonth", SqlDbType.Int,4),
                    new SqlParameter("@DeptId", SqlDbType.Int,4),
					new SqlParameter("@DeptName", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@ExpCompany", SqlDbType.NVarChar,50),
                    new SqlParameter("@Id", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.Sort;
            parameters[1].Value = model.ExpressNo;
            parameters[2].Value = model.Sender;
            parameters[3].Value = model.City;
            parameters[4].Value = model.SendTime;
            parameters[5].Value = model.Weight;
            parameters[6].Value = model.ExpPrice;
            parameters[7].Value = model.PackPrice;
            parameters[8].Value = model.InsureFee;
            parameters[9].Value = model.OtherFee;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.ExpYear;
            parameters[13].Value = model.ExpMonth;
            parameters[14].Value = model.DeptId;
            parameters[15].Value = model.DeptName;
            parameters[16].Value = model.UserCode;
            parameters[17].Value = model.UserId;
            parameters[18].Value = model.ExpCompany;
            parameters[19].Value = model.Id;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpressCheck ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int Delete(int year ,int month)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_ExpressCheck ");
            strSql.Append(" where ExpYear=@ExpYear and ExpMonth=@ExpMonth ");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpYear", SqlDbType.Int,4),
                    new SqlParameter("@ExpMonth", SqlDbType.Int,4)               
                                        };
            parameters[0].Value = year;
            parameters[1].Value = month;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ExpressCheckInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from F_ExpressCheck ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<ExpressCheckInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ExpressCheckInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_ExpressCheck ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(" order by Sort asc ");
            return CBO.FillCollection<ExpressCheckInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }


        #endregion  成员方法
  
    }
}
