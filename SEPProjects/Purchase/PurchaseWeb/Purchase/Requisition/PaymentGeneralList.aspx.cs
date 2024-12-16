using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_PaymentGeneralList : ESP.Web.UI.PageBase
{

    static bool isZBStockDeparment = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
    }

    private void ListBind()
    {
        isZBStockDeparment = false;
        SetDeparmentValue();

        List<SqlParameter> parms = new List<SqlParameter>();
        string term = string.Empty;

        if (txtsupplierName.Text.Trim() != "")
        {
            term += " and supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
        }

        if (txtPrNo.Text.Trim() != "")
        {
            term += " and prNo like '%'+@prNo+'%'";
            parms.Add(new SqlParameter("@prNo", txtPrNo.Text.Trim()));
        }
        if (txtGlideNo.Text.Trim() != "")
        {
            int totalgno = 0;
            bool res = int.TryParse(txtGlideNo.Text, out totalgno);
            if (res)
            {
                term += " and a.id = @id";
                parms.Add(new SqlParameter("@id", txtGlideNo.Text.TrimStart('0')));
            }
        }
        if (txtOrderNo.Text.Trim() != "")
        {
            term += " and orderid like '%'+@orderid+'%'";
            parms.Add(new SqlParameter("@orderid", txtOrderNo.Text.Trim()));
        }
        if (txtAudit.Text.Trim() != "")
        {
            term += " and first_assessorname like '%'+@first_assessorname+'%'";
            parms.Add(new SqlParameter("@first_assessorname", txtAudit.Text.Trim()));
        }
        if (txtgoods_receiver.Text.Trim() != "")
        {
            term += " and receivername like '%'+@receivername+'%'";
            parms.Add(new SqlParameter("@receivername", txtgoods_receiver.Text.Trim()));
        }
        if (txtFilAuditor.Text.Trim() != "")
        {
            term += " and Filiale_AuditName like '%'+@FilAuditor+'%'";
            parms.Add(new SqlParameter("@FilAuditor", txtFilAuditor.Text.Trim()));
        }

        if (!isZBStockDeparment)
        {
            term += " and requestor=" + CurrentUser.SysID;
        }
        List<GeneralInfo> list = GeneralInfoManager.GetPaymentGeneralList(term, parms);
        gvG.DataSource = list;
        gvG.DataBind();

        if (gvG.PageCount > 1)
        {
            PageBottom.Visible = true;
            PageTop.Visible = true;
        }
        else
        {
            PageBottom.Visible = false;
            PageTop.Visible = false;
        }
        if (list.Count > 0)
        {
            tabTop.Visible = true;
            tabBottom.Visible = true;
        }
        else
        {
            tabTop.Visible = false;
            tabBottom.Visible = false;
        }

        labAllNum.Text = labAllNumT.Text = list.Count.ToString();
        labPageCount.Text = labPageCountT.Text = (gvG.PageIndex + 1).ToString() + "/" + gvG.PageCount.ToString();
        if (gvG.PageCount > 0)
        {
            if (gvG.PageIndex + 1 == gvG.PageCount)
                disButton("last");
            else if (gvG.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }
    }

    private string getRedirectUrl(int gid)
    {
        int financeWebSiteID = ESP.Framework.BusinessLogic.SettingManager.Get().Items["FinanceWebSite"].GetValue<int>();
        ESP.Framework.Entity.WebSiteInfo financeWebSite = ESP.Framework.BusinessLogic.WebSiteManager.Get(financeWebSiteID);
        return string.Format(financeWebSite.HttpRootUrl + "/Default.aspx?contentUrl=/ForeGift/addForegift.aspx?{0}={1}", RequestName.GeneralID, gid);
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GeneralInfo model = (GeneralInfo)e.Row.DataItem;
            HyperLink hyl = (HyperLink)e.Row.FindControl("hyl");
            hyl.NavigateUrl = "PaymentRecipientList.aspx?backUrl=PaymentGeneralList.aspx&" + RequestName.GeneralID + "=" + model.id;
            HyperLink hylForeGift = (HyperLink)e.Row.FindControl("hylForeGift");
            hylForeGift.Attributes["onclick"] = "window.top.location='" + getRedirectUrl(model.id) + "';";
            //采购部的人员只能勾选各自公司的申请人提交的协议供应商的单子
            //普通申请人可以勾选任何人提交的非协议供应商的单子
            //eddy 没有限制
            if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower())
            {
                if (model.requestor != int.Parse(CurrentUser.SysID) || model.totalprice == 0)
                {
                    hyl.Visible = false;
                }

            }

            if (model.InUse != (int)State.PRInUse.Use)
            {
                hyl.Visible = false;
            }
            if (model.Foregift > 0 && ESP.Finance.BusinessLogic.ReturnManager.isHaveForeGift(model.id))
            {
                hylForeGift.Visible = false;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    /// <summary>
    /// 检查当前登陆人是否为采购部的人员
    /// </summary>
    private bool checkIsStockDeparmentUser()
    {
        int[] deparments = CurrentUser.GetDepartmentIDs();
        foreach (int uniqID in deparments)
        {
            if (uniqID == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["StockDeparmentUniqID"].ToString()))
                return true;

        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetDeparmentValue()
    {
        if (checkIsStockDeparmentUser())
            isZBStockDeparment = true;
    }

    protected string CreatePN(int paymentPeriodId, int recipientId)
    {
        PaymentPeriodInfo paymentPeriod = PaymentPeriodManager.GetModel(paymentPeriodId);
        GeneralInfo generalinfoModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(paymentPeriod.gid);

        //收货单列表
        List<RecipientInfo> recipientList = RecipientManager.getModelList(" and id=" + recipientId, new List<SqlParameter>());

        decimal recipientPrice = 0; //收货金额
        foreach (RecipientInfo model in recipientList)
        {
            recipientPrice += model.RecipientAmount;
        }
        paymentPeriod.inceptPrice = paymentPeriod.expectPaymentPrice;
        recipientPrice = paymentPeriod.expectPaymentPrice;
        paymentPeriod.inceptDate = DateTime.Now;
        paymentPeriod.Status = State.PaymentStatus_wait;

        //付款金额设置
        //检查是否是金额不符收货的付款
        if (RecipientManager.IsUnsureRecipient(recipientId.ToString()))
        {
            paymentPeriod.inceptPrice = RecipientManager.getRecipientSum(paymentPeriod.gid) - PaymentPeriodManager.getPaymentSum(paymentPeriod.gid);
        }
        else
        {
            if (recipientPrice > paymentPeriod.expectPaymentPrice) //如果所选收货金额大于账期金额，只收账期金额
            {
                paymentPeriod.inceptPrice = paymentPeriod.expectPaymentPrice;

            }
            else //如果所选收货金额小于账期金额，只收收货金额
            {
                paymentPeriod.inceptPrice = recipientPrice;
            }
        }

        //更新付款账期和收货单
        if (!PaymentPeriodManager.UpdatePaymentPeriodAndRecipient(paymentPeriod, recipientId.ToString()))
            return "";//ShowCompleteMessage("确认失败！", "PaymentRecipientList.aspx?" + Request.Url);
        else
        {
            //记录操作日志
            //ESP.Logging.Logger.Add(string.Format("{0}对T_PaymentPeriod表中的id={1}和T_Recipient表中的id={2}进行{3}操作", CurrentUser.Name, paymentPeriod.id, recipientId.ToString(), "关联"), "创建付款申请");
        }
        int periodId = paymentPeriodId;

        int returnId = 0;
        if (PaymentPeriodManager.CommitPeriod(periodId.ToString(), "0", CurrentUser.SysID, CurrentUserCode, CurrentUser.ITCode, CurrentUser.Name, ref returnId, false, generalinfoModel.IsMediaOrder))
        {
            //记录操作日志
            //ESP.Logging.Logger.Add(string.Format("{0}对T_PaymentPeriod表中的id={1}进行{2}操作", CurrentUser.Name, periodId, "提交付款申请"), "付款申请");
        }
        return ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId).ReturnCode;
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvG.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvG.PageIndex + 1) > gvG.PageCount ? gvG.PageCount : (gvG.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvG.PageIndex - 1) < 0 ? 0 : (gvG.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvG_PageIndexChanging(new object(), e);
    }

    protected void ExportToOrderInfoExcel(int id)
    {
        FileHelper.ToOrderInfoExcel(id, Server.MapPath("~"), Response);
        GC.Collect();
    }

    protected void ExportToGeneralInfoExcel(int id)
    {
        FileHelper.ToGeneralInfoExcel(id, Server.MapPath("~"), Response);
        GC.Collect();
    }
    protected void ExportRequisition(int id)
    {
        FileHelper.ExportRequisition(id, Server.MapPath("~"), Response);
        GC.Collect();
    }
    private void disButton(string page)
    {
        switch (page)
        {
            case "first":
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnFirst2.Enabled = false;
                btnPrevious2.Enabled = false;
                btnNext2.Enabled = true;
                btnLast2.Enabled = true;
                break;
            case "last":
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = false;
                btnLast.Enabled = false;

                btnFirst2.Enabled = true;
                btnPrevious2.Enabled = true;
                btnNext2.Enabled = false;
                btnLast2.Enabled = false;
                break;
            default:
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnFirst2.Enabled = true;
                btnPrevious2.Enabled = true;
                btnNext2.Enabled = true;
                btnLast2.Enabled = true;
                break;
        }
    }
}
