using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserInfo
{
    public partial class UserInfoUpdateType : System.Web.UI.Page
    {
        private int _supplierId = 0;
        SC_Supplier supplier;
        string script = string.Empty;
        private IList<SC_SupplierType> listSupplierType = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null && int.Parse(Session["user"].ToString()) > 0)
            {
                _supplierId = int.Parse(Session["user"].ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!IsPostBack)
            {
                listSupplierType = SC_SupplierTypeDataProvider.GetListBySupplierId(_supplierId);
                BindRP1();
            }
        }

        private void BindRP1()
        {
            this.rp1.DataSource = SC_TypeDataProvider.GetLevel1List();
            this.rp1.DataBind();
        }

        protected void rp1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SC_Type type1 = e.Item.DataItem as SC_Type;
                if (null != type1)
                {
                    Label lblMain = (Label) e.Item.FindControl("lblMain");
                    lblMain.Text = type1.TypeName;
                    DataList ListLevel2 = (DataList) e.Item.FindControl("ListLevel2");

                    IList<SC_Type> list = SC_TypeDataProvider.GetListByParentId(type1.Id);
                    ListLevel2.DataSource = list;
                    ListLevel2.DataBind();
                }
            }

        }

        protected void List2_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SC_Type type2 = e.Item.DataItem as SC_Type;

                DataList ListLevel3 = (DataList)e.Item.FindControl("ListLevel3");
                ListLevel3.DataSource = SC_TypeDataProvider.GetListByParentId(type2.Id);
                ListLevel3.DataBind();

                CheckBox ckxSelected = (CheckBox)e.Item.FindControl("ckxSelected");
                HiddenField hidTypeID = (HiddenField)e.Item.FindControl("hidTypeID");
                foreach (SC_SupplierType sst in listSupplierType)
                {
                    if (type2.Id == sst.TypeId)
                    {
                        if (ckxSelected != null && hidTypeID != null)
                        {
                            ckxSelected.Checked = true;
                        }

                    }
                }

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SC_SupplierTypeDataProvider.DeleteBySupplierId(_supplierId);

            foreach (DataListItem item in this.rp1.Items)
            {
                DataList ListLevel2 = (DataList) item.FindControl("ListLevel2");
                if (ListLevel2 != null)
                {
                    foreach (DataListItem itemLevel in ListLevel2.Items)
                    {

                        CheckBox ckxSelected = (CheckBox) itemLevel.FindControl("ckxSelected");
                        HiddenField hidTypeID = (HiddenField) itemLevel.FindControl("hidTypeID");

                        decimal itemAmount;
                        if (ckxSelected != null && ckxSelected.Checked && hidTypeID != null &&
                            hidTypeID.Value != string.Empty)
                        {

                            SC_SupplierType st = new SC_SupplierType();
                            st.SupplierId = _supplierId;
                            st.TypeId = Convert.ToInt32(hidTypeID.Value);
                            st.CreatedIP = Page.Request.UserHostAddress;
                            st.CreatTime = DateTime.Now;
                            st.LastModifiedIP = Page.Request.UserHostAddress;
                            st.LastUpdateTime = DateTime.Now;

                            if (SC_SupplierTypeDataProvider.Add(st) > 0)
                            {
                                ClientScript.RegisterStartupScript(typeof (string), "",
                                                                   "alert('物料信息更新成功!');window.location='UserInfo.aspx';",
                                                                   true);
                            }

                            else
                                ClientScript.RegisterStartupScript(typeof (string), "",
                                                                   "alert('联系人信息添加失败!');window.location='UserInfo.aspx';",
                                                                   true);
                        }
                    }
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfo.aspx");
        }
        
    }
}
