using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Common;

namespace SEPAdmin.HR.Employees
{
    public partial class RefundAdd : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["rid"]))
                {
                    RefundInfo model = new RefundManager().GetModel(int.Parse(Request["rid"]));
                    ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(model.UserId);
                    txtProductName.Text = model.ProductName;
                    txtProductNo.Text = model.ProductNo;
                    txtRemark.Text = model.Remark;
                    //model.UserId = int.Parse(users[0]);
                    txtBeginTime.Text = model.BeginTime == null ? "" : model.BeginTime.Value.ToString("yyyy-MM-dd");
                    txtCost.Text = model.Cost.ToString();
                    //model.Status = (int)RefundStatus.UnEnableStatus;
                    //model.Type = int.Parse(ddlType.SelectedValue);
                    ddlType.SelectedValue = model.Type.ToString();
                    labUserName.Text = userModel.LastNameCN + userModel.FirstNameCN;
                    hidUserName.Value = userModel.UserID.ToString() + "-" + userModel.LastNameCN + userModel.FirstNameCN;
                }
            }
        }

        /// <summary>
        /// 保存笔记租赁信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCommit_Click(object sender, EventArgs e)
        {
            try
            {
                string[] users = hidUserName.Value.Split('-');

                if (!string.IsNullOrEmpty(Request["rid"]))
                {
                    RefundInfo model = new RefundManager().GetModel(int.Parse(Request["rid"]));
                    model.ProductName = txtProductName.Text;
                    model.ProductNo = txtProductNo.Text;
                    model.Remark = txtRemark.Text;
                    model.UserId = int.Parse(users[0]);
                    model.Cost = decimal.Parse(txtCost.Text);
                    model.LastUpdateTime = DateTime.Now;
                    model.LastUpdater = UserInfo.FullNameCN;
                    model.LastUpdaterIP = HttpContext.Current.Request.UserHostAddress;
                    model.IsDeleted = false;
                    model.Status = (int)RefundStatus.UnEnableStatus;
                    model.Type = int.Parse(ddlType.SelectedValue);

                    int res = new RefundManager().Update(model);
                    if (res > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁修改成功！');window.location='ITRefundList.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁修改失败！');", true);
                    }
                }
                else
                {
                    List<RefundInfo> reflist = new RefundManager().GetModelList(int.Parse(users[0]), (int)RefundType.NetBookType, (int)RefundStatus.BeginStatus, 0);
                    if (reflist.Count > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('此用户已申请笔记本租赁！');window.location='ITRefundList.aspx';", true);
                    }
                    else
                    {
                        RefundInfo model = new RefundInfo();
                        model.ProductName = txtProductName.Text;
                        model.ProductNo = txtProductNo.Text;
                        model.Remark = txtRemark.Text;
                        model.UserId = int.Parse(users[0]);
                        model.Cost = decimal.Parse(txtCost.Text);
                        model.CreateTime = model.LastUpdateTime = DateTime.Now;
                        model.Creator = model.LastUpdater = UserInfo.FullNameCN;
                        model.CreatorIP = model.LastUpdaterIP = HttpContext.Current.Request.UserHostAddress;
                        model.IsDeleted = false;
                        model.Status = (int)RefundStatus.UnEnableStatus;
                        model.Type = int.Parse(ddlType.SelectedValue);

                        int res = new RefundManager().Add(model);
                        if (res > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁登记成功！');window.location='ITRefundList.aspx';", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁登记失败！');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add("HumanResource", ex.ToString(), ESP.Logging.LogLevel.Error, ex);
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁登记失败！');", true);
            }
        }

        /// <summary>
        /// 启动笔记本租赁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                string[] users = hidUserName.Value.Split('-');

                if (!string.IsNullOrEmpty(Request["rid"]))
                {
                    RefundInfo model = new RefundManager().GetModel(int.Parse(Request["rid"]));
                    model.ProductName = txtProductName.Text;
                    model.ProductNo = txtProductNo.Text;
                    model.Remark = txtRemark.Text;
                    model.UserId = int.Parse(users[0]);
                    model.Cost = decimal.Parse(txtCost.Text);
                    model.LastUpdateTime = DateTime.Now;
                    model.LastUpdater = UserInfo.FullNameCN;
                    model.LastUpdaterIP = HttpContext.Current.Request.UserHostAddress;
                    model.IsDeleted = false;
                    model.Status = (int)RefundStatus.BeginStatus;
                    model.Type = int.Parse(ddlType.SelectedValue);

                    int res = new RefundManager().Update(model);
                    if (res > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁修改成功！');window.location='ITRefundList.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁修改失败！');", true);
                    }
                }
                else
                {

                    if (ddlType.SelectedValue == RefundType.NetBookType.ToString())
                    {
                        List<RefundInfo> reflist = new RefundManager().GetModelList(int.Parse(users[0]), (int)RefundType.NetBookType, (int)RefundStatus.BeginStatus, 0);
                        if (reflist.Count > 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('此用户已申请笔记本租赁！');", true);
                            return;
                        }
                    }
                    //else
                    //{
                    RefundInfo model = new RefundInfo();
                    model.UserId = int.Parse(users[0]);
                    model.BeginOperator = UserInfo.UserID.ToString();
                    model.BeginTime = DateTime.Parse(txtBeginTime.Text);
                    model.ProductName = txtProductName.Text;
                    model.ProductNo = txtProductNo.Text;
                    model.Remark = txtRemark.Text;
                    model.Cost = decimal.Parse(txtCost.Text);
                    model.CreateTime = model.LastUpdateTime = DateTime.Now;
                    model.Creator = model.LastUpdater = UserInfo.FullNameCN;
                    model.CreatorIP = model.LastUpdaterIP = HttpContext.Current.Request.UserHostAddress;
                    model.IsDeleted = false;
                    model.Status = (int)RefundStatus.BeginStatus;
                    model.Type = int.Parse(ddlType.SelectedValue);

                    int res = new RefundManager().Add(model);
                    if (res > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁启动成功！');window.location='ITRefundList.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁启动失败！');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁启动失败！');", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ITRefundList.aspx");
        }
    }
}
