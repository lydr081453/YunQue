using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

namespace ESP.Administrative.DataAccess
{
    public class RequestForSealDataProvider
    {
        public RequestForSealDataProvider()
        { }

        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "AD_RequestForSeal");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AD_RequestForSeal");
            strSql.Append(" where ID= @ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Administrative.Entity.RequestForSealInfo model)
        {
            //model.ID=GetMaxId();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AD_RequestForSeal(");
            strSql.Append("BranchId,DataNum, RequestorId, RequestorName, DepartmentId, RequestDate, SealType, FileType, FileQuantity, FileName, Remark, Files, Status, CreatedDate)");
            strSql.Append(" values (");
            strSql.Append("@BranchId,@DataNum,@RequestorId,@RequestorName,@DepartmentId,@RequestDate,@SealType,@FileType,@FileQuantity,@FileName,@Remark,@Files,@Status,@CreatedDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DataNum", SqlDbType.NVarChar),
					new SqlParameter("@RequestorId", SqlDbType.NVarChar),
					new SqlParameter("@RequestorName", SqlDbType.NVarChar),
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
					new SqlParameter("@RequestDate", SqlDbType.DateTime),
					new SqlParameter("@SealType", SqlDbType.NVarChar),
					new SqlParameter("@FileType", SqlDbType.NVarChar),
					new SqlParameter("@FileQuantity", SqlDbType.Int),
					new SqlParameter("@FileName", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@Files", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                                        new SqlParameter("@BranchId",SqlDbType.Int),};
            parameters[0].Value = model.DataNum;
            parameters[1].Value = model.RequestorId;
            parameters[2].Value = model.RequestorName;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.RequestDate;
            parameters[5].Value = model.SealType;
            parameters[6].Value = model.FileType;
            parameters[7].Value = model.FileQuantity;
            parameters[8].Value = model.FileName;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.Files;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.CreatedDate;
            parameters[13].Value = model.BranchId;

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

        public void Update(ESP.Administrative.Entity.RequestForSealInfo model)
        {
            Update(model, null);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.Administrative.Entity.RequestForSealInfo model,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AD_RequestForSeal set ");
            strSql.Append("DataNum=@DataNum,");
            strSql.Append("RequestorId=@RequestorId,");
            strSql.Append("RequestorName=@RequestorName,");
            strSql.Append("DepartmentId=@DepartmentId,");
            strSql.Append("RequestDate=@RequestDate,");
            strSql.Append("SealType=@SealType,");
            strSql.Append("FileType=@FileType,");
            strSql.Append("FileQuantity=@FileQuantity,");
            strSql.Append("FileName=@FileName,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Files=@Files,");
            strSql.Append("Status=@Status,BranchId=@BranchId,");
            strSql.Append("AuditorId=@AuditorId,");
            strSql.Append("AuditorName=@AuditorName,SANo=@SANo");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@DataNum", SqlDbType.NVarChar),
					new SqlParameter("@RequestorId", SqlDbType.Int),
					new SqlParameter("@RequestorName", SqlDbType.NVarChar),
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
					new SqlParameter("@RequestDate", SqlDbType.DateTime),
					new SqlParameter("@SealType", SqlDbType.NVarChar),
					new SqlParameter("@FileType", SqlDbType.NVarChar),
					new SqlParameter("@FileQuantity", SqlDbType.Int),
					new SqlParameter("@FileName", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@Files", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@BranchId",SqlDbType.Int),
                    new SqlParameter("@AuditorId", SqlDbType.Int),
					new SqlParameter("@AuditorName", SqlDbType.NVarChar),
                    new SqlParameter("@SANo",SqlDbType.NVarChar),                    
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.DataNum;
            parameters[2].Value = model.RequestorId;
            parameters[3].Value = model.RequestorName;
            parameters[4].Value = model.DepartmentId;
            parameters[5].Value = model.RequestDate;
            parameters[6].Value = model.SealType;
            parameters[7].Value = model.FileType;
            parameters[8].Value = model.FileQuantity;
            parameters[9].Value = model.FileName;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.Files;
            parameters[12].Value = model.Status;
            parameters[13].Value = model.BranchId;
            parameters[14].Value = model.AuditorId;
            parameters[15].Value = model.AuditorName;
            parameters[16].Value = model.SANo;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete AD_RequestForSeal ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.RequestForSealInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(Defult_SearchSql);
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
           
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
                return ESP.Administrative.Common.CBO.FillObject<RequestForSealInfo>(ds);
            else
                return null;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Administrative.Entity.RequestForSealInfo GetModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(Defult_SearchSql);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataSet ds = DbHelperSQL.Query(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
                return ESP.Administrative.Common.CBO.FillObject<RequestForSealInfo>(ds);
            else
                return null;
        }

        public static string Defult_SearchSql = @"select a.*,b.BranchName,c.level1Id as DeptId1,c.level1 as DeptName1,
                                                    c.level2Id as DeptId2,c.level2 as DeptName2,
                                                    c.level3Id as DeptId3,c.level3 as DeptName3
                                                    from AD_RequestForSeal as a 
                                                    inner join F_Branch as b on a.BranchId =b.BranchID
                                                    inner join V_Department as c on a.DepartmentId=c.level3Id";

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<RequestForSealInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(Defult_SearchSql);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by a.CreatedDate desc");
            return ESP.Administrative.Common.CBO.FillCollection<RequestForSealInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 获取有审批权限的列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<RequestForSealInfo> GetAuditList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.*,b.AuditStatus from ("+Defult_SearchSql+@") as a
                            inner join F_ConsumptionAudit as b on a.Id=b.BatchID 
                            where a.Status not in (" + (int)ESP.Administrative.Common.Status.RequestForSealStatus.Save + "," + (int)ESP.Administrative.Common.Status.RequestForSealStatus.Rejected + ") and b.FormType =" + (int)ESP.Finance.Utility.FormType.RequestForSeal);
            if (strWhere != "")
                strSql.Append(strWhere);
            strSql.Append(" order by a.CreatedDate desc");
            return ESP.Administrative.Common.CBO.FillCollection<RequestForSealInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

        /// <summary>
        /// 用印申请审批
        /// </summary>
        /// <param name="requestForSealModel"></param>
        /// <param name="currentUser"></param>
        /// <param name="status"></param>
        /// <param name="suggestion"></param>
        /// <returns></returns>
        public int Audit(RequestForSealInfo requestForSealModel, ESP.Compatible.Employee currentUser, int status, string suggestion)
        {
            int ret = 0;
            string term = string.Empty;
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string DelegateUsers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(int.Parse(currentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }
            DelegateUsers = DelegateUsers.TrimEnd(',');

            term = " batchId=@batchId and AuditStatus=@AuditStatus and formType=@formType";

            SqlParameter p1 = new SqlParameter("@batchId", SqlDbType.Int, 4);
            p1.SqlValue = requestForSealModel.Id;
            paramlist.Add(p1);

            SqlParameter p2 = new SqlParameter("@formType", SqlDbType.Int, 4);
            p2.SqlValue = (int)ESP.Finance.Utility.FormType.RequestForSeal ;
            paramlist.Add(p2);

            SqlParameter p3 = new SqlParameter("@AuditStatus", SqlDbType.Int, 4);
            p3.SqlValue = (int)AuditHistoryStatus.UnAuditing;
            paramlist.Add(p3);

            var auditList = ESP.Finance.BusinessLogic.ConsumptionAuditManager.GetList(term, paramlist);

            if (auditList == null || auditList.Count == 0)
            {
                ret = 0;
            }
            else
            {
                //当前审核人
                ConsumptionAuditInfo firstRole = auditList[0];
                //下一级审核人
                ConsumptionAuditInfo nextRole = null;
                if (auditList.Count >= 2)
                    nextRole = auditList[1];

                if (firstRole.AuditorUserID == int.Parse(currentUser.SysID) || DelegateUsers.IndexOf(firstRole.AuditorUserID.ToString()) >= 0)//审核人与登录人校验
                {
                    if (status == (int)AuditHistoryStatus.PassAuditing)
                    {
                        if (nextRole != null)
                        {
                            requestForSealModel.AuditorId = nextRole.AuditorUserID;
                            requestForSealModel.AuditorName = nextRole.AuditorEmployeeName;
                        }
                        switch (firstRole.AuditType.Value)
                        {
                            case 2://业务第一级审批，状态不变

                                break;
                            case 5://
                                break;
                            case 8://CEO审批通过，状态改为审批完成
                                requestForSealModel.Status = Common.Status.RequestForSealStatus.Audited;
                                break;
                        }
                    }
                    else
                    {
                        requestForSealModel.Status = Common.Status.RequestForSealStatus.Rejected;
                    }

                    //更新当前审批流角色状态
                    firstRole.AuditStatus = status;
                    firstRole.AuditDate = DateTime.Now;
                    firstRole.Suggestion = suggestion;


                    #region begin transaction
                    using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
                    {

                        conn.Open();
                        SqlTransaction trans = conn.BeginTransaction();
                        try
                        {
                            Update(requestForSealModel, trans);//更新批次状态
                            ret++;
                            ESP.Finance.BusinessLogic.ConsumptionAuditManager.Update(firstRole, trans, ESP.Finance.Utility.FormType.RequestForSeal);//更新工作流状态
                            ret++;
                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                        }
                        finally
                        {
                            if (trans.Connection != null && trans.Connection.State != ConnectionState.Closed)
                            {
                                trans.Connection.Close();
                            }
                            if (trans != null)
                                trans = null;
                        }
                    }
                    #endregion

                    try
                    {
                        ESP.HumanResource.Entity.EmployeeBaseInfo creator = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(requestForSealModel.RequestorId);

                        ESP.HumanResource.Entity.EmployeeBaseInfo nextEmp = null;
                        string nextAuditorMail = string.Empty;
                        if (nextRole != null)
                        {
                            nextEmp = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(nextRole.AuditorUserID);
                            nextAuditorMail = nextEmp.InternalEmail;
                        }

                        ESP.Administrative.Common.SendMailHelper.SendMailRequestForSealAudit(requestForSealModel,status, creator.InternalEmail, currentUser.Name, nextAuditorMail);
                    }
                    catch { }
                }
                else
                {
                    ret = 0;
                }
            }

            return ret;
        }

        #endregion  成员方法
    }
}