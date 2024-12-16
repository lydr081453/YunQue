using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;

public partial class Dialogs_searchSupplier : System.Web.UI.Page
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
            if (txtsupplierName.Text.Trim() != "")
            {
                Terms += " and supplier_name like '%'+@suppliername+'%'";
                parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(Request["projectId"]))
            {
                IEnumerable<int> supplierIds = ProjectMediaManager.GetList(" projectId=" + Request["projectId"]).Select(x=>x.SupplierId);
                Terms += " and id in (";
                foreach (var id in supplierIds)
                {
                    Terms += id + ",";
                }
                Terms = Terms.TrimEnd(',') +  ")";
               
            }
            gvSupplier.DataSource = SupplierManager.getAllModelList(Terms,parms);
            gvSupplier.DataBind();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int selectId = int.Parse(btn.CommandArgument.ToString());
            var media = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(selectId);
            
            var branchList = ESP.Finance.BusinessLogic.BranchManager.GetList(" branchName='"+media.supplier_name+"'");
            decimal costRate = 0;
            if (branchList != null && branchList.Count > 0)
            {
                costRate = 100;
            }
            string script = string.Empty;
            script += "opener.setMedia('" + media.id + "','" + media.supplier_name + "','" + costRate + "');";
            script += @" window.close();";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
        }
    }
