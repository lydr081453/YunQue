using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data;

namespace FinanceWeb.Return
{
    public partial class OOPSetting : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnSetting_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
               int ret = ESP.Finance.BusinessLogic.ReturnManager.SettingOOPCurrentAudit(txtCode.Text.Trim());

               if (ret == 1)
               {
                   ClientScript.RegisterStartupScript(typeof(string), "", "alert('已经调整!');", true);
               }
               else if(ret ==0)
               {
                   ClientScript.RegisterStartupScript(typeof(string), "", "alert('未找到相关单据，调整失败!');", true);
               }
            }

        }
    }
}