using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ESP.Finance.Utility;

namespace FinanceWeb.RebateRegistration
{
    public partial class RebateRegistrationPrint : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.PNBatchInfo model = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            initPrintPage();
        }

        private void initPrintPage()
        {
            IList<ESP.Finance.Entity.RebateRegistrationInfo> list = null;
            if (!string.IsNullOrEmpty(Request["batchId"]))
            {
                int batchId = int.Parse(Request["batchId"].ToString());
                model = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchId);

                list = ESP.Finance.BusinessLogic.RebateRegistrationManager.GetList(" a.BatchId = " + batchId.ToString());

                var auditLogs = ESP.Finance.BusinessLogic.ConsumptionAuditManager.GetList(" BatchId = " + model.BatchID + " and AuditStatus!=0");
                foreach (var log in auditLogs)
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID);
                    if (emp != null)
                        labSuggestion.Text += log.AuditorEmployeeName + "(" + emp.FullNameEN + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ESP.Finance.Utility.ExpenseAccountAuditStatus.ConsumptionAuditStatus_Names[log.AuditStatus.Value] + ";&nbsp;&nbsp;&nbsp;" + log.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                }

                litPNno.Text = model.PurchaseBatchCode;
                litDate.Text = model.CreateDate == null ? "" : model.CreateDate.Value.ToString("yyyy-MM-dd");
               
                ESP.HumanResource.Entity.EmployeeBaseInfo creator = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(model.CreatorID.Value);
                ESP.HumanResource.Entity.EmployeesInPositionsInfo position = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(model.CreatorID.Value);

                List<ESP.Framework.Entity.DepartmentInfo> depList = new List<ESP.Framework.Entity.DepartmentInfo>();
                if (position!=null && position.DepartmentID!= 0)
                {
                    depList = ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(position.DepartmentID, depList);
                    string groupname = string.Empty;
                    foreach (ESP.Framework.Entity.DepartmentInfo dept in depList)
                    {
                        groupname += dept.DepartmentName + "-";
                    }
                    if (!string.IsNullOrEmpty(groupname))
                        litGroup.Text = groupname.Substring(0, groupname.Length - 1);
                }
                litRec.Text = model.Creator + "<br/>" + creator.Code ;
                litTotal.Text =litAmount.Text = model.Amounts==null? "0" : model.Amounts.Value.ToString("#,##0.00");
                litDesc.Text = model.Description;
            }
            else
            {
                list = new List<ESP.Finance.Entity.RebateRegistrationInfo>();
            }

            repExpense.DataSource = list;
            repExpense.DataBind();

            logoImg.ImageUrl = "/images/xingyan.png";

        }

        protected void repExpense_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ESP.Finance.Entity.RebateRegistrationInfo detailModel = (ESP.Finance.Entity.RebateRegistrationInfo)e.Item.DataItem;

            Literal lblProjectCode = (Literal)e.Item.FindControl("lblProjectCode");
            if (lblProjectCode != null)
            {
                lblProjectCode.Text = detailModel.Project == null ? "" : detailModel.Project.ProjectCode;
            }

            Literal lblYM = (Literal)e.Item.FindControl("lblYM");
            if (lblYM != null)
            {
                lblYM.Text = detailModel.CreditedDate;
            }

            Literal lblDESC = (Literal)e.Item.FindControl("lblDESC");
            if (lblDESC != null)
            {
                lblDESC.Text = detailModel.Remark;
            }

            Literal lblAmount = (Literal)e.Item.FindControl("lblAmount");
            if (lblAmount != null)
            {
                lblAmount.Text = detailModel.RebateAmount.ToString("#,##0.00");
            }
            Literal lblMedia = (Literal)e.Item.FindControl("lblMedia");
            if (lblMedia != null)
            {
                lblMedia.Text = detailModel.Supplier.supplier_name;
            }
            Literal labNum = (Literal)e.Item.FindControl("labNum");
            if (labNum != null)
            {
                labNum.Text = detailModel.AccountingNum;
            }
            Literal labType = (Literal)e.Item.FindControl("labType");
            if (labType != null)
            {
                labType.Text = detailModel.SettleType;
            }
            Literal labBranch = (Literal)e.Item.FindControl("labBranch");
            if (labBranch != null)
            {
                labBranch.Text = detailModel.Branch;
            }
        }

    }
}