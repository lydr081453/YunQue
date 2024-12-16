using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Entity;


public partial class VendeeTypeRelationEdit : ESP.Web.UI.PageBase
{
    public int did;
    public string dname;
    public int uid;
    public string uname;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            did = int.Parse(Request["did"].ToString());
            uid = int.Parse(Request["uid"].ToString());
            labuname.Text = getUname(uid);
            labdname.Text = getDname(did);
            labdid.Text = did.ToString();
            labuid.Text = uid.ToString();
            //左列表全部物料名
            string typWhere = "level=2 and parentid!=99999";
            //IList<ESP.Supplier.Entity.XML_VersionList> ltype = new ESP.Supplier.BusinessLogic.XML_VersionListManager().GetModelList(typWhere);
            IList<ESP.Supplier.Entity.XML_VersionClass> ltype = new ESP.Supplier.BusinessLogic.XML_VersionClassManager().GetModelList(typWhere);

            lbox.DataTextField = "Name";
            lbox.DataValueField = "ID";
            lbox.DataSource = ltype;
            lbox.DataBind();
            lbox.SelectionMode = ListSelectionMode.Multiple;

            //右列表Relation中物料名，同时去除左列表中同名
            rbox.DataTextField = "Name";
            rbox.DataValueField = "ID";
            rbox.SelectionMode = ListSelectionMode.Multiple;
            IList<ESP.Supplier.Entity.SC_VendeeTypeRelation> list = ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.GetListByUserId(uid);
            foreach (ESP.Supplier.Entity.SC_VendeeTypeRelation lst in list)
            {
                //ESP.Supplier.Entity.XML_VersionList stpye = new ESP.Supplier.BusinessLogic.XML_VersionListManager().GetModel(lst.TypeId);
                ESP.Supplier.Entity.XML_VersionClass stpye = new ESP.Supplier.BusinessLogic.XML_VersionClassManager().GetModel(lst.TypeId);
                if (stpye != null)
                {
                    ListItem Item = new ListItem();
                    Item.Text = stpye.Name;
                    Item.Value = stpye.ID.ToString();
                    rbox.Items.Add(Item);
                    lbox.Items.Remove(Item);
                }
            }

