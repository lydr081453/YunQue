using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.HR.Auxiliary
{
    public partial class EmployeesInAuxiliariesEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindUserList();
            }
        }

        private void BindUserList()
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" and a.auxiliaryid=@auxiliaryid");
            parms.Add(new SqlParameter("@auxiliaryid",int.Parse(Request["auxiliaryid"]) ));

            AuxiliaryInfo model = AuxiliaryManager.GetModel(int.Parse(Request["auxiliaryid"]));
            labHeader.Text = model.auxiliaryName;
            hidAux.Value = Request["auxiliaryid"];
            List<EmployeesInAuxiliariesInfo> list = EmployeesInAuxiliariesManager.GetModelList(strWhere.ToString(), parms);
            gvUser.DataSource = list;
            gvUser.DataBind();
        }

        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                GridView view = (GridView)sender;
                int auxiliaryid = int.Parse(view.DataKeys[int.Parse(e.CommandArgument.ToString())].Values[1].ToString());
                int userid = int.Parse(view.DataKeys[int.Parse(e.CommandArgument.ToString())].Values[0].ToString());
                AuxiliaryInfo info = AuxiliaryManager.GetModel(auxiliaryid);

                LogInfo logmodel = new LogInfo();
                logmodel.Des = string.Format("[{0}]删除了待入职的辅助工作[{1}]的人员", UserInfo.FullNameCN, info.auxiliaryName);
                logmodel.Status = 0;
                logmodel.LogUserId = UserID;
                logmodel.LogUserName = UserInfo.FullNameEN;
                logmodel.LogMedifiedTeme = DateTime.Now;
                EmployeesInAuxiliariesManager.Delete(userid,auxiliaryid,logmodel);
                BindUserList();
            }
        }

        protected void btnAddUsers_Command(object sender, CommandEventArgs e)
        {
            string val = hdnAddUsers.Value;
            string[] arr = val.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int userid = 0;
            foreach (string s in arr)
            {
                int.TryParse(s, out userid);
                if (userid > 0 && !EmployeesInAuxiliariesManager.Exists(userid, int.Parse(hidAux.Value.Trim())))
                {
                    EmployeesInAuxiliariesInfo model = new EmployeesInAuxiliariesInfo();
                    model.userId = userid;
                    model.auxiliaryId = int.Parse(hidAux.Value.Trim());
                    AuxiliaryInfo info = AuxiliaryManager.GetModel(int.Parse(hidAux.Value.Trim()));

                    LogInfo logmodel = new LogInfo();
                    logmodel.Des = string.Format("[{0}]添加了待入职的辅助工作[{1}]的人员", UserInfo.FullNameCN, info.auxiliaryName);
                    logmodel.Status = 0;
                    logmodel.LogUserId = UserID;
                    logmodel.LogUserName = UserInfo.FullNameEN;
                    logmodel.LogMedifiedTeme = DateTime.Now;
                    int returnValue = EmployeesInAuxiliariesManager.Add(model, logmodel);
                }
            }
            BindUserList();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AuxiliaryList.aspx");
        }
    }
}
