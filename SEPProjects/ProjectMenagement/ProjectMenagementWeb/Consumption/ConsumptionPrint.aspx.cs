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

namespace FinanceWeb.Consumption
{
    public partial class ConsumptionPrint : ESP.Web.UI.PageBase
    {
        ESP.Finance.Entity.PNBatchInfo model = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            initPrintPage();
        }

        private void initPrintPage()
        {
            IList<ESP.Finance.Entity.ConsumptionInfo> list = null;
            if (!string.IsNullOrEmpty(Request["batchId"]))
            {
                int batchId = int.Parse(Request["batchId"].ToString());
                model = ESP.Finance.BusinessLogic.PNBatchManager.GetModel(batchId);

                list = ESP.Finance.BusinessLogic.ConsumptionManager.GetList(" BatchId = " + batchId.ToString());

                var auditLogs = ESP.Finance.BusinessLogic.ConsumptionAuditManager.GetList(" BatchId = " + model.BatchID + " and AuditStatus!=0");
                foreach (var log in auditLogs)
                {
                    ESP.Framework.Entity.EmployeeInfo emp = ESP.Framework.BusinessLogic.EmployeeManager.Get(log.AuditorUserID);
                    if (emp != null)
                        labSuggestion.Text += log.AuditorEmployeeName + "(" + emp.FullNameEN + ")" + ";&nbsp;&nbsp;&nbsp;" + log.Suggestion + ";&nbsp;&nbsp;&nbsp;" + ESP.Finance.Utility.ExpenseAccountAuditStatus.ConsumptionAuditStatus_Names[log.AuditStatus.Value] + ";&nbsp;&nbsp;&nbsp;" + log.AuditDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
                }

               

                if (model.PeriodID != null && model.PeriodID == 1)
                {
                    litPNno.Text = model.PurchaseBatchCode + "(调整)";
                }
                else
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
                if(list!=null && list.Count>0)
                {
                    litMedia.Text = list[0].Media;
                }
            }
            else
            {
                list = new List<ESP.Finance.Entity.ConsumptionInfo>();
            }

            repExpense.DataSource = list;
            repExpense.DataBind();

            logoImg.ImageUrl = "/images/xingyan.png";

        }

        protected void repExpense_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ESP.Finance.Entity.ConsumptionInfo detailModel = (ESP.Finance.Entity.ConsumptionInfo)e.Item.DataItem;

            Literal lblProjectCode = (Literal)e.Item.FindControl("lblProjectCode");
            if (lblProjectCode != null)
            {
                lblProjectCode.Text = detailModel.ProjectCode;
            }

            Literal lblYM = (Literal)e.Item.FindControl("lblYM");
            if (lblYM != null)
            {
                lblYM.Text = detailModel.OrderYM;
            }

            Literal lblJSCode = (Literal)e.Item.FindControl("lblJSCode");
            if (lblJSCode != null)
            {
                lblJSCode.Text = detailModel.JSCode;
            }

            Literal lblXMCode = (Literal)e.Item.FindControl("lblXMCode");
            if (lblXMCode != null)
            {
                lblXMCode.Text = detailModel.XMCode;
            }

            Literal lblDESC = (Literal)e.Item.FindControl("lblDESC");
            if (lblDESC != null)
            {
                lblDESC.Text = detailModel.Description;
            }

            Literal lblAmount = (Literal)e.Item.FindControl("lblAmount");
            if (lblAmount != null)
            {
                lblAmount.Text = detailModel.Amount.ToString("#,##0.00");
            }

            Literal lblOrderType = (Literal)e.Item.FindControl("lblOrderType");
            if (lblOrderType != null)
            {
                lblOrderType.Text = detailModel.OrderType;
            }
        }

    }
}