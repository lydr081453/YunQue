using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using ESP.Framework.BusinessLogic;
using System.Net.Mail;
using System.IO;
using ESP.HumanResource.Common;
using SEPAdmin.Management.UserManagement;


namespace SEPAdmin.HR.Join
{
    public partial class InternConfirmPrint : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["UserId"]))
                {
                    int userid = int.Parse(Request["UserId"]);

                    ESP.HumanResource.Entity.UsersInfo userInfo = UsersManager.GetModel(userid);
                    ESP.HumanResource.Entity.EmployeeBaseInfo employeeBaseInfo = EmployeeBaseManager.GetModel(userid);
                   
                    if (userInfo != null)
                    {
                        labUserName.Text = userInfo.LastNameCN + userInfo.FirstNameCN;
                        labJoinDate.Text = employeeBaseInfo.EmployeeJobInfo.joinDate.ToString("yyyy年MM月dd日");
                        if (employeeBaseInfo.TypeID == 6)//劳务派遣
                        {
                            trPaiqian.Style["display"] = "block";
                        }
                        
                        if (employeeBaseInfo.EmployeeJobInfo.companyid == 230)//重庆
                        {
                            labAddress.Text = "重庆市渝北区大竹林街道杨柳路6号三狼公园6号D4-102";
                            lblCard.Text = "";
                            lblPhone.Text = "";
                            
                        }
                        else 
                        {
                            labAddress.Text = "北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼";
                            lblCard.Text = "北京：";
                            lblPhone.Text = "（北京）010-8509 5766";
                           
                        }
                    }
                   
                }
               
            }
        }

    }
}
