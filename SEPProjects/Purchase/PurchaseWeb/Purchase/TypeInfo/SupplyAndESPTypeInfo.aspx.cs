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
using System.Text;
using System.IO;


public partial class SupplyAndESPTypeInfo : ESP.Web.UI.PageBase
{
    int tid = 0;//typeid,当前物料类别id
    int pid = 0;//parentid，父物料类别id
    int lvl = 0;//物料级别
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
        if (!string.IsNullOrEmpty(Request["lvl"]))
        {
            lvl = int.Parse(Request["lvl"]);
        }
        if (!IsPostBack)
        {
            int n = 0;
            BindTreeVersionClass(stv1.Nodes, 0, ref n);
            stv1.CollapseAll();
            ShowTypeAndNode();
            if (tid != 0)
            {
                if (lvl == State.producttype_l1)
                {
                    txtName.Text = new XML_VersionClassManager().GetModel(tid).Name;

                    tdAuditor1.Visible = false;
                    tdAuditor2.Visible = false;
                    trSaG.Visible = false;

                    RequiredFieldValidator2.Enabled = false;
                    RequiredFieldValidator4.Enabled = false;
                    labaudit1.Visible = false;
                    labaudit2.Visible = false;
                    tr4.Visible = false;
                    lblEditTitle.Text = "一级物料维护";
                    lblAddTitle.Text = "添加二级物料";
                    lblListTitle.Text = "二级物料列表";
                }
                else if (lvl == State.producttype_l2)
                {
                    txtName.Text = new XML_VersionClassManager().GetModel(tid).Name;

                    tdAuditor1.Visible = true;
                    tdAuditor2.Visible = true;
                    trSaG.Visible = true;
                    btnSelect1.Visible = true;
                    btnSelectSH1.Visible = true;
                    btnSelectGZ1.Visible = true;

                    RequiredFieldValidator2.Enabled = false;
                    labaudit1.Visible = false;
                    tr4.Visible = false;
                    lblEditTitle.Text = "二级物料维护";
                    lblAddTitle.Text = "添加三级物料";
                    lblListTitle.Text = "三级物料列表";
                }
                else if (lvl == State.producttype_l3)
                {
                    XML_VersionList vlistModel = new XML_VersionListManager().GetModel(tid);
                    txtName.Text = vlistModel.Name;

                    
                    if (vlistModel.BJAuditorId.ToString().Trim() != "")
                    {
                        txtAuditor.Text = new ESP.Compatible.Employee(vlistModel.BJAuditorId).Name;
                        hidAuditor.Value = vlistModel.BJAuditorId.ToString();
                        hidSHAuditor.Value=vlistModel.SHAuditorId.ToString();
                        hidGZAuditor.Value = vlistModel.GZAuditorId.ToString();
                    }
                    if (vlistModel.SHAuditorId.ToString().Trim() != "")
                    {
                        txtSHAuditor.Text = new ESP.Compatible.Employee(vlistModel.SHAuditorId).Name;
                    }
                    if (vlistModel.GZAuditorId.ToString().Trim() != "")
                    {
                        txtGZAuditor.Text = new ESP.Compatible.Employee(vlistModel.GZAuditorId).Name;
                    }

                    tdAuditor.Visible = true;
                    tdAuditor0.Visible = true;
                    trSaG0.Visible = true;
                    btnSelect.Visible = true;
                    btnSelectSH.Visible = true;
                    btnSelectGZ.Visible = true;

                    btnSave1.Enabled = false;
                    tr2.Visible = false;
                    tr3.Visible = false;
                    tr4.Visible = true;
                    ESPAndSupplyTypeListBind(tid);
                    lblEditTitle.Text = "三级物料维护";
                }
            }
            else
            {
                tab1.Visible = false;
                tr4.Visible = false;
                lab1.Text = "添加第一级物料类别";
                RequiredFieldValidator4.Enabled = false;
                trEdit.Visible = false;
                trEdit1.Visible = false;
                lblAddTitle.Text = "添加一级物料";
                lblListTitle.Text = "一级物料列表";
            }
            ListBind();
        }
        if (lvl == 0)
        {
            btnPre.Enabled = false;
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
            tn = stv1.FindNode(vpath);
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
        stv1.Nodes.Clear();
        int n = 0;
        BindTreeVersionClass(stv1.Nodes, 0, ref n);
        stv1.CollapseAll();
        ShowTypeAndNode();
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
            tn = new TreeNode(dr["name"].ToString(), dr["id"].ToString(), null, "?lvl=1&tid=" + dr["id"].ToString() + "&pid=" + parentId, "_self");
            tn.ShowCheckBox = false;
            n++;
            nds.Add(tn);
            string tmp = tn.ValuePath;
            tn.NavigateUrl = "?lvl=1&tid=" + dr["id"].ToString() + "&pid=" + parentId + "&vpath=" + tmp;
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
            tn = new TreeNode(dr["name"].ToString(), dr["id"].ToString(), null, "?lvl=2&tid=" + dr["id"].ToString() + "&pid=" + parentId, "_self");
            tn.ShowCheckBox = false;
            n++;
            nds.Add(tn);
            string tmp = tn.ValuePath;
            tn.NavigateUrl = "?lvl=2&tid=" + dr["id"].ToString() + "&pid=" + parentId + "&vpath=" + tmp;
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
            tn = new TreeNode(dr["name"].ToString(), dr["id"].ToString(), null, "?lvl=3&tid=" + dr["id"].ToString() + "&pid=" + parentId, "_self");
            tn.ShowCheckBox = false;
            n++;
            nds.Add(tn);
            string tmp = tn.ValuePath;
            tn.NavigateUrl = "?lvl=3&tid=" + dr["id"].ToString() + "&pid=" + parentId + "&vpath=" + tmp;
        }
    }

    /// <summary>
    /// 修改物料内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (lvl == 3)
        {
            XML_VersionList vlistmodel = new XML_VersionListManager().GetModel(tid);
            vlistmodel.Name = txtName.Text.Trim();
            vlistmodel.BJAuditorId = int.Parse(hidAuditor.Value);
            vlistmodel.SHAuditorId = int.Parse(hidSHAuditor.Value);
            vlistmodel.GZAuditorId = int.Parse(hidGZAuditor.Value);
            vlistmodel.UpdateUser = CurrentUser.Name;
            vlistmodel.UpdateTime = DateTime.Now.ToString();
            vlistmodel.UpdateIP = HttpContext.Current.Request.UserHostAddress;
            vlistmodel.BJAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(hidAuditor.Value)).Username;
            vlistmodel.SHAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(hidSHAuditor.Value)).Username; ;
            vlistmodel.GZAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(hidGZAuditor.Value)).Username;

            new XML_VersionListManager().Update(vlistmodel);
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对物料类别中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, tid.ToString(), "保存"), "物料类别维护");

            flashTreeEsp();
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location=window.location;", true);
        }
        else
        {
            XML_VersionClass vclassmodel = new XML_VersionClassManager().GetModel(tid);
            vclassmodel.Name = txtName.Text.Trim();

            new XML_VersionClassManager().Update(vclassmodel);
            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对物料类别中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, tid.ToString(), "保存"), "物料类别维护");

            flashTreeEsp();
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location=window.location;", true);
        }
    }

    /// <summary>
    /// 添加物料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        if (lvl == 2)
        {
            XML_VersionList vlistmodel = new XML_VersionList();
            vlistmodel.Name = txtName1.Text.Trim();
            vlistmodel.BJAuditorId = int.Parse(hidAuditor1.Value); 
            vlistmodel.SHAuditorId = int.Parse(hidSHAuditor1.Value);
            vlistmodel.GZAuditorId = int.Parse(hidGZAuditor1.Value);
            vlistmodel.ClassID = tid;

            vlistmodel.InsertUser = CurrentUser.Name;
            vlistmodel.InsertTime = DateTime.Now.ToString();
            vlistmodel.InsertIP = HttpContext.Current.Request.UserHostAddress;
            vlistmodel.UpdateUser = CurrentUser.Name;
            vlistmodel.UpdateTime = DateTime.Now.ToString();
            vlistmodel.UpdateIP = HttpContext.Current.Request.UserHostAddress;

            vlistmodel.BJAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(hidAuditor1.Value)).Username;
            vlistmodel.SHAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(hidSHAuditor1.Value)).Username; ;
            vlistmodel.GZAuditor = ESP.Framework.BusinessLogic.EmployeeManager.Get(int.Parse(hidGZAuditor1.Value)).Username; ;
            vlistmodel.Type = 0;
            vlistmodel.Status = 0;
            
            int vid=new XML_VersionListManager().Add(vlistmodel);
            if (vid > 0)
            {
                //写入xml值
                vlistmodel.XML = xml_Create(vid);
                vlistmodel.ID=vid;
                vlistmodel.TableName = "Z_Detail"+vid.ToString();
                vlistmodel.Version = "v1.0";
                new XML_VersionListManager().Update(vlistmodel);

                xml_Save(vlistmodel);

                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对物料类别中的 {1} 的数据完成 {2} 的操作", CurrentUser.Name, txtName1.Text.Trim(), "创建"), "物料类别维护");

                ListBind();

                flashTreeEsp();

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加成功！');window.location=window.location;", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加失败！');", true);
            }
        }
        else
        {
            XML_VersionClass vclassmodel = new XML_VersionClass();
            vclassmodel.Name = txtName1.Text.Trim();
            vclassmodel.ParentID = tid;
            vclassmodel.Level = lvl+1;

            if (new XML_VersionClassManager().Add(vclassmodel)> 0)
            {
                //记录操作日志
                ESP.Logging.Logger.Add(string.Format("{0}对物料类别中的 {1} 的数据完成 {2} 的操作", CurrentUser.Name, txtName1.Text.Trim(), "创建"), "物料类别维护");

                ListBind();

                flashTreeEsp();

                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加成功！');window.location=window.location;", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加失败！');", true);
            }
        }
    }

    /// <summary>
    /// 添加物料关联
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave2_Click(object sender, EventArgs e)
    {
        int esptypeid = Convert.ToInt32(this.hidESPTypeid.Value);
        string where = " and supplytypeid=" + tid.ToString() + " and esptypeid=" + esptypeid.ToString();
        IList<ESPAndSupplyTypeRelationInfo> listtmp = ESPAndSupplyTypeRelationManager.GetList(where);

        if (listtmp != null && listtmp.Count > 0)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('已有该条记录，请重新选择！');", true);
        }
        else
        {
            tr5.Visible = false;
            ESPAndSupplyTypeListBind(tid);
            ESPAndSupplyTypeRelationInfo espAndsupplyModel = new ESPAndSupplyTypeRelationInfo();
            espAndsupplyModel.SupplyTypeId =  tid;
            espAndsupplyModel.ESPTypeId = Convert.ToInt32(esptypeid);
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
        string where = "";
        if (lvl == 0)
        {
            where = " level=1";
            gvItems.DataSource = new XML_VersionClassManager().GetList(where);
            gvItems.Columns[1].Visible = false;
            gvItems.Columns[2].Visible = false;
        }
        else if (lvl == 1)
        {
            where = " level=2 and parentid="+tid;
            gvItems.DataSource = new XML_VersionClassManager().GetList(where);
            gvItems.Columns[1].Visible = false;
            gvItems.Columns[2].Visible = false;
        }
        else if (lvl == 2)
        {
            where = " classid="+tid;
            gvItems.DataSource = new XML_VersionListManager().GetList(where);
            gvItems.Columns[2].Visible = false;
        }
        
        gvItems.DataBind();
        
    }

    /// <summary>
    /// 绑定物料关联列表
    /// </summary>
    /// <param name="esptypeid"></param>
    private void ESPAndSupplyTypeListBind(int supplytypeid)
    {
        string where = " and supplytypeid=" + supplytypeid.ToString();
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
            XML_VersionList vmodel = new XML_VersionListManager().GetModel(supplytypelevel3);
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
            int esptypeid=ESPAndSupplyTypeRelationManager.GetModel(Convert.ToInt32(e.CommandArgument)).ESPTypeId;
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
            Literal litSupplyTypeName = (Literal)e.Row.FindControl("litSupplyTypeName");
            Literal litauditorid = (Literal)e.Row.FindControl("litauditorid");
            Literal litoperationflow = (Literal)e.Row.FindControl("litoperationflow");
            Literal litEdit = (Literal)e.Row.FindControl("litEdit");

            DataRowView dv = (DataRowView)e.Row.DataItem;
            litSupplyTypeName.Text = dv["Name"].ToString();
            string nextlvl ="";
            string vpath = "";
            string nextpid = "";
            string nexttid = "";

            if (lvl ==0)
            {
                nextlvl = "1";
                vpath = dv["id"].ToString();
                nextpid = dv["parentid"].ToString();
                nexttid = dv["id"].ToString();
                XML_VersionClass tmpModel = new XML_VersionClassManager().GetModel(int.Parse(dv["id"].ToString()));
                if (tmpModel.State == State.typestatus_block)
                {
                    e.Row.Cells[4].Controls.Clear();
                    litEdit.Text = "";
                }
                else if (tmpModel.State == State.typestatus_used)
                {
                    e.Row.Cells[5].Controls.Clear();
                    litEdit.Text = "<a href='SupplyAndESPTypeInfo.aspx?lvl=" + nextlvl + "&tid=" + nexttid + "&pid=" + nextpid + "&vpath=" + vpath + "'><img src='../../images/edit.gif' border='0px;' title='编辑'></a>";
                }
            }
            else if(lvl==1)
            {
                nextlvl = "2";
                vpath = dv["parentid"].ToString()+"/"+dv["id"].ToString();
                nextpid = dv["parentid"].ToString();
                nexttid = dv["id"].ToString();
                XML_VersionClass tmpModel = new XML_VersionClassManager().GetModel(int.Parse(dv["id"].ToString()));
                if (tmpModel.State == State.typestatus_block)
                {
                    e.Row.Cells[4].Controls.Clear();
                    litEdit.Text = "";
                }
                else if (tmpModel.State == State.typestatus_used)
                {
                    e.Row.Cells[5].Controls.Clear();
                    litEdit.Text = "<a href='SupplyAndESPTypeInfo.aspx?lvl=" + nextlvl + "&tid=" + nexttid + "&pid=" + nextpid + "&vpath=" + vpath + "'><img src='../../images/edit.gif' border='0px;' title='编辑'></a>";
                }
            }
            else if (lvl == 2)
            {
                nextlvl = "3";
                vpath = pid.ToString()+"/"+tid.ToString()+ "/" + dv["id"].ToString();
                nextpid = dv["classid"].ToString();
                nexttid = dv["id"].ToString();

                litauditorid.Text = new ESP.Compatible.Employee(int.Parse(dv["BJAuditorId"].ToString())).Name;

                XML_VersionList tmpModel = new XML_VersionListManager().GetModel(int.Parse(dv["id"].ToString()));
                if (tmpModel.State == State.typestatus_block)
                {
                    e.Row.Cells[4].Controls.Clear();
                    litEdit.Text = "";
                }
                else if (tmpModel.State == State.typestatus_used)
                {
                    e.Row.Cells[5].Controls.Clear();
                    litEdit.Text = "<a href='SupplyAndESPTypeInfo.aspx?lvl=" + nextlvl + "&tid=" + nexttid + "&pid=" + nextpid + "&vpath=" + vpath + "'><img src='../../images/edit.gif' border='0px;' title='编辑'></a>";
                }
            }
            

            //if (nextlvl == "1" || nextlvl == "2")
            //{
            //    if (int.Parse(dv["state"].ToString()) == State.typestatus_block)
            //    {
            //        e.Row.Cells[4].Controls.Clear();
            //    }
            //    else if (int.Parse(dv["state"].ToString()) == State.typestatus_used)
            //    {
            //        e.Row.Cells[5].Controls.Clear();
            //    }
            //}
            //if (nextlvl == "3")
            //{
            //    if (int.Parse(dv["state"].ToString()) == State.typestatus_block)
            //    {
            //        e.Row.Cells[4].Controls.Clear();
            //    }
            //    else if (int.Parse(dv["state"].ToString()) == State.typestatus_used)
            //    {
            //        e.Row.Cells[5].Controls.Clear();
            //    }
            //}
            

        }
    }

    protected void Items_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string script = @"  window.location.href = window.location.href;";
        int typeid = int.Parse(gvItems.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
        if (e.CommandName == "Del")
        {
            new XML_VersionListManager().BlockUpOrUse(typeid, lvl+1, State.typestatus_block);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对供应链物料类别中的{1}级 id={2} 的数据完成 {3} 的操作", CurrentUser.Name,lvl.ToString(), typeid.ToString(), "停用"), "物料类别维护");
            ListBind();
            script += "alert('停用成功！');";
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), script, true);

        }
        else if (e.CommandName == "Use")
        {
            new XML_VersionListManager().BlockUpOrUse(typeid, lvl+1, State.typestatus_used);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对供应链物料别中的{1}级 id={2} 的数据完成 {3} 的操作", CurrentUser.Name,lvl.ToString(), typeid.ToString(), "启用"), "物料类别维护");
            ListBind();
            script += "alert('启用成功！');";
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),script, true);
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
        //txtEspTypeName.Text = TypeManager.GetModel(tid).typename;
        txtSupplyTypeName.Text = new XML_VersionListManager().GetModel(tid).Name;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("SupplyAndESPTypeInfo.aspx?tid=" + Request["tid"] + "&pid=" + Request["pid"] + "&vpath=" + Request["vpath"]);
    }

    /// <summary>
    /// 返回上一级
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPre_Click(object sender, EventArgs e)
    {
        if (lvl == 1)
        {
            Response.Redirect("SupplyAndESPTypeInfo.aspx?lvl=0&tid=0");
        }
        else if (lvl == 2)
        {
            int ppid = new XML_VersionClassManager().GetModel(pid).ParentID;
            Response.Redirect("SupplyAndESPTypeInfo.aspx?lvl=1&vpath=" + pid + "&tid=" + pid + "&pid=0");
        }
        else if (lvl == 3)
        {
            int ppid = new XML_VersionClassManager().GetModel(pid).ParentID;
            Response.Redirect("SupplyAndESPTypeInfo.aspx?lvl=2&vpath=" +ppid + "/" + pid + "&tid=" + pid + "&pid=" + ppid);
        }
    }


    #region 生成VersionList表数据

    #region 预生成字段
    private List<DataLib.Model.TableModel> getDefaultTable()
    {
        List<DataLib.Model.TableModel> modellist = new List<DataLib.Model.TableModel>();
        //常规报价属性

        //名称
        DataLib.Model.TableModel m1 = new DataLib.Model.TableModel();
        m1.ID = "Type";
        m1.cnDescription = "名称";
        m1.enDescription = "Name";
        m1.Type = "文本";
        m1.Length = 3000;
        m1.Control = "TextBox";
        m1.Option = "None";
        modellist.Add(m1);

        //描述
        DataLib.Model.TableModel m2 = new DataLib.Model.TableModel();
        m2.ID = "Description";
        m2.cnDescription = "描述";
        m2.enDescription = "Description";
        m2.Type = "文本";
        m2.Length = 5000;
        m2.Control = "TextBox";
        m2.Option = "None";
        modellist.Add(m2);

        //单位
        DataLib.Model.TableModel m3 = new DataLib.Model.TableModel();
        m3.ID = "Unit";
        m3.cnDescription = "单位";
        m3.enDescription = "Unit";
        m3.Type = "文本";
        m3.Length = 3000;
        m3.Control = "TextBox";
        m3.Option = "None";
        modellist.Add(m3);

        //单价
        DataLib.Model.TableModel m4 = new DataLib.Model.TableModel();
        m4.ID = "Price";
        m4.cnDescription = "单价";
        m4.enDescription = "Price";
        m4.Type = "数字";
        m4.Length = 10;
        m4.Control = "TextBox";
        m4.Option = "None";
        modellist.Add(m4);

        //起始时间
        DataLib.Model.TableModel m5 = new DataLib.Model.TableModel();
        m5.ID = "BeginTime";
        m5.cnDescription = "起始时间";
        m5.enDescription = "BeginTime";
        m5.Type = "文本";
        m5.Length = 3000;
        m5.Control = "DateTime";
        m5.Option = "None";
        modellist.Add(m5);

        //结束时间
        DataLib.Model.TableModel m6 = new DataLib.Model.TableModel();
        m6.ID = "EndTime";
        m6.cnDescription = "结束时间";
        m6.enDescription = "EndTime";
        m6.Type = "文本";
        m6.Length = 3000;
        m6.Control = "DateTime";
        m6.Option = "None";
        modellist.Add(m6);

        //发货周期
        DataLib.Model.TableModel m7 = new DataLib.Model.TableModel();
        m7.ID = "PayDays";
        m7.cnDescription = "发货周期";
        m7.enDescription = "PayDays";
        m7.Type = "文本";
        m7.Length = 3000;
        m7.Control = "TextBox";
        m7.Option = "None";
        modellist.Add(m7);

        return modellist;
    }
    #endregion

    #region 保存为XML
    private string xml_Create(int id)
    {
        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><Table Name=\"{1}\" Version=\"{2}\">{0}</Table>";
        string str = "<Frame ID=\"{1}\" cnDescription=\"{2}\" enDescription=\"{3}\" Type=\"{4}\" Length=\"{5}\" Control=\"{6}\" Option=\"{7}\">{0}</Frame>";
        StringBuilder sb = new StringBuilder();

        List<DataLib.Model.TableModel> mlist = getDefaultTable();
        for (int i = 0; i < mlist.Count; i++)
        {
            DataLib.Model.TableModel m = mlist[i];
            sb.Append(string.Format(str, "", m.ID, m.cnDescription, m.enDescription, m.Type, m.Length, m.Control, m.Option));
        }


        xml = string.Format(xml, sb.ToString(), "Z_Detail"+id.ToString(), "v1.0");
        return xml;
    }

 
    #endregion


    #region 根据XML生成表

    /// <summary>
    /// 保存信息
    /// </summary>
    private void xml_Save(XML_VersionList info)
    {
        DataLib.BLL.VersionList bll = new DataLib.BLL.VersionList();
        DataLib.Model.VersionList model = new DataLib.Model.VersionList();
        model.ClassID = info.ClassID;
        model.Name = info.Name;
        model.TableName = info.TableName;
        model.Content = info.Content;
        model.Version = info.Version;
        model.XML = info.XML;
        model.InsertIP = info.InsertIP;
        model.InsertTime = info.InsertTime;
        model.InsertUser = info.InsertUser;
        model.UpdateIP = info.UpdateIP;
        model.UpdateTime = info.UpdateTime;
        model.UpdateUser = info.UpdateUser;
        model.Url = info.ID.ToString().Trim() + ".xml";

        if (CreateTable(model.XML, model.TableName, model.Version))
        {
            //int id = bll.Add(model);
            int id = info.ID;
            if (id > 0)
            {
                AddLog(id, model);
            }
        }

    }



    /// <summary>
    /// 保存历史记录
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    private void AddLog(int id, DataLib.Model.VersionList model)
    {
        //保存为历史记录
        DataLib.BLL.VersionLog logDll = new DataLib.BLL.VersionLog();
        DataLib.Model.VersionLog logModel = new DataLib.Model.VersionLog();
        logModel.VersionID = id;
        logModel.ClassID = model.ClassID;
        logModel.Name = model.Name;
        logModel.TableName = model.TableName;
        logModel.Content = model.Content;
        logModel.Version = model.Version;
        logModel.XML = model.XML;
        logModel.InsertIP = model.InsertIP;
        logModel.InsertTime = model.InsertTime;
        logModel.InsertUser = model.InsertUser;
        logModel.UpdateIP = model.UpdateIP;
        logModel.UpdateTime = model.UpdateTime;
        logModel.UpdateUser = model.UpdateUser;
        logDll.Add(logModel);
    }


    #region 生成数据表
    /// <summary>
    /// 根据XML生成全部表单
    /// </summary>
    /// <param name="xmlStr"></param>
    private bool CreateTable(string xmlStr, string TableName, string Version)
    {
        List<DataLib.Model.TableModel> iList = new DataLib.BLL.TableManage().LoadXML(xmlStr, TableName);
        if (iList != null)
        {
            new DataLib.BLL.TableManage().Save(TableName, iList);
            return true;

        }
        else
        {
            return false;
        }
    }
    #endregion

    #endregion

    #endregion

}