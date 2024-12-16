using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.project
{
    public partial class FactoringAuditedList : ESP.Web.UI.PageBase
    {
        string term = string.Empty;
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(FinanceWeb.project.FactoringList));
            this.ddlBranch.Attributes.Add("onChange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");

            if (!IsPostBack)
            {
                Search();
            }
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }


        private void Search()
        {
            term = " and a.IsFactoring<>0 ";
            paramlist.Clear();

            if (!string.IsNullOrEmpty(this.txtKey.Text))
            {
                term += string.Format(" and (a.prno like '%{0}%' or returncode like '%{0}%' or project_code like '%{0}%' or supplier_name like '%{0}%')", txtKey.Text);
            }

            if (!string.IsNullOrEmpty(this.hidBranchCode.Value))
            {
                term += " and BranchCode ='" + this.hidBranchCode.Value + "'";
            }
            this.gvG.DataSource = ESP.Finance.BusinessLogic.ReturnManager.GetFactoringList(term);
            this.gvG.DataBind();

        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                ESP.Purchase.Entity.PaymentPeriodInfo periodModel = ESP.Purchase.BusinessLogic.PaymentPeriodManager.GetModelByPN(int.Parse(dr["ReturnID"].ToString()));
                ESP.Purchase.Entity.PeriodRecipientInfo relationModel = null;
                ESP.Purchase.Entity.GeneralInfo generalModel =null;
                if (periodModel != null)
                {
                    generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(periodModel.gid);
                    relationModel = ESP.Purchase.BusinessLogic.PeriodRecipientManager.GetModelByPeriodId(periodModel.id);

                }
                Label labState = (Label)e.Row.FindControl("labState");

                HyperLink linkPR = (HyperLink)e.Row.FindControl("linkPR");

                HyperLink linkPO = (HyperLink)e.Row.FindControl("linkPO");

                HyperLink linkPN = (HyperLink)e.Row.FindControl("linkPN");

                HyperLink linkGR = (HyperLink)e.Row.FindControl("linkGR");

                if (generalModel != null && generalModel.Requisitionflow == 0)
                {
                    linkPO.NavigateUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\Print\\FactoringOrderPrint.aspx?id=" + dr["Id"].ToString();

                }
                else
                {
                    linkPO.Visible = false;
                }

                labState.Text = ReturnPaymentType.ReturnStatusString(int.Parse(dr["ReturnStatus"].ToString()), 0, false);
                linkPR.NavigateUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\Print\\RequisitionFactoringPrint.aspx?id=" + dr["Id"].ToString();
                linkPN.NavigateUrl = "\\Purchase\\Print\\PNFactoringPrint.aspx?ReturnID=" + dr["ReturnID"].ToString();

                if (relationModel != null)
                {
                    linkGR.NavigateUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\Print\\RecipientFactoringPrint.aspx?newPrint=true&id=" + relationModel.recipientId.ToString();
                }
                else
                {
                    linkGR.Visible = false;
                }
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            Search();
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> GetBranchList()
        {
            IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
            List<List<string>> list = new List<List<string>>();
            List<string> item = null;
            foreach (ESP.Finance.Entity.BranchInfo branch in blist)
            {
                item = new List<string>();
                item.Add(branch.BranchID.ToString());
                item.Add(branch.BranchCode);
                list.Add(item);
            }
            List<string> c = new List<string>();
            c.Add("-1");
            c.Add("全部");
            list.Insert(0, c);
            return list;
        }
  
    }
}