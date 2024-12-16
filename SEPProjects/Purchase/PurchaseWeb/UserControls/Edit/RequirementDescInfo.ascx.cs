using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class UserControls_Edit_RequirementDescInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
            
    }

    /// <summary>
    /// 绑定信息
    /// </summary>
    /// <param name="g"></param>
    public void BindInfo(GeneralInfo g)
    {
        txtsow.Text = g.sow;
    }

    /// <summary>
    /// 设置对象信息
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    public void setModelInfo(GeneralInfo g)
    {
        g.sow = txtsow.Text.Trim();
    }
}