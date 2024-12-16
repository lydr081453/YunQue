using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FinanceWeb
{
    public partial class SearchKeyWord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
        }

        void getData()
        {
            string name = Request.QueryString["name"];
            int top = int.Parse(Request.QueryString["top"]);

            string al = "";
            Response.Clear();
            if (name.Length >= 1)
            {
                string strformat = string.Format(" city like '%{0}%' or cityEN like '%{0}%' or ShortName like '%{0}%'" , name);
                IList<ESP.Finance.Entity.TicketCityInfo> citylist = ESP.Finance.BusinessLogic.TicketCityManager.GetList(strformat);
                foreach(ESP.Finance.Entity.TicketCityInfo city in citylist)
                {
                    al += "," + city.City + "|" + city.CityEN;
                }
            }

            Response.Write(al);
            Response.Flush();
            Response.End();
        }

    }
}
