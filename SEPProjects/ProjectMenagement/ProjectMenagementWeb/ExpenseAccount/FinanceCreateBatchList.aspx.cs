/************************************************************************\
 * 个人报销单列表页
 *      
 * 只显示自己提交的报销单列表
 * 
 *
\************************************************************************/
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
    public partial class FinanceCreateBatchList : ESP.Web.UI.PageBase
    {
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();
        int ReturnType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                bindList();
            }
        }


        protected void lnkNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinanceCreateBatch.aspx");
        }

        private void bindList()
        {
            string whereStr = "";

            whereStr += string.Format(" and CreatorID = {0} and BatchType = 2 and status ={1}", UserInfo.UserID,(int)ESP.Finance.Utility.PaymentStatus.FAAudit);

            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                whereStr += string.Format(" and  BatchCode like '%{0}%' ", txtKey.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()))
            {
                whereStr += string.Format(" and datediff(dd, cast('{0}' as datetime), CreateDate ) >= 0 ", txtBeginDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                whereStr += string.Format(" and datediff(dd, cast('{0}' as datetime), CreateDate ) <= 0 ", txtBeginDate.Text.Trim());
            }


            DataTable dt = ESP.Finance.BusinessLogic.PNBatchManager.GetBatchByExpenseAccount(whereStr);

            this.gvG.DataSource = dt;
            this.gvG.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            bindList();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;

                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

                hylEdit.NavigateUrl = "FinanceCreateBatch.aspx?BatchID=" + dr["BatchID"];
                hylEdit.ImageUrl = "/images/edit.gif";


                Label labRequestUserName = (Label)e.Row.FindControl("labRequestUserName");
                labRequestUserName.Text = new ESP.Compatible.Employee(Convert.ToInt32(dr["CreatorID"])).Name;
                labRequestUserName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Convert.ToInt32(dr["CreatorID"])) + "');");

                List<ESP.Finance.Entity.PNBatchRelationInfo> batchRelationList = (List<ESP.Finance.Entity.PNBatchRelationInfo>)ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchID=" + dr["BatchID"], null);
                if (batchRelationList.Count > 0)
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                }

                if (dr["Status"].ToString().Trim() != ((int)PaymentStatus.FAAudit).ToString())
                {
                    hylEdit.NavigateUrl = "";
                    hylEdit.Visible = false;
                }
                lblStatus.Text = ESP.Finance.Utility.ExpenseStatusName.GetExpenseStatusName(Convert.ToInt32(dr["Status"].ToString().Trim()),0);

            }
        }
         
        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                //撤销付款申请
                int ID = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.BusinessLogic.PNBatchManager.Delete(ID);
                
                bindList();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
                
            }
        }

    }
}
