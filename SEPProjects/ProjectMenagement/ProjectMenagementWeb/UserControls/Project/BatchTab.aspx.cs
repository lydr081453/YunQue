using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace FinanceWeb.UserControls.Project
{
    public partial class BatchTab : System.Web.UI.Page
    {
        private int tabIndex = 0;
        public int TabIndex
        {
            get { return tabIndex; }
            set { tabIndex = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 1; i < 3; i++)
                {
                    System.Web.UI.HtmlControls.HtmlTable table = (System.Web.UI.HtmlControls.HtmlTable)FindControl("Table" + i);
                    if (table != null)
                    {
                        if (i == TabIndex)
                        {
                            table.Attributes["class"] = "button_on";
                        }
                        else
                        {
                            table.Attributes["class"] = "button_over";
                            table.Attributes.Add("onmouseover", "changeClass(this);");
                            table.Attributes.Add("onmouseout", "changeClass2(this);");
                        }
                        LinkButton Tab = (LinkButton)FindControl("Tab" + i);
                        int count = GetDataCount(i, Tab);
                        //if (count > 0)
                        //{
                        if (i != 1)
                            Tab.Text += "<font color='red'>(" + count + ")</font>";
                        //}
                    }
                }
            }
        }

        private int GetDataCount(int index, LinkButton lnk)
        {
            int count = 0;
            switch (index)
            {
                case 1:
                    lnk.PostBackUrl = "/ExpenseAccount/FinanceBatchAuditList.aspx";
                    break;
                case 2:
                    lnk.PostBackUrl = "/ExpenseAccount/FinanceBatchAuditedList.aspx";
                    break;
            }
            return count;
        }
    }
}
