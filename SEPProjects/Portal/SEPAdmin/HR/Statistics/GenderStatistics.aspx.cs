using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.Statistics
{
    public partial class GenderStatistics : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region AjaxProRegister
            AjaxPro.Utility.RegisterTypeForAjax(typeof(GenderStatistics));
            #endregion
            if (!IsPostBack)
            {
                DepartmentDataBind();
                ListBind();
            }
        }

        private void ListBind()
        {
            string typevalue = "";

            if (hidtype2.Value != "" && hidtype2.Value != "-1")
            {
                typevalue = hidtype2.Value;
            }
            else if (hidtype1.Value != "" && hidtype1.Value != "-1")
            {
                typevalue = hidtype1.Value;
            }
            else if (hidtype.Value != "" && hidtype.Value != "-1")
            {
                typevalue = hidtype.Value;
            }
            else
            {
            }

            int[] depids = null;
            if (!string.IsNullOrEmpty(typevalue) && ESP.HumanResource.Utilities.StringHelper.IsConvertInt(typevalue))
            {
                IList<ESP.Framework.Entity.DepartmentInfo> dlist;
                int selectedDep = int.Parse(typevalue);
                dlist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(selectedDep);
                if (dlist != null && dlist.Count > 0)
                {
                    depids = new int[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        depids[i] = dlist[i].DepartmentID;
                    }
                }
                else
                {
                    depids = new int[] { selectedDep };
                }
            }
            else
            {
                depids = null;
            }

            List<ESP.HumanResource.Entity.StatisticsInfo> list = null;
            if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
            {

                list = ESP.HumanResource.BusinessLogic.StatisticsManager.getListForGender(UserInfo,depids, "",true);
            }
            else
            {
                list = ESP.HumanResource.BusinessLogic.StatisticsManager.getListForGender(UserInfo, depids, "", false);
            }

         //   List<ESP.HumanResource.Entity.StatisticsInfo> list = StatisticsManager.getListForGender();
                        
           for(int i = 0 ; i < list.Count ; i++)
           {
               double countCo = StatisticsManager.getCountByCo(list[i].GroupID);
               double countGroup = StatisticsManager.getCountByGroup(list[i].GroupID);
               if (countCo != 0 && countGroup != 0)
               {
                   list[i].MensRateByCo = (list[i].MensNumByDep / countCo * 100);
                   list[i].MensRateByDep = (list[i].MensNumByDep / countGroup * 100);
                   list[i].LadysRateByCo = (list[i].LadysNumByDep / countCo * 100);
                   list[i].LadysRateByDep = (list[i].LadysNumByDep / countGroup * 100);
                   list[i].UnknownsRateByCo = (list[i].UnknownsNumByDep / countCo * 100);
                   list[i].UnknownsRateByDep = (list[i].UnknownsNumByDep / countGroup * 100);
               }

           }
            gvList.DataSource = list;
            gvList.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        [AjaxPro.AjaxMethod]
        public static List<List<string>> getalist(int parentId)
        {
            List<List<string>> list = new List<List<string>>();
            try
            {

                list = ESP.Compatible.DepartmentManager.GetListForAJAX(parentId);
            }
            catch (Exception e)
            {
                e.ToString();
            }


            List<string> c = new List<string>();
            c.Add("-1");
            c.Add("请选择...");
            list.Insert(0, c);

            return list;
        }

        private void DepartmentDataBind()
        {
            string hradmin = "";
            List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> empinposlist = new List<ESP.HumanResource.Entity.EmployeesInPositionsInfo>();
            if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))
            {
                object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
                ddltype.DataSource = dt;
                ddltype.DataTextField = "NodeName";
                ddltype.DataValueField = "UniqID";
                ddltype.DataBind();
                ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
                ddltype1.Items.Insert(0, new ListItem("请选择...", "-1"));
                ddltype2.Items.Insert(0, new ListItem("请选择...", "-1"));
            }
            if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("004", this.ModuleInfo.ModuleID, this.UserID))
            {
                empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", UserInfo.UserID));
                ddltype.Items.Insert(0, new ListItem(empinposlist[0].CompanyName, empinposlist[0].CompanyID.ToString()));
                hidtype.Value = empinposlist[0].CompanyID.ToString();

            }
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
