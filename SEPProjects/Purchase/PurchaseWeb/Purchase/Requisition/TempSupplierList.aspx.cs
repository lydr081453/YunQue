using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using ESP.Purchase.BusinessLogic;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class TempSupplierList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListBind();
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        private void ListBind()
        {
            string Terms = "";
            string typeids = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            if (txtsupplierName.Text.Trim() != "")
            {
                Terms += " and supplier_name like '%'+@suppliername+'%'";
                parms.Add(new SqlParameter("@suppliername", txtsupplierName.Text.Trim()));
            }
            if (txtArea.Text.Trim() != "")
            {
                Terms += " and supplier_area like '%'+@supplier_area+'%'";
                parms.Add(new SqlParameter("@supplier_area", txtArea.Text.Trim()));
            }
            if (txtlinkName.Text.Trim() != "")
            {
                Terms += " and contact_name like '%'+@linkName+'%'";
                parms.Add(new SqlParameter("@linkName", txtlinkName.Text.Trim()));
            }

            if (txtType.Text.Trim() != "")
            {
                typeids = txtType.Text.Trim();
            }
            Terms += " and supplier_type = " + (int)State.supplier_type.noAgreement;

            List<SupplierInfo> list = new List<SupplierInfo>();
            if (txtType.Text.Trim() == "")
            {
                list = SupplierManager.getModelList(Terms, parms);
            }
            else
            {
                list = SupplierManager.getSupplierListByTypeNames(typeids, Terms, parms);
            }

            gvSupplier.DataSource = list;
            gvSupplier.DataBind();
        }

        protected void gvSupplier_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = int.Parse(gvSupplier.DataKeys[e.RowIndex].Value.ToString());
            SupplierManager.Delete(id);
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对T_Supplier表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, id, "删除临时供应商"), "临时供应商");
            ListBind();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label labPhone = (Label)e.Row.FindControl("labPhone");
                if (labPhone != null && labPhone.Text != string.Empty)
                {
                    if (labPhone.Text[labPhone.Text.Length - 1] == '-')
                    {
                        labPhone.Text = labPhone.Text.Substring(0, labPhone.Text.Length - 1);
                    }
                }
                Label labFax = (Label)e.Row.FindControl("labFax");
                if (labFax != null && labFax.Text != string.Empty)
                {
                    if (labFax.Text[labFax.Text.Length - 1] == '-')
                    {
                        labFax.Text = labFax.Text.Substring(0, labFax.Text.Length - 1);
                    }
                }
            }
        }

        protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Changed")
            {
                int supplierId = int.Parse(e.CommandArgument.ToString());
                SupplierManager.ChangedTypeToRecommend(supplierId);

                ListBind();
            }
        }
    }
}
