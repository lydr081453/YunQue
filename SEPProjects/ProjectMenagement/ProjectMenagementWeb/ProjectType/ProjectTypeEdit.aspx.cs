using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
public partial class ProjectType_ProjectTypeEdit : ESP.Web.UI.PageBase
{
    int typeId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["typeid"]))
        {
            typeId = int.Parse(Request["typeid"]);
        }
        if (!IsPostBack)
        {
            bindInfo();
        }
    }

    /// <summary>
    /// 绑定页面信息
    /// </summary>
    private void bindInfo()
    {
        ddlParent.DataSource = ESP.Finance.BusinessLogic.ProjectTypeManager.GetList("parentid is null");
        ddlParent.DataTextField = "ProjectTypeName";
        ddlParent.DataValueField = "ProjectTypeID";
        ddlParent.DataBind();
        ddlParent.Items.Insert(0, "请选择...");
        ddlParent2.Items.Insert(0, "请选择...");

        

        //model.ProjectTypeName = txtProjectTypeName.Text.Trim();
        //model.Description = txtDesc.Text.Trim();
        //model.TypeCode = txtCode.Text.Trim();
        //model.ProjectHeadId = int.Parse(hidHead.Value);

        if (typeId > 0)
        {
            ddlParent.Enabled = ddlParent2.Enabled = false;
            ESP.Finance.Entity.ProjectTypeInfo model = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(typeId);
            if (model != null)
            {
                if (model.ParentID != null && model.ParentID != 0)
                {
                    var parent = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(model.ParentID.Value);
                    if (parent != null && parent.ParentID != null && parent.ParentID != 0)
                    {
                        ddlParent.Items.Insert(0, new ListItem(ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(model.ParentID.Value).ProjectTypeName, parent.ParentID.ToString()));
                        ddlParent2.Items.Insert(0, new ListItem(parent.ProjectTypeName, parent.ProjectTypeID.ToString()));
                        btnHead.Disabled = false;
                    }
                    else
                    {

                        ddlParent.Items.Insert(0, new ListItem(ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(model.ParentID.Value).ProjectTypeName, model.ParentID.ToString()));
                    }
                    
                }
                hidProjectTypeID.Value = model.ProjectTypeID.ToString();
                txtProjectTypeName.Text = model.ProjectTypeName;
                txtDesc.Text = model.Description;
                hidHead.Value = model.ProjectHeadId.ToString();
                if (model.ProjectHeadId != 0)
                {
                    var user = ESP.Framework.BusinessLogic.UserManager.Get(model.ProjectHeadId);
                    txtProjectHead.Text = user.LastNameCN + user.FirstNameCN;
                }
                txtCode.Text = model.TypeCode;
                txtCostRate.Text = ((model.CostRate ?? 0) * 100).ToString("0.00");
            }
        }
    }

    private bool checkCode()
    {
        if (txtCode.Text.Trim().ToUpper() == "P")
            return false;
        else
        {
            if (typeId == 0 && !string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                return ESP.Finance.BusinessLogic.ProjectTypeManager.GetList(" TypeCode='" + txtCode.Text.Trim().ToUpper() + "'").Count == 0;
            }
        }
        return true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if ( !checkCode() )
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('类型代码已被占用，请更换其他代码！');", true);
            return;
        }
        if (typeId > 0)
        {
            if (ESP.Finance.Utility.UpdateResult.Succeed == ESP.Finance.BusinessLogic.ProjectTypeManager.Update(getModel()))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location='ProjectTypeList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
            }
        }
        else
        {
            if (ESP.Finance.BusinessLogic.ProjectTypeManager.Add(getModel()) > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location='ProjectTypeList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectTypeList.aspx");
    }

    private ESP.Finance.Entity.ProjectTypeInfo getModel()
    {
        ESP.Finance.Entity.ProjectTypeInfo model = null;
        if (typeId > 0)
        {
            model = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(typeId);
        }
        else
        {
            model = new ESP.Finance.Entity.ProjectTypeInfo();
        }
        if (ddlParent2.SelectedIndex != 0)
        {
            model.ParentID = int.Parse(ddlParent2.SelectedValue);
        }
        else
        {
            if (ddlParent.SelectedIndex != 0)
                model.ParentID = int.Parse(ddlParent.SelectedValue);
        }
        model.ProjectTypeName = txtProjectTypeName.Text.Trim();
        model.Description = txtDesc.Text.Trim();
        model.TypeCode = txtCode.Text.Trim().ToUpper();
        model.ProjectHeadId = int.Parse(hidHead.Value);
        if (!string.IsNullOrEmpty(txtCostRate.Text))
            model.CostRate = decimal.Parse(txtCostRate.Text.Trim()) / 100;
        return model;
    }

    protected void ddlParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlParent.SelectedIndex != 0)
        {
            ddlParent2.DataSource = ESP.Finance.BusinessLogic.ProjectTypeManager.GetList(" parentid=" + ddlParent.SelectedValue);
            ddlParent2.DataTextField = "ProjectTypeName";
            ddlParent2.DataValueField = "ProjectTypeID";
            ddlParent2.DataBind();
        }
        else
        {
            ddlParent2.Items.Clear();
        }
        ddlParent2.Items.Insert(0, "请选择...");
        setCtlReq();
    }

    protected void ddlParent2_SelectedIndexChanged(object sender, EventArgs e)
    {
        setCtlReq();
    }

    private void setCtlReq()
    {
        if (ddlParent.SelectedIndex != 0 && ddlParent2.SelectedIndex == 0)
        {
            txtCode.Enabled = true;
            RequiredFieldValidator2.Enabled = true;
            RequiredFieldValidator3.Enabled = false;
        }
        else
        {
            txtCode.Text = "";
            txtCode.Enabled = false;
            RequiredFieldValidator2.Enabled = false;
        }
        if (ddlParent2.SelectedIndex != 0)
        {
            btnHead.Disabled = false;
            RequiredFieldValidator3.Enabled = true;
        }
        else
        {
            txtProjectHead.Text = "";
            hidHead.Value = "0";
            btnHead.Disabled = true;
            RequiredFieldValidator3.Enabled = false;
        }
    }
}
