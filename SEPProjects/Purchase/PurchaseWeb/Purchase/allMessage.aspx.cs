using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ESP.Purchase.Common;

public partial class Purchase_allMessage : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBind();
        }
    }

    private void ListBind()
    {

        for (int j = 0; j < State.Message_Area.Length; j++)
        {
            DataSet dsMessage = ESP.Purchase.DataAccess.MessageDataProvider.GetList(0, j);
            if (dsMessage == null)
            {
                dsMessage = new DataSet();
            }
            else
            {
                Table tbn = null;
                TableHeaderRow trhead = null;
                TableRow tr1 = null;

                tbn = new Table();
                tbn.Attributes["style"] = ";width:100%;border-collapse: collapse; border:solid 2px #eaedf1;";

                trhead = new TableHeaderRow();
                trhead.Style.Add("width", "100%");
                trhead.Attributes["style"] = "background-color:#f7f7f7;font:10pt";
                tbn.Controls.Add(trhead);

                TableHeaderCell tcheader1 = new TableHeaderCell();
                tcheader1.Text = "主题/内容  " + State.Message_Area[j];
                tcheader1.Attributes["style"] = "width:60%;text-align: left;border-spacing: 10px;border:solid 1px #eaedf1";
                tcheader1.Width = new Unit(50);

                trhead.Controls.Add(tcheader1);

                for (int i = 0; i < dsMessage.Tables[0].Rows.Count; i++)
                {

                    tr1 = new TableRow();
                    tr1.Attributes["style"] = "margin-top:1;margin-bottom:1";
                    tbn.Controls.Add(tr1);

                    TableCell tcsubject = new TableCell();
                    string subject = string.Format("<b><asp:Label id='lab1'  runat='server'>{0}</asp:Label></b><br/>", dsMessage.Tables[0].Rows[i]["subject"].ToString());
                    string body = dsMessage.Tables[0].Rows[i]["body"].ToString();
                    if (body.Length > 50)
                    {
                        body = body.Substring(0, 50) + "....";
                        if (dsMessage.Tables[0].Rows[i]["attFile"].ToString() != "")
                        {
                            body += "&nbsp;<a target='_blank' href='../../" + dsMessage.Tables[0].Rows[i]["attFile"] + "'><img src='/images/ico_04.gif' border='0' /></a>&nbsp;";
                        }
                        body = string.Format("<asp:Label id='lab2'  runat='server'>{0}</asp:Label>&nbsp;&nbsp;<img src='/images/more.gif' style='cursor:pointer' onclick='show({1})' />", body, dsMessage.Tables[0].Rows[i]["id"].ToString());
                    }
                    else
                    {
                        body = string.Format("<asp:Label id='lab2'  runat='server'>{0}</asp:Label>", body);
                    }
                    tcsubject.Text = subject + body;
                    tcsubject.Attributes["style"] = "border-spacing: 10px;border:solid 1px #eaedf1";

                    TableCell tccreatername = new TableCell();
                    tccreatername.Text = new ESP.Compatible.Employee(int.Parse(dsMessage.Tables[0].Rows[i]["createrid"].ToString())).Name;
                    tccreatername.Attributes["style"] = "text-align: center;border-spacing: 10px;border:solid 1px #eaedf1";

                    TableCell tctime = new TableCell();
                    tctime.Text = dsMessage.Tables[0].Rows[i]["lasttime"].ToString();
                    tctime.Attributes["style"] = "text-align: center;border-spacing: 10px;border:solid 1px #eaedf1";

                    tr1.Controls.Add(tcsubject);

                    switch (j)
                    {
                        case 0:
                            phMessage.Controls.Add(tbn);
                            break;
                        case 1:
                            phMessageSH.Controls.Add(tbn);
                            break;

                        case 2:
                            phMessageGZ.Controls.Add(tbn);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
