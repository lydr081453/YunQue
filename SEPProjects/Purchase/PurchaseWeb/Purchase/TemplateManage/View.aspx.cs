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
    public partial class View : ESP.Web.UI.PageBase
    {
        public int typeId = 0;
        public int modelId = 0;
        private int IsEdit = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            #region 传入数据
            if (null != Request["tid"] && !string.IsNullOrEmpty(Request["tid"]))
            {
                typeId = int.Parse(Request["tid"].ToString());
            }

            if (null != Request["mid"] && !string.IsNullOrEmpty(Request["mid"]))
            {
                modelId = int.Parse(Request["mid"].ToString());
            }

            if (null != Request["new"] && !string.IsNullOrEmpty(Request["new"]))
            {
                IsEdit = int.Parse(Request["new"].ToString());
            }

            #endregion

            if (!IsPostBack)
            {
                if (IsEdit>0)
                {
                    Session.Remove("HeaderTable");
                    Session.Remove("BodyTable");
                }

                LoadPage();
            }
        }

        private void LoadPage()
        {

            #region 设置页面信息

            if (modelId > 0)
            {
                SC_EnquiryTemplate et = SC_EnquiryTemplateManager.GetModel(modelId);
                txtModelName.Text = et.Name;
                if (Session["headerTable"] == null && Session["bodyTable"] == null)
                {
                    LoadDataInfo();
                }
                typeId = et.TypeID;
            }

            DataLib.Model.VersionList model = new DataLib.BLL.VersionList().GetModel(typeId);
            string strName = "[ " + model.Name + " ] 报价单模版";
            txtBatchName.Text = strName;

            

            PageBind();

            #endregion

            #region MyRegion

            #endregion
        }

        /// <summary>
        /// 读取指定模版
        /// </summary>
        private void LoadDataInfo()
        {
            #region 设置表头
            //定义表头
            DataTable headerTable = new DataTable();
            headerTable.Columns.Add("Name");
            headerTable.Columns.Add("Type");
            headerTable.Columns.Add("Use");

            //定义原报价模版表
            DataTable bodyTable = new DataTable();
            DataColumn colNumber = new DataColumn("ID");
            colNumber.AutoIncrement = true;//设置是否为自增列
            colNumber.AutoIncrementSeed = 1;//设置自增初始值
            colNumber.AutoIncrementStep = 1;//设置每次子增值
            bodyTable.Columns.Add(colNumber);

            //填充表头数据
            SC_EnquiryTemplate et = SC_EnquiryTemplateManager.GetModel(modelId);
            string[] str = et.Xml.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                string[] p = str[i].Split('|');
                DataRow dr = headerTable.NewRow();
                dr["Name"] = p[0];
                dr["Type"] = p[1];
                dr["Use"] = p[2];
                headerTable.Rows.Add(dr);

                bodyTable.Columns.Add(p[0]);//设置报价项表属性
            }
            Session["HeaderTable"] = headerTable;
            #endregion

            #region 设置报价项
            IList<SC_EnquiryList> mlist = SC_EnquiryListManager.GetListByTemplateID(modelId);
            if (mlist.Count > 0)
            {
                foreach (SC_EnquiryList model in mlist)
                {
                    string[] str1 = model.Note.Split(',');
                    DataRow dr = bodyTable.NewRow();


                    for (int j = 0; j < str1.Length; j++)
                    {
                        dr[j + 1] = str1[j];
                    }
                    bodyTable.Rows.Add(dr);
                }
            }
            Session["bodyTable"] = bodyTable;
            #endregion
        }

        /// <summary>
        /// 初始模版
        /// </summary>
        private void PageBind()
        {
            DataTable headerTable = new DataTable();
            DataTable bodyTable = new DataTable();

            //初始化表头
            if (Session["HeaderTable"] == null)
            {
                headerTable = SC_EnquiryTemplateManager.getHeaderTable(typeId);

            }
            else
            {
                headerTable = (DataTable)Session["HeaderTable"];
            }
            Session["HeaderTable"] = headerTable;


            //初始化数据
            if (Session["bodyTable"] == null)
            {
                bodyTable = SC_EnquiryTemplateManager.getBodyTable(headerTable, typeId);
            }
            else
            {
                bodyTable = (DataTable)Session["bodyTable"];
            }
            Session["bodyTable"] = bodyTable;


            //数据填充
            if (bodyTable.Rows.Count > 0)
            {
                list = dynamicGenerateColumns(list, bodyTable);
                list.DataSource = bodyTable.DefaultView;
                list.DataBind();
                lnkLink.Visible = true;
            }
            else
            {
                DataTable mdt = bodyTable.Clone();

                DataRow mdr = mdt.NewRow();


                for (int j = 0; j < mdt.Columns.Count; j++)
                {
                    for (int i = 0; i < headerTable.Rows.Count; i++)
                    {
                        if (mdt.Columns[j].Caption.ToLower() == headerTable.Rows[i]["Name"].ToString().ToLower())
                        {
                            mdr[j] = getModelType(headerTable.Rows[i]["Type"].ToString());
                        }

                    }
                }

                mdt.Rows.Add(mdr);

                list = dynamicGenerateColumns(list, mdt);
                list.DataSource = mdt.DefaultView;
                list.DataBind();
            }

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

        #region 数据读取

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

        private string getDateName(string name)
        {
            string str = DateTime.Now.ToString();
            str = str.Replace("-", "").Replace(":", "").Replace(" ", "").Replace("/", "");
            str = name + "_" + str;

            return str;
        }

        protected void list_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                int priceSite = 0;
                int countSite = 0;
                SC_EnquiryTemplateManager.getPCSite((DataTable)Session["headerTable"], ref priceSite, ref countSite);

                string pstr = ((DataRowView)e.Row.DataItem).Row.ItemArray[priceSite + 1].ToString();
                string cstr = ((DataRowView)e.Row.DataItem).Row.ItemArray[countSite + 1].ToString();

                switch (cstr)
                {
                    case "-777"://折扣
                        {
                            int columnCount = e.Row.Cells.Count;
                            e.Row.Cells.Clear();
                            e.Row.Cells.Add(new TableCell());
                            e.Row.Cells[0].ColumnSpan = columnCount;
                            e.Row.Cells[0].Text = "折扣";
                            e.Row.Cells[0].CssClass = "agvItem5";

                        }
                        break;
                    case "-888"://税金
                        {
                            int columnCount = e.Row.Cells.Count;
                            e.Row.Cells.Clear();
                            e.Row.Cells.Add(new TableCell());
                            e.Row.Cells[0].ColumnSpan = columnCount;
                            e.Row.Cells[0].Text = "税金(%)";
                            e.Row.Cells[0].CssClass = "agvItem6";

                        }
                        break;
                    case "-999"://分类
                        {
                            int columnCount = e.Row.Cells.Count;
                            e.Row.Cells.Clear();
                            e.Row.Cells.Add(new TableCell());
                            e.Row.Cells[0].ColumnSpan = columnCount;
                            e.Row.Cells[0].Text = pstr;
                            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                            e.Row.Cells[0].CssClass = "agvItem2";
                        }
                        break;
                    default://项目
                        {
                            List<int> ml = SC_EnquiryTemplateManager.getSupplierInput((DataTable)Session["headerTable"]);
                            foreach (int i in ml)
                            {
                                e.Row.Cells[i].Text = "<img src='/public/images/icon/lock_edit.png' title='此项由供应商填写' style='cursor:pointer;'>";
                            }
                        }
                        break;
                }
            }
        }
        #endregion

        #region 按钮操

        protected void lnkLink_Click(object sender, EventArgs e)
        {
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

                if (modelId > 0)
                {
                    SC_EnquiryTemplate model = SC_EnquiryTemplateManager.GetModel(modelId);
                    model.Name = txtModelName.Text.Trim();
                    model.Xml = str;
                    model.UpdateTime = DateTime.Now;
                    SC_EnquiryTemplateManager.Update(model);
                }
                else
                {
                    SC_EnquiryTemplate model = new SC_EnquiryTemplate();
                    model.Name = txtModelName.Text.Trim();
                    model.TypeID = typeId;
                    model.UserID = 0;
                    model.Xml = str;
                    model.CreateTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;
                    model.MessageId = CurrentUserID;
                    modelId = SC_EnquiryTemplateManager.Add(model);
                }
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
                    model.UserID = 0;
                    model.CreateTime = DateTime.Now;
                    model.PEID = i + 1;
                    model.MessageId = CurrentUserID;
                    SC_EnquiryListManager.Add(model);
                }
            }
            #endregion


            Session.Remove("headerTable");
            Session.Remove("bodyTable");

            string script = " parent.$('#floatBoxBg').hide();parent.$('#floatBox').hide();alert('报价单保存成功！');parent.document.location='List.aspx'";
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), script, true);
        }


        #endregion

    }
}
