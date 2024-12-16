using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Transfer
{
    public partial class TransferView : System.Web.UI.Page
    {
        private int transferId = 0;
        private TransferInfo transferModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                      
                    if (int.TryParse(Request["id"], out transferId))
                    {
                        transferModel = TransferManager.GetModel(transferId);

                        BindData(transferId);
                        GetTranferData(transferId);
                    }
                }
            }
        }

        private void BindData(int id)
        {
           // TransferInfo model = TransferManager.GetModel(id);
            if (transferModel != null)
            {
                ESP.Finance.Entity.DepartmentViewInfo oldDeptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(transferModel.OldGroupId);
                ESP.Finance.Entity.DepartmentViewInfo newDeptView = ESP.Finance.BusinessLogic.DepartmentViewManager.GetModel(transferModel.NewGroupId);

                hidGroupId.Value = transferModel.OldGroupId.ToString();
                lblTransOutGroup.Text = transferModel.OldGroupName;
                lblTransOutCompany.Text = oldDeptView.level1;
                lblTransOutDept.Text = oldDeptView.level2;
                lblTransInPosition.Text = transferModel.NewPositionName;
                lblTransInGroup.Text = transferModel.NewGroupName;
                lblTransInCompany.Text = newDeptView.level1;
                lblTransInDept.Text = newDeptView.level2;
                lblTransUser.Text = transferModel.TransName;
                lblSalaryBase.Text = transferModel.SalaryBase.ToString("#,##0.00");
                lblSalaryPromotion.Text = transferModel.SalaryPromotion.ToString("#,##0.00");
                lblTransInDate.Text = transferModel.TransOutDate.ToString("yyyy-MM-dd");
                lblTransOutDate.Text = transferModel.TransInDate.ToString("yyyy-MM-dd");
                lblRemark.Text = transferModel.Remark;

                // 审批日志信息
                string strAuditLog = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetTransferLogInfos(transferModel.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm);
                lblLog.Text = strAuditLog;
            }

        }

        private void GetTranferData(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@transferid", SqlDbType.Int,4)
				};
            parameters[0].Value = id;

            var list = TransferDetailsManager.GetList(" transferid=@transferid", parameters.ToList());

            gvDetailList.DataSource = list;
            gvDetailList.DataBind();
        }


        protected void btnReturn_Click(object sender, EventArgs e)
        {

        }

        protected void gvDetailList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                Label lab = e.Row.FindControl("labReceiverName") as Label;
                CheckBox chkTransfer = e.Row.FindControl("chkTransfer") as CheckBox;
                ESP.HumanResource.Entity.TransferDetailsInfo detailsInfo = e.Row.DataItem as ESP.HumanResource.Entity.TransferDetailsInfo;
                lab.Text = detailsInfo.ReceiverName;
                if (detailsInfo.TransGroup == 1)
                {
                    chkTransfer.Checked = true;
                    lab.Text = "转组";
                }

                if (transferModel.Status != (int)ESP.HumanResource.Common.Status.TransferStatus.HRConfirmed)
                {
                    chkTransfer.Enabled = false;
                }

            }
        }
    }
}