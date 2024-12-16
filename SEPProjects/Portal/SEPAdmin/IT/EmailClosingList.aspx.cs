using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.IT
{
    public partial class EmailClosingList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dataBind();
            }
        }


        private void dataBind()
        { 
          string ITUsers =System.Configuration.ConfigurationManager.AppSettings["EmailClosingOperator"];

          if (ITUsers.IndexOf("," + UserID.ToString() + ",") >= 0)
          {
              string strwhere = " status =" + ddlStatus.SelectedValue;

              if (!string.IsNullOrEmpty(txtCode.Text))
              {
                  strwhere += " and (namecn like '%" + txtCode.Text + "%' or email like '%" + txtCode.Text + "%')";
              }

              var emailList = ESP.HumanResource.BusinessLogic.EmailClosingManager.GetList(strwhere);

              this.gvList.DataSource = emailList;
              this.gvList.DataBind();
          }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblClose = (Label)e.Row.FindControl("lblClose");

                ESP.HumanResource.Entity.EmailClosingInfo model = (ESP.HumanResource.Entity.EmailClosingInfo)e.Row.DataItem;
                if (model.Status == 0)
                {
                    lblClose.Text = "<a href='EmailClosing.aspx?userId=" + model.UserId.ToString() + "'>确认</a>";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dataBind();
        }
    }
}