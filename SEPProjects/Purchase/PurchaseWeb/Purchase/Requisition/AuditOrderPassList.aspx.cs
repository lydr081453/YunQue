using System;
using System.Linq;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Data;

public partial class Purchase_Requisition_AuditOrderPassList : ESP.Web.UI.PageBase
{
    const int PAGESIZE = 20;//每页显示信息数量
    int PagesCount, RecordsCount;//记录总页数和信息总条数
    int CurrentPage, Pages;//当前页，信息总页数(用来控制按钮失效)，跳转页码

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] state = State.requistionOrderValue2_state;
            string[] statevalue = State.requistionOrderValue2_state2;
            for (int i = 0; i < state.Length; i++)
            {
                ddlState.Items.Add(new ListItem(state[i].ToString(), statevalue[i].ToString()));
            }
            ddlState.Items.Insert(0, new ListItem("请选择", "-1"));
            TypeBind();



            ListBind();

            PagesCount = RecordsCount / PAGESIZE + OverPage();//默认的页总数
            ViewState["PagesCount"] = RecordsCount / PAGESIZE - ModPage();//保存末页索引，比页总数小1
            ViewState["PageIndex"] = CurrentPage;//保存页面初始索引从0开始


            labAllNum.Text = labAllNumT.Text = RecordsCount.ToString();
            labPageCount.Text = labPageCountT.Text = (int.Parse(ViewState["PageIndex"].ToString()) + 1).ToString() + "/" + ViewState["PagesCount"];

            disButton();
        }
    }

    /// <summary>
    /// 绑定申请单流向
    /// </summary>
    private void TypeBind()
    {
        ddlRequisitionflow.Items.Add(new ListItem("全部", "-1"));
        ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
        ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toR], State.requisitionflow_toR.ToString()));
        ddlRequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));
    }

    /// <summary>
    /// 绑定列表
    /// </summary>
    private void ListBind()
    {
        List<SqlParameter> parms = new List<SqlParameter>();
        string term = string.Empty;

        term = " and (status not in(-1,0,2,4)) ";

        if (ddlState.SelectedValue != "-1")
        {
            term += " and status=@status2";
            parms.Add(new SqlParameter("@status2", ddlState.SelectedValue));
        }

        if (ddlRequisitionflow.SelectedValue != "-1")
        {
            term += " and requisitionflow = @requisitionflow ";
            parms.Add(new SqlParameter("@requisitionflow", ddlRequisitionflow.SelectedValue));
        }

        if (txtPrNo.Text.Trim() != "")
        {
            term += " and prNo like '%'+@prNo+'%'";
            parms.Add(new SqlParameter("@prNo", txtPrNo.Text.Trim()));
        }
        if (txtProjectCode.Text.Trim() != "")
        {
            term += " and project_code like '%'+@projectcode+'%'";
            parms.Add(new SqlParameter("@projectcode", txtProjectCode.Text.Trim()));
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
        if (txtProductType.Text.Trim() != "")
        {
            term += @" and a.id in (select distinct a.general_id from t_orderinfo as a 
                        inner join t_type as b on b.typeid=a.producttype
                        where b.typelevel=3 and b.typename like '%'+@typename+'%')";
            parms.Add(new SqlParameter("@typename", txtProductType.Text.Trim()));
        }

        string admindepts = System.Configuration.ConfigurationManager.AppSettings["MaxSearchPR"];

        if (admindepts.IndexOf(","+CurrentUser.GetDepartmentIDs()[0].ToString()+",") < 0)
        {
            List<ESP.Framework.Entity.OperationAuditManageInfo> deplist = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelList("depid<>0 and (DirectorId=" + CurrentUserID + " OR ManagerId=" + CurrentUserID + " OR CEOId=" + CurrentUserID + " OR RiskControlAccounter=" + CurrentUserID + " OR purchaseDirectorId=" + CurrentUserID + ")");
            if (deplist.Count > 0)
            {
                string depts = string.Empty;
                foreach (ESP.Framework.Entity.OperationAuditManageInfo op in deplist)
                {
                    depts += op.DepId.ToString() + ",";
                }
                depts = depts.TrimEnd(',');
                term += " and (departmentid in(" + depts + ") or (a.id in(select GId from T_auditLog where auditUserId= " + CurrentUserID + ")))";
            }
            else
            {
                term += " and a.id in(select GId from T_auditLog where auditUserId= " + CurrentUserID + ")";
            }
        }


        DataTable dtRecord = GeneralPageManager.GetRecordsCount(term, parms);//默认信息总数
        RecordsCount = int.Parse(dtRecord.Rows[0][0].ToString());

        if (string.IsNullOrEmpty(dtRecord.Rows[0][1].ToString()))
        {
            labTotal.Text = "0.00";
            labTotalT.Text = "0.00";
        }
        else
        {
            labTotal.Text = decimal.Parse(dtRecord.Rows[0][1].ToString()).ToString("#,##0.00");
            labTotalT.Text = decimal.Parse(dtRecord.Rows[0][1].ToString()).ToString("#,##0.00");
        }
        List<GeneralInfo> list = GeneralPageManager.GetModelListPage(PAGESIZE, CurrentPage, term, parms);
        list.Sort(new ESP.Purchase.BusinessLogic.GeneralInfoCompareAudit());
        gvG.DataSource = list;
        gvG.DataBind();





    }

    public int OverPage()
    {
        int pages = 0;
        if (RecordsCount % PAGESIZE != 0)
            pages = 1;
        else
            pages = 0;
        return pages;
    }


    public int ModPage()
    {
        int pages = 0;
        if (RecordsCount % PAGESIZE == 0 && RecordsCount != 0)
            pages = 1;
        else
            pages = 0;
        return pages;
    }

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = int.Parse(gvG.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text.Trim());
        GeneralInfo g = GeneralInfoManager.GetModel(id);
        if (g.InUse == (int)State.PRInUse.Use)
        {
            if (e.CommandName == "SendMail")
            {
                SendMail(g);

                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对PR单中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, id.ToString(), "发送PO单至供应商处"), "已审批通过PR");
            }
            if (e.CommandName == "HandConfirm")
            {
                HandConfirm(g);
                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对PR单中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, id.ToString(), "手动确认"), "已审批通过PR");
            }
            if (e.CommandName == "Return")
            {
                string extraMessage = string.Empty;
                if (null != g)
                {
                    ESP.ITIL.BusinessLogic.申请单业务设置.采购撤销订单(CurrentUser, ref g);
                    GeneralInfoManager.Update(g);

                    LogInfo log = new LogInfo();
                    log.Gid = g.id;
                    log.LogMedifiedTeme = DateTime.Now;
                    log.LogUserId = CurrentUserID;

                    log.Des = string.Format(State.log_requisition_cancelByauditor, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
                    LogManager.AddLog(log, Request);
                    //记录操作日志
                    ESP.Logging.Logger.Add(string.Format("{0}对PR单中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, id.ToString(), "撤销"), "已审批通过PR");
                    ESP.Purchase.Entity.SupplierSendInfo sendModel = ESP.Purchase.BusinessLogic.SupplierSendManager.GetModel(g.id, (int)State.DataType.PR);
                    if (sendModel != null)
                    {
                        //给供应商发送撤销通知
                        string url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/OrderPrint.aspx?id=" + id + "&showBottom=false";
                        string body = "该订单" + g.orderid + "已被撤销！</br>" + ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
                        ESP.ConfigCommon.SendMail.Send1("订单撤销通知", sendModel.Email, body, false, new Hashtable());
                        ESP.Purchase.BusinessLogic.SupplierSendManager.Delete(g.id, (int)State.DataType.PR);
                    }
                    else
                    {
                        extraMessage = "无法发送PO撤销邮件通知,";
                    }
                }
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + extraMessage + "申请单审批操作撤销成功!');window.location='AuditOrderPassList.aspx'", true);

            }
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ESP.Purchase.Common.State.DisabledMessageForPR + "');window.location='OrderCommitList.aspx'", true);
        }
    }

    /// <summary>
    /// 手动确认
    /// </summary>
    /// <param name="generalid"></param>
    protected void HandConfirm(GeneralInfo generalInfo)
    {
        string Msg1 = ESP.ITIL.BusinessLogic.申请单业务设置.采购确认订单(CurrentUser, ref generalInfo);
        if (Msg1 != "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg1 + "');", true);
            return;
        }
        GeneralInfoManager.Update(generalInfo);

        LogInfo log = new LogInfo();
        log.Gid = generalInfo.id;
        log.LogMedifiedTeme = DateTime.Now;
        log.LogUserId = CurrentUserID;
        log.Des = string.Format(State.log_confrim, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
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
        ClientScript.RegisterStartupScript(typeof(string), "", "window.location.href='AuditOrderPassList.aspx';alert('已完成供应商确认！"+exMail+"');", true);
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
        string clause2 = "";
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
            clause2 = Server.MapPath("~") + "ExcelTemplate\\" + branchModel.POStandard;

            FileHelper.DeleteFile(htmlFilePath);
            FileHelper.SaveFile(htmlFilePath, body);
            List<OrderInfo> orders = OrderInfoManager.GetListByGeneralId(generalInfo.id);
            Hashtable attFiles = new Hashtable();
            attFiles.Add(branchModel.POTerm, clause);
            attFiles.Add(branchModel.POStandard, clause2);
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

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GeneralInfo model = (GeneralInfo)e.Row.DataItem;
            
            ESP.Framework.Entity.OperationAuditManageInfo operationModel = null;

            if (model.Project_id != 0)
            {
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByProjectId(model.Project_id);
            }
            if (operationModel == null)
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(model.requestor); ;

            if (operationModel == null)
                operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(model.Departmentid);

            Label labState = ((Label)e.Row.FindControl("labState"));
            Panel printpo = (Panel)e.Row.FindControl("printpo");    // 打印订单
            LinkButton lnkSend = (LinkButton)e.Row.FindControl("lnkSend");
            HyperLink hypAuditurl = (HyperLink)e.Row.FindControl("hypView");
            HyperLink hypNoView = (HyperLink)e.Row.FindControl("hypNoView");
            LinkButton lnkHankConfirm = (LinkButton)e.Row.FindControl("lnkHankConfirm");

            Label lblItemNo = (Label)e.Row.FindControl("lblItemNo");
            Label lblProductTypeName = (Label)e.Row.FindControl("lblProductTypeName");


            lblItemNo.Text = model.itemno + "&nbsp;Total:￥" + model.totalprice.ToString("#,##0.00");


            if (null != hypAuditurl)
            {
                hypAuditurl.NavigateUrl = "OrderDetailTab.aspx?backUrl=AuditOrderPassList.aspx&" + RequestName.GeneralID + "=" + e.Row.Cells[0].Text.ToString() + "&helpfile=3";
                hypNoView.NavigateUrl = "OrderDetailTab.aspx?backUrl=AuditOrderPassList.aspx&" + RequestName.GeneralID + "=" + e.Row.Cells[0].Text.ToString() + "&helpfile=3";
            }

            if (null != labState)
            {
                if (model.status == State.order_ok || model.status == State.order_sended)
                {
                    if (lnkSend != null)
                    {
                        lnkSend.CommandArgument = e.Row.RowIndex.ToString();
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


                if ( model.status == State.order_sended )
                {
                    lnkHankConfirm.Visible = true;
                     if (lnkHankConfirm != null)
                        lnkHankConfirm.CommandArgument = e.Row.RowIndex.ToString();
                }
                else
                {
                   lnkHankConfirm.Visible = false;
                }

                labState.Text = State.requistionOrorder_state[int.Parse(labState.Text)].ToString();

            }
            //1.	订单已发出对方未确认   由初审人撤销至已审核状态
            //2.	采购总监已批尚未发出   由采购总监撤销至已审核状态
            LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
            btnCancel.CommandArgument = e.Row.RowIndex.ToString();

            if (model.first_assessor != CurrentUserID && model.Filiale_Auditor != CurrentUserID && (operationModel != null && CurrentUserID.ToString() != operationModel.PurchaseDirectorId.ToString() && CurrentUserID.ToString() != operationModel.PurchaseAuditorId.ToString()))
            {
                // if (model.first_assessor == CurrentUserID && model.status == State.order_sended)
                btnCancel.Visible = false;
            }
            else
            {
                if (model.status == State.order_ok || model.status == State.order_confirm || model.status == State.order_sended || model.status == State.requisition_recipiented)
                {
                    if (RecipientManager.getModelList(" and gid=" + model.id, new List<SqlParameter>()).Count == 0)//未收过货的才能撤销
                        btnCancel.Visible = true;
                }
            }
            //如果还有PN则不能撤销
            if (ESP.Finance.BusinessLogic.ReturnManager.GetList("PrId=" + model.id).Count > 0)
            {
                btnCancel.Visible = false;
            }
            
            if (model.InUse != (int)State.PRInUse.Use)
            {
                btnCancel.Visible = false;
                lnkSend.Visible = false;
                lnkHankConfirm.Visible = false;
            }

            Label labRequisitionflow = (Label)e.Row.FindControl("labRequisitionflow");
            if (null != labRequisitionflow && labRequisitionflow.Text != "")
            {
                if (labRequisitionflow.Text != State.requisitionflow_toO.ToString())
                {
                    if (lnkSend != null)
                        lnkSend.Visible = false;
                    if (printpo != null)
                        printpo.Visible = false;
                }
                labRequisitionflow.Text = State.requisitionflow_state[int.Parse(labRequisitionflow.Text)];
            }
        }
    }

    protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // gvG.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
        PagesCount = RecordsCount / PAGESIZE + OverPage();//默认的页总数
        ViewState["PagesCount"] = RecordsCount / PAGESIZE - ModPage();//保存末页索引，比页总数小1
        ViewState["PageIndex"] = CurrentPage;//保存页面初始索引从0开始

        labAllNum.Text = labAllNumT.Text = RecordsCount.ToString();
        labPageCount.Text = labPageCountT.Text = (int.Parse(ViewState["PageIndex"].ToString()) + 1).ToString() + "/" + ViewState["PagesCount"];

        disButton();
    }

    #region 分页
    protected void btnLast_Click(object sender, EventArgs e)
    {
        CurrentPage = (int)ViewState["PagesCount"];
        ViewState["PageIndex"] = CurrentPage;
        ListBind();
        labPageCount.Text = labPageCountT.Text = (int.Parse(ViewState["PageIndex"].ToString())).ToString() + "/" + ViewState["PagesCount"];
        disButton();

    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        ViewState["PageIndex"] = 0;
        ListBind();
        labPageCount.Text = labPageCountT.Text = (int.Parse(ViewState["PageIndex"].ToString()) + 1).ToString() + "/" + ViewState["PagesCount"];
        disButton();

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        CurrentPage = int.Parse(ViewState["PageIndex"].ToString());
        CurrentPage++;
        ViewState["PageIndex"] = CurrentPage;
        ListBind();
        labPageCount.Text = labPageCountT.Text = (int.Parse(ViewState["PageIndex"].ToString()) + 1).ToString() + "/" + ViewState["PagesCount"];
        disButton();

    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage = int.Parse(ViewState["PageIndex"].ToString());
        CurrentPage--;
        ViewState["PageIndex"] = CurrentPage;
        ListBind();
        labPageCount.Text = labPageCountT.Text = (int.Parse(ViewState["PageIndex"].ToString()) + 1).ToString() + "/" + ViewState["PagesCount"];
        disButton();

    }



    private void disButton()
    {
        CurrentPage = (int)ViewState["PageIndex"];
        Pages = (int)ViewState["PagesCount"];

        if (CurrentPage + 1 > 1)//当前页是否为首页
        {
            btnFirst.Enabled = true;
            btnFirst2.Enabled = true;
            btnPrevious.Enabled = true;
            btnPrevious2.Enabled = true;
        }
        else
        {
            btnFirst.Enabled = false;
            btnFirst2.Enabled = false;
            btnPrevious.Enabled = false;
            btnPrevious2.Enabled = false;
        }
        if (CurrentPage == Pages)//当前页是否为尾页
        {
            btnNext.Enabled = false;
            btnNext2.Enabled = false;
            btnLast.Enabled = false;
            btnLast2.Enabled = false;
        }
        else
        {
            btnNext.Enabled = true;
            btnNext2.Enabled = true;
            btnLast.Enabled = true;
            btnLast2.Enabled = true;
        }
    }

    #endregion

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

}
