using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.Ticket
{
    public partial class TicketCheckList :ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              //  BindAirData();
            }
        }

        private void BindAirData()
        {
            string strwhere = string.Empty;

            if (this.txtKey.Text.Trim() != string.Empty)
            {
                strwhere += string.Format("  and ( goairno like '%{0}%' or expensedesc like '%{0}%' or boarder like '%{0}%' or returncode like '%{0}%' or projectcode like '%{0}%')", this.txtKey.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBeginDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                strwhere += string.Format(" and (ExpenseDate between '{0}' and '{1}')", txtBeginDate.Text, DateTime.Parse(txtEndDate.Text).AddDays(1));
            }
          DataTable dt = ESP.Finance.BusinessLogic.ExpenseAccountDetailManager.GetIicketCheck(strwhere);
          gvList.DataSource = dt;
          gvList.DataBind();
        }


        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindAirData();
        }
    }
}