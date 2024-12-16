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
    public partial class TransferMgt : ESP.Web.UI.PageBase
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
            string strwhere = " and transInDate<='"+DateTime.Now.ToString("yyyy-MM-dd")+"' and status="+((int)ESP.HumanResource.Common.Status.TransferStatus.ManagerConfirmIn).ToString();

            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                strwhere += " and (Creater like '%" + txtCode.Text + "%' or Remark like '%" + txtCode.Text + "%' or OldPositionName like '%" + txtCode.Text + "%' or NewPositionName like '%" + txtCode.Text + "%' or OldGroupName like '%" + txtCode.Text + "%'  or NewGroupName like '%" + txtCode.Text + "%')";
            }

            var transList = ESP.HumanResource.BusinessLogic.TransferManager.GetList(strwhere, null);

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

                string ids = model.Id.ToString();
                while (ids.Length < 5)
                {
                    ids = "0" + ids;
                }
                lblId.Text = "TF" + ids;
                lblStatus.Text = ESP.HumanResource.Common.Status.TransferStatus_Names[model.Status];

            }
        }

        protected void gvTransfer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            TransferInfo model = TransferManager.GetModel(id);
        }
    }
}