using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_RecipientOkList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindInfo();
        }
    }

    /// <summary>
    /// 检查当前登陆人是否为采购部的人员
    /// </summary>
    private bool checkIsStockDeparmentUser()
    {
        int[] deparments = CurrentUser.GetDepartmentIDs();
        foreach (int uniqID in deparments)
        {
            if (uniqID == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["CQStockDeparmentUniqID"].ToString()))
                return true;
            if (uniqID == int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["StockDeparmentUniqID"].ToString()))
                return true;
        }
        return false;
    }

    /// <summary>
    /// 绑定列表信息
    /// </summary>
    public void bindInfo()
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        //协议供应商确认过或非协议供应商二次确认过的和
        //string strWhere = " and (a.isconfirm =1 or (a.isconfirm=2 and b.source!='协议供应商') or a.isconfirm=4 or a.isconfirm=3)";
        string strWhere = " and (a.isconfirm=1 or a.isconfirm=3  or a.isconfirm=4 or (a.isconfirm=2 and b.requisitionflow=" + State.requisitionflow_toR + "))";

        if (!checkIsStockDeparmentUser())
            strWhere += " and ( b.requestor=" + CurrentUser.SysID + " or b.goods_receiver=" + CurrentUser.SysID + " or b.first_assessor = " + CurrentUser.SysID + " or b.Filiale_Auditor = " + CurrentUser.SysID + ")";

        if (!string.IsNullOrEmpty(txtsupplier_name.Text.Trim()))
        {
            strWhere += " and supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplier_name.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(txtGlideNo.Text.Trim()))
        {
            int totalgno = 0;
            bool res = int.TryParse(txtGlideNo.Text, out totalgno);
            if (res)
            {
                strWhere += " and a.Gid = @Gid";
                parms.Add(new SqlParameter("@Gid", txtGlideNo.Text.Trim()));
            }
        }
        if (!string.IsNullOrEmpty(txtPrNo.Text.Trim()))
        {
            strWhere += " and b.orderid like '%'+@orderid+'%'";
            parms.Add(new SqlParameter("@orderid", txtPrNo.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(txtRNo.Text.Trim()))
        {
            strWhere += " and a.RecipientNo like '%'+@RecipientNo+'%'";
            parms.Add(new SqlParameter("@RecipientNo", txtRNo.Text.Trim()));
        }
        DataSet ds = RecipientManager.GetRecipientList(strWhere, parms);
        gvSupplier.DataSource = ds;
        gvSupplier.DataBind();

        if (gvSupplier.PageCount > 1)
        {
            PageBottom.Visible = true;
            PageTop.Visible = true;
        }
        else
        {
            PageBottom.Visible = false;
            PageTop.Visible = false;
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            tabTop.Visible = true;
            tabBottom.Visible = true;
        }
        else
        {
            tabTop.Visible = false;
            tabBottom.Visible = false;
        }

        labAllNum.Text = labAllNumT.Text = ds.Tables[0].Rows.Count.ToString();
        labPageCount.Text = labPageCountT.Text = (gvSupplier.PageIndex + 1).ToString() + "/" + gvSupplier.PageCount.ToString();
        if (gvSupplier.PageCount > 0)
        {
            if (gvSupplier.PageIndex + 1 == gvSupplier.PageCount)
                disButton("last");
            else if (gvSupplier.PageIndex == 0)
                disButton("first");
            else
                disButton("");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindInfo();
    }

    protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
            if (int.Parse(dv["isconfirm"].ToString()) == (int)ESP.Purchase.Common.State.recipentConfirm_Supplier || (Convert.ToInt32(dv["isconfirm"]) == (int)ESP.Purchase.Common.State.recipentConfirm_Emp2 && dv["source"].ToString().Trim() != "协议供应商") || PeriodRecipientManager.isJoinPeriod(int.Parse(dv["recipientId"].ToString())))
            {
            }
            else
            {
                e.Row.Cells[1].Controls.Clear();
                e.Row.Cells[1].Text = string.Empty;
            }
            if (int.Parse(dv["isconfirm"].ToString()) == (int)ESP.Purchase.Common.State.recipentConfirm_PaymentCommit || PeriodRecipientManager.isJoinPeriod(int.Parse(dv["recipientId"].ToString())) || (int.Parse(dv["prtype"].ToString()) == (int)PRTYpe.PrivatePR && ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(int.Parse(dv["GId"].ToString())) != null))
            {
                //如果已创建付款申请，则不能撤销
                btnCancel.Visible = false;
            }

            if (int.Parse(dv["inUse"] == DBNull.Value ? ((int)State.PRInUse.Use + "") : dv["inUse"].ToString()) != (int)State.PRInUse.Use)
            {
                btnCancel.Visible = false;
            }
        }
    }

    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Return")//撤销收货单
        {
            int recipientId = int.Parse(e.CommandArgument.ToString());
            ESP.Purchase.Entity.RecipientInfo model = RecipientManager.GetModel(recipientId);
            ESP.Purchase.Entity.GeneralInfo generalInfo = GeneralInfoManager.GetModel(model.Gid);
            if (generalInfo.InUse == (int)State.PRInUse.Use)
            {
                //给供应商发送撤销收货通知
                string url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/RecipientPrint.aspx?id=" + generalInfo.id + "&recipientId=" + recipientId;
                string body = "该收货单已被撤销！</br>" + ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
                ESP.ConfigCommon.SendMail.Send1("收货单撤销通知", generalInfo.supplier_email, body, false, new System.Collections.Hashtable());
                if (RecipientManager.Delete(recipientId, CurrentUser.Name, Request))
                {
                    //记录操作日志
                    //收货日志----begin
                    LogInfo log = new LogInfo();
                    log.Gid = model.Gid;
                    log.LogMedifiedTeme = DateTime.Now;
                    log.LogUserId = CurrentUserID;
                    log.Des = CurrentUserName + "(" + CurrentUser.ITCode + ")" + "撤销" + model.RecipientNo + "收货单 " + DateTime.Now.ToString();//string.Format(State.log_recipient, CurrentUserName, model.RecipientNo, DateTime.Now.ToString());
                    ESP.Framework.Entity.AuditBackUpInfo auditBackUp = ESP.Framework.BusinessLogic.AuditBackUpManager.GetLayOffModelByUserID(int.Parse(CurrentUser.SysID));
                    if (auditBackUp != null)
                        log.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + log.Des;
                    LogManager.AddLog(log, Request);

                    RecipientLogInfo Rlog = new RecipientLogInfo();
                    Rlog.Rid = recipientId;
                    Rlog.LogMedifiedTeme = DateTime.Now;
                    Rlog.LogUserId = CurrentUserID;
                    Rlog.Des = CurrentUserName + "(" + CurrentUser.ITCode + ")" + "撤销" + model.RecipientNo + "收货单 " + DateTime.Now.ToString(); //string.Format(State.log_recipient, CurrentUserName, model.RecipientNo, DateTime.Now.ToString());
                    if (auditBackUp != null)
                        Rlog.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + Rlog.Des;
                    RecipientLogManager.AddLog(Rlog, Request);
                    ESP.Logging.Logger.Add(string.Format("{0}对T_Recipient表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, recipientId, "撤销收货单"), "收货单");
                    bindInfo();
                    ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('收货单撤销成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('收货单撤销失败！');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ESP.Purchase.Common.State.DisabledMessageForPR + "');window.location='OrderCommitList.aspx'", true);
            }
        }
    }

    protected void gvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSupplier.PageIndex = e.NewPageIndex;
        bindInfo();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        Paging(gvSupplier.PageCount);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Paging(0);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Paging((gvSupplier.PageIndex + 1) > gvSupplier.PageCount ? gvSupplier.PageCount : (gvSupplier.PageIndex + 1));
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Paging((gvSupplier.PageIndex - 1) < 0 ? 0 : (gvSupplier.PageIndex - 1));
    }

    /// <summary>
    /// 翻页
    /// </summary>
    /// <param name="pageIndex">页码</param>
    private void Paging(int pageIndex)
    {
        GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
        gvSupplier_PageIndexChanging(new object(), e);
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
