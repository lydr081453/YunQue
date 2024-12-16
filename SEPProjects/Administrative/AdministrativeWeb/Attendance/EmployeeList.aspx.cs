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

public partial class Purchase_Requisition_EmployeeList : ESP.Web.UI.PageBase
{
    private string clientId = string.Empty;
    private string searchType = string.Empty;
    private int DeptID = 0;
    private int _reqUserID = 0;
    private string deptName = string.Empty;
    private string uniqueMemberId = "ctl00$ContentPlaceHolder1$SupporterInfo$";

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
        //#region AjaxProRegister
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(Purchase_Requisition_EmployeeList));
        //#endregion
        //divModuleTree.InnerHtml = DepartmentManager.GetHtml(Server.MapPath("DepartmentModify.xslt"));

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["UserSysID"]))
            {
                _reqUserID = Convert.ToInt32(Request["UserSysID"]);
            }
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.DeptID]))
            {
                DeptID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.DeptID]);
                Department d = DepartmentManager.GetDepartmentByPK(DeptID);
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
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.DeptName]))
            {
                deptName = Request[ESP.Finance.Utility.RequestName.DeptName];
                this.hidGroupName.Value = deptName;
            }
            if (_reqUserID != 0)
            {
                //DataSet dsU = Employee.GetDataSetBySysUserID1(UserID.ToString());
                //if (dsU != null && dsU.Tables.Count > 0)
                //{
                //    DataTable dt = dsU.Tables[0];
                //    if (dt.Rows.Count > 0)
                //    {
                IList<Department> deparmentsOfUserID = Employee.GetDepartments(_reqUserID);
                int[] depts = new int[deparmentsOfUserID.Count];
                for (int i = 0; i < deparmentsOfUserID.Count; i++)
                {
                    Department d = deparmentsOfUserID[i];
                    depts[i] = d.UniqID;
                }
                if (depts != null && depts.Length > 0)
                {
                    Department d = DepartmentManager.GetDepartmentByPK(depts[0]);
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
                //    }
                //}
                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.UserName]))
                {
                    txtName.Text = Request[ESP.Finance.Utility.RequestName.UserName];
                }
            }
            else if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.UserName]))
            {
                txtName.Text = Request[ESP.Finance.Utility.RequestName.UserName];
            }
            else
            {
                if (DeptID == 0)
                    DeptID = CurrentUser.GetDepartmentIDs()[0];
            }
        }
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SearchType]))
        {
            searchType = Request[ESP.Finance.Utility.RequestName.SearchType];
        }
        if (!IsPostBack)
        {
            DepartmentDataBind();
            gvDataBind();
        }
        listBind();
    }

    private void listBind()
    {
        btnClean.Visible = false;
        if (!string.IsNullOrEmpty(txtName.Text) || (hidtype.Value != "" && hidtype.Value != "-1") || (hidtype1.Value != "" && hidtype1.Value != "-1") || (hidtype2.Value != "" && hidtype2.Value != "-1"))
        {
            btnClean.Visible = true;
        }

        if (!string.IsNullOrEmpty(Request["clientId"]))
        {
            clientId = "ctl00_ContentPlaceHolder1_genericInfo_";
        }
    }

    protected void add()
    {
        CollectSelected();
        if (this.SelectedItems.Count == 0)
        {
            return;
        }
        //DataTable dt = Employee.GetDataSetBySysUserID1(this.SelectedItems[0].ToString()).Tables[0];
        //string username = dt.Rows[0]["UserName"].ToString().Trim();
        //string sysuserid = dt.Rows[0]["SysUserID"].ToString().Trim();
        //string phone = dt.Rows[0]["Telephone"].ToString().Trim();
        //string userid = dt.Rows[0]["UserID"].ToString().Trim();
        //string usercode = dt.Rows[0]["UserCode"].ToString().Trim();
        string sysuserid = this.SelectedItems[0].ToString();
        Employee emp = new Employee(int.Parse(sysuserid));

        string username = emp.Name;
        //string sysuserid = emp.SysID;
        string phone = emp.Telephone;
        string userid = emp.ID;
        string usercode = emp.ITCode;

        int[] deptIDs = emp.GetDepartmentIDs();
        string groupid = string.Empty;
        if (deptIDs != null && deptIDs.Length > 0)
            groupid = deptIDs[0].ToString();
        IList<string> deptNames = emp.GetDepartmentNames();
        string groupname = string.Empty;
        if (deptNames != null && deptNames.Count > 0)
            groupname = deptNames[0].ToString();
        string script = string.Empty;
        switch (searchType)
        {
            case "Applicant":
                clientId = "ctl00_ContentPlaceHolder1_Prepareinfo_";
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidApplicantID').value= '" + sysuserid + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "txtApplicant').value= '" + username + "'</script>");
                //Response.Write("<script>opener.document.getElementById('" + clientId + "hidGroupID').value= '" + groupid + "'</script>");
                //Response.Write("<script>opener.document.getElementById('" + clientId + "txtGroup').value= '" + groupname + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidApplicantUserID').value= '" + userid + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidApplicantUserCode').value= '" + usercode + "'</script>");
                Response.Write(@"<script>window.close();</script>");//txtMembers
                break;
            case "SupporterApplicant":
                clientId = "ctl00_ContentPlaceHolder1_SupporterInfo_";
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidLeaderID').value= '" + sysuserid + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "lblLeaderEmployeeName').innerHTML= '" + username + "'</script>");
                Response.Write(@"<script>window.close();</script>");
                break;
            case "ApplicantUpdate":
                clientId = "ctl00_ContentPlaceHolder1_PrepareUpdate_";
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidApplicantID').value= '" + sysuserid + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "txtApplicant').value= '" + username + "'</script>");
                //Response.Write("<script>opener.document.getElementById('" + clientId + "hidGroupID').value= '" + groupid + "'</script>");
                //Response.Write("<script>opener.document.getElementById('" + clientId + "txtGroup').value= '" + groupname + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidApplicantUserID').value= '" + userid + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidApplicantUserCode').value= '" + usercode + "'</script>");
                Response.Write(@"<script>window.close();</script>");//txtMembers
                break;
            case "NextConfirm":
                clientId = "ctl00_ContentPlaceHolder1_";
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidApplicantID').value= '" + sysuserid + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "txtApplicant').value= '" + username + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidApplicantUserID').value= '" + userid + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidApplicantUserCode').value= '" + usercode + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidGroupID').value= '" + groupid + "'</script>");
                Response.Write(@"<script>window.close();</script>");
                break;
            case "Member":
                foreach (object tmp in this.SelectedItems)
                {
                    Employee y = new Employee(int.Parse(tmp.ToString()));
                    ESP.Finance.Entity.ProjectMemberInfo m = new ESP.Finance.Entity.ProjectMemberInfo();
                    m.MemberUserID = int.Parse(y.SysID);
                    m.MemberUserName = y.ITCode;
                    m.MemberEmployeeName = y.Name;
                    m.MemberCode = y.ID;
                    m.GroupID = y.GetDepartmentIDs()[0];
                    m.GroupName = y.GetDepartmentNames()[0].ToString();
                    m.RoleName = y.PositionDescription;
                    m.MemberEmail = y.EMail;
                    m.MemberPhone = y.Telephone;
                    m.ProjectId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    m.ProjectCode = Request[ESP.Finance.Utility.RequestName.ProjectCode];
                    ESP.Finance.BusinessLogic.ProjectMemberManager.Add(m);
                }

                if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.BackUrl]))
                {
                    if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.Operate]))
                    {
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "opener.location='" + Request[ESP.Finance.Utility.RequestName.BackUrl] + "?" + RequestName.ProjectID + "=" + Request[ESP.Finance.Utility.RequestName.ProjectID] + "&" + RequestName.Operate + "=" + Request[ESP.Finance.Utility.RequestName.Operate] + "';window.close();", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "opener.location='" + Request[ESP.Finance.Utility.RequestName.BackUrl] + "?" + RequestName.ProjectID + "=" + Request[ESP.Finance.Utility.RequestName.ProjectID] + "';window.close();", true);
                }
                else
                {
                    string uniqueId = "ctl00$ContentPlaceHolder1$ProjectMember$";
                    script = @"
                    var uniqueId = '" + uniqueId + @"';
                    opener.__doPostBack(uniqueId + 'btnMember', '');
                    window.close(); ";
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
                }
                break;
            case "SupporterMember":
                foreach (object tmp in this.SelectedItems)
                {
                    Employee y = new Employee(int.Parse(tmp.ToString()));
                    ESP.Finance.Entity.SupportMemberInfo m = new ESP.Finance.Entity.SupportMemberInfo();
                    m.MemberUserID = int.Parse(y.SysID);
                    m.MemberUserName = y.ITCode;
                    m.MemberEmployeeName = y.Name;
                    m.MemberCode = y.ID;
                    m.GroupID = y.GetDepartmentIDs()[0];
                    m.GroupName = y.GetDepartmentNames()[0].ToString();
                    m.RoleName = y.PositionDescription;
                    m.MemberEmail = y.EMail;
                    m.MemberPhone = y.Telephone;
                    m.SupportID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
                    m.ProjectID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                    ProjectInfo project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
                    if (project != null)
                        m.ProjectCode = project.ProjectCode;
                    ESP.Finance.BusinessLogic.SupportMemberManager.Add(m);
                }
                script = @"
