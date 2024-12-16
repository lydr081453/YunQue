using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;

    public partial class Default2 : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                BindMessage();
                BindNews();
        }


        private void BindMessage()
        {
            string subject = string.Empty;
            int messageCount = 0;
            for (int j = 0; j < State.Message_Area.Length; j++)
            {
                DataSet dsMessage = ESP.Purchase.DataAccess.MessageDataProvider.GetList(5, j);
                if (dsMessage == null)
                {
                    dsMessage = new DataSet();
                }
                else
                {
                    for (int i = 0; i < dsMessage.Tables[0].Rows.Count; i++)
                    {
                        subject += string.Format("<span style='cursor:pointer' onMouseOver=\"this.className='hrefover';\" onMouseOut=\"this.className='hrefout';\" onclick='show({0})'>{1}</span><br/>", dsMessage.Tables[0].Rows[i]["id"].ToString(), dsMessage.Tables[0].Rows[i]["subject"].ToString());
                       messageCount++;
                       if (messageCount == 5)
                           break;
                    }
                    if (messageCount < dsMessage.Tables[0].Rows.Count)
                    {
                        subject += " <a target=\"_blank\" style=\"cursor:hand;font-size:12px;\"  onclick=\"openAll();return false;\">all news</a>";
                    }
                    this.litMessage.Text = subject;
                }
            }
        }

        private void BindNews()
        {
            string subject = string.Empty;
            int messageCount = 0;
            DataSet dsMessage = PolicyFlowManager.GetAllList();
            if (dsMessage == null)
            {
                dsMessage = new DataSet();
            }
            else
            {
                for (int i = 0; i < dsMessage.Tables[0].Rows.Count; i++)
                {
                    subject += string.Format("<span style='cursor:pointer' onMouseOver=\"this.className='hrefover';\" onMouseOut=\"this.className='hrefout';\">{0}{1}</span>&nbsp;<a target='_blank' href='/Purchase/Requisition/UpfileDownload.aspx?OrderId={2}&policy=1'><img src='/images/ico_04.gif' border='0' /></a><br/>", (i + 1).ToString() + ". ", dsMessage.Tables[0].Rows[i]["title"].ToString(), dsMessage.Tables[0].Rows[i]["id"].ToString());
                    messageCount++;
                    
                }
              
                this.litNews.Text = subject;
            }
        }

        //private void BindSupplier()
        //{
        //    string subject = string.Empty;
        //    int messageCount = 0;
        //    List<ESP.Purchase.Entity.SupplierInfo> list = SupplierManager.getTopModelList(" and supplier_type=" + (int)State.supplier_type.recommend);
        //    foreach(ESP.Purchase.Entity.SupplierInfo model in list)
        //    {
        //        subject += string.Format("<span>{0}</span>&nbsp;<br/>", model.supplier_name);
        //        messageCount++;
        //    }

        //    this.litSupplier.Text = subject;
        //}
    }

