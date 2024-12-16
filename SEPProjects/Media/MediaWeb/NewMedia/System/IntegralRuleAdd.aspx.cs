using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Compatible;
public partial class NewMedia_System_IntegralRuleAdd : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getOperateType();
            getTables();
            reset();
        }
    }
    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);
        int userid = CurrentUserID;
    }
    static Hashtable htOperate = new Hashtable();
    static Hashtable htTable = new Hashtable();

    void getOperateType()
    {
        DataTable dtOperate = ESP.MediaLinq.BusinessLogic.OperateTypeManager.GetAll();
        if (dtOperate != null && dtOperate.Rows.Count > 0)
        {
            ddlOperateType.DataSource = dtOperate;
            ddlOperateType.DataTextField = "AltName";
            ddlOperateType.DataValueField = "ID";
            ddlOperateType.DataBind();
            if (!htOperate.ContainsKey(0))
            {
                htOperate.Add(0, "operate");
            }
            foreach (DataRow row in dtOperate.Rows)
            {
                if (!htOperate.ContainsKey(Convert.ToInt32(row["ID"])))
                {
                    htOperate.Add(Convert.ToInt32(row["ID"]), row["Name"]);
                }
            }
        }
        ddlOperateType.Items.Insert(0,new ListItem("请选择","0"));
    }

    void getTables()
    {
        DataTable dtTables = ESP.MediaLinq.BusinessLogic.TableManager.GetAll();
        if (dtTables != null && dtTables.Rows.Count > 0)
        {
            ddlTable.DataSource = dtTables;
            ddlTable.DataTextField = "AltTableName";
            ddlTable.DataValueField = "TableID";
            ddlTable.DataBind();
            if (!htTable.ContainsKey(0))
            {
                htTable.Add(0, "table");
            }
            foreach (DataRow row in dtTables.Rows)
            {
                if (!htTable.ContainsKey(Convert.ToInt32(row["TableID"])))
                {
                    htTable.Add(Convert.ToInt32(row["TableID"]), row["TableName"]);
                }
            }
        }
        ddlTable.Items.Insert(0, new ListItem("请选择", "0"));
    }

    void reset()
    {
        ddlOperateType.SelectedIndex = 0;
        ddlTable.SelectedIndex = 0;
        txtIntegral.Text = string.Empty;
        txtkey.Text = string.Empty;
        txtAltname.Text = string.Empty;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    { 
        string msg = string.Empty;
        if (txtkey.Text.Length <= 0)
        {
            msg += "关键字 ";
        }
        if (txtAltname.Text.Length <= 0)
        {
            msg += "操作名称 ";
        }
        if (txtIntegral.Text.Length <= 0)
        {
            msg += "积分 ";
        }
        if (msg.Length > 0)
        {
            msg = "请填写 :" + msg + "信息!";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "saveintegralok", string.Format("alert('{0}')",msg), true);
            return;
        }

        ESP.MediaLinq.Entity.media_IntegralRule rule = new ESP.MediaLinq.Entity.media_IntegralRule();
        rule.TableID = Convert.ToInt32( ddlTable.SelectedValue);
        rule.OperateID = Convert.ToInt32(ddlOperateType.SelectedValue);
        rule.name = txtkey.Text;
        rule.altname = txtAltname.Text;
        rule.Integral = Convert.ToInt32(txtIntegral.Text);
        int result = ESP.MediaLinq.BusinessLogic.IntegralManager.Add(rule);
        if (result > 0)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "saveintegralok", "alert('保存成功!')",true);
            reset();
            return;
        }
        else
        {
            if (result == -1)
            {
                msg = "积分规则已经存在!请重新选择.";
            }
            else if (result == -2)
            {
                msg = "关键字已使用!请重新输入.";
            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "saveintegralok", string.Format("alert('{0}')", msg), true);
        }
    }
    protected void ddlOperateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtkey.Text = htOperate[Convert.ToInt32(ddlOperateType.SelectedValue)].ToString().Trim() + htTable[Convert.ToInt32(ddlTable.SelectedValue)].ToString().Trim();
        txtAltname.Text = ddlOperateType.SelectedItem.Text.Trim() + ddlTable.SelectedItem.Text.ToString().Trim();
    }
    protected void ddlTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtkey.Text = htOperate[Convert.ToInt32(ddlOperateType.SelectedValue)].ToString().Trim() + htTable[Convert.ToInt32(ddlTable.SelectedValue)].ToString().Trim();
        txtAltname.Text = ddlOperateType.SelectedItem.Text.Trim() + ddlTable.SelectedItem.Text.Trim();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("IntegralConfig.aspx");
    }
 
}
