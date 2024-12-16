using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrativeWeb.RequestForSeal
{
    public partial class RequestForSealSearch : ESP.Web.UI.PageBase
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
            string strWhere = " status=" + (int)ESP.Administrative.Common.Status.RequestForSealStatus.Audited;
            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
                strWhere += " and (FileName like '%" + txtKey.Text.Trim() + "%' or requestorName like '%" + txtKey.Text.Trim() + "%'or datanum like '%" + txtKey.Text.Trim() + "%' or saNo like '%" + txtKey.Text.Trim() +"%')";
            var list = manager.GetList(strWhere);
            gvList.DataSource = list;
            gvList.DataBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var model = (RequestForSealInfo)e.Row.DataItem;

            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }
    }
}