using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data;

namespace FinanceWeb.ExpenseAccount
{
    public partial class ExpenseAccountAuditList : ESP.Web.UI.PageBase
    {
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        ESP.Finance.Entity.ExpenseAccountExtendsInfo model = null;
        ESP.Finance.SqlDataAccess.WorkItem workitem = null;
        bool isFinances = false;
        bool isF1 = false;
        bool isF2orF3 = false;
        bool isFa = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Server.ScriptTimeout = 60000;
            this.ClientScript.RegisterStartupScript(typeof(ScriptManager), "AppInitialize", "", true);

            if (!IsPostBack)
            {
                bindList();
                bindBranch();
                // bindHistList();
            }
        }

        protected void bindBranch()
        {
            //string whereStr = string.Format(" FirstFinanceID = {0} or otherfinancialusers like '%,{1},%'", CurrentUserID,CurrentUserID);
            if (isF2orF3)
            {
                //  whereStr = "";
            }
            List<ESP.Finance.Entity.BranchInfo> list = (List<ESP.Finance.Entity.BranchInfo>)ESP.Finance.BusinessLogic.BranchManager.GetList("");

            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, new ListItem("请选择..", "0"));

            int count = 1;
            foreach (ESP.Finance.Entity.BranchInfo branch in list)
            {
                ddlBranch.Items.Insert(count, new ListItem(branch.BranchCode, branch.BranchCode));
                count++;
            }
        }
        /// <summary>
        /// 更具代理人获取其代理的所有人
        /// </summary>
        /// <returns></returns>
        private string GetDelegateUser()
        {
            string users = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegateList = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegateList)
            {
                users += model.UserID.ToString() + ",";
            }
            users += CurrentUser.SysID;
            return users;
        }

        private void bindList()
        {
            this.AuditTab.BindData();

            string whereStr = "";
            //and wi.workitemname != '财务第一级审批' and wi.workitemname != '财务第二级审批' and wi.workitemname != '财务第三级审批'
            whereStr += string.Format(" and wa.assigneeid in ({0}) and wi.status = {1} and r.returnType<>40 ", GetDelegateUser(), (int)ESP.Workflow.WorkItemStatus.Open);

            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                whereStr += string.Format(" and ( r.ProjectCode like '%{0}%' or r.ReturnCode like '%{0}%' or r.RequestEmployeeName like '%{0}%' or r.PreFee like '%{0}%' or  r.PaymentTypeCode like '%{0}%' )", txtKey.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()))
            {
                whereStr += string.Format(" and datediff(dd, cast('{0}' as datetime), r.CommitDate ) >= 0 ", txtBeginDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                whereStr += string.Format(" and datediff(dd, cast('{0}' as datetime), r.CommitDate ) <= 0 ", txtBeginDate.Text.Trim());
            }

            if (!string.IsNullOrEmpty(ddlBranch.SelectedValue) && !"0".Equals(ddlBranch.SelectedValue))
            {
                whereStr += string.Format(" and r.BranchCode = '{0}' ", ddlBranch.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlType.SelectedValue) && !"-1".Equals(ddlType.SelectedValue))
            {
                whereStr += string.Format(" and r.returntype = '{0}' ", ddlType.SelectedValue);
            }
            whereStr += " and r.returnid not in(SELECT  a.ReturnID FROM F_PNBatchRelation as a inner join F_PNBatch as b on a.batchid=b.batchid where b.batchtype=2) ";

            DataSet ds = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetMajorAuditList(whereStr, "");


            if (ds.Tables[0].Rows.Count == 0)
            {
                GridNoNeed.DataSource = ds.Tables[0];
                GridNoNeed.DataBind();
            }

            string returnIds = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["WebPage"] != null && (dr["WebPage"].ToString().IndexOf("step=fa") > 0 || dr["WebPage"].ToString().IndexOf("step=f1") > 0 || dr["WebPage"].ToString().IndexOf("step=f2") > 0 || dr["WebPage"].ToString().IndexOf("step=f3") > 0))
                {
                    isFinances = true;
                }
                if (dr["WebPage"].ToString().IndexOf("step=fa") > 0)
                {
                    isFa = true;
                }
                if (dr["WebPage"].ToString().IndexOf("step=f1") > 0)
                {
                    isF1 = true;
                }
                if (dr["WebPage"].ToString().IndexOf("step=f2") > 0 || dr["WebPage"].ToString().IndexOf("step=f3") > 0)
                {
                    isF2orF3 = true;
                }
                returnIds += dr["ReturnID"].ToString() + ",";
            }
            if (isFinances)
            {
                GridNoNeed.GroupBy = "WorkMonth ASC";
            }
            else
            {
                GridNoNeed.GroupBy = "";
            }


            if (isF1 || isFa)
            {
                btnExport.Visible = true;
            }
            else
            {
                btnExport.Visible = false;
            }






















            if (!string.IsNullOrEmpty(returnIds))
            {
                IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> detailList = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and returnID in(" + returnIds.TrimEnd(',') + ")");
                DataSet ds2 = ESP.MediaLinq.Utilities.ListToDataSet.ToDataSet(detailList);
                DataTable dt = new DataTable();
                dt = ds2.Tables[0].Copy();
                dt.TableName = "detail";
                ds.Tables.Add(dt);
                if (detailList != null && detailList.Count != 0)
                    ds.Relations.Add(ds.Tables[0].Columns["ReturnID"], ds.Tables["detail"].Columns["ReturnID"]);
                GridNoNeed.DataSource = ds.Tables[0];
                GridNoNeed.DataBind();
            }


















        }
        protected void tab1_click(object sender, EventArgs e)
        {
            Response.Redirect("ExpenseAccountAuditedList.aspx");
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();
        }

        protected string getReturnCode(int returnid)
        {
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnid);
            return returnModel.ReturnCode;
        }

        protected string getUrl(string webpage, string workitemid, string workMonth)
        {
            var sum = "<a href='" + webpage + "&workitemid=" + workitemid + "&BackUrl=ExpenseAccountAuditList.aspx'><img src='images/Audit.gif' /></a>";
            if (workMonth != "1.本次处理" && isFinances)
            {
                return "";








            }
            return sum;





        }

        public void OnNeedRebind(object sender, EventArgs oArgs)
        {
            GridNoNeed.DataBind();
        }

        protected string getPrintUrl(string returnID, string returnType)
        {
            var sum = "";
            if (Convert.ToInt32(returnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountThirdParty && Convert.ToInt32(returnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCheckAndTelegraphic && Convert.ToInt32(returnType) != (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountMedia)
            {
                sum = "<a href='Print/ExpensePrint.aspx?expenseID=" + returnID + "' target='_blank'><img src='/images/Icon_Output.gif' /></a>";
            }
            else
            {
                sum = "<a href='Print/ThirdPartyPrint.aspx?expenseID=" + returnID + "' target='_blank'><img src='/images/Icon_Output.gif' /></a>";
            }
            return sum;
        }

        protected string getIsBatchInput(string returnID, decimal preFee, string workitemid, decimal confirmFee, string returntype, string workMonth)
        {
            bool isBatchAudit = true;

            if (Convert.ToInt32(returntype) == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccount)
            {
                isBatchAudit = true;
            }
            else
            {

                if (confirmFee > 0 && preFee != confirmFee)
                {
                    isBatchAudit = false;
                }
                else
                {
                    isBatchAudit = true;
                }
            }

            if (isBatchAudit && ("1.本次处理".Equals(workMonth) || !isFinances))
            {
                return "<input type=\"checkbox\" id=\"chkAudit\" name=\"chkAudit\" value='" + workitemid + "' OnClick='CalSelected(this.checked," + preFee + "," + returnID + ");'/>";
            }
            return "";
        }

        protected void btnAuditAll_Click(object sender, EventArgs e)
        {
            string workitemids = hidWorkItemID.Value;
            List<ESP.Finance.Entity.ExpenseAccountBatchAudit> auditList = new List<ESP.Finance.Entity.ExpenseAccountBatchAudit>();
            ESP.Finance.Entity.ExpenseAccountBatchAudit auditInfo = null;
            bool haveFinanceAudit = false;
            string stepType = "";
            int stepPonter = 1;
            if (!string.IsNullOrEmpty(workitemids) && workitemids.Split(',').Length > 0)
            {
                int workitemid = 0;
                for (int i = 0; i < workitemids.Split(',').Length; i++)
                {
                    workitemid = Convert.ToInt32(workitemids.Split(',')[i]);
                    if (workitemid > 0)
                    {
                        auditInfo = new ESP.Finance.Entity.ExpenseAccountBatchAudit();

                        workitem = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkFlowModel(workitemid);
                        model = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetWorkItemModel(workitem.EntityId);

                        auditInfo.Model = model;
                        auditInfo.Workitem = workitem;

                        if (stepPonter == 1)
                        {
                            stepType = workitem.WorkItemName;
                        }
                        stepPonter++;

                        try
                        {
                            //设置审核日志记录
                            ESP.Finance.Entity.ExpenseAuditDetailInfo logModel = new ESP.Finance.Entity.ExpenseAuditDetailInfo();
                            logModel.AuditeDate = DateTime.Now;
                            logModel.AuditorEmployeeName = CurrentUser.Name;
                            logModel.AuditorUserID = Convert.ToInt32(CurrentUser.SysID);
                            logModel.AuditorUserCode = CurrentUser.ID;
                            logModel.AuditorUserName = CurrentUser.ITCode;
                            logModel.AuditType = GetStep();
                            logModel.ExpenseAuditID = model.ReturnID;
                            logModel.AuditeStatus = (int)ESP.Finance.Utility.ExpenseAccountAuditStatus.AuditStatus_Passed;
                            logModel.Suggestion = "批量审批";

                            //设置工作流委托方法的参数
                            Dictionary<string, object> prarms = new Dictionary<string, object>() 
                            { 
                                { "EntID", model.ReturnID } ,
                                { "ReturnStatus",  GetStatus(model.ReturnType.Value)} ,
                                { "LogModel", logModel }
                            };

                            int assigneeID = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeIdByWorkItemID(workitem.WorkItemId, dataContext);

                            if (assigneeID != Convert.ToInt32(CurrentUser.SysID))
                            {
                                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(assigneeID);
                                logModel.Suggestion += "(" + CurrentUser.Name + " 代理 " + emp.Name + ")";
                            }

                            auditInfo.Prarms = prarms;
                            auditInfo.CurrentUserId = assigneeID;


                            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage) && (workitem.WebPage.IndexOf("step=f1") > 0 || workitem.WebPage.IndexOf("step=f2") > 0 || workitem.WebPage.IndexOf("step=f3") > 0))
                            {
                                haveFinanceAudit = true;
                            }

                            if (!stepType.Equals(workitem.WorkItemName))
                            {
                                Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审批项角色不同，无法批量审核通过，请检查！');window.location='MajorExpenseAccountList.aspx';", true);
                                return;
                            }

                            decimal ooptotal = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTotalOOPByReturnID(model.ReturnID);
                            decimal costtotal = model.PreFee.Value - ooptotal;
                            //if (ESP.Finance.BusinessLogic.ExpenseAccountManager.ValidateMoney(model, costtotal, ooptotal))
                            //{
                            auditList.Add(auditInfo);
                            //}
                            //else
                            //{
                            //    Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + model.ReturnCode + "项目成本金额不足，无法审核通过，请检查！');window.location='MajorExpenseAccountList.aspx';", true);
                            //    return;
                            //}


                        }
                        catch (Exception ex)
                        {
                            Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('审核通过操作失败，请重试！');window.location='MajorExpenseAccountList.aspx';", true);
                            return;
                        }
                    }

                }
                if (!haveFinanceAudit)
                {
                    if (ESP.Finance.BusinessLogic.ExpenseAccountManager.BatchAuditExpense(auditList, new List<int>(), null))
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批量审核通过操作成功！');window.location='MajorExpenseAccountList.aspx';", true);
                        return;
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(typeof(string), "", "alert('批量审核操作失败，请重试！');window.location='MajorExpenseAccountList.aspx';", true);
                        return;
                    }
                }
                else
                {
                    Session["AuditListByBatch"] = auditList;
                    Page.ClientScript.RegisterStartupScript(typeof(string), "", "window.location='SetNextAuditer.aspx';", true);
                    return;
                }

            }

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string workitemids = hidWorkItemID.Value;
            string whereStr = "";

            whereStr += string.Format(" and wi.WorkItemName != '收货' and wi.status = {0} ", (int)ESP.Workflow.WorkItemStatus.Open);
            whereStr += string.Format(" and wi.WorkItemId in({0}) ", workitemids.TrimEnd(','));

            DataTable dt = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetExportExpenseDetail(whereStr).Tables[0];

            ESP.Finance.BusinessLogic.ExpenseAccountManager.ExportExpenseAccountDetail(dt, Response);
        }

        #region
        protected int GetStep()
        {
            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage))
            {
                if (workitem.WebPage.IndexOf("step=pa") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_YS;
                }
                else if (workitem.WebPage.IndexOf("step=pm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_XMFZ;
                }
                else if (workitem.WebPage.IndexOf("step=mj") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ZJSP;
                }
                else if (workitem.WebPage.IndexOf("step=gm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_ZJLSP;
                }
                else if (workitem.WebPage.IndexOf("step=ceo") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_CEO;
                }
                else if (workitem.WebPage.IndexOf("step=fa") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_FA;
                }
                else if (workitem.WebPage.IndexOf("step=f1") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial;
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial2;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_Financial3;
                }
                else if (workitem.WebPage.IndexOf("step=fm") > 0)
                {
                    return (int)ESP.Finance.Utility.auditorType.operationAudit_Type_FinancialMajor;
                }
                else if (workitem.WebPage.IndexOf("step=ra") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FJ_Recived;
                }
            }
            return 0;
        }

        protected int GetStatus(int returnType)
        {
            if (workitem != null && !string.IsNullOrEmpty(workitem.WebPage))
            {
                if (workitem.WebPage.IndexOf("step=pa") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.PrepareAudit;
                }
                else if (workitem.WebPage.IndexOf("step=pm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.ProjectManagerAudit;
                }
                else if (workitem.WebPage.IndexOf("step=mj") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.MajorAudit;
                }
                else if (workitem.WebPage.IndexOf("step=gm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.GeneralManagerAudit;
                }
                else if (workitem.WebPage.IndexOf("step=ceo") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.CEOAudit;
                }
                else if (workitem.WebPage.IndexOf("step=fa") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FAAudit;
                }
                else if (workitem.WebPage.IndexOf("step=f1") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel1;
                }
                else if (workitem.WebPage.IndexOf("step=f2") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.FinanceLevel2;
                }
                else if (workitem.WebPage.IndexOf("step=f3") > 0)
                {
                    if (returnType == (int)ESP.Purchase.Common.PRTYpe.PN_ExpenseAccountCashBorrow)
                        return (int)ESP.Finance.Utility.PaymentStatus.WaitReceiving;
                    else
                        return (int)ESP.Finance.Utility.PaymentStatus.FinanceComplete;
                }
                else if (workitem.WebPage.IndexOf("step=fm") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.EddyAudit;
                }
                else if (workitem.WebPage.IndexOf("step=rv") > 0)
                {
                    return (int)ESP.Finance.Utility.PaymentStatus.Recived;
                }
            }
            return 0;
        }
        #endregion

        protected void GridNoNeed_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            DataRow dr = (DataRow)e.Item.DataItem;
            string printPage = string.Empty;
            if (dr.Table.Columns.Contains("DepartmentId") && dr["DepartmentId"] != null)
            {
                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                if (dr["DepartmentId"] != "" && dr["DepartmentId"] != "0")
                {
                    depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(int.Parse(dr["DepartmentId"].ToString()), depList);
                    string groupname = string.Empty;
                    foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                    {
                        groupname += dept.DepartmentName + "-";
                    }
                    if (!string.IsNullOrEmpty(groupname))
                        e.Item["DepartmentName"] = groupname.Substring(0, groupname.Length - 1);
                }
            }
        }
    }
}

