using System;
using System.Collections.Generic;
using System.Configuration;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using ESP.Finance.Utility;

public partial class Customer_CustomerInfoEdit : System.Web.UI.Page
{

    public int customerId = 0;
    private IList<ESP.Finance.Entity.CustomerAttachInfo> attlist;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.CustomerID]))
        {
            customerId = int.Parse(Request[ESP.Finance.Utility.RequestName.CustomerID]);

        }
        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    protected void gvG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label labDown = (Label)e.Row.FindControl("labDown");
            labDown.Text = attlist[e.Row.RowIndex].Attachment == null ? "" : "<a target='_blank' href='/Dialogs/CustomerFileDownload.aspx?" + RequestName.CustomerAttachID + "=" + attlist[e.Row.RowIndex].AttachID.ToString() + " '><img src='/images/ico_04.gif' border='0' /></a>";
        }
    }

    private void bindFrame(int cid)
    {
        attlist = ESP.Finance.BusinessLogic.CustomerAttachManager.GetList(" customerid=" + cid);
        this.gvG.DataSource = attlist;
       this.gvG.DataBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerList.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CustomerInfo model = null;
        string openurl = string.Empty;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.CustomerID]))
        {
            customerId = int.Parse(Request[ESP.Finance.Utility.RequestName.CustomerID]);

        }
        if (customerId == 0)
        {
            model = new CustomerInfo();

            model.AppCompany = this.txtAppCompany.Text;
            model.AppDate = DateTime.Now;
            int ret = ESP.Finance.BusinessLogic.CustomerManager.Add(SetModel(model));
            if (ret > 0)
            {
                trFrame.Visible = true;
                customerId = ret;
                openurl = string.Format(" window.open('/Dialogs/FrameDlg.aspx?{0}={1}', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');", RequestName.CustomerID, customerId);
                    ClientScript.RegisterStartupScript(typeof(string), "", "if(confirm('要现在添加框架协议吗?')){" + openurl + "}", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存失败,请检查客户缩写、客户名称是否重复！');", true);
            }
        }
        else
        {
            model = ESP.Finance.BusinessLogic.CustomerManager.GetModel(customerId);
            UpdateResult operResult = ESP.Finance.BusinessLogic.CustomerManager.Update(SetModel(model));
            if (operResult == UpdateResult.Succeed)
            {
                //SaveImage();
                ClientScript.RegisterStartupScript(typeof(string), "", "window.location='CustomerList.aspx';alert('保存成功！');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + operResult.ToString() + "！');", true);
            }
        }
    }

    private CustomerInfo SetModel(CustomerInfo model)
    {
        #region 基础信息
        model.CustomerCode = txtCustomerCode.Text.Trim();
        model.AddressCode = this.txtA0.Text.Trim() + txtCustomerCode.Text.Trim();
        model.ShortEN = txtShortEN.Text.Trim();
        model.NameCN1 = txtNameCN1.Text.Trim();
        model.Address1 = txtAddress1.Text.Trim();
        model.ContactTel = txtContactTel.Text.Trim();
        model.ContactMobile = txtContactTel.Text.Trim();
        model.ContactName = this.txtContactName.Text.Trim();
        model.ContactFax = this.txtContactFax.Text.Trim();
        model.ContactEmail = txtContactEmail.Text.Trim();
        model.Website = this.txtContactWebsite.Text.Trim();
        model.AO = this.txtA0.Text.Trim();
        model.InvoiceTitle = this.txtInvoiceTitle.Text.Trim();

        if (this.hidAreaID.Value != string.Empty)
        {
            model.AreaName = this.txtArea.Text;
            model.AreaCode = this.hidAreaCode.Value;
            model.AreaID = Convert.ToInt32(this.hidAreaID.Value);
        }
        if (this.hidIndustryID.Value != string.Empty)
        {
            model.IndustryName = this.txtIndustry.Text;
            model.IndustryCode = this.hidIndustryCode.Value;
            model.IndustryID = Convert.ToInt32(this.hidIndustryID.Value);
        }
        if(!string.IsNullOrEmpty(txtRebateRate.Text)){
            model.RebateRate = decimal.Parse(txtRebateRate.Text.Trim()) / 100;
        }

        #endregion

        return model;
    }

    private void BindInfo()
    {
        if (customerId == 0)
        {
            this.txtAppDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.txtAppDate.Enabled = false;
            trFrame.Visible = false; ;
            //this.txtTaxRate.Text = "0.00";
            return;
        }
        CustomerInfo model = ESP.Finance.BusinessLogic.CustomerManager.GetModel(customerId);
        bindFrame(customerId);
        if (model != null)
        {
            txtCustomerCode.Text = model.CustomerCode;
            txtAddressCode.Text = model.AddressCode;
            txtNameCN1.Text = model.NameCN1;
            txtShortEN.Text = model.ShortEN;
            txtAddress1.Text = model.Address1;
            txtContactName.Text = model.ContactName;
            txtInvoiceTitle.Text = model.InvoiceTitle;
            //this.txtTaxRate.Text = model.DefaultTaxRate.ToString();

            //if (model.DefaultTaxRate != null)
            //    txtTaxRate.Text = model.DefaultTaxRate.ToString();
            txtArea.Text = model.AreaName;
            hidAreaCode.Value = model.AreaCode;
            hidAreaID.Value = model.AreaID.ToString();

            txtIndustry.Text = model.IndustryName;
            hidIndustryCode.Value = model.IndustryCode;
            hidIndustryID.Value = model.IndustryID.ToString();

            txtContactTel.Text = model.ContactTel;
            this.txtContactFax.Text = model.ContactFax;
            txtContactEmail.Text = model.ContactEmail;
            this.txtContactWebsite.Text = model.Website;
            this.txtA0.Text = model.AO;

            this.txtAppCompany.Text = model.AppCompany;
            this.txtAppDate.Text = Convert.ToDateTime(model.AppDate).ToString("yyyy-MM-dd");
            this.txtAppCompany.Enabled = false;
            this.txtAppDate.Enabled = false;

            btnAppCompany.Visible = false;
            txtRebateRate.Text = ((model.RebateRate??0)*100).ToString("0.00");
        }
    }

    protected void gvG_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int attchId = int.Parse(e.CommandArgument.ToString());
            var projectListCount = ProjectManager.GetList(" and customerAttachId like '%,"+ attchId +",%'").Count;
            if (projectListCount <= 0)
            {
                if (ESP.Finance.BusinessLogic.CustomerAttachManager.Delete(attchId) == ESP.Finance.Utility.DeleteResult.Succeed)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！已有项目关联该框架协议');", true);
            }
            bindFrame(customerId);
        }
    }

}