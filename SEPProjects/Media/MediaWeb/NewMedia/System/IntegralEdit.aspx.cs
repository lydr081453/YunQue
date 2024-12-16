using System;
using System.Data;
using Web.Components;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ESP.Framework.Entity;

using ESP.Framework.BusinessLogic;

public partial class NewMedia_System_IntegralEdit : ESP.Web.UI.PageBase
{
    protected string strEmployeeJsonSource = string.Empty;
    protected override void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
    }

    private void InitDataGridColumn()
    {
        string strColumn = "UserCode#UserName#Counts";
        string strHeader = "用户编号#用户姓名#用户积分";
        string strH = "center#center#center";
        string sort = "UserCode#UserName#Counts";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, strH, this.dgList);

        TemplateField tf = new TemplateField();
        string clientclick = "";
        MyControls.GridViewOperate.ImageButtonItem itmEdit = new MyControls.GridViewOperate.ImageButtonItem(ESP.Media.Access.Utilities.ConfigManager.EditIconPath, clientclick, "userid", true);
        itmEdit.DoSomething += new MyControls.GridViewOperate.DoSomethingHandler(itmEdit_DoSomething);
        tf.ItemTemplate = itmEdit;
        tf.HeaderText = "编辑";
        dgList.Columns.Add(tf);
    }

    void itmEdit_DoSomething(object sender, string value)
    {
        ESP.MediaLinq.Entity.media_Counter counter = ESP.MediaLinq.BusinessLogic.CounterManager.GetModel(Convert.ToInt32(value));
        this.txtEditUsername.Text = counter.UserName;
        this.lblUserCode.Text = counter.UserCode;
        this.txtUserIntegral.Text = counter.counts.ToString();
        this.hidIntegralID.Value = counter.UserID.ToString();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        bindData();

        List<ESP.MediaLinq.Entity.HeaderInfo> empheaders = new List<ESP.MediaLinq.Entity.HeaderInfo>();
        empheaders.Add(new ESP.MediaLinq.Entity.HeaderInfo("Sysid", "选择"));
        empheaders.Add(new ESP.MediaLinq.Entity.HeaderInfo("Userid", "员工编号"));
        empheaders.Add(new ESP.MediaLinq.Entity.HeaderInfo("Username", "员工姓名"));
        strEmployeeJsonSource = @"{""HeadItem"":" + empheaders.ToJSON() + "}";

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Collections.Hashtable ht = new System.Collections.Hashtable();
        ht.Add("@userID", this.hidLeaderID.Value);
        DataTable dtIntegral = ESP.MediaLinq.BusinessLogic.CounterManager.GetListByUserID(Convert.ToInt32(this.hidLeaderID.Value));
        DataTable dt = dtIntegral.Clone();
        List<int> roleids = ESP.Media.Access.Utilities.ConfigManager.NotIntegralRoleIDList;

        foreach (DataRow dr in dtIntegral.Rows)
        {
            int empid = dr["userid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["userid"]);
            if (empid > 0)
            {
                int[] roles = RoleManager.GetUserRoleIDs(empid);
                //List<FrameWork.Security.Role> roles = GetRoleList(empid);
                if (checkRole(roles, roleids))
                {
                    dt.ImportRow(dr);
                }
            }
        }

        this.dgList.DataSource = dt.DefaultView;
    }
    protected void btnAll_Click(object sender, EventArgs e)
    {
        bindData();
    }

    bool checkRole(int[] source, List<int> des)
    {
        for (int i = 0; i < source.Length; i++)
        {
            for (int j = 0; j < des.Count; j++)
            {
                if (source[i] == des[j])
                    return false;
            }
        }
        return true;
    }

    private void bindData()
    {

        DataTable dtIntegral = ESP.MediaLinq.BusinessLogic.CounterManager.GetAll();
        DataTable dt = dtIntegral.Clone();
        List<int> roleids = ESP.Media.Access.Utilities.ConfigManager.NotIntegralRoleIDList;

        foreach (DataRow dr in dtIntegral.Rows)
        {
            int empid = dr["userid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["userid"]);
            if (empid > 0)
            {
                int[] roles = ESP.Framework.BusinessLogic.RoleManager.GetUserRoleIDs(empid);
                //List<FrameWork.Security.Role> roles = GetRoleList(empid);
                if (checkRole(roles, roleids))
                {
                    dt.ImportRow(dr);
                }
            }
        }

        this.dgList.DataSource = dt.DefaultView;
    }

    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = e.Row.Cells[1].Text;// string.Format("<a href='#' onclick='onclickName(\"" + e.Row.Cells[0].Text + "\",\"" + e.Row.Cells[0].Text + "\",\"" + e.Row.Cells[1].Text + "\",\"" + e.Row.Cells[2].Text + "\");'>{0}</a>", e.Row.Cells[1].Text);
            e.Row.Cells[0].Text = e.Row.Cells[0].Text;
            e.Row.Cells[2].Text = e.Row.Cells[2].Text;

        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        string tip;
        int currentcount = 0;
        if (this.chkAll.Checked)
        {
            int cnt = 0;

            DataTable dtIntegral = ESP.MediaLinq.BusinessLogic.CounterManager.GetAll();
            for (int i = 0; i < dtIntegral.Rows.Count; i++)
            {
                if (ESP.Media.BusinessLogic.CounterManager.DeleteAll(Convert.ToInt32(dtIntegral.Rows[i][0])))
                    cnt++;

            }

            if (cnt == dtIntegral.Rows.Count)
            {
                tip = "alert('积分修改成功.');";
                this.bindData();
            }
            else
            {
                tip = "alert('积分修改失败.');";
            }
        }
        else
        {
            ESP.MediaLinq.Entity.media_Counter counter = ESP.MediaLinq.BusinessLogic.CounterManager.GetModel(Convert.ToInt32(this.hidIntegralID.Value));
            currentcount = counter.counts;// = Convert.ToInt32(this.txtUserIntegral.Text);
            counter.counts = currentcount - Convert.ToInt32(this.txtUserIntegral.Text);
            if (ESP.MediaLinq.BusinessLogic.CounterManager.Update(counter))
            {
                tip = "alert('积分修改成功.');";
                this.bindData();

            }
            else
            {
                tip = "alert('积分修改失败.');";
            }
        }

        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), tip, true);

    }
}
