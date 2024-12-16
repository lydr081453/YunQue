using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class ProvinceEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountries();
                BindItem();
            }
        }

        private void BindCountries()
        {
            this.ddlCountry.DataSource = ESP.MediaLinq.BusinessLogic.CountryManager.GetList();
            this.ddlCountry.DataBind();
        }

        private void BindItem()
        {
            int id = 0;
            if (!string.IsNullOrEmpty(Request["pid"]) && int.TryParse(Request["pid"], out id))
            {
                media_ProvinceInfo info = ESP.MediaLinq.BusinessLogic.ProvinceManager.GetModel(id);
                if (info != null)
                {
                    this.txtProvince_Name.Text = info.Province_Name;
                    this.ddlCountry.SelectedValue = info.Country_ID.ToString();
                }
            }
        }

        private bool Save()
        {
            media_ProvinceInfo info = new media_ProvinceInfo();
            info.Province_Name = this.txtProvince_Name.Text;
            info.Country_ID = Convert.ToInt32(this.ddlCountry.SelectedValue);
            try
            {
                int id = 0;
                if (!string.IsNullOrEmpty(Request["pid"]) && int.TryParse(Request["pid"], out id))
                {
                    media_ProvinceInfo exinfo = ESP.MediaLinq.BusinessLogic.ProvinceManager.GetModel(id);
                    if (info != null)
                    {
                        info.Province_ID = exinfo.Province_ID;
                        ESP.MediaLinq.BusinessLogic.ProvinceManager.Update(info);
                        return true;
                    }
                    else
                    {
                        return false; 
                    }
                }
                else
                {
                    ESP.MediaLinq.BusinessLogic.ProvinceManager.Add(info);
                    return true;    
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功！');", true);
                Response.Redirect("ProvinceList.aspx");
            }
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存失败，出现错误！');", true);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProvinceList.aspx");
        }
    }
}