using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.Entity;
using ESP.Framework.BusinessLogic;

namespace SEPAdmin.UserManagement
{
    public partial class DepartmentsTypeForm : ESP.Web.UI.PageBase
    {
        private DepartmentTypeInfo CurrentDepartmentsType
        {
            get
            {
                if (null != ViewState["CurrentDepartmentsType"])
                    return (DepartmentTypeInfo)ViewState["CurrentDepartmentsType"];
                else
                {
                    return new DepartmentTypeInfo();
                }
            }
            set { ViewState["CurrentDepartmentsType"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    BindDepartmentTypeInfo(Convert.ToInt32(Request["id"]));
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save(CurrentDepartmentsType);
            //ShowCompleteMessage("修改成功", "ItemBrowse.aspx");
            Response.Redirect("DepartmentsTypeBrowse.aspx");
        }

        private void Save(DepartmentTypeInfo info)
        {            
            info.DepartmentTypeName = this.txtName.Text.Trim();
            info.Description = this.txtDes.Text.Trim();
            //info.IsSubCompany = this.chkSubCompany.Checked;
            //info.IsSaleDepartment = this.chkSales.Checked;
            //////
            if (info.DepartmentTypeID > 0)
            {
                DepartmentTypeManager.Update(info);
            }
            else
            {
                DepartmentTypeManager.Create(info);
            }
        }

        private void BindDepartmentTypeInfo(int id)
        {
            DepartmentTypeInfo info = DepartmentTypeManager.Get(id);
            if (info != null)
            {
                CurrentDepartmentsType = info;
                this.lblID.Text = info.DepartmentTypeID.ToString();
                this.txtName.Text = info.DepartmentTypeName;
                this.txtDes.Text = info.Description;
                //this.chkSales.Checked = info.IsSaleDepartment;
                //this.chkSubCompany.Checked = info.IsSubCompany;
            }
        }
    }
}