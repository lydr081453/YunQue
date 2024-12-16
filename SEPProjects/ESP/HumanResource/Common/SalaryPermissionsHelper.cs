using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.BusinessLogic;

namespace ESP.HumanResource.Common
{
    public class SalaryPermissionsHelper
    {
        public static bool IsPermissions(string EmployeeUserID,int ModuleID,int UserID)
        {
            
               // string userid = Request["userid"].Trim();
                //string hradmin = "";
                //公司高级人员
                string ss = System.Configuration.ConfigurationManager.AppSettings["HighClass"];
                //要访问的用户
                List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", EmployeeUserID));                
                if (empinposlist.Count > 0)
                {
                    if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("007", ModuleID,UserID))
                    {                        
                        return true;
                    }
                    else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("008", ModuleID, UserID))
                    {
                        int index = ss.IndexOf(empinposlist[0].UserID.ToString());
                        if (index >= 0)
                        {
                            return false;
                        }
                        return true;
                       
                    }
                    else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("009", ModuleID,UserID))
                    {
                        int index = ss.IndexOf(empinposlist[0].UserID.ToString());
                        if (index > 0)
                        {
                            return false;
                        }
                        for (int i = 0; i < empinposlist.Count; i++)
                        {
                            int indexH = empinposlist[i].DepartmentPositionName.IndexOf("总监");
                            if (indexH >= 0)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("010", ModuleID, UserID))
                    {
                        ESP.HumanResource.Entity.SnapshotsInfo snapshote = SnapshotsManager.GetTopModel(Convert.ToInt32(EmployeeUserID));
                        //固定工资
                        string basepay = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(snapshote.newBasePay) ? snapshote.newBasePay.Trim() : "0");
                        //绩效
                        string basemerit = ESP.Salary.Utility.DESEncrypt.Decode(!string.IsNullOrEmpty(snapshote.newMeritPay) ? snapshote.newMeritPay.Trim() : "0");
                        decimal pay = Convert.ToDecimal(basepay) + Convert.ToDecimal(basemerit);
                        decimal salary = decimal.MaxValue;
                        int[] depts = new ESP.Compatible.Employee(UserID).GetDepartmentIDs();
                        int dep = 0;
                        foreach (int dp in depts)
                        {
                            ESP.Framework.Entity.DepartmentInfo info = ESP.Framework.BusinessLogic.DepartmentManager.Get(dp);
                            while (info.ParentID != 0)
                            {
                                info = ESP.Framework.BusinessLogic.DepartmentManager.Get(info.ParentID);
                            }
                            dep = info.DepartmentID;
                            if (dep == 18)
                                break;
                        }
                        if(dep == 18)
                            salary = SalaryPermissionsManager.GetModel(2).SalaryPermissions;
                        else
                            salary = SalaryPermissionsManager.GetModel(1).SalaryPermissions;


                        if (salary <= pay) return false;

                        return true;

                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
             
            }
       
        }
  
}
