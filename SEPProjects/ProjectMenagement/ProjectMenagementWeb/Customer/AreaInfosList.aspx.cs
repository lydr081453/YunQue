using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ESP.Finance.Entity;

public partial class Customer_AreaInfosList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { Search(); }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    { Search(); }

    protected void Search()
    {
        StringBuilder condition = new StringBuilder();
        List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();

        condition.Append(" 1 = 1 ");
        if (this.txtAreaCode.Text.Trim() != string.Empty)
        {
            condition.Append(" AND AreaCode Like('%@AreaCode%')");
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@AreaCode", this.txtAreaCode.Text.Trim()));
        }
        if (this.txtAreaName.Text.Trim() != string.Empty)
        {
            condition.Append(" AND AreaName Like('%@AreaName%')");
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@AreaName", this.txtAreaName.Text.Trim()));
        }
        if (this.txtSearchCode.Text.Trim() != string.Empty)
        {
            condition.Append(" AND SearchCode Like('%@SearchCode%')");
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@SearchCode", this.txtSearchCode.Text.Trim()));
        }

        IList<AreaInfo> list = ESP.Finance.BusinessLogic.AreaManager.GetList(condition.ToString(), paramlist);
        this.gvArea.DataSource = list;
        this.gvArea.DataBind();
    }
}