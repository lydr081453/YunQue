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
    public partial class TransferList : ESP.Web.UI.PageBase
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
            var deptlist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" hrid=" + UserID.ToString() + " or directorid=" + UserID.ToString() + " or managerid=" + UserID.ToString());
            string depts = string.Empty;
            foreach (var dep in deptlist)
            {
                depts += dep.DepId.ToString() + ",";
            }

            depts = depts.TrimEnd(',');
            string strwhere = "";
            if (!string.IsNullOrEmpty(depts))
            {
                strwhere += " and (NewGroupId in(" + depts + ") or oldGroupId in(" + depts + "))";
            }
            else if (CurrentUserID == int.Parse(System.Configuration.ConfigurationManager.AppSettings["AdvanceID"]) || System.Configuration.ConfigurationManager.AppSettings["AdvanceID"].IndexOf(CurrentUserID.ToString()) >= 0)
            {
                strwhere = "";
            }
            else
            {
                strwhere += " and createrid=" + UserID;
            }

            int hrAdmin = int.Parse(System.Configuration.ConfigurationManager.AppSettings["HRAdminID"]);
            string hrIds = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetHRId();

            if (CurrentUserID == hrAdmin || hrIds.IndexOf(CurrentUserID.ToString()) >= 0)
            {
                strwhere = "";
            }

            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                strwhere += " and (Creater like '%" + txtCode.Text + "%' or Remark like '%" + txtCode.Text + "%' or OldPositionName like '%" + txtCode.Text + "%' or NewPositionName like '%" + txtCode.Text + "%' or OldGroupName like '%" + txtCode.Text + "%'  or NewGroupName like '%" + txtCode.Text + "%')";
            }
            if (ddlStatus.SelectedValue != "-1")
            {
                strwhere += " and Status in(" + ddlStatus.SelectedValue + ")";
            }

            var transList = ESP.HumanResource.BusinessLogic.TransferManager.GetList(strwhere, null);

            this.gvTransfer.DataSource = transList;
            this.gvTransfer.DataBind();
        }


        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransferEdit.aspx");
        }

        protected void gvTransfer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TransferInfo model = (TransferInfo)e.Row.DataItem;
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Label lblId = (Label)e.Row.FindControl("lblId");

                string ids = model.Id.ToString();
                while (ids.Length < 5)
                {
                    ids = "0" + ids;
                }
                lblId.Text = "TF" + ids;
                lblStatus.Text = ESP.HumanResource.Common.Status.TransferStatus_Names[model.Status];

                if (model.Status != (int)ESP.HumanResource.Common.Status.TransferStatus.Save && model.Status != (int)ESP.HumanResource.Common.Status.TransferStatus.Reject)
                {
                    e.Row.Cells[11].Text = "";
                    e.Row.Cells[12].Text = "";
                }

            }
        }

        protected void gvTransfer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "del")
            {
                  TransferInfo model = TransferManager.GetModel(id);
                HeadAccountInfo hc = (new HeadAccountManager()).GetModel(model.HeadCountId);

                TransferManager.Delete(id, hc);
                BindData();
            }
        }
    }
}