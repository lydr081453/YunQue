﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Common;

namespace SEPAdmin.HR.SocialSecurity
{
    public partial class SocialSecurityList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listBind();
            }
        }

        protected void listBind()
        {
            string strCondition = string.Empty;
            
            List<ESP.HumanResource.Entity.SocialSecurityInfo> list = null;
            if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
            {
                list = ESP.HumanResource.BusinessLogic.SocialSecurityManager.GetModelList("");
            }
            else
            {
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> listeip = EmployeesInPositionsManager.GetModelList(" a.userID = " + UserInfo.UserID);
                if(listeip.Count > 0)
                list = ESP.HumanResource.BusinessLogic.SocialSecurityManager.GetModelList(" SocialInsuranceCompany=" + listeip[0].CompanyID);
            }
            gvE.DataSource = list;
            gvE.DataBind();


            if (gvE.PageCount > 1)
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
            labPageCount.Text = labPageCountT.Text = (gvE.PageIndex + 1).ToString() + "/" + gvE.PageCount.ToString();
            if (gvE.PageCount > 0)
            {
                if (gvE.PageIndex + 1 == gvE.PageCount)
                    disButton("last");
                else if (gvE.PageIndex == 0)
                    disButton("first");
                else
                    disButton("");
            }
        }

        #region 翻页相关
        protected void gvE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvE.PageIndex = e.NewPageIndex;
            listBind();
        }


        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvE.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvE.PageIndex + 2) >= gvE.PageCount ? gvE.PageCount : (gvE.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvE.PageIndex - 1) < 1 ? 0 : (gvE.PageIndex - 1));
        }
        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvE_PageIndexChanging(new object(), e);
        }

        //翻页判断
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