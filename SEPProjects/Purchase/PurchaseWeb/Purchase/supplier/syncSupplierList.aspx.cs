using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;
using System.Data;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using ESP.Supplier.BusinessLogic;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace PurchaseWeb.Purchase.supplier
{
    public partial class syncSupplierList : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(TypeDataProvider));
            if (!IsPostBack)
            {
                ListBind();
                TypeBind();
            }
        }

        protected void btnSerach_Click(object sender, EventArgs e)
        {
            ListBind();
        }

        private void TypeBind()
        {
            List<TypeInfo> list = TypeManager.GetListByParentId(0);
            ddltype.DataSource = list;
            ddltype.DataTextField = "typename";
            ddltype.DataValueField = "typeid";
            ddltype.DataBind();
            ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
            ddltype1.Items.Insert(0, new ListItem("请选择...", "-1"));
            ddltype2.Items.Insert(0, new ListItem("请选择...", "-1"));
        }

        private void ListBind()
        {
            string where = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            if (txtsupplierName.Text.Trim() != "")
            {
                where += " and (a.supplier_name like '%'+@name+'%')";
                parms.Add(new SqlParameter("@name", txtsupplierName.Text.Trim()));
            }
            if (ddlSync.SelectedValue != "0")
            {
                where += " and b.id" + (ddlSync.SelectedValue == "1" ? " is not null" : " is null");
            }
            if (!string.IsNullOrEmpty(hidtype2.Value) && hidtype2.Value.Trim() != "-1")
            {
                where += string.Format(" and a.id in(select a.supplierId from SC_SupplierProtocolType a join T_ESPAndSupplyTypeRelation c on a.typeId =c.SupplyTypeId join T_Type d on c.ESPTypeId=d.typeid where d.typeid ={0})",hidtype2.Value);

            }
            else
            {
                if (!string.IsNullOrEmpty(hidtype1.Value) && hidtype1.Value.Trim() != "-1")
                {
                    where += string.Format(" and a.id in(select a.supplierId from SC_SupplierProtocolType a join T_ESPAndSupplyTypeRelation c on a.typeId =c.SupplyTypeId join T_Type d on c.ESPTypeId=d.typeid where d.parentid ={0})", hidtype1.Value);
                }
                else
                {
                    if (!string.IsNullOrEmpty(hidtype.Value) && hidtype.Value.Trim() != "-1")
                    {
                        where += string.Format(" and a.id in(select a.supplierId from SC_SupplierProtocolType a join T_ESPAndSupplyTypeRelation c on a.typeId =c.SupplyTypeId join T_Type d on c.ESPTypeId=d.typeid where d.parentId in(select typeId from T_Type where parentId={0}))", hidtype.Value);
                    }
                }
            }

            gvSupplier.DataSource = SupplierManager.GetSyncSupplierList(where, parms);
            gvSupplier.DataBind();
        }

        protected void gvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSupplier.PageIndex = e.NewPageIndex;
            ListBind();
        }

        protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dv = (DataRowView)e.Row.DataItem;
                Literal litView = (Literal)e.Row.FindControl("litView");
                Literal litUser = (Literal)e.Row.FindControl("litUser");
                Literal litCity = (Literal)e.Row.FindControl("litCity");
                Label labReviewers = (Label)e.Row.FindControl("labReviewers");
                Literal litTel = (Literal)e.Row.FindControl("litTel");

                Label lblState = (Label)e.Row.FindControl("lblState");

                IList<ESP.Supplier.Entity.SC_AgencySupplierReg> asr = new ESP.Supplier.DataAccess.SC_AgencySupplierRegProvider().GetList(" and supplierid=" + dv["id"].ToString(), new SqlParameter[0] { });

                if (lblState != null && lblState.Text != string.Empty)
                {
                    if (Convert.ToInt32(lblState.Text) > State.SupplierStatus_show.Length)
                    { lblState.Text = "已删除"; }
                    else
                        lblState.Text = State.SupplierStatus_show[Convert.ToInt32(lblState.Text)];
                }
                

                litTel.Text = dv["contact_Tel"].ToString();
                litCity.Text = dv["supplier_province"].ToString() + "  " + dv["supplier_city"].ToString();

                int i = 0;
                string str = "";
                string ltype = "";
                Label labTypes = (Label)e.Row.FindControl("labTypes");
                List<SC_SupplierType> typeList = SC_SupplierTypeDataProvider.GetList(" SupplierId=" + dv["id"].ToString() + " and TypeLV=3", new SqlParameter[0] { });
                foreach (SC_SupplierType t in typeList)
                {
                    XML_VersionList typeModel = new XML_VersionListManager().GetModel(t.TypeId);
                    ltype += typeModel == null ? "" : typeModel.Name + "，"; ;
                    if (i < 2)
                    {
                        str += typeModel == null ? "" : typeModel.Name + "，";
                        i++;
                    }

                }
                labTypes.Text = str.TrimEnd('，');
                labTypes.ToolTip = ltype.TrimEnd('，');
            }
        }
    }
}
