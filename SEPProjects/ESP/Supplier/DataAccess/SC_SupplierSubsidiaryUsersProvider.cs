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
    public class SC_SupplierSubsidiaryUsersProvider
    {
        public int Add(SC_SupplierSubsidiaryUsers model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" INSERT INTO ESP.dbo.SC_SupplierSubsidiaryUsers");
            strSql.Append(" (SupplierID,Name,Name_en,LogName,Password,Email,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserName,IsAdmin,IsDel,IsEffective,Gender,Departments,Duties,Ages,Phone,Mobile,Title,Status,Types)");
            strSql.Append(" VALUES (@SupplierID,@Name,@Name_en,@LogName,@Password,@Email,@CreatedDate,@CreatedUserId,@ModifiedDate,@ModifiedUserName,@IsAdmin,@IsDel,@IsEffective,@Gender,@Departments,@Duties,@Ages,@Phone,@Mobile,@Title,@Status,@Types)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                           new SqlParameter("@SupplierID",SqlDbType.Int,4),
                           new SqlParameter("@Name",SqlDbType.NVarChar,200),
                           new SqlParameter("@Name_en",SqlDbType.NVarChar,200),
                           new SqlParameter("@LogName",SqlDbType.NVarChar,50),
                           new SqlParameter("@Password",SqlDbType.NVarChar,50),
                           new SqlParameter("@Email",SqlDbType.NVarChar,100),
                           new SqlParameter("@CreatedDate",SqlDbType.DateTime),
                           new SqlParameter("@CreatedUserId",SqlDbType.Int),
                           new SqlParameter("@ModifiedDate",SqlDbType.DateTime),
                           new SqlParameter("@ModifiedUserName",SqlDbType.NVarChar,50),
                           new SqlParameter("@IsAdmin",SqlDbType.Bit),
                           new SqlParameter("@IsDel",SqlDbType.Bit),
                           new SqlParameter("@IsEffective",SqlDbType.Bit),
                           new SqlParameter("@Gender",SqlDbType.Int,4),
                           new SqlParameter("@Departments",SqlDbType.NVarChar,50),
                           new SqlParameter("@Duties",SqlDbType.NVarChar,50),
                           new SqlParameter("@Ages",SqlDbType.Int,4),
                           new SqlParameter("@Phone",SqlDbType.NVarChar,50),
                           new SqlParameter("@Mobile",SqlDbType.NVarChar,20),
                           new SqlParameter("@Title",SqlDbType.NVarChar,20),
                           new SqlParameter("@Status",SqlDbType.NChar,10),
                           new SqlParameter("@Types",SqlDbType.Int,4)
                                                    };
            parameters[0].Value = model.SupplierID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Name_en;
            parameters[3].Value = model.LogName;
            parameters[4].Value = model.Password;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.CreatedDate;
            parameters[7].Value = model.CreatedUserId;
            parameters[8].Value = model.CreatedDate;
            parameters[9].Value = model.ModifiedUserName;
            parameters[10].Value = model.IsAdmin;
            parameters[11].Value = model.IsDel;
            parameters[12].Value = model.IsEffective;
            parameters[13].Value = model.Gender;
            parameters[14].Value = model.Departments;
            parameters[15].Value = model.Duties;
            parameters[16].Value = model.Ages;
            parameters[17].Value = model.Phone;
            parameters[18].Value = model.Mobile;
            parameters[19].Value = model.Title;
            parameters[20].Value = model.Status;
            parameters[21].Value = model.Types;

            object obj = null;
            if (trans != null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            else
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public void Update(SC_SupplierSubsidiaryUsers model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("   UPDATE SC_SupplierSubsidiaryUsers ");
            strSql.Append("      SET SupplierID = @SupplierID ");
            strSql.Append("     ,Name = @Name ");
            strSql.Append("     ,Name_en = @Name_en ");
            strSql.Append("     ,LogName = @LogName ");
            strSql.Append("     ,Password = @Password ");
            strSql.Append("     ,Email = @Email ");
            strSql.Append("     ,ModifiedDate = @ModifiedDate ");
            strSql.Append("     ,IsAdmin = @IsAdmin ");
            strSql.Append("     ,IsDel = @IsDel ");
            strSql.Append("     ,IsEffective = @IsEffective ");
            strSql.Append("     ,Gender = @Gender ");
            strSql.Append("     ,Departments = @Departments ");
            strSql.Append("     ,Duties = @Duties ");
            strSql.Append("     ,Ages = @Ages ");
            strSql.Append("     ,Phone = @Phone ");
            strSql.Append("     ,Mobile = @Mobile ");
            strSql.Append("     ,Title = @Title ");
            strSql.Append("     ,Status = @Status ");
            strSql.Append("     ,Types = @Types ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                           new SqlParameter("@SupplierID",SqlDbType.Int,4),
                           new SqlParameter("@Name",SqlDbType.NVarChar,200),
                           new SqlParameter("@Name_en",SqlDbType.NVarChar,200),
                           new SqlParameter("@LogName",SqlDbType.NVarChar,50),
                           new SqlParameter("@Password",SqlDbType.NVarChar,50),
                           new SqlParameter("@Email",SqlDbType.NVarChar,100),
                           new SqlParameter("@ModifiedDate",SqlDbType.DateTime),
                           new SqlParameter("@IsAdmin",SqlDbType.Bit),
                           new SqlParameter("@IsDel",SqlDbType.Bit),
                           new SqlParameter("@IsEffective",SqlDbType.Bit),
                           new SqlParameter("@Gender",SqlDbType.Int,4),
                           new SqlParameter("@Departments",SqlDbType.NVarChar,50),
                           new SqlParameter("@Duties",SqlDbType.NVarChar,50),
                           new SqlParameter("@Ages",SqlDbType.Int,4),
                           new SqlParameter("@Phone",SqlDbType.NVarChar,50),
                           new SqlParameter("@Mobile",SqlDbType.NVarChar,20),
                           new SqlParameter("@Title",SqlDbType.NVarChar,20),
                           new SqlParameter("@Status",SqlDbType.NChar,10),
                           new SqlParameter("@Types",SqlDbType.Int,4),
                           new SqlParameter("@Id",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.SupplierID;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Name_en;
            parameters[3].Value = model.LogName;
            parameters[4].Value = model.Password;
            parameters[5].Value = model.Email;
            parameters[6].Value = DateTime.Now;
            parameters[7].Value = model.IsAdmin;
            parameters[8].Value = model.IsDel;
            parameters[9].Value = model.IsEffective;
            parameters[10].Value = model.Gender;
            parameters[11].Value = model.Departments;
            parameters[12].Value = model.Duties;
            parameters[13].Value = model.Ages;
            parameters[14].Value = model.Phone;
            parameters[15].Value = model.Mobile;
            parameters[16].Value = model.Title;
            parameters[17].Value = model.Status;
            parameters[18].Value = model.Types;
            parameters[19].Value = model.ID;

            if (trans == null)
                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            else
                DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        public SC_SupplierSubsidiaryUsers GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from SC_SupplierSubsidiaryUsers where id=" + id);
            return ESP.Finance.Utility.CBO.FillObject<SC_SupplierSubsidiaryUsers>(DbHelperSQL.Query(strSql.ToString()));
        }

        public IList<SC_SupplierSubsidiaryUsers> GetList(string strCondition)
        {
            StringBuilder strSql = new StringBuilder();
            if (strCondition != string.Empty)
                strCondition = " Where " + strCondition;
            strSql.Append(" select * from SC_SupplierSubsidiaryUsers" + strCondition);
            return ESP.Finance.Utility.CBO.FillCollection<SC_SupplierSubsidiaryUsers>(DbHelperSQL.Query(strSql.ToString()));
        }

        public int Delete(int id, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierSubsidiaryUsers");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id",SqlDbType.Int,4)};
            parameters[0].Value = id;

            if (trans == null)
                return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            else
                return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }
    }
}
