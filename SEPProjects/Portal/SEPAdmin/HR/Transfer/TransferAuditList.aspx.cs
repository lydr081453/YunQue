using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Transfer
{
    public partial class TransferAuditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            string strwhere = string.Empty;

            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                strwhere += " and (Creater like '%" + txtCode.Text + "%' or Remark like '%" + txtCode.Text + "%' or OldPositionName like '%" + txtCode.Text + "%' or NewPositionName like '%" + txtCode.Text + "%' or OldGroupName like '%" + txtCode.Text + "%'  or NewGroupName like '%" + txtCode.Text + "%')";
            }

            var transList = ESP.HumanResource.BusinessLogic.TransferManager.GetWaitAuditList( UserID, strwhere, null);

            this.gvTransfer.DataSource = transList;
            this.gvTransfer.DataBind();
        }


        protected void gvTransfer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TransferInfo model = (TransferInfo)e.Row.DataItem;
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Label lblId = (Label)e.Row.FindControl("lblId");
                HyperLink hlAudit = (HyperLink)e.Row.FindControl("hlAudit");

                string ids = model.Id.ToString();
                while (ids.Length < 5)
                {
                    ids = "0" + ids;
                }
                lblId.Text = "TF" + ids;
                lblStatus.Text = ESP.HumanResource.Common.Status.TransferStatus_Names[model.Status];

                if (model.Status == (int)ESP.HumanResource.Common.Status.TransferStatus.HRCommit)
                {
                    hlAudit.NavigateUrl = "TransferHrEdit.aspx?id=" + model.Id;
                }
                else
                {
                    hlAudit.NavigateUrl = "TransferAudit.aspx?id="+model.Id;
                }
            }
        }

        protected void gvTransfer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            TransferInfo model = TransferManager.GetModel(id);
        }
    }
}