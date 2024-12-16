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
    public partial class Body : ESP.Web.UI.PageBase
    {
        public int typeId = 0;
        public int modelId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (null != Request["tid"] && !string.IsNullOrEmpty(Request["tid"]))
            {
                typeId = int.Parse(Request["tid"].ToString());
            }

            if (!string.IsNullOrEmpty(Request["mid"]))
            {
                modelId = int.Parse(Request["mid"]);
            }

            if (!IsPostBack)
            {
                if (typeId > 0)
                {
                    DataLib.Model.VersionList model = new DataLib.BLL.VersionList().GetModel(typeId);
                    txtBatchName.Text = model.Name;
                }

                PageBind();
            }


        }


        private void PageBind()
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


                //数据填充
                if (bodyTable.Rows.Count > 0)
                {
                    list = dynamicGenerateColumns(list, bodyTable);
                    list.DataSource = bodyTable.DefaultView;
                    list.DataBind();

                }
                else
                {
                    DataTable mdt = bodyTable.Clone();
                    mdt.Rows.Add(mdt.NewRow());

                    list = dynamicGenerateColumns(list, mdt);
                    list.DataSource = mdt.DefaultView;
                    list.DataBind();

                    int columnCount = list.Rows[0].Cells.Count;
                    list.Rows[0].Cells.Clear();
                    list.Rows[0].Cells.Add(new TableCell());
                    list.Rows[0].Cells[0].ColumnSpan = columnCount;
                    list.Rows[0].Cells[0].Text = "目前还没有报价项，请<a href='javascript:void(0);' onclick=\"showTempleteData(0,0)\" class='linkadd'>添加报价项</a>。";
                    list.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                }
            }

        }


        public GridView dynamicGenerateColumns(GridView gv, DataTable dt)
        {
            gv.AutoGenerateColumns = false;

            // 清空所有的Columns
            //gv.Columns.Clear();
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                if (i > 0)
                {
                    gv.Columns.RemoveAt(i);
                }
            }
            foreach (DataColumn item in dt.Columns)
            {
                if (item.ColumnName.ToLower() != "id")
                {
                    BoundField col = new BoundField();
                    col.HeaderText = item.ColumnName;
                    col.DataField = item.ColumnName;
                    col.Visible = true;
                    gv.Columns.Add(col);
                }
            }


            return gv;
        }



        /// <summary>
        /// 删除产品报价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            ImageButton lnkDel = (ImageButton)sender;
            int pid = int.Parse(lnkDel.CommandArgument.ToString());
            DataTable dt = (DataTable)Session["bodyTable"];

            DataRow[] dr = dt.Select("ID=" + pid.ToString());
            dt.Rows.Remove(dr[0]);
            Session["bodyTable"] = dt;
            ClientScript.RegisterStartupScript(typeof(string), "", "location=location;", true);
        }

        /// <summary>
        /// 删除选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDelAll_Click(object sender, EventArgs e)
        {
            Button lnkDel = (Button)sender;
            DataTable dt = (DataTable)Session["bodyTable"];
            for (int i = 0; i < list.Rows.Count; i++)
            {
                CheckBox ck = (CheckBox)list.Rows[i].FindControl("ckbox");
                Label mid = (Label)list.Rows[i].FindControl("mId");
                if (ck.Checked)
                {
                    DataRow[] dr = dt.Select("ID=" + mid.Text);
                    dt.Rows.Remove(dr[0]);
                    Session["bodyTable"] = dt;
                }
            }

            ClientScript.RegisterStartupScript(typeof(string), "", "location=location;", true);
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

            DataTable dt = (DataTable)Session["bodyTable"];
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

            Session["bodyTable"] = dt;

            ClientScript.RegisterStartupScript(typeof(string), "", "location=location;", true);


        }

        protected void list_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                int priceSite = 0;
                int countSite = 0;
                //SC_EnquiryTemplateManager.getPCSite(modelId, ref priceSite, ref countSite);
                SC_EnquiryTemplateManager.getPCSite((DataTable)Session["headerTable"], ref priceSite, ref countSite);

                string pstr = ((DataRowView)e.Row.DataItem).Row.ItemArray[priceSite + 1].ToString();
                string cstr = ((DataRowView)e.Row.DataItem).Row.ItemArray[countSite + 1].ToString();

                int columnCount = e.Row.Cells.Count;


                switch (cstr)
                {
                    case "-777"://折扣
                        {
                            TableCell tc = e.Row.Cells[0];
                            tc.CssClass = "agvItem51";

                            e.Row.Cells.Clear();
                            e.Row.Cells.Add(tc);
                            e.Row.Cells.Add(new TableCell());
                            e.Row.Cells[1].ColumnSpan = columnCount - 1;
                            e.Row.Cells[1].Text = "折扣";
                            e.Row.Cells[1].CssClass = "agvItem5";
                        }
                        break;
                    case "-888"://税率
                        {
                            TableCell tc = e.Row.Cells[0];
                            tc.CssClass = "agvItem61";

                            e.Row.Cells.Clear();
                            e.Row.Cells.Add(tc);
                            e.Row.Cells.Add(new TableCell());
                            e.Row.Cells[1].ColumnSpan = columnCount - 1;
                            e.Row.Cells[1].Text = "税率(%)";
                            e.Row.Cells[1].CssClass = "agvItem6";
                        }
                        break;
                    case "-999"://分类
                        {
                            TableCell tc = e.Row.Cells[0];
                            tc.CssClass = "agvItem21";

                            e.Row.Cells.Clear();
                            e.Row.Cells.Add(tc);
                            e.Row.Cells.Add(new TableCell());
                            e.Row.Cells[1].ColumnSpan = columnCount - 1;
                            e.Row.Cells[1].Text = pstr;
                            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                            e.Row.Cells[1].CssClass = "agvItem2";
                        }
                        break;
                    default://项目
                        {
                            List<int> ml = SC_EnquiryTemplateManager.getSupplierInput((DataTable)Session["headerTable"]);
                            foreach (int i in ml)
                            {
                                e.Row.Cells[i + 1].Text = "<img src='/public/images/icon/lock_edit.png' title='此项由供应商填写' style='cursor:pointer;'>";
                            }
                        }
                        break;
                }
            }
        }


        protected void lnkLink1_Click(object sender, EventArgs e)
        {
            Response.Redirect("View.aspx?tid="+typeId.ToString()+"&mid="+modelId.ToString());

        }

        #region 保存报价单
        private void SaveEnquieyPage()
        {
            SC_EnquiryTemplate etModel = new SC_EnquiryTemplate();
            #region 生成表头
            string str = "";
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
                str = str.Substring(1);
                int oldtid = modelId;

                etModel = SC_EnquiryTemplateManager.GetModel(modelId);
                etModel.Xml = str;
                etModel.UpdateTime = DateTime.Now;
                SC_EnquiryTemplateManager.Update(etModel);
            }
            #endregion

            #region 生成报价项
            if (modelId > 0)
            {
                SC_EnquiryListManager.DeleteByTemplateID(modelId);//删除之前的旧数据
            }

            dt = (DataTable)Session["bodyTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str = "";
                    for (int j = 1; j < dt.Columns.Count; j++)
                    {
                        str += "," + dt.Rows[i][j].ToString();
                    }

                    str = str.Substring(1);


                    SC_EnquiryList model = new SC_EnquiryList();
                    model.Note = str;
                    model.TemplateID = modelId;
                    model.TypeID = typeId;
                    model.UserID = etModel.UserID;
                    model.CreateTime = DateTime.Now;
                    model.PEID = i + 1;
                    SC_EnquiryListManager.Add(model);
                }
            }
            #endregion

            Session.Remove("headerTable");
            Session.Remove("bodyTable");

        }

        #endregion

    }
}
