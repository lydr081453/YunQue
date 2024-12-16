using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.HR.Statistics
{
    public partial class AgeStatistics : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region AjaxProRegister
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AgeStatistics));
            #endregion
            if (!IsPostBack)
            {
                DepartmentDataBind();
                ListBind();
            }
        }

        private void ListBind()
        {
            //List<ESP.HumanResource.Entity.StatisticsInfo> list = StatisticsManager.getListForAge();
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

                list = ESP.HumanResource.BusinessLogic.StatisticsManager.getListForAge(UserInfo, depids, "", true);
            }
            else
            {
                list = ESP.HumanResource.BusinessLogic.StatisticsManager.getListForAge(UserInfo, depids, "", false);
            }

            for (int i = 0; i < list.Count; i++)
            {
                double countCo = StatisticsManager.getCountByCo(list[i].GroupID);
                double countGroup = StatisticsManager.getCountByGroup(list[i].GroupID);
                if (countCo != 0 && countGroup != 0)
                {
                    list[i].L60RateByCo = (list[i].L60NumByDep / countCo * 100);
                    list[i].L60RateByDep = (list[i].L60NumByDep / countGroup * 100);
                    list[i].Y60RateByCo = (list[i].Y60NumByDep / countCo * 100);
                    list[i].Y60RateByDep = (list[i].Y60NumByDep / countGroup * 100);
                    list[i].Y70RateByCo = (list[i].Y70NumByDep / countCo * 100);
                    list[i].Y70RateByDep = (list[i].Y70NumByDep / countGroup * 100);
                    list[i].Y80RateByCo = (list[i].Y80NumByDep / countCo * 100);
                    list[i].Y80RateByDep = (list[i].Y80NumByDep / countGroup * 100);
                    list[i].Y90RateByCo = (list[i].Y90NumByDep / countCo * 100);
                    list[i].Y90RateByDep = (list[i].Y90NumByDep / countGroup * 100);
                    list[i].Y2kRateByCo = (list[i].Y2kNumByDep / countCo * 100);
                    list[i].Y2kRateByDep = (list[i].Y2kNumByDep / countGroup * 100);
                    list[i].Unknow3RateByCo = (list[i].Unknow3NumByDep / countCo * 100);
                    list[i].Unknow3RateByDep = (list[i].Unknow3NumByDep / countGroup * 100);
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
            //string hradmin = "";
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
