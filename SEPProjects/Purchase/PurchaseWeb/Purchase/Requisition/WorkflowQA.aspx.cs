using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ESP.Supplier.Common;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class WorkflowQA : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            int prid = int.Parse(Request[RequestName.GeneralID]);
            string str = string.Empty;
            string str2 = string.Empty;

            if (string.IsNullOrEmpty(this.rdAdvance.SelectedValue) || string.IsNullOrEmpty(this.rdContent.SelectedValue) || string.IsNullOrEmpty(this.rdCoorperation.SelectedValue) ||
                string.IsNullOrEmpty(this.rdEmergency.SelectedValue) || string.IsNullOrEmpty(this.rdOne.SelectedValue) || string.IsNullOrEmpty(this.rdAdvance.SelectedValue) ||
                string.IsNullOrEmpty(this.rdPaid.SelectedValue) || string.IsNullOrEmpty(this.rdPayment.SelectedValue) || string.IsNullOrEmpty(this.rdProxy.SelectedValue) || string.IsNullOrEmpty(this.rdRisk.SelectedValue))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请完整填写评估内容！');", true);
                return;
            }
            int ret = int.Parse(this.rdAdvance.SelectedValue) + int.Parse(this.rdContent.SelectedValue) + int.Parse(this.rdCoorperation.SelectedValue) +
                      int.Parse(this.rdEmergency.SelectedValue) + int.Parse(this.rdOne.SelectedValue) + int.Parse(this.rdAdvance.SelectedValue) +
                      int.Parse(this.rdPaid.SelectedValue) + int.Parse(this.rdPayment.SelectedValue) + int.Parse(this.rdProxy.SelectedValue) +
                      int.Parse(this.rdRisk.SelectedValue);
            if (ret <= 11)//低
            {
                str = "<img src='/images/re-07.jpg' width='211' height='23' />";
                str2 = "";
            }
            else if (ret >= 17)//高
            {
                str = "<img src='/images/re-09.jpg' width='211' height='23' />";
                str2 = "<img src='/images/re-10.jpg' width='14' height='12' / style='margin:0 5px -2px 20px;'>建议您向采购部咨询";
            }
            else //中
            {
                str = "<img src='/images/re-08.jpg' width='211' height='23' />";
                str2 = "";
            }
            lblResultTitle.Text = "风险评估结果为：";
            lblResult.Text = str;
            lblResult2.Text = str2;
            ESP.Purchase.Entity.RiskConsultationInfo model = new ESP.Purchase.Entity.RiskConsultationInfo();
            model.Prid = prid;
            model.UserId = int.Parse(CurrentUser.SysID);
            model.Total = ret;
            model.CommitDate = DateTime.Now;
            ESP.Purchase.BusinessLogic.RiskConsultationManager.Add(model);

            Response.Write("<script>opener.location='/Purchase/Requisition/AddRequisitionStep6.aspx?GeneralID="+prid.ToString()+"';</script>");
        }

        protected void rdProxy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdProxy.SelectedIndex == 0 || rdOne.SelectedIndex == 0)
            {
                rdCoorperation.SelectedIndex = 1;
                rdCoorperation.Enabled = false;
            }
            else
            {
                rdCoorperation.SelectedIndex = 0;
                rdCoorperation.Enabled = false;
            }
        }

        protected void rdOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdProxy.SelectedIndex == 0 || rdOne.SelectedIndex == 0)
            {
                rdCoorperation.SelectedIndex = 1;
                rdCoorperation.Enabled = false;
            }
            else
            {
                rdCoorperation.SelectedIndex = 0;
                rdCoorperation.Enabled = false;
            }
        }

        protected void rdCoorperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdCoorperation.SelectedIndex == 0)
            {
                rdProxy.SelectedIndex = 1;
                rdOne.SelectedIndex = 1;
            }

        }
    }
}
