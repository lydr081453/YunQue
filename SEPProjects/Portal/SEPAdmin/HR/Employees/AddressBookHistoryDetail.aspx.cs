using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;

namespace SEPAdmin.HR.Employees
{
    public partial class AddressBookHistoryDetail : ESP.Web.UI.PageBase
    {
        private static string departmentName = "";
        private static int num = 1;
        private static int patentId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initDropDownList();
                listBind(patentId);
            }
        }
        private void initDropDownList() 
        {
            IList<ESP.Framework.Entity.DepartmentInfo> deps = ESP.Framework.BusinessLogic.DepartmentManager.GetChildren(0);
            patentId = deps[deps.Count - 1].DepartmentID;
            foreach (var item in deps)
            {
                ListItem it = new ListItem();
                it.Text = item.DepartmentName;
                it.Value = item.DepartmentID.ToString();
                if (item.DepartmentID == patentId)
                {
                    it.Selected = true;
                }
                DropDownListDep.Items.Add(it);
            }
        }
        protected void listBind(int parentId)
        {
            if (this.Request.QueryString["id"] == null || this.Request.QueryString["id"].Trim() == "")
            {
                ShowCompleteMessage("请在列表里面重新选择", "AddressBookHistoryList.aspx");
            }
            int id = Convert.ToInt32(this.Request.QueryString["id"]);
            IList<AddressItemInfo> list = ESP.HumanResource.BusinessLogic.AddressBookManager.GetData(id, parentId);
            gvE.DataSource = list;
            gvE.DataBind();

            if (gvE.PageCount > 1)
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
            labPageCount.Text = labPageCountT.Text = (gvE.PageIndex + 1).ToString() + "/" + gvE.PageCount.ToString();
            if (gvE.PageCount > 0)
            {
                if (gvE.PageIndex + 1 == gvE.PageCount)
                    disButton("last");
                else if (gvE.PageIndex == 0)
                    disButton("first");
                else
                    disButton("");
            }
        }

        protected void gvE_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text == departmentName)
                {
                    e.Row.Cells[0].Text = "";
                    num++;
                    e.Row.Cells[1].Text = num.ToString();
                }
                else
                {
                    departmentName = e.Row.Cells[0].Text;
                    num = 1;
                    e.Row.Cells[1].Text = num.ToString();
                }
            }
        }

        #region 翻页相关
        protected void gvE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvE.PageIndex = e.NewPageIndex;
            listBind(patentId);
        }


        protected void btnLast_Click(object sender, EventArgs e)
        {
            Paging(gvE.PageCount);
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            Paging(0);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Paging((gvE.PageIndex + 2) >= gvE.PageCount ? gvE.PageCount : (gvE.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvE.PageIndex - 1) < 1 ? 0 : (gvE.PageIndex - 1));
        }
        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        private void Paging(int pageIndex)
        {
            GridViewPageEventArgs e = new GridViewPageEventArgs(pageIndex);
            gvE_PageIndexChanging(new object(), e);
        }

        //翻页判断
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

        protected void DropDownListDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            patentId = int.Parse(DropDownListDep.SelectedValue);
            listBind(patentId);
            DropDownListDep.Visible = true;
        }

    }
}
