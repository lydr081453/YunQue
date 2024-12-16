using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Dimission.Controls
{
    public partial class DirectorView : ESP.Web.UI.UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvDetailView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                Label lab = e.Row.FindControl("labReceiverStatus") as Label;

                Label labReceiver = e.Row.FindControl("labReceiverName") as Label;
                HiddenField hid = e.Row.FindControl("hidReceiverName") as HiddenField;

                ESP.HumanResource.Entity.DimissionDetailsInfo detailsInfo = e.Row.DataItem as ESP.HumanResource.Entity.DimissionDetailsInfo;
                if (detailsInfo.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit)
                    lab.Text = "未确认";
                else if (detailsInfo.Status == (int)ESP.HumanResource.Common.AuditStatus.Audited)
                    lab.Text = "已确认";
                else
                    lab.Text = "已驳回";

                labReceiver.Text = detailsInfo.ReceiverName;
                hid.Value = detailsInfo.ReceiverId.ToString();
            }
        }
    }
}