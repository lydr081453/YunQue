using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Administrative.Common;
using ESP.Administrative.BusinessLogic;
using ESP.HumanResource.BusinessLogic;

namespace AdministrativeWeb.Audit
{
    public partial class DirectorAuditList : ESP.Web.UI.PageBase
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        protected void BindInfo()
        {
            string strWhere = " and (a.status = 1 or a.status = 3) ";

            // List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = null;
            // list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelList(UserInfo, null, strWhere);

            DataSet ds = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModel( EmployeesInPositionsManager.GetUserGroup(UserInfo.UserID.ToString()), strWhere);
            Grid1.DataSource = ds;
            Grid1.DataBind();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            BindInfo();
        }
    }
}
