using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Reports
{
    public partial class PCCredenceRpt : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Server.ScriptTimeout = 600;
            this.ddlBranch.Attributes.Add("onChange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
            if (!IsPostBack)
            {
                BindBranch();
                Search();
            }
        }
        private void BindBranch()
        {
            IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(" firstfinanceid =" + CurrentUser.SysID + " or otherfinancialusers like '%," + CurrentUser.SysID + ",%'", null);
            this.ddlBranch.DataSource = blist;
            this.ddlBranch.DataTextField = "BranchCode";
            this.ddlBranch.DataValueField = "BranchID";
            this.ddlBranch.DataBind();

            ListItem list = new ListItem();
            list.Value = "-1";
            list.Text = "请选择...";
            ddlBranch.Items.Insert(0, list);
        }

        private IList<ESP.Finance.Entity.ReturnInfo> getReturnList()
        {
            string sql = "(paymenttypeid=1 or returntype in(30,32,20)) and (paymenttypecode ='' or paymenttypecode is null) and returnstatus=140";
            if(!string.IsNullOrEmpty(hidBranchName.Value))
            {
              sql+=" and projectcode like '"+this.hidBranchName.Value+"%'";
            }
            if (!string.IsNullOrEmpty(this.txtBeginDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                sql += " and (returnfactdate between '"+this.txtBeginDate.Text+"' and '"+this.txtEndDate.Text+"')";
            }
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(sql);
            var sortList = from c in returnlist orderby c.ReturnFactDate ascending select c;
            return sortList.ToList();
        }
        private void Search()
        {
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = getReturnList();
            this.gvG.DataSource = returnlist;
            this.gvG.DataBind();
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }
        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;
                Label lblGroup = (Label)e.Row.FindControl("lblGroup");
                ESP.Compatible.Department dept = ESP.Compatible.DepartmentManager.GetDepartmentByPK(returnModel.DepartmentID.Value);
                lblGroup.Text = dept == null ? "" : dept.DepartmentName;

                Label lblDate = (Label)e.Row.FindControl("lblDate");
                lblDate.Text = returnModel.ReturnFactDate == null ? returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd") : returnModel.ReturnFactDate.Value.ToString("yyyy-MM-dd");
                Label lblFee = (Label)e.Row.FindControl("lblFee");
                lblFee.Text = returnModel.FactFee == null ? returnModel.PreFee.Value.ToString("#,##0.00") : returnModel.FactFee.Value.ToString("#,##0.00");
            }
        }
        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = "";
            this.txtBeginDate.Text = "";
            this.txtEndDate.Text = "";
            this.ddlBranch.SelectedIndex = 0;
            Search();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if(this.ddlBranch.SelectedIndex==0)
                return;
            ESP.Finance.Entity.BranchInfo branchModel =ESP.Finance.BusinessLogic.BranchManager.GetModel(Convert.ToInt32(this.ddlBranch.SelectedValue));
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = getReturnList();
            //try
            //{
                ESP.Finance.BusinessLogic.ReturnManager.ExportPCCredence(int.Parse(CurrentUser.SysID), branchModel, returnlist, this.Response);
            //}
            //catch (Exception ex)
            //{
            //    Page.ClientScript.RegisterStartupScript(typeof(string), new Guid().ToString(), "alert('" + ex.Message + "');", true);
            //}

        }
    }
}
