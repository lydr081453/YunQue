using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Compatible;

namespace SEPAdmin.HR.Employees
{
    public partial class _ITRefundList : ESP.Web.UI.PageBase
    {
        ESP.HumanResource.Entity.EmployeesInPositionsInfo pos = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region AjaxProRegister
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Employees_EmployeesAllList));
            #endregion
            pos = ESP.HumanResource.BusinessLogic.EmployeesInPositionsManager.GetModel(CurrentUserID);
            if (!IsPostBack)
            {
                DepartmentDataBind();

                if (!string.IsNullOrEmpty(Request["rid"]) && !string.IsNullOrEmpty(Request["flag"]) && Request["flag"] == "1")
                {
                    int id = int.Parse(Request["rid"]);
                    DeleteRefundInfo(id);
                }
                //if (System.Configuration.ConfigurationManager.AppSettings["ITDept"].IndexOf(pos.DepartmentID.ToString()) < 0)
                //{
                //    lbAddUsers.Visible = false;
                //}
                ListBind();
            }
        }

        private void ListBind()
        {
            string strWhere = " and r.isDeleted=0 ";
            if (ddlStatus.SelectedValue != "")
            {                
                strWhere += " and (r.status = " + ddlStatus.SelectedValue + ")";
            }
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere += string.Format(" and (e.code like '%{0}%' or u.lastnamecn+u.firstnamecn like '%{0}%'  or u.username like '%{0}%' or r.productname like '%{0}%' or r.productNo like '%{0}%' or r.creator like '%{0}%')", txtKeyword.Text.Trim());
            }

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
            if (!string.IsNullOrEmpty(ddlProductType.SelectedValue))
            {
                strWhere += string.Format(" and r.type={0} ", ddlProductType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
            {
                strWhere += string.Format(" and r.status={0} ", ddlStatus.SelectedValue);
            }

            DataSet ds = new ESP.Administrative.BusinessLogic.RefundManager().GetListByUser(strWhere, typevalue);

            gvList.DataSource = ds;
            gvList.DataBind();

            this.ddlCurrentPage2.Items.Clear();
            for (int i = 1; i <= this.gvList.PageCount; i++)
            {
                this.ddlCurrentPage2.Items.Add(i.ToString());
            }
            if (this.gvList.PageCount > 0)
            {
                this.ddlCurrentPage2.SelectedIndex = this.gvList.PageIndex;
            }
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
            if (ds.Tables[0].Rows.Count > 0)
            {
                tabTop.Visible = true;
                tabBottom.Visible = true;
            }
            else
            {
                tabTop.Visible = false;
                tabBottom.Visible = false;
            }

            labAllNum.Text = labAllNumT.Text = ds.Tables[0].Rows.Count.ToString();
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

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvList.PageIndex = this.ddlCurrentPage2.SelectedIndex;
            ListBind();
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
            object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
            //object dt = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(0);
            ddltype.DataSource = dt;
            ddltype.DataTextField = "NodeName";
            ddltype.DataValueField = "UniqID";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
            ddltype1.Items.Insert(0, new ListItem("请选择...", "-1"));
            ddltype2.Items.Insert(0, new ListItem("请选择...", "-1"));

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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(gvList.DataKeys[e.Row.RowIndex].Values[1].ToString()))
                {
                    if (((int)ESP.Administrative.Common.RefundStatus.EndStatus).ToString() == gvList.DataKeys[e.Row.RowIndex].Values[1].ToString())
                    {
                        e.Row.Cells[10].Text = "租赁已结束";
                    }
                    else if (((int)ESP.Administrative.Common.RefundStatus.UnEnableStatus).ToString() == gvList.DataKeys[e.Row.RowIndex].Values[1].ToString())
                    {
                        e.Row.Cells[10].Text = "<a href='RefundAdd.aspx?rid=" + gvList.DataKeys[e.Row.RowIndex].Values[0].ToString() + "'>编辑</a>&nbsp;&nbsp;<a id='hf' href='RefundEnd.aspx?rid=" + gvList.DataKeys[e.Row.RowIndex].Values[0].ToString() + "&flag=1'>启动租赁</a>&nbsp;&nbsp;<a id='hf' onclick='if (confirm(\"您确定要删除此条笔记本租赁记录？\")) location.href=\"ITRefundList.aspx?rid=" + gvList.DataKeys[e.Row.RowIndex].Values[0].ToString() + "&flag=1\"' style=\"cursor:pointer;\" >删除</a>";
                    }
                    else
                    {
                        e.Row.Cells[10].Text = "<a href='RefundAdd.aspx?rid=" + gvList.DataKeys[e.Row.RowIndex].Values[0].ToString()+"'>编辑</a>&nbsp;&nbsp;<a id='hf' href='RefundEnd.aspx?rid=" + gvList.DataKeys[e.Row.RowIndex].Values[0].ToString() + "&flag=2'>结束租赁</a>";
                    }
                }
            }
        }

        /// <summary>
        /// 删除笔记本租赁信息
        /// </summary>
        private void DeleteRefundInfo(int id)
        {
            new ESP.Administrative.BusinessLogic.RefundManager().Delete(id);
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('笔记本租赁记录删除成功！');window.location='ITRefundList.aspx';", true);
        }
    }
}
