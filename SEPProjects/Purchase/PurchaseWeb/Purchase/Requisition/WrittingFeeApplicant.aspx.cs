using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using System.Web;

public partial class Purchase_Requisition_WrittingFeeApplicant : ESP.Web.UI.PageBase
{
    private string projectcode = string.Empty;
    private int projectid = 0;
    private int generalid = 0;
    private int orderid = 0;
    MediaOrderInfo qreporter;
    OrderInfo order;
    GeneralInfo generalinfo;
    private void bindData(int mediaOrderID)
    {
        if (!string.IsNullOrEmpty(Request["orderid"]) || !string.IsNullOrEmpty(hidOrderID.Value))
        {
            hidOrderID.Value = string.IsNullOrEmpty(Request["orderid"]) == false ? Request["orderid"] : hidOrderID.Value;
            orderid = Convert.ToInt32(hidOrderID.Value);
            order = OrderInfoManager.GetModel(orderid);
        }
        qreporter = MediaOrderManager.GetModel(mediaOrderID);
        txtDescription.Text = order.desctiprtion;

        hidReporterID.Value = qreporter.ReporterID.ToString();
        hidMediaID.Value = qreporter.MediaID.ToString();
        txtMediaSelect.Text = qreporter.MediaName;
        lblMedia.Text = qreporter.MediaName;
        txtReporterSelect.Text = qreporter.ReporterName;
        lblReporter.Text = qreporter.ReporterName;
        txtBankAcountName.Text = qreporter.ReceiverName;
        hidMediaOrderID.Value = qreporter.MeidaOrderID.ToString();
        txtCity.Text = qreporter.CityName;
        txtIC.Text = qreporter.CardNumber;
        txtBank.Text = qreporter.BankName;
        txtAccount.Text = qreporter.BankAccountName;
        txtPhone.Text = qreporter.Tel;
        txtReporterPhone.Text = qreporter.Mobile;
        ddlPaymentType.SelectedIndex = Convert.ToInt32(qreporter.PayType);
        if (Convert.ToInt32(qreporter.PayType) == 1)//刊后
        {
            palH.Style["display"] = "block";
            palQ.Style["display"] = "none";
            txtAmount.Text = qreporter.TotalAmount.ToString();
        }
        else//刊前
        {
            palH.Style["display"] = "none";
            palQ.Style["display"] = "block";
            txtAmountQ.Text = qreporter.TotalAmount.ToString();
            txtPreWordLength.Text = qreporter.WordLength == null ? "0" : qreporter.WordLength.Value.ToString();
        }
        if (qreporter.ReleaseDate == null)
            txtReleaseDate.Text = "";
        else
            txtReleaseDate.Text = qreporter.ReleaseDate.Value.ToString("yyy-MM-dd");
        hidEditPrice.Value = qreporter.TotalAmount.ToString();//判断是否修改了金额
        radioList.SelectedIndex = qreporter.IsDelegate == true ? 0 : 1;
        rdImageList.SelectedIndex = qreporter.IsImage == true ? 0 : 1;
        txtImgSize.Text = qreporter.ImageSize;
        txtTitle.Text = qreporter.Subject;
        if (qreporter.WordLength == 0)
        {
            txtWordLength.Text = "";
            txtPreWordLength.Text = "";
        }
        else
        {
            txtWordLength.Text = qreporter.WordLength.ToString();
            txtPreWordLength.Text = qreporter.WordLength.ToString();
        }
        txtHref.Text = qreporter.WrittingURL;
        txtDateBegin.Text = qreporter.BeginDate == null ? "" : qreporter.BeginDate.Value.ToString("yyyy-MM-dd");
        txtDateEnd.Text = qreporter.EndDate == null ? "" : qreporter.EndDate.Value.ToString("yyyy-MM-dd");
        if (!string.IsNullOrEmpty(qreporter.Attachment))
        {
            lblFileName.Text = "<a target='_blank' href='\\upFile\\" + qreporter.Attachment + "'><img src='/images/ico_04.gif' border='0' /></a>";
        }
        chkdownSow.Visible = qreporter.Attachment == null ? false : true;
    }

