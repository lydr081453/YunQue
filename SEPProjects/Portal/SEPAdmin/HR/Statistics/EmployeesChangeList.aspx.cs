using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Statistics
{
    public partial class EmployeesChangeList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region AjaxProRegister
            AjaxPro.Utility.RegisterTypeForAjax(typeof(EmployeesChangeList));
            #endregion
            if (!IsPostBack)
            {
                DepartmentDataBind();
                ListBind();
            }

        }

        private void ListBind()
        {
            //string strWhere = " (a.status = 1 or a.status = 3) ";
            string strWhere = " and a.status != 999";
            if (txtITCode.Text.Trim() != "")
            {
                strWhere += string.Format(" and (a.code like '%{0}%')", txtITCode.Text.Trim());
            }
            if (txtuserName.Text.Trim() != "")
            {
                strWhere += string.Format(" and (b.lastnamecn+b.firstnamecn like '%{0}%'  or b.username like '%{0}%' )", txtuserName.Text.Trim());
            }
           // List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(strWhere);

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

            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = null;
            if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("001", this.ModuleInfo.ModuleID, this.UserID))            {
               
                 list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelListByDeparmtnetID(depids,strWhere);
            }
            else
            {
                 list = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetUserModelList(depids, strWhere);
            }
            gvList.DataSource = list;
            gvList.DataBind();

            if (gvList.PageCount > 1)
            {
                PageBottom.Visible = true;
                PageTop.Visible = true;
            }
            else
            {
                PageBottom.Visible = false;
                PageTop.Visible = false;
            }
            if (list.Count > 0)
            {
                tabTop.Visible = true;
                tabBottom.Visible = true;
            }
            else
            {
                tabTop.Visible = false;
                tabBottom.Visible = false;
            }

            labAllNum.Text = labAllNumT.Text = list.Count.ToString();
            labPageCount.Text = labPageCountT.Text = (gvList.PageIndex + 1).ToString() + "/" + gvList.PageCount.ToString();
            if (gvList.PageCount > 0)
            {
                if (gvList.PageIndex + 1 == gvList.PageCount)
                    disButton("last");
                else if (gvList.PageIndex == 0)
                    disButton("first");
                else
                    disButton("");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(gvList.DataKeys[e.Row.RowIndex].Values[0].ToString()))
                {
                    e.Row.Cells[1].Text = string.Format("<a href='EmployeesChangeDetail.aspx?userid={0}&back=1'>{1}</a>", gvList.DataKeys[e.Row.RowIndex].Values[0].ToString(),e.Row.Cells[1].Text);                    
                }
                if (!string.IsNullOrEmpty(gvList.DataKeys[e.Row.RowIndex].Values[1].ToString()))
                {
                    if (gvList.DataKeys[e.Row.RowIndex].Values[1].ToString() == "1")
                    {
                        e.Row.Cells[4].Text = "试用期员工";                       
                    }
                    else
                    {
                        e.Row.Cells[4].Text = "正式员工";                        
                    }
                }
            }
        }

        #region 分页设置
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvList.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvList.PageIndex + 1) > gvList.PageCount ? gvList.PageCount : (gvList.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvList.PageIndex - 1) < 0 ? 0 : (gvList.PageIndex - 1));
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvList_PageIndexChanging(new object(), e);
        }

        /// <summary>
        /// 分页按钮的显示设置
        /// </summary>
        /// <param name="page"></param>
        private void disButton(string page)
        {
            switch (page)
            {
                case "first":
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = false;
                    btnPrevious2.Enabled = false;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
                case "last":
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = false;
                    btnLast2.Enabled = false;
                    break;
                default:
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnFirst2.Enabled = true;
                    btnPrevious2.Enabled = true;
                    btnNext2.Enabled = true;
                    btnLast2.Enabled = true;
                    break;
            }
        }

        #endregion

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
            //if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("002", this.ModuleInfo.ModuleID, this.UserID))
            //{
            //    hradmin = "培恩公关";
            //    empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", UserInfo.UserID));
            //    if (empinposlist != null && empinposlist.Count > 0)
            //    {
            //        IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[0].CompanyID);
            //        int depid = IsStringInArray(hradmin, deplist);
            //        if (depid > 0)
            //        {
            //            ddltype1.Items.Insert(0, new ListItem(hradmin, depid.ToString()));
            //            hidtype1.Value = depid.ToString();
            //        }
            //    }
            //}
            //else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("003", this.ModuleInfo.ModuleID, this.UserID))
            //{
            //    hradmin = "TCG";
            //    empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", UserInfo.UserID));
            //    if (empinposlist != null && empinposlist.Count > 0)
            //    {
            //        IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[0].CompanyID);
            //        int depid = IsStringInArray(hradmin, deplist);
            //        if (depid > 0)
            //        {

            //            ddltype1.Items.Insert(0, new ListItem(hradmin, depid.ToString()));
            //            hidtype1.Value = depid.ToString();
            //        }
            //    }
            //}
            //else if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("005", this.ModuleInfo.ModuleID, this.UserID))
            //{
            //    hradmin = "国际公关";
            //    empinposlist = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModelList(string.Format(" a.userid={0} ", UserInfo.UserID));
            //    if (empinposlist != null && empinposlist.Count > 0)
            //    {
            //        IList<ESP.Framework.Entity.DepartmentInfo> deplist = ESP.Framework.BusinessLogic.DepartmentManager.GetChildrenRecursion(empinposlist[0].CompanyID);
            //        int depid = IsStringInArray(hradmin, deplist);
            //        if (depid > 0)
            //        {

            //            ddltype1.Items.Insert(0, new ListItem(hradmin, depid.ToString()));
            //            hidtype1.Value = depid.ToString();
            //        }
            //    }
            //}
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
