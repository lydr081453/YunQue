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

namespace PurchaseWeb.Purchase.TemplateManage
{
    public partial class Header : ESP.Web.UI.PageBase
    {
        public int typeId = 0;
        int modelId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (null != Request["tid"] && !string.IsNullOrEmpty(Request["tid"]))
            {
                typeId = int.Parse(Request["tid"].ToString());
            }

            if (null != Request["mid"] && !string.IsNullOrEmpty(Request["mid"]))
            {
                modelId = int.Parse(Request["mid"].ToString());
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
                lbModelName.Text = vl.Name;
            }

            BindList();
        }


        private void BindList()
        {
            DataTable headerTable = new DataTable();
            DataTable bodyTable = new DataTable();

            //初始化表头
            if (Session["HeaderTable"] != null)
            {
                headerTable = (DataTable)Session["HeaderTable"];
            }

            //初始化数据
            if (Session["bodyTable"] != null)
            {
                bodyTable = (DataTable)Session["bodyTable"];
            }

            if (headerTable != null && bodyTable != null)
            {

                gvList.DataSource = headerTable.DefaultView;
                gvList.DataBind();
            }

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

            DataTable dt = (DataTable)Session["headerTable"];
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

            Session["headerTable"] = dt;

            ClientScript.RegisterStartupScript(typeof(string), "", "location=location;", true);

        }

        /// <summary>
        /// 删除产品报价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            ImageButton lnkDel = (ImageButton)sender;
            string name = lnkDel.CommandArgument.ToString();
            DataTable dt = (DataTable)Session["headerTable"];
            DataRow[] dr = dt.Select("Name='" + name + "'");

            dt.Rows.Remove(dr[0]);

            Session["headerTable"] = dt;
            ClientScript.RegisterStartupScript(typeof(string), "", "window.location=window.location;", true);
        }


        public string getModelType(string str)
        {
            string instr = "文字型";
            switch (str)
            {
                case "":
                    instr = "文字型";
                    break;
                case "datetime":
                    instr = "日期型";
                    break;
                case "integral":
                    instr = "数字型";
                    break;
                default:
                    instr = "文字型";
                    break;
            }
            return instr;
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

        protected void lnkLink_Click1(object sender, EventArgs e)
        {
            Button lnkLink = (Button)sender;

            string str = "";
            int PEID = 0;
            bool mb = false;
            bool mb1 = false;
            DataTable dt = (DataTable)Session["headerTable"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string s = dt.Rows[i]["Name"].ToString() + "|" + dt.Rows[i]["Type"].ToString() + "|" + dt.Rows[i]["Use"].ToString();
                str += "," + s;

                if (dt.Rows[i]["Name"].ToString() == "单价") mb = true;
                if (dt.Rows[i]["Name"].ToString() == "数量") mb1 = true;
            }

            if (!mb || !mb1)
            {
                Response.Write("<script>alert('当前报价模版中缺少“单价”或“数量”项。请检查并添加后再提交。');history.back();</script>");
            }
            else
            {
                CreateNewTable();

                Response.Redirect("Body.aspx?tid=" + typeId.ToString() + "&mid=" + modelId.ToString());

            }
        }

        /// <summary>
        /// 根据调整后的报价单属性，重新布局每个报价项
        /// </summary>
        private void CreateNewTable()
        {
            if (Session["headerTable"] != null)
            {
                DataTable oTable = (DataTable)Session["headerTable"];
                DataTable dt = (DataTable)Session["bodyTable"];

                DataTable nTable = new DataTable();

                DataColumn colNumber = new DataColumn("ID");
                colNumber.AutoIncrement = true;//设置是否为自增列
                colNumber.AutoIncrementSeed = 1;//设置自增初始值
                colNumber.AutoIncrementStep = 1;//设置每次子增值
                nTable.Columns.Add(colNumber);

                for (int i = 0; i < oTable.Rows.Count; i++)
                {
                    nTable.Columns.Add(oTable.Rows[i]["Name"].ToString());
                }

                if (dt.Rows.Count > 0)
                {
                    //比对原、新报价模版属性，并按表头名称向新表添加数据项
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = nTable.NewRow();

                        for (int j = 1; j < nTable.Columns.Count; j++)
                        {

                            for (int a = 1; a < dt.Columns.Count; a++)
                            {
                                if (nTable.Columns[j].Caption == dt.Columns[a].Caption)
                                {
                                    dr[j] = dt.Rows[i][a].ToString();
                                    break;
                                }

                            }
                        }

                        nTable.Rows.Add(dr);
                    }

                }

                Session["bodyTable"] = nTable;
            }

        }
    }
}