    private void bindGrid()
    {
        if (!string.IsNullOrEmpty(hidOrderID.Value))
        {
            orderid = Convert.ToInt32(hidOrderID.Value);
            System.Data.DataTable orderlist = MediaOrderManager.GetMediaOrderList(" orderid=" + orderid.ToString()).Tables[0];
            rptParent.DataSource = orderlist;
            rptParent.DataBind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ddlPaymentType.Attributes.Add("onChange", "selectPayType(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        if (!IsPostBack)
        {
            hidEditPrice.Value = "0";
        }
        chkdownSow.Visible = false;
        if (ddlPaymentType.SelectedIndex == 1)//刊后支付
        {
            palH.Style["display"] = "block";
            palQ.Style["display"] = "none";
        }
        else if (ddlPaymentType.SelectedIndex == 2)
        {

            palQ.Style["display"] = "block";
            palH.Style["display"] = "none";

        }
        if (!string.IsNullOrEmpty(Request[RequestName.ProjectID]))
        {
            projectid = Convert.ToInt32(Request[RequestName.ProjectID]);
        }

        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = Convert.ToInt32(Request[RequestName.GeneralID]);
            if (generalid > 0)
            {
                generalinfo = GeneralInfoManager.GetModel(generalid);
                projectcode = generalinfo.project_code;
            }
        }

        if (!string.IsNullOrEmpty(Request[RequestName.ProjectCode]))
        {
            projectcode = Request[RequestName.ProjectCode];
        }
        if (!string.IsNullOrEmpty(Request["orderid"]) || !string.IsNullOrEmpty(hidOrderID.Value))
        {
            hidOrderID.Value = string.IsNullOrEmpty(Request["orderid"]) == false ? Request["orderid"] : hidOrderID.Value;
            OrderInfo order = new OrderInfo();
            order = OrderInfoManager.GetModel(Convert.ToInt32(hidOrderID.Value));
            txtDescription.Text = order.desctiprtion;
        }
        bindGrid();

    }
    private void clearContent()
    {
        txtAmount.Text = string.Empty;
        txtAmountQ.Text = string.Empty;
        txtPreWordLength.Text = string.Empty;
        txtDateBegin.Text = string.Empty;
        txtDateEnd.Text = string.Empty;
        txtHref.Text = string.Empty;
        txtTitle.Text = string.Empty;
        txtWordLength.Text = string.Empty;
        txtPreWordLength.Text = string.Empty;
        lblFileName.Text = string.Empty;
        radioList.SelectedIndex = 1;
        rdImageList.SelectedIndex = 1;
        txtImgSize.Text = string.Empty;
        this.hidMediaOrderID.Value = string.Empty;
        this.txtReleaseDate.Text = string.Empty;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImportData();
    }

    private void ImportData()
    {
        string filename = "";
        if (fupReportersImport.PostedFile != null && fupReportersImport.PostedFile.ContentLength > 0)
        {
            filename = SaveFile();
            ImportData(filename);
        }
    }

    private void ImportData(string filename)
    {
        try
        {
            if (!string.IsNullOrEmpty(filename))
            {
                int gid = Convert.ToInt32(Request[RequestName.GeneralID]);
                TypeInfo type = TypeManager.GetModelByOperationFlow(State.typeoperationflow_Media);

                if (!string.IsNullOrEmpty(Request["orderid"]) || !string.IsNullOrEmpty(hidOrderID.Value))
                {
                    hidOrderID.Value = string.IsNullOrEmpty(Request["orderid"]) == false ? Request["orderid"] : hidOrderID.Value;
                    if (!string.IsNullOrEmpty(hidOrderID.Value))
                    {
                        orderid = Convert.ToInt32(hidOrderID.Value);
                        order = OrderInfoManager.GetModel(orderid);
                    }
                    else
                        order = new OrderInfo();
                }
                else
                {
                    order = new OrderInfo();

                }
                order.Item_No = type.typename;
                order.producttype = 0;
                order.producttypename = "媒介中心";
                order.quantity = 1;
                order.uom = "份";
                order.desctiprtion = txtDescription.Text.Trim();
                order.upfile = "";
                order.BillID = 0;
                order.BillType = (int)BillType.WritingFeeBill;
                order.producttype = type.typeid;
                order.producttypename = "媒介报销项目";
                order.general_id = gid;

                Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
                Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
                Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets[1];
                int s = sheet.UsedRange.Count;
                object[,] obs = (object[,])sheet.UsedRange.Value2;

                List<ESP.Purchase.Entity.MediaOrderInfo> list = new List<ESP.Purchase.Entity.MediaOrderInfo>();

                int startRowIndex = 2;//起始行索引
                int rowIndex = 0; //顺序行索引  
                // bool bo = false;//判断是否有错误的code 
                try
                {
                    while ((startRowIndex + rowIndex) <= obs.GetLength(0) && obs[startRowIndex + rowIndex, 1] != null)
                    {
                        ESP.Media.Entity.ReportersInfo reporter = ESP.Media.BusinessLogic.ReportersManager.GetModel(Convert.ToInt32(obs[startRowIndex + rowIndex, 19].ToString()));

                        if (reporter != null)
                        {
                            try
                            {
                                #region 保存
                                string err = string.Empty;
                                string oname = string.Empty;
                                order.Item_No = type.typename;
                                order.producttype = 0;
                                order.producttypename = "媒介中心";
                                order.quantity = 1;
                                order.uom = "份";
                                order.desctiprtion = txtDescription.Text.Trim();
                                order.upfile = "";
                                order.BillID = 0;
                                order.BillType = (int)BillType.WritingFeeBill;
                                order.producttype = type.typeid;
                                order.producttypename = "媒介报销项目";

                                //order.general_id = gid;
                                if (!string.IsNullOrEmpty(hidMediaOrderID.Value))
                                    qreporter = MediaOrderManager.GetModel(Convert.ToInt32(hidMediaOrderID.Value));
                                else
                                    qreporter = new MediaOrderInfo();

                                if (!string.IsNullOrEmpty(obs[startRowIndex + rowIndex, 19].ToString()))
                                    qreporter.ReporterID = Convert.ToInt32(obs[startRowIndex + rowIndex, 19].ToString());
                                else
                                    qreporter.ReporterID = 0;
                                if (!string.IsNullOrEmpty(obs[startRowIndex + rowIndex, 20].ToString()))
                                    qreporter.MediaID = Convert.ToInt32(obs[startRowIndex + rowIndex, 20].ToString());
                                else
                                    qreporter.MediaID = 0;

                                //if (!string.IsNullOrEmpty(hidMediaOrderID.Value))
                                //    qreporter.MeidaOrderID = Convert.ToInt32(hidMediaOrderID.Value);

                                if (obs[startRowIndex + rowIndex, 4] == null || obs[startRowIndex + rowIndex, 4].ToString().Trim() == "")
                                {
                                    list.Clear();
                                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('上传文件的稿费字数为必填项!');", true);
                                    break;
                                }

                                qreporter.ReporterName = reporter.Reportername;
                                qreporter.MediaName = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Convert.ToInt32(qreporter.MediaID)).Mediacname;
                                //
                                qreporter.CityName = obs[startRowIndex + rowIndex, 9] == null ? "" : obs[startRowIndex + rowIndex, 9].ToString().Trim();
                                qreporter.CardNumber = obs[startRowIndex + rowIndex, 12] == null ? "" : obs[startRowIndex + rowIndex, 12].ToString().Trim();
                                qreporter.BankName = obs[startRowIndex + rowIndex, 10] == null ? "" : obs[startRowIndex + rowIndex, 10].ToString().Trim();
                                //
                                qreporter.ReceiverName = obs[startRowIndex + rowIndex, 8] == null ? "" : obs[startRowIndex + rowIndex, 8].ToString().Trim();//收款人
                                qreporter.BankAccountName = obs[startRowIndex + rowIndex, 11] == null ? "" : obs[startRowIndex + rowIndex, 11].ToString().Trim();//帐号
                                //
                                qreporter.Tel = obs[startRowIndex + rowIndex, 13] == null ? "" : obs[startRowIndex + rowIndex, 13].ToString().Trim();   //收款人联系方式
                                qreporter.Mobile = obs[startRowIndex + rowIndex, 7] == null ? "" : obs[startRowIndex + rowIndex, 7].ToString().Trim();  //联系人联系方式

                                qreporter.PayType = "1";//刊后
                                if (obs[startRowIndex + rowIndex, 16] != null && obs[startRowIndex + rowIndex, 17] != null && obs[startRowIndex + rowIndex, 16].ToString().Trim() != string.Empty && obs[startRowIndex + rowIndex, 17].ToString().Trim() != string.Empty)
                                {
                                    qreporter.PayType = "2";//刊前
                                }
                                qreporter.TotalAmount = obs[startRowIndex + rowIndex, 5] == null ? 0 : Convert.ToDecimal(obs[startRowIndex + rowIndex, 5].ToString().Trim());//字数
                                qreporter.IsDelegate = obs[startRowIndex + rowIndex, 18] == null ? false : Convert.ToBoolean(Convert.ToInt32(obs[startRowIndex + rowIndex, 18]));  //是否代理
                                qreporter.IsImage = false;
                                qreporter.ImageSize = string.Empty;
                                if (obs[startRowIndex + rowIndex, 14] != null && obs[startRowIndex + rowIndex, 14].ToString().Trim() != string.Empty)
                                {
                                    qreporter.IsImage = true;
                                    qreporter.ImageSize = obs[startRowIndex + rowIndex, 14].ToString().Trim();
                                }
                                qreporter.IsTax = false;
                                qreporter.Subject = (obs[startRowIndex + rowIndex, 2] == null || obs[startRowIndex + rowIndex, 2].ToString().Trim() == "") ? "" : obs[startRowIndex + rowIndex, 2].ToString().Trim();
                                qreporter.ReleaseDate = (obs[startRowIndex + rowIndex, 3] == null || obs[startRowIndex + rowIndex, 3].ToString().Trim() == "") ? DateTime.Now : Convert.ToDateTime(obs[startRowIndex + rowIndex, 3].ToString().Trim());
                                qreporter.WordLength = (obs[startRowIndex + rowIndex, 4] == null || obs[startRowIndex + rowIndex, 4].ToString().Trim() == "") ? 0 : Convert.ToInt32(obs[startRowIndex + rowIndex, 4].ToString().Trim());
                                qreporter.WrittingURL = (obs[startRowIndex + rowIndex, 15] == null || obs[startRowIndex + rowIndex, 15].ToString().Trim() == "") ? "" : obs[startRowIndex + rowIndex, 15].ToString().Trim();
                                qreporter.BeginDate = (obs[startRowIndex + rowIndex, 16] == null || obs[startRowIndex + rowIndex, 16].ToString().Trim() == "") ? DateTime.Now : Convert.ToDateTime(obs[startRowIndex + rowIndex, 16].ToString().Trim());
                                qreporter.EndDate = (obs[startRowIndex + rowIndex, 17] == null || obs[startRowIndex + rowIndex, 17].ToString().Trim() == "") ? DateTime.Now : Convert.ToDateTime(obs[startRowIndex + rowIndex, 17].ToString().Trim());
                                hidOrderID.Value = string.IsNullOrEmpty(Request["orderid"]) == false ? Request["orderid"] : hidOrderID.Value;
                                if (!string.IsNullOrEmpty(hidOrderID.Value))
                                    qreporter.OrderID = Convert.ToInt32(hidOrderID.Value);
                                #endregion
                                list.Add(qreporter);
                                //SaveReporterPrivateInfo(qreporter);
                                rowIndex++;
                            }
                            catch
                            {
                                rowIndex++;
                            }
                        }
                        else
                        {
                            rowIndex++;
                        }

                    }
                }
                catch (Exception ex)
                {
                    ESP.Logging.Logger.Add(ex.ToString());
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ImportAccount.aspx';alert('导入失败！" + ex.Message + "');", true);
                }
                finally
                {
                    workbook.Close(null, null, null);
                    app.Workbooks.Close();
                    app.Application.Quit();
                    app.Quit();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                    sheet = null;
                    workbook = null;
                    app = null;
                }

                GeneralInfo general = GeneralInfoManager.GetModel(gid);
                try
                {
                    if (list != null && list.Count > 0)
                        this.hidOrderID.Value = ESP.Purchase.BusinessLogic.MediaOrderManager.AddReporterIntoOrder(general, order, list, CurrentUserID, CurrentUserName).ToString();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.Message + "');", true);
                    pnlReporter.Style["display"] = "block";
                    return;
                }
                clearContent();
                bindGrid();
                pnlReporter.Style["display"] = "block";
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
        }
    }

