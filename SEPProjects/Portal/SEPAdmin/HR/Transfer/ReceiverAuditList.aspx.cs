using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Net.Mail;
using ESP.HumanResource.BusinessLogic;
using System.Data.SqlClient;
using System.Linq;

namespace SEPAdmin.HR.Transfer
{

    public partial class ReceiverAuditList : ESP.Web.UI.PageBase
    {
        #region property
        /// <summary>
        /// 获取或设置选中项的集合
        /// </summary>
        protected ArrayList SelectedItems
        {
            get
            {
                return (ViewState["PageSelectedItems"] != null) ? (ArrayList)ViewState["PageSelectedItems"] : null;
            }
            set
            {
                ViewState["PageSelectedItems"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["detailid"]))
                {
                    int detailid = 0;
                    if (!int.TryParse(Request["detailid"], out detailid))
                        detailid = 0;
                    ReceiverDetail(detailid);
                }
                ListBind();
            }
        }

        public void ListBind()
        {
            SqlParameter[] parameters = {
					new SqlParameter("@receiverid", SqlDbType.Int,4),
                    new SqlParameter("@status", SqlDbType.Int,4)
				};
            parameters[0].Value = UserID;
            parameters[1].Value = (int)ESP.HumanResource.Common.AuditStatus.NotAudit ;

            var list = ESP.HumanResource.BusinessLogic.TransferDetailsManager.GetList(" receiverid=@receiverid and status=@status", parameters.ToList());
            gvDetailList.DataSource = list;
            gvDetailList.DataBind();

            if (gvDetailList.PageCount > 1)
            {
                PageBottom.Visible = true;
                PageTop.Visible = true;
            }
            else
            {
                PageBottom.Visible = false;
                PageTop.Visible = false;
            }
            if (gvDetailList.Rows.Count > 0)
            {
                tabTop.Visible = true;
                tabBottom.Visible = true;
            }
            else
            {
                tabTop.Visible = false;
                tabBottom.Visible = false;
                btnReceiverBottom.Visible = false;
                btnReceiverTop.Visible = false;
            }

            labAllNum.Text = labAllNumT.Text = gvDetailList.Rows.Count.ToString();
            labPageCount.Text = labPageCountT.Text = (gvDetailList.PageIndex + 1).ToString() + "/" + gvDetailList.PageCount.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        /// <summary>
        /// 交接人确认交接
        /// </summary>
        public void ReceiverDetail(int detailid)
        {
            ESP.HumanResource.Entity.TransferDetailsInfo DetailInfo = ESP.HumanResource.BusinessLogic.TransferDetailsManager.GetModel(detailid);
            if (DetailInfo != null)
            {
                DetailInfo.ReceiverTime = DateTime.Now;
                DetailInfo.Status = (int)ESP.HumanResource.Common.AuditStatus.Audited;

                bool b = ESP.HumanResource.BusinessLogic.TransferDetailsManager.UpdateDetailAndCheckTransfer(DetailInfo);

                if (b)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('交接成功。');window.location='ReceiverAuditList.aspx';", true);

                    int flag = 0;  // 标记是否需要修改离职单
                    var detailList = TransferDetailsManager.GetList(" transferId=@transferId and receiverid<>0", new List<SqlParameter>() { new SqlParameter("@transferId", DetailInfo.TransferId) });

                    if (detailList != null && detailList.Count > 0)
                    {
                        foreach (ESP.HumanResource.Entity.TransferDetailsInfo detail in detailList)
                        {
                            if (detail.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit)
                            {
                                flag = 1;
                            }
                        }
                    }
                    if (flag == 0)
                    {
                        ESP.HumanResource.DataAccess.TransferDataProvider dimFormDal = new ESP.HumanResource.DataAccess.TransferDataProvider();
                        ESP.HumanResource.Entity.TransferInfo formInfo = dimFormDal.GetModel(DetailInfo.TransferId);

                        ESP.Framework.Entity.OperationAuditManageInfo oldManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(formInfo.OldGroupId);
                        ESP.Framework.Entity.OperationAuditManageInfo newManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(formInfo.OldGroupId);

                        List<MailAddress> mailAddressList = new List<MailAddress>();

                        ESP.Framework.Entity.UserInfo hrOut = ESP.Framework.BusinessLogic.UserManager.Get(oldManageModel.HRId);
                        if (hrOut != null && !string.IsNullOrEmpty(hrOut.Email))
                            mailAddressList.Add(new MailAddress(hrOut.Email));

                        ESP.Framework.Entity.UserInfo hrIn = ESP.Framework.BusinessLogic.UserManager.Get(newManageModel.HRId);
                        if (hrIn != null && !string.IsNullOrEmpty(hrIn.Email))
                            mailAddressList.Add(new MailAddress(hrIn.Email));

                        if (mailAddressList != null && mailAddressList.Count > 0)
                        {
                            string url = "http://" + Request.Url.Authority + "/HR/Transfer/TransferMail.aspx?Id=" + formInfo.Id;
                            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                            ESP.Mail.MailManager.Send("员工转组", body, true, mailAddressList.ToArray());
                        }
                        ESP.Mail.MailManager.Send("员工转组--交接人确认", "您好，您的单据交接人（" + UserInfo.FullNameCN + "）已确认。", false, new MailAddress[] { new MailAddress(ESP.Framework.BusinessLogic.UserManager.Get(formInfo.TransId).Email) });
                    }

                    else
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('交接失败，请联系统管理员。');window.location='ReceiverAuditList.aspx';", true);
                }
            }
        }

