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

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class PaymentNotify : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ListBind();
        }

        private void ListBind()
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string term = "";
            if (CurrentUserID.ToString() != ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"].ToString())
            {
                term += @"and case when patindex(supplier_area,'北京') > 0 then b.bjpaymentuserid
	                        when patindex(supplier_area,'上海') > 0 then b.shpaymentuserid
	                        when patindex(supplier_area,'广州') > 0 then b.gzpaymentuserid
                            end  =" + CurrentUser.SysID;
            }

            if (txtPrNo.Text.Trim() != "")
            {
                term += " and a.prNo like '%'+@prNo+'%'";
                parms.Add(new SqlParameter("@prNo", txtPrNo.Text.Trim()));
            }
            if (txtGlideNo.Text.Trim() != "")
            {
                int totalgno = 0;
                bool res = int.TryParse(txtGlideNo.Text, out totalgno);
                if (res)
                {
                    term += " and a.prid = @id";
                    parms.Add(new SqlParameter("@id", txtGlideNo.Text.TrimStart('0')));
                }
            }
            if (txtProjectCode.Text.Trim() != "")
            {
                term += " and a.projectCode like '%'+@projectcode+'%'";
                parms.Add(new SqlParameter("@projectcode", txtProjectCode.Text.Trim()));
            }
            if (txtsupplierName.Text.Trim() != "")
            {
                term += " and b.supplierName like '%'+@suppliername+'%'";
                parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
            }
            if (txtRequestor.Text.Trim() != "")
            {
                term += " and b.requestEmployeeName like '%'+@requestorname+'%'";
                parms.Add(new SqlParameter("@requestorname", txtRequestor.Text.Trim()));
            }
            DataSet ds = RecipientManager.GetPaymentNotifyAuditing(term, parms);
            gvG.DataSource = ds;
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
        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        { }
        protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SendMail")
            {
                string alertMsg = SendMail(int.Parse(e.CommandArgument.ToString()));
                if (alertMsg != "")
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + alertMsg + "');", true);
                else
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('发送成功！');", true);
            }
        }
        protected void gvG_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvG.PageIndex = e.NewPageIndex;
            ListBind();
        }

        private string SendMail(int ReturnID)
        {
            ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(ReturnID);
            try
            {
                string body = "您有一笔付款申请单[" + returnModel.ReturnCode + "]处于[" + ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value, 0, returnModel.IsDiscount) + "]，请及时将付款申请单流转到采购部，以便工作的顺利完成。";
                ESP.ConfigCommon.SymmetricCrypto crypto = new ESP.ConfigCommon.SymmetricCrypto();
                string url = HttpContext.Current.Request.Url.ToString();
                ESP.ConfigCommon.SendMail.Send1("付款申请提醒", ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(returnModel.RequestorID.Value), body, true, new Hashtable());
                return "";
            }
            catch
            {
                return returnModel.ReturnCode + "发送失败!</br>";
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string[] itemIds = hidItemIds.Value.TrimEnd(',').Split(',');
            string alertMsg = "";
            foreach (string id in itemIds)
            {
                if (id.Trim() != "")
                    alertMsg += SendMail(int.Parse(id));
            }
            if (alertMsg != "")
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + alertMsg + "');", true);
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('发送成功！');", true);
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
    }
}
