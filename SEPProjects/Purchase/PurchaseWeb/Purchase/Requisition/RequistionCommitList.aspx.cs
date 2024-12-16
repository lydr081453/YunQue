using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;

public partial class Purchase_Requisition_RequistionCommitList : ESP.Web.UI.PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] state = State.requistionOrorderValue1_state;
            string[] statevalue = State.requistionOrorderValue_state;
            for (int i = 0; i < state.Length; i++)
            {
                ddlState.Items.Add(new ListItem(state[i].ToString(), statevalue[i].ToString()));
            }
            ddlState.Items.Insert(0, new ListItem("请选择", "-1"));

            ddlRequisitionflow.Items.Add(new ListItem("全部", "-1"));
            ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
            ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toR], State.requisitionflow_toR.ToString()));
            ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));


            ListBind();
        }
    }

    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();

        string term = " and status not in (@status,@status1,@status2,@status3)";
        parms.Add(new SqlParameter("@status", State.requisition_save));
        parms.Add(new SqlParameter("@status1", State.requisition_return));
        parms.Add(new SqlParameter("@status2", State.requisition_del));
        parms.Add(new SqlParameter("@status3", State.order_return));
        //采购审批角色的人可以看所有单据
        if (ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorName"].ToString().ToLower() != CurrentUser.ITCode.ToLower())
        {
            term += " and (requestor = @requestor or enduser = @requestor or goods_receiver = @requestor or Filiale_Auditor=@requestor or  first_assessor=@requestor or purchaseAuditor=@requestor or departmentid in(select depid from sep_operationauditmanage where directorId =@requestor or managerId=@requestor))";
            parms.Add(new SqlParameter("@requestor", CurrentUser.SysID));
        }
        if (ddlState.SelectedValue != "-1")
        {
            term += " and status=@status3";
            parms.Add(new SqlParameter("@status3", ddlState.SelectedValue));
        }

        if (ddlRequisitionflow.SelectedValue != "-1")
        {
            term += " and requisitionflow = @requisitionflow ";
            parms.Add(new SqlParameter("@requisitionflow", ddlRequisitionflow.SelectedValue));
        }
        if (txtProjectCode.Text.Trim() != "")
        {
            term += " and project_code like '%'+@projectcode+'%'";
            parms.Add(new SqlParameter("@projectcode", txtProjectCode.Text.Trim()));
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
        if (txtsupplierName.Text.Trim() != "")
        {
            term += " and supplier_name like '%'+@suppliername+'%'";
            parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
        }
        if (txtAudit.Text.Trim() != "")
        {
            term += " and first_assessorname like '%'+@first_assessorname+'%'";
            parms.Add(new SqlParameter("@first_assessorname", txtAudit.Text.Trim()));
        }
        if (txtRequestor.Text.Trim() != "")
        {
            term += " and requestorname like '%'+@requestorname+'%'";
            parms.Add(new SqlParameter("@requestorname", txtRequestor.Text.Trim()));
        }
        if (txtTotalMin.Text.Trim() != "")
        {
            int totalmin = 0;
            bool res = int.TryParse(txtTotalMin.Text, out totalmin);
            if (res)
            {
                term += " and b.totalprice >=@totalmin";
                parms.Add(new SqlParameter("@totalmin", txtTotalMin.Text.Trim()));
            }

        }

        if (txtTotalMax.Text.Trim() != "")
        {
            int totalmax = 0;
            bool res = int.TryParse(txtTotalMax.Text, out totalmax);
            if (res)
            {
                term += " and b.totalprice <= @totalmax";
                parms.Add(new SqlParameter("@totalmax", txtTotalMax.Text.Trim()));
            }
        }
        if (txtBegin.Text.Trim() != "")
        {
            term += " and order_audittime >=CONVERT(datetime , @begin, 120 )";
            parms.Add(new SqlParameter("@begin", txtBegin.Text.Trim()));
        }
        if (txtEnd.Text.Trim() != "")
        {
            term += " and order_audittime <= dateadd(d,1,CONVERT(datetime , @end, 120 ))";
            parms.Add(new SqlParameter("@end", txtEnd.Text.Trim()));
        }

        List<GeneralInfo> list = GeneralInfoManager.GetRequisitionCommitList(term, parms);
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

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string gid = e.CommandArgument.ToString();
        GeneralInfo g = GeneralInfoManager.GetModel(int.Parse(gid));
        if (g.InUse == (int)State.PRInUse.Use)
        {
            if (e.CommandName == "SendMail")
            {
                SendMail(g);

                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对PR单中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, g.id.ToString(), "发送PO单至供应商处"), "已审批通过PR");
            }
            else if (e.CommandName == "Return")
            {
                if (null != g && (((g.status == State.order_commit || g.status == State.requisition_commit || g.status == State.order_confirm || g.status == State.order_ok || g.status == State.order_sended) && g.ValueLevel == 1) || g.status == State.requisition_commit || g.status == State.order_commit || g.status == State.order_return || g.status == State.requisition_operationAduit || g.status == State.order_mediaAuditWait || g.status == State.order_ADAuditWait))
                {
                    IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(" prid =" + g.id.ToString());
                    if (returnlist == null || returnlist.Count == 0)
                    {
                        WorkFlowDAO.ProcessInstanceDao dao = new ProcessInstanceDao();
                        dao.TerminateProcess(g.ProcessID, g.InstanceID);

                        ESP.ITIL.BusinessLogic.申请单业务设置.申请人撤销PR(CurrentUser, ref g);
                        //GeneralInfoManager.Update(g);

                        LogInfo log = new LogInfo();
                        log.Gid = g.id;
                        log.LogMedifiedTeme = DateTime.Now;
                        log.LogUserId = CurrentUserID;

                        log.Des = string.Format(State.log_requisition_cancel, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
                        ESP.Framework.Entity.AuditBackUpInfo auditBackUp = ESP.Framework.BusinessLogic.AuditBackUpManager.GetLayOffModelByUserID(int.Parse(CurrentUser.SysID));
                        if (auditBackUp != null)
                            log.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + log.Des;
                        //LogManager.AddLog(log, Request);

                        //$$$$$ PR撤销
#if debug
                    System.Diagnostics.Debug.WriteLine("PR撤销");
                    Trace.Write("PR撤销");
#endif
                        GeneralInfoManager.UpdateAndAddLog(g, log, null, Request, ESP.Purchase.Common.State.PR_CostRecordsType.PR单撤销);
                        //clear pre log
                        LogManager.CancelByGid(g.id);
                        //记录操作日志
                        ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, g.id, "撤销采购申请"), "撤销采购申请");
                        ClientScript.RegisterStartupScript(typeof(string), "", "alert('申请单已撤回到编辑中心！');window.location='RequistionCommitList.aspx'", true);
                    }
                }

            }
            else
            {
                if (e.CommandName == "HandConfirm")
                {
                    HandConfirm(g);
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ESP.Purchase.Common.State.DisabledMessageForPR + "');window.location='OrderCommitList.aspx'", true);
        }
    }
    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labState = ((Label)e.Row.FindControl("labState"));
            LinkButton lnkSend = (LinkButton)e.Row.FindControl("lnkSend");
            LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
            Panel printpo = (Panel)e.Row.FindControl("printpo");    // 打印订单
            Label labOrderid = (Label)e.Row.FindControl("labOrderid");    // 订单号
            GeneralInfo model = (GeneralInfo)e.Row.DataItem;
            HyperLink hypAuditurl = (HyperLink)e.Row.FindControl("hypView");
            HyperLink hypNoView = (HyperLink)e.Row.FindControl("hypNoView");

            ESP.Framework.Entity.OperationAuditManageInfo operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.Departmentid);

            if (null != hypAuditurl)
            {
                hypAuditurl.NavigateUrl = "OrderDetailTab.aspx?" + RequestName.GeneralID + "=" + model.id + "&" + RequestName.BackUrl + "=RequistionCommitList.aspx&helpfile=3";
                hypNoView.NavigateUrl = "OrderDetailTab.aspx?" + RequestName.GeneralID + "=" + model.id + "&" + RequestName.BackUrl + "=RequistionCommitList.aspx&helpfile=3";
            }

            if (CurrentUserID != operationModel.PurchaseAuditorId && CurrentUserID != operationModel.PurchaseDirectorId)
            {
                e.Row.Cells[12].Controls.Clear();
                e.Row.Cells[12].Text = "";
            }


            // 状态为申请单提交、申请单审核通过、业务审核、 申请单审批驳回、待媒介审批、待AD审批状态时撤销按钮就显示，否则不显示
            if (((labState.Text == State.order_confirm.ToString() || labState.Text == State.order_sended.ToString() || labState.Text == State.order_ok.ToString() || labState.Text == State.order_commit.ToString() || labState.Text == State.requisition_commit.ToString()) && ((GeneralInfo)e.Row.DataItem).ValueLevel == 1) || labState.Text == State.requisition_commit.ToString() || labState.Text == State.order_commit.ToString() || labState.Text == State.order_return.ToString() || labState.Text == State.requisition_operationAduit.ToString() || labState.Text == State.order_mediaAuditWait.ToString() || labState.Text == State.order_ADAuditWait.ToString())
            {
                if (model.requestor.ToString() == CurrentUser.SysID)
                {
                    if (null != btnCancel)
                        btnCancel.Visible = true;
                }
                else
                    btnCancel.Visible = false;
            }
            else
            {
                if (null != btnCancel)
                    btnCancel.Visible = false;
            }


            if (!string.IsNullOrEmpty(labOrderid.Text))    // 有订单号的时候显示打印订单连接，否则不显示
            {
                if (null != printpo)
                {
                    printpo.Visible = true;
                    if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_OtherAdvertisement)
                    {
                        printpo.Controls.Clear();
                        Label lblPO = new Label();
                        lblPO.Text = "<a href='Print/ADOrder.aspx?id=" + model.id.ToString() + "' target=\"_blank\"><img title=\"打印订单\" src=\"../../images/Icon_PrintPo.gif\" border='0' /></a>";
                        printpo.Controls.Add(lblPO);

                    }
                }
            }
            else
            {
                if (null != printpo)
                    printpo.Visible = false;
            }

            labState.Text = State.requistionOrorder_state[int.Parse(labState.Text)].ToString();

            LinkButton lnkHankConfirm = (LinkButton)e.Row.FindControl("lnkHankConfirm");
            if ((model.status == State.order_ok || model.status == State.order_sended))
            {
                //if (lnkHankConfirm != null)
                //    lnkHankConfirm.CommandArgument = e.Row.RowIndex.ToString();

            }
            else
            {
                lnkHankConfirm.Visible = false;
            }

            if ((model.status == State.order_ok || model.status == State.order_sended))
            {
                if (lnkSend != null)
                {
                    lnkSend.OnClientClick = "var returnValue = confirmEmail('" + model.supplier_email + "');if(returnValue == 2){return true;}else if (returnValue == 1){alert('供应商邮件不能为空！');return false;}else{return false;}";
                    if (model.status == State.order_sended)
                    {
                        lnkSend.OnClientClick = "var returnValue = confirmEamil1('" + model.supplier_email + "');if(returnValue == 2){return true;}else if (returnValue == 1){alert('供应商邮件不能为空！');return false;}else{return false;}";
                    }
                }
            }
            else
            {
                lnkSend.Text = "";
            }

            if (model.InUse != (int)State.PRInUse.Use)
            {
                lnkHankConfirm.Visible = false;
                btnCancel.Visible = false;
            }
            Label labRequisitionflow = (Label)e.Row.FindControl("labRequisitionflow");
            if (null != labRequisitionflow && labRequisitionflow.Text != "")
            {
                labRequisitionflow.Text = State.requisitionflow_state[int.Parse(labRequisitionflow.Text)];
            }

            Repeater repProduct = (Repeater)e.Row.FindControl("repProduct");
            List<OrderInfo> list = OrderInfoManager.GetListByGeneralId(((GeneralInfo)e.Row.DataItem).id);
            string mtype = "";
            if (gvG.DataKeys[e.Row.RowIndex].Values[1].ToString() == "美元")
                mtype = "＄";
            else
                mtype = "￥";

            for (int i = 0; i < list.Count; i++)
            {
                list[i].moneytype = mtype + list[i].total.ToString("#,##0.####");
            }

            Literal litOperation = (Literal)e.Row.FindControl("litOperation");
            litOperation.Text = "<a href='' onclick='openAuditOperation(\"" + model.id + "\");return false;'><img title='审核列表' src='/images/dc.gif' border='0' /></a>";


            repProduct.DataSource = list;
            repProduct.DataBind();
            //对私3000以上和媒体3000以上，不显示初审人
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MPPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.ADPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA || model.PRType == (int)PRTYpe.MPPR)
            {
                e.Row.Cells[7].Controls.Clear();
                e.Row.Cells[6].Controls.Clear();
                e.Row.Cells[7].Text = "";
                e.Row.Cells[6].Text = "";
            }
            //如果是对私的PR单且是媒体合作和媒体采买，初审人也屏蔽掉
            int typeoperationflow = OrderInfoManager.getTypeOperationFlow(((GeneralInfo)e.Row.DataItem).id);
            if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
            {
                if (typeoperationflow == State.typeoperationflow_MP)
                {
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[6].Controls.Clear();
                    e.Row.Cells[7].Text = "";
                    e.Row.Cells[6].Text = "";
                }
            }


        }
    }

    /// <summary>
    /// 手动确认
    /// </summary>
    /// <param name="generalid"></param>
    protected void HandConfirm(GeneralInfo generalInfo)
    {
        string Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.申请人手动确认PR(CurrentUser, ref generalInfo);
        if (Msg1 != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg1 + "');", true);
            return;
        }
        GeneralInfoManager.Update(generalInfo);
        //记录操作日志
        ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, generalInfo.id, "手动确认申请单"), "手动确认申请单");

        LogInfo log = new LogInfo();
        log.Gid = generalInfo.id;
        log.LogMedifiedTeme = DateTime.Now;
        log.LogUserId = CurrentUserID;
        log.Des = string.Format(State.log_confrim, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
        ESP.Framework.Entity.AuditBackUpInfo auditBackUp = ESP.Framework.BusinessLogic.AuditBackUpManager.GetLayOffModelByUserID(int.Parse(CurrentUser.SysID));
        if (auditBackUp != null)
            log.Des = ESP.Framework.BusinessLogic.EmployeeManager.Get(auditBackUp.BackupUserID).FullNameCN + "代替" + log.Des;
        LogManager.AddLog(log, Request);

        string exMail = string.Empty;
        string auditEmail = State.getEmployeeEmailBySysUserId(generalInfo.Filiale_Auditor == 0 ? generalInfo.first_assessor : generalInfo.Filiale_Auditor);
        ListBind();
        try
        {
            string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPOConfirm(generalInfo, generalInfo.orderid, State.getEmployeeEmailBySysUserId(generalInfo.requestor), State.getEmployeeEmailBySysUserId(generalInfo.goods_receiver), auditEmail);
        }
        catch
        {
            exMail = "(邮件发送失败!)";
        }
        ClientScript.RegisterStartupScript(typeof(string), "", "window.location.href='RequistionCommitList.aspx';alert('已完成供应商确认！"+exMail+"');", true);
    }



    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="generalid"></param>
    protected void SendMail(GeneralInfo generalInfo)
    {
        string htmlFilePath = "";
        string url = "";
        string body = "";
        string clause = "";
        try
        {
            if (generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_OtherAdvertisement)
            {
                url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/ADOrder.aspx?id=" + generalInfo.id + "&mail=yes";
                body = ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
            }
            else
            {
                url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/OrderPrint.aspx?id=" + generalInfo.id + "&mail=yes";
                body = ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
            }
            ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0, 1));

            htmlFilePath = Server.MapPath("~") + "ExcelTemplate\\" + "订单" + generalInfo.orderid + ".htm";
            clause = Server.MapPath("~") + "ExcelTemplate\\" + branchModel.POTerm;

            FileHelper.DeleteFile(htmlFilePath);
            FileHelper.SaveFile(htmlFilePath, body);
            List<OrderInfo> orders = OrderInfoManager.GetListByGeneralId(generalInfo.id);
            Hashtable attFiles = new Hashtable();
            attFiles.Add(branchModel.POTerm, clause);
            attFiles.Add("", htmlFilePath);
            //李彦娥处理产生的新PR单不需要发工作描述
            if ((generalInfo.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA && generalInfo.PRType != (int)ESP.Purchase.Common.PRTYpe.PR_PriFA) && generalInfo.sow2.Trim() != "")
            {
                attFiles.Add("工作描述" + generalInfo.sow2.Substring(generalInfo.sow2.IndexOf(".")), ESP.Purchase.Common.ServiceURL.UpFilePath + generalInfo.sow2);
            }

            int filecount = 1;
            foreach (OrderInfo model in orders)
            {
                string[] upfiles = model.upfile.TrimEnd('#').Split('#');
                foreach (string upfile in upfiles)
                {
                    if (upfile.Trim() != "")
                    {
                        if (generalInfo.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_OtherAdvertisement)
                        {
                            //attFiles.Add("采购物品报价" + filecount.ToString() + upfile.Trim().Substring(model.upfile.IndexOf(".")), ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("default.aspx", "") + upfile.Trim());
                        }
                        else
                        {
                            attFiles.Add("采购物品报价" + filecount.ToString() + upfile.Trim().Substring(upfile.IndexOf(".")), ESP.Purchase.Common.ServiceURL.UpFilePath + upfile.Trim());
                        }
                        filecount++;
                    }
                }
            }
            string supplierEmail = hidSupplierEmail.Value == "" ? generalInfo.supplier_email : hidSupplierEmail.Value;
            string Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.供应商邮件发送(CurrentUser, ref generalInfo);
            if (Msg1 != "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg1 + "');", true);
                return;
            }
            
            generalInfo.supplier_email = supplierEmail;
            GeneralInfoManager.Update(generalInfo);
            hidSupplierEmail.Value = "";

            string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPO(generalInfo, generalInfo.orderid, State.getEmployeeEmailBySysUserId(generalInfo.requestor), supplierEmail, body, attFiles);

        }
        catch (ESP.ConfigCommon.MailException ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.ToString() + "');", true);
            return;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.ToString() + "');", true);
            return;
        }

        //重新绑定列表
        ListBind();
        ClientScript.RegisterStartupScript(typeof(string), "", "alert('发送成功！');", true);
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvG.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
