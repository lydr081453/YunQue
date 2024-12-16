using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Net.Mail;
using ESP.HumanResource.BusinessLogic;

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
        DataSet ds = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionDetailsByUserId(UserID);
        gvDetailList.DataSource = ds;
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
        ESP.HumanResource.Entity.DimissionDetailsInfo dimissionDetailInfo = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetModel(detailid);

        ESP.HumanResource.DataAccess.DimissionFormProvider dimFormDal = new ESP.HumanResource.DataAccess.DimissionFormProvider();
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = dimFormDal.GetModel(dimissionDetailInfo.DimissionId);

        
        if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance || dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('已经交接成功。');window.location='ReceiverAuditList.aspx';", true);
            return;
        }
        
        if (dimissionDetailInfo != null)
        {
            dimissionDetailInfo.ReceiverTime = DateTime.Now;
            dimissionDetailInfo.Status = (int)ESP.HumanResource.Common.AuditStatus.Audited;

            bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.UpdateDetailAndCheckDimission(dimissionDetailInfo);

            if (b)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('交接成功。');window.location='ReceiverAuditList.aspx';", true);
                
                int flag = 0;  // 标记是否需要修改离职单
                List<ESP.HumanResource.Entity.DimissionDetailsInfo> detailList = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionData(dimissionDetailInfo.DimissionId);
                if (detailList != null && detailList.Count > 0)
                {
                    foreach (ESP.HumanResource.Entity.DimissionDetailsInfo detail in detailList)
                    {
                        if (detail.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit)
                        {
                            flag = 1;
                        }
                    }
                }
                if (flag == 0)
                {
                    ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
                    if (manageModel == null)
                        manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);

                    List<MailAddress> mailAddressList = new List<MailAddress>();
                    int mailType = 1;
                    if (dimissionFormInfo.DirectorId == dimissionFormInfo.ManagerId)
                    {
                        ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAtt = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                        ESP.Framework.Entity.DepartmentInfo companyDep = userAtt.GetRootDepartmentID(dimissionFormInfo.UserId);
                        List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(companyDep.DepartmentID, ESP.HumanResource.Common.Status.DimissionSendMail);
                        if (list != null && list.Count > 0)
                        {
                            foreach (ESP.HumanResource.Entity.UsersInfo userModel in list)
                            {
                                mailAddressList.Add(new MailAddress(userModel.Email));
                            }
                        }

                        ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(manageModel.HRId);
                        if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email))
                            mailAddressList.Add(new MailAddress(userInfo.Email));
                        mailType = 2;
                    }
                    else
                    {
                        ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(manageModel.DimissionManagerId);
                        if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email))
                            mailAddressList.Add(new MailAddress(userInfo.Email));
                        mailType = 1;
                    }
                    if (mailAddressList != null && mailAddressList.Count > 0)
                    {
                        try
                        {
                            string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=" + mailType;
                            string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                            ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                        }
                        catch
                        { }
                    }
                    try
                    {
                        ESP.Mail.MailManager.Send("离职管理--交接人确认", "您好，您未处理的单据交接人（" + dimissionDetailInfo.ReceiverName + "）已确认。", false, new MailAddress[] { new MailAddress(ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId).Email) });
                    }
                    catch
                    { }
                }
            }
            else
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('交接失败，请联系统管理员。');window.location='ReceiverAuditList.aspx';", true);
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
        List<ESP.HumanResource.Entity.DimissionDetailsInfo> detailList = new List<ESP.HumanResource.Entity.DimissionDetailsInfo>();
        int dimissionNO = 0;
        foreach (object tmp in this.SelectedItems)
        {
            int detailid = 0;
            if (tmp != null)
            {
                if (int.TryParse(tmp.ToString(), out detailid))
                {
                    ESP.HumanResource.Entity.DimissionDetailsInfo dimissionDetailInfo = 
                        ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetModel(detailid);
                    if (dimissionDetailInfo != null)
                    {
                        dimissionDetailInfo.ReceiverTime = DateTime.Now;
                        dimissionDetailInfo.Status = (int)ESP.HumanResource.Common.AuditStatus.Audited;
                        detailList.Add(dimissionDetailInfo);
                        dimissionNO = dimissionDetailInfo.DimissionId;
                    }
                }
            }
        }
        ESP.HumanResource.DataAccess.DimissionFormProvider dimFormDal = new ESP.HumanResource.DataAccess.DimissionFormProvider();
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = dimFormDal.GetModel(dimissionNO);

        if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance || dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('已经交接成功。');window.location='ReceiverAuditList.aspx';", true);
            return;
        }

        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.UpdateDetailAndCheckDimission(detailList);
        if (b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('交接成功。');window.location='ReceiverAuditList.aspx';", true);

            int flag = 0;  // 标记是否需要修改离职单
            List<ESP.HumanResource.Entity.DimissionDetailsInfo> unAuditDetailList = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionData(dimissionNO);
            if (unAuditDetailList != null && unAuditDetailList.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.DimissionDetailsInfo detail in unAuditDetailList)
                {
                    if (detail.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit)
                    {
                        flag = 1;
                    }
                }
            }
            if (flag == 0)
            {
                ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
                if (manageModel == null)
                    manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);

                List<MailAddress> mailAddressList = new List<MailAddress>();
                int mailType = 1;
                if (dimissionFormInfo.PreAuditorId == dimissionFormInfo.ManagerId)
                {
                    ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAtt = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                    ESP.Framework.Entity.DepartmentInfo companyDep = userAtt.GetRootDepartmentID(dimissionFormInfo.UserId);
                    List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(companyDep.DepartmentID, ESP.HumanResource.Common.Status.DimissionSendMail);
                    if (list != null && list.Count > 0)
                    {
                        foreach (ESP.HumanResource.Entity.UsersInfo userModel in list)
                        {
                            mailAddressList.Add(new MailAddress(userModel.Email));
                        }
                    }

                    ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(manageModel.HRId);
                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email))
                        mailAddressList.Add(new MailAddress(userInfo.Email));
                    mailType = 3;
                }
                if (dimissionFormInfo.PreAuditorId == dimissionFormInfo.DirectorId)
                {
                    ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(manageModel.DimissionManagerId);
                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email))
                        mailAddressList.Add(new MailAddress(userInfo.Email));
                    mailType = 2;
                }
                else
                {
                    ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(manageModel.DimissionDirectorid);
                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email))
                        mailAddressList.Add(new MailAddress(userInfo.Email));
                    mailType = 1;
                }
                if (mailAddressList != null && mailAddressList.Count > 0)
                {
                    try
                    {
                        string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=" + mailType;
                        string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                        ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                    }
                    catch
                    { }
                }
                try
                {
                    ESP.Mail.MailManager.Send("离职管理--交接人确认", "您好，您未处理的单据交接人（" + UserInfo.FullNameCN + "）已确认。", false, new MailAddress[] { new MailAddress(ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId).Email) });
                }
                catch
                { }
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
    