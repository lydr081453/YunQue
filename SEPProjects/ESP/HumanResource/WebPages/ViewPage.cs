using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.WebPages
{
    public class ViewPage : ESP.Web.UI.PageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                string userid = Request["userid"].Trim();
                string hradmin = "";
                // 要访问的用户
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", userid));
                // 当前用户
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinpos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(" a.userid=" + this.UserID);
                if (empinposlist.Count > 0 && empinpos.Count > 0)
                {
                    if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("004", this.ModuleInfo.ModuleID, this.UserID))
                    {
                        int vil = 0;
                        for (int i = 0; i < empinposlist.Count; i++)
                        {
                            if (empinpos[0].CompanyID == empinposlist[i].CompanyID)
                            {
                                vil = 1;
                                break;
                            }
                        }
                        if (vil == 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='/hr/default.aspx';", true);
                        }
                    }

                    if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("002", this.ModuleInfo.ModuleID, this.UserID))
                    {
                        hradmin = "培恩公关";

                        int vil = 0;
                        for (int i = 0; i < empinposlist.Count; i++)
                        {
                            IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[i].CompanyID);
                            int depid = IsStringInArray(hradmin, deplist);
                            if (depid > 0)
                            {
                                vil = 1;
                                break;
                            }
                        }
                        if (vil == 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='/hr/default.aspx';", true);
                        }
                    }
                    else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("003", this.ModuleInfo.ModuleID, this.UserID))
                    {
                        hradmin = "TCG";
                        int vil = 0;
                        for (int i = 0; i < empinposlist.Count; i++)
                        {
                            IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[i].CompanyID);
                            int depid = IsStringInArray(hradmin, deplist);
                            if (depid > 0)
                            {
                                vil = 1;
                                break;
                            }
                        }
                        if (vil == 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='/hr/default.aspx';", true);
                        }
                    }
                    else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("005", this.ModuleInfo.ModuleID, this.UserID))
                    {
                        hradmin = "国际公关";
                        int vil = 0;
                        for (int i = 0; i < empinposlist.Count; i++)
                        {
                            IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[i].CompanyID);
                            int depid = IsStringInArray(hradmin, deplist);
                            if (depid > 0)
                            {
                                vil = 1;
                                break;
                            }
                        }
                        if (vil == 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='/hr/default.aspx';", true);
                        }
                    }
                    else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("006", this.ModuleInfo.ModuleID, this.UserID))
                    {
                        hradmin = "集团";
                        int vil = 0;
                        for (int i = 0; i < empinposlist.Count; i++)
                        {
                            IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[i].CompanyID);
                            int depid = IsStringInArray(hradmin, deplist);
                            if (depid > 0)
                            {
                                vil = 1;
                                break;
                            }
                        }
                        if (vil == 0)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='/hr/default.aspx';", true);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='/hr/default.aspx';", true);
                }
            }
            base.OnLoad(e);
        }

        private static int IsStringInArray(string s, IList<ESP.Framework.Entity.DepartmentInfo> array)
        {
            if (array == null || s == null)
                return 0;

            for (int i = 0; i < array.Count; i++)
            {
                if (string.Compare(s, array[i].DepartmentName, true) == 0)
                {
                    return array[i].DepartmentID;
                }
            }
            return 0;
        }
    }
}
