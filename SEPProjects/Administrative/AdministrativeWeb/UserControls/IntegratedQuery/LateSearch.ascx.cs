using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;
using ComponentArt.Web.UI;

namespace AdministrativeWeb.UserControls.IntegratedQuery
{
    public partial class LateSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDepartmentInfo();
            }
        }

        #region 部门信息加载
        /// <summary>
        /// 加载一级部门
        /// </summary>
        public void LoadDepartmentInfo()
        {
            IList<DepartmentInfo> deptlist = DepartmentManager.GetAll();
            if (deptlist != null && deptlist.Count > 0)
            {
                foreach (DepartmentInfo dept in deptlist)
                { 
                    if (dept.DepartmentLevel == 1)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Text = dept.DepartmentName;
                        item.Value = dept.DepartmentID.ToString();
                        cbCompany.Items.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// 根据用户选择的一级部门加载相应的二级部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbCompany.SelectedValue))
                {
                    int companyId = int.Parse(cbCompany.SelectedValue);
                    IList<DepartmentInfo> childList = DepartmentManager.GetChildren(companyId);
                    if (childList != null && childList.Count > 0)
                    {
                        cbDepartment1.Items.Clear();
                        cbDepartment1.SelectedItem = null;
                        cbDepartment1.Text = "";
                        cbDepartment2.Items.Clear();
                        cbDepartment2.SelectedItem = null;
                        cbDepartment2.Text = "";
                        foreach (DepartmentInfo dept in childList)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Text = dept.DepartmentName;
                            item.Value = dept.DepartmentID.ToString();
                            cbDepartment1.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "", ESP.Logging.LogLevel.Error, ex, "");
            }
        }

        /// <summary>
        /// 根据用户选择的二级部门加载相应的三级部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbDepartment1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cbDepartment1.SelectedValue))
                {
                    int departmentId = int.Parse(cbDepartment1.SelectedValue);
                    IList<DepartmentInfo> childList = DepartmentManager.GetChildren(departmentId);
                    if (childList != null && childList.Count > 0)
                    {
                        cbDepartment2.Items.Clear();
                        cbDepartment2.SelectedItem = null;
                        cbDepartment2.Text = "";
                        foreach (DepartmentInfo dept in childList)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Text = dept.DepartmentName;
                            item.Value = dept.DepartmentID.ToString();
                            cbDepartment2.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ESP.Logging.Logger.Add(ex.Message, "", ESP.Logging.LogLevel.Error, ex, "");
            }
        }
        #endregion 


    }
}