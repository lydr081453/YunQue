using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;

namespace SEPAdmin.Management.UserManagement
{
    public partial class PositionManagement : System.Web.UI.Page
    {
        int Id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["Id"]))
            {
                Id = int.Parse(Request["Id"]);
            }
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private void BindInfo()
        {

            List<PositionLevelsInfo> levels = PositionLevelsManager.GetList("");
            ddlLevels.DataSource = levels;
            ddlLevels.DataTextField = "LevelName";
            ddlLevels.DataValueField = "Id";
            ddlLevels.DataBind();

            if (Id != 0)
            {
                PositionBaseInfo model = PositionBaseManager.GetModel(Id);
                if (model != null)
                {
                    txtName.Text = model.PositionName;
                    ddlLevels.SelectedValue = model.LeveId.ToString();
                }
            }
        }

        protected void btnSave_Click(object sende, EventArgs e)
        {
            PositionBaseInfo model = PositionBaseManager.GetModel(Id);
            if (model == null)
                model = new PositionBaseInfo();
            model.PositionName = txtName.Text.Trim();
            model.LeveId = int.Parse(ddlLevels.SelectedValue);
            model.LevelName = ddlLevels.SelectedItem.Text;

            if (Id > 0)
                PositionBaseManager.Update(model);
            else
                PositionBaseManager.Add(model);
            Response.Redirect("PositionList.aspx");

        }

        protected void btnBack_Click(object sende, EventArgs e)
        {
            Response.Redirect("PositionList.aspx");
        }
    }
}