    //添加记者的付款信息
    private void SaveReporterPrivateInfo(MediaOrderInfo orderInfo)
    {
        ESP.MediaLinq.Entity.media_PrivateInfo info = new ESP.MediaLinq.Entity.media_PrivateInfo();
        info.ReporterID = orderInfo.ReporterID;
        info.bankname = orderInfo.BankName;
        info.bankcardcode = orderInfo.BankAccountName;
        info.Mobile = orderInfo.Tel;
        info.City = orderInfo.CityName;
        info.IDCardNumber = orderInfo.CardNumber;

        int newID = ESP.MediaLinq.BusinessLogic.PrivateManager.Add(info);
        if (newID > 0)
        {
            ProjectRelationReporterPrivateInfo prpInfo = new ProjectRelationReporterPrivateInfo();
            prpInfo.ReporterPrivateID = newID;
            prpInfo.ProjectID = projectid;
            ProjectRelationReporterPrivateManager.Add(prpInfo);
        }
        //Remark			
        //Del				
        //PayType			
        //bankcardname = orderInfo.
        //bankacountname	
        //writingfee		
        //referral			
        //haveInvoice		
        //paystatus			
        //uploadstarttime	
        //uploadendtime		
        //paymentmode		
        //PrivateRemark		
        //cooperatecircs	
        //Tel				
        //Mobile			

    }

