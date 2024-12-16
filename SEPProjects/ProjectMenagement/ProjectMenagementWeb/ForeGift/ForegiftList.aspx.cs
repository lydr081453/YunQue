using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;

public partial class ForeGift_ForegiftList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindList();
                bindRePayList();
            }
        }

        private void bindList()
        {
            string delegateusers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                delegateusers += model.UserID.ToString() + ",";
            }
            delegateusers = delegateusers.TrimEnd(',');

            string term = string.Empty;
            if (!string.IsNullOrEmpty(delegateusers))
            {
                term = " (returnID in(select returnid from F_ReturnAuditHist where AuditorUserID=@currentUserId  or AuditorUserID in(" + delegateusers + ")) or (RequestorID=@currentUserId or RequestorID in(" + delegateusers + "))) ";
            }
            else
            {
                term = " (returnID in(select returnid from F_ReturnAuditHist where AuditorUserID=@currentUserId) or (RequestorID=@currentUserId)) ";
            }
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
            puserid.SqlValue = int.Parse(CurrentUser.SysID);
            paramlist.Add(puserid);

            term += " and returnType=@returnType";
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@returnType", ESP.Purchase.Common.PRTYpe.PN_ForeGift));
            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (prno like '%'+@prno+'%' or projectcode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
                System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                p1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(p1);
            }
            if (this.ddlStatus.SelectedIndex != 0)
            {
                term += " and returnStatus=@returnStatus";
                System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@returnStatus", System.Data.SqlDbType.Int, 4);
                p2.SqlValue = this.ddlStatus.SelectedValue;
                paramlist.Add(p2);
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                {
                    term += " and RequestDate between @beginDate and @endDate";
                    System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                    p3.SqlValue = this.txtBeginDate.Text;
                    paramlist.Add(p3);
                    System.Data.SqlClient.SqlParameter p4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                    p4.SqlValue = this.txtEndDate.Text;
                    paramlist.Add(p4);

                }
            }
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
            this.gvG.DataSource = returnlist;
            this.gvG.DataBind();
        }
        private void bindRePayList()
        {
            string delegateusers = string.Empty;
            IList<ESP.Framework.Entity.AuditBackUpInfo> delegates = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(Convert.ToInt32(CurrentUser.SysID));
            foreach (ESP.Framework.Entity.AuditBackUpInfo model in delegates)
            {
                delegateusers += model.UserID.ToString() + ",";
            }
            delegateusers = delegateusers.TrimEnd(',');

            string term = string.Empty;
            if (!string.IsNullOrEmpty(delegateusers))
            {
                term = " (returnID in(select returnid from F_ReturnAuditHist where AuditorUserID=@currentUserId  or AuditorUserID in(" + delegateusers + ")) or (RequestorID=@currentUserId or RequestorID in(" + delegateusers + "))) ";
            }
            else
            {
                term = " (returnID in(select returnid from F_ReturnAuditHist where AuditorUserID=@currentUserId) or (RequestorID=@currentUserId)) ";
            }
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            System.Data.SqlClient.SqlParameter puserid = new System.Data.SqlClient.SqlParameter("@currentUserId", System.Data.SqlDbType.Int, 4);
            puserid.SqlValue = int.Parse(CurrentUser.SysID);
            paramlist.Add(puserid);
            term += " and returnType=@returnType";
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@returnType", ESP.Purchase.Common.PRTYpe.PN_ForeGift));
            if (this.txtKey.Text.Trim() != string.Empty)
            {
                term += "  and (prno like '%'+@prno+'%' or projectcode like '%'+@prno+'%' or returncode like '%'+@prno+'%' or prid like '%'+@prno+'%'  or prefee  like '%'+@prno+'%' or RequestEmployeeName  like '%'+@prno+'%' or SupplierName  like '%'+@prno+'%' )";
                System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@prno", System.Data.SqlDbType.NVarChar, 50);
                p1.SqlValue = this.txtKey.Text.Trim();
                paramlist.Add(p1);
            }
            term += " and returnStatus=@returnStatus";
            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@returnStatus", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = (int)ESP.Finance.Utility.PaymentStatus.FinanceReject;
            paramlist.Add(p2);
            if (!string.IsNullOrEmpty(txtBeginDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
                {
                    term += " and RequestDate between @beginDate and @endDate";
                    System.Data.SqlClient.SqlParameter p3 = new System.Data.SqlClient.SqlParameter("@beginDate", System.Data.SqlDbType.DateTime, 8);
                    p3.SqlValue = this.txtBeginDate.Text;
                    paramlist.Add(p3);
                    System.Data.SqlClient.SqlParameter p4 = new System.Data.SqlClient.SqlParameter("@endDate", System.Data.SqlDbType.DateTime, 8);
                    p4.SqlValue = this.txtEndDate.Text;
                    paramlist.Add(p4);

                }
            }
            IList<ESP.Finance.Entity.ReturnInfo> returnlist = ESP.Finance.BusinessLogic.ReturnManager.GetList(term, paramlist);
            this.gvRePayment.DataSource = returnlist;
            this.gvRePayment.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindList();
            bindRePayList();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            this.txtKey.Text = string.Empty;
            this.ddlStatus.SelectedIndex = 0;
            this.txtBeginDate.Text = string.Empty;
            this.txtEndDate.Text = string.Empty;
            bindList();
            bindRePayList();
        }

        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            bindList();
        }


        protected void gvRePayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRePayment.PageIndex = e.NewPageIndex;
            bindRePayList();
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;

                Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
                if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                    labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
                Label lblPR = (Label)e.Row.FindControl("lblPR");
                lblPR.Text = model.PRNo;
                Label lblBeginDate = (Label)e.Row.FindControl("lblBeginDate");
                if (lblBeginDate != null && lblBeginDate.Text != string.Empty)
                    lblBeginDate.Text = Convert.ToDateTime(lblBeginDate.Text).ToString("yyyy-MM-dd");
                Label lblEndDate = (Label)e.Row.FindControl("lblEndDate");
                if (lblEndDate != null && lblEndDate.Text != string.Empty)
                    lblEndDate.Text = Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd");

                Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
                HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
                if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                    lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, model.IsDiscount);
                LinkButton lnkAttach = (LinkButton)e.Row.FindControl("lnkAttach");
                if (lnkAttach != null)
                {
                    if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
                    {
                        lnkAttach.Text = "<img src='/images/PrintDefault.gif' title='导出Excel' border='0'>";
                    }
                    else
                    {
                        lnkAttach.Text = "";
                    }
                }
                Label lblAttach = (Label)e.Row.FindControl("lblAttach");
                if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
                {
                    lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\MediaPrint.aspx?OrderID=" + model.MediaOrderIDs + "'style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
                }
                else
                {
                    lblAttach.Text = "";
                }

                Label lblName = (Label)e.Row.FindControl("lblName");

                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");
                LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                if (hylEdit != null)
                {
                    if (model.ReturnStatus == (int)PaymentStatus.Save)
                    {
                        hylEdit.NavigateUrl = "addForegift.aspx?" + RequestName.ReturnID + "=" + e.Row.Cells[0].Text;
                    }
                    else
                    {
                        hylEdit.Visible = false;
                        btnCancel.Visible = false;//只可以撤销保存状态的数据
                    }
                }
                if (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR )
                {
                    //如果经过媒介和个人处理的3000一下付款申请，不可以撤销
                    btnCancel.Visible = false;
                }
                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                if (hylPrint != null)
                {
                    hylPrint.Target = "_blank";
                    hylPrint.NavigateUrl = "/Purchase/Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + e.Row.Cells[0].Text;
                }
                //如果申请单暂停，不能编辑付款申请
                if (model.PRID != null && model.PRID.Value != 0)
                {
                    if (ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.PRID.Value).InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
                    {
                        btnCancel.Visible = false;
                        hylEdit.Visible = false;
                    }
                }
            }
        }

        protected void gvRePayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.ReturnInfo model = (ESP.Finance.Entity.ReturnInfo)e.Row.DataItem;

                Label labExpectPaymentPrice = (Label)e.Row.FindControl("labExpectPaymentPrice");
                if (labExpectPaymentPrice != null && labExpectPaymentPrice.Text != string.Empty)
                    labExpectPaymentPrice.Text = Convert.ToDecimal(labExpectPaymentPrice.Text).ToString("#,##0.00");
                Label lblPR = (Label)e.Row.FindControl("lblPR");
                if (model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.MediaPR && model.ReturnType != (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA)
                    lblPR.Text = "<a href='" + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseServer"] + "Purchase\\Requisition\\OrderDetailTab.aspx?GeneralID=" + model.PRID.ToString() + "'style='cursor: hand' target='_blank'>" + model.PRNo + "</a>";
                else
                    lblPR.Text = model.PRNo;
                Label lblBeginDate = (Label)e.Row.FindControl("lblBeginDate");
                if (lblBeginDate != null && lblBeginDate.Text != string.Empty)
                    lblBeginDate.Text = Convert.ToDateTime(lblBeginDate.Text).ToString("yyyy-MM-dd");
                Label lblEndDate = (Label)e.Row.FindControl("lblEndDate");
                if (lblEndDate != null && lblEndDate.Text != string.Empty)
                    lblEndDate.Text = Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd");

                Label lblStatusName = (Label)e.Row.FindControl("lblStatusName");
                HiddenField hidStatusNameID = (HiddenField)e.Row.FindControl("hidStatusNameID");
                if (lblStatusName != null && hidStatusNameID != null && hidStatusNameID.Value != string.Empty)
                    lblStatusName.Text = ReturnPaymentType.ReturnStatusString(Convert.ToInt32(hidStatusNameID.Value), 0, model.IsDiscount);
                LinkButton lnkAttach = (LinkButton)e.Row.FindControl("lnkAttach");
                if (lnkAttach != null)
                {
                    if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
                    {
                        lnkAttach.Text = "<img src='/images/PrintDefault.gif' title='导出Excel' border='0'>";
                    }
                    else
                    {
                        lnkAttach.Text = "";
                    }
                }
                Label lblAttach = (Label)e.Row.FindControl("lblAttach");
                if (!string.IsNullOrEmpty(model.MediaOrderIDs) && (model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.MediaPR || model.ReturnType == (int)ESP.Purchase.Common.PRTYpe.PR_MediaFA))
                {
                    lblAttach.Text = "<a href='" + ESP.Finance.Configuration.ConfigurationManager.PurchaseServer + "Purchase\\Requisition\\Print\\MediaPrint.aspx?OrderID=" + model.MediaOrderIDs + "'style='cursor: hand' target='_blank'> <img title='附件' src='/images/PrintDefault.gif' border='0px;' /></a>";
                }
                else
                {
                    lblAttach.Text = "";
                }
                Label lblName = (Label)e.Row.FindControl("lblName");

                lblName.Attributes.Add("onclick", "javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(model.RequestorID.Value) + "');");

                HyperLink hylEdit = (HyperLink)e.Row.FindControl("hylEdit");
                if (hylEdit != null)
                {
                    if (model.ReturnStatus == (int)PaymentStatus.FinanceReject)
                        hylEdit.NavigateUrl = "ReturnBankCancel.aspx?" + RequestName.ReturnID + "=" + e.Row.Cells[0].Text;
                    else
                        hylEdit.Visible = false;
                }
                HyperLink hylPrint = (HyperLink)e.Row.FindControl("hylPrint");
                if (hylPrint != null)
                {
                    hylPrint.Target = "_blank";
                    hylPrint.NavigateUrl = "Print/PaymantPrint.aspx?" + RequestName.ReturnID + "=" + e.Row.Cells[0].Text;
                }
                //如果申请单暂停，不能编辑付款申请
                if (model.PRID != null && model.PRID.Value != 0)
                {
                    if (ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(model.PRID.Value).InUse != (int)ESP.Purchase.Common.State.PRInUse.Use)
                    {
                        hylEdit.Visible = false;
                    }
                }
            }
        }
        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                //删除押金付款
                int returnID = int.Parse(e.CommandArgument.ToString());
                if (ESP.Finance.BusinessLogic.ReturnManager.Delete(returnID) == DeleteResult.Succeed)
                {
                    bindList();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
                }
            }

            if (e.CommandName == "Export")
            {
                int returnID = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
                if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
                {
                    string filename;
                    string serverpath = Server.MapPath(ESP.Finance.Configuration.ConfigurationManager.MediaOrderPath);
                    ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(CurrentUserID, returnModel, serverpath, out filename,false);
                    if (!string.IsNullOrEmpty(filename))
                    {
                        outExcel(serverpath + filename, filename, true);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        protected void gvRePayment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Export")
            {
                int returnID = int.Parse(e.CommandArgument.ToString());
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnID);
                if (!string.IsNullOrEmpty(returnModel.MediaOrderIDs))
                {
                    string filename;
                    string serverpath = ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceMainPage"].Replace("Default.aspx", "");
                    ESP.Finance.BusinessLogic.ReturnManager.ExportMediaOrderExcel(CurrentUserID, returnModel, serverpath, out filename, false);
                    if (!string.IsNullOrEmpty(filename))
                    {
                        outExcel(serverpath + filename, filename, true);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        private void outExcel(string pathandname, string filename, bool isDelete)
        {
            if (!File.Exists(pathandname))
                return;
            FileStream fin = File.OpenRead(pathandname);
            Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);
            Response.AddHeader("Connection", "Close");
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Length", fin.Length.ToString());

            byte[] buf = new byte[1024];
            while (true)
            {
                int length = fin.Read(buf, 0, buf.Length);
                if (length > 0)
                    Response.OutputStream.Write(buf, 0, length);
                if (length < buf.Length)
                    break;
            }
            fin.Close();
            Response.Flush();
            Response.Close();
            if (isDelete)
            {
                FileInfo finfo = new FileInfo(pathandname);
                finfo.Delete();
            }
        }
    }
