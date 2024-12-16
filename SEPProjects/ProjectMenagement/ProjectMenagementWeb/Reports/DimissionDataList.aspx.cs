using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data;

namespace FinanceWeb.Reports
{
    public partial class DimissionDataList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
            }
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType==DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPnStatus = (Label)e.Row.FindControl("lblPnStatus");
                Label lblPrStatus = (Label)e.Row.FindControl("lblPrStatus");
                if (lblPnStatus != null)
                {
                    if ((!string.IsNullOrEmpty(e.Row.Cells[0].Text.ToString())) && e.Row.Cells[0].Text!="&nbsp;")
                    {
                        lblPnStatus.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(e.Row.Cells[0].Text.ToString()),0, null);
                    }
                    if ((!string.IsNullOrEmpty(e.Row.Cells[1].Text.ToString())) && e.Row.Cells[1].Text != "&nbsp;")
                    {
                        lblPrStatus.Text = ESP.Purchase.Common.State.requistionOrorder_state[int.Parse(e.Row.Cells[1].Text.ToString())].ToString();
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPR();
            BindProject();
        }

        protected void btnCloseUser_Click(object sender, EventArgs e)
        {
            try
            {
                ESP.Framework.Entity.UserInfo emp = ESP.Framework.BusinessLogic.UserManager.Get(this.txtKey.Text);
                if (emp != null)
                {
                    ESP.HumanResource.Entity.UsersInfo user = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(emp.UserID);
                    if (user != null)
                    {
                        user.IsDeleted = true;
                        ESP.HumanResource.BusinessLogic.UsersManager.Update(user);
                    }
                }
            }
            catch(Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('" + ex.Message + "');", true);
                return;
            }

            Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('修改成功!');", true);

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //System.Data.DataTable dtPr = ESP.Finance.BusinessLogic.DimissionManager.GetPRList(this.txtKey.Text);
            //System.Data.DataTable dtProject = ESP.Finance.BusinessLogic.DimissionManager.GetProjectList(this.txtKey.Text);
            //try
            //{
            //    ESP.Finance.BusinessLogic.DimissionManager.ExportPrList(dtPr, dtProject, Response);
            //    GC.Collect();
            //}
            //catch(Exception ex)
            //{
            //    Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('" + ex.Message + "');", true);
            //}
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                if ((!string.IsNullOrEmpty(e.Row.Cells[0].Text.ToString())) && e.Row.Cells[0].Text != "&nbsp;")
                {
                    lblStatus.Text = State.SetState(int.Parse(e.Row.Cells[0].Text));
                }
            }
        }

        protected void gvProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        private void BindPR()
        {
            //this.gvG.DataSource = ESP.Finance.BusinessLogic.DimissionManager.GetPRList(this.txtKey.Text);
            //this.gvG.DataBind();
        }

        private void BindProject()
        {
            //this.gvProject.DataSource = ESP.Finance.BusinessLogic.DimissionManager.GetProjectList(this.txtKey.Text);
            //this.gvProject.DataBind();
        }
    }
}
