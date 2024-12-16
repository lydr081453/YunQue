using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Finance.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrativeWeb.RequestForSeal
{
    public partial class RequestForSealAuditList : ESP.Web.UI.PageBase
    {
        RequestForSealManager manager = new RequestForSealManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        private void ListBind()
        {
            string strWhere = " and b.auditorUserId=" + CurrentUser.SysID;
            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
                strWhere += " and (FileName like '%" + txtKey.Text.Trim() + "%' or requestorName like '%" + txtKey.Text.Trim() + "%'or datanum like '%" + txtKey.Text.Trim() + "%' or saNo like '%" + txtKey.Text.Trim() + "%')";
            var list = manager.GetAuditList(strWhere);
            gvList.DataSource = list;
            gvList.DataBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var model = (RequestForSealInfo)e.Row.DataItem;

                HyperLink hylAudit = (HyperLink)e.Row.FindControl("hylAudit");
                hylAudit.Visible = checkAudit(model);
            }
        }

        /// <summary>
        /// 当前登录人是否为待审批人
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        private bool checkAudit(RequestForSealInfo model)
        {
            return model.AuditorId == CurrentUserID && model.Status == ESP.Administrative.Common.Status.RequestForSealStatus.Auditing;
            //var waitAuditor = ConsumptionAuditManager.GetList(" batchId="+ dataId + " and AuditStatus=0" + " and FormType=" + (int)ESP.Finance.Utility.FormType.RequestForSeal)
            //                                        .OrderBy(x=>x.SquenceLevel).FirstOrDefault();
            //if (waitAuditor != null && waitAuditor.AuditorUserID == CurrentUserID)
            //    return true;
            //return false;
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }
    }
}