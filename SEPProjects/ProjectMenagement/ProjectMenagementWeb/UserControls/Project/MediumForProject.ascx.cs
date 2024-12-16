using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinanceWeb.UserControls.Project
{
    public partial class MediumForProject : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindMediaForProject();
        }

        private int userid;
        public int CurrentUserID
        {
            get { return userid; }
            set { userid = value; }
        }

        protected void linkbtnMediaForProject_Click(object sender, EventArgs e)
        {
            BindMediaForProject();
        }

        private void BindMediaForProject()
        {
            IList<ESP.Finance.Entity.MediumForProjectInfo> list = ESP.Finance.BusinessLogic.MediumForProjectManager.GetList(" ProjectID=" + Request["projectid"] + " AND (IsDel=0 OR IsDel is Null)");
            if (list != null && list.Count > 0)
            {
                this.gvMedia.DataSource = list;
                this.gvMedia.DataBind();
            }
        }

        protected void gvMedia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESP.Finance.Entity.MediumForProjectInfo info = (ESP.Finance.Entity.MediumForProjectInfo)e.Row.DataItem;
                Label lblMediaName = (Label)e.Row.FindControl("lblMediaName");
                Label lblMediaType = (Label)e.Row.FindControl("lblMediaType");
                if (lblMediaName != null && lblMediaType != null)
                {
                    ESP.Media.Entity.MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(info.MediaID);
                    if (media != null)
                    {
                        lblMediaName.Text = media.Mediacname;
                        lblMediaType.Text = media.Mediatype;
                    }
                }
            }
        }

        protected void gvMedia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DelMediaForPro" && e.CommandArgument.ToString() != string.Empty)
            {
                int id = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out id))
                {
                    if (id > 0)
                    {
                        ESP.Finance.Entity.MediumForProjectInfo model = ESP.Finance.BusinessLogic.MediumForProjectManager.GetModel(id);
                        if (model != null)
                        {
                            model.IsDel = true;
                            model.ModifiedUserID = CurrentUserID;
                            ESP.Finance.BusinessLogic.MediumForProjectManager.Update(model);
                        }
                    }
                }
            }
        }


    }
}