var uniqueId = '" + uniqueMemberId + @"';
opener.__doPostBack(uniqueId + 'btnMember', '');
window.close(); ";
                ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
                break;

            case "BusinessCard":
                clientId = "ctl00_ContentPlaceHolder1_";
                Response.Write("<script>opener.document.getElementById('" + clientId + "lblUserCode').innerHTML= '" + emp.ID + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "lblUserName').innerHTML= '" + username + "'</script>");
                Response.Write("<script>opener.document.getElementById('" + clientId + "hidUserId').value= '" + emp.SysID + "'</script>");
                Response.Write(@"<script>window.close();</script>");
                break;
        }
    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        IList list = ESP.Compatible.Employee.GetDataSetByName(txtName.Text);


        if (null != list && list.Count > 0)
        {
            //if (searchType == "SupporterMember")
            //{
            //    IList<SupportMemberInfo> list = ESP.Finance.BusinessLogic.SupportMemberManager.GetList("SupportID=" + Request[ESP.Finance.Utility.RequestName.SupportID]);
            //    if (list != null && list.Count > 0)
            //    {
            //        foreach (DataRow dr in ds.Tables[0].Rows)
            //        {
            //            if()
            //        }
            //    }
            //}
            gv.DataSource = list;
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
        else
        {
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
       
        string selectedusers =GetUserSelected(Convert.ToInt32(Request[RequestName.ProjectID]));
        DataSet ds;
        if(!string.IsNullOrEmpty(selectedusers))
            ds= ESP.Compatible.Employee.GetDataSetUserByKey(value, depids," and e.userid not in("+selectedusers+")");
        else
            ds = ESP.Compatible.Employee.GetDataSetUserByKey(value, depids);

        gv.DataSource = ds;
        gv.DataBind();

        #region "old version"
        //string depids = string.Empty;
        //List<Department> dlist;
        //Department dep;
        //if (hidtype.Value != "" && hidtype.Value != "-1")
        //{
        //    //depids = int.Parse(hidtype.Value.Trim());
        //    dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(int.Parse(hidtype.Value.Trim()));
        //    dlist = GetDepartmentListByParentID(dep.UniqID);
        //    if (null != dlist && dlist.Count > 0)
        //    {
        //        depids = " and F.depid in (";
        //        foreach (Department d in dlist)
        //        {
        //            depids += d.UniqID.ToString() + ",";
        //            List<Department> dlistl2 = GetDepartmentListByParentID(d.UniqID);
        //            if (null != dlistl2 && dlistl2.Count > 0)
        //            {
        //                foreach (Department dl2 in dlistl2)
        //                {
        //                    depids += dl2.UniqID.ToString() + ",";
        //                }
        //            }
        //        }
        //        depids = depids.Substring(0, depids.Length - 1);
        //        depids += " ) ";
        //    }
        //    else
        //    {
        //        depids = " and F.depid = " + hidtype.Value.Trim();
        //    }
        //}
        //if (hidtype1.Value != "" && hidtype1.Value != "-1")
        //{
        //    //depids = int.Parse(hidtype1.Value.Trim());
        //    dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(int.Parse(hidtype1.Value.Trim()));
        //    dlist = GetDepartmentListByParentID(dep.UniqID);
        //    if (null != dlist && dlist.Count > 0)
        //    {
        //        depids = " and F.depid in (";
        //        foreach (Department d in dlist)
        //        {
        //            depids += d.UniqID.ToString() + ",";
        //        }
        //        depids = depids.Substring(0, depids.Length - 1);
        //        depids += " ) ";
        //    }
        //    else
        //    {
        //        depids = " and F.depid = " + hidtype1.Value.Trim();
        //    }
        //}
        //if (hidtype2.Value != "" && hidtype2.Value != "-1")
        //{
        //    //depids = int.Parse(hidtype2.Value.Trim());
        //    dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(int.Parse(hidtype2.Value.Trim()));
        //    dlist = GetDepartmentListByParentID(dep.UniqID);
        //    if (null != dlist && dlist.Count > 0)
        //    {
        //        depids = " and F.depid in (";
        //        foreach (Department d in dlist)
        //        {
        //            depids += d.UniqID.ToString() + ",";
        //        }
        //        depids = depids.Substring(0, depids.Length - 1);
        //        depids += " ) ";
        //    }
        //    else
        //    {
        //        depids = " and F.depid = " + hidtype2.Value.Trim();
        //    }
        //}
        //DataSet ds = ESP.Compatible.Employee.GetDataSetUserByKey(value, depids);
        //if (null != ds && ds.Tables.Count > 0)
        //{
        //    gv.DataSource = ds;
        //    gv.DataBind();
        //    if (ds.Tables[0].Rows.Count < 1)
        //    {
        //        btnSubMit.Visible = false;
        //        btnClose.Visible = false;
        //        btnSubMit1.Visible = false;
        //    }
        //    else
        //    {
        //        btnSubMit.Visible = true;
        //        btnClose.Visible = true;
        //        btnSubMit1.Visible = true;
        //    }
        //}
        #endregion
    }

    private void DepartmentDataBind()
    {
        object dt = ESP.Compatible.DepartmentManager.GetByParent(0);
        ddltype.DataSource = dt;
        ddltype.DataTextField = "NodeName";
        ddltype.DataValueField = "UniqID";
        ddltype.DataBind();
        ddltype.Items.Insert(0, new ListItem("请选择...", "-1"));
    }

    private string GetUserSelected(int projectID)
    {
        string UserSelected = string.Empty;
        IList<ESP.Finance.Entity.ProjectMemberInfo> projectMember = ESP.Finance.BusinessLogic.ProjectMemberManager.GetListByProject(projectID, null, null);
        if (projectMember != null && projectMember.Count > 0)
        {
            foreach (ESP.Finance.Entity.ProjectMemberInfo m in projectMember)
            {
                UserSelected += m.MemberUserID.ToString() + ",";
            }
        }

        IList<ESP.Finance.Entity.SupporterInfo> supporterList = ESP.Finance.BusinessLogic.SupporterManager.GetListByProject(projectID, null, null);
        if (supporterList != null && supporterList.Count > 0)
        {
            foreach (ESP.Finance.Entity.SupporterInfo sup in supporterList)
            {
                IList<ESP.Finance.Entity.SupportMemberInfo> supMember = ESP.Finance.BusinessLogic.SupportMemberManager.GetList("SupportID=" + sup.SupportID.ToString());
                if (supMember != null && supMember.Count > 0)
                {
                    foreach (ESP.Finance.Entity.SupportMemberInfo mem in supMember)
                    {
                        UserSelected += mem.MemberUserID.Value.ToString() + ",";
                    }
                }
            }
        }

        return UserSelected.TrimEnd(',');
    }
    //[AjaxPro.AjaxMethod]
    public static List<List<string>> getalist(int parentId)
    {
        List<List<string>> list = new List<List<string>>();
        //Department deps = new Department();

        //Department dep = ESP.Compatible.DepartmentManager.GetDepartmentByPK(parentId);
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
