using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckInfo_NewCheck : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveList(this.txtBegin.Text.Trim(), Convert.ToInt32(txtCheckCount.Text.Trim())) > 0)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次添加成功!');window.location='CheckList.aspx'", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('批次添加时出现异常!');", true);
        }
    }

    private int SaveList(string BeginCode, int count)
    {
        List<ESP.Finance.Entity.CheckInfo> checkList = new List<ESP.Finance.Entity.CheckInfo>();
        string FixCode=string.Empty;
        int ret = 0;
        if (BeginCode.Length > 4)
            FixCode = BeginCode.Substring(0, BeginCode.Length - 4);
        else
            FixCode = BeginCode;
        int beginValue=0;
        try
        {
            beginValue=Convert.ToInt32(BeginCode.Substring(BeginCode.Length-4));
        }
        catch
        {
             ClientScript.RegisterStartupScript(typeof(string), "", "alert('您输入的支票起始号码后四位有误!');", true);
        }
        ESP.Finance.Entity.CheckInfo model = null;
        for(int i=0;i<count;i++)
        {
            model = new ESP.Finance.Entity.CheckInfo();
            model.CheckCode = FixCode + beginValue.ToString();
            model.CheckSysCode = "Z" + beginValue.ToString();
            model.CreateDate = DateTime.Now;
            model.CreatorEmployeeName = CurrentUser.Name;
            model.CreatorID = Convert.ToInt32(CurrentUser.SysID);
            model.CreatorUserCode = CurrentUser.ID;
            model.CreatorUserName = CurrentUser.ITCode;
            model.CheckStatus = ESP.Finance.Utility.CheckStatus.New;
            checkList.Add(model);
            beginValue++;
        }
        try
        {
            ret = ESP.Finance.BusinessLogic.CheckManager.Add(checkList);
        }
        catch
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('您输入的支票起始号码可能已经存在!');", true);
        }
        return ret;
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("CheckList.aspx");
    }
}
