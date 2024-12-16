using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Entity;

public partial class VendeeDepartmentTypeRelationEdit : ESP.Web.UI.PageBase
{
    public int did;
    public string dname;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                did = int.Parse(Request["did"].ToString());
                labdname.Text = getDname(did);
                labdid.Text = did.ToString();
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
                IList<ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation> list = ESP.Supplier.BusinessLogic.SC_VendeeDepartmentTypeRelationManager.GetListByDepartmentId(did);
                foreach (ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation lst in list)
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
                gvEload();
            }
            // a.status not in(5,6) and j.departmentid=
            //ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList()
        }

        private void gvEload()
        {
            //IList<ESP.Framework.Entity.EmployeeInfo> elist = ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeesByDepartment(Convert.ToInt32(labdid.Text));
            IList<ESP.HumanResource.Entity.EmployeeBaseInfo> ebi = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(" and a.status not in(5,6) and j.groupid=" + Convert.ToInt32(labdid.Text));
            gv.DataSource = ebi;
            gv.DataBind();
        }
        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label labuname = (Label)e.Row.FindControl("labuname");
                ESP.Framework.Entity.EmployeeInfo umodel = (ESP.Framework.Entity.EmployeeInfo)e.Row.DataItem;
                int uid = umodel.UserID;
                string uname = umodel.FullNameCN;
                labuname.Text = uname;

                Label labtype = (Label)e.Row.FindControl("labtype");
                IList<ESP.Supplier.Entity.SC_VendeeTypeRelation> ulist = ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.GetListByUserId(uid);
                IList<ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation> dlist = ESP.Supplier.BusinessLogic.SC_VendeeDepartmentTypeRelationManager.GetListByDepartmentId(did);
                string typeNames = string.Empty;
                foreach (ESP.Supplier.Entity.SC_VendeeTypeRelation lst in ulist)
                {
                    int noD = 0;
                    foreach (ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation dlst in dlist)
                    {
                        if (lst.TypeId != dlst.TypeId)
                        {
                            noD = 0;
                        }
                        else
                        {
                            noD = 1; break;
                        }
                    }
                    if (noD == 0)
                    {
                        //ESP.Supplier.Entity.XML_VersionList stpye = new ESP.Supplier.BusinessLogic.XML_VersionListManager().GetModel(lst.TypeId);
                        ESP.Supplier.Entity.XML_VersionClass stpye = new ESP.Supplier.BusinessLogic.XML_VersionClassManager().GetModel(lst.TypeId);
                        if (stpye != null)
                        {
                            typeNames += stpye.Name + ",";
                        }
                    }

                }
                if (typeNames != string.Empty)
                    typeNames = typeNames.Substring(0, typeNames.Length - 1);
                labtype.Text = typeNames;
                Literal litView = (Literal)e.Row.FindControl("litView");
                litView.Text = "<a href='VendeeTypeRelationEdit.aspx?uid=" + uid + "&did="+did+"'><img src='../../images/dc.gif' border='0px;' title='查看'></a>";
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
            for(int i=0;i<count;i++)
            {
                    ListItem li = lbox.Items[i];
                   if(lbox.Items[i].Selected==true)
                   {
                        rbox.Items.Add(lbox.SelectedItem);
                        lbox.Items.Remove(lbox.SelectedItem);
                        i--;  count--;
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
            did = int.Parse(labdid.Text);
            IList<ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation> list = ESP.Supplier.BusinessLogic.SC_VendeeDepartmentTypeRelationManager.GetListByDepartmentId(did);
            ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation vdtRel =new ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation();
            ESP.Supplier.Entity.SC_VendeeTypeRelation vtRel = new ESP.Supplier.Entity.SC_VendeeTypeRelation();
            //IList<ESP.Framework.Entity.EmployeeInfo> elist = ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeesByDepartment(did);
            //IList<ESP.HumanResource.Entity.EmployeeBaseInfo> elist = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModelList(" and a.status not in(5,6) and j.groupid=" + did);


            if(list.Count>0)
            {
                foreach(ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation lst in list) 
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
                            x=1;
                        }
                    }
                    if (rbox.Items.Count == 0)
                    {               
                        //foreach (ESP.Framework.Entity.EmployeeInfo elst in elist)
                        //{
                        //    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.DeleteByCompanySystemUserID(elst.UserID,lst.TypeId);
                        //}
                        ESP.Supplier.BusinessLogic.SC_VendeeDepartmentTypeRelationManager.Delete(did);
                    }
                    if (x == 1) //删除数据
                    {
                        //foreach (ESP.Framework.Entity.EmployeeInfo elst in elist)
                        //{
                        //    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.DeleteByCompanySystemUserID(elst.UserID, lst.TypeId);
                        //}
                        ESP.Supplier.BusinessLogic.SC_VendeeDepartmentTypeRelationManager.DeleteByCompanyDepartmentID(did, lst.TypeId);
                    }
                }

                foreach(ListItem ltr in rbox.Items) 
                {
                    int x = 0;
                    foreach (ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation lst in list)
                    {
                        if (ltr.Value == lst.TypeId.ToString())
                        {
                            x = 0;break;
                        }
                        else
                        {
                            x = 1;
                        }
                    }
                    if (x == 1) //更新新数据
                    {
                        vdtRel.CompanyDepartmentID = did.ToString();
                        vdtRel.TypeId = Convert.ToInt32(ltr.Value);
                        vdtRel.CreatTime = DateTime.Now;
                        vdtRel.LastUpdateTime = DateTime.Now;
                        vdtRel.CreatIP = HttpContext.Current.Request.UserHostAddress;
                        vdtRel.LastUpdateIP = HttpContext.Current.Request.UserHostAddress;
                        ESP.Supplier.BusinessLogic.SC_VendeeDepartmentTypeRelationManager.Add(vdtRel);

                        //foreach (ESP.Framework.Entity.EmployeeInfo lst in elist)
                        //{
                        //    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.DeleteByCompanySystemUserID(lst.UserID, Convert.ToInt32(ltr.Value));
                        //}
                        
                    }
                }
            }
            else //添加数据
            {
                foreach(ListItem ltr in rbox.Items)
                {
                    vdtRel.CompanyDepartmentID = did.ToString();
                    vdtRel.TypeId = Convert.ToInt32( ltr.Value);
                    vdtRel.CreatTime = DateTime.Now;
                    vdtRel.LastUpdateTime = DateTime.Now;
                    vdtRel.CreatIP = HttpContext.Current.Request.UserHostAddress;
                    vdtRel.LastUpdateIP = HttpContext.Current.Request.UserHostAddress;
                    ESP.Supplier.BusinessLogic.SC_VendeeDepartmentTypeRelationManager.Add(vdtRel);

                    //foreach (ESP.Framework.Entity.EmployeeInfo lst in elist)
                    //{
                    //    ESP.Supplier.BusinessLogic.SC_VendeeTypeRelationManager.DeleteByCompanySystemUserID(lst.UserID, Convert.ToInt32(ltr.Value));
                    //}
                    
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
            gvEload();
            //Response.Write("<script>alert('保存成功！')</script>");
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存成功!');", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("VendeeDepartmentTypeRelation.aspx");
        }

        protected void lbox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void rbox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private string getDname(int did)
        {
            ESP.Framework.Entity.DepartmentInfo dmodel= ESP.Framework.BusinessLogic.DepartmentManager.Get(did);
            string dname=dmodel.DepartmentName;
            return dname;
        }
        private string getUname(int uid)
        {
            string uname = "";
            return uname;
        }
}

