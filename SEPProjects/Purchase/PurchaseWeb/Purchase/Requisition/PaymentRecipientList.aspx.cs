using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_PaymentRecipientList : ESP.Web.UI.PageBase
{
    int generalid = 0;
    int IsMediaOrder = 0;
    //string recipientIds = "";
    decimal dynamicPercent = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        if (!IsPostBack)
        {
            /*
            radCalPaymentType.Items.Add(new ListItem("默认方式", "0"));
            radCalPaymentType.Items.Add(new ListItem("依据收货金额计算", "1"));
            radCalPaymentType.Items.Add(new ListItem("根据帐期金额计算", "2"));
            radCalPaymentType.SelectedValue = "0";
            */
            if (generalid > 0)
            {
                GeneralInfo gmodel = GeneralInfoManager.GetModel(generalid);
                IsMediaOrder = gmodel.IsMediaOrder;

                labPrno.Text = gmodel.PrNo;
                txtaccountBank.Text = gmodel.account_bank;
                txtaccountName.Text = gmodel.account_name;
                txtaccountNum.Text = gmodel.account_number;
                PRTopMessage.Model = gmodel;
                txtaccountBank.Enabled = txtaccountName.Enabled = txtaccountNum.Enabled = gmodel.source != "协议供应商";
            }
            bindInfo();
        }
        ListBind();
    }

    #region 账期
    private void ListBind()
    {
        DataTable dt = PaymentPeriodManager.GetPaymentPeriodAllList(int.Parse(Request[RequestName.GeneralID]));
        gvPayment.DataSource = dt;
        gvPayment.DataBind();
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        int paymentPeriodId = int.Parse(Request["radPeriod"].ToString().Split('#')[0]);
        PaymentPeriodInfo paymentPeriod = PaymentPeriodManager.GetModel(paymentPeriodId);
        ESP.Purchase.Entity.GeneralInfo generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(paymentPeriod.gid);
        decimal OrderTotalPrice = ESP.Purchase.BusinessLogic.OrderInfoManager.getTotalPriceByGID(generalModel.id);
        string recipientIds = hidRecipientIds.Value;

        int periodCount = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetPaymentPeriodCount(paymentPeriod.gid);
        int pnCount = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetPNCount(paymentPeriod.gid);
        //收货单列表
        List<RecipientInfo> recipientList = null;
        if (!string.IsNullOrEmpty(recipientIds))
        {
            recipientList = RecipientManager.getModelList(" and id in (" + recipientIds + ")", new List<SqlParameter>());
        }
        else
        {
            recipientList = new List<RecipientInfo>();
        }
        if (paymentPeriod.inceptPrice != 0 || paymentPeriod.Status != State.PaymentStatus_save || paymentPeriod.ReturnId!=0 || pnCount>=periodCount)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('该付款条款已经创建付款申请！');", true);
            return;
        }

        if (paymentPeriod.periodType == 1 && recipientList.Count > 0)//如果是预付条款,不能和收货关联
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('预付条款不能与收货关联，请直接根据预付款创建付款申请！');", true);
            return;
        }

        decimal recipientPrice = 0; //收货金额
        foreach (RecipientInfo model in recipientList)
        {
            recipientPrice += model.RecipientAmount;
        }
        //here add code to impliment the funcation as oppenning or standard payment
        if (generalModel.PeriodType == (int)ESP.Purchase.Common.State.PeriodType4CreatePN.Standard)
        {
            if (recipientIds == "" && recipientPrice == 0)
            {
                paymentPeriod.inceptPrice = paymentPeriod.expectPaymentPrice;
                recipientPrice = paymentPeriod.expectPaymentPrice;
            }
            paymentPeriod.inceptDate = DateTime.Now;
            paymentPeriod.Status = State.PaymentStatus_wait;

            //付款金额设置
            //检查是否是金额不符收货的付款

            if (!string.IsNullOrEmpty(recipientIds) && RecipientManager.IsUnsureRecipient(recipientIds))
            {
                List<RecipientInfo> list = RecipientManager.getModelList(" and gid=" + generalid + " and id not in (" + recipientIds + ")", new List<SqlParameter>());
                foreach (RecipientInfo m in list)
                {
                    if (m.IsConfirm != State.recipentConfirm_PaymentCommit && m.IsConfirm != State.recipentConfirm_PaymentCommit)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('本次收货为最后一次收货，请先处理其他收货！');", true);
                        return;
                    }
                }
                //paymentPeriod.inceptPrice = (RecipientManager.getRecipientSum(paymentPeriod.gid) + PaymentPeriodManager.getPayemntYF(generalid)) - PaymentPeriodManager.getPaymentSum(paymentPeriod.gid);
                paymentPeriod.inceptPrice = RecipientManager.getRecipientSum(paymentPeriod.gid) - PaymentPeriodManager.getPaymentSum(paymentPeriod.gid);
            }
            else
            {
                ////金额检查
                //if (gvPayment.Rows.Count == 1 && recipientPrice > paymentPeriod.expectPaymentPrice)
                //    //如果所选收货账期为最后一个收货账期，收货金额不能大于账期金额
                //{
                //    //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('收货金额不能大于账期金额！');", true);
                //    ShowCompleteMessage("收货金额不能大于账期金额！", "PaymentRecipientList.aspx?" + Request.Url);
                //    return;
                //}
                if (recipientPrice > paymentPeriod.expectPaymentPrice) //如果所选收货金额大于账期金额，只收账期金额
                {
                    paymentPeriod.inceptPrice = paymentPeriod.expectPaymentPrice;

                }
                else //如果所选收货金额小于账期金额，只收收货金额
                {
                    paymentPeriod.inceptPrice = recipientPrice;
                }
            }
        }
        else//开放式账期以收货单的金额为准
        {
            if (recipientIds == "" && recipientPrice == 0)
            {
                paymentPeriod.inceptPrice = paymentPeriod.expectPaymentPrice;
            }
            else
            {
                paymentPeriod.inceptPrice = recipientPrice;
            }
            paymentPeriod.inceptDate = DateTime.Now;
            paymentPeriod.Status = State.PaymentStatus_wait;
            decimal recipientTotal = RecipientManager.GetSumRecipientConfirmed(generalModel.id);
            //如果提交PN总金额已经大于PR总金额*110%，则不能创建PN
            if (recipientTotal + recipientPrice > OrderTotalPrice * Convert.ToDecimal(1.1))
            {
                ShowCompleteMessage("收货金额已经超出申请单的总金额！", "PaymentRecipientList.aspx?" + Request.Url);
            }
        }

        //更新付款账期和收货单
        if (!PaymentPeriodManager.UpdatePaymentPeriodAndRecipient(paymentPeriod, recipientIds))
            ShowCompleteMessage("确认失败！", "PaymentRecipientList.aspx?" + Request.Url);
        else
        {
            //记录操作日志
            //ESP.Logging.Logger.Add(string.Format("{0}对T_PaymentPeriod表中的id={1}和T_Recipient表中的id={2}进行{3}操作", CurrentUser.Name, paymentPeriod.id, recipientIds, "关联"), "创建付款申请");
            //Response.Redirect("PaymentRecipientList.aspx?" + Request.Url);
        }
        btnUpdate_Click(new object(), new EventArgs());
        int periodId = paymentPeriodId;

        int returnId = 0;
        if (PaymentPeriodManager.CommitPeriod(periodId.ToString(), "0", CurrentUser.SysID, CurrentUserCode, CurrentUser.ITCode, CurrentUser.Name, ref returnId, isNeedPurchaseAudit(), generalModel.IsMediaOrder))
        {
            getRedirectUrl(returnId);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交付款申请失败!');", true);
        }
        bindInfo();
        ListBind();
    }

    /// <summary>
    /// 是否需要采购部审核
    /// </summary>
    /// <returns></returns>
    private bool isNeedPurchaseAudit()
    {
        //List<OrderInfo> orderList = OrderInfoManager.GetListByGeneralId(generalid);
        //GeneralInfo model = GeneralInfoManager.GetModel(generalid);
        //bool isXY = false;
        //int typecount = 0;

        //foreach (OrderInfo m in orderList)
        //{
        //    if (m.supplierId > 0)
        //    {
        //        ESP.Purchase.Entity.ESPAndSupplySuppliersRelation relation = ESP.Purchase.BusinessLogic.ESPAndSupplySuppliersRelationManager.GetModelByEid(m.supplierId);
        //        if (relation != null)
        //            typecount = ESP.Supplier.BusinessLogic.SC_SupplierprotocolTypeManager.GetListForSupplier(relation.SupplySupplierId);
        //    }
        //    if (typecount > 0 && !isADMediaType(m.producttype.ToString().Trim()))
        //    {
        //        isXY = true;
        //        break;
        //    }
        //}

        //if (isXY && model.PRType != (int)PRTYpe.PR_OtherMedia)
        //    return true;
        //else
            return false;
    }

    private bool isADMediaType(string type)
    {
        //由王祯审批的腾信,不需要采购部审批,转到FA即可
        string ADTypeIDs = "," + ESP.Configuration.ConfigurationManager.SafeAppSettings["ADTypeIDs"] + ",";
        if (ADTypeIDs.IndexOf("," + type + ",") >= 0)
        {
            return true;
        }
        return false;
    }
    ///// <summary>
    ///// 采购物料类别是否包含北京采购部专属类别
    ///// </summary>
    ///// <returns></returns>
    //private bool isSingleProductType(List<OrderInfo> orderList)
    //{
    //    SupplierInfo supplierModel = null;
    //    System.Collections.ArrayList orderTypes = new System.Collections.ArrayList();//PR单下所有物料类别
    //    string[] zbProductTypes = ESP.Configuration.ConfigurationManager.SafeAppSettings["BJSingle"].Split(',');
    //    foreach (OrderInfo orderModel in orderList)
    //    {
    //        if (orderModel.supplierId > 0)
    //            supplierModel = SupplierManager.GetModel(orderModel.supplierId);
    //        orderTypes.Add(orderModel.producttype);
    //    }
    //    if (supplierModel != null)
    //    {
    //        foreach (string type in zbProductTypes)
    //        {
    //            //如果物料类别包含北京采购部专属类别，则北京采购部可以创建付款申请
    //            if (orderTypes.Contains(int.Parse(type)))
    //                return true;
    //        }
    //    }
    //    return false;
    //}

    ///// <summary>
    ///// 是否为本地协议供应商
    ///// </summary>
    ///// <param name="model"></param>
    ///// <returns></returns>
    //private bool IsLocalSupplier(List<OrderInfo> orderList, GeneralInfo model)
    //{
    //    SupplierInfo supplierModel = null;
    //    System.Collections.ArrayList orderTypes = new System.Collections.ArrayList();//PR单下所有物料类别
    //    string[] zbProductTypes = ESP.Configuration.ConfigurationManager.SafeAppSettings["BJSingle"].Split(',');
    //    foreach (OrderInfo orderModel in orderList)
    //    {
    //        if (orderModel.supplierId > 0)
    //            supplierModel = SupplierManager.GetModel(orderModel.supplierId);
    //        orderTypes.Add(orderModel.producttype);
    //    }
    //    if (supplierModel != null)
    //    {
    //        if (GetLevel1DeparmentID(model.requestor) == State.filialeName_GZ)
    //        {
    //            if (supplierModel.supplier_area.IndexOf("广州") != -1)
    //                return true;
    //            else
    //                return false;
    //        }
    //        if (GetLevel1DeparmentID(model.requestor) == State.filialeName_SH)
    //        {
    //            if (supplierModel.supplier_area.IndexOf("上海") != -1)
    //                return true;
    //            else
    //                return false;
    //        }
    //        if (GetLevel1DeparmentID(model.requestor) == "总部")
    //        {
    //            if (supplierModel.supplier_area.IndexOf("北京") != -1)
    //                return true;
    //            else
    //                return false;
    //        }
    //    }
    //    return false;
    //}

    private string GetLevel1DeparmentID(int sysId)
    {
        IList<ESP.Compatible.Department> dtdep = ESP.Compatible.Employee.GetDepartments(sysId);
        string nodename = "";
        if (dtdep.Count > 0)
        {
            string level = dtdep[0].Level.ToString();
            if (level == "1")
            {
                nodename = dtdep[0].NodeName;
            }
            else if (level == "2")
            {
                ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                nodename = dep.Parent.DepartmentName;

            }
            else if (level == "3")
            {
                ESP.Compatible.Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dtdep[0].UniqID);
                ESP.Compatible.Department dep2 = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dep.Parent.UniqID);
                nodename = dep2.Parent.DepartmentName;

            }
        }
        return nodename;
    }

    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;

            Literal LitOverplusPrice = (Literal)e.Row.FindControl("LitOverplusPrice");
            dynamicPercent += decimal.Parse(dv["expectPaymentPercent"].ToString());
        }
    }
    public string GetExpectPaymentPrice(decimal expectPaymentPrice)
    {
        return expectPaymentPrice.ToString("#,##0.00");
    }
    #endregion

    /// <summary>
    /// 绑定列表信息
    /// </summary>
    public void bindInfo()
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        string strWhere = " and a.Gid = " + generalid;
        //strWhere += " and a.receivePrice = 0 and (isconfirm is null or isconfirm = 0 or isconfirm = " + State.recipentConfirm_Supplier + " or (isconfirm = " + State.recipentConfirm_Emp2 + " and (b.appendReceiver is null or b.appendReceiver=0)) or (isconfirm = " + State.recipentConfirm_Emp2 + " and b.source<>'协议供应商'))";
        
        strWhere += " and isconfirm = " + State.recipentConfirm_Supplier;

        DataSet ds = RecipientManager.GetRecipientList(strWhere, parms);
        gvSupplier.DataSource = ds;
        gvSupplier.DataBind();

        bindGvPaymentPeriodList();
    }

    /// <summary>
    /// Binds the gv payment period list.
    /// </summary>
    private void bindGvPaymentPeriodList()
    {
        string gvPaymentPeriodStrWhere = string.Format(" and inceptPrice>0 and a.gid = {0} and a.status in (" + (int)State.PaymentStatus_wait + "," + (int)State.PaymentStatus_commit + ")", generalid);
        DataSet gvPaymentPeriodDS = PaymentPeriodManager.GetGeneralPaymentList(gvPaymentPeriodStrWhere);
        gvPaymentPeriod.DataSource = gvPaymentPeriodDS;
        gvPaymentPeriod.DataBind();

        string logWhere = string.Format(" and inceptPrice>0 and a.gid = {0} and a.status={1}", generalid, State.PaymentStatus_commit);
        DataSet gvLogDS = PaymentPeriodManager.GetGeneralPaymentList(logWhere);
        gvLogList.DataSource = gvLogDS;
        gvLogList.DataBind();
    }

    #region GridView事件
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindInfo();
        ListBind();
    }

    protected void gvPaymentPeriod_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int periodId = int.Parse(e.CommandArgument.ToString());
            PaymentPeriodInfo model = PaymentPeriodManager.GetModel(periodId);

            if (ESP.Finance.BusinessLogic.ReturnManager.returnPaymentInfo(model.ReturnId))
            {
                ESP.Purchase.Entity.LogInfo logModel = new ESP.Purchase.Entity.LogInfo();
                logModel.Gid = model.gid;
                logModel.Des = CurrentUserName + "撤销付款申请[" + model.ReturnCode + "] " + DateTime.Now;
                logModel.LogUserId = CurrentUserID;
                logModel.LogMedifiedTeme = DateTime.Now;
                ESP.Purchase.BusinessLogic.LogManager.AddLog(logModel, Request);
                //ListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('撤销成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('撤销失败！');", true);
            }
            bindInfo();
            ListBind();
        }
        if (e.CommandName == "Submit")
        {
            btnUpdate_Click(new object(), new EventArgs());
            int periodId = int.Parse(e.CommandArgument.ToString());

            int returnId = 0;
            if (PaymentPeriodManager.CommitPeriod(periodId.ToString(), "0", CurrentUser.SysID, CurrentUserCode, CurrentUser.ITCode, CurrentUser.Name, ref returnId, isNeedPurchaseAudit(), IsMediaOrder))
            {
                //记录操作日志
               // ESP.Logging.Logger.Add(string.Format("{0}对T_PaymentPeriod表中的id={1}进行{2}操作", CurrentUser.Name, periodId, "提交付款申请"), "付款申请");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "if(confirm('付款申请单提交成功!是否要转向到财务系统?')) {window.top.location.href='" + getRedirectUrl(returnId) + "';}", true);
                getRedirectUrl(returnId);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交付款申请失败!');", true);
            }
            bindInfo();
            ListBind();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        GeneralInfo generalInfo = GeneralInfoManager.GetModel(generalid);
        generalInfo.account_bank = txtaccountBank.Text.Trim();
        generalInfo.account_name = txtaccountName.Text.Trim();
        generalInfo.account_number = txtaccountNum.Text.Trim();

        IsMediaOrder = generalInfo.IsMediaOrder;

        try
        {
            GeneralInfoManager.Update(generalInfo);
            //记录操作日志
          //  ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}进行{2}操作", CurrentUser.Name, generalInfo.id, "付款申请处更新账户"), "编辑");
        }
        catch
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('更新帐户信息失败！');", true);
        }
    }

    protected void gvPaymentPeriod_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            if (int.Parse(dv["returnStatus"].ToString()) != (int)ESP.Finance.Utility.PaymentStatus.Save)
            {
                //LinkButton linkEdit = (LinkButton)e.Row.FindControl("linkEdit");
                //LinkButton linkDel = (LinkButton)e.Row.FindControl("linkDel");
                //linkEdit.Visible = false;
                //linkDel.Visible = false;
                //LinkButton linkSubmit = (LinkButton)e.Row.FindControl("linkSubmit");
                //linkSubmit.Visible = false;
                e.Row.Cells[4].Controls.Clear();
                e.Row.Cells[5].Controls.Clear();
            }

            TextBox txtBegin = (TextBox)e.Row.FindControl("txtBegin");
            TextBox txtEnd = (TextBox)e.Row.FindControl("txtEnd");

            System.Web.UI.HtmlControls.HtmlImage img1 = (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("img1");
            System.Web.UI.HtmlControls.HtmlImage img2 = (System.Web.UI.HtmlControls.HtmlImage)e.Row.FindControl("img2");

            if (img1 != null)
                img1.Attributes["onclick"] = "popUpCalendar(document.getElementById('" + txtBegin.ClientID + "'), document.getElementById('" + txtBegin.ClientID + "'), 'yyyy-mm-dd');";
            if (img2 != null)
                img2.Attributes["onclick"] = "popUpCalendar(document.getElementById('" + txtEnd.ClientID + "'), document.getElementById('" + txtEnd.ClientID + "'), 'yyyy-mm-dd');";
        }
    }
    #endregion

    protected void btnPeriodSave_Click(object s, EventArgs e)
    {
        //
    }

    protected void btnPeriodCommit_Click(object s, EventArgs e)
    {
        //
    }

    protected void btnPeriodCancel_Click(object s, EventArgs e)
    {
        NotShow();
    }

    protected void Show()
    {
        btnCommit.Visible = false;
        btnBack.Visible = false;
    }

    /// <summary>
    /// Nots the show.
    /// </summary>
    protected void NotShow()
    {
        btnCommit.Visible = true;
        btnBack.Visible = true;
    }

    /// <summary>
    /// Gets the format price.
    /// </summary>
    /// <param name="price">The price.</param>
    /// <returns></returns>
    public string GetFormatPrice(decimal price)
    {
        return price.ToString("#,##0.00");
    }

    /// <summary>
    /// Gets the name of the status.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <returns></returns>
    public string GetStatusName(int status)
    {
        return State.Payment_Status[status];
    }

    /// <summary>
    /// Handles the RowEditing event of the gvPaymentPeriod control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvPaymentPeriod_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPaymentPeriod.EditIndex = e.NewEditIndex;
        bindGvPaymentPeriodList();
    }

    /// <summary>
    /// Handles the RowCancelingEdit event of the gvPaymentPeriod control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
    protected void gvPaymentPeriod_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPaymentPeriod.EditIndex = -1;
        bindGvPaymentPeriodList();
    }

    /// <summary>
    /// Handles the RowUpdating event of the gvPaymentPeriod control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
    protected void gvPaymentPeriod_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int periodId = int.Parse(gvPaymentPeriod.DataKeys[e.RowIndex].Value.ToString());
        PaymentPeriodInfo model = PaymentPeriodManager.GetModel(periodId);
        TextBox txtBegin = (TextBox)gvPaymentPeriod.Rows[e.RowIndex].FindControl("txtBegin");
        TextBox txtEnd = (TextBox)gvPaymentPeriod.Rows[e.RowIndex].FindControl("txtEnd");
        TextBox txtInceptPrice = (TextBox)gvPaymentPeriod.Rows[e.RowIndex].FindControl("txtInceptPrice");

        if (DateTime.Parse(txtBegin.Text.Trim()) < model.beginDate)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('预计支付起始时间必须晚于修改前');", true);
            return;
        }
        if (model.endDate != DateTime.Parse(State.datetime_minvalue) && (txtEnd.Text.Trim() != "" && DateTime.Parse(txtEnd.Text.Trim()) < model.endDate))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('预计支付结束时间必须晚于修改前');", true);
            return;
        }

        model.beginDate = DateTime.Parse(txtBegin.Text.Trim());
        if (txtEnd.Text.Trim() != "")
            model.endDate = DateTime.Parse(txtEnd.Text.Trim());
        else
            model.endDate = DateTime.Parse(State.datetime_minvalue);

        PaymentPeriodManager.Update(model);

        gvPaymentPeriod.EditIndex = -1;
        bindGvPaymentPeriodList();
    }


    /// <summary>
    /// Gets the redirect URL.
    /// </summary>
    /// <param name="returnId">The return id.</param>
    /// <returns></returns>
    private void getRedirectUrl(int returnId)
    {
        //int financeWebSiteID = ESP.Framework.BusinessLogic.SettingManager.Get().Items["FinanceWebSite"].GetValue<int>();
        //ESP.Framework.Entity.WebSiteInfo financeWebSite = ESP.Framework.BusinessLogic.WebSiteManager.Get(financeWebSiteID);
        //return string.Format(financeWebSite.HttpRootUrl + "/Default.aspx?contentUrl=/Purchase/ReturnEdit.aspx?ReturnID={0}", returnId);

        Response.Redirect("ReturnEdit.aspx?ReturnID=" + returnId);
    }

    /// <summary>
    /// Gets the submit URL.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="status">The status.</param>
    /// <returns></returns>
    public string GetSubmitUrl(int id, int status)
    {
        if (status == State.PaymentStatus_wait)
        {
            return "<input name=\"chkPeriod\" id=\"chkPeriod\" type=\"checkbox\" value='" + id + "' />";
        }
        return "";
    }
}
