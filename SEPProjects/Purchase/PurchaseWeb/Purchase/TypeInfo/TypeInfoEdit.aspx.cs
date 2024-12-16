using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;

public partial class Purchase_TypeInfo_TypeInfoEdit : ESP.Web.UI.PageBase
{
    int tid = 0;//typeid,当前物料类别id
    int pid = 0;//parentid，父物料类别id
    private int level = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["tid"]))
        {
            tid = int.Parse(Request["tid"]);
        }
        if (!string.IsNullOrEmpty(Request["pid"]))
        {
            pid = int.Parse(Request["pid"]);
        }

        if (!IsPostBack)
        {
            if (tid != 0)
            {
                TypeInfo model = TypeManager.GetModel(tid);
                txtName.Text = model.typename;
                if (model.auditorid.Trim() != "")
                {
                    txtAuditor.Text = new ESP.Compatible.Employee(int.Parse(model.auditorid)).Name;
                }
                if(/*null != model.shauditorid && */model.shauditorid >0)
                {
                    txtSHAuditor.Text = new ESP.Compatible.Employee(model.shauditorid).Name;
                }
                if (/*null != model.gzauditorid && */model.gzauditorid > 0)
                {
                    txtGZAuditor.Text = new ESP.Compatible.Employee(model.gzauditorid).Name;
                }

                level = model.typelevel;
                hidAuditor.Value = model.auditorid;
                if(model.typelevel == State.producttype_l1)
                {
                    RequiredFieldValidator2.Enabled = false;
                    RequiredFieldValidator4.Enabled = false;
                    labaudit1.Visible = false;
                    labaudit2.Visible = false;
                }
                else if (model.typelevel == State.producttype_l2)
                {
                    RequiredFieldValidator2.Enabled = false;
                    labaudit1.Visible = false;
                }
                else if (model.typelevel == State.producttype_l3)
                {
                    btnSave1.Enabled = false;
                }
            }
            else 
            {
                tab1.Visible = false;
                lab1.Text = "添加第一级物料类别";
                RequiredFieldValidator4.Enabled = false;
            }
            ListBind();
        }
        TypeInfo model1 = TypeManager.GetModel(pid);
        if (model1 == null)
            btnPre.Enabled = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (tid > 0)
        {
            TypeInfo model = new TypeInfo();

            model.typeid = tid;
            model.parentId = pid;
            model.typename = txtName.Text.Trim();
            if (hidAuditor.Value != "")
            {
                model.auditorid = hidAuditor.Value;
            }
            else
            {
                model.auditorid = "0";
            }
            if (pid > 0)
            {
                TypeInfo p = TypeManager.GetModel(pid);
                level = p.typelevel;
            }
            model.typelevel = ++level;

          
            //更新
            if (TypeManager.Update(model) > 0)
            {

                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对物料类别中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, tid.ToString(), "保存"), "物料类别维护");

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location=window.location;", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
            }
        }
    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
        TypeInfo model = new TypeInfo();
        model.parentId = tid;
        model.typename = txtName1.Text.Trim();

        if (hidAuditor1.Value != "")
        {
            model.auditorid = hidAuditor1.Value;
        }
        else
        {
            model.auditorid = "0";
        }
        if(tid >0)
        {
            TypeInfo p = TypeManager.GetModel(tid);
            level = p.typelevel;
        }
        model.typelevel = ++level;


        //更新
        if (TypeManager.Add(model) > 0)
        {
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对物料类别中的 {1} 的数据完成 {2} 的操作", CurrentUser.Name, model.typename.ToString(), "创建"), "物料类别维护");

            ListBind();
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加成功！');window.location=window.location;", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加失败！');", true);
        }
    }

    private void ListBind()
    {
        gvItems.DataSource = TypeManager.GetListForBase(tid);
        gvItems.DataBind();
    }

    protected void Items_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TypeInfo model = (TypeInfo)e.Row.DataItem;
            if (model.status == State.typestatus_block)
            {
                e.Row.Cells[4].Controls.Clear();
            }
            else if (model.status == State.typestatus_used)
            {
                e.Row.Cells[5].Controls.Clear();
            }
        }
    }

    protected void Items_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int typeid = int.Parse(gvItems.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
        if (e.CommandName == "Del")
        {
            TypeInfo typeModel = TypeManager.GetModel(typeid);
            TypeManager.BlockUpOrUse(typeid, typeModel.typelevel, State.typestatus_block);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对物料类别中的 id={1} 的数据完成 {2} 的操作", CurrentUser.Name, typeid.ToString(), "停用"), "物料类别维护");

            ListBind();
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('停用成功！');", true);
        }
        else if (e.CommandName == "Use")
        {
            TypeInfo typeModel = TypeManager.GetModel(typeid);
            TypeManager.BlockUpOrUse(typeid, typeModel.typelevel, State.typestatus_used);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对物料类别中的 id={1} 的数据完成 {2} 的操作", CurrentUser.Name, typeid.ToString(), "启用"), "物料类别维护");

            ListBind();
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('启用成功！');", true);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TypeInfoList.aspx");
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        TypeInfo model = TypeManager.GetModel(pid);
        
        Response.Redirect("TypeInfoEdit.aspx?tid="+pid+"&pid="+(model == null ? 0 : model.parentId).ToString());
    }
}
