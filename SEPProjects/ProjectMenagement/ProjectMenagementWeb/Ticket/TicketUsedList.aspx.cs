﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;

namespace FinanceWeb.Ticket
{
    public partial class TicketUsedList : ESP.Web.UI.PageBase
    {
        private IList<ESP.Finance.Entity.ReturnInfo> returnlist = null;
        private IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> detaillist = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                IList<ESP.Finance.Entity.BranchInfo> blist = ESP.Finance.BusinessLogic.BranchManager.GetList(null, null);
                ddlBranch.DataSource = blist;
                ddlBranch.DataTextField = "BranchCode";
                ddlBranch.DataValueField = "BranchID";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("请选择", "0"));

                bindTicketUsed();
            }
        }

        private void bindTicketUsed()
        {
            string delegateusers = string.Empty;
            string deptIds = string.Empty;

            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                delegateusers += model.UserID.ToString() + ",";
            }
            delegateusers = delegateusers.TrimEnd(',');

            IList<ESP.Framework.Entity.OperationAuditManageInfo> operationList = null;
            if (string.IsNullOrEmpty(delegateusers))
            {
                operationList = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" receptionId =" + CurrentUser.SysID);
            }
            else
            {
                operationList = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList(" receptionId =" + CurrentUser.SysID + " or receptionId in(" + delegateusers + ")");
            }
            foreach (ESP.Framework.Entity.OperationAuditManageInfo model in operationList)
            {
                deptIds += model.DepId.ToString() + ",";
            }
            deptIds = deptIds.TrimEnd(',');

            string strwhere = string.Empty;

            if (!string.IsNullOrEmpty(deptIds))
            {
                strwhere = " and (b.departmentid in(" + deptIds + ") or b.receptionid =" + CurrentUser.SysID + ") ";
            }

            if (chkUsed.Checked)
                strwhere += " and TicketIsUsed=1 ";
            else
                strwhere += " and TicketIsUsed=0 ";

            if (this.txtKey.Text.Trim() != string.Empty)
            {
                strwhere += string.Format("  and ( goairno like '%{0}%' or expensedesc like '%{0}%' or boarder like '%{0}%' or boarderidcard like '%{0}%' or  b.returncode like '%{0}%' or b.projectcode like '%{0}%')", this.txtKey.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                strwhere += string.Format(" and (b.returnpredate between '{0}' and '{1}')", txtBeginDate.Text, DateTime.Parse(txtEndDate.Text).AddDays(1));
            }
            else
            {
                strwhere += string.Format(" and (b.returnpredate between '{0}' and '{1}')", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1));
            }
            if (this.ddlBranch.SelectedIndex != 0)
            {
                strwhere += string.Format(" and b.branchcode='{0}'", this.ddlBranch.SelectedItem.Text);
            }
            if (!string.IsNullOrEmpty(this.txtSupplier.Text))
            {
                strwhere += string.Format(" and SupplierName like '%{0}%' ", this.txtSupplier.Text);
            }

            IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> detailUsedlist = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetIicketUsed(strwhere);
            this.gvUsed.DataSource = detailUsedlist;
            this.gvUsed.DataBind();
        }

        protected void gvUsed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ESP.Finance.Entity.ExpenseAccountDetailInfo detail = null;

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                detail = (ESP.Finance.Entity.ExpenseAccountDetailInfo)e.Row.DataItem;
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(detail.ReturnID.Value);
                Label lblReturnCode = (Label)e.Row.FindControl("lblReturnCode");
                Label lblTicketUsed = (Label)e.Row.FindControl("lblTicketUsed");
                Label labOrderDate = (Label)e.Row.FindControl("labOrderDate");
                lblReturnCode.Text = returnModel.ReturnCode;
                if (returnModel.TicketNo != 0)
                    lblReturnCode.Text += "-" + returnModel.TicketNo.ToString();
                if (detail.TicketIsUsed == true)
                    lblTicketUsed.Text = "已使用";
                else
                    lblTicketUsed.Text = "未使用";

                labOrderDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
            }
        }

        protected void chkTicket_OnCheckedChanged(object sender, EventArgs e)
        {
            CollectSelected();
            string returnids = string.Empty;
            for (int i = 0; i < SelectedItems.Count; i++)
            {
                returnids += SelectedItems[i].ToString() + ",";
            }
            returnids = returnids.TrimEnd(',');
            if (!string.IsNullOrEmpty(returnids))
            {
                decimal total = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetIicketUsed(" and id in(" + returnids + ")").Sum(x => x.ExpenseMoney ?? 0);
                lblUsedTotal.Text = "已选中" + SelectedItems.Count.ToString() + "条记录,共计:" + total.ToString("#,##0.00");
            }
            else
                lblUsedTotal.Text = string.Empty;
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindTicketUsed();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            this.ddlBranch.SelectedIndex = 0;
            bindTicketUsed();
        }

        protected ArrayList SelectedItems
        {
            get
            {
                return (ViewState["mySelectedItems"] != null) ? (ArrayList)ViewState["mySelectedItems"] : null;
            }
            set
            {
                ViewState["mySelectedItems"] = value;
            }
        }


        protected void CollectSelected()
        {
            if (this.SelectedItems == null)
                SelectedItems = new ArrayList();
            else
                SelectedItems.Clear();
            string ID = string.Empty;
            for (int i = 0; i < this.gvUsed.Rows.Count; i++)
            {
                ID = gvUsed.Rows[i].Cells[0].Text.Trim();
                CheckBox cb = this.gvUsed.Rows[i].FindControl("chkTicket") as CheckBox;
                if (SelectedItems.Contains(ID) && !cb.Checked)
                    SelectedItems.Remove(ID);
                if (!SelectedItems.Contains(ID) && cb.Checked)
                    SelectedItems.Add(ID);
            }
            this.SelectedItems = SelectedItems;
        }

        protected void btnUsed_Click(object sender, EventArgs e)
        {
            CollectSelected();
            string ids = string.Empty;
            if (this.SelectedItems != null && this.SelectedItems.Count > 0)
            {
                for (int i = 0; i < this.SelectedItems.Count; i++)
                {
                    ids += SelectedItems[i] + ",";
                }
                ids = ids.TrimEnd(',');
                if (!string.IsNullOrEmpty(ids))
                {
                    ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.UpdateTicketUsed(ids);
                    bindTicketUsed();
                }
            }
        }


    }
}