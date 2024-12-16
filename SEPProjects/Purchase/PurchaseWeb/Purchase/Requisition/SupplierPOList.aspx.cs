using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class SupplierPOList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listBind();
            }
        }

        private void listBind()
        {


            string Term = " and a.supplier_Name=@supplierName and a.status not in(0,-1,2,4)";
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            parms.Add(new System.Data.SqlClient.SqlParameter("@supplierName", Request["supplierName"]));
            List<ESP.Purchase.Entity.GeneralInfo> list = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetStatusList(Term, parms);
            gvList.DataSource = list;
            gvList.DataBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            listBind();
        }
    }
}
