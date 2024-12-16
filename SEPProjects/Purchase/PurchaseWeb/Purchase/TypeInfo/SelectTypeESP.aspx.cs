using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Entity;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class SelectTypeESP : ESP.Web.UI.PageBase
{
    string script = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int n = 0;
            BindTree(stv0.Nodes, 0, ref n);
            stv0.CollapseAll();
        }
    }

    /// <summary>
    /// 绑定树，采购物料
    /// </summary>
    /// <param name="nds"></param>
    /// <param name="parentId"></param>
    /// <param name="n"></param>
    void BindTree(TreeNodeCollection nds, int parentId, ref int n)
    {
        DataTable dt = TypeManager.GetAllList().Tables[0];
        TreeNode tn = null;
        foreach (DataRow dr in dt.Select("parentId=" + parentId))
        {
            tn = new TreeNode(dr["typename"].ToString(), dr["typeid"].ToString(), null, "", "_self");
            tn.ShowCheckBox = false;
            n++;
            nds.Add(tn);
            string tmp = tn.ValuePath;
            tn.NavigateUrl = "";
            BindTree(tn.ChildNodes, Convert.ToInt32(dr["typeid"]), ref n);
        }
    }

    protected void stv0_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode t = this.stv0.SelectedNode;
        int levelnum = LevelNum(t.ValuePath);
        if (levelnum == 3)
        {
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_hidESPTypeid').value= '" + t.Value + "';";
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_txtESPTypeName').value= '" + t.Text + "';";
            script += @" parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择三级物料！');", true);

        }
    }

    /// <summary>
    /// 获取页面传参中 vpath 的物料级别
    /// </summary>
    /// <param name="vpath"></param>
    /// <returns></returns>
    public int LevelNum(string vpath)
    {
        int n = 1;
        foreach (char c in vpath)
        {
            if (c == '/')
                n++;
        }
        return n;
    }
}

