using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Purchase.Common;

public partial class UserControls_Edit_supplierInfo : System.Web.UI.UserControl
{
    private ESP.Purchase.Entity.GeneralInfo model;
    public ESP.Purchase.Entity.GeneralInfo Model
    {
        set { model = value; }
        get { return model; }
    }

    private bool _iscansave = false;
    public bool isCanSave
    {
        set { _iscansave = value; }
        get { return _iscansave; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btnEditUser.Attributes["onclick"] = "showUserList('" + Request[RequestName.GeneralID] + "');return false;";
        btnEditUser1.Attributes["onclick"] = "showUserList('" + Request[RequestName.GeneralID] + "');return false;";
        if (ddlsource.SelectedItem.Text == "客户指定")
        {
            divEmail.Style["display"] = "block";
        }

        if (!IsPostBack)
        {
            //radOperationType.Attributes.Add("onclick", "javascript:SupplierTypeChange();");
            palView.Visible = false;
        }
    }

    protected void supplier_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtsupplier_name.SelectedValue != null)
        {
            ESP.Purchase.Entity.SupplierInfo supplier = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(int.Parse(txtsupplier_name.SelectedValue));
            txtsupplier_name.Text = supplier.supplier_name;
            txtsupplier_address.Text = supplier.contact_address;
            txtsupplier_linkman.Text = supplier.contact_name;
            if (supplier.contact_tel.Split('-').Length == 4)
            {
                txtsupplier_con.Text = supplier.contact_tel;//.Split('-')[0];
                //txtsupplier_area.Text = supplier.contact_tel.Split('-')[1];
                //txtsupplier_phone.Text = supplier.contact_tel.Split('-')[2];
                //txtsupplier_ext.Text = supplier.contact_tel.Split('-')[3];
            }
            if (supplier.contact_fax.Split('-').Length == 4)
            {
                txtsupplierfax_con.Text = supplier.contact_fax;//.Split('-')[0];
                //txtsupplierfax_area.Text = supplier.contact_fax.Split('-')[1];
                //txtsupplierfax_phone.Text = supplier.contact_fax.Split('-')[2];
                //txtsupplierfax_ext.Text = supplier.contact_fax.Split('-')[3];
            }
            txtsupplier_email.Text = supplier.contact_email;
            txtfa_no.Text = supplier.supplier_frameNO;
            txtaccountBank.Text = supplier.account_bank;
            txtaccountName.Text = supplier.account_name;
            txtaccountNum.Text = supplier.account_number;
        }
        Page.SetFocus(txtaccountBank);
    }

    public ESP.Purchase.Entity.GeneralInfo setModelInfo()
    {
        Model.supplier_name = txtsupplier_name.Text.Trim();
        Model.supplier_address = txtsupplier_address.Text.Trim();
        Model.supplier_linkman = txtsupplier_linkman.Text.Trim();

        string supplier_con = txtsupplier_con.Text.Trim();
        //string supplier_area = txtsupplier_area.Text.Trim();
        //if (!string.IsNullOrEmpty(txtsupplier_phone.Text))
        //{
        //    if (string.IsNullOrEmpty(supplier_con))
        //        supplier_con = "86";
        //    if (string.IsNullOrEmpty(supplier_area))
        //        supplier_area = "010";
        //}

        string supplierfax_con = txtsupplierfax_con.Text.Trim();
        //string supplierfax_area = txtsupplierfax_area.Text.Trim();
        //if (!string.IsNullOrEmpty(txtsupplierfax_phone.Text))
        //{
        //    if (string.IsNullOrEmpty(supplierfax_con))
        //        supplierfax_con = "86";
        //    if (string.IsNullOrEmpty(supplierfax_area))
        //        supplierfax_area = "010";
        //}
        Model.supplier_phone = supplier_con;// +"-" + supplier_area + "-" + txtsupplier_phone.Text.Trim() + "-" + txtsupplier_ext.Text;
        Model.Supplier_cellphone = txtsupplier_cellphone.Text.Trim();
        Model.supplier_fax = supplierfax_con;// +"-" + supplierfax_area + "-" + txtsupplierfax_phone.Text + "-" + txtsupplierfax_ext.Text;
        Model.supplier_email = txtsupplier_email.Text.Trim();
        Model.source = txtsource.Text.Trim();
        Model.fa_no = txtfa_no.Text.Trim();
        if (Model.source == "客户指定")
        {
            if (null != filEmailFile.PostedFile && filEmailFile.PostedFile.FileName != "")
            {
                string fileName = string.IsNullOrEmpty(model.CusAskEmailFile) ? ("cursask_" + model.id + "_" + DateTime.Now.Ticks.ToString()) : model.CusAskEmailFile.Split('\\')[1].ToString().Split('.')[0].ToString();
                model.CusAskEmailFile = FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, filEmailFile);
            }
        }
        else
        {
            model.CusAskEmailFile = "";
        }
        Model.account_bank = txtaccountBank.Text.Trim();
        Model.account_name = txtaccountName.Text.Trim();
        Model.account_number = txtaccountNum.Text.Trim();
        //if (null != radOperationType.SelectedValue && "" != radOperationType.SelectedValue)
        //{
        //    Model.OperationType = int.Parse(radOperationType.SelectedValue);
        //}
        //else
        //{
            Model.OperationType = 0;
        //}
        //if (chkHaveInvoice.Visible)
        //    Model.HaveInvoice = chkHaveInvoice.Checked;
        //else
            Model.HaveInvoice = false;
        Model.Requisitionflow = !string.IsNullOrEmpty(rblrequisitionflow.SelectedValue) ? int.Parse(rblrequisitionflow.SelectedValue) : 0;
        if (model.Requisitionflow != State.requisitionflow_toO)
            model.orderid = "";
        return model;
    }

    /// <summary>
    /// 将数据源绑定到被调用的服务器控件及其所有子控件。
    /// </summary>
    public void radioBind()
    {
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toFC], State.requisitionflow_toFC.ToString()));
        //rblrequisitionflow.SelectedValue = State.requisitionflow_toR.ToString();
    }

    protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(rblrequisitionflow.SelectedValue) == State.requisitionflow_toO)
        {
            RequiredFieldValidator7.Enabled = true ;
            labStar.Visible = true;
        }
        else
        {
            RequiredFieldValidator7.Enabled = false;
            labStar.Visible = false;
        }
        Page.SetFocus(txtfa_no.ClientID);
    }

    //protected void radOperationType_SelectIndexChanged(object sender, EventArgs e)
    //{
    //    chkHaveInvoice.Visible = radOperationType.SelectedValue == State.OperationTypePri.ToString();
    //}

    public void BindInfo()
    {
        radioBind();
        //radOperationType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePub], State.OperationTypePub.ToString()));
        //radOperationType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePri], State.OperationTypePri.ToString()));
        //radOperationType.SelectedIndex = 0;

        txtsupplier_name.Text = Model.supplier_name;
        txtsupplier_address.Text = Model.supplier_address;
        txtsupplier_linkman.Text = Model.supplier_linkman;
        //if (Model.supplier_phone.Split('-').Length == 4)
        //{
            txtsupplier_con.Text = Model.supplier_phone;//.Split('-')[0];
            //txtsupplier_area.Text = Model.supplier_phone.Split('-')[1];
            //txtsupplier_phone.Text = Model.supplier_phone.Split('-')[2];
            //txtsupplier_ext.Text = Model.supplier_phone.Split('-')[3];
        //}

        txtsupplier_cellphone.Text = Model.Supplier_cellphone;
        //if (Model.supplier_fax.Split('-').Length == 4)
        //{
            txtsupplierfax_con.Text = Model.supplier_fax;//.Split('-')[0];
            //txtsupplierfax_area.Text = Model.supplier_fax.Split('-')[1];
            //txtsupplierfax_phone.Text = Model.supplier_fax.Split('-')[2];
            //txtsupplierfax_ext.Text = Model.supplier_fax.Split('-')[3];
        //}
        txtsupplier_email.Text = Model.supplier_email;
        ddlsource.SelectedValue = Model.source;
        txtsource.Text = Model.source;
        txtfa_no.Text = Model.fa_no;
        txtaccountBank.Text = Model.account_bank;
        txtaccountName.Text = Model.account_name;
        txtaccountNum.Text = Model.account_number;
        //radOperationType.SelectedValue = model.OperationType.ToString();
        //chkHaveInvoice.Visible = radOperationType.SelectedValue == State.OperationTypePri.ToString();
        //chkHaveInvoice.Checked = model.HaveInvoice;

        //labOperationType.Text = State.OperationTypeShow[Model.OperationType].ToString();
        if (Model.OperationType == State.OperationTypePub)
        {
            labSAShow.Text = "供应商地址";
        }
        else
        {
            labSAShow.Text = "身份证";
        }
        labsupplier_name.Text = Model.supplier_name;
        labsupplier_address.Text = Model.supplier_address;
        labsupplier_linkman.Text = Model.supplier_linkman;
       // labsupplier_phone.Text = Model.supplier_phone;
        labsupplier_cellphone.Text = Model.Supplier_cellphone;
       // labsupplier_fax.Text = Model.supplier_fax;
        labsupplier_email.Text = Model.supplier_email == "" ? "无" : Model.supplier_email;
        labsource.Text = Model.source;
        labfa_no.Text = Model.fa_no;
        
        if (Model.source == "协议供应商")
        {
            viewControl("协议供应商");
            RequiredFieldValidator4.Enabled = false;
            RequiredFieldValidator5.Enabled = false;
            RequiredFieldValidator6.Enabled = false;
        }
        else
        {
            viewControl("非协议供应商");
        }
        if (model.source == "客户指定")
        {
            //palEmailFile.Visible = true;
            labEmailFile.Text = model.CusAskEmailFile == "" ? "" : "<a target='_blank' href='/Purchase/Requisition/PrFileDownLoad.aspx?GeneralId=" + Model.id.ToString() + "&Index=0&Type=CusAskEmailFile'><img src='/images/ico_04.gif' border='0' /></a>";
        }
        else if (Model.source == "临时供应商")
        {
            txtsupplier_name.DataSource = ESP.Purchase.BusinessLogic.SupplierManager.getModelList(" and supplier_type = " + (int)State.supplier_type.noAgreement, new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>());
            txtsupplier_name.DataBind();
        }
        else if (Model.source == "采购部推荐")
        {
            txtsupplier_name.DataSource = ESP.Purchase.BusinessLogic.SupplierManager.getModelList(" and supplier_type = " + (int)State.supplier_type.recommend, new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>());
            txtsupplier_name.DataBind();
        }
        rblrequisitionflow.SelectedValue = Model.Requisitionflow.ToString();
        if (int.Parse(rblrequisitionflow.SelectedValue) == State.requisitionflow_toO)
        {
            RequiredFieldValidator7.Enabled = true;
            labStar.Visible = true;
        }
        else
        {
            RequiredFieldValidator7.Enabled = false;
            labStar.Visible = false;
        }
    }

    public void SetSupplyInfo()
    {
        ESP.Supplier.Entity.SC_Supplier supply = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetModel(int.Parse(Request["supplyId"]));

        radioBind();
        //radOperationType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePub], State.OperationTypePub.ToString()));
        //radOperationType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePri], State.OperationTypePri.ToString()));
        //radOperationType.SelectedIndex = 0;

        txtsupplier_name.Text = supply.supplier_name;
        txtsupplier_name.Enabled = false;
        if (supply.IsPerson == 1)
        {
            labSAShow.Text = "身份证：";
            txtsupplier_address.Text = "";
            //radOperationType.SelectedValue = "1";
            //radOperationType_SelectIndexChanged(new object(), new EventArgs());
        }
        else
        {
            labSAShow.Text = "供应商地址";
            txtsupplier_address.Text = supply.contact_address;
        }
        txtsupplier_linkman.Text = "";

        txtsupplier_cellphone.Text = supply.contact_Mobile;
        txtsupplier_email.Text = supply.contact_Email;
        ddlsource.SelectedValue = "采购部推荐";
        ddlsource.Enabled = false;
        txtsource.Text = "采购部推荐";
        txtaccountBank.Text = supply.account_bank;
        txtaccountName.Text = supply.account_name;
        txtaccountNum.Text = supply.account_number;

            viewControl("非协议供应商");

    }

    public ESP.Purchase.Entity.SupplierInfo GetNewSupplierModel()
    {
        ESP.Purchase.Entity.SupplierInfo newSupplier = new ESP.Purchase.Entity.SupplierInfo();
        newSupplier.supplier_name = txtsupplier_name.Text;
        newSupplier.contact_address = txtsupplier_address.Text.Trim();
        newSupplier.contact_name = txtsupplier_linkman.Text.Trim();

        string supplier_con = txtsupplier_con.Text.Trim();
        //string supplier_area = txtsupplier_area.Text.Trim();
        //if (!string.IsNullOrEmpty(txtsupplier_phone.Text))
        //{
        //    if (string.IsNullOrEmpty(supplier_con))
        //        supplier_con = "86";
        //    if (string.IsNullOrEmpty(supplier_area))
        //        supplier_area = "010";
        //}

        string supplierfax_con = txtsupplierfax_con.Text.Trim();
        //string supplierfax_area = txtsupplierfax_area.Text.Trim();
        //if (!string.IsNullOrEmpty(txtsupplierfax_phone.Text))
        //{
        //    if (string.IsNullOrEmpty(supplierfax_con))
        //        supplierfax_con = "86";
        //    if (string.IsNullOrEmpty(supplierfax_area))
        //        supplierfax_area = "010";
        //}
        newSupplier.contact_tel = supplier_con;// +"-" + supplier_area + "-" + txtsupplier_phone.Text.Trim() + "-" + txtsupplier_ext.Text;
        newSupplier.contact_mobile = txtsupplier_cellphone.Text.Trim();
        newSupplier.contact_fax = supplierfax_con;// +"-" + supplierfax_area + "-" + txtsupplierfax_phone.Text + "-" + txtsupplierfax_ext.Text;
        newSupplier.contact_email = txtsupplier_email.Text.Trim();
        newSupplier.supplier_source = txtsource.Text.Trim();
        newSupplier.account_bank = txtaccountBank.Text.Trim();
        newSupplier.account_name = txtaccountName.Text.Trim();
        newSupplier.account_number = txtaccountNum.Text.Trim();
        newSupplier.supplier_type = (int)State.supplier_type.recommend;
        return newSupplier;
    }


    protected void ddlsource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsource.SelectedItem.Text == "临时供应商")
        {
            txtsupplier_name.DataSource = ESP.Purchase.BusinessLogic.SupplierManager.getModelList(" and supplier_type = " + (int)State.supplier_type.noAgreement, new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>());
            txtsupplier_name.DataBind();
        }
        else if (ddlsource.SelectedItem.Text == "采购部推荐")
        {
            txtsupplier_name.DataSource = ESP.Purchase.BusinessLogic.SupplierManager.getModelList(" and supplier_type = " + (int)State.supplier_type.recommend, new System.Collections.Generic.List<System.Data.SqlClient.SqlParameter>());
            txtsupplier_name.DataBind();
        }
        else
        {
            txtsupplier_name.DataSource = new DataTable();
            txtsupplier_name.DataBind();
        }
        Page.SetFocus(txtaccountBank);
    }

    public void viewControl(string source)
    {
        txtsource.Style["display"] = "none";
  
        txtsupplier_name.Style["display"] = "block";
        txtsupplier_address.Style["display"] = "block";
        txtsupplier_linkman.Style["display"] = "block";
        txtsupplier_con.Style["display"] = "block";
       // txtsupplier_area.Style["display"] = "block";
       // txtsupplier_phone.Style["display"] = "block";
       // txtsupplier_ext.Style["display"] = "block";
        txtsupplier_cellphone.Style["display"] = "block";
        txtsupplierfax_con.Style["display"] = "block";
       // txtsupplierfax_area.Style["display"] = "block";
       // txtsupplierfax_phone.Style["display"] = "block";
       // txtsupplierfax_ext.Style["display"] = "block";
        txtsupplier_email.Style["display"] = "block";
        ddlsource.Style["display"] = "block";
        //radOperationType.Style["display"] = "block";
        CompareValidator1.Enabled = true;

        labsupplier_name.Style["display"] = "none";
        labsupplier_address.Style["display"] = "none";
        labsupplier_linkman.Style["display"] = "none";
       // labsupplier_phone.Style["display"] = "none";
        labsupplier_cellphone.Style["display"] = "none";
       // labsupplier_fax.Style["display"] = "none";
        labsupplier_email.Style["display"] = "none";
        labfa_no.Style["display"] = "none";
        labsource.Style["display"] = "none";
        //labOperationType.Style["display"] = "none";
       

            if (source.Equals("协议供应商"))
            {
                ddlsource.Items.Insert(ddlsource.Items.Count, new ListItem("协议供应商", "协议供应商"));
                ddlsource.SelectedValue = "协议供应商";

                txtfa_no.Visible = true;
                labfa_no.Visible = false;
            }           
    }

    public bool PreView()
    {
        setModelInfo();
        palView.Visible = true;
        isCanSave = true;
        //labOperationType1.Text = radOperationType.SelectedItem.Text;
        //radOperationType.SelectedValue = model.OperationType.ToString();
        labsupplier_address1.Text = model.supplier_address;
        if (Model.OperationType > 0)
        {
            labOperationType1.Text =
                "<font style='color:Blue'>您选择的是：[个人]，是否确认无误？</font>";
            labSAShow1.Text = labSAShow.Text = "身份证";
            string checkresult = CheckCidInfo(model.supplier_address);
            if (checkresult != "<font style='color:green'>合法</font>")
            {
                isCanSave = false;
            }
            labsupplier_address1.Text = model.supplier_address + "（" + checkresult + "）";
        }
        else
        {
            labOperationType1.Text = "<font style='color:Blue'>您选择的是：[对公]，是否确认无误？</font>";
            labSAShow1.Text = labSAShow.Text = "供应商名称";
        }
        labSNShow1.Text = labSNShow.Text;
        labsupplier_name1.Text = model.supplier_name;
        labsource1.Text = ddlsource.SelectedItem.Text;
        labSAShow1.Text = labSAShow.Text;
        //labEmailFile1.Text = labEmailFile.Text;
        labsupplier_phone1.Text = model.supplier_phone;
        labsupplier_linkman1.Text = txtsupplier_linkman.Text;
        labsupplier_fax1.Text = model.supplier_fax;
        labsupplier_cellphone1.Text = txtsupplier_cellphone.Text;

        //labfa_no1.Text = "";
        labsupplier_email1.Text = txtsupplier_email.Text;
        labaccountName1.Text = txtaccountName.Text;
        labaccountBank1.Text = txtaccountBank.Text;
        labaccountNum1.Text = txtaccountNum.Text;

        return isCanSave;
    }

    private   string   CheckCidInfo(string   cid)
    {
        string[] aCity = new string[]
                             {
                                 null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西",
                                 "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null
                                 , null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东"
                                 , "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null
                                 , null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null
                                 , null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null
                                 , null, "国外"
                             };
        double iSum = 0;
        string info = "";
        System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|x)$");
        System.Text.RegularExpressions.Match mc = rg.Match(cid);
        if (!mc.Success)
        {
            return "<font style='color:red'>身份证格式有误</font>";
        }
        cid = cid.ToLower();
        cid = cid.Replace("x", "a");
        if (aCity[int.Parse(cid.Substring(0, 2))] == null)
        {
            return "<font style='color:red'>地区编码有误</font>";
        }
        try
        {
            DateTime.Parse(cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2));
        }
        catch
        {
            return "<font style='color:red'>生日有误</font>";
        }
        for (int i = 17; i >= 0; i--)
        {
            iSum += (System.Math.Pow(2, i)%11)*
                    int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);

        }
        if (iSum%11 != 1)
            return ("<font style='color:red'>身份证号有误</font>");
        return "<font style='color:green'>合法</font>";
    }

}
