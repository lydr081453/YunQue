using System;
using System.Data;
using Web.Components;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.BusinessLogic;

using ESP.Framework.Entity;

using ESP.Framework.BusinessLogic;
using ESP.Media.Entity;

public partial class System_IntegralEdit : ESP.Web.UI.PageBase
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
        ESP.Media.Entity.CounterInfo counter = ESP.Media.BusinessLogic.CounterManager.getModel(Convert.ToInt32(value));
        this.txtEditUsername.Text = counter.Username;
        this.lblUserCode.Text = counter.Usercode;
        this.txtUserIntegral.Text = counter.Counts.ToString();
        this.hidIntegralID.Value = counter.Userid.ToString();
    }


    protected void Page_Load(object sender, EventArgs e)
    { 
        bindData();
       
            List<ESP.Media.Entity.HeaderInfo> empheaders = new List<ESP.Media.Entity.HeaderInfo>();
            empheaders.Add(new ESP.Media.Entity.HeaderInfo("Sysid", "选择"));
            empheaders.Add(new ESP.Media.Entity.HeaderInfo("Userid", "员工编号"));
            empheaders.Add(new ESP.Media.Entity.HeaderInfo("Username", "员工姓名"));
            strEmployeeJsonSource = @"{""HeadItem"":" + empheaders.ToJSON() + "}";
 
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Collections.Hashtable ht = new System.Collections.Hashtable();
        ht.Add("@userID",this.hidLeaderID.Value);
        DataTable dtIntegral = ESP.Media.BusinessLogic.CounterManager.GetList(" and userID=@userID", ht);
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

        DataTable dtIntegral = ESP.Media.BusinessLogic.CounterManager.GetAllList();
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
        int currentcount=0;
        if (this.chkAll.Checked)
        {
            int cnt = 0;

            DataTable dtIntegral = ESP.Media.BusinessLogic.CounterManager.GetAllList();
            for (int i = 0; i < dtIntegral.Rows.Count; i++)
            {
                if(ESP.Media.BusinessLogic.CounterManager.DeleteAll(Convert.ToInt32(dtIntegral.Rows[i][0])))
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
            CounterInfo counter = CounterManager.getModel(Convert.ToInt32(this.hidIntegralID.Value));
            currentcount= counter.Counts;// = Convert.ToInt32(this.txtUserIntegral.Text);
            counter.Counts = currentcount - Convert.ToInt32(this.txtUserIntegral.Text);
            if (ESP.Media.BusinessLogic.CounterManager.modify(counter, CurrentUserID))
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
