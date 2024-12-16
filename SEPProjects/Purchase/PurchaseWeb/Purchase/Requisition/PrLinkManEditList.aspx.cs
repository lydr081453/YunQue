using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class PrLinkManEditList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string script = @" parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
        }

        private void bindLinkman()
        {
            GeneralInfo Model = GeneralInfoManager.GetModel(int.Parse(Request["gid"]));

            Model.supplier_linkman = txtLinkman.Text;
            Model.Supplier_cellphone = txtMobile.Text;
            Model.supplier_email = txtMail.Text;

            ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(Model);

            string script = @" parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();";
            script += @" window.parent.onPageRefresh();";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            bindLinkman();
        }
    }
}
