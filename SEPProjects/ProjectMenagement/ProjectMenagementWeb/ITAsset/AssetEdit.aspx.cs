using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.ITAsset
{
    public partial class AssetEdit : System.Web.UI.Page
    {
        int assetId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["assetId"]))
            {
                assetId = int.Parse(Request["assetId"]);
            }

            txtCode.Enabled = false;

            if (!IsPostBack)
            {
                bindCategory();
                bindInfo();
            }
        }

        private void bindCategory()
        {
            IList<ESP.Finance.Entity.ITAssetCategoryInfo> category = ESP.Finance.BusinessLogic.ITAssetCategoryManager.GetList();
            this.ddlCategory.DataSource = category;
            this.ddlCategory.DataTextField = "Category";
            this.ddlCategory.DataValueField = "Id";
            this.ddlCategory.DataBind();
        }

        private void bindInfo()
        {
            if (!string.IsNullOrEmpty(Request["assetId"]))
            {
                assetId = int.Parse(Request["assetId"]);
            }
            if (assetId > 0)
            {


                ESP.Finance.Entity.ITAssetsInfo model = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(assetId);
                if (model != null && !string.IsNullOrEmpty(model.UpFile))
                {
                    hpFile.Visible = true;
                    hpFile.ToolTip = "下载附件：" + model.UpFile;
                    this.hpFile.NavigateUrl = "FileDownLoad.aspx?assetId=" + model.Id.ToString();
                }

                if (model != null && !string.IsNullOrEmpty(model.Photo))
                {
                    imgPhoto.Visible = true;
                    imgPhoto.ImageUrl = ESP.Configuration.ConfigurationManager.Items["FinancialHeader"] + "/images/ITAssets/" + model.Photo;
                }

                txtBrand.Text = model.Brand;
                txtCode.Text = model.SerialCode;
                txtConfig.Text = model.Configuration;
                txtDate.Text = model.PurchaseDate.ToString("yyyy-MM-dd");
                txtDesc.Text = model.AssetDesc;
                txtModel.Text = model.Model;
                txtName.Text = model.AssetName;
                txtPrice.Text = model.Price.ToString("#,##0.00");
                txtPR.Text = model.RelationPO;
                ddlCategory.SelectedValue = model.CategoryId.ToString();
                ddlArea.SelectedValue = model.SerialCode.Substring(0,2);
            }


        }

        private string CreateSerialCode(string area, string category)
        {
            string sno = string.Empty;
            sno = area + "-A-" + category + "-";

            List<SqlParameter> parms = new List<SqlParameter>();

            string terms = " and serialcode like @serialcode+'%'";

            parms.Add(new SqlParameter("@serialcode", sno));
            var list = ESP.Finance.BusinessLogic.ITAssetsManager.GetList(terms, parms);

            int sortno = list.Count() + 1;
            string sortnoStr = sortno.ToString();

            while (sortnoStr.Length < 4)
            {
                sortnoStr = "0" + sortnoStr;
            }
            sno = sno + sortnoStr;

            return sno;
        }

        private ESP.Finance.Entity.ITAssetsInfo getModel()
        {
            if (!string.IsNullOrEmpty(Request["assetId"]))
            {
                assetId = int.Parse(Request["assetId"]);
            }
            ESP.Finance.Entity.ITAssetsInfo model = null;
            if (assetId > 0)
            {
                model = ESP.Finance.BusinessLogic.ITAssetsManager.GetModel(assetId);
            }
            else
            {
                model = new ESP.Finance.Entity.ITAssetsInfo();
                model.Status = (int)ESP.Finance.Utility.FixedAssetStatus.Normal;


                model.ScrapAuditDate = new DateTime(1900, 1, 1);

                model.ScrapDate = new DateTime(1900, 1, 1);

                model.ScrapLeaderDate = new DateTime(1900, 1, 1);
            }
            model.Brand = txtBrand.Text;

            model.Configuration = txtConfig.Text;
            model.PurchaseDate = DateTime.Parse(txtDate.Text);
            model.AssetDesc = txtDesc.Text;
            model.Model = txtModel.Text;
            model.AssetName = txtName.Text;
            model.Price = decimal.Parse(txtPrice.Text);
            model.RelationPO = txtPR.Text;
            model.EditDate = DateTime.Now;

            ESP.Finance.Entity.ITAssetCategoryInfo category = ESP.Finance.BusinessLogic.ITAssetCategoryManager.GetModel(int.Parse(ddlCategory.SelectedValue));

            if (string.IsNullOrEmpty(txtCode.Text))
            {
                model.SerialCode = this.CreateSerialCode(ddlArea.SelectedValue, category.Sort.ToString());
            }
            model.CategoryId = category.Id;
            model.CategoryName = category.Category;

            if (this.fileUp.FileName != string.Empty)
            {
                string fileName = "Asset_" + Guid.NewGuid().ToString() + "_" + this.fileUp.FileName;

                this.fileUp.SaveAs(ESP.Finance.Configuration.ConfigurationManager.ContractPath + fileName);

                if (fileName != string.Empty)
                    model.UpFile = fileName;
            }

            if (this.filePhoto.FileName != string.Empty)
            {
                string fileName = "Asset_Photo_" + Guid.NewGuid().ToString() + "_" + this.filePhoto.FileName;

                this.filePhoto.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "\\images\\ITAssets\\" + fileName);

                if (fileName != string.Empty)
                    model.Photo = fileName;
            }


            return model;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["assetId"]))
            {
                assetId = int.Parse(Request["assetId"]);
            }
            if (assetId > 0)
            {

                if (ESP.Finance.BusinessLogic.ITAssetsManager.Update(getModel()) == 1)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location='AssetList.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
                }
            }
            else
            {
                if (ESP.Finance.BusinessLogic.ITAssetsManager.Add(getModel()) > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location='AssetList.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssetList.aspx");
        }

    }
}