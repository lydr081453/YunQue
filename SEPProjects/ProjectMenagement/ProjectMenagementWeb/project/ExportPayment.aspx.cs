using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.project
{
    public partial class ExportPayment : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["PaymentId"]))
            {
                int paymentId = int.Parse(Request["PaymentId"]);
                ESP.Finance.BusinessLogic.PaymentManager.ExportPaymentDetail(paymentId, this.Response);
            }
            
        }
    }
}