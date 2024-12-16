using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
public partial class Dialogs_CustomerHistoryDlg : System.Web.UI.Page
{
    IList<ESP.Finance.Entity.CustomerTmpInfo> customerList = null;
    private string clientId = "ctl00_ContentPlaceHolder1_CustomerInfo_";
    private int deptid=0;
    string term = string.Empty;
    List<System.Data.SqlClient.SqlParameter> paramList = null;
    private string ListSate = "CustomerList";
    private string SequenceState = "Sequence";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getCustomerList();
            if (customerList != null && customerList.Count != 0)
            {
                bindCustomer(customerList[0]);
                ViewState[SequenceState] = 0;
                this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + customerList.Count.ToString() + "条记录";
            }
        }
    }

    private void getCustomerList()
    {
        paramList = new List<SqlParameter>();
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.DeptID]))
        {
            deptid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.DeptID]);
            term = " projectid in(select projectid from f_project where groupid=@groupid)";
            System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter("@groupid", System.Data.SqlDbType.Int, 4);
            p.SqlValue = deptid;  
            paramList.Add(p);
        }
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.CustomerID]))
        {
            int cid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.CustomerID]);
            term += " and customerid=@customerid";
            System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@customerid", System.Data.SqlDbType.Int, 4);
            p2.SqlValue = cid;
            paramList.Add(p2);
        }
        customerList = ESP.Finance.BusinessLogic.CustomerTmpManager.GetList(term, paramList);
        this.ViewState[ListSate] = customerList;
    }

    private void bindCustomer(ESP.Finance.Entity.CustomerTmpInfo c)
    { 
      if (c != null)
      {
          this.hidCustomerID.Value = c.CustomerTmpID.ToString();
            this.lblAddress1.Text = c.Address1;
            //this.lblAddress2.Text = c.Address2;
            this.lblArea.Text = c.AreaName;
            this.lblContact.Text = c.ContactName;
            this.lblContactFax.Text = c.ContactFax;
            this.lblContactMobile.Text = c.ContactMobile;
            this.lblContactPosition.Text = c.ContactPosition;
            this.lblEmail.Text = c.ContactEmail;
            this.lblIndustry.Text = c.IndustryName;
            this.lblInvoiceTitle.Text = c.InvoiceTitle;
            this.lblNameCN1.Text = c.NameCN1;
            //this.lblNameCN2.Text = c.NameCN2;
            //this.lblNameEN1.Text = c.NameEN1;
            //this.lblNameEN2.Text = c.NameEN2;
            //this.lblPostCode.Text = c.PostCode;
            //this.lblShortCN.Text = c.ShortCN;
            this.lblShortEN.Text = c.ShortEN;
            this.lblWebSite.Text = c.Website;
        }
    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        if (this.ViewState[ListSate] != null)
        {
            customerList = (IList<ESP.Finance.Entity.CustomerTmpInfo>)this.ViewState[ListSate];
        }
        else
        {
            getCustomerList();
        }
      
        if (customerList != null && customerList.Count != 0)
        {
            bindCustomer(customerList[0]);
            ViewState[SequenceState] = 0;

            this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + customerList.Count.ToString() + "条记录";
        }
    }
    protected void btnPret_Click(object sender, EventArgs e)
    {
        if (this.ViewState[ListSate] != null)
        {
            customerList = (IList<ESP.Finance.Entity.CustomerTmpInfo>)this.ViewState[ListSate];
        }
        else
        {
            getCustomerList();
        }
        if (customerList != null && customerList.Count != 0)
        {
            int seq = Convert.ToInt32(ViewState[SequenceState])-1;
            if(seq-1<0)
                seq=0;
            bindCustomer(customerList[seq]);
            ViewState[SequenceState] = seq;
            this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + customerList.Count.ToString() + "条记录";
       
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (this.ViewState[ListSate] != null)
        {
            customerList = (IList<ESP.Finance.Entity.CustomerTmpInfo>)this.ViewState[ListSate];
        }
        else
        {
            getCustomerList();
        }
        if (customerList != null && customerList.Count != 0)
        {
            int seq = Convert.ToInt32(ViewState[SequenceState]) +1;
            if (seq + 1 >=customerList.Count)
                seq = customerList.Count-1;
            bindCustomer(customerList[seq]);
            ViewState[SequenceState] = seq;
            this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + customerList.Count.ToString() + "条记录";
       
        }
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        if (this.ViewState[ListSate] != null)
        {
            customerList = (IList<ESP.Finance.Entity.CustomerTmpInfo>)this.ViewState[ListSate];
        }
        else
        {
            getCustomerList();
        }
        if (customerList != null && customerList.Count != 0)
        {
            int seq = customerList.Count - 1;
            bindCustomer(customerList[seq]);
            ViewState[SequenceState] = seq;
            this.lblRepresent.Text = "当前是第" + (Convert.ToInt32(ViewState[SequenceState]) + 1).ToString() + "条记录 / 共" + customerList.Count.ToString() + "条记录";

        }
    }

    private void add()
    {
        if (this.ViewState[ListSate] != null)
        {
            customerList = (IList<ESP.Finance.Entity.CustomerTmpInfo>)this.ViewState[ListSate];
        }
        else
        {
            getCustomerList();
        }
        if (customerList == null || customerList.Count == 0)
        {
            return;
        }
        ESP.Finance.Entity.CustomerTmpInfo customer = customerList[Convert.ToInt32(ViewState[SequenceState])];
        string strscript = "";
        strscript += "opener.document.getElementById('" + clientId + "hidCustomerID').value= '" + customer.CustomerID + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtShortEN').value= '" + customer.ShortEN + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtShortEN').disabled=true;";
        //strscript += "opener.document.getElementById('" + clientId + "txtShortCN').value= '" + customer.ShortCN + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtCN1').value= '" + customer.NameCN1 + "';";
        //strscript += "opener.document.getElementById('" + clientId + "txtCN2').value= '" + customer.NameCN2 + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtTitle').value= '" + customer.InvoiceTitle + "';";
        //strscript += "opener.document.getElementById('" + clientId + "txtEN1').value= '" + customer.NameEN1 + "';";
        //strscript += "opener.document.getElementById('" + clientId + "txtEN2').value= '" + customer.NameEN2 + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtAddress1').value= '" + customer.Address1 + "';";
        //strscript += "opener.document.getElementById('" + clientId + "txtAddress2').value= '" + customer.Address2 + "';";
        //strscript += "opener.document.getElementById('" + clientId + "txtPostCode').value= '" + customer.PostCode + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtWebSite').value= '" + customer.Website + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidAreaID').value= '" + customer.AreaID + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidAreaCode').value= '" + customer.AreaCode + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtArea').value= '" + customer.AreaName + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidCustomerCode').value= '" + customer.CustomerCode + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidIndustryID').value= '" + customer.IndustryID + "';";
        strscript += "opener.document.getElementById('" + clientId + "hidIndustryCode').value= '" + customer.IndustryCode + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtIndustry').value= '" + customer.IndustryName + "';";
        //contact infomation
        strscript += "opener.document.getElementById('" + clientId + "txtContact').value= '" + customer.ContactName + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactPosition').value= '" + customer.ContactPosition + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactMobile').value= '" + customer.ContactMobile + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactFax').value= '" + customer.ContactFax + "';";
        strscript += "opener.document.getElementById('" + clientId + "txtContactEmail').value= '" + customer.ContactEmail + "';";
        strscript += "window.close(); ";
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), strscript, true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       // #error "错误信息"
        /*
         * 
行 170:            getCustomerList();
行 171:        }
行 172:        ESP.Finance.Entity.CustomerTmpInfo customer = customerList[Convert.ToInt32(ViewState[SequenceState])];
行 173:        string strscript = "";
行 174:        strscript += "opener.document.getElementById('" + clientId + "hidCustomerID').value= '" + customer.CustomerID + "';";
         * 当选择某些客户的时候，没有历史，点击确定就会出现上面的错误！！！
*/
            add();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string strscript = "window.close(); ";
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), strscript, true);
    }
}
