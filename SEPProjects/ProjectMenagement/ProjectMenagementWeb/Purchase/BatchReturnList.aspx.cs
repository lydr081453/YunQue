using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

namespace FinanceWeb.Purchase
{
    public partial class BatchReturnList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(BatchReturnList));
            this.ddlBranch.Attributes.Add("onChange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
            Search();
            SearchHist();
        }
        private void Search()
        {
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            string DelegateUsers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                DelegateUsers += model.UserID.ToString() + ",";
            }
            DelegateUsers = DelegateUsers.TrimEnd(',');
            if (!string.IsNullOrEmpty(DelegateUsers))
                term = " (Status=@status1 or  Status=@status2 or Status=@status3 or Status=@status4 ) AND (PaymentUserID=@sysID or PaymentUserID in(" + DelegateUsers + ")) and BatchType in(1) ";
            else
                term = " (Status=@status1 or  Status=@status2 or Status=@status3 or Status=@status4) AND PaymentUserID=@sysID and BatchType in(1)";
            SqlParameter p3 = new SqlParameter("@status3", System.Data.SqlDbType.Int, 4);
            p3.SqlValue = (int)PaymentStatus.MajorAudit;
            paramlist.Add(p3);
            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.FinanceLevel1;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)PaymentStatus.FinanceLevel2;
            paramlist.Add(p2);
            SqlParameter p4 = new SqlParameter("@status4", System.Data.SqlDbType.Int, 4);
            p4.SqlValue = (int)PaymentStatus.FinanceLevel3;
            paramlist.Add(p4);
            SqlParameter p6 = new SqlParameter("@sysID", System.Data.SqlDbType.Int, 4);
            p6.SqlValue = CurrentUser.SysID;
            paramlist.Add(p6);
            if (!string.IsNullOrEmpty(term))
            {
                if (this.txtKey.Text.Trim() != string.Empty)
                {
                    term += "  and ( amounts like '%'+@prno+'%' or batchcode like '%'+@prno+'%'  or  purchasebatchcode like '%'+@prno+'%' or  suppliername like '%'+@prno+'%')";
                    SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                    sp1.SqlValue = this.txtKey.Text.Trim();
                    paramlist.Add(sp1);

                }
                if (!string.IsNullOrEmpty(this.hidBranchID.Value) && !string.IsNullOrEmpty(this.hidBranchName.Value))
                {
                    term += " and Branchcode = @BranchCode ";
                    System.Data.SqlClient.SqlParameter pBrach = new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 50);
                    pBrach.SqlValue = this.hidBranchName.Value;
                    paramlist.Add(pBrach);
                }
                if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
                {
                    if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                    {
                        term += " and LastUpdateDateTime between @beginDate and @endDate";
                        System.Data.SqlClient.SqlParameter sp3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                        sp3.SqlValue = this.txtBeginDate.Text;
                        paramlist.Add(sp3);
                        System.Data.SqlClient.SqlParameter sp4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                        sp4.SqlValue = this.txtEndDate.Text;
                        paramlist.Add(sp4);

                    }
                }
                IList<ESP.Finance.Entity.PNBatchInfo> returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetList(term, paramlist);
                var tmplist = returnList.OrderBy(N => N.PaymentDate);
                IList<ESP.Finance.Entity.PNBatchInfo> returnlist = tmplist.ToList();
                this.gvG.DataSource = returnlist;
                this.gvG.DataBind();
            }
        }

        private void SearchHist()
        {
            IList<ESP.Finance.Entity.PNBatchInfo> list;
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            IList<ESP.Finance.Entity.BranchInfo> branches;
            string Branchs = string.Empty;

            branches = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
            var branchList = branches.Where(x => x.OtherFinancialUsers != null && x.OtherFinancialUsers.Contains("," + CurrentUser.SysID + ","));
            if (branchList != null)
            {
                foreach (ESP.Finance.Entity.BranchInfo b in branchList)
                {
                    Branchs += "'" + b.BranchCode + "',";
                }
            }
            Branchs = Branchs.TrimEnd(',');

            term = " branchcode  in (" + Branchs + ")  and BatchType in(1,3) ";

            SqlParameter p1 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
            p1.Value = CurrentUser.SysID;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@AuditeStatus", SqlDbType.Int, 4);
            p2.Value = (int)ESP.Finance.Utility.AuditHistoryStatus.UnAuditing;
            paramlist.Add(p2);
            if (!string.IsNullOrEmpty(term))
            {
                if (this.txtKey.Text.Trim() != string.Empty)
                {
                    term += "  and ( amounts like '%'+@prno+'%' or batchcode like '%'+@prno+'%' or  purchasebatchcode like '%'+@prno+'%' or suppliername like '%'+@prno+'%')";
                    SqlParameter sp1 = new SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                    sp1.SqlValue = this.txtKey.Text.Trim();
                    paramlist.Add(sp1);

                }
                if (!string.IsNullOrEmpty(this.hidBranchID.Value) && !string.IsNullOrEmpty(this.hidBranchName.Value))
                {
                    term += " and Branchcode = @BranchCode";
                    System.Data.SqlClient.SqlParameter pBrach = new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 50);
                    pBrach.SqlValue = this.hidBranchName.Value;
                    paramlist.Add(pBrach);
                }
                if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
                {
                    if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                    {
                        term += " and LastUpdateDateTime between @beginDate and @endDate";
                        System.Data.SqlClient.SqlParameter sp3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                        sp3.SqlValue = this.txtBeginDate.Text;
                        paramlist.Add(sp3);
                        System.Data.SqlClient.SqlParameter sp4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                        sp4.SqlValue = this.txtEndDate.Text;
                        paramlist.Add(sp4);

                    }
                }
            }
            list = ESP.Finance.BusinessLogic.PNBatchManager.GetList(term, paramlist);
            this.GvHist.DataSource = list;
            this.GvHist.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
            SearchHist();
        }

        protected void lnkNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateBatchReturn.aspx?");
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            this.hidBranchID.Value = string.Empty;
            this.hidBranchName.Value = string.Empty;
            this.txtBeginDate.Text = string.Empty;
            this.txtEndDate.Text = string.Empty;
            Search();
            SearchHist();
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            Search();
        }
        protected void GvHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvHist.PageIndex = e.NewPageIndex;
            SearchHist();
        }
        protected void GvHist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.PNBatchInfo batchModel = (ESP.Finance.Entity.PNBatchInfo)e.Row.DataItem;
                Label lblName = (Label)e.Row.FindControl("lblName");
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(batchModel.CreatorID.Value);
                lblName.Text = emp.Name;
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(batchModel.CreatorID.Value) + "');");
                HyperLink hylDisplay = (HyperLink)e.Row.FindControl("hylDisplay");
                hylDisplay.NavigateUrl = "BatchReturnDisplay.aspx?" + RequestName.BatchID + "=" + batchModel.BatchID.ToString();
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = ReturnPaymentType.ReturnStatusString(batchModel.Status.Value, 0, null);
                Label lblAmounts = (Label)e.Row.FindControl("lblAmounts");
                lblAmounts.Text = ESP.Finance.BusinessLogic.PNBatchManager.GetTotalAmounts(batchModel).ToString("#,##0.00");
                Label lblAttach = (Label)e.Row.FindControl("lblAttach");

                Label lblPrint = (Label)e.Row.FindControl("lblPrint");
                if (lblPrint != null)
                {
                    lblPrint.Text = " <a style=\"cursor:hand\" onclick=\"window.open('Print/PNPrintForPurchaseBatch.aspx?" + ESP.Finance.Utility.RequestName.BatchID + "=" + batchModel.BatchID.ToString() + "&Type=Finance');\"><img src=\"/images/Icon_Output.gif\" /></a>";
                    lblPrint.Text += "&nbsp;<a style=\"cursor:hand\" onclick=\"window.open('Print/PNPrintForPurchaseBatch.aspx?" + ESP.Finance.Utility.RequestName.BatchID + "=" + batchModel.BatchID.ToString() + "');\"><img src=\"/images/Icon_Output.gif\" /></a>";
                }
                IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchModel.BatchID);
                string mediaorderIDs = string.Empty;
                if (returnList != null && returnList.Count > 0)
                {
                    foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
                    {
                        if (model != null)
                        {
                            mediaorderIDs += model.MediaOrderIDs + ",";
                        }
                    }
                    mediaorderIDs = mediaorderIDs.TrimEnd(',');
                    if (!string.IsNullOrEmpty(mediaorderIDs))
                    {
                        lblAttach.Text = "<a href='Print\\MediaUnPayment.aspx?OrderID=" + mediaorderIDs + "'style='cursor: hand' target='_blank'> <img title='未付款记者浏览' src='/images/PrintDefault.gif' border='0px;' /></a>";
                    }
                    else
                    {
                        lblAttach.Text = "";
                    }
                }
                //重汇
                if (batchModel.Status != (int)PaymentStatus.FinanceComplete || this.GetFinanceUser(batchModel).IndexOf("," + CurrentUser.SysID + ",") < 0)
                {
                    e.Row.Cells[11].Text = "";
                }
                else
                {
                    e.Row.Cells[11].Text = "<a href='/Purchase/BatchRepay.aspx?" + RequestName.BatchID + "=" + batchModel.BatchID.ToString() + "' style='cursor: hand'><img title='重汇' src='/images/Edit.gif' border='0px;' ></img></a>";
                }
            }
        }

        private string GetFinanceUser(ESP.Finance.Entity.PNBatchInfo model)
        {
            ESP.Finance.Entity.BranchInfo branch = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(model.BranchCode);
            if (branch != null)
            {
                string users = "," + branch.FirstFinanceID.ToString() + "," + branch.FinalAccounter.ToString() + ",";
                return users;
            }
            else
                return "";
        }


        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int BatchID = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.BusinessLogic.PNBatchManager.Delete(BatchID);
                Search();
            }

        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.PNBatchInfo batchModel = (ESP.Finance.Entity.PNBatchInfo)e.Row.DataItem;
                Label lblName = (Label)e.Row.FindControl("lblName");
                ESP.Compatible.Employee emp = new ESP.Compatible.Employee(batchModel.CreatorID.Value);
                lblName.Text = emp.Name;
                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(batchModel.CreatorID.Value) + "');");
                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                hylEdit.NavigateUrl = "BatchReturnEdit.aspx?" + RequestName.BatchID + "=" + batchModel.BatchID.ToString() + "&" + RequestName.Operate + "=Audit";
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = ReturnPaymentType.ReturnStatusString(batchModel.Status.Value, 0, null);
                Label lblAmounts = (Label)e.Row.FindControl("lblAmounts");
                lblAmounts.Text = ESP.Finance.BusinessLogic.PNBatchManager.GetTotalAmounts(batchModel).ToString("#,##0.00");
                Label lblAttach = (Label)e.Row.FindControl("lblAttach");

                Label lblPrint = (Label)e.Row.FindControl("lblPrint");
                if (lblPrint != null)
                {
                    lblPrint.Text = " <a style=\"cursor:hand\" onclick=\"window.open('Print/PNPrintForPurchaseBatch.aspx?" + ESP.Finance.Utility.RequestName.BatchID + "=" + batchModel.BatchID.ToString() + "&Type=Finance');\"><img src=\"/images/Icon_Output.gif\" /></a>";
              lblPrint.Text += "&nbsp;<a style=\"cursor:hand\" onclick=\"window.open('Print/PNPrintForPurchaseBatch.aspx?" + ESP.Finance.Utility.RequestName.BatchID + "=" + batchModel.BatchID.ToString() + "');\"><img src=\"/images/Icon_Output.gif\" /></a>";
             
                }

                IList<ESP.Finance.Entity.ReturnInfo> returnList = ESP.Finance.BusinessLogic.PNBatchManager.GetReturnList(batchModel.BatchID);
                string mediaorderIDs = string.Empty;
                if (returnList != null)
                {
                    foreach (ESP.Finance.Entity.ReturnInfo model in returnList)
                    {
                        if (model!=null && model.MediaOrderIDs != null && model.MediaOrderIDs != "")
                            mediaorderIDs += model.MediaOrderIDs + ",";
                    }
                    mediaorderIDs = mediaorderIDs.TrimEnd(',');
                    if (!string.IsNullOrEmpty(mediaorderIDs))
                    {
                        lblAttach.Text = "<a href='Print\\MediaUnPayment.aspx?OrderID=" + mediaorderIDs + "'style='cursor: hand' target='_blank'> <img title='未付款记者浏览' src='/images/PrintDefault.gif' border='0px;' /></a>";
                    }
                    else
                    {
                        lblAttach.Text = "";
                    }
                }
            }
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
            c.Add("请选择...");
            list.Insert(0, c);
            return list;
        }
    }
}
