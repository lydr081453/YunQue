using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Compatible;
using System.IO;

namespace SEPAdmin.Talents
{
    public partial class BackupList : ESP.Web.UI.PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
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


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("TalentNew.aspx");
        }

        protected int DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            int dateDiff = 0;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days / 365;
            return dateDiff;
            #region note
            //C#中使用TimeSpan计算两个时间的差值
            //可以反加两个日期之间任何一个时间单位。
            //TimeSpan ts = Date1 - Date2;
            //double dDays = ts.TotalDays;//带小数的天数，比如1天12小时结果就是1.5 
            //int nDays = ts.Days;//整数天数，1天12小时或者1天20小时结果都是1  
            #endregion
        }

        private void BindData()
        {
            string strWhere = " status=0 ";
            if (txtPosition.Text.Trim() != "")
            {
                strWhere += string.Format(" and (position like '%{0}%')", txtPosition.Text.Trim());
            }
            if (txtuserName.Text.Trim() != "")
            {
                strWhere += string.Format(" and (namecn like '%{0}%' )", txtuserName.Text.Trim());
            }
            if (txtEducation.Text.Trim() != "")
            {
                strWhere += string.Format(" and (education like '%{0}%') ", txtEducation.Text.Trim());
            }
            if (txtYear.Text.Trim() != "")
            {
                int workyear = 0;
                int.TryParse(txtYear.Text, out workyear);
                int ret = DateTime.Now.Year - workyear;
                DateTime begin = new DateTime(ret, 1, 1);
                DateTime end = new DateTime(ret, 12, 31);
                strWhere += string.Format(" and (WorkBegin between '{0}' and '{1}')", begin.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));
            }

            if (!string.IsNullOrEmpty(txtArea.Text))
            {
                strWhere += string.Format(" and (deptshunya like '%{0}%')", txtArea.Text);
            }

            if (!string.IsNullOrEmpty(txtKeyword.Text))
            {
                strWhere += string.Format(" and (HRInterview like '%{0}%' or GroupInterview like '%{0}%' or Resume like '%{0}%' or Customer like '%{0}%' or Professional like '%{0}%')", txtKeyword.Text);
            }

            var talents = (new ESP.HumanResource.BusinessLogic.TalentManager()).GetList(strWhere);

            this.gvList.DataSource = talents;
            this.gvList.DataBind();


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
            if (talents.Count > 0)
            {
                tabTop.Visible = true;
                tabBottom.Visible = true;
            }
            else
            {
                tabTop.Visible = false;
                tabBottom.Visible = false;
            }

            labAllNum.Text = labAllNumT.Text = talents.Count.ToString();
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


        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var model = (ESP.HumanResource.Entity.TalentInfo)e.Row.DataItem;

                HyperLink lnkEdit = (HyperLink)e.Row.FindControl("lnkEdit");

                HyperLink lnkPrint = (HyperLink)e.Row.FindControl("lnkPrint");
                //lnkHC
                HyperLink lnkHC = (HyperLink)e.Row.FindControl("lnkHC");
                // lblWorkYear
                Label lblWorkYear = (Label)e.Row.FindControl("lblWorkYear");
                if (model.WorkBegin != null && model.WorkBegin > new DateTime(1900, 1, 1))
                {
                    lblWorkYear.Text = DateDiff(model.WorkBegin, DateTime.Now).ToString();

                }

                lnkEdit.NavigateUrl = "TalentNew.aspx?talentid=" + model.Id;
                lnkPrint.NavigateUrl = "BackupPrint.aspx?talentid=" + model.Id;
                lnkHC.NavigateUrl = "/HR/Join/HeadAccountEdit.aspx?talentId="+model.Id;
            }
        }


        #region 翻页相关
        protected void gvE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            BindData();
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
            Paging((gvList.PageIndex + 2) >= gvList.PageCount ? gvList.PageCount : (gvList.PageIndex + 1));
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Paging((gvList.PageIndex - 1) < 1 ? 0 : (gvList.PageIndex - 1));
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

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvList.PageIndex = this.ddlCurrentPage2.SelectedIndex;
            BindData();
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

    }
}