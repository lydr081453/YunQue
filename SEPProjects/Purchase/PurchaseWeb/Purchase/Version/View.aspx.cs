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
    public partial class View : System.Web.UI.Page
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
                Session.Remove("TableModel");
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
            DataLib.Model.VersionList tableModel = new DataLib.BLL.VersionList().GetModel(typeId);
            List<DataLib.Model.TableModel> iList = new DataLib.BLL.TableManage().LoadXML(tableModel.XML, tableModel.TableName);

            gvList.DataSource = iList;
            gvList.DataBind();

        }



        public string getModelType(string t, string l)
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


        public string getUseType(string s)
        {
            string str = "否";
            if (s == "1")
            {
                str = "<font color=red>是</font>";
            }
            return str;
        }

        private string getDateName(string name)
        {
            string str = DateTime.Now.ToString();
            str = str.Replace("-", "").Replace(":", "").Replace(" ", "").Replace("/", "");
            str = name + "_" + str;

            return str;
        }

        public string getOption(string v,string idstr,string indexstr)
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




    }
}
