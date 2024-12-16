using System;
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


namespace FinanceWeb.ExpenseAccount
{
    public partial class TicketList : ESP.Web.UI.PageBase
    {
        private IList<ESP.Finance.Entity.ReturnInfo> returnlist = null;
        //private IList<ESP.Finance.Entity.ExpenseAccountDetailInfo> detaillist = null;
        ESP.Finance.SqlDataAccess.WorkFlowDataContext dataContext = new ESP.Finance.SqlDataAccess.WorkFlowDataContext();

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
                bindList();

            }
        }


        protected ArrayList PNSelectedItems
        {
            get
            {
                return (ViewState["PNSelectedItems"] != null) ? (ArrayList)ViewState["PNSelectedItems"] : null;
            }
            set
            {
                ViewState["PNSelectedItems"] = value;
            }
        }

        protected void CollectSelectedPN()
        {
            if (this.PNSelectedItems == null)
                PNSelectedItems = new ArrayList();
            else
                PNSelectedItems.Clear();
            string MID = string.Empty;
            for (int i = 0; i < this.gvG.Rows.Count; i++)
            {
                MID = gvG.Rows[i].Cells[1].Text.Trim();
                CheckBox cb = this.gvG.Rows[i].FindControl("chkReturn") as CheckBox;
                if (PNSelectedItems.Contains(MID) && !cb.Checked)
                    PNSelectedItems.Remove(MID);
                if (!PNSelectedItems.Contains(MID) && cb.Checked)
                    PNSelectedItems.Add(MID);
            }
            this.PNSelectedItems = PNSelectedItems;
        }


        private void bindList()
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
            if (!string.IsNullOrEmpty(deptIds))
            {
                string term = string.Empty;
                List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

                term = " ReturnType=40 and returnstatus not in(0,-1,1) and (receptionid =" + CurrentUser.SysID + " or requestorid in(select userid from sep_employeesinpositions where departmentid in(" + deptIds + "))) and returnid not in(select returnid from f_pnbatchrelation) ";
                if (this.txtKey.Text.Trim() != string.Empty)
                {
                    term += "  and (projectcode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%' or prefee  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%')";
                    System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                    p1.SqlValue = this.txtKey.Text.Trim();
                    paramlist.Add(p1);
                }
                if (!string.IsNullOrEmpty(txtBeginDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                {
                    term += string.Format(" and (returnpredate between '{0}' and '{1}')", txtBeginDate.Text, DateTime.Parse(txtEndDate.Text).AddDays(1));
                }
                //else
                //{
                //    term += string.Format(" and (returnpredate between '{0}' and '{1}')", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1));
                //}
                if (this.ddlBranch.SelectedIndex != 0)
                {
                    term += " and Branchcode = @BranchCode";
                    System.Data.SqlClient.SqlParameter pBrach = new System.Data.SqlClient.SqlParameter("@BranchCode", System.Data.SqlDbType.NVarChar, 50);
                    pBrach.SqlValue = this.ddlBranch.SelectedItem.Text;
                    paramlist.Add(pBrach);
                }
                if (!string.IsNullOrEmpty(this.txtSupplier.Text))
                {
                    term += " and SupplierName like '%'+@SupplierName+'%' ";
                    System.Data.SqlClient.SqlParameter psup = new System.Data.SqlClient.SqlParameter("@SupplierName", System.Data.SqlDbType.NVarChar, 50);
                    psup.SqlValue = this.txtSupplier.Text;
                    paramlist.Add(psup);
                }
                returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
                //int[] ids = returnlist.Select(x => x.ReturnID).ToArray();
                //if (ids != null && ids.Count() > 0)
                //{
                //    var t = new System.Text.StringBuilder(" and returnid in (");
                //    t.Append(ids[0]);
                //    for (var i = 1; i < ids.Length; i++)
                //    {
                //        t.Append(",").Append(ids[i]);
                //    }
                //    t.Append(") ");

                //  detaillist = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and id not in(select parentid from f_expenseaccountdetail  where parentid<>0) " + t.ToString());
                //}
                this.gvG.DataSource = returnlist;
                this.gvG.DataBind();
            }
        }

        protected void chkReturn_OnCheckedChanged(object sender, EventArgs e)
        {
            CollectSelectedPN();
            string returnids = string.Empty;
            for (int i = 0; i < PNSelectedItems.Count; i++)
            {
                returnids += PNSelectedItems[i].ToString() + ",";
            }
            returnids = returnids.TrimEnd(',');
            if (!string.IsNullOrEmpty(returnids))
            {
                decimal total = ESP.Finance.BusinessLogic.ReturnManager.GetList(" returnid in(" + returnids + ")").Sum(x => x.PreFee ?? 0);
                lblTotal.Text = "已选中" + PNSelectedItems.Count.ToString() + "条记录,共计:" + total.ToString("#,##0.00");
            }
            else
                lblTotal.Text = string.Empty;
        }




        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;

                Label labReturncode = (Label)e.Row.FindControl("labReturncode");
                Label labTicketUsed = (Label)e.Row.FindControl("labTicketUsed");
                //edit
                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                Label labState = (Label)e.Row.FindControl("labState");

                Label labAuditor = (Label)e.Row.FindControl("labAuditor");

                //labDate
                Label labDate = (Label)e.Row.FindControl("labDate");
                //labPrefee
                Label labPrefee = (Label)e.Row.FindControl("labPrefee");
                //lnkConfirm
                LinkButton lnkConfirm = (LinkButton)e.Row.FindControl("lnkConfirm");
                //lnkMail
                LinkButton lnkMail = (LinkButton)e.Row.FindControl("lnkMail");
                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");

                CheckBox chkReturn = (CheckBox)e.Row.FindControl("chkReturn");

                var detailsNocancel = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetList(" and id not in(select parentid from f_expenseaccountdetail  where parentid<>0 and returnid =" + returnModel.ReturnID.ToString() + ") and (TicketIsUsed=0 or TicketIsConfirm=0)  and returnid =" + returnModel.ReturnID.ToString());

                if (detailsNocancel.Count() > 0)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                    }
                    chkReturn.Enabled = false;
                }

                if (labReturncode != null)
                {
                    labReturncode.Text = returnModel.ReturnCode;
                    if (returnModel.TicketNo != 0)
                        labReturncode.Text += "-" + returnModel.TicketNo.ToString();
                }
                if (hylEdit != null)
                {
                    hylEdit.NavigateUrl = "TicketEdit.aspx?id=" + returnModel.ReturnID.ToString() + "&" + ESP.Finance.Utility.RequestName.BackUrl + "=TicketList.aspx";
                }
                if (labState != null)
                {
                    labState.Text = ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 40, returnModel.IsDiscount);
                }
                if (labDate != null)
                {
                    labDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
                }

                if (labAuditor != null)
                {
                    string auditor = ESP.Finance.BusinessLogic.ExpenseAccountManager.GetAssigneeNameByWorkItemID(returnModel.ReturnID, (int)ESP.Workflow.WorkItemStatus.Open, dataContext);

                    if (returnModel.ReturnStatus == 100)
                        auditor = "前台";
                    else if (returnModel.ReturnStatus == 107)
                        auditor = "供应商";
                    labAuditor.Text = auditor;
                }


                if (labPrefee != null)
                {
                    labPrefee.Text = returnModel.PreFee.Value.ToString("#,##0.00");
                }
                if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Submit || returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.PrepareAudit || returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.GeneralManagerAudit || returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.CEOAudit || returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.ProjectManagerAudit || returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.MajorAudit || returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_ReceptionConfirm || returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_SupplierConfirm || returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_Received)
                {
                    hylEdit.Visible = true;
                }
                else
                {
                    hylEdit.Visible = false;
                }
                if (returnModel.ReturnStatus >= (int)ESP.Finance.Utility.PaymentStatus.MajorAudit)
                {
                    hylPrint.Visible = true;
                    hylPrint.NavigateUrl = "Print/TicketPrint.aspx?expenseID=" + returnModel.ReturnID.ToString();
                    hylPrint.Target = "_blank";
                }
                else
                {
                    hylPrint.Visible = false;
                }
                if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_ReceptionConfirm)
                {
                    lnkMail.Visible = true;
                }
                else
                    lnkMail.Visible = false;


                if (returnModel.ReturnStatus == (int)ESP.Finance.Utility.PaymentStatus.Ticket_SupplierConfirm)
                {
                    string ticketUsed = string.Empty;
                    string ticketConfirm = string.Empty;
                    List<ESP.Finance.Entity.ExpenseAccountDetailInfo> ticketList = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetTicketDetail(returnModel.ReturnID);

                    if (ticketList != null && ticketList.Count > 0)
                    {
                        //供应商是否确认
                        var TicketIsUsedList = ticketList.Where(x => x.TicketIsUsed == false).ToList();
                        //业务是否确认使用
                        var TicketIsConfirmList = ticketList.Where(x => x.TicketIsConfirm == false).ToList();

                        if (TicketIsUsedList.Count != 0)
                        {
                            ticketUsed = "供应商待出票";
                        }
                        if (TicketIsConfirmList.Count != 0)
                        {
                            ticketConfirm = "申请人待使用";
                        }

                        if (ticketUsed != string.Empty && ticketConfirm != string.Empty)
                        {
                            labTicketUsed.Text = ticketUsed + "<br>" + ticketConfirm;
                        }
                        else
                        {
                            labTicketUsed.Text = ticketUsed + "&nbsp;" + ticketConfirm;
                        }
                    }
                }
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();

        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            this.ddlBranch.SelectedIndex = 0;
            bindList();

        }



        protected void btnBatchCreate_Click(object sender, EventArgs e)
        {
            CollectSelectedPN();


            if (this.PNSelectedItems != null && this.PNSelectedItems.Count > 0)
            {
                try
                {
                    // IList<ESP.Finance.Entity.TicketSupplier> suplierList = ESP.Finance.BusinessLogic.TicketSupplierManager.GetList(" receptionid=" + CurrentUser.SysID, new List<SqlParameter>());

                    ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(Convert.ToInt32(PNSelectedItems[0]));

                    ESP.Finance.Entity.PNBatchInfo batchModel = new PNBatchInfo();
                    batchModel.BatchType = 3;
                    batchModel.BatchCode = ESP.Finance.BusinessLogic.PNBatchManager.CreatePurchaseBatchCode();
                    batchModel.PurchaseBatchCode = batchModel.BatchCode;
                    batchModel.PaymentDate = DateTime.Now;
                    batchModel.IsInvoice = 1;
                    batchModel.Description = "机票申请单";
                    //if (suplierList != null && suplierList.Count > 0)
                    //{
                    batchModel.SupplierName = returnModel.SupplierName;
                    batchModel.SupplierBankName = returnModel.SupplierBankName;
                    batchModel.SupplierBankAccount = returnModel.SupplierBankAccount;
                    //}

                    int ret = ESP.Finance.BusinessLogic.PNBatchManager.BatchTicketCreate(batchModel, PNSelectedItems, CurrentUser);
                    if (ret > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('创建成功!');", true);
                        bindList();

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('创建失败!');'", true);

                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
                }
            }
        }

        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int returnid = int.Parse(e.CommandArgument.ToString());
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnid);
            if (returnModel.TicketSupplierId != 0)
            {
                ESP.Finance.Entity.TicketSupplier suplierModel = ESP.Finance.BusinessLogic.TicketSupplierManager.GetModel(returnModel.TicketSupplierId);

                string mail = string.Empty;
                if (suplierModel != null )
                {
                    mail = suplierModel.Email;
                }
                if (e.CommandName == "SendMail")
                {
                    sendMail(returnid, mail);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('发送成功!');", true);
                }
            }
        }

        private void sendMail(int returnId, string mail)
        {
            ESP.Finance.Utility.SendMailHelper.SendMailTicket(returnId, mail, 1);
        }

        //private void BindSupplier()
        //{
        //    IList<ESP.Finance.Entity.TicketSupplier> suplierList = ESP.Finance.BusinessLogic.TicketSupplierManager.GetList(" receptionid=" + CurrentUser.SysID, new List<SqlParameter>());
        //    if (suplierList != null && suplierList.Count > 0)
        //    {
        //        this.lblAddress.Text = suplierList[0].Address;
        //        this.lblContacter.Text = suplierList[0].Contacter;
        //        this.lblMail.Text = suplierList[0].Email;
        //        this.lblMobile.Text = suplierList[0].Mobile;
        //        this.lblSupplierName.Text = suplierList[0].SupplierName;
        //        this.lblTel.Text = suplierList[0].Tel;
        //    }
        //}

    }
}
