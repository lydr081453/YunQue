using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.Win32;
using System.Configuration;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Entity;

namespace PurchaseWeb.Purchase.Requisition
{
    public partial class SupplierPriceFils : System.Web.UI.Page
    {
                int supplierId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["supplierId"]))
            {
                supplierId = int.Parse(Request["supplierId"]);
                //SC_Supplier supplierModel = SC_SupplierManager.GetModel(supplierId);
            }

            if (!IsPostBack)
            {
                BindList();
            }
        }

        private void BindList()
        {
            IList<SC_SupplierPriceFiles> list = new SC_SupplierPriceFilesManager().GetList(supplierId);
            gvList.DataSource = list;
            gvList.DataBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            BindList();
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "down")
            {
                SC_SupplierPriceFiles file = new SC_SupplierPriceFilesManager().GetModel(Convert.ToInt32(e.CommandArgument.ToString()));
                if (file != null)
                {
                    DownPaper(file.FileUrl);
                }
            }
        }

        private void DownPaper(string file)
        {
            string a = ConfigurationSettings.AppSettings["SupplierPriceFiles"] + "\\" + file;
            string filename = Server.MapPath(a);
            int intStart = filename.LastIndexOf("\\") + 1;
            string saveFileName = filename.Substring(intStart, filename.Length - intStart);

            System.IO.FileInfo fi = new System.IO.FileInfo(filename);
            string fileextname = fi.Extension;
            string DEFAULT_CONTENT_TYPE = "application/unknown";
            RegistryKey regkey, fileextkey;
            string filecontenttype;
            try
            {
                regkey = Registry.ClassesRoot;
                fileextkey = regkey.OpenSubKey(fileextname);
                filecontenttype = fileextkey.GetValue("Content Type", DEFAULT_CONTENT_TYPE).ToString();
            }
            catch
            {
                filecontenttype = DEFAULT_CONTENT_TYPE;
            }


            Response.Clear();
            Response.Charset = "utf-8";
            Response.Buffer = true;
            this.EnableViewState = false;
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            string downLoadName = HttpUtility.UrlEncode(saveFileName, Encoding.UTF8).Replace("+", "%20");

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + downLoadName);
            Response.ContentType = filecontenttype;

            Response.WriteFile(filename);
            Response.Flush();
            Response.Close();

            Response.End();

        }

    }
}
