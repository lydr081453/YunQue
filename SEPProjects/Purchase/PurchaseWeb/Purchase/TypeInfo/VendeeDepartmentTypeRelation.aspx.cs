using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Supplier.BusinessLogic;
using ESP.Supplier.Entity;

public partial class VendeeDepartmentTypeRelation : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<ESP.Framework.Entity.DepartmentInfo> list = ESP.Framework.BusinessLogic.DepartmentManager.GetAll();
            list = list.Where(n=>n.DepartmentLevel==3 && !n.DepartmentName.Contains("作废")).ToList();
            
            gv.DataSource = list;
            gv.DataBind();
        }

        protected void gv_RowDataBound(object sender,GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label labtype=(Label)e.Row.FindControl("labtype");
                ESP.Framework.Entity.DepartmentInfo model = (ESP.Framework.Entity.DepartmentInfo)e.Row.DataItem;
                int did = model.DepartmentID;
                string dname = model.DepartmentName;
                IList<ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation> list = ESP.Supplier.BusinessLogic.SC_VendeeDepartmentTypeRelationManager.GetListByDepartmentId(did);
                string typeNames = string.Empty;
                foreach (ESP.Supplier.Entity.SC_VendeeDepartmentTypeRelation lst in list)
                {
                    //ESP.Supplier.Entity.XML_VersionList stpye = new ESP.Supplier.BusinessLogic.XML_VersionListManager().GetModel(lst.TypeId);
                    ESP.Supplier.Entity.XML_VersionClass stpye = new ESP.Supplier.BusinessLogic.XML_VersionClassManager().GetModel(lst.TypeId);
                    if (stpye != null)
                    {
                        typeNames += stpye.Name + ",";
                    }
                }
                if (typeNames != string.Empty)
                    typeNames = typeNames.Substring(0, typeNames.Length - 1);
                labtype.Text = typeNames;
                Literal litView = (Literal)e.Row.FindControl("litView");
                litView.Text = "<a href='VendeeDepartmentTypeRelationEdit.aspx?did=" + did + "'><img src='../../images/dc.gif' border='0px;' title='查看'></a>";

            }
        }

    }
