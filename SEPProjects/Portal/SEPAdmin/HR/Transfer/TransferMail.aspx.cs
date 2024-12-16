using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Transfer
{
    public partial class TransferMail :  ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    initForm(int.Parse(Request["id"].ToString()));
                }
            }
        }

        protected void initForm(int id)
        {
            imgs.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["PortalSite"] + "/images/mail_03.jpg";

            ESP.HumanResource.Entity.TransferInfo model = ESP.HumanResource.BusinessLogic.TransferManager.GetModel(id);

            lblCode.Text = model.TransCode;
            lblName.Text = model.TransName;
            lblNewDept.Text = model.NewGroupName;
            lblNewPosition.Text = model.NewPositionName;
            lblOldDept.Text = model.OldGroupName;
            lblOldPosition.Text = model.OldPositionName;
            lblTransDate.Text = model.TransInDate.ToString("yyyy-MM-dd");

            string strAuditLog = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetTransferLogInfos(model.Id, (int)ESP.HumanResource.Common.HRFormType.TransferForm);
            lblMessage.Text = strAuditLog;

        }

      
    }
}