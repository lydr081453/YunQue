using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserInfo
{
    public partial class UserInfoUpdateIntro : System.Web.UI.Page
    {
        private int _supplierId = 0;
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
                SC_SupplierManager ssm = new SC_SupplierManager();
                SC_Supplier model = ssm.GetModel(_supplierId);

                if (null != model)
                {
                    BindInfo(model);
                }
            }
        }

        private void BindInfo(SC_Supplier model)
        {
            if (null != model)
            {
                txtNeiRong.Value = model.supplier_Intro;
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SC_SupplierManager ssm = new SC_SupplierManager();
            SC_SupplierHistoryManager sshm = new SC_SupplierHistoryManager();
            SC_Supplier model = ssm.GetModel(_supplierId);

            if (null != model)
            {
                SC_SupplierHistory shmodel = sshm.SetModel(model);

                model.supplier_Intro = txtNeiRong.Value;
                model.LastUpdateTime = DateTime.Now;

                if (sshm.Add(shmodel) > 0)
                {
                    ssm.Update(model);
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('信息更新成功!');window.location='UserInfo.aspx';", true);
                }
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('信息更新失败!');window.location='UserInfo.aspx';", true);

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfo.aspx");
        }
    }
}
