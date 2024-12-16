using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Actity
{
    public partial class RecentActity : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetRecentActity();
        }

        private void GetRecentActity()
        {
            ESP.HumanResource.BusinessLogic.ActityManager actityManager = new ESP.HumanResource.BusinessLogic.ActityManager();
            ESP.HumanResource.Entity.ActityInfo actityInfo = actityManager.GetModel();
            this.lblTitle.Text = actityInfo.ActityTitle;
            this.lblTime.Text = actityInfo.ActityTime.ToString();
            this.lblLecturer.Text = actityInfo.Lecturer;
            this.txtContent.Text = actityInfo.ActityContent;
            this.lblId.Text = actityInfo.Id.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('此功能待完善')</script>");
            Server.Transfer("ListActity.aspx");
        }
    }
}
