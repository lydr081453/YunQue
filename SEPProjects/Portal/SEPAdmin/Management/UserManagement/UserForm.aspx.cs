using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
namespace SEPAdmin.UserManagement
{
    public partial class UserForm : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["userid"]))
                {
                    ListBind();
                }
            }

        }
        private void ListBind()
        {
            string strWhere = string.Format(" a.UserID={0}",Request["userid"]);
          
            IList<EmployeePositionInfo> list = DepartmentPositionManager.GetEmployeePositions(int.Parse(Request["userid"]));
            if (list.Count > 0)
            {
                ESP.HumanResource.Entity.UsersInfo user = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(int.Parse(Request["userid"]));
                
                labName.Text = list[0].UsernameCN.Trim();
                labEmail.Text = user.Email;
                labItCode.Text = user.Username;
                labUserId.Text = user.UserID.ToString();
            }
            gvList.DataSource = list;
            gvList.DataBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                CheckBox cbox1 = (CheckBox)e.Row.FindControl("CheckBox1");
                CheckBox cbox2 = (CheckBox)e.Row.FindControl("CheckBox2");
                if (gvList.DataKeys[e.Row.RowIndex].Values[1].ToString() == "True")
                    cbox1.Checked = true;
                if (gvList.DataKeys[e.Row.RowIndex].Values[2].ToString() == "True")
                    cbox2.Checked = true;
               
            }
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                int userid = int.Parse(gvList.DataKeys[i].Values[0].ToString());
                int departmentid = int.Parse(gvList.DataKeys[i].Values[3].ToString());
                int depPositionid = int.Parse(gvList.DataKeys[i].Values[4].ToString());
                CheckBox cbox1 = (CheckBox)gvList.Rows[i].FindControl("CheckBox1");
                CheckBox cbox2 = (CheckBox)gvList.Rows[i].FindControl("CheckBox2");
                
                EmployeePositionInfo eip = DepartmentPositionManager.GetEmployeePosition(userid, depPositionid, departmentid);
                if (cbox1.Checked)
                    eip.IsManager = true;
                else
                    eip.IsManager = false;
                if (cbox2.Checked)
                    eip.IsActing = true;
                else
                    eip.IsActing = false;

                DepartmentPositionManager.UpdateEmployeePosition(eip);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserBrowse.aspx");
        }
    }
}
