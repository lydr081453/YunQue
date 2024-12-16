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



public partial class ESPAndSupplyTypeInfo : ESP.Web.UI.PageBase
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
            int n = 0;
            BindTree(stv0.Nodes, 0, ref n);
            stv0.CollapseAll();
            ShowTypeAndNode();

            if (tid != 0)
            {
                TypeInfo model = TypeManager.GetModel(tid);
                txtName.Text = model.typename;
                if (model.auditorid.Trim() != "")
                {
                    txtAuditor.Text = new ESP.Compatible.Employee(int.Parse(model.auditorid)).Name;
                }
                if (/*null != model.shauditorid && */model.shauditorid > 0)
                {
                    txtSHAuditor.Text = new ESP.Compatible.Employee(model.shauditorid).Name;
                }
                if (/*null != model.gzauditorid && */model.gzauditorid > 0)
                {
                    txtGZAuditor.Text = new ESP.Compatible.Employee(model.gzauditorid).Name;
                }

                level = model.typelevel;
                hidAuditor.Value = model.auditorid;
                if (model.typelevel == State.producttype_l1)
                {
                    RequiredFieldValidator2.Enabled = false;
                    RequiredFieldValidator4.Enabled = false;
                    labaudit1.Visible = false;
                    labaudit2.Visible = false;
                    tr4.Visible = false;
                    lblEditTitle.Text = "一级物料维护";
                    lblAddTitle.Text = "添加二级物料";
                    lblListTitle.Text = "二级物料列表";
                }
                else if (model.typelevel == State.producttype_l2)
                {
                    RequiredFieldValidator2.Enabled = false;
                    labaudit1.Visible = false;
                    tr4.Visible = false;
                    lblEditTitle.Text = "二级物料维护";
                    lblAddTitle.Text = "添加三级物料";
                    lblListTitle.Text = "三级物料列表";
                }
                else if (model.typelevel == State.producttype_l3)
                {
                    btnSave1.Enabled = false;
                    tr2.Visible = false;
                    tr3.Visible = false;
                    tr4.Visible = true;
                    ESPAndSupplyTypeListBind(model.typeid);
                    lblEditTitle.Text = "三级物料维护";
                }
            }
            else
            {
                tab1.Visible = false;
                tr4.Visible = false;
                lab1.Text = "添加第一级物料类别";
                RequiredFieldValidator4.Enabled = false;
                btnPre.Enabled = false;
                trEdit.Visible = false;
                trEdit1.Visible = false;
                lblAddTitle.Text = "添加一级物料";
                lblListTitle.Text = "一级物料列表";
            }
            ListBind();
        }
    }

    /// <summary>
    /// 根据页面参数展开相应模块物料和节点
    /// </summary>
    public void ShowTypeAndNode()
    {
        //根据参数展开相应树节点
        if (!string.IsNullOrEmpty(Request["vpath"]))
        {
            string vpath = Request["vpath"].ToString();
            TreeNode tn = new TreeNode();

            tn = stv0.FindNode(vpath);
            if (tn != null)
            {
                switch (LevelNum(vpath))
                {
                    case 1:
                        tn.Expand();
                        break;
                    case 2:
                        tn.Parent.Expand();
                        tn.Expand();
                        break;
                    case 3:
                        tn.Parent.Expand();
                        tn.Parent.Parent.Expand();
                        break;
                }
            }
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

    /// <summary>
    /// 树内容改变后重新绑定刷新
    /// </summary>
    public void flashTreeEsp()
    {
        stv0.Nodes.Clear();
        int n = 0;
        BindTree(stv0.Nodes, 0, ref n);
        stv0.CollapseAll();
        ShowTypeAndNode();
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
            tn = new TreeNode(dr["typename"].ToString(), dr["typeid"].ToString(), null, "?tid=" + dr["typeid"].ToString() + "&pid=" + parentId, "_self");
            tn.ShowCheckBox = false;
            n++;
            nds.Add(tn);
            string tmp = tn.ValuePath;
            tn.NavigateUrl = "?tid=" + dr["typeid"].ToString() + "&pid=" + parentId + "&vpath=" + tmp;
            BindTree(tn.ChildNodes, Convert.ToInt32(dr["typeid"]), ref n);
        }
    }

    /// <summary>
    /// 修改物料内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

                flashTreeEsp();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location=window.location;", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
            }
        }
    }

    /// <summary>
    /// 添加物料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        if (tid > 0)
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
            flashTreeEsp();
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加成功！');window.location=window.location;", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加失败！');", true);
        }
    }

    /// <summary>
    /// 添加物料关联
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave2_Click(object sender, EventArgs e)
    {
        int supplyid= Convert.ToInt32(this.hidSupplyTypeid.Value);
        string where = " and esptypeid=" + tid.ToString() + " and supplytypeid=" + supplyid.ToString();
        IList<ESPAndSupplyTypeRelationInfo> listtmp = ESPAndSupplyTypeRelationManager.GetList(where);

        if (listtmp!=null&&listtmp.Count>0)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('已有该条记录，请重新选择！');", true);
        }
        else
        {
            tr5.Visible = false;
            ESPAndSupplyTypeListBind(tid);
            ESPAndSupplyTypeRelationInfo espAndsupplyModel = new ESPAndSupplyTypeRelationInfo();
            espAndsupplyModel.SupplyTypeId = Convert.ToInt32(hidSupplyTypeid.Value);
            espAndsupplyModel.ESPTypeId = tid;
            espAndsupplyModel.CreatedDate = DateTime.Now;
            espAndsupplyModel.CreatedUserId = int.Parse(CurrentUser.SysID);
            espAndsupplyModel.TypeLevel = 3;
            if (ESPAndSupplyTypeRelationManager.Add(espAndsupplyModel) > 0)
            {
                tr5.Visible = false;
                ESPAndSupplyTypeListBind(tid);
                flashTreeEsp();
                //记录操作日志
                string esptypename = TypeManager.GetModel(espAndsupplyModel.ESPTypeId).typename;
                string supplytypename = new XML_VersionListManager().GetModel(espAndsupplyModel.SupplyTypeId).Name;
                ESP.Logging.Logger.Add(string.Format("{0}对采购物料 {1} 和供应链物料 {2} 的关联，数据完成 {3} 的操作", CurrentUser.Name, esptypename, supplytypename, "创建"), "物料类别维护");

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加成功！');window.location=window.location;", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加失败！');", true);
            }
        }
    }

    /// <summary>
    /// 绑定物料列表
    /// </summary>
    private void ListBind()
    {
        gvItems.DataSource = TypeManager.GetListForBase(tid);
        gvItems.DataBind();
    }

    /// <summary>
    /// 绑定物料关联列表
    /// </summary>
    /// <param name="esptypeid"></param>
    private void ESPAndSupplyTypeListBind(int esptypeid)
    {
        string where = " and esptypeid=" + esptypeid.ToString();
        gv1.DataSource = ESPAndSupplyTypeRelationManager.GetList(where);
        gv1.DataBind();
    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal litEspType = (Literal)e.Row.FindControl("litEspType");
            Literal litSupplyType = (Literal)e.Row.FindControl("litSupplyType");
            Literal litDel = (Literal)e.Row.FindControl("litDel");
            ESPAndSupplyTypeRelationInfo model = (ESPAndSupplyTypeRelationInfo)e.Row.DataItem;
            int esptypelevel3 = model.ESPTypeId;
            TypeInfo emodel = TypeManager.GetModel(esptypelevel3);
            if (emodel != null)
            {
                int esptypelevel2 = TypeManager.GetModel(esptypelevel3).parentId;
                int esptypelevel1 = TypeManager.GetModel(esptypelevel2).parentId;

                string esptypename1 = TypeManager.GetModel(esptypelevel1).typename;
                string esptypename2 = TypeManager.GetModel(esptypelevel2).typename;
                string esptypename3 = TypeManager.GetModel(esptypelevel3).typename;

                litEspType.Text = esptypename1 + " - " + esptypename2 + " - " + esptypename3;
            }

            int supplytypelevel3 = model.SupplyTypeId;
            XML_VersionList vmodel=new XML_VersionListManager().GetModel(supplytypelevel3);
            if (vmodel != null)
            {
                int supplytypelevel2 = new XML_VersionListManager().GetModel(supplytypelevel3).ClassID;
                int supplytypelevel1 = new XML_VersionClassManager().GetModel(supplytypelevel2).ParentID;

                string supplytypename1 = new XML_VersionClassManager().GetModel(supplytypelevel1).Name;
                string supplytypename2 = new XML_VersionClassManager().GetModel(supplytypelevel2).Name;
                string supplytypename3 = new XML_VersionListManager().GetModel(supplytypelevel3).Name;

                litSupplyType.Text = supplytypename1 + " - " + supplytypename2 + " - " + supplytypename3;
            }
        }
    }

    protected void gv1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DelLink")
        {
            //记录操作日志
            int esptypeid = ESPAndSupplyTypeRelationManager.GetModel(Convert.ToInt32(e.CommandArgument)).ESPTypeId;
            string esptypename = TypeManager.GetModel(esptypeid).typename;
            int supplytypeid = ESPAndSupplyTypeRelationManager.GetModel(Convert.ToInt32(e.CommandArgument)).SupplyTypeId;
            string supplytypename = new XML_VersionListManager().GetModel(supplytypeid).Name;
            ESP.Logging.Logger.Add(string.Format("{0}对采购物料 {1} 和供应链物料 {2} 的关联，数据完成 {3} 的操作", CurrentUser.Name, esptypename, supplytypename, "删除"), "物料类别维护");


            ESPAndSupplyTypeRelationManager.Delete(Convert.ToInt32(e.CommandArgument));
            ESPAndSupplyTypeListBind(tid);
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "", true);
        }
    }

    protected void Items_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TypeInfo model = (TypeInfo)e.Row.DataItem;


            Literal litEdit = (Literal)e.Row.FindControl("litEdit");
            string vpath = "";
            if (model.typelevel == 1)
            {
                vpath = model.typeid.ToString();
            }
            else if(model.typelevel==2)
            {
                vpath = model.parentId.ToString() + "/" + model.typeid.ToString();
            }
            else if (model.typelevel == 3)
            {
                vpath =Request["pid"]+"/"+ model.parentId.ToString() + "/" + model.typeid.ToString();
            }
            litEdit.Text = "<a href='ESPAndSupplyTypeInfo.aspx?vpath="+vpath+"&pid="+model.parentId+"&tid=" + model.typeid + "'><img src='../../images/edit.gif' border='0px;' title='编辑'></a>";
            if (model.status == State.typestatus_block)
            {
                e.Row.Cells[4].Controls.Clear();
                litEdit.Text = "";
            }
            else if (model.status == State.typestatus_used)
            {
                e.Row.Cells[5].Controls.Clear();
            }
        }
    }

    protected void Items_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string script = @"  window.location.href = window.location.href;";
        int typeid = int.Parse(gvItems.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
        if (e.CommandName == "Del")
        {
            TypeInfo typeModel = TypeManager.GetModel(typeid);
            TypeManager.BlockUpOrUse(typeid, typeModel.typelevel, State.typestatus_block);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对物料类别中的 id={1} 的数据完成 {2} 的操作", CurrentUser.Name, typeid.ToString(), "停用"), "物料类别维护");
            ListBind();
            script += "alert('停用成功！');";
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
        else if (e.CommandName == "Use")
        {
            TypeInfo typeModel = TypeManager.GetModel(typeid);
            TypeManager.BlockUpOrUse(typeid, typeModel.typelevel, State.typestatus_used);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对物料类别中的 id={1} 的数据完成 {2} 的操作", CurrentUser.Name, typeid.ToString(), "启用"), "物料类别维护");
            ListBind();
            script += "alert('启用成功！');";
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }

    /// <summary>
    /// 显示添加物料关联部分
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowAddLink_Click(object sender, EventArgs e)
    {
        tr5.Visible = true;
        txtEspTypeName.Text = TypeManager.GetModel(tid).typename;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("ESPAndSupplyTypeInfo.aspx?tid=" +Request["tid"]+ "&pid=" +Request["pid"]+ "&vpath="  +Request["vpath"]);
    }

    /// <summary>
    /// 返回上一级
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPre_Click(object sender, EventArgs e)
    {
        TypeInfo model = TypeManager.GetModel(pid);
        TypeInfo model1 = TypeManager.GetModel(tid);
        if (model1.typelevel == 1)
        {
            Response.Redirect("ESPAndSupplyTypeInfo.aspx?tid=0");
        }
        else if (model1.typelevel == 2)
        {
            Response.Redirect("ESPAndSupplyTypeInfo.aspx?vpath=" + pid + "&tid=" + pid + "&pid=" + (model == null ? 0 : model.parentId).ToString());
        }
        else if (model1.typelevel == 3)
        {
            Response.Redirect("ESPAndSupplyTypeInfo.aspx?vpath=" + model.parentId.ToString() + "/" + pid + "&tid=" + pid + "&pid=" + (model == null ? 0 : model.parentId).ToString());
        }      
    }
}

