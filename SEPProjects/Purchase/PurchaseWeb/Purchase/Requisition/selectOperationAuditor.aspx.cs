﻿using System;
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
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;
using ESP.Compatible;
using WorkFlowImpl;
using ESP.Purchase.Common;
using ESP.Framework.BusinessLogic;

public partial class Purchase_Requisition_selectOperationAuditor : ESP.Web.UI.PageBase
{
    private string clientId = "ctl00_ContentPlaceHolder1_";


    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_Requisition_selectOperationAuditor));
        #endregion

        divModuleTree.InnerHtml = ESP.Compatible.DepartmentManager.GetHtml(Server.MapPath("DepartmentModify.xslt"));

        if (!IsPostBack)
        {

            int currentDeptId = CurrentUser.GetDepartmentIDs()[0];
            ESP.Compatible.Department currentD = ESP.Compatible.DepartmentManager.GetDepartmentByPK(currentDeptId);
            if (currentD.Level == 1)
            {
                hidtype.Value = currentD.UniqID.ToString();
            }
            else if (currentD.Level == 2)
            {
                hidtype1.Value = currentD.UniqID.ToString();
                hidtype.Value = currentD.Parent.UniqID.ToString();
            }
            else if (currentD.Level == 3)
            {
                hidtype2.Value = currentD.UniqID.ToString();
                hidtype1.Value = currentD.Parent.UniqID.ToString();
                hidtype.Value = currentD.Parent.Parent.UniqID.ToString();
            }

            if (!string.IsNullOrEmpty(Request["deptid"]))
            {
                int depid = Convert.ToInt32(Request["deptid"]);
                ESP.Compatible.Department d = ESP.Compatible.DepartmentManager.GetDepartmentByPK(depid);
                if (d.Level == 1)
                {
                    hidtype.Value = d.UniqID.ToString();
                }
                else if (d.Level == 2)
                {
                    hidtype1.Value = d.UniqID.ToString();
                    hidtype.Value = d.Parent.UniqID.ToString();
                }
                else if (d.Level == 3)
                {
                    hidtype2.Value = d.UniqID.ToString();
                    hidtype1.Value = d.Parent.UniqID.ToString();
                    hidtype.Value = d.Parent.Parent.UniqID.ToString();
                }
            }

            GeneralInfo generalInfoModel = GeneralInfoManager.GetModel(int.Parse(Request[RequestName.GeneralID]));

            if (string.IsNullOrEmpty(Request["changedTrId"]))
            {
                if (Request["type"] == "YS")
                {
                    radUserType.SelectedValue = Request["type"];
                    radUserType.Enabled = false;
                }
            }
            else
            {
                foreach (ListItem li in radUserType.Items)
                {
                    li.Selected = false;
                }
                radUserType.Enabled = false;
                radUserType_SelectedIndexChanged(new object(), new EventArgs());
            }

            DepartmentDataBind();
            setHidIds();
        }
        listBind();
    }

    private void setHidIds()
    {
        gvDataBind();
    }

    private string getSelectedValue()
    {
        string selectedValue = "";
        if (Request["type"] == "YS")
        {
            selectedValue = "YS";
        }
        else if (Request["type"] == "ZJSP" || Request["type"] == "ZJZH" || Request["type"] == "ZJFJ")
        {
            if (radUserType.SelectedValue != "YS")
                selectedValue = "ZJ" + radUserType.SelectedValue;
            else
                selectedValue = "YS";
        }
        else if (Request["type"] == "ZJLSP" || Request["type"] == "ZJLZH" || Request["type"] == "ZJLFJ")
        {
            if (radUserType.SelectedValue != "YS")
                selectedValue = "ZJL" + radUserType.SelectedValue;
            else
                selectedValue = "YS";
        }
        else if (Request["type"] == "FASP")
        {
            if (radUserType.SelectedValue != "YS")
                selectedValue = "FA" + radUserType.SelectedValue;
            else
                selectedValue = "YS";
        }
        if (!string.IsNullOrEmpty(Request["changedTrId"]))
        {
            selectedValue = Request["type"];
        }
        return selectedValue;
    }

    private void listBind()
    {
        btnClean.Visible = false;

        if (!string.IsNullOrEmpty(Request["clientId"]))
        {
            clientId = "ctl00_ContentPlaceHolder1_genericInfo_";
        }
    }

    protected void add(string sysUserId)
    {
        Employee y = new Employee(int.Parse(sysUserId));
        string username = y.Name;
        string sysuserid = y.SysID;
        string phone = y.Telephone;
        string job = y.PositionDescription;

        string group = y.GetDepartmentNames().Count == 0 ? "" : y.GetDepartmentNames()[0].ToString();
        string[] phonenum = phone.Split('-');

        if (string.IsNullOrEmpty(Request["changedTrId"]))
            Response.Write("<script>window.opener.addRow1('" + getSelectedValue() + "','" + sysuserid + "','" + username + "','" + job + "','add','','');</script>");
        else
            Response.Write("<script>window.opener.addRow1('" + getSelectedValue() + "','" + sysuserid + "','" + username + "','" + job + "','change','" + Request["changedTrId"] + "','" + Request["oldId"] + "');</script>");
        Response.Write(@"<script>window.close();</script>");
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string sysUserID = gv.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            add(sysUserID);
        }
    }

    protected void radUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["changedTrId"]))
        {
            if ((Request["type"] == "ZJSP" || Request["type"] == "ZJZH" || Request["type"] == "ZJFJ"))
                hidIds.Value = OperationAuditManageManager.GetDirectorIds();
            else if (Request["type"] == "ZJLSP" || Request["type"] == "ZJLZH" || Request["type"] == "ZJLFJ")
                hidIds.Value = OperationAuditManageManager.GetManagerIds();
            else if (Request["type"] == "CEO")
                hidIds.Value = OperationAuditManageManager.GetCEOIds();
            else
                hidIds.Value = "";
        }
        if (radUserType.SelectedValue == "YS")
        {
            hidIds.Value = "";
        }
        gvDataBind();
    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        List<ESP.Compatible.Employee> ds = ESP.Compatible.Employee.GetDataSetByName(txtName.Text);

        if (null != ds && ds.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
        }
        if (!string.IsNullOrEmpty(Request["clientId"]))
        {
            clientId = "ctl00_ContentPlaceHolder1_genericInfo_";
        }
        hidtype.Value = "";
        hidtype1.Value = "";
        hidtype2.Value = "";
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        gvDataBind();
    }

    //绑定
    private void gvDataBind()
    {
        string value = txtName.Text.Trim();
        int[] depids = null;
        List<Department> dlist;
        //Department dep;
        string typevalue = null;

        if (hidtype2.Value != "" && hidtype2.Value != "-1")
        {
            typevalue = hidtype2.Value;
        }
        else if (hidtype1.Value != "" && hidtype1.Value != "-1")
        {
            typevalue = hidtype1.Value;
        }
        else if (hidtype.Value != "" && hidtype.Value != "-1")
        {
            typevalue = hidtype.Value;
        }

        if (typevalue != null && typevalue.Length != 0)
        {
            int selectedDep = int.Parse(typevalue);
            dlist = GetLeafChildDepartments(selectedDep);
            if (dlist != null && dlist.Count > 0)
            {
                depids = new int[dlist.Count];
                for (int i = 0; i < dlist.Count; i++)
                {
                    depids[i] = dlist[i].UniqID;
                }
            }
            else
            {
                depids = new int[] { selectedDep };
            }
        }
        string terms = "";
        if (hidIds.Value != "")
        {
            terms += " and U.UserId in (" + hidIds.Value.TrimEnd(',') + ")";
        }
        terms += " and U.UserId <> " + Request["curU"];
        DataSet ds = ESP.Compatible.Employee.GetDataSetUserByKey(value, depids, terms);
        gv.DataSource = ds;
        gv.DataBind();
    }

    private void DepartmentDataBind()
    {
        object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
        ddltype.DataSource = dt;
        ddltype.DataTextField = "NodeName";
        ddltype.DataValueField = "Uniqid";
        ddltype.DataBind();
        ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
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
}