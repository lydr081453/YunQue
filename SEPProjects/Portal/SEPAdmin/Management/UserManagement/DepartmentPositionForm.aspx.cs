using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;
using System.Data.SqlClient;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;

namespace SEPAdmin.Management.UserManagement
{
    public partial class DepartmentPositionForm : ESP.Web.UI.PageBase
    {
         private DepartmentPositionInfo CurrentDepartment
        {
            get
            {
                if (null != ViewState["CurrentDepartment"])
                    return (DepartmentPositionInfo)ViewState["CurrentDepartment"];
                else
                {
                    return new DepartmentPositionInfo();
                }
            }
            set { ViewState["CurrentDepartment"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
                ListBind();
            }
        }        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save(CurrentDepartment);
            if (!string.IsNullOrEmpty(Request["backurl"]))
                Response.Redirect(Request["backurl"] + ".aspx");
            else
                Response.Redirect("DepartmentsTree.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["backurl"]))
                Response.Redirect(Request["backurl"] + ".aspx");
            else
                Response.Redirect("DepartmentsTree.aspx");
        }

        private void Bind()
        {
             
             if (!string.IsNullOrEmpty(Request["depposID"]))
             {
                 DepartmentPositionInfo info = DepartmentPositionManager.Get(Convert.ToInt32(Request["depposID"]));
                 CurrentDepartment = info;
                 if (info != null)
                 {
                     lblID.Text = info.DepartmentID.ToString();
                     this.txtName.Text = info.DepartmentPositionName;
                     this.txtDescription.Text = info.Description;
                     this.ddlPositionLevel.SelectedValue = info.PositionLevel.ToString();

                     PositionBaseInfo pos = PositionBaseManager.GetModel(info.PositionBaseId);
                     hidPositionLevelId.Value = pos.LeveId.ToString();
                     hidPositionBaseId.Value = pos.Id.ToString();
                     PositionLevelsInfo lev = PositionLevelsManager.GetModel(pos.LeveId);
                     if (lev != null)
                     {
                         txtChargeRate.Text = lev.ChargeRate.ToString();
                         txtSalaryHigh.Text = lev.SalaryHigh.ToString();
                         txtSalaryLow.Text = lev.SalaryLow.ToString();
                     }
                 }   
             }
        }

        private void Save(DepartmentPositionInfo info)
        {
            info.DepartmentPositionName = this.txtName.Text.Trim();
            info.Description = this.txtDescription.Text.Trim();
            info.PositionLevel = Convert.ToInt32(this.ddlPositionLevel.SelectedValue);
            info.PositionBaseId = int.Parse(hidPositionLevelId.Value == "" ? "0" : hidPositionBaseId.Value);

            if (!string.IsNullOrEmpty(Request["depposid"]))
                DepartmentPositionManager.Update( info);
            else
            {
                info.DepartmentID = string.IsNullOrEmpty(Request["depid"]) ? 0 : Convert.ToInt32(Request["depid"]);
                DepartmentPositionManager.Create( info);
            }


            CurrentDepartment = info;
        }

        protected void lnkLink_Click(object sender, EventArgs e)
        {
            int Id = int.Parse(((LinkButton)sender).CommandArgument.ToString());
            PositionBaseInfo model = PositionBaseManager.GetModel(Id);
            PositionLevelsInfo lev = PositionLevelsManager.GetModel(model.LeveId);
            if (model != null)
            {
                txtName.Text = model.PositionName;
                txtChargeRate.Text = lev.ChargeRate.ToString();
                txtSalaryHigh.Text = lev.SalaryHigh.ToString();
                txtSalaryLow.Text = lev.SalaryLow.ToString();
                
                hidPositionLevelId.Value = model.LeveId.ToString();
                hidPositionBaseId.Value = model.Id.ToString();
            }
        }

        private void ListBind()
        {
            string terms = "";
            List<SqlParameter> parms = new List<SqlParameter>();

            if (txtKey.Text.Trim() != "")
            {
                terms += " and PositionName like '%'+@key+'%' or LevelName like '%'+@key+'%'";
                parms.Add(new SqlParameter("@key", txtName.Text.Trim()));
            }

            gvList.DataSource = PositionBaseManager.GetList(terms, parms);
            gvList.DataBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }
    
    }
}
