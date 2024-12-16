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


public partial class SelectTypeSupply : ESP.Web.UI.PageBase
{
    string script = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int n = 0;
            BindTreeVersionClass(stv1.Nodes, 0, ref n);
            stv1.CollapseAll();
        }
    }

    /// <summary>
    /// 绑定树，供应链一级物料
    /// </summary>
    /// <param name="nds"></param>
    /// <param name="parentId"></param>
    /// <param name="n"></param>
    void BindTreeVersionClass(TreeNodeCollection nds, int parentId, ref int n)
    {
        DataTable dt = new XML_VersionClassManager().GetAllListUsed().Tables[0];
        TreeNode tn = null;
        foreach (DataRow dr in dt.Select("parentId=" + parentId))
        {
            tn = new TreeNode(dr["name"].ToString(), dr["id"].ToString(), null, "", "_self");
            tn.ShowCheckBox = false;
            n++;
            nds.Add(tn);
            string tmp = tn.ValuePath;
            tn.NavigateUrl = "";
            BindTreeVersionClass2(tn.ChildNodes, Convert.ToInt32(dr["id"]), ref n);
        }
    }

    /// <summary>
    /// 绑定树，供应链二级物料
    /// </summary>
    /// <param name="nds"></param>
    /// <param name="parentId"></param>
    /// <param name="n"></param>
    void BindTreeVersionClass2(TreeNodeCollection nds, int parentId, ref int n)
    {
        DataTable dt = new XML_VersionClassManager().GetAllListUsed().Tables[0];
        TreeNode tn = null;
        foreach (DataRow dr in dt.Select("parentId=" + parentId))
        {
            tn = new TreeNode(dr["name"].ToString(), dr["id"].ToString(), null, "", "_self");
            tn.ShowCheckBox = false;
            n++;
            nds.Add(tn);
            string tmp = tn.ValuePath;
            tn.NavigateUrl = "";
            BindTreeVersionList(tn.ChildNodes, Convert.ToInt32(dr["id"]), ref n);
        }
    }

    /// <summary>
    /// 绑定树，供应链三级物料
    /// </summary>
    /// <param name="nds"></param>
    /// <param name="parentId"></param>
    /// <param name="n"></param>
    void BindTreeVersionList(TreeNodeCollection nds, int parentId, ref int n)
    {
        DataTable dt = new XML_VersionListManager().GetAllListUsed().Tables[0];
        TreeNode tn = null;
        foreach (DataRow dr in dt.Select("classid=" + parentId))
        {
            tn = new TreeNode(dr["name"].ToString(), dr["id"].ToString(), null, "", "_self");
            tn.ShowCheckBox = false;
            n++;
            nds.Add(tn);
            string tmp = tn.ValuePath;
            tn.NavigateUrl = "";
        }
    }

    protected void stv1_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode t = this.stv1.SelectedNode;
        int levelnum = LevelNum(t.ValuePath);
        if (levelnum == 3)
        {
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_hidSupplyTypeid').value= '" + t.Value + "';";
            script += "parent.document.getElementById('ctl00_ContentPlaceHolder1_txtSupplyTypeName').value= '" + t.Text + "';";
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
