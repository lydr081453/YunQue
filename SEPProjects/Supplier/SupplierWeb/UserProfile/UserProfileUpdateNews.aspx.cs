using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserProfile
{
    public partial class UserProfileUpdateNews : System.Web.UI.Page
    {
        private int _supplierId = 0;
        private int _infomationId = 0;
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
            if (null != Request["iid"] && !string.IsNullOrEmpty(Request["iid"]))
            {
                _infomationId = int.Parse(Request["iid"].ToString());
            }
            if (!IsPostBack)
            {
                SC_SupplierInfomation model = (new SC_SupplierInfomationDataProvider()).GetModel(_infomationId);

                if (null != model)
                {
                    BindInfo(model);
                }
            }

        }

        private void BindInfo(SC_SupplierInfomation model)
        {
            if (null != model)
            {
                txtTitle.Text = model.Title.ToString();
                txtContent.Text = model.Content.ToString();
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SC_SupplierInfomationDataProvider dp = new SC_SupplierInfomationDataProvider();
            SC_SupplierInfomation model;
            if (_infomationId > 0)
            {
                model = dp.GetModel(_infomationId);
            }
            else
            {
                model = new SC_SupplierInfomation();
                model.SupplierId = _supplierId;
                model.CreatedIP = Page.Request.UserHostAddress;
                model.CreatTime = DateTime.Now;
            }

            model.Title = txtTitle.Text.Trim();
            model.Content = txtContent.Text.Trim();
            model.LastUpdateTime = DateTime.Now;
            model.LastModifiedIP = Page.Request.UserHostAddress;

            if (_infomationId > 0)
            {
                dp.Update(model);
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('信息更新成功!');window.location='UserProfile.aspx';", true);
            }
            else
            {
                if (dp.Add(model) > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('信息添加成功!');window.location='UserProfile.aspx';", true);
                }
                else
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('信息添加失败!');window.location='UserProfile.aspx';", true);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx");
        }
    }
}
