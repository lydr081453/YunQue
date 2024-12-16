using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.UserControls.Purchase
{
    public partial class TrafficDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int returnId = 0;
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
                returnId = int.Parse(Request[ESP.Finance.Utility.RequestName.ReturnID]);
            if (!IsPostBack)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
                if (returnModel != null)
                {
                    if (returnModel.ProjectID != null && returnModel.ProjectID.Value != 0)
                    {
                        TopMessage.ProjectModel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(returnModel.ProjectID.Value);
                        decimal allTraffic = GetAllTraffic(returnModel.ProjectID.Value);
                        labAllTraffic.Text = allTraffic.ToString("#,##0.##");
                    }
                    txtProjectCode.Text = returnModel.ProjectCode;
                    txtBeginDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
                    labPaymentType.Text = returnModel.PaymentTypeName;
                    txtPreFee.Text = returnModel.PreFee == null ? "" : returnModel.PreFee.Value.ToString("#,##0.##");
                    txtDesc.Text = returnModel.ReturnContent;

                    labDepartment.Text = returnModel.DepartmentName;
                }
            }
        }

        private decimal GetAllTraffic(int projectId)
        {
            decimal allTraffic = 0;
            foreach (ESP.Finance.Entity.ProjectExpenseInfo expenseInfo in ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectId).Expenses)
            {
                if (expenseInfo.Description.ToLower() == "media")
                    allTraffic += expenseInfo.Expense == null ? 0 : expenseInfo.Expense.Value;
            }
            return allTraffic;
        }
    }
}