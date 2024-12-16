using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Common;
using ESP.Supplier.DataAccess;
using ESP.Supplier.Entity;

namespace SupplierWeb.UserRegister
{
    public partial class UserRegisterCompanyStep3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null && int.Parse(Session["user"].ToString()) > 0)
            {
                Response.Redirect("../UserPage/MainPage.aspx");
            }
            if (Session["Register"] == null)
            {
                ClientScript.RegisterStartupScript(typeof(string), "",
                                                   "alert('超时,请重新填写!');window.location='../Default.aspx';", true);
            }
            else
            {
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            SC_Supplier model = null;
            if (Session["Register"] != null)
            {
                model = Session["Register"] as SC_Supplier;
                Random rnd = new Random(); 

                if (this.Request.Files[0] != null)
                {
                    System.Web.HttpPostedFile myFile = this.Request.Files[0];
                    if (myFile.FileName != "")
                    {
                        model.introfile = upFile("IntroFile_" + model.supplier_nameEN +"_" +  rnd.Next().ToString(), myFile);
                    }
                }
                if (this.Request.Files[1] != null)
                {
                    System.Web.HttpPostedFile myFile = this.Request.Files[1];
                    if (myFile.FileName != "")
                    {
                        model.productfile = upFile("Productfile_" + model.supplier_nameEN + "_" +  rnd.Next().ToString(), myFile);
                    }
                }
                if (this.Request.Files[2] != null)
                {
                    System.Web.HttpPostedFile myFile = this.Request.Files[2];
                    if (myFile.FileName != "")
                    {
                        model.pricefile = upFile("Pricefile_" + model.supplier_nameEN + "_" +  rnd.Next().ToString(), myFile);
                    }
                }

                MD5 m = MD5CryptoServiceProvider.Create();
                byte[] p = m.ComputeHash(Encoding.Default.GetBytes(model.Password));
                model.Password = Encoding.Default.GetString(p);

                model.Status = State.SupplierStatus_save;
                model.CreatTime = DateTime.Now;
                model.LastUpdateTime = DateTime.Now;
                SC_SupplierManager scsm = new SC_SupplierManager();

                int result = scsm.Add(model);
                if (result > 0)
                {
                    string strType = hidTypeIdP.Value;
                    if(!string.IsNullOrEmpty(strType))
                    {
                        string[] sArray = strType.Split(','); 
                        foreach(string s in sArray)
                        {
                            if(!string.IsNullOrEmpty(s))
                            {
                                SC_SupplierType st = new SC_SupplierType();
                                st.SupplierId = result;
                                st.TypeId = int.Parse(s);
                                st.CreatTime = DateTime.Now;
                                st.LastUpdateTime = DateTime.Now;
                                st.CreatedIP = Page.Request.UserHostAddress;
                                st.LastModifiedIP = Page.Request.UserHostAddress;
                                SC_SupplierTypeDataProvider.Add(st);
                            }
                        }
                    }
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('人员注册成功,请等待激活信!');window.location='../Default.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('人员注册失败!');window.location='../Default.aspx';", true);
                }

            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "",
                                                   "alert('超时,请重新填写!');window.location='../Default.aspx';", true);
            }
            if (null != model)
            {
                
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "",
                                                   "alert('人员注册失败!');window.location='../Default.aspx';", true);
            }
        }

        private string upFile(string fileName, HttpPostedFile postFile)
        {
            string directoryPath = Server.MapPath("~") + @"\Uploads\";
            string savePath = "";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string extension = System.IO.Path.GetExtension(postFile.FileName).ToLower();
            savePath = Server.MapPath("~") + @"\Uploads\" + fileName + extension;
            try
            {
                postFile.SaveAs(savePath);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert(@'" + ex.Message + "');", true);
            }
            return "Uploads\\" + fileName + extension;
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            //返回首页
            Response.Redirect("UserRegisterCompanyStep2.aspx");
        }
    }
}
