using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.project
{
    public partial class PRSync : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSync_onclick(object sender, EventArgs e)
        {
            int ret = ESP.Finance.BusinessLogic.ReturnManager.PRProcessSync();
            txtCount.Text = ret.ToString();
        }

        protected void btnAddLog_Click(object sender, EventArgs e)
        {
            int ret = ESP.Finance.BusinessLogic.ReturnManager.AddOOPLog();
            txtOOP.Text = ret.ToString();
        }
    }
}
