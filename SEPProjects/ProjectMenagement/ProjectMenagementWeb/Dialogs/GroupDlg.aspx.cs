using System;
using System.Data;
using System.Collections.Generic;
using ESP.Finance.Utility;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Compatible;

public partial class Dialogs_GroupDlg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Dialogs_GroupDlg));
        if (!IsPostBack)
        {
            DepartmentDataBind();
        }
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
}