        /// <summary>
        /// 从当前页收集选中项的情况
        /// </summary>
        protected void CollectSelected()
        {
            ArrayList selectedItems = null;
            if (this.SelectedItems == null)
                selectedItems = new ArrayList();
            else
                selectedItems = this.SelectedItems;

            for (int i = 0; i < this.gvDetailList.Rows.Count; i++)
            {
                string id = gvDetailList.DataKeys[i].Value.ToString();
                CheckBox cb = this.gvDetailList.Rows[i].FindControl("ckb") as CheckBox;
                if (selectedItems.Contains(id) && !cb.Checked)
                    selectedItems.Remove(id);
                if (!selectedItems.Contains(id) && cb.Checked)
                    selectedItems.Add(id);
            }
            this.SelectedItems = selectedItems;
        }

        /// <summary>
        /// 确认交接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReceiverTop_Click(object sender, EventArgs e)
        {
            ReceiverAllInfo();
        }

        /// <summary>
        /// 确认交接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReceiverBottom_Click(object sender, EventArgs e)
        {
            ReceiverAllInfo();
        }

        /// <summary>
        /// 确认交接用户所选择的单据信息
        /// </summary>
        protected void ReceiverAllInfo()
        {
            CollectSelected();
            List<ESP.HumanResource.Entity.TransferDetailsInfo> detailList = new List<ESP.HumanResource.Entity.TransferDetailsInfo>();
            int transferId = 0;
            foreach (object tmp in this.SelectedItems)
            {
                int detailid = 0;
                if (tmp != null)
                {
                    if (int.TryParse(tmp.ToString(), out detailid))
                    {
                        ESP.HumanResource.Entity.TransferDetailsInfo detailInfo =
                            ESP.HumanResource.BusinessLogic.TransferDetailsManager.GetModel(detailid);
                        if (detailInfo != null)
                        {
                            detailInfo.ReceiverTime = DateTime.Now;
                            detailInfo.Status = (int)ESP.HumanResource.Common.AuditStatus.Audited;
                            detailList.Add(detailInfo);
                            transferId = detailInfo.TransferId;
                        }
                    }
                }
            }

            bool b = ESP.HumanResource.BusinessLogic.TransferDetailsManager.UpdateDetailAndCheckTransfer(detailList);
            if (b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('交接成功。');window.location='ReceiverAuditList.aspx';", true);

                int flag = 0;  // 标记是否需要修改离职单
                var unAuditDetailList = TransferDetailsManager.GetList(" transferId=@transferId and receiverid<>0", new List<SqlParameter>() { new SqlParameter("@transferId", transferId)});
                   
                if (unAuditDetailList != null && unAuditDetailList.Count > 0)
                {
                    foreach (ESP.HumanResource.Entity.TransferDetailsInfo detail in unAuditDetailList)
                    {
                        if (detail.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit)
                        {
                            flag = 1;
                        }
                    }
                }
                if (flag == 0)
                {
                    ESP.HumanResource.DataAccess.TransferDataProvider dimFormDal = new ESP.HumanResource.DataAccess.TransferDataProvider();
                    ESP.HumanResource.Entity.TransferInfo formInfo = dimFormDal.GetModel(transferId);

                    ESP.Framework.Entity.OperationAuditManageInfo oldManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(formInfo.OldGroupId);
                    ESP.Framework.Entity.OperationAuditManageInfo newManageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(formInfo.OldGroupId);

                    List<MailAddress> mailAddressList = new List<MailAddress>();

                    ESP.Framework.Entity.UserInfo hrOut = ESP.Framework.BusinessLogic.UserManager.Get(oldManageModel.HRId);
                    if (hrOut != null && !string.IsNullOrEmpty(hrOut.Email))
                        mailAddressList.Add(new MailAddress(hrOut.Email));

                    ESP.Framework.Entity.UserInfo hrIn = ESP.Framework.BusinessLogic.UserManager.Get(newManageModel.HRId);
                    if (hrIn != null && !string.IsNullOrEmpty(hrIn.Email))
                        mailAddressList.Add(new MailAddress(hrIn.Email));

                    if (mailAddressList != null && mailAddressList.Count > 0)
                    {
                        string url = "http://" + Request.Url.Authority + "/HR/Transfer/TransferMail.aspx?Id=" + formInfo.Id;
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        ESP.Mail.MailManager.Send("员工转组 - 交接人确认", body, true, mailAddressList.ToArray());
                    }
                    ESP.Mail.MailManager.Send("员工转组 - 交接人确认", "您转组需交接的单据，交接人（" + UserInfo.FullNameCN + "）已确认。", false, new MailAddress[] { new MailAddress(ESP.Framework.BusinessLogic.UserManager.Get(formInfo.TransId).Email) });
                }
            }
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('交接失败，请联系统管理员。');window.location='ReceiverAuditList.aspx';", true);
        }

        #region 分页设置
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetailList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvDetailList.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvDetailList.PageIndex + 1) > gvDetailList.PageCount ? gvDetailList.PageCount : (gvDetailList.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvDetailList.PageIndex - 1) < 0 ? 0 : (gvDetailList.PageIndex - 1));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvList_PageIndexChanging(new object(), e);
        }

        /// <summary>
        /// 分页按钮的显示设置
        /// </summary>
        /// <param name="page"></param>
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
    }
}