using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data.SqlClient;

namespace FinanceWeb.ITAsset
{
    public partial class AssetView : ESP.Web.UI.PageBase
    {
        int assetId = 0;
        int op = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["assetId"]))
            {
                assetId = int.Parse(Request["assetId"]);
            }
            if (!string.IsNullOrEmpty(Request["op"]))
            {
                op = int.Parse(Request["op"]);
            }
            if (!IsPostBack)
            {
                bindInfo();
            }
        }

        private string GetReceiveLog(ESP.Finance.Entity.ITAssetsInfo model)
        {
            string log = string.Empty;

            string terms = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            terms += " AssetId=@AssetId";
            parms.Add(new SqlParameter("@AssetId", model.Id));

            IList<ESP.Finance.Entity.ITAssetReceivingInfo> receiveList = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(terms, parms);

            foreach (ESP.Finance.Entity.ITAssetReceivingInfo lo in receiveList)
            {
                ESP.Framework.Entity.EmployeeInfo op = ESP.Framework.BusinessLogic.EmployeeManager.Get(lo.OperatorId);
                
                log += lo.UserName + "[" + lo.UserCode + "]从" + op.LastNameCN + op.FirstNameCN + "处领用登记" + lo.ReceiveDesc.ToString() + "</br>";
                if (lo.Status != (int)ESP.Finance.Utility.FixedAssetStatus.Receiving)
                    log += lo.UserName + "[" + lo.UserCode + "] 确认领用" + lo.ReceiveDate.ToString("yyyy-MM-dd") + "</br>";
                if (lo.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Normal)
                    log += lo.UserName + "[" + lo.UserCode + "] 归还" + lo.ReturnDate.ToString("yyyy-MM-dd") + "</br>";
            }

            return log;
        }

        private string GetScrapLog(ESP.Finance.Entity.ITAssetsInfo model)
        {
            string log = string.Empty;
            ESP.Framework.Entity.EmployeeInfo user1 = null;
            ESP.Framework.Entity.EmployeeInfo user2 = null;
            ESP.Framework.Entity.EmployeeInfo user3 = null;
            if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Scrapping2)
            {
                if (model.ScrapUserId != 0)
                {
                    user1 = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.ScrapUserId);
                    log += user1.LastNameCN + user1.FirstNameCN + "[" + user1.Code + "] 报废申请 " + model.ScrapDesc + " " + model.ScrapDate.ToString() + "</br>";
                }
            }

            if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Scrapping)
            {
                if (model.ScrapUserId != 0)
                {
                    user1 = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.ScrapUserId);
                    log += user1.LastNameCN + user1.FirstNameCN + "[" + user1.Code + "] 报废申请 " + model.ScrapDesc + " " + model.ScrapDate.ToString() + "</br>";
                }

                if (model.ScrapLeaderId != 0)
                {
                    user2 = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.ScrapLeaderId);
                    log += user2.LastNameCN + user2.FirstNameCN + "[" + user2.Code + "] 报废审核通过 " + model.ScrapLeaderDesc + " " + model.ScrapLeaderDate.ToString() + "</br>";
                }

            }

            if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Scrapped)
            {
                if (model.ScrapUserId != 0)
                {
                    user1 = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.ScrapUserId);
                    log += user1.LastNameCN + user1.FirstNameCN + "[" + user1.Code + "] 报废申请 " + model.ScrapDesc + " " + model.ScrapDate.ToString() + "</br>";
                }
                if (model.ScrapLeaderId != 0)
                {
                    user2 = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.ScrapLeaderId);
                    log += user2.LastNameCN + user2.FirstNameCN + "[" + user2.Code + "] 报废审核通过 " + model.ScrapLeaderDesc + " " + model.ScrapLeaderDate.ToString() + "</br>";
                }
                if (model.ScrapAuditorId != 0)
                {
                    user3 = ESP.Framework.BusinessLogic.EmployeeManager.Get(model.ScrapAuditorId);
                    log += user3.LastNameCN + user3.FirstNameCN + "[" + user3.Code + "] 报废审核通过 " + model.ScrapAuditDesc + " " + model.ScrapAuditDate.ToString() + "</br>";
                }
            }
            return log;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssetList.aspx");
        }

        private void bindInfo()
        {
            if (op == 2)
            {
                tabReceiver.Visible = true;
                btnReceiver.Visible = true;
            }
            else if (op == 3)
            {
                tabScrap.Visible = true;
                btnScrap.Visible = true;
            }
            else if (op == 4)
            {
                tabReturn.Visible = true;
                btnReturn.Visible = true;
            }
            //领用人确认
            else if (op == 5)
            {
                btnReceiver.Visible = true;
                btnBack.Visible = false;
            }
            else if (op == 6)
            {
                tabReturn.Visible = true;
                btnReturn.Visible = true;
            }
            else if (op == 7)
            {
                tabScrptAudit.Visible = true;
                btnScrapAudit.Visible = true;
            }
            else
            {
                tabReceiver.Visible = false;
                btnReceiver.Visible = false;
                tabScrap.Visible = false;
                btnScrap.Visible = false;
                tabReturn.Visible = false;
                btnReturn.Visible = false;
            }
            if (assetId > 0)
            {
                ESP.Finance.Entity.ITAssetsInfo model = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(assetId);
                if (model != null)
                {
                    lblName.Text = model.AssetName;
                    lblBrand.Text = model.Brand;
                    lblCategory.Text = model.CategoryName;
                    lblCode.Text = model.SerialCode;
                    lblConfig.Text = model.Configuration;
                    lblDate.Text = model.PurchaseDate.ToString("yyyy-MM-dd");
                    lblModel.Text = model.Model;
                    lblPrice.Text = model.Price.ToString("#,##0.00");
                    lblDesc.Text = model.AssetDesc;
                    lblPR.Text = model.RelationPO;

                    if (!string.IsNullOrEmpty(model.UpFile))
                    {
                        hpFile.Visible = true;
                        hpFile.ToolTip = "下载附件：" + model.UpFile;
                        this.hpFile.NavigateUrl = "FileDownLoad.aspx?assetId=" + model.Id.ToString();
                    }

                    if (!string.IsNullOrEmpty(model.Photo))
                    {
                        imgPhoto.Visible = true;
                        this.imgPhoto.ImageUrl = ESP.Configuration.ConfigurationManager.Items["FinancialHeader"] + "/images/ITAssets/" + model.Photo;
                    }


                    lblScrapReason.Text = model.ScrapDesc;

                    this.lblScrapLog.Text = this.GetScrapLog(model);
                    this.lblReceiveLog.Text = this.GetReceiveLog(model);

                    if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Received)
                    {
                        List<SqlParameter> parms = new List<SqlParameter>();
                        parms.Add(new SqlParameter("@assetId", model.Id));
                        parms.Add(new SqlParameter("@status", (int)ESP.Finance.Utility.FixedAssetStatus.Received));

                        ESP.Finance.Entity.ITAssetReceivingInfo rec = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(" assetId =@assetId and status=@status ", parms).FirstOrDefault();
                        this.hiddenUserid.Value = rec.UserId.ToString();
                        txtReturnRes.Text = rec.ReceiveDesc;
                    }

                    if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Receiving)
                    {
                        List<SqlParameter> parms = new List<SqlParameter>();
                        parms.Add(new SqlParameter("@assetId", model.Id));
                        parms.Add(new SqlParameter("@status", (int)ESP.Finance.Utility.FixedAssetStatus.Receiving));

                        ESP.Finance.Entity.ITAssetReceivingInfo rec = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(" assetId =@assetId and status=@status ", parms).FirstOrDefault();
                        this.hiddenUserid.Value = rec.UserId.ToString();
                        txtReturnRes.Text = rec.ReceiveDesc;
                    }
                }
            }
        }

        protected void btnReceiver_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hiddenUserid.Value))
                return;

            ESP.Finance.Entity.ITAssetsInfo model = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(assetId);
            //状态正常
            if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Normal)
            {
                ESP.Finance.Entity.ITAssetReceivingInfo rec = new ESP.Finance.Entity.ITAssetReceivingInfo();

                int status = (int)ESP.Finance.Utility.FixedAssetStatus.Receiving;

                if(ddlServer.SelectedValue!="0")
                    status = (int)ESP.Finance.Utility.FixedAssetStatus.Received;

                rec.AssetId = model.Id;
                rec.AssetName = model.AssetName;
                rec.Brand = model.Brand;
                rec.Model = model.Model;
                rec.SerialCode = model.SerialCode;
                rec.Price = model.Price;
                rec.OperatorId = CurrentUserID;
                rec.ReceiveDate = DateTime.Now;
                rec.ReceiveDesc = txtReceiveDesc.Text;
                rec.Status = status;
                rec.UserId = int.Parse(hiddenUserid.Value);
                rec.ReturnDate = new DateTime(1900, 1, 1);
                rec.UserCode = this.lblReceiverCode.Text;
                rec.UserName = this.lblReceiver.Text;
                rec.Email = this.lblReceiverEmail.Text;
                rec.Mobile = this.lblMobile.Text;
                rec.DataServer = this.ddlServer.SelectedValue;

                ESP.Finance.BusinessLogic.ITAssetReceivingManager.Add(rec);

                if (ddlServer.SelectedValue != "0")
                {
                    ESP.Finance.BusinessLogic.ITAssetReceivingManager.Add(rec,ddlServer.SelectedValue);
                }

                model.Status = status;
                model.ScrapAuditDate = new DateTime(1900, 1, 1);
                model.ScrapDate = new DateTime(1900, 1, 1);
                model.EditDate = DateTime.Now;

                ESP.Finance.BusinessLogic.ITAssetsManager.Update(model);

                string exMail = string.Empty;
                try
                {
                if(!string.IsNullOrEmpty(rec.Email))
                SendMailHelper.AssetReceivingSendMail("您已申请领用了公司固定资产，请妥善保管与使用。</br>资产名称："+rec.AssetName+"</br>资产编号："+rec.SerialCode,rec.Email);
                }
                catch
                {
                    exMail = "(邮件发送失败!)";
                }
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('领用申请成功！"+exMail+"');window.location='AssetList.aspx';", true);

            }
            //状态领用中，待领用人确认
            else if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Receiving)
            {
                List<SqlParameter> parms = new List<SqlParameter>();
                parms.Add(new SqlParameter("@assetId", model.Id));
                parms.Add(new SqlParameter("@UserId", CurrentUserID));
                parms.Add(new SqlParameter("@status", (int)ESP.Finance.Utility.FixedAssetStatus.Receiving));

                ESP.Finance.Entity.ITAssetReceivingInfo rec = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(" assetId =@assetId and userid =@userid and status=@status ", parms).FirstOrDefault();

                if (rec != null)
                {
                    rec.Status = (int)ESP.Finance.Utility.FixedAssetStatus.Received;
                    model.Status = (int)ESP.Finance.Utility.FixedAssetStatus.Received;

                    ESP.Finance.BusinessLogic.ITAssetReceivingManager.Update(rec);

                    ESP.Finance.BusinessLogic.ITAssetsManager.Update(model);

                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('确认领用成功！');window.location='/project/Default.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('未找到匹配数据，无法确认领用！');", true);
                    return;
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('资产状态锁定中，无法领用！');", true);
                return;

            }
        }

        protected void btnScrap_Click(object sender, EventArgs e)
        {
            ESP.Finance.Entity.ITAssetsInfo model = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(assetId);
            ESP.Finance.Entity.ITAssetReceivingInfo receiving = ESP.Finance.BusinessLogic.ITAssetReceivingManager.getLastModel(assetId);

           ESP.HumanResource.Entity.EmployeesInPositionsInfo positon = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(receiving.UserId);

            ESP.Framework.Entity.OperationAuditManageInfo operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(receiving.UserId);
            if (operation == null)
                operation = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(positon.DepartmentID);

            if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Normal)
            {
                model.Status = (int)ESP.Finance.Utility.FixedAssetStatus.Scrapping2;
                model.ScrapDate = DateTime.Now;
                model.ScrapLeaderId = operation.DirectorId;
                model.ScrapLeader = operation.DirectorName;
                model.ScrapDesc = txtScrap.Text;
                model.ScrapUserId = CurrentUserID;
                model.ScrapUserName = CurrentUserName;
                model.EditDate = DateTime.Now;

                ESP.Finance.BusinessLogic.ITAssetsManager.Update(model);

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('资产报废申请成功，请等待审核！');window.location='AssetList.aspx';", true);

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('资产在库状态可做报废操作！');", true);
                return;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            ESP.Finance.Entity.ITAssetsInfo model = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(assetId);
            //状态已领用
            if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Received)
            {
                List<SqlParameter> parms = new List<SqlParameter>();
                parms.Add(new SqlParameter("@assetId", model.Id));
                parms.Add(new SqlParameter("@status", (int)ESP.Finance.Utility.FixedAssetStatus.Received));

                ESP.Finance.Entity.ITAssetReceivingInfo rec = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(" assetId =@assetId and status=@status ", parms).FirstOrDefault();

                if (rec != null)
                {
                    rec.Status = (int)ESP.Finance.Utility.FixedAssetStatus.Normal;
                    rec.ReturnDate = DateTime.Now;
                    rec.ReceiveDesc = rec.ReceiveDesc + txtReturnRes.Text;

                    model.Status = (int)ESP.Finance.Utility.FixedAssetStatus.Normal;
                    model.EditDate = DateTime.Now;
                    ESP.Finance.BusinessLogic.ITAssetReceivingManager.Update(rec);

                    if (!string.IsNullOrEmpty(rec.DataServer) && rec.DataServer!="0")
                        ESP.Finance.BusinessLogic.ITAssetReceivingManager.UpdateReturnStatus(rec,rec.DataServer);

                    ESP.Finance.BusinessLogic.ITAssetsManager.Update(model);
                    string exMail = string.Empty;
                    try
                    {
                    SendMailHelper.AssetReturnSendMail("您已归还公司固定资产。</br>资产名称：" + rec.AssetName + "</br>资产编号：" + rec.SerialCode, rec.Email);
                    }
                    catch
                    {
                        exMail = "(邮件发送失败!)";
                    }
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('资产归还成功！"+exMail+"');window.location='AssetList.aspx';", true);

                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('未找到匹配数据，无法资产归还操作！');", true);
                    return;
                }

            }
        }

        protected void btnUserAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUser.Text))
            {
                string strWhere = string.Empty;
                strWhere = " and a.status in(1,3) and (b.username like '%" + txtUser.Text + "%' or b.lastnamecn+b.firstnamecn like '%" + txtUser.Text + "%' or a.code like '%" + txtUser.Text + "%')";

                ESP.HumanResource.Entity.EmployeeBaseInfo userModel = null;

                if (ddlServer.SelectedValue == "0")
                    userModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(strWhere).FirstOrDefault();
                else
                {
                    userModel = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(strWhere, ddlServer.SelectedValue).FirstOrDefault();
                }
                if (userModel != null)
                {
                    hiddenUserid.Value = userModel.UserID.ToString();
                    lblReceiver.Text = userModel.FullNameCN;
                    lblReceiverCode.Text = userModel.Code;
                    lblReceiverEmail.Text = userModel.Email;
                    lblMobile.Text = userModel.MobilePhone;
                }
            }
        }

        protected void btnScrapAudit_Click(object sender, EventArgs e)
        {
            ESP.Finance.Entity.ITAssetsInfo model = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(assetId);
            //状态报废待确认
            if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Scrapping)
            {

                model.ScrapAuditDate = DateTime.Now;
                model.ScrapAuditDesc = txtScrapAudit.Text;
                model.Status=(int)ESP.Finance.Utility.FixedAssetStatus.Scrapped;

                ESP.Finance.BusinessLogic.ITAssetsManager.Update(model);

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('资产报废审核成功！');window.location='/project/Default.aspx';", true);
            }
            else if (model.Status == (int)ESP.Finance.Utility.FixedAssetStatus.Scrapping2)
            {
                model.Status = (int)ESP.Finance.Utility.FixedAssetStatus.Scrapping;
                model.ScrapLeaderDate = DateTime.Now;
                model.ScrapLeaderDesc = txtScrap.Text;

                model.ScrapAuditorId = 0;
                model.ScrapAuditor = "";

                ESP.Finance.BusinessLogic.ITAssetsManager.Update(model);

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('资产报废申请成功，请等待审核！');window.location='AssetList.aspx';", true);

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('资产报废申请状态异常！');", true);
                return;
            }
        }

        protected void ddlServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hiddenUserid.Value = string.Empty;
            this.lblReceiver.Text = "";
            this.lblReceiverCode.Text = "";
            this.lblReceiverEmail.Text = "";
            this.lblMobile.Text = "";

        }

    }
}