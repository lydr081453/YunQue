using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrativeWeb.RequestForSeal
{
    public partial class RequestForSealList : ESP.Web.UI.PageBase
    {
        RequestForSealManager manager = new RequestForSealManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        private void ListBind()
        {
            string strWhere = " requestorId=" + CurrentUser.SysID;
            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
                strWhere += " and (FileName like '%" + txtKey.Text.Trim() + "%' or requestorName like '%" + txtKey.Text.Trim() + "%'or datanum like '%" + txtKey.Text.Trim() + "%' or saNo like '%" + txtKey.Text.Trim() + "%')";
            var list = manager.GetList(strWhere);
            gvList.DataSource = list;
            gvList.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestForSealEdit.aspx");
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var model = (RequestForSealInfo)e.Row.DataItem;

                LinkButton lnkCancel = (LinkButton)e.Row.FindControl("lnkCancel");
                LinkButton lnkDel = (LinkButton)e.Row.FindControl("lnkDel");
                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");

                if (model.Status != ESP.Administrative.Common.Status.RequestForSealStatus.Save && model.Status != ESP.Administrative.Common.Status.RequestForSealStatus.Rejected)
                    lnkDel.Visible = false;
                if (model.Status == ESP.Administrative.Common.Status.RequestForSealStatus.Save || model.Status == ESP.Administrative.Common.Status.RequestForSealStatus.Rejected)
                {
                    hylEdit.Visible = true;
                }
                if(model.Status == ESP.Administrative.Common.Status.RequestForSealStatus.Auditing)
                    lnkCancel.Visible = true;
                else
                    e.Row.Cells[11].Text = "";
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var model = manager.GetModel(int.Parse(e.CommandArgument.ToString()));
            if (e.CommandName == "Cancell")
            {
                model.Status = ESP.Administrative.Common.Status.RequestForSealStatus.Save;
                manager.Update(model);
            }
            else if (e.CommandName == "Del")
            {
                manager.Delete(model.Id);
            }
            ListBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }
    }
}