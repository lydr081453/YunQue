using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;

namespace PurchaseWeb.Purchase.supplier
{
    public partial class searchSupplier : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                txtsupplierName.Text = Request["key"];
                ListBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        protected void gvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSupplier.PageIndex = e.NewPageIndex;
            ListBind();
        }

        private void ListBind()
        {
            string Terms = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            Terms += " and id not in (select ESPSupplierId from T_ESPAndSupplySuppliersRelation)";
            if (txtsupplierName.Text.Trim() != "")
            {
                Terms += " and supplier_name like '%'+@suppliername+'%'";
                parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
            }
            gvSupplier.DataSource = SupplierManager.getAllModelList(Terms,parms);
            gvSupplier.DataBind();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int selectId = int.Parse(btn.CommandArgument.ToString());
            string script = string.Empty;
            script += "parent.setValues('"+selectId+"');";
            script += @" parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
        }
    }
}
