using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.Entity;
using ESP.Finance.Entity;
using ESP.Purchase.Common;


namespace ESP.Purchase.DataAccess
{
    public class CostRecordsProvider
    {
        public CostRecordsProvider()
        { }

        public int InsertProject(ESP.Finance.Entity.ProjectInfo projectModel, IList<ContractCostInfo> costlist, IList<ProjectExpenseInfo> expenselist, SqlTransaction trans)
        {
            int ret = 0;
            DeleteProject(projectModel.ProjectId, State.CostRecord_FormType_Project, trans);

            foreach (ESP.Finance.Entity.ContractCostInfo cost in costlist)
            {
                CostRecordsInfo model = new CostRecordsInfo();
                model.ProjectId = projectModel.ProjectId;
                model.ProjectCode = projectModel.ProjectCode;

                model.DepartmentId = projectModel.GroupID.Value;
                model.DepartmentName = projectModel.GroupName;

                model.MaterialId = cost.CostTypeID.Value;
                model.MaterialName = cost.Description;
                model.PreAmount = cost.Cost.Value;
                model.FactAmount = 0;
                model.FormType = State.CostRecord_FormType_Project;
                model.FormId = 0;
                model.RefFormId = 0;
                model.ParentId = 0;
                model.Remark = string.Empty;
                model.Status = 0;
                model.CreateDate = DateTime.Now;
                Insert(model, trans);
                ret++;
            }
            foreach (ESP.Finance.Entity.ProjectExpenseInfo exp in expenselist)
            {
                CostRecordsInfo model = new CostRecordsInfo();
                model.ProjectId = projectModel.ProjectId;
                model.ProjectCode = projectModel.ProjectCode;

                model.DepartmentId = projectModel.GroupID.Value;
                model.DepartmentName = projectModel.GroupName;

                model.MaterialId = 0;
                model.MaterialName = exp.Description;
                model.PreAmount = exp.Expense.Value;
                model.FactAmount = 0;
                model.FormType = State.CostRecord_FormType_Project;
                model.FormId = 0;
                model.RefFormId = 0;
                model.ParentId = 0;
                model.Remark = string.Empty;
                model.Status = 0;
                model.CreateDate = DateTime.Now;
                Insert(model, trans);
                ret++;
            }
            return ret;
        }

        public int InsertSupporter(ESP.Finance.Entity.SupporterInfo supportModel, IList<SupporterCostInfo> costlist, IList<SupporterExpenseInfo> expenselist, SqlTransaction trans)
        {
            int ret = 0;

            DeleteSupporter(supportModel.ProjectID, supportModel.SupportID, State.CostRecord_FormType_Project, trans);

            foreach (ESP.Finance.Entity.SupporterCostInfo cost in costlist)
            {
                CostRecordsInfo model = new CostRecordsInfo();
                model.ProjectId = supportModel.ProjectID;
                model.ProjectCode = supportModel.ProjectCode;

                model.SupporterId = supportModel.SupportID;
                model.DepartmentId = supportModel.GroupID.Value;
                model.DepartmentName = supportModel.GroupName;

                model.MaterialId = cost.CostTypeID.Value;
                model.MaterialName = cost.Description;
                model.PreAmount = cost.Amounts;
                model.FactAmount = 0;
                model.FormType = State.CostRecord_FormType_Project;
                model.FormId = 0;
                model.RefFormId = 0;
                model.ParentId = 0;
                model.Remark = string.Empty;
                model.Status = 0;
                model.CreateDate = DateTime.Now;
                Insert(model, trans);
                ret++;
            }
            foreach (ESP.Finance.Entity.SupporterExpenseInfo exp in expenselist)
            {
                CostRecordsInfo model = new CostRecordsInfo();
                model.ProjectId = supportModel.ProjectID;
                model.ProjectCode = supportModel.ProjectCode;

                model.SupporterId = supportModel.SupportID;
                model.DepartmentId = supportModel.GroupID.Value;
                model.DepartmentName = supportModel.GroupName;

                model.MaterialId = 0;
                model.MaterialName = exp.Description;
                model.PreAmount = exp.Expense.Value;
                model.FactAmount = 0;
                model.FormType = State.CostRecord_FormType_Project;
                model.FormId = 0;
                model.RefFormId = 0;
                model.ParentId = 0;
                model.Remark = string.Empty;
                model.Status = 0;
                model.CreateDate = DateTime.Now;
                Insert(model, trans);
                ret++;
            }
            return ret;
        }


