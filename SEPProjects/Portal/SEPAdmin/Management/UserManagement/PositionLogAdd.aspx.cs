using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Management.UserManagement
{
    public partial class PositionLogAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userid = int.Parse(Request["userid"]);
            ESP.HumanResource.Entity.PositionLogInfo log = new ESP.HumanResource.Entity.PositionLogInfo();
            log.BeginDate = DateTime.Parse(txtBeginDate.Text);
            log.EndDate = DateTime.Parse(txtEndDate.Text);
            log.CreateDate = DateTime.Now;
            log.PositionName = txtPosition.Text;
            log.DepartmentName = txtDept.Text;
            log.UserId = userid;

            ESP.HumanResource.BusinessLogic.PositionLogManager.Add(log);

            string str = string.Format("var win = art.dialog.open.origin;win.location.reload();");

            ClientScript.RegisterStartupScript(typeof(string), "", str, true);
        }
    }
}
