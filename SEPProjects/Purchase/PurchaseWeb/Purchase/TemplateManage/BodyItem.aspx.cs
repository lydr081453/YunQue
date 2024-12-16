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
    public partial class BodyItem : System.Web.UI.Page
    {
        int productId = 0;
        public int mSite = 0;
        public int typeId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (null != Request["pid"] && !string.IsNullOrEmpty(Request["pid"]))
            {
                productId = int.Parse(Request["pid"].ToString());
            }

            if (null != Request["site"] && !string.IsNullOrEmpty(Request["site"]))
            {
                mSite = int.Parse(Request["site"].ToString());
            }

            if (null != Request["tid"] && !string.IsNullOrEmpty(Request["tid"]))
            {
                typeId = int.Parse(Request["tid"].ToString());
            }

            LoadPage();
            if (!IsPostBack)
            {
                txtName.Text = string.Empty;
                ddlType.SelectedValue = "1";
                BindInfo();
            }
        }


        private void LoadPage()
        {
            if (Session["headerTable"] != null)
            {
                DataTable dt = (DataTable)Session["headerTable"];
                list.DataSource = dt.DefaultView;
                list.DataBind();
            }
        }


        private void BindInfo()
        {
            if (productId > 0)
            {
                ddlType.Enabled = false;
                if (Session["bodyTable"] != null)
                {
                    lbTitle.Text = "修改";
                    DataTable dt = (DataTable)Session["bodyTable"];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow[] dr = dt.Select("id=" + productId.ToString());
                        switch (dr[0]["数量"].ToString())
                        {
                            case "-777":
                                {
                                    ddlType.SelectedValue = "3";
                                    //txtName.Text = dr[0]["单价"].ToString();
                                    ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "changeitem(3);", true);
                                }
                                break;
                            case "-888":
                                {
                                    ddlType.SelectedValue = "2";
                                    //txtName.Text = dr[0]["单价"].ToString();
                                    ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "changeitem(2);", true);
                                }
                                break;
                            case "-999":
                                {
                                    ddlType.SelectedValue = "1";
                                    txtName.Text = dr[0]["单价"].ToString();
                                    ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "changeitem(1);", true);
                                }
                                break;
                            default:
                                {
                                    ddlType.SelectedValue = "0";
                                    for (int i = 0; i < list.Rows.Count; i++)
                                    {
                                        HiddenField hName = (HiddenField)list.Rows[i].FindControl("hName");
                                        TextBox tb = (TextBox)list.Rows[i].FindControl(hName.Value);
                                        tb.Text = dr[0][i + 1].ToString();
                                    }
                                    ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), "changeitem(0);", true);
                                }
                                break;
                        }
                    }
                }
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["bodyTable"];

            switch (int.Parse(ddlType.SelectedValue))
            {
                case 0://新项目
                    InsertItem(ref dt);
                    break;
                case 1://新分类
                    InsertClass(ref dt);
                    break;
                case 2://税金
                    InsertTax(ref dt);
                    break;
                case 3://折扣
                    InsertDiscount(ref dt);
                    break;
            }
            Session["bodyTable"] = dt;

            //string script = "parent.document.getElementById('ctl00_ContentPlaceHolder1_hmID').value= '" + productId.ToString() + "';";
            //script += @" parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();parent.document.location.reload();";
            string script = " parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();parent.document.location.reload();";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
        }

        #region 插入数据项
        /// <summary>
        /// 插入分类
        /// </summary>
        /// <param name="dt"></param>
        private void InsertClass(ref DataTable dt)
        {
            if (productId > 0)
            {
                DataRow[] dr = dt.Select("id='" + productId + "'");

                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    switch (dt.Columns[i].Caption.ToLower())
                    {
                        case "单价":
                            dr[0][i] = txtName.Text.Trim();
                            break;
                        case "数量":
                            dr[0][i] = "-999";
                            break;
                    }
                }

            }
            else
            {
                DataRow dr = dt.NewRow();
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    switch (dt.Columns[i].Caption.ToLower())
                    {
                        case "单价":
                            dr[i] = txtName.Text.Trim();
                            break;
                        case "数量":
                            dr[i] = "-999";
                            break;
                    }
                }

                //数据插入位置
                int tSite = mSite;
                if (ddlSite.SelectedValue == "0")
                {
                    tSite = mSite + 1;
                }

                if (mSite < 0) mSite = 0;
                if (mSite >= dt.Rows.Count) mSite = dt.Rows.Count;

                dt.Rows.InsertAt(dr, tSite);
            }
        }

        /// <summary>
        /// 插入项目
        /// </summary>
        /// <param name="dt"></param>
        private void InsertItem(ref DataTable dt)
        {
            string str1 = "";
            //获取页面填写数据
            for (int i = 0; i < list.Rows.Count; i++)
            {
                HiddenField hName = (HiddenField)list.Rows[i].FindControl("hName");
                HiddenField hType = (HiddenField)list.Rows[i].FindControl("hType");
                Control ct = (Control)list.Rows[i].FindControl(hName.Value);

                string s = getControlValue(ct, hType.Value, list.Rows[i].Cells);
                str1 += "," + s;
            }

            str1 = str1.Substring(1);



            if (productId > 0)
            {
                DataRow[] dr = dt.Select("id='" + productId + "'");

                string[] str = str1.Split(',');
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    dr[0][i] = str[i - 1];
                }

            }
            else
            {
                string[] str = str1.Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    dr[i] = str[i - 1];
                }


                //数据插入位置
                int tSite = mSite;
                if (ddlSite.SelectedValue == "0")
                {
                    tSite = mSite + 1;
                }

                if (mSite < 0) mSite = 0;
                if (mSite >= dt.Rows.Count) mSite = dt.Rows.Count;

                dt.Rows.InsertAt(dr, tSite);
            }
        }

        /// <summary>
        /// 插入税金
        /// </summary>
        /// <param name="dt"></param>
        private void InsertTax(ref DataTable dt)
        {
            if (productId > 0)
            {
                DataRow[] dr = dt.Select("id='" + productId + "'");

                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    switch (dt.Columns[i].Caption.ToLower())
                    {
                        case "单价":
                            dr[0][i] = "0";
                            break;
                        case "数量":
                            dr[0][i] = "-888";
                            break;
                    }
                }

            }
            else
            {
                DataRow dr = dt.NewRow();
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    switch (dt.Columns[i].Caption.ToLower())
                    {
                        case "单价":
                            dr[i] = "0";
                            break;
                        case "数量":
                            dr[i] = "-888";
                            break;
                    }
                }

                //数据插入位置
                int tSite = mSite;
                if (ddlSite.SelectedValue == "0")
                {
                    tSite = mSite + 1;
                }

                if (mSite < 0) mSite = 0;
                if (mSite >= dt.Rows.Count) mSite = dt.Rows.Count;

                dt.Rows.InsertAt(dr, tSite);
            }
        }

        /// <summary>
        /// 插入折扣
        /// </summary>
        /// <param name="dt"></param>
        private void InsertDiscount(ref DataTable dt)
        {
            if (productId > 0)
            {
                DataRow[] dr = dt.Select("id='" + productId + "'");

                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    switch (dt.Columns[i].Caption.ToLower())
                    {
                        case "单价":
                            dr[0][i] = "0";
                            break;
                        case "数量":
                            dr[0][i] = "-777";
                            break;
                    }
                }

            }
            else
            {
                DataRow dr = dt.NewRow();
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    switch (dt.Columns[i].Caption.ToLower())
                    {
                        case "单价":
                            dr[i] = "0";
                            break;
                        case "数量":
                            dr[i] = "-777";
                            break;
                    }
                }

                //数据插入位置
                int tSite = mSite;
                if (ddlSite.SelectedValue == "0")
                {
                    tSite = mSite + 1;
                }

                if (mSite < 0) mSite = 0;
                if (mSite >= dt.Rows.Count) mSite = dt.Rows.Count;

                dt.Rows.InsertAt(dr, tSite);
            }
        }
        #endregion


        protected void list_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                HiddenField hType = (HiddenField)e.Row.FindControl("hType");
                HiddenField hName = (HiddenField)e.Row.FindControl("hName");
                HiddenField hUse = (HiddenField)e.Row.FindControl("hUse");

                if (hUse.Value == "1")
                {
                    e.Row.CssClass = "tdHide";
                }
                getControl(e.Row.Cells[1].Controls, hType.Value, hName.Value, hUse.Value);
            }
        }

        #region 组件操作

        /// <summary>
        /// 转换组件
        /// </summary>
        private void getControl(ControlCollection pan, string cType, string cID, string use)
        {
            switch (cType)
            {
                case "":
                    {
                        printTextBox(pan, cID, use);
                    }
                    break;
                case "integral":
                    {
                        printIntTextBox(pan, cID, use);
                    }
                    break;
                case "datetime":
                    {
                        printDateTime(pan, cID, use);
                    }
                    break;
                default:
                    {
                        printTextBox(pan, cID, use);
                    }
                    break;
            }

        }

        private void printTextBox(ControlCollection pan, string cID, string use)
        {
            TextBox tb = new TextBox();
            tb.ID = cID;
            tb.Width = Unit.Parse("250px");
            if (use == "1")
            {
                tb.ReadOnly = true;
                tb.Enabled = false;
            }
            pan.Add(tb);
        }

        private void printIntTextBox(ControlCollection pan, string cID, string use)
        {
            TextBox tb = new TextBox();
            tb.ID = cID;
            tb.Width = Unit.Parse("120px");
            if (use == "1")
            {
                tb.ReadOnly = true;
                tb.Enabled = false;
            }
            pan.Add(tb);
        }

        private void printDateTime(ControlCollection pan, string cID, string use)
        {
            TextBox tb = new TextBox();
            tb.ID = cID;
            if (use == "1")
            {
                tb.ReadOnly = true;
                tb.Enabled = false;
            }
            else
            {
                tb.Attributes.Add("onfocus", "this.blur();popUpCalendar(this, this, 'yyyy-mm-dd');");
            }
            pan.Add(tb);
        }

        /// <summary>
        /// 获取组件中的值
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="cType"></param>
        /// <returns></returns>
        private string getControlValue(Control ct, string cType, TableCellCollection pan)
        {
            string str = string.Empty;
            switch (cType)
            {
                case "":
                    {
                        TextBox obj = (TextBox)ct;
                        str = obj.Text;
                    }
                    break;
                case "integral":
                    {
                        TextBox obj = (TextBox)ct;
                        str = obj.Text;
                    }
                    break;
                case "datetime":
                    {
                        TextBox obj = (TextBox)ct;
                        str = obj.Text;
                    }
                    break;
                default:
                    {
                        TextBox obj = (TextBox)ct;
                        str = obj.Text;
                    }
                    break;
            }
            return str;
        }
        #endregion
    }
}
