using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;
using System.Data;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Entity;
using System.Data.SqlClient;
using ESP.Purchase.BusinessLogic;

namespace PurchaseWeb.Purchase.supplier
{
    public partial class syncSupplierInfo : ESP.Web.UI.PageBase
    {
        int syncId = 0;
        int eid = 0;
        int sid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["syncId"]))
            {
                syncId = int.Parse(Request["syncId"]);
            }
            if (!string.IsNullOrEmpty(Request["eid"]))
            {
                eid = int.Parse(Request["eid"]);
                hidSupplierId.Value = Request["eid"];
            }
            else
            {
                ESP.Purchase.Entity.ESPAndSupplySuppliersRelation relation = ESPAndSupplySuppliersRelationManager.GetModel(syncId);
                if (relation != null)
                {
                    eid = relation.ESPSupplierId;
                    hidSupplierId.Value = eid.ToString();
                    sid = relation.SupplySupplierId;
                }
            }
            if (!string.IsNullOrEmpty(Request["sid"]))
            {
                sid = int.Parse(Request["sid"]);
            }
            if (!IsPostBack)
            {
                BindEsupplier();
                BindSsupplier();
                BindTypes();
                gvUserDatabind();
                if (syncId != 0)
                {
                    btnSearch.Visible = false;
                }
                TypeListBind();
            }
            btnSave1.Visible = btnSave2.Visible = hidSupplierId.Value != "";
        }

        private void BindEsupplier()
        {

            for (int i = 0; i < ESP.Purchase.Common.State.supplierstatus.Length; i++)
            {
                ddlStatus.Items.Add(new ListItem(ESP.Purchase.Common.State.supplierstatus[i], i.ToString()));
            }
            if (hidSupplierId.Value != "")
            {
                ESP.Purchase.Entity.SupplierInfo model = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(int.Parse(hidSupplierId.Value));
                txtSupplierName.Text = model.supplier_name;
                txtsupplier_area1.Text = model.supplier_area;
                txtsupplier_industry.Text = model.supplier_industry;
                txtsupplier_scale.Text = model.supplier_scale;
                txtsupplier_principal.Text = model.supplier_principal;
                txtsupplier_builttime.Text = model.supplier_builttime;
                txtsupplier_website.Text = model.supplier_website;
                txtfa_no.Text = model.supplier_frameNO;

                ddlStatus.SelectedValue = model.supplier_status.ToString();

                txtaccount_name.Text = model.account_name;
                txtaccount_bank.Text = model.account_bank;
                txtaccount_number.Text = model.account_number;
            }
        }

        private void BindSsupplier()
        {
            if (sid != 0)
            {
                ESP.Supplier.Entity.SC_Supplier model = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetModel(sid);
                labSName.Text = model.supplier_name;
                labCity.Text = model.supplier_province + "-" + model.supplier_city;
                labInvoiceTitle.Text = model.InvoiceTitle; 
            }
        }

        protected void linkBind_Click(object sender, EventArgs e)
        {
            BindEsupplier();
        }

        /// <summary>
        /// 保存采购供应商信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave1_Click(object sender, EventArgs e)
        {
            if (ESP.Purchase.BusinessLogic.SupplierManager.Update(GetEmodel()) > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
            }
        }

        private ESP.Purchase.Entity.SupplierInfo GetEmodel()
        {
            ESP.Purchase.Entity.SupplierInfo model = new ESP.Purchase.Entity.SupplierInfo();
            if (hidSupplierId.Value != "")
            {
                model = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(int.Parse(hidSupplierId.Value));
            }
            model.supplier_name = txtSupplierName.Text.Trim();
            model.supplier_area = txtsupplier_area1.Text.Trim();
            model.supplier_industry = txtsupplier_industry.Text.Trim();
            model.supplier_scale = txtsupplier_scale.Text.Trim();
            model.supplier_principal = txtsupplier_principal.Text.Trim();
            model.supplier_builttime = txtsupplier_builttime.Text.Trim();
            model.supplier_website = txtsupplier_website.Text.Trim();
            model.supplier_frameNO = txtfa_no.Text.Trim();
            model.supplier_status = int.Parse(ddlStatus.SelectedValue);
            model.account_name = txtaccount_name.Text.Trim();
            model.account_bank = txtaccount_bank.Text.Trim();
            model.account_number = txtaccount_number.Text.Trim();

            return model;
        }

        /// <summary>
        /// 编辑更多采购供应商信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave2_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Purchase/Requisition/SupplierInfoEdit.aspx?supplierId=" + hidSupplierId.Value);
        }

        /// <summary>
        /// 同步并建立关联
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSync_Click(object sender, EventArgs e)
        {
            ESP.Purchase.Entity.SupplierInfo Emodel = GetEmodel();
            ESP.Supplier.Entity.SC_Supplier Smodel = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetModel(sid);
            Emodel.supplier_name = Smodel.supplier_name;
            Emodel.supplier_area = Smodel.supplier_province == "广东" ? "广州" : Smodel.supplier_province;
            if (labInvoiceTitle.Text.Trim() != "")
            {
                Emodel.supplier_name = labInvoiceTitle.Text.Trim();
                Emodel.account_name = labInvoiceTitle.Text.Trim();
            }

            DataTable dt = ESP.Purchase.BusinessLogic.SupplierManager.SyncSupplier(Emodel, Smodel, syncId, int.Parse(CurrentUser.SysID));
            if (dt == null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('同步失败！');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('同步成功！');window.location.href='syncSupplierInfo.aspx?syncId=" + dt.Rows[0]["id"].ToString() + "&eid=" + dt.Rows[0]["ESPSupplierId"].ToString() + "&sid=" + dt.Rows[0]["SupplySupplierId"].ToString() + "';", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("syncSupplierList.aspx");
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            TabContainer1.ActiveTabIndex = 2;
            this.rp1.DataSource = new XML_VersionClassManager().GetChooseList(sid);
            this.rp1.DataBind();
        }

        protected void lnk1_Click(object sender, EventArgs e)
        {
            TabContainer1.ActiveTabIndex = 1;
            gvUserDatabind();
        }

        private void BindTypes()
        {
            this.rp1.DataSource = new XML_VersionClassManager().GetChooseList(sid);
            this.rp1.DataBind();
        }

        protected void rp1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                XML_VersionClass type1 = e.Item.DataItem as XML_VersionClass;
                if (null != type1)
                {
                    Label lblMain = (Label)e.Item.FindControl("lblMain");
                    lblMain.Text = type1.Name;
                    DataList ListLevel2 = (DataList)e.Item.FindControl("ListLevel2");

                    IList<XML_VersionClass> list = new XML_VersionClassManager().GetChooseList(sid, type1.ID);
                    ListLevel2.DataSource = list;
                    ListLevel2.DataBind();
                }
            }

        }

        protected void List2_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                XML_VersionClass type2 = e.Item.DataItem as XML_VersionClass;

                Label lblName = (Label)e.Item.FindControl("lblName");
                DataList ListLevel3 = (DataList)e.Item.FindControl("ListLevel3");
                ListLevel3.DataSource = new XML_VersionListManager().GetChooseList(sid, type2.ID);
                ListLevel3.DataBind();

            }
        }


        private void TypeListBind()
        {
            if (!IsPostBack)
            {
                ddltypes.Items.Insert(0, new ListItem("全部", "-2"));
                ddltypes.Items.Insert(1, new ListItem("未设置", "-1"));
                ddltypes.Items.Insert(2, new ListItem("一般", "0"));
                ddltypes.Items.Insert(3, new ListItem("协议", "1"));
                ddltypes.Items.Insert(4, new ListItem("推荐", "2"));
            }
            string terms = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            terms += " and a.id=" + sid;
            if (ddltypes.SelectedValue != "-2")
            {
                if (ddltypes.SelectedValue == "-1")
                    terms += " and supplierType is null";
                else
                    terms += " and supplierType=" + ddltypes.SelectedValue;
            }
            if (txtName.Text.Trim() != "")
            {
                terms += " and typename like '%'+@name+'%'";
                parms.Add(new SqlParameter("@name", txtName.Text.Trim()));
            }
            DataTable dt = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetSupplierListJoinSupplierType(terms, parms);
            gvTypeList.DataSource = dt;
            gvTypeList.DataBind();
        }

        protected void gvTypeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTypeList.PageIndex = e.NewPageIndex;
            TypeListBind();
        }

        protected void gvTypeList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dv = (DataRowView)e.Row.DataItem;
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk = (System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Row.FindControl("chk");
                Button btnML = (Button)e.Row.FindControl("btnML");
                Literal litSupplierType = (Literal)e.Row.FindControl("litSupplierType");
                string[] supplierNames = { "一般", "协议", "推荐" };
                if (dv["supplierType"] == null || dv["supplierType"] == DBNull.Value)
                    litSupplierType.Text = "一般";
                else{
                    litSupplierType.Text = supplierNames[int.Parse(dv["supplierType"].ToString())];
                    if (dv["supplierType"].ToString() == "1" && eid != 0)
                    {
                        btnML.Attributes["onclick"] = "editML('" + Request["syncId"] + "','" + btnML.CommandArgument.ToString() + "');return false;";
                        btnML.Visible = true;
                    }
                }

                
                if (hidValues.Value.Contains(";" + dv["id"].ToString() + "-" + dv["typeId"].ToString() + ";"))
                {
                    chk.Checked = true;
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            hidValues.Value = "";
            TypeListBind();
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            if (hidValues.Value.Replace(";", "") != "")
            {
                string[] values = hidValues.Value.TrimEnd(';').TrimStart(';').Split(';');
                if (ESP.Supplier.BusinessLogic.SC_SupplierprotocolTypeManager.insertInfos(values, int.Parse(rdl.SelectedValue), int.Parse(CurrentUser.SysID)))
                {
                    hidValues.Value = "";
                    TypeListBind();
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('设置成功！');", true);
                }
                else
                {
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('设置失败！');", true);
                }
            }
        }

        //protected void btnML_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("mlProducts.aspx?syncId=" + syncId + "&allIds=" + ((Button)sender).CommandArgument.ToString());
        //}


        public void gvUserDatabind()
        {
            string where = "supplierid=" + Request["sid"] + " and isdel=0 and iseffective=1";
            IList<SC_SupplierSubsidiaryUsers> ssuList = SC_SupplierSubsidiaryUsersManager.GetList(where);
            this.gvUser.DataSource = ssuList;
            this.gvUser.DataBind();
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbUserNameCN = (Label)e.Row.FindControl("lbUserNameCN");
                Label lbUserNameEN = (Label)e.Row.FindControl("lbUserNameEN");
                Label lbUserEmail = (Label)e.Row.FindControl("lbUserEmail");
                Label lbUserPhone = (Label)e.Row.FindControl("lbUserPhone");
                Label lbUserMobile = (Label)e.Row.FindControl("lbUserMobile");
                Label lbUserDep = (Label)e.Row.FindControl("lbUserDep");
                Label lbUserDuties = (Label)e.Row.FindControl("lbUserDuties");
                HiddenField hid = (HiddenField)e.Row.FindControl("hID");
                Literal litEdit = (Literal)e.Row.FindControl("litEdit");

                SC_SupplierSubsidiaryUsers model = SC_SupplierSubsidiaryUsersManager.GetModel(Convert.ToInt32(hid.Value));
                lbUserNameCN.Text = model.Name;
                lbUserNameEN.Text = model.Name_en;
                lbUserEmail.Text = model.Email;
                lbUserPhone.Text = model.Phone;
                lbUserMobile.Text = model.Mobile;
                lbUserDep.Text = model.Departments;
                lbUserDuties.Text = model.Duties;
                litEdit.Text = "<a href='#' onclick=linkManClick('" + model.ID + "')><img src='../../images/dc.gif' border='0px;' title='编辑'></a>";
            }
        }
    }
}
