using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.project
{
    public partial class VATSetting : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtProjectCode.Text))
            {
                var plist = ESP.Finance.BusinessLogic.ProjectManager.GetList(" and projectcode='"+txtProjectCode.Text+"' or serialcode='"+txtProjectCode.Text+"'");
                if (plist.Count > 0)
                {
                    ESP.Finance.Entity.ProjectInfo model = plist[0];
                    model.IsCalculateByVAT = int.Parse(ddlContactStatus.SelectedItem.Value);
                    ESP.Finance.BusinessLogic.ProjectManager.Update(model);
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据设置成功!');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('未找到符合条件的项目!');", true);
                }
            }
        }
    }
}
