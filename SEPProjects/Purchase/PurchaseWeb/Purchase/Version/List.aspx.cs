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

namespace PurchaseWeb.Purchase.Version
{
    public partial class List : System.Web.UI.Page
    {
        public int typeId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (null != Request["tid"] && !string.IsNullOrEmpty(Request["tid"]))
            {
                typeId = int.Parse(Request["tid"].ToString());
            }

            if (!IsPostBack)
            {
                LoadPage();
            }

        }

        private void LoadPage()
        {
            if (typeId > 0)
            {
                DataLib.Model.VersionList vl = new DataLib.BLL.VersionList().GetModel(typeId);
                txtBatchName.Text = vl.Name;
                BindList();
            }
        }


        private void BindList()
        {
            if (Session["TableModel"] == null)
            {
                DataLib.Model.VersionList tableModel = new DataLib.BLL.VersionList().GetModel(typeId);
                List<DataLib.Model.TableModel> iList = new DataLib.BLL.TableManage().LoadXML(tableModel.XML, tableModel.TableName);
                Session["TableModel"] = iList;
            }

            gvList.DataSource = (List<DataLib.Model.TableModel>)Session["TableModel"];
            gvList.DataBind();

        }


        /// <summary>
        /// 移动项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Event"></param>
        protected void btn_Move(object sender, EventArgs Event)
        {
            ImageButton lnkDel = (ImageButton)sender;
            int site = int.Parse(lnkDel.CommandArgument.ToString());
            string type = lnkDel.CommandName;

            DataTable dt = (DataTable)Session["TableModel"];
            var dv = dt.Rows[site].ItemArray;

            //数据插入位置
            int mSite = 0;
            switch (type)
            {
                case "up":
                    {
                        mSite = site - 1;
                    }
                    break;
                case "down":
                    mSite = site + 1;
                    break;
            }

            if (mSite < 0) mSite = 0;
            if (mSite >= dt.Rows.Count) mSite = dt.Rows.Count - 1;

            var dv2 = dt.Rows[mSite].ItemArray;

            dt.Rows[site].ItemArray = dv2;
            dt.Rows[mSite].ItemArray = dv;

            Session["TableModel"] = dt;

            ClientScript.RegisterStartupScript(typeof(string), "", "location=location;", true);

        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            ImageButton lnkDel = (ImageButton)sender;
            string name = lnkDel.CommandArgument.ToString();

            List<DataLib.Model.TableModel> modellist = (List<DataLib.Model.TableModel>)Session["TableModel"];
            DataLib.Model.TableModel model = modellist.Find(new Predicate<DataLib.Model.TableModel>(delegate(DataLib.Model.TableModel x)
            {
                return x.ID == name;
            }));


            modellist.Remove(model);

            Session["TableModel"] = modellist;
            ClientScript.RegisterStartupScript(typeof(string), "", "window.location=window.location;", true);
        }


        public string getModelType(string t,string l)
        {
            string str = "";
            if (t == "文本")
            {
                if (int.Parse(l) < 5000)
                {
                    str = "普通文字";
                }
                else
                {
                    str = "大文本";
                }
            }
            else
            {
                str = "数字";
            }
            return str;
        }

        public string getControls(string c)
        {
            string str = "";
            switch (c)
            {
                case "TextBox":
                    str = "文本框";
                    break;
                case "FileUpload":
                    str = "上传文件框";
                    break;
                case "CheckBox":
                    str = "选择框";
                    break;
                case "DropDownList":
                    str = "下拉菜单";
                    break;
                case "RadioButtonList":
                    str = "单选框";
                    break;
                case "CheckBoxList":
                    str = "多选框";
                    break;
                case "DateTime":
                    str = "日期选择框";
                    break;
                default:
                    str = "文本框";
                    break;

            }
            return str;
        }

        public string getUse(string s)
        {
            string str = s;

            return str;
        }

        public string getOption(string v, string idstr, string indexstr)
        {
            string str = "无";
            switch (v)
            {
                case "Single":
                    str = "单选";
                    break;
                case "Multiple":
                    str = "多选";
                    break;
                default:
                    str = "无";
                    break;
            }
            if (str != "无")
            {
                str = "<a href='javascript:void(0);' onclick=\"showSelectOption('" + idstr + "'," + indexstr + ");\" class='optionstyle'>" + str + "</a>";
            }

            return str;
        }



        protected void gvList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                DropDownList gvType = (DropDownList)e.Item.FindControl("gvType");
                RadioButtonList gvUse = (RadioButtonList)e.Item.FindControl("gvUse");

                if (gvType != null && gvUse != null)
                {
                    DataRow dr = ((DataRowView)e.Item.DataItem).Row;
                    gvType.SelectedValue = dr["Type"].ToString();
                    gvUse.SelectedValue = dr["Use"].ToString();
                }

            }
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {

            Response.Redirect("View.aspx?tid="+typeId.ToString());
        }
    }
}
