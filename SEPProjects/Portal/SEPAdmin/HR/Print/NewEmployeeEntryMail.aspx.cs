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
    public partial class NewEmployeeEntryMail : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["userid"]) && !string.IsNullOrEmpty(Request["keyno"]))
                {
                    initForm(Request["userid"].ToString(), Request["keyno"].ToString());
                }
            }
        }

        protected void initForm(string userid, string keyno)
        {
           // imgs.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/mail_03.jpg";

            string[] userids = userid.Split(',');
            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeeBaseManager.GetModelList(" and a.userid in (" + userid + ")");

            if (list.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.EmployeeBaseInfo info in list)
                {
                    if (!string.IsNullOrEmpty(keyno))
                    {
                        if (keyno.Trim() != "0")
                        {
                            info.Memo = keyno.Trim();
                        }
                        else
                        {
                            info.Memo = "门禁绑定失败，请手动绑定门禁卡！";
                        }
                    }
                }
                rptUserList.DataSource = list;
                rptUserList.DataBind();
            }
        }

        public void rptUserList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = (ESP.HumanResource.Entity.EmployeeBaseInfo)e.Item.DataItem;

                Label labFullNameCn = (Label)e.Item.FindControl("labFullNameCn");
                labFullNameCn.Text = model.LastNameCN + model.FirstNameCN;

                Label labUserCode = (Label)e.Item.FindControl("labUserCode");
                labUserCode.Text = model.Code;

                Label labCompanyName = (Label)e.Item.FindControl("labCompanyName");
                labCompanyName.Text = model.EmployeeJobInfo.companyName;
                Label labDepartmentName = (Label)e.Item.FindControl("labDepartmentName");
                labDepartmentName.Text = model.EmployeeJobInfo.departmentName;
                Label labGroupName = (Label)e.Item.FindControl("labGroupName");
                labGroupName.Text = model.EmployeeJobInfo.groupName;

                Label labKeyNo = (Label)e.Item.FindControl("labKeyNo");
                Label labTel = (Label)e.Item.FindControl("labTel");


                Label labAddress = (Label)e.Item.FindControl("labAddress");
                labAddress.Text = model.WorkCity;

                labKeyNo.Text = model.Memo;

                labTel.Text = model.Phone2;

                if (model.OwnedPC == true)
                {
                    labKeyNo.Text = "自带笔记本电脑;   " + model.Memo;
                }
            }
        }
    }
}
