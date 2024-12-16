using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using ESP.Framework.BusinessLogic;


public partial class Purchase_BatchPurchasePaid : ESP.Web.UI.PageBase
    {
        private Dictionary<int, string> UserNames;
        private int AuditorId;
        private int AuditorId2;

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_BatchPurchasePaid));
            this.ddlBranch.Attributes.Add("onchange", "selectBranch(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["AuditorId"], out AuditorId);
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["AuditorId2"], out AuditorId2);
            if (!Page.IsPostBack)
            {
                SearchAuditing();
                SearchFinance();
                SearchComplete();
            }
        }


        /// <summary>
        /// 审批中
        /// </summary>
        private void SearchAuditing()
        {
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            term = " (Status=@status2)  and BatchType in(1,3) ";
            
            SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)PaymentStatus.PurchaseMajor1;
            paramlist.Add(p2);

            if (CurrentUserID != AuditorId && CurrentUserID != AuditorId2)
            {
                term += " and (creatorId = " + CurrentUserID + " or batchid in(select formid from f_auditlog where auditorsysid=" + CurrentUserID + " and formtype=6))";
            }
            if (!string.IsNullOrEmpty(term))
            {
                if (this.txtKey.Text.Trim() != string.Empty)
                {
                    term += "  and ( amounts like '%'+@prno+'%' or suppliername like '%'+@prno+'%'or batchcode like '%'+@prno+'%' or purchasebatchcode like '%'+@prno+'%')";
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

                grAuditing.DataSource = returnlist;
                grAuditing.DataBind();

                decimal total = 0;
                string batchIds = "";
                foreach (ESP.Finance.Entity.PNBatchInfo model in returnlist)
                {
                    total += model.Amounts == null ? 0 : model.Amounts.Value;
                    batchIds += model.BatchID + ",";
                }
                litPZ2.Text = total.ToString("#,##0.00");
                litPN2.Text = batchIds.Length == 0 ? "0" : ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchId in (" + batchIds.TrimEnd(',') + ")", new List<SqlParameter>()).Count.ToString();
            }
        }

        /// <summary>
        /// 财务处理中
        /// </summary>
        private void SearchFinance()
        {
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            term = " (Status=@status1 or Status=@status2 or status=@status3 or status=@status4) and BatchType in(1,3) ";
            if (CurrentUserID != AuditorId && CurrentUserID != AuditorId2)
            {
                term += " and (creatorId = " + CurrentUserID + " or batchid in(select formid from f_auditlog where auditorsysid=" + CurrentUserID + " and formtype=6))";
            }
            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.MajorAudit;
            paramlist.Add(p1);
            SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)PaymentStatus.FinanceLevel1;
            paramlist.Add(p2);
            SqlParameter p3 = new SqlParameter("@status3", System.Data.SqlDbType.Int, 4);
            p3.SqlValue = (int)PaymentStatus.FinanceLevel2;
            paramlist.Add(p3);
            SqlParameter p4 = new SqlParameter("@status4", System.Data.SqlDbType.Int, 4);
            p4.SqlValue = (int)PaymentStatus.FinanceLevel3;
            paramlist.Add(p4);
            if (!string.IsNullOrEmpty(term))
            {
                if (this.txtKey.Text.Trim() != string.Empty)
                {
                    term += "  and ( amounts like '%'+@prno+'%' or suppliername like '%'+@prno+'%' or purchasebatchcode like '%'+@prno+'%' or batchcode like '%'+@prno+'%')";
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

                grFinance.DataSource = tmplist.ToList();
                grFinance.DataBind();
                decimal total = 0;
                string batchIds = "";
                foreach (var model in tmplist)
                {
                    total += model.Amounts == null ? 0 : model.Amounts.Value;
                    batchIds += model.BatchID + ",";
                }
                litPZ3.Text = total.ToString("#,##0.00");
                litPN3.Text = batchIds.Length == 0 ? "0" : ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchId in (" + batchIds.TrimEnd(',') + ")", new List<SqlParameter>()).Count.ToString();
            }
        }

        /// <summary>
        /// 已完成付款
        /// </summary>
        private void SearchComplete()
        {
            List<SqlParameter> paramlist = new List<SqlParameter>();
            string term = string.Empty;
            term = " (Status=@status1 or Status=@status2)  and (purchaseBatchCode is not null or purchaseBatchCode <> '')  and BatchType in(1,3) ";
            SqlParameter p1 = new SqlParameter("@status1", System.Data.SqlDbType.Int, 4);
            p1.SqlValue = (int)PaymentStatus.FinanceComplete;
            paramlist.Add(p1);

            SqlParameter p2 = new SqlParameter("@status2", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)PaymentStatus.FinanceReject;
            paramlist.Add(p2);

            if (CurrentUserID != AuditorId && CurrentUserID != AuditorId2)
            {
                term += " and (creatorId = " + CurrentUserID + "or batchid in(select formid from f_auditlog where auditorsysid=" + CurrentUserID + " and formtype=6))";
            }
            if (!string.IsNullOrEmpty(term))
            {
                if (this.txtKey.Text.Trim() != string.Empty)
                {
                    term += "  and ( amounts like '%'+@prno+'%' or suppliername like '%'+@prno+'%' or batchcode like '%'+@prno+'%' or purchasebatchcode like '%'+@prno+'%')";
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
                returnList = returnList.OrderBy(N => N.PaymentDate).ToList();

                var batchIds = returnList.Select(x => x.BatchID).ToArray();
                var totals = ESP.Finance.BusinessLogic.PNBatchManager.GetTotalAmounts(batchIds);

                var userIds = returnList.Where(x => x.CreatorID != null).Select(x => x.CreatorID.Value).Distinct().ToArray();
                this.UserNames = ESP.Framework.BusinessLogic.UserManagerEx.GetUserNames(userIds);

                ESP.Triplet<int, int, decimal> item;
                foreach (ESP.Finance.Entity.PNBatchInfo model in returnList)
                {
                    if (!totals.TryGetValue(model.BatchID, out item))
                        item = new ESP.Triplet<int, int, decimal>(model.BatchID, 0, 0);

                    model.PnCount = item.Second;
                    model.Total = item.Third;
                    model.Amounts = item.Third;

                    model.CreateYearMonth = model.CreateDate.Value.ToString("yyyy-MM");
                    //model.Total = ESP.Finance.BusinessLogic.PNBatchManager.GetTotalAmounts(model);
                    //model.PnCount = ESP.Finance.BusinessLogic.PNBatchRelationManager.GetList(" batchId=" + model.BatchID, new List<SqlParameter>()).Count;
                }
                returnList = returnList.OrderByDescending(N => N.CreateYearMonth).ToList();
                grComplete.DataSource = returnList;
                grComplete.DataBind();
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchAuditing();
            SearchFinance();
            SearchComplete();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            this.hidBranchID.Value = string.Empty;
            this.hidBranchName.Value = string.Empty;
            this.txtBeginDate.Text = string.Empty;
            this.txtEndDate.Text = string.Empty;
            SearchAuditing();
            SearchFinance();
            SearchComplete();
        }

        public string getUser(int userId)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userId);
            return "<a style='cursor:pointer;color:Black' onclick=\"ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(userId) + "');\">" + emp.Name + "</a>";
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


        protected void grAuditing_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            e.Item["BatchPrint"] = " <a onclick=\"window.open('Print/PNPrintForPurchaseBatch.aspx?BatchID=" + e.Item["BatchID"].ToString() + "');\"><img src=\"/images/Icon_Output.gif\" /></a>";
            e.Item["CreatorName"] = getUser(Convert.ToInt32(e.Item["CreatorID"]));
            e.Item["StatusName"] = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(int.Parse(e.Item["Status"].ToString()), 0, null);
        }
        protected void grFinance_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            e.Item["BatchPrint"] = " <a onclick=\"window.open('Print/PNPrintForPurchaseBatch.aspx?BatchID=" + e.Item["BatchID"].ToString() + "');\"><img src=\"/images/Icon_Output.gif\" /></a>";
            e.Item["CreatorName"] = getUser(Convert.ToInt32(e.Item["CreatorID"]));
            e.Item["StatusName"] = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(int.Parse(e.Item["Status"].ToString()), 0, null);
        }
        protected void grComplete_ItemDataBound(object sender, ComponentArt.Web.UI.GridItemDataBoundEventArgs e)
        {
            ESP.Finance.Entity.PNBatchInfo batchModel = (ESP.Finance.Entity.PNBatchInfo)e.Item.DataItem;
            if (batchModel.Status == (int)ESP.Finance.Utility.PaymentStatus.FinanceReject)
            {
                e.Item["RePayment"] = "<a href='BatchRepayEdit.aspx?" + RequestName.BatchID + "=" + e.Item["BatchID"].ToString() + "'><img src='/images/edit.gif'/></a>";
            }

            var id = batchModel.CreatorID;
            string creatorName;
            if (id == null || !this.UserNames.TryGetValue(id.Value, out creatorName))
            {
                creatorName = string.Empty;
            }

            e.Item["BatchPrint"] = " <a onclick=\"window.open('Print/PNPrintForPurchaseBatch.aspx?BatchID=" + e.Item["BatchID"].ToString() + "');\"><img src=\"/images/Icon_Output.gif\" /></a>";
            e.Item["CreatorName"] = creatorName; // getUser(Convert.ToInt32(e.Item["CreatorID"]));
            e.Item["StatusName"] = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(int.Parse(e.Item["Status"].ToString()), 0, null);
        }
    }

