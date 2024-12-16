using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;

public partial class Purchase_TypeInfo_TypeInfoList : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int n = 0;
            BindTree(stv1.Nodes, 0, ref n);
            stv1.CollapseAll();
        }
    }

    void BindTree(TreeNodeCollection nds, int parentId, ref int n)
    {
        DataTable dt = TypeManager.GetAllList().Tables[0];
        TreeNode tn = null;
        foreach (DataRow dr in dt.Select("parentId=" + parentId))
        {
            tn = new TreeNode(dr["typename"].ToString(), dr["typeid"].ToString(), null, "TypeInfoEdit.aspx?tid=" + dr["typeid"].ToString()+"&pid="+parentId, "_self");
            tn.ShowCheckBox = false;
            n++;
            nds.Add(tn);
            BindTree(tn.ChildNodes, Convert.ToInt32(dr["typeid"]), ref n);
        }
    }

}
