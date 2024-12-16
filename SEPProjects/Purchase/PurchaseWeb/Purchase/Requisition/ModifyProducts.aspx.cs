using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.IO;

public partial class Purchase_Requisition_ModifyProducts : ESP.Purchase.WebPage.EditPageForPR
{
    int generalid = 0;
    int orderid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        if (!string.IsNullOrEmpty(Request["orderid"]))
        {
            orderid = int.Parse(Request["orderid"]);
        }
        btnPriceAtt.Attributes["onclick"] = "showPriceAtt('" + generalid.ToString() + "','" + orderid.ToString() + "');return false;";
        if (!IsPostBack)
        {
            labMoneyType.Text = GeneralInfoManager.GetModel(generalid).moneytype;
            InitPage();
        }
    }

    private void InitPage()
    {
        OrderInfo model = OrderInfoManager.GetModel(orderid);
        TypeInfo typeModel = TypeManager.GetModel(model.producttype);

        txtName.Text = model.Item_No;

        TypeInfo typeModel2 = TypeManager.GetModel(typeModel.parentId);
        TypeInfo typeModel1 = TypeManager.GetModel(typeModel2.parentId);
        labProductType.Text = typeModel1.typename + " - " + typeModel2.typename + " - " + typeModel.typename;

        hidProductTypeId.Value = typeModel.typeid.ToString();
        txtPrice.Text = model.price.ToString("#,##0.####");
        labUnit.Text = model.uom;
        labSupplierName.Text = model.supplierName;
        hidSupplierId.Value = model.supplierId.ToString();

        desctiprtion.Text = model.desctiprtion;

        if (model.intend_receipt_date.Split('#').Length > 1)
        {
            intend_receipt_date.Text = model.intend_receipt_date.Split('#')[0];
            Eintend_receipt_date.Text = model.intend_receipt_date.Split('#')[1];
        }
        else
            intend_receipt_date.Text = model.intend_receipt_date;

        quantity.Text = model.quantity.ToString("0.0000");
        labtotal.Text = (model.price * model.quantity).ToString("#,##0.####");

        //labdown.Text = model.upfile == "" ? "" : "<a target='_blank' href='../../" + model.upfile + "'><img src='/images/ico_04.gif' border='0' /></a>";
        //chkdown.Visible = model.upfile == "" ? false : true;


        string[] files = model.upfile.TrimEnd('#').Split('#');
        int i = 0;
        foreach (string filepath in files)
        {
            if (filepath.Trim() != "")
            {
                Label labDown = new Label();
                //labDown.Text = "<a href='../../" + filepath + "' target='_blank'>下载</a>";
                labDown.Text = "<a target='_blank' href='/Purchase/Requisition/UpfileDownload.aspx?OrderId=" + orderid.ToString() + "&Index=" + i.ToString() + "&Type=ContrastFile'>下载</a>&nbsp;";
                labDown.Text += "&nbsp;<input type='checkbox' name='chkUpFile' value=\"" + filepath + "\"/>删除&nbsp;";
                plcontrastDown.Controls.Add(labDown);
                i++;
            }
        }


        //只有添加采购物品中不包括目录物品，才可以变更供应商
        List<OrderInfo> orderList = OrderInfoManager.GetListByGeneralId(generalid);
        bool isContainerML = false;
        foreach (OrderInfo order in orderList)
        {
            if (order.productAttribute == (int)State.productAttribute.ml)
            {
                isContainerML = true;
            }
        }
        //if (isContainerML)
        //    btnChange.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        OrderInfo model = OrderInfoManager.GetModel(orderid);
        if (!checkPrice(model))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('采购物品单价不能大于修改前，请修改后保存！')", true);
            return;
        }

        model.general_id = generalid;
        model.Item_No = txtName.Text;
        if (model.productAttribute == (int)State.productAttribute.ml)
            txtName.Enabled = false;
        model.desctiprtion = desctiprtion.Text.Trim();
        model.intend_receipt_date = intend_receipt_date.Text.Trim();
        if (Eintend_receipt_date.Text.Trim() != "")
            model.intend_receipt_date += "#" + Eintend_receipt_date.Text.Trim();
        model.price = decimal.Parse(txtPrice.Text);
        model.uom = labUnit.Text;
        model.quantity = decimal.Parse(quantity.Text);
        model.producttype = int.Parse(hidProductTypeId.Value);
        model.total = model.price * model.quantity;
        model.supplierName = hidSupplierName.Value == "" ? labSupplierName.Text : hidSupplierName.Value;
        GeneralInfo generalModel = GeneralInfoManager.GetModel(model.general_id);
        if (generalModel.status == State.requisition_save || generalModel.status == State.requisition_return)
        {
            model.oldPrice = model.price;
            model.oldQuantity = model.quantity;
        }
        bool ispurchase = isPurchase();
        if (!string.IsNullOrEmpty(Request["chkUpFile"]))
        {
            //如果全部选择删除附件的话，非目录物品必须添加报价信息，条件：产品订单类型为非目录、没有新添加的附件、订单本身已没有附件、不是采购部人员
            string[] contrastFiles = Request["chkUpFile"].Split(',');
            string[] files = model.upfile.TrimEnd('#').Split('#');

            if (contrastFiles.Length == files.Length && model.productAttribute == (int)State.productAttribute.fml && this.hidNames.Value == "" && ispurchase == false)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('非目录物品必须添加报价信息！')", true);
                int i = 0;
                foreach (string filepath in files)
                {
                    if (filepath.Trim() != "")
                    {
                        Label labDown = new Label();
                        //labDown.Text = "<a href='../../" + filepath + "' target='_blank'>下载</a>";
                        labDown.Text = "<a target='_blank' href='/Purchase/Requisition/UpfileDownload.aspx?OrderId=" + orderid.ToString() + "&Index=" + i.ToString() + "&Type=ContrastFile'>下载</a>&nbsp;";
                        labDown.Text += "&nbsp;<input type='checkbox' name='chkUpFile' value=\"" + filepath + "\"/>删除&nbsp;";
                        plcontrastDown.Controls.Add(labDown);
                        i++;
                    }
                }
                return;
            }

            //删除附件，从订单的upfile字段中移除，从ordermsg表中，删除订单号下回复字段中，相同回复id
            foreach (string filePath in contrastFiles)
            {
                OrderMsg omModel = OrderMsgManager.GetModelByOrderId(orderid);
                if (omModel != null)
                {
                    string[] sArray = omModel.MsgReturnId.Split(',');
                    //sTmp 得出当前附件的回复id
                    string sTmp = filePath;
                    sTmp = filePath.Replace("upFile\\", "");
                    int iTmp = sTmp.IndexOf('_');
                    if (iTmp > 0)
                        sTmp = sTmp.Substring(0, iTmp);

                    string rtIds = "";
                    int tmp = 0;
                    //除去当前要删除的id外，重新记录回复id
                    foreach (string s in sArray)
                    {
                        if (sTmp == s && tmp == 0)
                            tmp = 1;//相同数据只记录一次不存
                        else
                            rtIds += s.ToString() + ",";
                    }
                    if (rtIds != string.Empty)
                        rtIds = rtIds.Substring(0, rtIds.Length - 1);
                    omModel.MsgReturnId = rtIds;
                    OrderMsgManager.Update(omModel);
                }

                model.upfile = model.upfile.Replace(filePath + "#", "");
                model.upfile = model.upfile.Replace(filePath, "");
            }
        }

        //非目录物品必须添加报价信息，条件：产品订单类型为非目录、没有新添加的附件、订单本身已没有附件、不是采购部人员

        if (model.productAttribute == (int)State.productAttribute.fml && model.upfile == "" && this.hidNames.Value == "" && ispurchase == false)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), Guid.NewGuid().ToString(), "alert('非目录物品必须添加报价信息！')", true);
            string[] files = model.upfile.TrimEnd('#').Split('#');
            int i = 0;
            foreach (string filepath in files)
            {
                if (filepath.Trim() != "")
                {
                    Label labDown = new Label();
                    //labDown.Text = "<a href='../../" + filepath + "' target='_blank'>下载</a>";
                    labDown.Text = "<a target='_blank' href='/Purchase/Requisition/UpfileDownload.aspx?OrderId=" + orderid.ToString() + "&Index=" + i.ToString() + "&Type=ContrastFile'>下载</a>&nbsp;";
                    labDown.Text += "&nbsp;<input type='checkbox' name='chkUpFile' value=\"" + filepath + "\"/>删除&nbsp;";
                    plcontrastDown.Controls.Add(labDown);
                    i++;
                }
            }
            return;
        }


        if (model.upfile.Trim() != "")
            model.upfile += "#";
        for (int i = 0; i < Request.Files.Count; i++)
        {
            if (Request.Files.Keys[i] == "filBJ1" && Request.Files[i].FileName != "")
            {
                System.Web.HttpPostedFile postFile = Request.Files[i];
                string fileName = "wuliao_" + generalid + "_" + DateTime.Now.Ticks.ToString();
                model.upfile += FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, postFile) + "#";
            }
            System.Threading.Thread.Sleep(100);
        }
        //添加寻价附件
        //if (this.txtPriceAtt.Text != "")
        //if (this.lbPriceAtt.Text != "")
        if (this.hidNames.Value != "")
        {
            //string[] sArray = this.txtPriceAtt.Text.Split(';');
            //string[] sArray = this.lbPriceAtt.Text.Split(';');
            string[] sArray = this.hidNames.Value.Split(';');
            for (int i = 0; i < sArray.Length; i++)
            {
                string mapPath = ESP.Purchase.Common.ServiceURL.UpFilePath;
                string fileName = sArray[i];
                //存储附件名，以回复id开头
                string savePath = mapPath + "upFile/" + "wuliao_" + fileName;
                string filePath = ESP.Configuration.ConfigurationManager.SafeAppSettings["supplyNewsFilePath"];
                filePath = filePath + fileName;

                File.Copy(filePath, savePath, true);
                model.upfile += "upFile/" + "wuliao_" + fileName + "#";
                System.Threading.Thread.Sleep(100);

            }
        }
        if (!string.IsNullOrEmpty(model.upfile))
        {
            model.upfile = model.upfile.TrimEnd('#');
            //Session["upAtts"] = model.upfile;
        }
        model.id = orderid;
        ESP.Purchase.Entity.SupplierInfo supplier = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(model.supplierId);
        if (supplier != null)
        {
            generalModel.supplier_name = model.supplierName;
            generalModel.supplier_linkman = supplier.contact_name;
            generalModel.supplier_phone = supplier.contact_tel;
            generalModel.Supplier_cellphone = supplier.contact_mobile;
            generalModel.supplier_fax = supplier.contact_fax;
            generalModel.supplier_email = supplier.contact_email;
            generalModel.fa_no = supplier.supplier_frameNO;
            generalModel.account_bank = supplier.account_bank;
            generalModel.account_name = supplier.account_name;
            generalModel.account_number = supplier.account_number;
            generalModel.supplier_address = supplier.contact_address;
            //generalModel.source = "协议供应商";

        }
        if (hidSupplierId.Value != "" && model.supplierId != int.Parse(hidSupplierId.Value))
        {
            model.supplierId = int.Parse(hidSupplierId.Value);
            if (palFX.Visible)
            {
                if (palGR.Visible)
                    generalModel.supplier_address = txtCardNum.Text.Trim();
                generalModel.account_bank = txtaccountBank.Text.Trim();
                generalModel.account_name = txtaccountName.Text.Trim();
                generalModel.account_number = txtaccountNum.Text.Trim();


                if (null != radOperationType.SelectedValue && "" != radOperationType.SelectedValue)
                {
                    generalModel.OperationType = int.Parse(radOperationType.SelectedValue);
                }
                else
                {
                    generalModel.OperationType = 0;
                }
                if (palGR.Visible)
                    generalModel.HaveInvoice = chkHaveInvoice.Checked;
                else
                    generalModel.HaveInvoice = false;
                generalModel.Requisitionflow = !string.IsNullOrEmpty(rblrequisitionflow.SelectedValue) ? int.Parse(rblrequisitionflow.SelectedValue) : 0;
                if (ddlsource.SelectedValue == "客户指定")
                {
                    if (null != filEmailFile.PostedFile && filEmailFile.PostedFile.FileName != "")
                    {
                        string fileName = string.IsNullOrEmpty(generalModel.CusAskEmailFile) ? ("cursask_" + generalModel.id + "_" + DateTime.Now.Ticks.ToString()) : generalModel.CusAskEmailFile.Split('\\')[1].ToString().Split('.')[0].ToString();
                        generalModel.CusAskEmailFile = FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, filEmailFile);
                    }
                }
                else
                {
                    generalModel.CusAskEmailFile = "";
                }
                //generalModel.source = ddlsource.SelectedValue;
            }

            OrderInfoManager.updateByTrans(model);
        }
        else if (hidSupplyId.Value != "")
        {
            model.supplierId = SaveSupplySupplier();
            OrderInfoManager.updateByTrans(model);
        }
        else
        {
            OrderInfoManager.Update(model, 0, "");
        }
        //记录更新ordermsg中当前订单的 回复id字段
        if (hidIds.Value != null && hidIds.Value != "")
        {
            string[] sIds1 = hidIds.Value.Split(',');
            OrderMsg omModel = OrderMsgManager.GetModelByOrderId(orderid);
            if (omModel != null)
            {
                string rtIds = omModel.MsgReturnId;
                //string[] rtIds1 = rtIds.Split(',');
                foreach (string sId in sIds1)
                {
                    //if(!rtIds1.Contains(sId))
                    //rtIds += sId + ",";
                    if (rtIds == "")
                        rtIds += sId;
                    else
                        rtIds += "," + sId;
                }
                //if (rtIds != string.Empty)
                //    rtIds = rtIds.Substring(0, rtIds.Length - 1);
                rtIds = rtIds.TrimStart(',').TrimEnd(',');
                omModel.MsgReturnId = rtIds;
                omModel.UpdateTime = DateTime.Now;
                omModel.UpdateUserId = CurrentUserID;
                OrderMsgManager.Update(omModel);
            }
        }
        generalModel.ValueLevel = 0;
        GeneralInfoManager.Update(generalModel);

        //记录操作日志
        ESP.Logging.Logger.Add(string.Format("{0}对T_OrderInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, model.id, "编辑采购物品"), "采购物品");
        Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID]);

    }

    /// <summary>
    /// 申请单提交后，修改采购物品的单价只能比原始小
    /// </summary>
    /// <param name="orderInfo"></param>
    /// <returns></returns>
    private bool checkPrice(OrderInfo orderInfo)
    {
        GeneralInfo model = GeneralInfoManager.GetModel(generalid);
        if (model.status != State.requisition_save && model.status != State.requisition_return)
        {
            if (decimal.Parse(txtPrice.Text) > (orderInfo.oldPrice == 0 ? orderInfo.price : orderInfo.oldPrice))
            {
                return false;
            }
        }
        return true;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID]);
    }

    protected void lnkShowFX_Click(object sender, EventArgs e)
    {
        if (hidSupplierId.Value != "")
        {
            ESP.Purchase.Entity.SupplierInfo s = ESP.Purchase.BusinessLogic.SupplierManager.GetModel(int.Parse(hidSupplierId.Value));
            if (s != null && s.supplier_type == (int)ESP.Purchase.Common.State.supplier_type.agreement)
            {
                palFX.Visible = false;
            }
            else
            {
                palFX.Visible = true;
                setSupplyInfo();

                txtaccountBank.Text = s.account_bank;
                txtaccountName.Text = s.account_name;
                txtaccountNum.Text = s.account_number;
            }
        }
        else
        {
            palFX.Visible = true;
            setSupplyInfo();
        }
        labSupplierName.Text = hidSupplierName.Value;
    }

    public int SaveSupplySupplier()
    {
        if (hidSupplyId.Value != "")
        {
            int supplyId = int.Parse(hidSupplyId.Value);
            GeneralInfo general = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);
            ESP.Purchase.Entity.SupplierInfo newSupplier = new ESP.Purchase.Entity.SupplierInfo();
            ESP.Supplier.Entity.SC_Supplier supply = new ESP.Supplier.BusinessLogic.SC_SupplierManager().GetModel(supplyId);

            general.supplier_name = newSupplier.supplier_name = supply.supplier_name;
            newSupplier.contact_address = supply.contact_address;
            if (palGR.Visible)
                general.supplier_address = txtCardNum.Text.Trim();
            else
                general.supplier_address = newSupplier.contact_address;
            List<ESP.Supplier.Entity.SC_LinkMan> linkers = new ESP.Supplier.BusinessLogic.SC_LinkManManager().GetListBySupplierId(supply.id);
            string linker = "";
            if (linkers != null && linkers.Count > 0)
            {
                linker = linkers[0].Name;
            }
            general.supplier_linkman = newSupplier.contact_name = linker;
            general.supplier_phone = newSupplier.contact_tel = getTel(supply.contact_Tel);
            general.Supplier_cellphone = newSupplier.contact_mobile = supply.contact_Mobile;
            general.supplier_fax = newSupplier.contact_fax = getTel(supply.contact_fax);
            general.supplier_email = newSupplier.contact_email = supply.contact_Email;
            general.account_bank = newSupplier.account_bank = txtaccountBank.Text.Trim();
            general.account_name = newSupplier.account_name = txtaccountName.Text.Trim();
            general.account_number = newSupplier.account_number = txtaccountNum.Text.Trim();
            newSupplier.supplier_source = "临时供应商";
            newSupplier.supplier_type = (int)State.supplier_type.noAgreement;
            newSupplier.supplier_status = State.supplierstatus_used;
            if (null != radOperationType.SelectedValue && "" != radOperationType.SelectedValue)
            {
                general.OperationType = int.Parse(radOperationType.SelectedValue);
            }
            else
            {
                general.OperationType = 0;
            }
            if (palGR.Visible)
                general.HaveInvoice = chkHaveInvoice.Checked;
            else
                general.HaveInvoice = false;
            general.Requisitionflow = !string.IsNullOrEmpty(rblrequisitionflow.SelectedValue) ? int.Parse(rblrequisitionflow.SelectedValue) : 0;
            general.source = ddlsource.SelectedValue;
            if (general.source == "客户指定")
            {
                if (null != filEmailFile.PostedFile && filEmailFile.PostedFile.FileName != "")
                {
                    string fileName = string.IsNullOrEmpty(general.CusAskEmailFile) ? ("cursask_" + general.id + "_" + DateTime.Now.Ticks.ToString()) : general.CusAskEmailFile.Split('\\')[1].ToString().Split('.')[0].ToString();
                    general.CusAskEmailFile = FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, filEmailFile);
                }
            }
            else
            {
                general.CusAskEmailFile = "";
            }
            hidSupplyId.Value = "";
            //创建新的供应商并和供应链关联
            return ESP.Purchase.BusinessLogic.SupplierManager.insertSupplierAndLinkSupply(general, newSupplier, supplyId);
        }
        return 0;
    }

    private string getTel(string tel)
    {
        tel = tel.Replace("_", "-").Replace("－", "-").Replace("—", "-");
        if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^(\d{7,8})|\d{11}"))
            return "--" + tel + "-";
        else if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^((\d{7,8})-\d{3,4})$"))
            return "--" + tel;
        else if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^((\d{2,3}-\d{7,8})-\d{3,4})$"))
            return "-" + tel;
        else if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^((\d{2,3}-\d{2,3}-\d{7,8})-\d{3,4})$"))
            return tel;
        else if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^((\d{2,3}-\d{2,3}-\d{7,8}))$"))
            return tel + "-";
        else if (System.Text.RegularExpressions.Regex.IsMatch(tel, @"^((\d{2,3}-\d{7,8}))$"))
            return "-" + tel + "-";
        return "---";
    }

    protected void radOperationType_SelectIndexChanged(object sender, EventArgs e)
    {
        palGR.Visible = radOperationType.SelectedValue == State.OperationTypePri.ToString();
    }

    private void setSupplyInfo()
    {
        radioBind();
        radOperationType.Items.Clear();
        radOperationType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePub], State.OperationTypePub.ToString()));
        radOperationType.Items.Add(new ListItem(State.OperationTypeShow[State.OperationTypePri], State.OperationTypePri.ToString()));
        radOperationType.SelectedIndex = 0;
    }

    /// <summary>
    /// 将数据源绑定到被调用的服务器控件及其所有子控件。
    /// </summary>
    public void radioBind()
    {
        rblrequisitionflow.Items.Clear();
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toO], State.requisitionflow_toO.ToString()));
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toR], State.requisitionflow_toR.ToString()));
        rblrequisitionflow.Items.Add(new ListItem(State.requisitionflow_state[State.requisitionflow_toC], State.requisitionflow_toC.ToString()));
        rblrequisitionflow.SelectedValue = State.requisitionflow_toR.ToString();
    }

    public bool isPurchase()
    {
        //检查当前用户部门是否为采购部
        if (CurrentUser.GetDepartmentIDs() != null)
        {
            int[] did = CurrentUser.GetDepartmentIDs();
            IList<ESP.Framework.Entity.DepartmentInfo> list = ESP.Framework.BusinessLogic.DepartmentManager.GetAll();
            list = list.Where(n => n.DepartmentName.Contains("采购") && !n.DepartmentName.Contains("作废")).ToList();
            foreach (int d in did)
            {
                foreach (ESP.Framework.Entity.DepartmentInfo model in list)
                {
                    if (model.DepartmentID == d)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
