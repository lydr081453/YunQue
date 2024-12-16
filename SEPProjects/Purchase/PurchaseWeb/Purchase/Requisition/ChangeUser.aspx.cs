using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class ChangeUser : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void lnkPost_Click(object sender, EventArgs e)
        {
            string[] oldUser = hidOldUser.Value.Split('#');
            txtOldUser.Text = oldUser[1];
            hidOldUser.Value = oldUser[0];
            prListBind();
            pnListBind();
            pjListBind();
            spListBind();
            kqListBind();
            bxListBind();
        }

        protected void lnkPost1_Click(object sender, EventArgs e)
        {
            string[] newUser = hidNewUser.Value.Split('#');
            txtNewUser.Text = newUser[1];
            hidNewUser.Value = newUser[0];
        }

        #region PR单

        private void prListBind()
        {
            grPR.DataSource = ESP.Purchase.BusinessLogic.GeneralInfoManager.getGeneralJoinOperationAudit(int.Parse(hidOldUser.Value),radUserType.SelectedValue);
            grPR.DataBind();
        }

        protected void radUserType_SelectIndexChanged(object sender, EventArgs e)
        {
            prListBind();
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            int count = 0;
            if(radUserType.SelectedValue != "auditorId")
                count = ESP.Purchase.BusinessLogic.GeneralInfoManager.ChangePrUsers(Request["chkPR"], radUserType.SelectedValue, int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser, Request);
            else
                count = ESP.Purchase.BusinessLogic.GeneralInfoManager.ChangePrOperationAuditor(Request["chkPR"], int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser, Request);
            if ( count > 0)
            {
                prListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        /// <summary>
        /// 申请人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnR1_Click(object sender, EventArgs e)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.ChangePrUsers(Request["chkPR"], "requestor", int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value),CurrentUser,Request) > 0)
            {
                prListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        /// <summary>
        /// 收货人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnR2_Click(object sender, EventArgs e)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.ChangePrUsers(Request["chkPR"], "goods_receiver", int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser, Request) > 0)
            {
                prListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        /// <summary>
        /// 附加收货人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnR3_Click(object sender, EventArgs e)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.ChangePrUsers(Request["chkPR"], "appendreceiver", int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser, Request) > 0)
            {
                prListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        /// <summary>
        /// 分公司审核人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnR4_Click(object sender, EventArgs e)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.ChangePrUsers(Request["chkPR"], "Filiale_Auditor", int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser, Request) > 0)
            {
                prListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        /// <summary>
        /// 物料审核人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnR5_Click(object sender, EventArgs e)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.ChangePrUsers(Request["chkPR"], "first_assessor", int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser, Request) > 0)
            {
                prListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        /// <summary>
        /// 采购总监
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnR6_Click(object sender, EventArgs e)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.ChangePrUsers(Request["chkPR"], "purchaseauditor", int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser, Request) > 0)
            {
                prListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        /// <summary>
        /// 媒介审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnR7_Click(object sender, EventArgs e)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.ChangePrUsers(Request["chkPR"], "mediaauditor", int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser, Request) > 0)
            {
                prListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        /// <summary>
        /// AD审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnR8_Click(object sender, EventArgs e)
        {
            if (ESP.Purchase.BusinessLogic.GeneralInfoManager.ChangePrUsers(Request["chkPR"], "adauditor", int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser, Request) > 0)
            {
                prListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        #endregion

        #region 付款申请
        private void pnListBind()
        {
            grPN.DataSource = ESP.Finance.BusinessLogic.ReturnManager.GetReturnListJoinHist(int.Parse(hidOldUser.Value));
            grPN.DataBind();
        }

        /// <summary>
        /// 申请人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnN1_Click(object sender, EventArgs e)
        {
            if (ESP.Finance.BusinessLogic.ReturnManager.changeRequestor(Request["chkPN"], int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value),CurrentUser) > 0)
            {
                pnListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        /// <summary>
        /// 下级审核人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnN2_Click(object sender, EventArgs e)
        {
            if (ESP.Finance.BusinessLogic.ReturnManager.changAuditor(Request["chkPN"], int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value),CurrentUser) > 0)
            {
                pnListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }
        #endregion

        #region 项目号

        private void pjListBind()
        {
            grPJ.DataSource = ESP.Finance.BusinessLogic.ProjectManager.GetProjectListJoinHist(int.Parse(hidOldUser.Value));
            grPJ.DataBind();
        }

        protected void btnPJ1_Click(object sender, EventArgs e)
        {
            if (ESP.Finance.BusinessLogic.ProjectManager.changeApplicant(Request["chkPJ"], int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value),CurrentUser) > 0)
            {
                pjListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        protected void btnPJ2_Click(object sender, EventArgs e)
        {
            if (ESP.Finance.BusinessLogic.ProjectManager.changAuditor(Request["chkPJ"], int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser) > 0)
            {
                pjListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        #endregion

        #region 支持方

        private void spListBind()
        {
            grSP.DataSource = ESP.Finance.BusinessLogic.SupporterManager.GetSupportListJoinHist(int.Parse(hidOldUser.Value));
            grSP.DataBind();
        }

        protected void btnSP1_Click(object sender, EventArgs e)
        {
            if (ESP.Finance.BusinessLogic.SupporterManager.changeLeader(Request["chkSP"], int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser) > 0)
            {
                spListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        protected void btnSP2_Click(object sender, EventArgs e)
        {
            if (ESP.Finance.BusinessLogic.SupporterManager.changAuditor(Request["chkSP"], int.Parse(hidOldUser.Value), int.Parse(hidNewUser.Value), CurrentUser) > 0)
            {
                spListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }

        #endregion

        #region 考勤

        private void kqListBind()
        {
            ESP.Administrative.BusinessLogic.ApproveLogManager manager = new ESP.Administrative.BusinessLogic.ApproveLogManager();
            grKQ.DataSource = manager.GetWaitApproveList(int.Parse(hidOldUser.Value));
            grKQ.DataBind();
        }

        protected void btnKQ_Click(object sender, EventArgs e)
        {
            ESP.Administrative.BusinessLogic.ApproveLogManager logManager = new ESP.Administrative.BusinessLogic.ApproveLogManager();
            if (logManager.UpdateWaitApproveInfo(Request["chkKQ"], int.Parse(hidNewUser.Value),txtNewUser.Text.Trim()) > 0)
            {
                kqListBind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('变更人员失败！');", true);
            }
        }
        #endregion

        #region 报销
        private void bxListBind()
        {

        }



        #endregion
    }
}
