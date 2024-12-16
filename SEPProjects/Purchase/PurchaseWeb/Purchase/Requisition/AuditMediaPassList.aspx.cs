using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class AuditMediaPassList : ESP.Web.UI.PageBase
    {
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
            //6是对私，有媒体合作对私的单子，所以加入该状态
            term = " and (status = @status or status = @status_sended  or status = @confirm or status = @recipiented or status=@order_mediaAuditYes) and prtype in (1,4,6,7,8)";
            parms.Add(new SqlParameter("@status", State.order_ok));
            parms.Add(new SqlParameter("@status_sended", State.order_sended));
            parms.Add(new SqlParameter("@confirm", State.order_confirm));
            parms.Add(new SqlParameter("@recipiented", State.requisition_recipiented));
            parms.Add(new SqlParameter("@order_mediaAuditYes", State.order_mediaAuditYes));

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
            if (txtFiliale_AuditName.Text.Trim() != "")
            {
                term += " and Filiale_AuditName like '%'+@Filiale_AuditName+'%'";
                parms.Add(new SqlParameter("@Filiale_AuditName", txtFiliale_AuditName.Text.Trim()));
            }
            if (txtProductType.Text.Trim() != "")
            {
                term += @" and a.id in (select distinct a.general_id from t_orderinfo as a 
                        inner join t_type as b on b.typeid=a.producttype
                        where b.typelevel=3 and b.typename like '%'+@typename+'%')";
                parms.Add(new SqlParameter("@typename", txtProductType.Text.Trim()));
            }


            List<GeneralInfo> list = GeneralInfoManager.GetStatusList(term, parms);
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
                    if (null != g)
                    {
                        //if (CurrentUser.EMail.ToLower() == ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorEmail"].ToString().ToLower())
                        //    g.isMajordomoUndo = true;

                        ////设置媒介审批人
                        //ESP.Framework.Entity.EmployeeInfo mediaAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["mediaAuditorId"]));

                        ////设置媒体广告采买审批人
                        //ESP.Framework.Entity.EmployeeInfo adAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(ESP.Configuration.ConfigurationManager.SafeAppSettings["ADAuditorId"]));

                        //switch (g.PRType)
                        //{
                        //    case (int)PRTYpe.CommonPR:
                        //    case (int)PRTYpe.PrivatePR:
                        //    case (int)PRTYpe.PR_TMP2:
                        //        g.status = State.order_commit;
                        //        break;

                        //    case (int)PRTYpe.MPPR:
                        //    case (int)PRTYpe.PR_MediaFA:
                        //    case (int)PRTYpe.PR_PriFA:
                        //    case (int)PRTYpe.PR_TMP1:
                        //        g.status = State.order_mediaAuditWait;
                        //        g.mediaAuditor = mediaAuditor.UserID;
                        //        g.mediaAuditorName = mediaAuditor.Username;
                        //        break;
                        //    case (int)PRTYpe.ADPR:
                        //        g.status = State.order_ADAuditWait;
                        //        g.adAuditor = adAuditor.UserID;
                        //        g.adAuditorName = adAuditor.Username;
                        //        break;
                        //    default:
                        //        g.status = State.order_commit;
                        //        break;
                        //}
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
                    }
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('申请单撤销成功!');window.location='AuditMediaPassList.aspx'", true);

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
            generalInfo.status = State.order_confirm;
            GeneralInfoManager.Update(generalInfo);

            LogInfo log = new LogInfo();
            log.Gid = generalInfo.id;
            log.LogMedifiedTeme = DateTime.Now;
            log.LogUserId = CurrentUserID;
            log.Des = string.Format(State.log_confrim, CurrentUserName + "(" + CurrentUser.ITCode + ")", DateTime.Now.ToString());
            LogManager.AddLog(log, Request);

            string auditEmail = State.getEmployeeEmailBySysUserId(generalInfo.Filiale_Auditor == 0 ? generalInfo.first_assessor : generalInfo.Filiale_Auditor);
            string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPOConfirm(generalInfo, generalInfo.orderid, State.getEmployeeEmailBySysUserId(generalInfo.requestor), State.getEmployeeEmailBySysUserId(generalInfo.goods_receiver), auditEmail);
            ListBind();
            ClientScript.RegisterStartupScript(typeof(string), "", "window.location.href='AuditMediaPassList.aspx';alert('已完成供应商确认！');", true);
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
                url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1) + "Print/OrderPrint.aspx?id=" + generalInfo.id + "&mail=yes";
                body = ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
                ESP.Finance.Entity.BranchInfo branchModel = ESP.Finance.BusinessLogic.BranchManager.GetModelByCode(generalInfo.project_code.Substring(0, 1));

                htmlFilePath = Server.MapPath("~") + "ExcelTemplate\\" + "订单" + generalInfo.orderid + ".htm";
                clause = Server.MapPath("~") + "ExcelTemplate\\" + branchModel.POTerm;

                FileHelper.DeleteFile(htmlFilePath);
                FileHelper.SaveFile(htmlFilePath, body);
                List<OrderInfo> orders = OrderInfoManager.GetListByGeneralId(generalInfo.id);
                Hashtable attFiles = new Hashtable();
                attFiles.Add(branchModel.POTerm, clause);
                attFiles.Add("", htmlFilePath);
                if (generalInfo.sow2.Trim() != "")
                {
                    attFiles.Add("工作描述" + generalInfo.sow2.Substring(generalInfo.sow2.IndexOf(".")), ESP.Purchase.Common.ServiceURL.UpFilePath + generalInfo.sow2);
                }

                int filecount = 1;
                foreach (OrderInfo model in orders)
                {
                    if (model.upfile.Trim() != "")
                    {
                        attFiles.Add("采购物品报价" + filecount.ToString() + model.upfile.Substring(model.upfile.IndexOf(".")), ESP.Purchase.Common.ServiceURL.UpFilePath + model.upfile);
                        filecount++;
                    }
                }
                string supplierEmail = hidSupplierEmail.Value == "" ? generalInfo.supplier_email : hidSupplierEmail.Value;
                //修改状态
                generalInfo.status = State.order_sended;
                generalInfo.supplier_email = supplierEmail;
                GeneralInfoManager.Update(generalInfo);
                hidSupplierEmail.Value = "";

                string ret = ESP.Purchase.BusinessLogic.SendMailHelper.SendMailPO(generalInfo, generalInfo.orderid, State.getEmployeeEmailBySysUserId(generalInfo.requestor), supplierEmail, body, attFiles);
                
            }
            catch (ESP.ConfigCommon.MailException ex)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.ToString() + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
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
                //var operationModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(model.requestor);
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
                if (null != hypAuditurl)
                {
                    hypAuditurl.NavigateUrl = "OrderDetail.aspx?backUrl=AuditMediaPassList.aspx&" + RequestName.GeneralID + "=" + e.Row.Cells[0].Text.ToString() + "&helpfile=3";
                    hypNoView.NavigateUrl = "OrderDetail.aspx?backUrl=AuditMediaPassList.aspx&" + RequestName.GeneralID + "=" + e.Row.Cells[0].Text.ToString() + "&helpfile=3";
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


                    if (model.status == State.requisition_recipiented || model.status == State.order_confirm)
                    {
                        lnkHankConfirm.Visible = false;
                    }
                    else
                    {
                        if (lnkHankConfirm != null)
                            lnkHankConfirm.CommandArgument = e.Row.RowIndex.ToString();
                    }

                    labState.Text = State.requistionOrorder_state[int.Parse(labState.Text)].ToString();

                }
                //1.	订单已发出对方未确认   由初审人撤销至已审核状态
                //2.	采购总监已批尚未发出   由采购总监撤销至已审核状态
                LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
                btnCancel.CommandArgument = e.Row.RowIndex.ToString();
                //if (CurrentUser.EMail.ToLower() != ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorEmail"].ToString().ToLower())
                if (CurrentUserID.ToString() != operationModel.PurchaseAuditorId.ToString() && CurrentUserID.ToString() != operationModel.PurchaseDirectorId.ToString())
                {
                   // if (model.first_assessor == CurrentUserID && model.status == State.order_sended)
                        btnCancel.Visible = false;
                }
                else
                {
                    if (model.status == State.order_ok || model.status == State.order_confirm || model.status == State.requisition_recipiented || model.status == State.order_mediaAuditYes)
                    {
                        if (RecipientManager.getModelList(" and gid=" + model.id, new List<SqlParameter>()).Count == 0)//未收过货的才能撤销
                            btnCancel.Visible = true;
                    }
                }
                //如果是媒体稿费报销单，对私且李艳娥已经处理，采购部无权撤销
                if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                {
                    ESP.Purchase.Entity.MediaPREditHisInfo relationModel = ESP.Purchase.BusinessLogic.MediaPREditHisManager.GetModelByOldPRID(model.id);
                    if (relationModel != null)
                    {
                        btnCancel.Visible = false;
                    }
                }

                if (model.InUse != (int)State.PRInUse.Use)
                {
                    btnCancel.Visible = false;
                    lnkSend.Visible = false;
                    lnkHankConfirm.Visible = false;
                }

                Repeater repProductType = (Repeater)e.Row.FindControl("repProductType");
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
                repProduct.DataSource = list;
                repProduct.DataBind();

                repProductType.DataSource = list;
                repProductType.DataBind();



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
                //对私3000以上和媒体3000以上，不显示初审人
                if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.ADPR || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_PriFA || model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                {
                    e.Row.Cells[8].Controls.Clear();
                    e.Row.Cells[7].Controls.Clear();
                    e.Row.Cells[8].Text="";
                    e.Row.Cells[7].Text="";
                }
                //如果是对私的PR单且是媒体合作和媒体采买，初审人也屏蔽掉
                int typeoperationflow = OrderInfoManager.getTypeOperationFlow(((GeneralInfo)e.Row.DataItem).id);
                if (model.PRType == (int)ESP.Purchase.Common.PRTYpe.PrivatePR)
                {
                    if (typeoperationflow == State.typeoperationflow_MP)
                    {
                        e.Row.Cells[7].Controls.Clear();
                        e.Row.Cells[8].Controls.Clear();
                        e.Row.Cells[7].Text = "";
                        e.Row.Cells[8].Text = "";
                    }
                }
            }
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

        #region 分页
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
}
