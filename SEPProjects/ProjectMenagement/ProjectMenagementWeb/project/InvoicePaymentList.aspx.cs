using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.Entity;
using System.Data;
using ESP.Finance.BusinessLogic;

namespace FinanceWeb.project
{
    public partial class InvoicePaymentList : ESP.Web.UI.PageBase
    {
        int pid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["pid"]))
                pid = int.Parse(Request["pid"]);

            if (!IsPostBack)
                ListBind();
        }

        private void ListBind()
        {
            var plist = PaymentManager.GetListByProject(pid, "",null);
            gvPayment.DataSource = plist;
            gvPayment.DataBind();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string paymentIds = Request["chk"];
            Response.Write("<script>window.parent.bindList('" + paymentIds + "');parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();parent.hidselect();</script>");

        }
    }
}
