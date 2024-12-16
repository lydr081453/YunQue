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
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;

namespace PurchaseWeb.Purchase.TemplateManage
{
    public partial class HeaderItem : System.Web.UI.Page
    {
        public string mName = string.Empty;
        int mSite = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null != Request["name"] && !string.IsNullOrEmpty(Request["name"]))
            {
                mName = Request["name"].ToString();
            }

            if (null != Request["site"] && !string.IsNullOrEmpty(Request["site"]))
            {
                mSite = int.Parse(Request["site"].ToString());
            }

            if (!IsPostBack)
            {
                txtName.Text = "";
                ddlType.SelectedIndex = 0;
                addUse.SelectedValue = "0";
                ddlSite.SelectedValue = "0";

                ListBind();
            }
        }


        private void ListBind()
        {
            if (!string.IsNullOrEmpty(mName))
            {
                DataTable dt = (DataTable)Session["headerTable"];
                DataRow[] dr = dt.Select("Name='" + mName + "'");
                txtName.Text = dr[0]["Name"].ToString();
                ddlType.SelectedValue = dr[0]["Type"].ToString();
                addUse.SelectedValue = dr[0]["Use"].ToString();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["headerTable"];

            if (!string.IsNullOrEmpty(mName))
            {
                DataRow[] dr1 = dt.Select("Name='" + txtName.Text.Trim() + "' and Name<>'" + mName + "'");
                if (dr1.Length > 0)
                {
                    Response.Write("<script>alert('此项目已经存在。请更换一个属性名称。');history.back();</script>");
                }
                else
                {
                    DataRow[] dr = dt.Select("Name='" + mName + "'");
                    dr[0]["Name"] = txtName.Text.Trim();
                    dr[0]["Type"] = ddlType.SelectedValue;
                    dr[0]["Use"] = addUse.SelectedValue;
                }
            }
            else
            {
                DataRow[] dr1 = dt.Select("Name='" + txtName.Text.Trim() + "'");
                if (dr1.Length > 0)
                {
                    Response.Write("<script>alert('此项目已经存在。请更换一个属性名称。');history.back();</script>");
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["Name"] = txtName.Text.Trim();
                    dr["Type"] = ddlType.SelectedValue;
                    dr["Use"] = addUse.SelectedValue;

                    //数据插入位置
                    int tSite = mSite;
                    if (ddlSite.SelectedValue == "0")
                    {
                        tSite = mSite + 1;
                    }

                    if (mSite < 0) mSite = 0;
                    if (mSite >= dt.Rows.Count) mSite = dt.Rows.Count;

                    dt.Rows.InsertAt(dr, tSite);

                }
            }
            Session["headerTable"] = dt;
            txtName.Text = "";
            ddlType.SelectedIndex = 0;
            addUse.SelectedValue = "0";
            ddlSite.SelectedValue = "0";

            string script = " parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();parent.document.location.reload();";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);

        }
    }
}
