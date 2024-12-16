using System;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using ESP.Compatible;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;

public partial class Purchase_FinancialUserList : ESP.Web.UI.PageBase
{
    private string clientId = string.Empty;
    private string searchType = string.Empty;
    //private string uniqueMemberId = "ctl00$ContentPlaceHolder1$SupporterInfo$";

    protected ArrayList SelectedItems
    {
        get
        {
            return (ViewState["mySelectedItems"] != null) ? (ArrayList)ViewState["mySelectedItems"] : null;
        }
        set
        {
            ViewState["mySelectedItems"] = value;
        }
    }

    protected void gv_DataBinding(object sender, EventArgs e)
    {
        //在每一次重新绑定之前，需要调用CollectSelected方法从当前页收集选中项的情况
        CollectSelected();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;

            Label lblPhone = (Label)e.Row.FindControl("lblPhone");
            if (lblPhone != null)
                lblPhone.Text = StringHelper.FormatPhoneLastChar(lblPhone.Text);
        }
        //这里的处理是为了回显之前选中的情况
        if (e.Row.RowIndex > -1 && this.SelectedItems != null)
        {
            DataRowView row = e.Row.DataItem as DataRowView;
            CheckBox cb = e.Row.FindControl("chkEmp") as CheckBox;
            if (this.SelectedItems.Contains(row["sysuserid"].ToString()))
                cb.Checked = true;
            else
                cb.Checked = false;
        }
    }
    /**/
    /// <summary>
    /// 从当前页收集选中项的情况
    /// </summary>
    protected void CollectSelected()
    {
        if (this.SelectedItems == null)
            SelectedItems = new ArrayList();
        else
            SelectedItems.Clear();

        for (int i = 0; i < this.gv.Rows.Count; i++)
        {
            string id = this.gv.Rows[i].Cells[1].Text;
            CheckBox cb = this.gv.Rows[i].FindControl("chkEmp") as CheckBox;
            if (SelectedItems.Contains(id) && !cb.Checked)
                SelectedItems.Remove(id);
            if (!SelectedItems.Contains(id) && cb.Checked)
                SelectedItems.Add(id);
        }
        this.SelectedItems = SelectedItems;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_FinancialUserList));
        #endregion

        if (!IsPostBack)
        {
            this.ddlDept.DataSource = ESP.Finance.Configuration.ConfigurationManager.GetFinanceDept();
            this.ddlDept.DataTextField = "DepartmentName";
            this.ddlDept.DataValueField = "DepartmentID";
            this.ddlDept.DataBind();
            gvDataBind();
        }
        listBind();
    }

    private void listBind()
    {
        btnClean.Visible = false;
        if (!string.IsNullOrEmpty(txtName.Text) )
        {
            btnClean.Visible = true;
        }

    }

    protected void add()
    {
        CollectSelected();
        if (this.SelectedItems.Count == 0)
        {
            return;
        }

        string empSysUserID = this.SelectedItems[0].ToString();

        Employee emp = new Employee(int.Parse(empSysUserID));

        string username = emp.Name;
        string sysuserid = empSysUserID;
        string phone = emp.Telephone;
        string userid = emp.ID;
        string usercode = emp.ITCode;


        string groupid = emp.GetDepartmentIDs()[0].ToString();
        string groupname = emp.GetDepartmentNames()[0].ToString();
        string script = string.Empty;

        Response.Write("<script>window.opener.setnextAuditor('"+username+"','"+sysuserid+"');</script>");
        Response.Write("<script>window.close();</script>");
    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        DataSet ds = ESP.Compatible.Employee.GetDataSetUserByKey_Department("", Convert.ToInt32(ddlDept.SelectedValue));
        if (null != ds && ds.Tables.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        gvDataBind();
    }

    //绑定
    private void gvDataBind()
    {
        string value = txtName.Text.Trim();
        DataSet ds = ESP.Compatible.Employee.GetDataSetUserByKey_Department(value,Convert.ToInt32(ddlDept.SelectedValue));
        if (null != ds && ds.Tables.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
        }
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> getalist(int parentId)
    {
        List<List<string>> list = new List<List<string>>();
        try
        {

            list = ESP.Compatible.DepartmentManager.GetListForAJAX(parentId);
        }
        catch (Exception e)
        {
            e.ToString();
        }

        List<string> c = new List<string>();
        c.Add("-1");
        c.Add("请选择...");
        list.Insert(0, c);
        return list;
    }
    protected void btnSubMit_Click(object sender, EventArgs e)
    {
        add();
    }
}
