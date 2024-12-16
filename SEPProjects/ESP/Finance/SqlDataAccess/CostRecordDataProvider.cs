using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
using ESP.Finance.IDataAccess;

namespace ESP.Finance.DataAccess
{
        internal class CostRecordDataProvider :ICostRecordProvider
        {
            #region  成员方法
            /// <summary>
            /// 是否存在该记录
            /// </summary>
            public bool Exists(int RecordID)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(1) from F_CostRecord");
                strSql.Append(" where RecordID= @RecordID");
                SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.Int,4)
				};
                parameters[0].Value = RecordID;
                return DbHelperSQL.Exists(strSql.ToString(), parameters);
            }


            /// <summary>
            /// 增加一条数据
            /// </summary>
            public int Add(ESP.Finance.Entity.CostRecordInfo model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into F_CostRecord(");
                strSql.Append("PRID,PRNO,Requestor,GroupName,AppDate,AppAmount,PrType,PNTotal,Description,TypeID,TypeName,ProjectID,ProjectCode,SupplierName,CostPreAmount,TypeTotalAmount,DepartmentID,DepartmentName,PaidAmount,UnPaidAmount,ReturnType,RecordType,AddDate)");
                strSql.Append(" values (");
                strSql.Append("@PRID,@PRNO,@Requestor,@GroupName,@AppDate,@AppAmount,@PrType,@PNTotal,@Description,@TypeID,@TypeName,@ProjectID,@ProjectCode,@SupplierName,@CostPreAmount,@TypeTotalAmount,@DepartmentID,@DepartmentName,@PaidAmount,@UnPaidAmount,@ReturnType,@RecordType,getdate())");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@PRNO", SqlDbType.NVarChar),
					new SqlParameter("@Requestor", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupName", SqlDbType.NVarChar),
					new SqlParameter("@AppDate", SqlDbType.DateTime),
					new SqlParameter("@AppAmount", SqlDbType.Decimal,13),
					new SqlParameter("@PrType", SqlDbType.Int,4),
					new SqlParameter("@PNTotal", SqlDbType.Decimal,13),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@TypeName", SqlDbType.NVarChar),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar),
					new SqlParameter("@CostPreAmount", SqlDbType.Decimal,13),
					new SqlParameter("@TypeTotalAmount", SqlDbType.Decimal,13),
					new SqlParameter("@DepartmentID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@PaidAmount", SqlDbType.Decimal,13),
					new SqlParameter("@UnPaidAmount", SqlDbType.Decimal,13),
					new SqlParameter("@ReturnType", SqlDbType.Int,4),
					new SqlParameter("@RecordType", SqlDbType.Int,4)};
                parameters[0].Value = model.PRID;
                parameters[1].Value = model.PRNO;
                parameters[2].Value = model.Requestor;
                parameters[3].Value = model.GroupName;
                parameters[4].Value = model.AppDate;
                parameters[5].Value = model.AppAmount;
                parameters[6].Value = model.PrType;
                parameters[7].Value = model.PNTotal;
                parameters[8].Value = model.Description;
                parameters[9].Value = model.TypeID;
                parameters[10].Value = model.TypeName;
                parameters[11].Value = model.ProjectID;
                parameters[12].Value = model.ProjectCode;
                parameters[13].Value = model.SupplierName;
                parameters[14].Value = model.CostPreAmount;
                parameters[15].Value = model.TypeTotalAmount;
                parameters[16].Value = model.DepartmentID;
                parameters[17].Value = model.DepartmentName;
                parameters[18].Value = model.PaidAmount;
                parameters[19].Value = model.UnPaidAmount;
                parameters[20].Value = model.ReturnType;
                parameters[21].Value = model.RecordType;

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

            public int Add(ESP.Finance.Entity.CostRecordInfo model,SqlTransaction trans)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into F_CostRecord(");
                strSql.Append("PRID,PRNO,Requestor,GroupName,AppDate,AppAmount,PrType,PNTotal,Description,TypeID,TypeName,ProjectID,ProjectCode,SupplierName,CostPreAmount,TypeTotalAmount,DepartmentID,DepartmentName,PaidAmount,UnPaidAmount,ReturnType,RecordType,AddDate)");
                strSql.Append(" values (");
                strSql.Append("@PRID,@PRNO,@Requestor,@GroupName,@AppDate,@AppAmount,@PrType,@PNTotal,@Description,@TypeID,@TypeName,@ProjectID,@ProjectCode,@SupplierName,@CostPreAmount,@TypeTotalAmount,@DepartmentID,@DepartmentName,@PaidAmount,@UnPaidAmount,@ReturnType,@RecordType,getdate())");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@PRNO", SqlDbType.NVarChar),
					new SqlParameter("@Requestor", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupName", SqlDbType.NVarChar),
					new SqlParameter("@AppDate", SqlDbType.DateTime),
					new SqlParameter("@AppAmount", SqlDbType.Decimal,13),
					new SqlParameter("@PrType", SqlDbType.Int,4),
					new SqlParameter("@PNTotal", SqlDbType.Decimal,13),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@TypeName", SqlDbType.NVarChar),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar),
					new SqlParameter("@CostPreAmount", SqlDbType.Decimal,13),
					new SqlParameter("@TypeTotalAmount", SqlDbType.Decimal,13),
					new SqlParameter("@DepartmentID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@PaidAmount", SqlDbType.Decimal,13),
					new SqlParameter("@UnPaidAmount", SqlDbType.Decimal,13),
					new SqlParameter("@ReturnType", SqlDbType.Int,4),
					new SqlParameter("@RecordType", SqlDbType.Int,4)};
                parameters[0].Value = model.PRID;
                parameters[1].Value = model.PRNO;
                parameters[2].Value = model.Requestor;
                parameters[3].Value = model.GroupName;
                parameters[4].Value = model.AppDate;
                parameters[5].Value = model.AppAmount;
                parameters[6].Value = model.PrType;
                parameters[7].Value = model.PNTotal;
                parameters[8].Value = model.Description;
                parameters[9].Value = model.TypeID;
                parameters[10].Value = model.TypeName;
                parameters[11].Value = model.ProjectID;
                parameters[12].Value = model.ProjectCode;
                parameters[13].Value = model.SupplierName;
                parameters[14].Value = model.CostPreAmount;
                parameters[15].Value = model.TypeTotalAmount;
                parameters[16].Value = model.DepartmentID;
                parameters[17].Value = model.DepartmentName;
                parameters[18].Value = model.PaidAmount;
                parameters[19].Value = model.UnPaidAmount;
                parameters[20].Value = model.ReturnType;
                parameters[21].Value = model.RecordType;

                object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans, parameters);
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
            public void Update(ESP.Finance.Entity.CostRecordInfo model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update F_CostRecord set ");
                strSql.Append("PRID=@PRID,");
                strSql.Append("PRNO=@PRNO,");
                strSql.Append("Requestor=@Requestor,");
                strSql.Append("GroupName=@GroupName,");
                strSql.Append("AppDate=@AppDate,");
                strSql.Append("AppAmount=@AppAmount,");
                strSql.Append("PrType=@PrType,");
                strSql.Append("PNTotal=@PNTotal,");
                strSql.Append("Description=@Description,");
                strSql.Append("TypeID=@TypeID,");
                strSql.Append("TypeName=@TypeName,");
                strSql.Append("ProjectID=@ProjectID,");
                strSql.Append("ProjectCode=@ProjectCode,");
                strSql.Append("SupplierName=@SupplierName,");
                strSql.Append("CostPreAmount=@CostPreAmount,");
                strSql.Append("TypeTotalAmount=@TypeTotalAmount,");
                strSql.Append("DepartmentID=@DepartmentID,");
                strSql.Append("DepartmentName=@DepartmentName,");
                strSql.Append("PaidAmount=@PaidAmount,");
                strSql.Append("UnPaidAmount=@UnPaidAmount,");
                strSql.Append("ReturnType=@ReturnType,");
                strSql.Append("RecordType=@RecordType");
                strSql.Append(" where RecordID=@RecordID");
                SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.Int,4),
					new SqlParameter("@PRID", SqlDbType.Int,4),
					new SqlParameter("@PRNO", SqlDbType.NVarChar),
					new SqlParameter("@Requestor", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupName", SqlDbType.NVarChar),
					new SqlParameter("@AppDate", SqlDbType.DateTime),
					new SqlParameter("@AppAmount", SqlDbType.Decimal,13),
					new SqlParameter("@PrType", SqlDbType.Int,4),
					new SqlParameter("@PNTotal", SqlDbType.Decimal,13),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@TypeName", SqlDbType.NVarChar),
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar),
					new SqlParameter("@CostPreAmount", SqlDbType.Decimal,13),
					new SqlParameter("@TypeTotalAmount", SqlDbType.Decimal,13),
					new SqlParameter("@DepartmentID", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
					new SqlParameter("@PaidAmount", SqlDbType.Decimal,13),
					new SqlParameter("@UnPaidAmount", SqlDbType.Decimal,13),
					new SqlParameter("@ReturnType", SqlDbType.Int,4),
					new SqlParameter("@RecordType", SqlDbType.Int,4)};
                parameters[0].Value = model.RecordID;
                parameters[1].Value = model.PRID;
                parameters[2].Value = model.PRNO;
                parameters[3].Value = model.Requestor;
                parameters[4].Value = model.GroupName;
                parameters[5].Value = model.AppDate;
                parameters[6].Value = model.AppAmount;
                parameters[7].Value = model.PrType;
                parameters[8].Value = model.PNTotal;
                parameters[9].Value = model.Description;
                parameters[10].Value = model.TypeID;
                parameters[11].Value = model.TypeName;
                parameters[12].Value = model.ProjectID;
                parameters[13].Value = model.ProjectCode;
                parameters[14].Value = model.SupplierName;
                parameters[15].Value = model.CostPreAmount;
                parameters[16].Value = model.TypeTotalAmount;
                parameters[17].Value = model.DepartmentID;
                parameters[18].Value = model.DepartmentName;
                parameters[19].Value = model.PaidAmount;
                parameters[20].Value = model.UnPaidAmount;
                parameters[21].Value = model.ReturnType;
                parameters[22].Value = model.RecordType;

                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }

            /// <summary>
            /// 删除一条数据
            /// </summary>
            public void Delete(int RecordID)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete F_CostRecord ");
                strSql.Append(" where RecordID=@RecordID");
                SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.Int,4)
				};
                parameters[0].Value = RecordID;
                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }

            public void DeleteByProject(int projectId)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete F_CostRecord where ProjectId=@ProjectId");
                SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4)
				};
                parameters[0].Value = projectId;
                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }
            public void DeleteAll(SqlTransaction trans)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete F_CostRecord ");
                DbHelperSQL.ExecuteSql(strSql.ToString(),trans);
            }

            /// <summary>
            /// 得到一个对象实体
            /// </summary>
            public ESP.Finance.Entity.CostRecordInfo GetModel(int RecordID)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from F_CostRecord ");
                strSql.Append(" where RecordID=@RecordID");
                SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.Int,4)};
                parameters[0].Value = RecordID;
                ESP.Finance.Entity.CostRecordInfo model = new ESP.Finance.Entity.CostRecordInfo();
                 return CBO.FillObject<ESP.Finance.Entity.CostRecordInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            }
            /// <summary>
            /// 获得数据列表
            /// </summary>
            public List<ESP.Finance.Entity.CostRecordInfo> GetList(string strWhere,List<SqlParameter> param)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select [RecordID],[PRID],[PRNO],[Requestor],[GroupName],[AppDate],[AppAmount],[PrType],[PNTotal],[Description],[TypeID],[TypeName],[ProjectID],[ProjectCode],[SupplierName],[CostPreAmount],[TypeTotalAmount],[DepartmentID],[DepartmentName],[PaidAmount],[UnPaidAmount],[ReturnType],[RecordType] ");
                strSql.Append(" FROM F_CostRecord ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return CBO.FillCollection<ESP.Finance.Entity.CostRecordInfo>(DbHelperSQL.Query(strSql.ToString(), (param == null ? null : param.ToArray())));
            }

            #endregion  成员方法
        }
    }