            dbox.DataTextField = "Name";
            dbox.DataValueField = "ID";
            dbox.SelectionMode = ListSelectionMode.Multiple;
            IList<ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation> dlist = ESP.Supplier.BusinessLogic.SC_VendeeDepartmentTypeRelationManager.GetListByDepartmentId(did);
            foreach (ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation lst in dlist)
            {
                //ESP.Supplier.Entity.XML_VersionList stpye = new ESP.Supplier.BusinessLogic.XML_VersionListManager().GetModel(lst.TypeId);
                ESP.Supplier.Entity.XML_VersionClass stpye = new ESP.Supplier.BusinessLogic.XML_VersionClassManager().GetModel(lst.TypeId);
                if (stpye != null)
                {
                    ListItem Item = new ListItem();
                    Item.Text = stpye.Name;
                    Item.Value = stpye.ID.ToString();
                    dbox.Items.Add(Item);
                    lbox.Items.Remove(Item);
                    rbox.Items.Remove(Item);
                    dbox.BorderWidth = 0;
                }
            }
        } 
    }
    protected void addType_Click(object sender, EventArgs e)
    {
        //if (lbox.SelectedValue != "")
        //{
        //    rbox.Items.Add(lbox.SelectedItem);
        //    lbox.Items.Remove(lbox.SelectedItem);
        //}
        int count = lbox.Items.Count;
        for (int i = 0; i < count; i++)
        {
            ListItem li = lbox.Items[i];
            if (lbox.Items[i].Selected == true)
            {
                rbox.Items.Add(lbox.SelectedItem);
                lbox.Items.Remove(lbox.SelectedItem);
                i--; count--;
            }
        }
    }

    protected void cancelType_Click(object sender, EventArgs e)
    {
        //if (rbox.SelectedValue != "")
        //{
        //    lbox.Items.Add(rbox.SelectedItem);
        //    rbox.Items.Remove(rbox.SelectedItem);
        //}
        int count = rbox.Items.Count;
        for (int i = 0; i < count; i++)
        {
            ListItem li = rbox.Items[i];
            if (rbox.Items[i].Selected == true)
            {
                lbox.Items.Add(rbox.SelectedItem);
                rbox.Items.Remove(rbox.SelectedItem);
                i--; count--;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        uid = int.Parse(labuid.Text);
        did = int.Parse(labdid.Text);
        IList<ESP.Supplier.Entity.SC_VendeeTypeRelation> list = ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.GetListByUserId(uid);
        ESP.Supplier.Entity.SC_VendeeTypeRelation vdtRel = new ESP.Supplier.Entity.SC_VendeeTypeRelation();
        IList<ESP.Framework.Entity.EmployeeInfo> elist = ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeesByDepartment(did);

        if (list.Count > 0)
        {
            foreach (ESP.Supplier.Entity.SC_VendeeTypeRelation lst in list)
            {
                int x = 0;
                foreach (ListItem ltr in rbox.Items)
                {
                    if (lst.TypeId.ToString() == ltr.Value)
                    {
                        x = 0;
                        break;
                    }
                    else
                    {
                        x = 1;
                    }
                }
                if (rbox.Items.Count == 0)
                {
                    //foreach (ESP.Framework.Entity.EmployeeInfo elst in elist)
                    //{
                    //    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.DeleteByCompanySystemUserID(elst.UserID,lst.TypeId);
                    //}
                    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.Delete(uid);
                }
                if (x == 1) //删除数据
                {
                    //foreach (ESP.Framework.Entity.EmployeeInfo elst in elist)
                    //{
                    //    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.DeleteByCompanySystemUserID(elst.UserID, lst.TypeId);
                    //}
                    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.DeleteByCompanySystemUserID(uid, lst.TypeId);
                }
            }

            foreach (ListItem ltr in rbox.Items)
            {
                int x = 0;
                foreach (ESP.Supplier.Entity.SC_VendeeTypeRelation lst in list)
                {
                    if (ltr.Value == lst.TypeId.ToString())
                    {
                        x = 0; break;
                    }
                    else
                    {
                        x = 1;
                    }
                }
                if (x == 1) //更新新数据
                {
                    vdtRel.CompanySystemUserID = uid.ToString();
                    vdtRel.TypeId = Convert.ToInt32(ltr.Value);
                    vdtRel.CreatTime = DateTime.Now;
                    vdtRel.LastUpdateTime = DateTime.Now;
                    vdtRel.CreatIP = HttpContext.Current.Request.UserHostAddress;
                    vdtRel.LastUpdateIP = HttpContext.Current.Request.UserHostAddress;
                    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.Add(vdtRel);
                    
                    //foreach (ESP.Framework.Entity.EmployeeInfo lst in elist)
                    //{
                    //    vtRel.CompanySystemUserID = lst.UserID.ToString();
                    //    vtRel.TypeId = Convert.ToInt32(ltr.Value);
                    //    vtRel.CreatTime = DateTime.Now;
                    //    vtRel.LastUpdateTime = DateTime.Now;
                    //    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.Add(vtRel);
                    //}
                }
            }
        }
        else //添加数据
        {
            foreach (ListItem ltr in rbox.Items)
            {
                vdtRel.CompanySystemUserID = uid.ToString();
                vdtRel.TypeId = Convert.ToInt32(ltr.Value);
                vdtRel.CreatTime = DateTime.Now;
                vdtRel.LastUpdateTime = DateTime.Now;
                vdtRel.CreatIP = HttpContext.Current.Request.UserHostAddress;
                vdtRel.LastUpdateIP = HttpContext.Current.Request.UserHostAddress;
                ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.Add(vdtRel);
                
                //foreach (ESP.Framework.Entity.EmployeeInfo lst in elist)
                //{
                //    vtRel.CompanySystemUserID = lst.UserID.ToString();
                //    vtRel.TypeId = Convert.ToInt32(ltr.Value);
                //    vtRel.CreatTime = DateTime.Now;
                //    vtRel.LastUpdateTime = DateTime.Now;
                //    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.Add(vtRel);
                //}
            }
        }
        //Response.Write("<script>alert('保存成功！')</script>");
        ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功!');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("VendeeDepartmentTypeRelationEdit.aspx?did="+labdid.Text);
    }
    private string getDname(int did)
    {
        ESP.Framework.Entity.DepartmentInfo dmodel = ESP.Framework.BusinessLogic.DepartmentManager.Get(did);
        string dname = dmodel.DepartmentName;
        return dname;
    }
    private string getUname(int uid)
    {
        ESP.Framework.Entity.EmployeeInfo emodel = ESP.Framework.BusinessLogic.EmployeeManager.Get(uid);
        string uname = emodel.FullNameCN;
        return uname;
    }
}

