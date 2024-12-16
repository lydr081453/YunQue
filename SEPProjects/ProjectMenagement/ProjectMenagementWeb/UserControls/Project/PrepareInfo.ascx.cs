using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Utility;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Compatible;
using ESP.Finance.Entity;

public partial class UserControls_Project_PrepareInfo : System.Web.UI.UserControl
{
    int projectid = 0;
    private Employee emp = null;
    public Employee CurrentUser
    {
        get { return emp; }
        set { emp = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(UserControls_Project_PrepareInfo));
        this.ddlProjectType.SelectedIndexChanged += new EventHandler(ddlProjectType_SelectedIndexChanged);
        //this.ddlBizType.Attributes.Add("onChange", "selectBizType(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        //this.ddlProjectType.Attributes.Add("onChange", "selectProjectType(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");
        this.ddlContactStatus.Attributes.Add("onChange", "selectContactStatus(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);");

        if (!IsPostBack)
        {
            DepartmentDataBind();
            IList<ESP.Finance.Entity.ProjectTypeInfo> typeList = typeList = ESP.Finance.BusinessLogic.ProjectTypeManager.GetList("parentid is null");
            this.ddlProjectType.DataSource = typeList;
            this.ddlProjectType.DataTextField = "ProjectTypeName";
            this.ddlProjectType.DataValueField = "ProjectTypeID";
            this.ddlProjectType.DataBind();
            ddlProjectType.Items.Insert(0, new ListItem("请选择", "0"));
            ddlTypeLevel2.Items.Insert(0, new ListItem("请选择", "0"));
            ddlTypeLevel3.Items.Insert(0, new ListItem("请选择", "0"));

            if (emp != null)
            {
                this.hidGroupID.Value = emp.GetDepartmentIDs()[0].ToString();
                this.txtGroup.Text = emp.GetDepartmentNames()[0].ToString();
            }
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                if (project.ProjectTypeID != null)
                {
                    //foreach (ESP.Finance.Entity.ProjectTypeInfo type in typeList)
                    //{
                    //    if (type.ProjectTypeID == project.ProjectTypeID.Value)
                    //    {
                    //        this.ddlProjectType.SelectedValue = type.ProjectTypeID.ToString();
                    //        break;
                    //    }
                    //}
                    ddlProjectType.SelectedValue = project.ProjectTypeID.ToString();
                    ddlTypeLevel2.DataSource = ESP.Finance.BusinessLogic.ProjectTypeManager.GetList("parentid=" + project.ProjectTypeID.ToString());
                    this.ddlTypeLevel2.DataTextField = "ProjectTypeName";
                    this.ddlTypeLevel2.DataValueField = "ProjectTypeID";
                    this.ddlTypeLevel2.DataBind();
                }
                if (project.ProjectTypeLevel2ID != null)
                {
                    ddlTypeLevel2.SelectedValue = project.ProjectTypeLevel2ID.ToString();

                    ddlTypeLevel3.DataSource = ESP.Finance.BusinessLogic.ProjectTypeManager.GetList("parentid=" + ddlTypeLevel2.SelectedValue);
                    this.ddlTypeLevel3.DataTextField = "ProjectTypeName";
                    this.ddlTypeLevel3.DataValueField = "ProjectTypeID";
                    this.ddlTypeLevel3.DataBind();
                }
                if (project.ProjectTypeLevel3ID != null)
                {
                    ddlTypeLevel3.SelectedValue = project.ProjectTypeLevel3ID.ToString();
                    // btnGroup.Disabled = true;
                    btnApplicant.Disabled = true;
                }
                initProjectModel();
            }

            // this.chkNotFromJoint.Checked = true;

        }
    }


    private ESP.Finance.Entity.ProjectInfo project = null;
    public ESP.Finance.Entity.ProjectInfo ProjectModel
    {
        get
        {
            if (project == null)
                project = new ESP.Finance.Entity.ProjectInfo();
            return project;
        }
        set { project = value; }
    }


    private void initProjectModel()
    {
        if (this.project == null)
        {
            // this.chkNotFromJoint.Checked = true;
            return;
        }
        if (!string.IsNullOrEmpty(this.project.ProjectCode))
        {
            this.lblProjectCode.Visible = false;
            this.txtProjectCode.Visible = true;
            this.txtProjectCode.Text = this.project.ProjectCode;
            this.txtProjectCode.Enabled = false;
            this.ddltype.Enabled = false;
            this.ddltype1.Enabled = false;
            this.ddltype2.Enabled = false;
        }
        //this.lblBdProjectCode.Text = project.BDProjectCode;
        this.txtBizDesc.Text = project.BusinessDescription;
        this.txtGroup.Text = project.GroupName;
        this.hidGroupID.Value = project.GroupID.ToString().Trim();


        this.hidContactStatus.Value = project.ContractStatusID.ToString() + "," + project.ContractStatusName;
        this.hidApplicantUserID.Value = project.ApplicantCode;
        this.hidApplicantUserCode.Value = project.ApplicantUserName;


        ddlBizType.SelectedValue = project.BusinessTypeID.ToString();
        this.hidApplicantID.Value = project.ApplicantUserID.ToString();
        this.txtApplicant.Text = project.ApplicantEmployeeName;

        txtBrands.Text = project.Brands;
        txtBusinessPersonName.Text = project.BusinessPersonName;
        hidBusinessPersonId.Value = project.BusinessPersonId == null ? "" : project.BusinessPersonId.ToString();
        txtAdvertiserID.Text = project.AdvertiserID;
        txtCustomerProjectCode.Text = project.CustomerProjectCode;
        hidRelevanceProjectId.Value = project.RelevanceProjectId.ToString();
        txtRelevanceProjectCode.Text = project.RelevanceProjectCode;
    }

    [AjaxPro.AjaxMethod]
    public static string SetRelevanceProjectId(string relevanceProjectCode)
    {
        var relevanceProject = ESP.Finance.BusinessLogic.ProjectManager.GetModelByProjectCode(relevanceProjectCode);
        if (relevanceProject != null)
        {
            return relevanceProject.ProjectId.ToString();
        }
        return "error";
    }

    public void setProjectModel()
    {
        if (project == null)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
                project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            else
                project = new ESP.Finance.Entity.ProjectInfo();
        }
        string[] strs = null;
        //project.BDProjectCode = this.lblBdProjectCode.Text;
        project.ProjectCode = this.txtProjectCode.Text.Trim().ToUpper();
        if (!string.IsNullOrEmpty(this.hidContactStatus.Value))
        {
            strs = this.hidContactStatus.Value.Split(',');
            project.ContractStatusID = Convert.ToInt32(strs[0]);
            project.ContractStatusName = strs[1];
        }

        project.BusinessTypeID = int.Parse(ddlBizType.SelectedValue);

        project.BusinessTypeName = ddlBizType.SelectedItem.Text;

        project.ProjectTypeID = Convert.ToInt32(this.ddlProjectType.SelectedValue);

        project.ProjectTypeName = this.ddlProjectType.SelectedItem.Text;

        project.ProjectTypeLevel2ID = int.Parse(ddlTypeLevel2.SelectedValue);
        ESP.Finance.Entity.ProjectTypeInfo type2Model = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(project.ProjectTypeLevel2ID.Value);
        project.ProjectTypeCode = type2Model.TypeCode;
        project.ProjectTypeLevel3ID = int.Parse(ddlTypeLevel3.SelectedValue);
        ESP.Finance.Entity.ProjectTypeInfo type3Model = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(project.ProjectTypeLevel3ID.Value);
        if (project.MediaCostRate == null)
            project.MediaCostRate = type3Model.CostRate;


        if (!string.IsNullOrEmpty(this.hidGroupID.Value))
            project.GroupID = Convert.ToInt32(this.hidGroupID.Value);
        project.GroupName = this.txtGroup.Text;

        project.ApplicantCode = this.hidApplicantUserID.Value;
        project.ApplicantUserName = this.hidApplicantUserCode.Value;

        project.BusinessDescription = this.txtBizDesc.Text;
        if (!string.IsNullOrEmpty(this.hidApplicantID.Value))
        {
            project.ApplicantUserID = int.Parse(this.hidApplicantID.Value);
            ESP.Compatible.Employee emp = new Employee(project.ApplicantUserID);
            project.ApplicantUserEmail = emp.EMail;
            project.ApplicantUserPhone = emp.Telephone;
            project.ApplicantUserPosition = emp.PositionDescription;
        }
        project.ApplicantEmployeeName = this.txtApplicant.Text;
        project.IsCalculateByVAT = 1;
        project.Brands = txtBrands.Text.Trim();
        project.BusinessPersonId = int.Parse(hidBusinessPersonId.Value);
        project.BusinessPersonName = txtBusinessPersonName.Text.Trim();
        project.AdvertiserID = txtAdvertiserID.Text.Trim();
        project.CustomerProjectCode = txtCustomerProjectCode.Text.Trim();

        project.RelevanceProjectCode = txtRelevanceProjectCode.Text.Trim();
        project.RelevanceProjectId = int.Parse(hidRelevanceProjectId.Value=="" ? "0" : hidRelevanceProjectId.Value);
    }
    public ESP.Finance.Entity.ProjectInfo setProjectModel(ESP.Finance.Entity.ProjectInfo project)
    {
        string[] strs = null;

        //project.BDProjectCode = this.lblBdProjectCode.Text;
        project.ProjectCode = this.txtProjectCode.Text.Trim().ToUpper();
        if (!string.IsNullOrEmpty(this.hidContactStatus.Value))
        {
            strs = this.hidContactStatus.Value.Split(',');
            project.ContractStatusID = Convert.ToInt32(strs[0]);
            project.ContractStatusName = strs[1];
        }

        project.BusinessTypeID = int.Parse(ddlBizType.SelectedValue);

        project.BusinessTypeName = ddlBizType.SelectedItem.Text;

        project.ProjectTypeID = Convert.ToInt32(this.ddlProjectType.SelectedValue);
        project.ProjectTypeName = this.ddlProjectType.SelectedItem.Text;

        project.ProjectTypeLevel2ID = int.Parse(ddlTypeLevel2.SelectedValue);
        ESP.Finance.Entity.ProjectTypeInfo typeModel = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(project.ProjectTypeLevel2ID.Value);
        project.ProjectTypeCode = typeModel.TypeCode;
        ESP.Finance.Entity.ProjectTypeInfo type3Model = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(project.ProjectTypeLevel3ID.Value);
        project.MediaCostRate = type3Model.CostRate;
        //if (project.ContractStatusName == ESP.Finance.Utility.ProjectType.BDProject)
        //{
        //    project.ContractStatusID = 0;
        //    project.ContractStatusName = string.Empty;
        //}


        if (!string.IsNullOrEmpty(this.hidGroupID.Value))
            project.GroupID = Convert.ToInt32(this.hidGroupID.Value);
        project.GroupName = this.txtGroup.Text;

        project.ApplicantCode = this.hidApplicantUserID.Value;
        project.ApplicantUserName = this.hidApplicantUserCode.Value;

        project.BusinessDescription = this.txtBizDesc.Text;
        if (!string.IsNullOrEmpty(this.hidApplicantID.Value))
        {
            project.ApplicantUserID = int.Parse(this.hidApplicantID.Value);
            ESP.Compatible.Employee emp = new Employee(project.ApplicantUserID);
            project.ApplicantUserEmail = emp.EMail;
            project.ApplicantUserPhone = emp.Telephone;
            project.ApplicantUserPosition = emp.PositionDescription;
        }
        project.ApplicantEmployeeName = this.txtApplicant.Text;
        project.BusinessPersonId = int.Parse(hidBusinessPersonId.Value);
        project.BusinessPersonName = txtBusinessPersonName.Text.Trim();
            project.RelevanceProjectCode = txtRelevanceProjectCode.Text.Trim();
            project.RelevanceProjectId = int.Parse(hidRelevanceProjectId.Value == "" ? "0" : hidRelevanceProjectId.Value);
        return project;
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> getContactStatus()
    {
        IList<ESP.Finance.Entity.ContractStatusInfo> plist = ESP.Finance.BusinessLogic.ContractStatusManager.GetList(null, null);
        List<List<string>> list = new List<List<string>>();
        List<string> item = null;
        foreach (ESP.Finance.Entity.ContractStatusInfo type in plist)
        {
            item = new List<string>();
            item.Add(type.ContractStatusID.ToString());
            item.Add(type.ContractStatusName);
            list.Add(item);
        }
        List<string> c = new List<string>();
        c.Add("-1");
        c.Add("请选择...");
        list.Insert(0, c);
        return list;
    }

    [AjaxPro.AjaxMethod]
    public static List<List<string>> getBizType()
    {
        IList<ESP.Finance.Entity.BizTypeInfo> plist = ESP.Finance.BusinessLogic.BizTypeManager.GetList(null, null);
        List<List<string>> list = new List<List<string>>();
        List<string> item = null;
        foreach (ESP.Finance.Entity.BizTypeInfo type in plist)
        {
            item = new List<string>();
            item.Add(type.BizID.ToString());
            item.Add(type.BizTypeName);
            list.Add(item);
        }
        List<string> c = new List<string>();
        c.Add("-1");
        c.Add("请选择...");
        list.Insert(0, c);
        return list;

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

    protected void ddlProjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
        //{
        //    projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        //    project = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
        //    project.ProjectTypeID = Convert.ToInt32(this.ddlProjectType.SelectedValue);

        //    ESP.Finance.Entity.ProjectTypeInfo typeModel = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(project.ProjectTypeID.Value);

        //    project.ProjectTypeName = this.ddlProjectType.SelectedItem.Text;
        //    project.ProjectTypeCode = typeModel.TypeCode;

        //    ESP.Finance.BusinessLogic.ProjectManager.Update(project);
        //}

        //绑定二级分类
        var typeLevele2List = ESP.Finance.BusinessLogic.ProjectTypeManager.GetList("parentid =" + ddlProjectType.SelectedValue);
        ddlTypeLevel2.DataSource = typeLevele2List;
        ddlTypeLevel2.DataTextField = "ProjectTypeName";
        ddlTypeLevel2.DataValueField = "ProjectTypeID";
        ddlTypeLevel2.DataBind();
        ddlTypeLevel2.Items.Insert(0, new ListItem("请选择", "0"));

        ddlTypeLevel3.Items.Clear();
        ddlTypeLevel3.Items.Insert(0, new ListItem("请选择", "0"));

    }

    protected void ddlTypeLevel2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTypeLevel3.Items.Clear();

        var typeLevele3List = ESP.Finance.BusinessLogic.ProjectTypeManager.GetList("parentid =" + ddlTypeLevel2.SelectedValue);
        ddlTypeLevel3.DataSource = typeLevele3List;
        ddlTypeLevel3.DataTextField = "ProjectTypeName";
        ddlTypeLevel3.DataValueField = "ProjectTypeID";
        ddlTypeLevel3.DataBind();
        ddlTypeLevel3.Items.Insert(0, new ListItem("请选择", "0"));

        if (ddlTypeLevel2.SelectedIndex != 0)
        {
            var typeModel = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(int.Parse(ddlTypeLevel2.SelectedValue));
            hidProjectTypeCode.Value = typeModel.TypeCode;
        }
        else
        {
            hidProjectTypeCode.Value = "";
        }
    }
    //    project.ApplicantCode = this.hidApplicantUserID.Value;
    //project.ApplicantUserName = this.hidApplicantUserCode.Value;

    //project.BusinessDescription = this.txtBizDesc.Text;
    //if (!string.IsNullOrEmpty(this.hidApplicantID.Value))
    //{
    //    project.ApplicantUserID = int.Parse(this.hidApplicantID.Value);
    protected void ddlTypeLevel3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTypeLevel3.SelectedIndex != 0)
        {
            var type2Model = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(int.Parse(ddlTypeLevel3.SelectedValue));
            if (type2Model.ProjectHeadId != 0)
            {
                //类型负责人、部门信息
                ESP.Framework.Entity.UserInfo user = ESP.Framework.BusinessLogic.UserManager.Get(type2Model.ProjectHeadId);
                ESP.Framework.Entity.EmployeeInfo employee = ESP.Framework.BusinessLogic.EmployeeManager.Get(user.UserID);
                hidApplicantID.Value = user.UserID.ToString(); ;
                txtApplicant.Text = user.LastNameCN + user.FirstNameCN;
                hidApplicantUserID.Value = employee.Code;
                hidApplicantUserCode.Value = user.Username;

                //ESP.Framework.Entity.DepartmentInfo department = ESP.Framework.BusinessLogic.DepartmentManager.

                Employee emp = new Employee(user.UserID);

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

                txtGroup.Text = groupname;
                hidGroupID.Value = groupid;
                btnApplicant.Disabled = true;
                //btnGroup.Disabled = true;
            }
        }
        else
        {
            txtApplicant.Text = "";
            txtGroup.Text = "";

            txtGroup.Text = "";
            hidGroupID.Value = "";
            btnApplicant.Disabled = false;
            // btnGroup.Disabled = false;
        }
    }
}