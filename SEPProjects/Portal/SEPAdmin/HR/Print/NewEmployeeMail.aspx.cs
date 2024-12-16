using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SEPAdmin.HR.Print
{
    public partial class NewEmployeeMail : ESP.Web.UI.PageBase
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["userid"]))
                {
                    initForm(Request["userid"].ToString());
                }

            }
        }

        protected void initForm(string userid)
        {

            imgs.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/mail_03.jpg";

            string[] userids = userid.Split(','); 
           List<ESP.HumanResource.Entity.EmployeeBaseInfo> model = EmployeeBaseManager.GetModelList(" and a.userid in (" + userid +")");


           if (model.Count > 0)
           {
               rptUserList.DataSource = model;
               rptUserList.DataBind();

               int departmentid = model[0].EmployeeJobInfo.departmentid;

               ESP.Framework.Entity.DepartmentInfo deptModel = ESP.Framework.BusinessLogic.DepartmentManager.Get(departmentid);

               List<ESP.HumanResource.Entity.UsersInfo> list2 = UsersManager.GetUserListByDepartmentID(departmentid);
               rptAdminList.DataSource = list2;
               rptAdminList.DataBind();

               DataSet ds = AuxiliaryManager.GetList(" companyID=" + deptModel.Description + " and Apply=" + ESP.HumanResource.Common.Status.WaitEntrySendMail);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = ds.Tables[0].Rows.Count - 1; i > 0; i--)
                    {
                        if ("抄送人员" == ds.Tables[0].Rows[i]["auxiliaryName"].ToString())
                        {
                            ds.Tables[0].Rows.RemoveAt(i);
                        }
                    }
                }
                rptAuxList.DataSource = ds;
                rptAuxList.DataBind();   
           }

                     
        }

        public void rptAuxList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                int auxid = int.Parse(((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[0].ToString());
                string term = string.Format(" and a.auxiliaryid={0}", auxid);
                List<SqlParameter> parmrec = new List<SqlParameter>();

                List<ESP.HumanResource.Entity.EmployeesInAuxiliariesInfo> ea = EmployeesInAuxiliariesManager.GetModelList(term, parmrec);
                Repeater repItem = (Repeater)(e.Item.FindControl("rptUserList"));

                repItem.DataSource = ea;
                repItem.DataBind();
            }
            catch { }
        }

        public void rptUserList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = (ESP.HumanResource.Entity.EmployeeBaseInfo)e.Item.DataItem;
                ESP.Administrative.Entity.OperationAuditManageInfo operation =(new ESP.Administrative.BusinessLogic.OperationAuditManageManager()).GetOperationAuditModelByUserID(model.UserID);


                Label labEmail = (Label)e.Item.FindControl("labEmail");
                labEmail.Text = model.Email;

                Label labTel = (Label)e.Item.FindControl("labTel");
                labTel.Text = model.Phone2;

                Label labFullNameCn = (Label)e.Item.FindControl("labFullNameCn");
                labFullNameCn.Text = model.LastNameCN + model.FirstNameCN;

                Label labSex = (Label)e.Item.FindControl("labSex");
                labSex.Text = ESP.HumanResource.Common.Status.Gender_Names[model.Gender];

                try
                {
                    Label labJoinDate = (Label)e.Item.FindControl("labJoinDate");
                    labJoinDate.Text = model.EmployeeJobInfo.joinDate.ToString("yyyy-MM-dd");
                }
                catch { }

                Label labJoinJob = (Label)e.Item.FindControl("labJoinJob");
                labJoinJob.Text = model.EmployeeJobInfo.joinJob;

                Label labCompanyName = (Label)e.Item.FindControl("labCompanyName");
                labCompanyName.Text = model.EmployeeJobInfo.companyName;
                Label labDepartmentName = (Label)e.Item.FindControl("labDepartmentName");
                labDepartmentName.Text = model.EmployeeJobInfo.departmentName;
                Label labGroupName = (Label)e.Item.FindControl("labGroupName");

                labGroupName.Text = model.EmployeeJobInfo.groupName;

                if(operation!=null)
                labGroupName.Text = model.EmployeeJobInfo.groupName + "(" + operation.TeamLeaderName + ")";

                Label labJob_Memo = (Label)e.Item.FindControl("labJob_Memo");
                labJob_Memo.Text = model.Memo;
                if (model.OwnedPC == true)
                {
                    labJob_Memo.Text = "自带笔记本电脑;   " + model.Memo;
                }
                else
                {
                    labJob_Memo.Text = "公司提供;   " + model.Memo;
                }

                try
                {
                    Label labWorkComp = (Label)e.Item.FindControl("labWorkComp");

                    labWorkComp.Text = model.WorkCity;                        

                }
                catch { }
            }
 
        }
    }
           
}