    private string SaveFile()
    {
        HttpPostedFile myFile = fupReportersImport.PostedFile;
        try
        {
            if (myFile.FileName != null && myFile.ContentLength > 0)//&& theFile.ContentLength <= Config.PHOTO_CONTENT_LENGTH)
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                string fn = ESP.Purchase.Common.ServiceURL.ReporterPath + DateTime.Now.ToString("yyyyMMdd") + random.Next(100, 999).ToString() + fupReportersImport.FileName;
                myFile.SaveAs(fn);

                return fn;
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            ESP.Logging.Logger.Add(ex.ToString());
            return "";
        }

    }

    private void SaveForImport(int gid)
    {

    }




    private void saveOrderInfo(int gid)
    {
        string err = string.Empty;
        string oname = string.Empty;
        TypeInfo type = TypeManager.GetModelByOperationFlow(State.typeoperationflow_Media);

        if (!string.IsNullOrEmpty(Request["orderid"]) || !string.IsNullOrEmpty(hidOrderID.Value))
        {
            hidOrderID.Value = string.IsNullOrEmpty(Request["orderid"]) == false ? Request["orderid"] : hidOrderID.Value;
            if (!string.IsNullOrEmpty(hidOrderID.Value))
            {
                orderid = Convert.ToInt32(hidOrderID.Value);
                order = OrderInfoManager.GetModel(orderid);
            }
            else
                order = new OrderInfo();
        }
        else
        {
            order = new OrderInfo();

        }
        order.Item_No = type.typename;
        order.producttype = 0;
        order.producttypename = "媒介中心";
        order.quantity = 1;
        order.uom = "份";
        order.desctiprtion = txtDescription.Text.Trim();
        order.upfile = "";
        order.BillID = 0;
        order.BillType = (int)BillType.WritingFeeBill;
        order.producttype = type.typeid;
        order.producttypename = "媒介报销项目";
        order.general_id = gid;
        if (!string.IsNullOrEmpty(hidMediaOrderID.Value))
            qreporter = MediaOrderManager.GetModel(Convert.ToInt32(hidMediaOrderID.Value));
        else
            qreporter = new MediaOrderInfo();

        if (!string.IsNullOrEmpty(hidReporterID.Value))
            qreporter.ReporterID = Convert.ToInt32(hidReporterID.Value);
        else
            qreporter.ReporterID = 0;
        if (!string.IsNullOrEmpty(hidMediaID.Value))
            qreporter.MediaID = Convert.ToInt32(hidMediaID.Value);
        else
            qreporter.MediaID = 0;
        if (!string.IsNullOrEmpty(hidMediaOrderID.Value))
            qreporter.MeidaOrderID = Convert.ToInt32(hidMediaOrderID.Value);
        qreporter.ReporterName = lblReporter.Text;
        qreporter.MediaName = lblMedia.Text;
        qreporter.CityName = txtCity.Text.Trim();
        qreporter.CardNumber = txtIC.Text.Trim();
        qreporter.BankName = txtBank.Text.Trim();
        qreporter.ReceiverName = txtBankAcountName.Text;//收款人
        qreporter.BankAccountName = txtAccount.Text;//帐号
        qreporter.Tel = txtPhone.Text;
        qreporter.Mobile = txtReporterPhone.Text;
        qreporter.PayType = ddlPaymentType.SelectedIndex.ToString();
        if (ddlPaymentType.SelectedIndex == 1)
            qreporter.TotalAmount = Convert.ToDecimal(txtAmount.Text);
        else
            qreporter.TotalAmount = Convert.ToDecimal(txtAmountQ.Text);
        qreporter.IsDelegate = radioList.SelectedIndex == 0 ? true : false;
        qreporter.IsImage = rdImageList.SelectedIndex == 0 ? true : false;
        if (!string.IsNullOrEmpty(this.txtReleaseDate.Text.Trim()))
            qreporter.ReleaseDate = Convert.ToDateTime(this.txtReleaseDate.Text.Trim());
        qreporter.IsTax = radioTax.SelectedIndex == 0 ? true : false;
        qreporter.Subject = txtTitle.Text;
        qreporter.ImageSize = txtImgSize.Text;
        if (txtWordLength.Text.Trim() == "")
        {
            if (txtPreWordLength.Text.Trim() == "")
            {
                qreporter.WordLength = 0;
            }
            else
                qreporter.WordLength = Convert.ToInt32(txtPreWordLength.Text);
        }
        else
            qreporter.WordLength = Convert.ToInt32(txtWordLength.Text);

        qreporter.WrittingURL = txtHref.Text.Trim();
        if (!string.IsNullOrEmpty(txtDateBegin.Text.Trim()))
            qreporter.BeginDate = Convert.ToDateTime(txtDateBegin.Text.Trim());
        if (!string.IsNullOrEmpty(txtDateEnd.Text.Trim()))
            qreporter.EndDate = Convert.ToDateTime(txtDateEnd.Text.Trim());
        if (chkdownSow.Checked)
            qreporter.Attachment = "";

        if (filSow.PostedFile != null && filSow.PostedFile.FileName != "")
        {
            string selectedpath = filSow.FileName;
            string extention=string.Empty;
            if (selectedpath.LastIndexOf(".") != -1)
            {
                extention = selectedpath.Substring(selectedpath.LastIndexOf("."));
            }
            else
                extention = string.Empty;
           string filename ="WrittingFee_" + Guid.NewGuid().ToString() + extention;
           qreporter.Attachment = FileHelper.upFile(filename, ESP.Purchase.Common.ServiceURL.UpFilePath, filSow);
        }
        hidOrderID.Value = string.IsNullOrEmpty(Request["orderid"]) == false ? Request["orderid"] : hidOrderID.Value;
        if (!string.IsNullOrEmpty(hidOrderID.Value))
            qreporter.OrderID = Convert.ToInt32(hidOrderID.Value);
    }

