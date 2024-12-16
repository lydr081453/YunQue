using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class ProjectTypeInfo_ProjectTypeList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
    }

    private void ListBind()
    {
        string terms = "";
        List<SqlParameter> parms = new List<SqlParameter>();
        if (txtProjectTypeName.Text.Trim() != "")
        {
            terms += " ProjectTypename like '%'+@ProjectTypename+'%'";
            parms.Add(new SqlParameter("@ProjectTypename", txtProjectTypeName.Text.Trim()));
        }
        IList<ESP.Finance.Entity.ProjectTypeInfo> list = ESP.Finance.BusinessLogic.ProjectTypeManager.GetList(terms, parms);
        gvList.DataSource = list;
        gvList.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ListBind();
    }

    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        ListBind();
    }

    protected string getTypeName(object parentId,string typeName)
    {
        var parent = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(parentId == null ? 0 : (int)parentId);
        if (parent != null)
        {
            typeName = parent.ProjectTypeName + " - " + typeName;
            return getTypeName(parent.ParentID, typeName);
        }
        return typeName;
    }

    protected string getHeadName(object headId)
    {
        if (headId != null && (int)headId !=0)
        {
            var user = ESP.Framework.BusinessLogic.UserManager.Get((int)headId);
            return user.LastNameCN + user.FirstNameCN;
        }
        return "";
    }

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int projectTypeID = int.Parse(e.CommandArgument.ToString());
            var projectType = ESP.Finance.BusinessLogic.ProjectTypeManager.GetModel(projectTypeID);
            projectType.Status = 0;
            if (ESP.Finance.BusinessLogic.ProjectTypeManager.Update(projectType) == ESP.Finance.Utility.UpdateResult.Succeed)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除成功！');", true);
                ListBind();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('删除失败！');", true);
            }
        }
    }
}