        public int InsertPR(ESP.Purchase.Entity.CostRecordsInfo recordModel, SqlTransaction trans)
        {
            int ret = 0;
            decimal total = GetSumPR(recordModel.FormId, trans);
           
            if (recordModel.Status != 2 && recordModel.Status != 0)
                recordModel.PreAmount = 0-recordModel.PreAmount;
            //PR单驳回或撤销，插入正数金额 
            else
                recordModel.PreAmount = 0-total;
            recordModel.FactAmount = 0;


            if (recordModel.FormType == ESP.Purchase.Common.State.CostRecord_FormType_PRNew)
            {
                ret = Insert(recordModel, trans);
                ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(recordModel.FormId, trans);
                recordModel.PreAmount = generalModel.MediaOldAmount;
                ret += Insert(recordModel, trans);
            }
            else
            {

                ret = Insert(recordModel, trans);
                //
                if (recordModel.Status != 2 && recordModel.Status != 0 && total!=0)
                {
                    recordModel.PreAmount = 0-total;
                    ret += Insert(recordModel, trans);
                }
            }
            return ret;
        }

        public int InsertPN(CostRecordsInfo recordModel, SqlTransaction trans)
        {
            int ret = 0;
            recordModel.PreAmount = 0-recordModel.PreAmount;
            recordModel.FactAmount = 0;
           
            //稿费或者对私单据
            if (recordModel.FormType == ESP.Purchase.Common.State.CostRecord_FormType_PNNew)
            {
                Insert(recordModel, trans);
                recordModel.PreAmount = 0-recordModel.PreAmount;
                Insert(recordModel, trans);
            }
            //其他类型单据通用处理
            else
            {
                decimal total = GetSumPN(recordModel.FormId, trans);
                //PN reject
                if (recordModel.Status == 0 || recordModel.Status == 1 || recordModel.Status == -1)
                {
                    recordModel.PreAmount = 0 - total;
                    Insert(recordModel, trans);
                }
                else if (recordModel.Status == 140)//payment
                {
                    recordModel.FactAmount = -recordModel.PreAmount;
                    recordModel.PreAmount = 0;
                    Insert(recordModel, trans);
                }
                else
                {
                    Insert(recordModel, trans);
                    CostRecordsInfo costModel = new CostRecordsInfo();
                    ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(recordModel.FormId, trans);
                    ESP.Purchase.Entity.PaymentPeriodInfo periodModel = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(recordModel.FormId, trans);
                    if (returnModel.PRID != null && returnModel.PRID.Value != 0)
                    {
                        CostRecordsInfo periodCostModel = GetModel(returnModel.PRID.Value, State.CostRecord_FormType_PR, trans);
                        if (periodCostModel != null)
                        {
                            periodCostModel.CreateDate = DateTime.Now;
                            periodCostModel.PreAmount = periodModel.expectPaymentPrice;
                            periodCostModel.FormId = recordModel.FormId;
                            periodCostModel.FormType = State.CostRecord_FormType_PaymentPeriod;
                            Insert(periodCostModel, trans);
                        }
                    }
                }
            }
            return ret;
        }

        private decimal GetSumPN(int formid, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            decimal total = 0;
            strSql.Append("select sum(preamount) from P_CostRecords where formid=@formid and (formtype=@formtype or formtype=@formtype2)");
            SqlParameter[] parameters = {
					new SqlParameter("@formid", SqlDbType.Int,4),
					new SqlParameter("@formtype", SqlDbType.Int,4),
                    new SqlParameter("@formtype2", SqlDbType.Int,4)
                                           };
            parameters[0].Value = formid;
            parameters[1].Value = State.CostRecord_FormType_PN;
            parameters[2].Value = State.CostRecord_FormType_PaymentPeriod;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            if (obj == null || obj == DBNull.Value)
                total = 0;
            else
                total = Convert.ToDecimal(obj);
            return total;
        }

        private decimal GetSumPR(int formid, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            decimal total = 0;
            strSql.Append("select sum(preamount) from P_CostRecords where formid=@formid and formtype=@formtype");
            SqlParameter[] parameters = {
					new SqlParameter("@formid", SqlDbType.Int,4),
					new SqlParameter("@formtype", SqlDbType.Int,4)
                                           };
            parameters[0].Value = formid;
            parameters[1].Value = State.CostRecord_FormType_PR;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            if (obj == null || obj == DBNull.Value)
                total = 0;
            else
                total = Convert.ToDecimal(obj);
            return total;
        }

