using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.BusinessLogic;
using System.Data;

namespace MediaWeb.newReporter
{
    public partial class ReporterList : ESP.Web.UI.PageBase
    {
        public int page
        {
            get { return int.Parse(hidPage.Value); }
            set { hidPage.Value = value.ToString(); }
        }

        public int PageSize
        {
            get { return 18; }
        }

        public int RecordCount
        {
            get { return int.Parse(hidRecordCount.Value); }
            set { hidRecordCount.Value = value.ToString(); }
        }
        public int PageCount
        {
            get { return RecordCount % PageSize == 0 ? (RecordCount / PageSize) : (RecordCount / PageSize + 1); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["ListView"] != null)
                {
                    if (Request.Cookies["ListView"].Value == "0")
                        btnView1_Click(sender, e);
                    else
                        btnView2_Click(sender, e);
                }
                ListBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            page = 0;
            ListBind();
        }

        private void ListBind()
        {
            RecordCount = ReportersManager.GetReporterCount(txtKeys.Text.Trim());
            DataTable dt = ReportersManager.GetReporterPage(PageSize, page, txtKeys.Text.Trim(), "");
            dgList.DataSource = dt;
            dgList.DataBind();

            repList.DataSource = dt;
            repList.DataBind();

            btnFirst.Visible = btnPre.Visible = (page != 0);
            btnNext.Visible = btnLast.Visible = (page != PageCount - 1);
        }

        protected void btnView1_Click(object sender, EventArgs e)
        {
            dgList.Visible = true;
            repList.Visible = false;
            btnView2.Attributes["style"] = "background-image:url(/images/01-09.jpg);border:0px; cursor:pointer;";
            btnView1.Attributes["style"] = "background-image:url(/images/01-10.jpg);border:0px; cursor:pointer;";
            HttpCookie cookie = new HttpCookie("ListView");
            cookie.Expires.AddYears(1);
            cookie.Value = "0";
            Response.Cookies.Add(cookie);
        }

        protected void btnView2_Click(object sender, EventArgs e)
        {
            dgList.Visible = false;
            repList.Visible = true;
            btnView2.Attributes["style"] = "background-image:url(/images/01-07.jpg);border:0px; cursor:pointer;";
            btnView1.Attributes["style"] = "background-image:url(/images/01-06.jpg);border:0px; cursor:pointer;";
            HttpCookie cookie = new HttpCookie("ListView");
            cookie.Expires.AddYears(1);
            cookie.Value = "1";
            Response.Cookies.Add(cookie);
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            page = 0;
            ListBind();
        }

        protected void btnPre_Click(object sender, EventArgs e)
        {
            page = page - 1;
            ListBind();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            page = page + 1;
            ListBind();
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            page = PageCount - 1;
            ListBind();
        }
    }
}