    protected void rptParent_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView GVChild = (GridView)e.Item.FindControl("GVChild");
            HyperLink btnViewChild = (HyperLink)e.Item.FindControl("btnViewChild");
            HyperLink btnCancelChild = (HyperLink)e.Item.FindControl("btnCancelChild");
            if (btnViewChild != null)
                btnViewChild.Style["display"] = "none";
            if (btnCancelChild != null)
                btnCancelChild.Style["display"] = "block";
            System.Data.DataRowView drv = (System.Data.DataRowView)e.Item.DataItem;
            if (GVChild != null)
            {
                string term = string.Empty;
                if (!string.IsNullOrEmpty(drv["receivername"].ToString()))
                    term = " orderid=" + orderid.ToString() + " and receivername='" + drv["receivername"].ToString() + "' and cardnumber='" + drv["cardnumber"].ToString() + "'";
                else
                    term = " orderid=" + orderid.ToString() + " and receivername='" + drv["reportername"].ToString() + "' and cardnumber='" + drv["cardnumber"].ToString() + "'";
                System.Data.DataTable orderlist = MediaOrderManager.GetList(term).Tables[0];
                GVChild.DataSource = orderlist;
                GVChild.DataBind();
                btnViewChild.Style["cursor"] = "hand";
                btnCancelChild.Style["cursor"] = "hand";
                btnViewChild.Attributes.Add("onClick", "DisplayDetail('" + GVChild.ClientID + "','block','" + btnCancelChild.ClientID + "','" + btnViewChild.ClientID + "');");
                btnCancelChild.Attributes.Add("onClick", "DisplayDetail('" + GVChild.ClientID + "','none','" + btnViewChild.ClientID + "','" + btnCancelChild.ClientID + "');");

            }
        }

    }

    protected void btnHid_onclick(object sender, EventArgs e)
    {
        GeneralInfo general = GeneralInfoManager.GetModel(generalid);
        saveOrderInfo(generalid);

        try
        {
            this.hidOrderID.Value = ESP.Purchase.BusinessLogic.MediaOrderManager.AddReporterIntoOrder(general, order, qreporter, CurrentUserID, CurrentUserName).ToString();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.Message + "');", true);
            pnlReporter.Style["display"] = "block";
            return;
        }
        clearContent();
        bindGrid();
        pnlReporter.Style["display"] = "block";
    }

    protected void btnHidReturn_onclick(object sender, EventArgs e)
    {
        GeneralInfo general = GeneralInfoManager.GetModel(generalid);
        saveOrderInfo(generalid);
        try
        {
            this.hidOrderID.Value = ESP.Purchase.BusinessLogic.MediaOrderManager.AddReporterIntoOrder(general, order, qreporter, CurrentUserID, CurrentUserName).ToString();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.Message + "');", true);
            return;
        }
        Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID]);
    }

    protected void btnAdd_onclick(object sender, EventArgs e)
    {
        GeneralInfo general = GeneralInfoManager.GetModel(generalid);
        saveOrderInfo(generalid);
        try
        {
            this.hidOrderID.Value = ESP.Purchase.BusinessLogic.MediaOrderManager.AddReporterIntoOrder(general, order, qreporter, CurrentUserID, CurrentUserName).ToString();
            //SaveReporterPrivateInfo(qreporter);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.Message + "');", true);
            pnlReporter.Style["display"] = "block";
            return;
        }
        clearContent();
        bindGrid();
        pnlReporter.Style["display"] = "block";
    }

    protected void btnSave_onclick(object sender, EventArgs e)
    {
        GeneralInfo general = GeneralInfoManager.GetModel(generalid);
        saveOrderInfo(generalid);
        try
        {
            this.hidOrderID.Value = ESP.Purchase.BusinessLogic.MediaOrderManager.AddReporterIntoOrder(general, order, qreporter, CurrentUserID, CurrentUserName).ToString();

            //SaveReporterPrivateInfo(qreporter);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + ex.Message + "');", true);
            return;
        }
        Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID]);
    }

    protected void btnReturnNoSave_onclick(object sender, EventArgs e)
    {
        Response.Redirect(Request["pageUrl"] + "?" + RequestName.GeneralID + "=" + Request[RequestName.GeneralID]);
    }

    private decimal getMediaAvgPrice(int mediaID)
    {
        decimal avgPrice = 0;
        List<MediaOrderInfo> medialist = MediaOrderManager.GetModelList("mediaid=" + mediaID.ToString());
        if (medialist == null || medialist.Count == 0)
            return 0;
        else
        {
            int totalWordLength = 0;
            decimal totalAmount = 0;
            foreach (MediaOrderInfo item in medialist)
            {
                totalWordLength += item.WordLength == null ? 0 : item.WordLength.Value;
                totalAmount += item.TotalAmount == null ? 0 : item.TotalAmount.Value;
            }
            if (totalWordLength == 0)
                return 0;
            else
                avgPrice = totalAmount / totalWordLength;
        }
        return avgPrice;
    }

    protected void GVChild_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblNo = (Label)e.Row.FindControl("lblNo");
            lblNo.Text = (e.Row.RowIndex + 1).ToString();
            Label lblAvgPrice = (Label)e.Row.FindControl("lblAvgPrice");
            lblAvgPrice.Text = getMediaAvgPrice(Convert.ToInt32(e.Row.Cells[2].Text)).ToString("#,##0.00");

            Label lblUnitPrice = (Label)e.Row.FindControl("lblUnitPrice");
            decimal totalamount = Convert.ToDecimal(e.Row.Cells[10].Text);
            decimal wordLength = Convert.ToDecimal(e.Row.Cells[4].Text);
            if (wordLength == 0)
                lblUnitPrice.Text = "0.00";
            else
                lblUnitPrice.Text = (totalamount / wordLength).ToString("#,##0.00");
        }
    }

    protected void GVChild_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int mediaorderid = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "Del")
        {
            ESP.Purchase.BusinessLogic.MediaOrderManager.DeleteReporterFromOrder(Convert.ToInt32(Request[RequestName.GeneralID]), Convert.ToInt32(Request["orderid"]), mediaorderid, CurrentUserID, CurrentUserName);
            bindGrid();
        }
        if (e.CommandName == "Edit")
        {
            pnlReporter.Style["display"] = "block";
            bindData(mediaorderid);
        }
    }

    protected void GVChild_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
    }
}