        public CostRecordsInfo GetModel(int formid, int formtype, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from P_CostRecords where formid=@formid and formtype=@formtype");
            SqlParameter[] parameters = {
					new SqlParameter("@formid", SqlDbType.Int,4),
					new SqlParameter("@formtype", SqlDbType.Int,4)
                                         };
            parameters[0].Value = formid;
            parameters[1].Value = formtype;
            return ESP.Finance.Utility.CBO.FillObject<CostRecordsInfo>(DbHelperSQL.Query(strSql.ToString(),trans.Connection,trans, parameters));
        }

        public int InsertOOP(CostRecordsInfo recordModel, SqlTransaction trans)
        {
            int ret = 0;
            ret = Insert(recordModel, trans);
            return ret;
        }
        private int DeleteProject(int projectid, int formtype, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete P_CostRecords where projectid =@projectid and supporterid=0 and formtype=@formtype");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@formtype", SqlDbType.Int,4)
                                           };
            parameters[0].Value = projectid;
            parameters[1].Value = formtype;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        private int DeleteSupporter(int projectid, int supporterid, int formtype, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete P_CostRecords where projectid =@projectid and supporterid=@supporterid and formtype=@formtype");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
                    new SqlParameter("@supporterid", SqlDbType.Int,4),
					new SqlParameter("@formtype", SqlDbType.Int,4)
                                           };
            parameters[0].Value = projectid;
            parameters[1].Value = supporterid;
            parameters[2].Value = formtype;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        private int Insert(CostRecordsInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into P_CostRecords(");
            strSql.Append("ProjectId,ProjectCode,SupporterId,DepartmentId,DepartmentName,MaterialId,MaterialName,PreAmount,FactAmount,FormId,RefFormId,ParentId,FormType,Status,Remark,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@ProjectId,@ProjectCode,@SupporterId,@DepartmentId,@DepartmentName,@MaterialId,@MaterialName,@PreAmount,@FactAmount,@FormId,@RefFormId,@ParentId,@FormType,@Status,@Remark,@CreateDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SupporterId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentId", SqlDbType.Int,4),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50),
					new SqlParameter("@MaterialId", SqlDbType.Int,4),
					new SqlParameter("@MaterialName", SqlDbType.NVarChar,100),
					new SqlParameter("@PreAmount", SqlDbType.Decimal,18),
					new SqlParameter("@FactAmount", SqlDbType.Decimal,18),
					new SqlParameter("@FormId", SqlDbType.Int,4),
					new SqlParameter("@RefFormId", SqlDbType.Int,4),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@FormType", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,2000),
					new SqlParameter("@CreateDate", SqlDbType.DateTime,8)};
            parameters[0].Value = model.ProjectId;
            parameters[1].Value = model.ProjectCode;
            parameters[2].Value = model.SupporterId;
            parameters[3].Value = model.DepartmentId;
            parameters[4].Value = model.DepartmentName;
            parameters[5].Value = model.MaterialId;
            parameters[6].Value = model.MaterialName;
            parameters[7].Value = model.PreAmount;
            parameters[8].Value = model.FactAmount;
            parameters[9].Value = model.FormId;
            parameters[10].Value = model.RefFormId;
            parameters[11].Value = model.ParentId;
            parameters[12].Value = model.FormType;
            parameters[13].Value = model.Status;
            parameters[14].Value = model.RemarkModel == null ? model.Remark : GetRemark(model.RemarkModel);
            parameters[15].Value = model.CreateDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        private string GetRemark(CostRecord_Remark model)
        {
            return model.Role + "$" + model.Operation + "$" + model.FormCode + "$" + model.Log;
        }

        private CostRecord_Remark GetRemarkModel(string remark)
        {
            string[] strs = remark.Split('$');
            CostRecord_Remark model = new CostRecord_Remark();
            model.Role = strs[0];
            model.Operation = strs[1];
            model.FormCode = strs[2];
            model.Log = strs[3];

            return model;
        }

        private int GetSupporter(int projectid, int departmentid)
        {
            int supportid = 0;
            return supportid;
        }

        private int deleteOOP(int formid)
        {
            int ret = 0;
            if (formid > 0)
            {

            }
            return ret;
        }
        private bool isLastPaymentPeriod(int formid)
        {
            bool ret = false;

            return ret;
        }
    }
}
