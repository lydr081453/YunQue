using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

namespace ESP.Finance.DataAccess
{
    internal class FinanceObjectProvider : ESP.Finance.IDataAccess.IFinanceObjectProvider
    {
        #region IFinanceObjectProvider 成员

        public int Add(ESP.Finance.Entity.FinanceObjectInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_FinanceObject(");
            strSql.Append("ObjectType,ObjectCode,ObjectName,ObjectId,Code0,code1,code2,code3,code4,code5,code6,CredenceTypeCode,RowLevel,RowDesc)");
            strSql.Append(" values (");
            strSql.Append("@ObjectType,@ObjectCode,@ObjectName,@ObjectId,@Code0,@code1,@code2,@code3,@code4,@code5,@code6,@CredenceTypeCode,@RowLevel,@RowDesc)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ObjectType", SqlDbType.NChar,10),
					new SqlParameter("@ObjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ObjectName", SqlDbType.NVarChar,100),
                    new SqlParameter("@ObjectId", SqlDbType.Int,4),
                    new SqlParameter("@Code0", SqlDbType.NChar,10),
                    new SqlParameter("@code1", SqlDbType.NChar,10),
                    new SqlParameter("@code2", SqlDbType.NChar,10),
                    new SqlParameter("@code3", SqlDbType.NChar,10),
                    new SqlParameter("@code4", SqlDbType.NChar,10),
                    new SqlParameter("@code5", SqlDbType.NChar,10),
                    new SqlParameter("@code6", SqlDbType.NChar,10),
                    new SqlParameter("@CredenceTypeCode", SqlDbType.NChar,10),
                    new SqlParameter("@RowLevel", SqlDbType.Int,4),
                    new SqlParameter("@RowDesc", SqlDbType.NVarChar,50)

                                        };
            parameters[0].Value = model.ObjectType;
            parameters[1].Value = model.ObjectCode;
            parameters[2].Value = model.ObjectName;
            parameters[3].Value = model.ObjectId;
            parameters[4].Value = model.Code0;
            parameters[5].Value = model.code1;
            parameters[6].Value = model.code2;
            parameters[7].Value = model.code3;
            parameters[8].Value = model.code4;
            parameters[9].Value = model.code5;
            parameters[10].Value = model.code6;
            parameters[11].Value = model.CredenceTypeCode;
            parameters[12].Value = model.RowLevel;
            parameters[13].Value = model.RowDesc;


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

        public int Update(ESP.Finance.Entity.FinanceObjectInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_FinanceObject(");
            strSql.Append("ObjectType=@ObjectType,ObjectCode=@ObjectCode,ObjectName=@ObjectName,ObjectId=@ObjectId,Code0=@Code0,code1=@code1,code2=@code2,code3=@code3,code4=@code4,code5=@code5,code6=@code6,CredenceTypeCode=@CredenceTypeCode,RowLevel=@RowLevel,RowDesc=@RowDesc)");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@ObjectType", SqlDbType.NChar,10),
					new SqlParameter("@ObjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ObjectName", SqlDbType.NVarChar,100),
                    new SqlParameter("@ObjectId", SqlDbType.Int,4),
                    new SqlParameter("@Code0", SqlDbType.NChar,10),
                    new SqlParameter("@code1", SqlDbType.NChar,10),
                    new SqlParameter("@code2", SqlDbType.NChar,10),
                    new SqlParameter("@code3", SqlDbType.NChar,10),
                    new SqlParameter("@code4", SqlDbType.NChar,10),
                    new SqlParameter("@code5", SqlDbType.NChar,10),
                    new SqlParameter("@code6", SqlDbType.NChar,10),
                    new SqlParameter("@CredenceTypeCode", SqlDbType.NChar,10),
                    new SqlParameter("@RowLevel", SqlDbType.Int,4),
                    new SqlParameter("@RowDesc", SqlDbType.NVarChar,50),
                     new SqlParameter("@Id", SqlDbType.Int,4),

                                        };
            parameters[0].Value = model.ObjectType;
            parameters[1].Value = model.ObjectCode;
            parameters[2].Value = model.ObjectName;
            parameters[3].Value = model.ObjectId;
            parameters[4].Value = model.Code0;
            parameters[5].Value = model.code1;
            parameters[6].Value = model.code2;
            parameters[7].Value = model.code3;
            parameters[8].Value = model.code4;
            parameters[9].Value = model.code5;
            parameters[10].Value = model.code6;
            parameters[11].Value = model.CredenceTypeCode;
            parameters[12].Value = model.RowLevel;
            parameters[13].Value = model.RowDesc;
            parameters[14].Value = model.Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters); 
        }

        public int Delete(int objectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_FinanceObject ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = objectId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public ESP.Finance.Entity.FinanceObjectInfo GetModel(int objectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ObjectType,ObjectCode,ObjectName,ObjectId,Code0,code1,code2,code3,code4,code5,code6,CredenceTypeCode,RowLevel,RowDesc from F_FinanceObject ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = objectId;

            return CBO.FillObject<FinanceObjectInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        public ESP.Finance.Entity.FinanceObjectInfo GetModel(string CredenceTypeCode,int RowLevel,string RowDesc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 Id,ObjectType,ObjectCode,ObjectName,ObjectId,Code0,code1,code2,code3,code4,code5,code6,CredenceTypeCode,RowLevel,RowDesc from F_FinanceObject ");
            strSql.Append(" where CredenceTypeCode=@CredenceTypeCode and RowLevel=@RowLevel and RowDesc=@RowDesc");
            SqlParameter[] parameters = {
                    new SqlParameter("@CredenceTypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@RowLevel", SqlDbType.Int,4),
                    new SqlParameter("@RowDesc", SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = CredenceTypeCode;
            parameters[1].Value = RowLevel;
            parameters[2].Value = RowDesc;
            return CBO.FillObject<FinanceObjectInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        public IList<ESP.Finance.Entity.FinanceObjectInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ObjectType,ObjectCode,ObjectName,ObjectId,Code0,code1,code2,code3,code4,code5,code6,CredenceTypeCode,RowLevel,RowDesc from F_FinanceObject ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<FinanceObjectInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        #endregion
    }
}
