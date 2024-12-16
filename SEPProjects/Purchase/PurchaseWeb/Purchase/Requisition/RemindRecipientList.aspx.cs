using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class RemindRecipientList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        private void ListBind()
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            string term = "";
            if (CurrentUserID.ToString() != ESP.Configuration.ConfigurationManager.SafeAppSettings["AuditorId"].ToString())
            {
                term += @"and case when patindex(supplier_area,'北京') > 0 then c.bjpaymentuserid
	                        when patindex(supplier_area,'上海') > 0 then c.shpaymentuserid
	                        when patindex(supplier_area,'广州') > 0 then c.gzpaymentuserid
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
                    term += " and a.id = @id";
                    parms.Add(new SqlParameter("@id", txtGlideNo.Text.TrimStart('0')));
                }
            }
            if (txtProjectCode.Text.Trim() != "")
            {
                term += " and project_code like '%'+@projectcode+'%'";
                parms.Add(new SqlParameter("@projectcode", txtProjectCode.Text.Trim()));
            }
            if (txtsupplierName.Text.Trim() != "")
            {
                term += " and a.supplier_name like '%'+@suppliername+'%'";
                parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
            }
            if (txtRequestor.Text.Trim() != "")
            {
                term += " and a.requestorname like '%'+@requestorname+'%'";
                parms.Add(new SqlParameter("@requestorname", txtRequestor.Text.Trim()));
            }
            if (txtB1.Text.Trim() != "")
            {
                term += " and order_audittime >=CONVERT(datetime , @b1, 120 )";
                parms.Add(new SqlParameter("@b1", txtB1.Text.Trim()));
            }
            if (txtE1.Text.Trim() != "")
            {
                term += " and order_audittime <= dateadd(d,1,CONVERT(datetime , @e1, 120 ))";
                parms.Add(new SqlParameter("@e1", txtE1.Text.Trim()));
            }
            if (txtB2.Text.Trim() != "")
            {
                term += " and recipientDate >=@b2";
                parms.Add(new SqlParameter("@b2", txtB2.Text.Trim()));
            }
            if (txtE2.Text.Trim() != "")
            {
                term += " and recipientDate <= @e2";
                parms.Add(new SqlParameter("@e2", txtE2.Text.Trim()));
            }


            DataTable list = GeneralInfoManager.GetRemindRecipientList(term, parms);
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
            if (list.Rows.Count > 0)
            {
                tabTop.Visible = true;
                tabBottom.Visible = true;
            }
            else
            {
                tabTop.Visible = false;
                tabBottom.Visible = false;
            }

            labAllNum.Text = labAllNumT.Text = list.Rows.Count.ToString();
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
            if (e.CommandName == "SendMail")
            {
                string alertMsg = SendMail(e.CommandArgument.ToString());
                if (alertMsg != "")
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + alertMsg + "');", true);
                else
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('发送成功！');", true);
            }
        }

        private string SendMail(string str)
        {
            int generalId = int.Parse(str.Split('#')[0]);
            GeneralInfo generalModel = GeneralInfoManager.GetModel(generalId);
            try
            {
                ESP.Purchase.Entity.BlackDetailInfo blackModel = new BlackDetailInfo();
                blackModel.OrderID = generalId;
                blackModel.OrderType = (int)ESP.Purchase.Common.State.RemindOrderType.Recipient;
                blackModel.SenderID = CurrentUser.SysID;
                blackModel.SenderName = CurrentUserName;
                blackModel.SendMailTime = DateTime.Now;
                blackModel.Status = (int)ESP.Purchase.Common.State.BlackListStatus.MailSended;
                int BlackDetailId = BlackDetailManager.Add(blackModel);

                string alertUserEmail = "";
                string body = "";
                if (str.Split('#')[2].Trim() == "")
                {
                    alertUserEmail = ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalModel.goods_receiver);
                    body = "您为[" + generalModel.PrNo + "]申请单的收货人，在预计收货时间[" + str.Split('#')[1] + "]仍然没有完全收货，请及时完成收货以便工作的顺利完成。";
                }
                else
                {
                    alertUserEmail = ESP.Purchase.Common.State.getEmployeeEmailBySysUserId(generalModel.appendReceiver);
                    body = "你为[" + str.Split('#')[2].Trim() + "]收货单的附加收货人,请及时进行收货确认以便工作的顺利完成。";
                }
                //SymmetricCrypto crypto = new SymmetricCrypto();
                //string url = HttpContext.Current.Request.Url.ToString();
                //string no_http = url.Substring(url.IndexOf("//") + 2);
                //string host_url = "http://" + no_http.Substring(0, no_http.IndexOf("/") + 1) + "Purchase/Requisition/";
                //string link = host_url + "FeedBack.aspx?" + RequestName.GeneralID + "=" + generalModel.id + "&BlackDetailId=" + BlackDetailId;
                //body += "<br/><a href='" + link + "' target='_blank'>添加延迟原因</a>";
                ESP.ConfigCommon.SendMail.Send1("收货提醒", alertUserEmail, body, true, new Hashtable());
                return "";
            }
            catch
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('邮件发送失败！');", true);
                return (str.Split('#')[2].Trim() == "" ? generalModel.PrNo : str.Split('#')[2].Trim()) + "发送失败！</br>";
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string[] itemIds = hidItemIds.Value.TrimEnd(',').Split(',');
            string alertMsg = "";
            foreach (string id in itemIds)
            {
                if (id.Trim() != "")
                    alertMsg += SendMail(id);
            }
            if (alertMsg != "")
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + alertMsg + "');", true);
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('发送成功！');", true);
        }

        protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dv = (DataRowView)e.Row.DataItem;

                HyperLink hypAuditurl = (HyperLink)e.Row.FindControl("hypView");
                if (null != hypAuditurl)
                {
                    hypAuditurl.NavigateUrl = "OrderDetailTab.aspx?" + RequestName.GeneralID + "=" + dv["id"].ToString() + "&" + RequestName.BackUrl + "=RemindRecipientList.aspx";
                }
                ArrayList Items = new ArrayList(hidItemIds.Value.Split(','));
                if (Items.Contains(dv["id"].ToString() + "#" + dv["recipientDate"].ToString()))
                {
                    System.Web.UI.HtmlControls.HtmlInputCheckBox chk = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Row.FindControl("chkItem");
                    chk.Checked = true;
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
            hidItemIds.Value = "";
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
