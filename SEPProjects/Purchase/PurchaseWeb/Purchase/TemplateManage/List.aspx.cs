using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;
using System.Text;

namespace PurchaseWeb.Purchase.TemplateManage
{
    public partial class List : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new DataLib.BLL.VersionClass().getClass(ddlType);
                ddlType.Items.RemoveAt(0);
                ddlType.Items.Insert(0, new ListItem("全部", "0"));
                ddlLevel3.Items.Insert(0, new ListItem("全部", "0"));
                ListBind();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<DataLib.Model.VersionList> mList = new DataLib.BLL.VersionList().GetModelList(" ClassId=" + ddlType.SelectedValue);
            ddlLevel3.DataSource = mList;
            ddlLevel3.DataBind();
            ddlLevel3.Items.Insert(0, new ListItem("全部", "0"));
            ListBind();
        }

        private void ListBind()
        {
            int cid = int.Parse(ddlType.SelectedValue);
            int lid = int.Parse(ddlLevel3.SelectedValue);

            List<SC_EnquiryTemplate> pList = null;
            string whereSql = " UserId=0";
            if (cid > 0)
            {
                string str = "";
                if (lid > 0)
                {
                    str = ","+lid.ToString();
                }
                else
                {
                    //检测物料级别
                    DataLib.Model.VersionClass cModel = new DataLib.BLL.VersionClass().GetModel(cid);
                    if (cModel.ParentID > 0)
                    {
                        //二级 获取二级物料下所有的三级物料
                        List<DataLib.Model.VersionList> hlist = new DataLib.BLL.VersionList().GetModelList(" ClassId=" + cid.ToString());
                        foreach (DataLib.Model.VersionList model in hlist)
                        {
                            str += "," + model.ID.ToString();
                        }

                    }
                    else
                    {
                        //一级 获取全部二级物料下的三级物料
                        List<DataLib.Model.VersionClass> tlist = new DataLib.BLL.VersionClass().GetModelList(" ParentId=" + cid.ToString());
                        foreach (DataLib.Model.VersionClass pmodel in tlist)
                        {
                            List<DataLib.Model.VersionList> hlist = new DataLib.BLL.VersionList().GetModelList(" ClassId=" + pmodel.ID.ToString());
                            foreach (DataLib.Model.VersionList model in hlist)
                            {
                                str += "," + model.ID.ToString();
                            }
                        }

                    }
                }
                str = str.Substring(1);
                whereSql += string.Format(" and TypeId in ({0})", str);
            }

            pList = (List<SC_EnquiryTemplate>)SC_EnquiryTemplateManager.GetModelList(whereSql);

            pList.Sort((x, y) => y.ID - x.ID);
            gvList.DataSource = pList;
            gvList.DataBind();

        }


        public string GetTypeName(string tid)
        {
            string str = new DataLib.BLL.VersionList().GetList(" ID=" + tid).Tables[0].Rows[0]["Name"].ToString();
            return str;
        }


        /// <summary>
        /// 删除产品报价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDel_Click(object sender, EventArgs Event)
        {
            ImageButton lnkDel = (ImageButton)sender;
            int id = int.Parse(lnkDel.CommandArgument.ToString());
            SC_EnquiryTemplate model = SC_EnquiryTemplateManager.GetModel(id);
            model.IsDelete = 1;
            SC_EnquiryTemplateManager.Update(model);
            ClientScript.RegisterStartupScript(typeof(string), "", "location=location;", true);
        }


        /// <summary>
        /// 引用报价单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkInclude_Click(object sender, EventArgs e)
        {
            ImageButton lnkDel = (ImageButton)sender;
            int id = int.Parse(lnkDel.CommandArgument.ToString());
            SC_EnquiryTemplate model = SC_EnquiryTemplateManager.GetModel(id);

            Session.Remove("headerTable");
            Session.Remove("bodyTable");
            Session["hEnquiryId"] = model.ID.ToString();
        }

        protected void LinkPageChanged(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;

            if (lb.CommandArgument == "p")
            {
                if (this.gvList.PageIndex > 0) this.gvList.PageIndex--;
            }
            else if (lb.CommandArgument == "n")
            {
                this.gvList.PageIndex++;
            }
            ListBind();
        }

        protected void GridView1_DataBound(object sender, EventArgs e) //控件被数据绑定后触发
        {
            List<SC_EnquiryTemplate> pList = (List<SC_EnquiryTemplate>)gvList.DataSource;

            if (pList.Count > 0)
            {

                Literal li = (Literal)gvList.BottomPagerRow.Cells[0].FindControl("pageTotal");
                li.Text = pList.Count.ToString();

                DropDownList ddl = (DropDownList)gvList.BottomPagerRow.Cells[0].FindControl("ddl");
                for (int i = 0; i < gvList.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem((i + 1).ToString(), i.ToString()));
                }

                ddl.SelectedIndex = this.gvList.PageIndex;
            }
            
        }

        protected void DropPageChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;

            this.gvList.PageIndex = int.Parse(ddl.SelectedValue);
            ListBind();

        }

        protected void ddlLevel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBind();
        }
 


    }
}
