using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ESP.Finance.Entity;

public partial class Customer_CustomerIndustryInfosList : System.Web.UI.Page
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
        if (this.txtIndustryCode.Text.Trim() != string.Empty)
        {
            condition.Append(" AND IndustryCode Like('%@IndustryCode%')");
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@IndustryCode", this.txtIndustryCode.Text.Trim()));
        }
        if (this.txtIndustryName.Text.Trim() != string.Empty)
        {
            condition.Append(" AND CategoryName Like('%@CategoryName%')");
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@CategoryName", this.txtIndustryName.Text.Trim()));
        }

        IList<CustomerIndustryInfo> list = ESP.Finance.BusinessLogic.CustomerIndustryManager.GetList(condition.ToString(),paramlist);
        this.gvIndustry.DataSource = list;
        this.gvIndustry.DataBind();
    }
}