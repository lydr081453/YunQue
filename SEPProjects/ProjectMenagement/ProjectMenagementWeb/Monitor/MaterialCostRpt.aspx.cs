using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Monitor
{
    public partial class MaterialCostRpt : ESP.Web.UI.PageBase
    {
        protected static DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Server.ScriptTimeout = 6000000;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBeginDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                dt = ESP.Finance.BusinessLogic.ProjectManager.GetMaterialCost(this.txtBeginDate.Text, this.txtEndDate.Text);
                this.gvG.DataSource = dt;
                this.gvG.DataBind();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            dt = ESP.Finance.BusinessLogic.ProjectManager.GetMaterialCost(this.txtBeginDate.Text, this.txtEndDate.Text);
            ESP.Finance.BusinessLogic.ProjectManager.ExportOriginalDataCost(dt, this.Response);
        }
    }
}
