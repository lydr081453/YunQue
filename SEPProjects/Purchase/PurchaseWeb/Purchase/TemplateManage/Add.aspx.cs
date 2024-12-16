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
    public partial class Add : ESP.Web.UI.PageBase
    {
        string script = string.Empty;
        private IList<SC_VendeeTypeRelation> listRelation = null;

        private string idsRelation = "0";
        private string ids1 = "0";
        private string ids2 = "0";
        private string ids3 = "0";
        public int TypeID = 0;
        public int Level = 0;
        private int[] level1ids = null;
        private int[] level2ids = null;
        private int[] level3ids = null;

        public int IsUse = 0;
        public int typeId2 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (null != Request["use"] && !string.IsNullOrEmpty(Request["use"]))
            {
                IsUse = int.Parse(Request["use"].ToString());
            }

            if (null != Request["cid"] && !string.IsNullOrEmpty(Request["cid"]))
            {
                typeId2 = int.Parse(Request["cid"].ToString());
            }
            if (!IsPostBack)
            {
                LoadPage();
            }
        }

        private void LoadPage()
        {
            if (null != Request["tid"] && !string.IsNullOrEmpty(Request["tid"]))
            {
                TypeID = int.Parse(Request["tid"].ToString());
            }

            if (null != Request["level"] && !string.IsNullOrEmpty(Request["level"]))
            {
                Level = int.Parse(Request["level"].ToString());
            }

            DataSet ds = new DataLib.BLL.VersionClass().SelectByParentID(0);
            if (ds.Tables.Count > 0)
            {
                list.DataSource = ds.Tables[0].DefaultView;
                list.DataBind();
            }
        }


        /// <summary>
        /// 二级物料
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataSet getDataSource(string pid)
        {
            DataLib.BLL.VersionClass bll = new DataLib.BLL.VersionClass();
            DataSet ds = bll.GetList(" ParentID=" + pid);
            return ds;
        }

        /// <summary>
        /// 三级物料
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataSet getDataSource1(string cid)
        {
            DataLib.BLL.VersionList bll = new DataLib.BLL.VersionList();
            DataSet ds = bll.GetList(" ClassID=" + cid);
            return ds;
        }

        /// <summary>
        /// 选择三级物料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Event"></param>
        protected void lnkDel_Click(object sender, EventArgs Event)
        {
            Button lnkDel = (Button)sender;
            Response.Redirect("View.aspx?tid=" + lnkDel.CommandArgument.ToString() + "&mid=0");
        }

    }
}
