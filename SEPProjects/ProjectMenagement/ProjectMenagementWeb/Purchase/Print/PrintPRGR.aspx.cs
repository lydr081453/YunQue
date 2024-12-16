using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

namespace FinanceWeb.Purchase.Print
{
    public partial class PrintPRGR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtList = ESP.Finance.BusinessLogic.ReturnManager.GetPNTableLinkPR(" and returnid="+Request[ESP.Finance.Utility.RequestName.ReturnID], new List<System.Data.SqlClient.SqlParameter>());
                printPRGR(dtList);
            }
        }

        private void printPRGR(DataTable dtList)
        {
            string printContent = "";
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            foreach (DataRow dr in dtList.Rows)
            {
                if (!ht.Contains(int.Parse(dr["prid"].ToString())))
                {
                    ht.Add(int.Parse(dr["prid"].ToString()), int.Parse(dr["returnid"].ToString()));
                }
                else
                {
                    ht[int.Parse(dr["prid"].ToString())] = ht[int.Parse(dr["prid"].ToString())] + "," + dr["returnid"].ToString();
                }
            }
            foreach (System.Collections.DictionaryEntry de in ht)
            {
                //PR打印
                printContent += GetPrHtml(int.Parse(de.Key.ToString()));
                printContent += "<p style=\"page-break-after:always\">&nbsp;</p>";
                //GR打印
                DataTable recipientIdList = ESP.Finance.BusinessLogic.ReturnManager.GetRecipientIds(de.Value.ToString());
                for(int i=0;i<recipientIdList.Rows.Count;i++)
                {
                    printContent += GetGrHtml(int.Parse(recipientIdList.Rows[i]["recipientId"].ToString()));
                    if(i < recipientIdList.Rows.Count-1)
                        printContent += "<p style=\"page-break-after:always\">&nbsp;</p>";
                }
            }
            labPRGR.Text = printContent;
        }


        private string GetPrHtml(int generalId)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("default.aspx", "");
            string url = "";
            string content = "";
            url = hostUrl + "Purchase/Requisition/Print/RequisitionPrint.aspx?id=" + generalId + "&viewButton=no";
            content += ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
            return content;
        }

        private string GetGrHtml(int recipientId)
        {
            string hostUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["MainPage"].Replace("default.aspx", "");
            string url = "";
            string content = "";
            url = hostUrl + "Purchase/Requisition/Print/MultiRecipientPrint.aspx?id=" + recipientId + "&newPrint=true&viewbutton=no";
            content += ESP.ConfigCommon.SendMail.ScreenScrapeHtml(url);
            return content;
        }
    }
}
