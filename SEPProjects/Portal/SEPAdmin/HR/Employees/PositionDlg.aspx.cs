using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Employees
{
    public partial class PositionDlg : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Databind();
            }
        }

        private void Databind()
        {
            int deptid = int.Parse(Request["DeptId"].ToString());
            ESP.HumanResource.BusinessLogic.PositionViewManager manager =new ESP.HumanResource.BusinessLogic.PositionViewManager();
            List<ESP.HumanResource.Entity.PositionView> positionlist = manager.GetModelList(deptid);
            gvList.DataSource = positionlist;
            gvList.DataBind();
        }
    }
}
