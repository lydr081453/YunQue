using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdministrativeWeb.UserControls
{
    public partial class ddlDepartments : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Init_DefaultList();
                
            }
        }

        public bool Enabled {
            set
            {
                ddlDept3.Enabled = ddlDept2.Enabled = ddlDept1.Enabled = value;
            }
        }

        public int Level1
        {
            get { return int.Parse(ddlDept1.SelectedItem.Value); }
        }

        public int Level2
        {
            get { return int.Parse(ddlDept2.SelectedItem.Value); }
        }

        public int Level3
        {
            get { return int.Parse(ddlDept3.SelectedItem.Value); }
        }

        public string Level1Name
        {
            get { return ddlDept1.SelectedItem.Text; }
        }

        public string Level2Name
        {
            get { return ddlDept2.SelectedItem.Text; }
        }

        public string Level3Name
        {
            get { return ddlDept3.SelectedItem.Text; }
        }

        public void BindByLevel3Id(int level3Id)
        {
            var dept3 = DepartmentManager.Get(level3Id);
            Bind_List(ddlDept3, dept3.ParentID);
            ddlDept3.SelectedValue = dept3.DepartmentID.ToString();
            var dept2 = DepartmentManager.Get(dept3.ParentID);
            Bind_List(ddlDept2, dept2.ParentID);
            ddlDept2.SelectedValue = dept2.DepartmentID.ToString();
            var dept1 = DepartmentManager.Get(dept2.ParentID);
            Bind_List(ddlDept1, dept1.ParentID);
            ddlDept1.SelectedValue = dept1.DepartmentID.ToString();
        }

        private void Init_DefaultList()
        {
            Bind_List(ddlDept1, 0);
        }

        private void Bind_List(DropDownList ddl, int parentId)
        {
            ddl.DataSource = DepartmentManager.GetChildren(parentId);
            ddl.DataTextField = "DepartmentName";
            ddl.DataValueField = "DepartmentId";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("请选择...", "-1"));
        }

        protected void ddlDept1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_List(ddlDept2, int.Parse(ddlDept1.SelectedValue));
            ddlDept3.Items.Clear();
            ddlDept3.Items.Insert(0, new ListItem("请选择...", "-1"));
        }

        protected void ddlDept2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_List(ddlDept3, int.Parse(ddlDept2.SelectedValue));
        }
    }
}