/*
 * 
 * 按月离职人员发信模板
 * 
 */
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
    public partial class DimissionMail : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["year"]) && !string.IsNullOrEmpty(Request["month"]))
                {
                    initForm(Request["year"].ToString(), Request["month"].ToString());
                }

            }
        }

        protected void initForm(string year,string month)
        {

            imgs.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/mail_03.jpg";

            labYear.Text = year;
            labMonth.Text = month;

            string date = year + "-" + month;

            List<ESP.HumanResource.Entity.DimissionInfo> dimlist = DimissionManager.GetModelList(string.Format(" and dimissionDate >='{0}' and dimissionDate <='{1}' order by companyID desc", date + "-01", date + "-" + DateTime.DaysInMonth(int.Parse(year), int.Parse(month))));

            if (dimlist.Count > 0)
            {
                rptDimission.DataSource = dimlist;
                rptDimission.DataBind();               
            }


        }

        public void rptDimission_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ESP.HumanResource.Entity.DimissionInfo model = (ESP.HumanResource.Entity.DimissionInfo)e.Item.DataItem;
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> eip = EmployeesInPositionsManager.GetModelList(" a.userid="+model.userId);
                ESP.HumanResource.Entity.SnapshotsInfo snap = SnapshotsManager.GetModel(model.snapshotsId);

                Label labFullNameCn = (Label)e.Item.FindControl("labFullNameCn");
                labFullNameCn.Text = model.userName;                

                try
                {
                    Label labDimissionDate = (Label)e.Item.FindControl("labDimissionDate");
                    labDimissionDate.Text = model.dimissionDate.ToString("yyyy-MM-dd");
                }
                catch { }

                if (snap != null)
                {
                    Label labSIMonth = (Label)e.Item.FindControl("labSIMonth");
                    labSIMonth.Text = snap.endowmentInsuranceEndTime.Month.ToString();

                    Label labMIMonth = (Label)e.Item.FindControl("labMIMonth");
                    labMIMonth.Text = snap.medicalInsuranceEndTime.Month.ToString();

                }
                if (eip.Count > 0)
                {
                    Label labCompany = (Label)e.Item.FindControl("labCompany");
                    labCompany.Text = eip[0].CompanyName;
                    Label labDepartmentName = (Label)e.Item.FindControl("labDepartmentName");
                    labDepartmentName.Text = eip[0].DepartmentName;
                    Label labGroupName = (Label)e.Item.FindControl("labGroupName");
                    labGroupName.Text = eip[0].GroupName;
                    Label labPositions = (Label)e.Item.FindControl("labPositions");
                    labPositions.Text = eip[0].DepartmentPositionName;
                    Label labMail = e.Item.FindControl("labMail") as Label;
                    labMail.Text = eip[0].Email;
                }
                
            }

        }
    }
}
