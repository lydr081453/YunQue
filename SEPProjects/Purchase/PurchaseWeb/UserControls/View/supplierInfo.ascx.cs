using System;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class UserControls_View_supplierInfo : System.Web.UI.UserControl
{
    private GeneralInfo model;
    public GeneralInfo Model
    {
        set { model = value; }
        get { return model; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btnEditUser.Attributes["onclick"] = "showUserList('" + Request[RequestName.GeneralID] + "');return false;";
        if (model != null && model.PRType == (int)ESP.Purchase.Common.PRTYpe.PR_OtherAdvertisement)
        {
            this.btn.Visible = true;
        }
    }

    public void BindInfo()
    {
        if (Model == null)
        {
            Model = GeneralInfoManager.GetModel(int.Parse(Request[RequestName.GeneralID]));
        }
        txtsupplier_name.Text = Model.supplier_name;
        txtsupplier_address.Text = Model.supplier_address;
        txtsupplier_linkman.Text = Model.supplier_linkman;
        txtsupplier_phone.Text = Model.supplier_phone;
        txtsupplier_cellphone.Text = Model.Supplier_cellphone;
        txtsupplier_fax.Text = Model.supplier_fax;
        txtsupplier_email.Text = Model.supplier_email;
        txtsource.Text = Model.source;
        txtfa_no.Text = Model.fa_no;
        labEmailFile.Text = model.CusAskEmailFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownload.aspx?GeneralId=" + Model.id.ToString() + "&Index=0&Type=CusAskEmailFile'><img src='/images/ico_04.gif' border='0' /></a>";

        labaccountBank.Text = Model.account_bank;
        labaccountName.Text = Model.account_name;
        labaccountNum.Text = Model.account_number;
        labOperationType.Text = "";// State.OperationTypeShow[Model.OperationType].ToString();
        //if (Model.OperationType == State.OperationTypePri)
        //    labOperationType.Text += Model.HaveInvoice ? " [存在发票]" : " [不存在发票]";
        if (Model.OperationType == State.OperationTypePub)
        {
            labSAShow.Text = "供应商地址";
        }
        else
        {
            labSAShow.Text = "身份证";
        }
    }


    protected void btnUpdateInfo_Click(object sender, EventArgs e)
    {
        BindInfo();
        Page.SetFocus(txtsupplier_name);
    }
}
