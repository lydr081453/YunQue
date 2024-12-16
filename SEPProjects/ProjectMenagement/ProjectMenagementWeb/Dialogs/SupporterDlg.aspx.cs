using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Compatible;
using ESP.Finance.Utility;

public partial class Dialogs_SupporterDlg : ESP.Web.UI.PageBase
{
    private int projectid = 0;
    ESP.Finance.Entity.ProjectInfo projectinfo;
    private int supportid = 0;
    ESP.Finance.Entity.SupporterInfo supporterModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Dialogs_SupporterDlg));

        if (!IsPostBack)
        {
            DepartmentDataBind();
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                if (!string.IsNullOrEmpty(projectinfo.ProjectCode) && !string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]))
                {
                    this.ddltype.Enabled = false;
                    this.ddltype1.Enabled = false;
                    this.ddltype2.Enabled = false;
                    this.btnSearch.Enabled = false;
                    this.txtResponser.Enabled = false;
                }
            }

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ResponserID]))
            {
                this.hidResponserID.Value = Request[ESP.Finance.Utility.RequestName.ResponserID];
                this.txtResponser.Text = Request[ESP.Finance.Utility.RequestName.ResponserName];
            }


            #region "begin load existed suporter infomation by supporter ID"

            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]) && Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]) != 0)
            {
                supportid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]);
                supporterModel = ESP.Finance.BusinessLogic.SupporterManager.GetModel(supportid);

                this.txtAmount.Text = supporterModel.BudgetAllocation == null ? "" : supporterModel.BudgetAllocation.Value.ToString("#,##0.00");
                this.txtResponser.Text = supporterModel.LeaderEmployeeName;
                this.hidResponserID.Value = supporterModel.LeaderUserID == null ? "" : supporterModel.LeaderUserID.Value.ToString();
                this.hidGroupID.Value = supporterModel.GroupID == null ? "" : supporterModel.GroupID.Value.ToString();
                this.hidGroupName.Value = supporterModel.GroupName == null ? "" : supporterModel.GroupName;
                switch (supporterModel.IncomeType)
                {
                    case "Fee":
                        this.ddlAmountType.SelectedIndex = 1;
                        break;
                    case "Cost":
                        this.ddlAmountType.SelectedIndex = 2;
                        break;
                    case "Fee & Cost":
                        this.ddlAmountType.SelectedIndex = 3;
                        break;
                    default:
                        this.ddlAmountType.SelectedIndex = 0;
                        break;
                }
                int level = 0;
                ESP.Compatible.Department dp2;
                ESP.Compatible.Department dp1;
                dp2 = ESP.Compatible.DepartmentManager.GetDepartmentByPK(supporterModel.GroupID == null ? 0 : supporterModel.GroupID.Value);
                level = dp2.Level;
                switch (level)
                {
                    case 1:
                        this.hidtype.Value = dp2.UniqID.ToString();
                        this.hidGroupName.Value = dp2.DepartmentName;
                        this.ddltype.SelectedValue = this.hidtype.Value;
                        break;
                    case 2:
                        this.hidtype1.Value = dp2.UniqID.ToString();
                        this.hidtype.Value = dp2.Parent.UniqID.ToString();
                        this.ddltype.SelectedValue = this.hidtype.Value;
                        this.ddltype1.SelectedValue = this.hidtype1.Value;
                        this.hidtype2.Value = "";
                        this.hidGroupName.Value = dp2.DepartmentName;
                        break;
                    case 3:
                        this.hidtype2.Value = dp2.UniqID.ToString();
                        this.hidGroupName.Value = dp2.DepartmentName;
                        dp1 = ESP.Compatible.DepartmentManager.GetDepartmentByPK(dp2.Parent.UniqID);
                        this.hidtype1.Value = dp1.UniqID.ToString();
                        this.hidtype.Value = dp1.Parent.UniqID.ToString();
                        this.ddltype.SelectedValue = this.hidtype.Value;
                        this.ddltype1.SelectedValue = this.hidtype1.Value;
                        this.ddltype2.SelectedValue = this.hidtype2.Value;
                        break;
                }
            }
            #endregion

            gvDataBind();
        }
    }

    //绑定
    private void gvDataBind()
    {
        string value = txtResponser.Text.Trim();
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

        DataSet ds = ESP.Compatible.Employee.GetDataSetUserByKey(value, depids);

        gv.DataSource = ds;
        gv.DataBind();

    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (divEmp.Style["display"] == "none")
            divEmp.Style["display"] = "block";
        gvDataBind();
    }

    private void add(string sysid)
    {
        Employee y = new Employee(int.Parse(sysid));
        this.hidGroupName.Value = y.GetDepartmentNames().Count == 0 ? "" : y.GetDepartmentNames()[0].ToString();
        int[] groupids = y.GetDepartmentIDs();
        if (groupids != null && groupids.Length > 0)
        {
            this.hidGroupID.Value = groupids[0].ToString();
        }
        else
        {
            this.hidGroupID.Value = "-1";
        }
        this.hidResponserID.Value = y.SysID;
        this.txtResponser.Text = y.Name;
        this.hidResponserCode.Value = y.ID;
        this.hidResponserUserName.Value = y.ITCode;
        this.divEmp.Style["display"] = "none";
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string sysUserID = gv.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            add(sysUserID);

        }
    }

    protected void btnNewSupporter_Click(object sender, EventArgs e)
    {
        if (projectinfo == null)
        {
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
        }
        if (projectinfo.GroupID.Value == Convert.ToInt32(this.hidGroupID.Value))
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "alert('支持方与主申请方不能同属一组。');", true);
            return;
        }
        ESP.Finance.Entity.SupporterInfo Supporter = null;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]) && Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]) != 0)
        {
            Supporter = ESP.Finance.BusinessLogic.SupporterManager.GetModel(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]));
        }
        else
        {
            Supporter = new ESP.Finance.Entity.SupporterInfo();
        }
                
        Supporter.ProjectID = projectinfo.ProjectId;
        Supporter.ProjectCode = projectinfo.ProjectCode;
        Supporter.GroupID = Convert.ToInt32(this.hidGroupID.Value);
        Supporter.GroupName = this.hidGroupName.Value;
        Supporter.ServiceType = projectinfo.BusinessTypeName;
        Supporter.ServiceDescription = projectinfo.BusinessDescription;
        Supporter.BudgetAllocation = Convert.ToDecimal(this.txtAmount.Text);
        Supporter.LeaderUserID = Convert.ToInt32(this.hidResponserID.Value);
        Supporter.LeaderCode = this.hidResponserCode.Value;
        Supporter.LeaderUserName = this.hidResponserUserName.Value;
        Supporter.LeaderEmployeeName = this.txtResponser.Text;
        Supporter.IncomeType = this.ddlAmountType.SelectedItem.Text.ToString();
        Supporter.Status = (int)Status.Saved;
        Supporter.TaxType = chkTax.Checked;
        if (ESP.Finance.BusinessLogic.SupporterManager.GetSupporterCount(projectinfo.ProjectId, Supporter.SupportID, Supporter.GroupID.Value) > 0)
        {
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "alert('支持方组别已经存在。');", true);
            return;
        }
        int ret = 0;
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.SupportID]) && Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.SupportID]) != 0)
        {
            if (ESP.Finance.BusinessLogic.SupporterManager.Update(Supporter) == UpdateResult.Succeed)
                ret = 1;
        }
        else
        {
            ret = ESP.Finance.BusinessLogic.SupporterManager.Add(Supporter);
        }
        string retstr = string.Empty;
        if (ret > 0)
        {
            string script = @"var uniqueId = 'ctl00$ContentPlaceHolder1$ProjectSupporter$';
opener.__doPostBack(uniqueId + 'btnRet', '');
window.close(); ";

            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);

            // retstr = string.Format("opener.location='{0}?{1}={2}';window.close();", Request[ESP.Finance.Utility.RequestName.BackUrl], RequestName.ProjectID,Request[ESP.Finance.Utility.RequestName.ProjectID]);
        }
        else
        {
            retstr = "alert('请检查项目整体费用,无法添加支持方.');";
        }
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), retstr, true);
    }

    private void DepartmentDataBind()
    {
        List<Department> dt = ESP.Compatible.DepartmentManager.GetByParent(0);
        ddltype.DataSource = dt;
        ddltype.DataTextField = "NodeName";
        ddltype.DataValueField = "UniqID";
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "window.close(); ", true);
    }
}