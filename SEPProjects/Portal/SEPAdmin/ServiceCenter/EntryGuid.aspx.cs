using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;
using ESP.HumanResource.BusinessLogic;
using ESP.HumanResource.Entity;
using System.Net.Mail;
using ESP.HumanResource.Common;
using System.Drawing;

namespace SEPAdmin.ServiceCenter
{
    public partial class EntryGuid : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["alert"] == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "login_stock", "alert('星言云汇新增“公司治理制度汇编”，请您到员工服务中心 - 在职指引中阅读。');", true);
                }
              
            }
        }
        protected void btnDaily_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDaily.Text))
                SendMail("内部日常事务-邮件反馈(" + CurrentUser.Name + ")", txtDaily.Text);
        }
        protected void btnAdmin_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAdmin.Text))
                SendMail("考勤系统的使用-邮件反馈(" + CurrentUser.Name + ")", txtAdmin.Text);
        }
        protected void btnTrain_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTrain.Text))
                SendMail("人事-邮件反馈(" + CurrentUser.Name + ")", txtTrain.Text);
        }


        void SendMail(string title, string content)
        {
            MailAddress[] list = new MailAddress[] { new MailAddress("qi.zhang@shunyagroup.com"), new MailAddress("wei.zhang@shunyagroup.com") };

            ESP.Mail.MailManager.Send(title, content, true, list);
        }

        private void AddLog(int index)
        {
            StockFileReaderManager stockManager = new StockFileReaderManager();

            List<StockFileReaderInfo> stockList = stockManager.GetList(" where userid = " + UserID.ToString() + " and fileindex =" + index);
            if (stockList.Count == 0)
            {
                StockFileReaderInfo stock = new StockFileReaderInfo();
                stock.UserId = UserID;
                stock.FileIndex = index;
                stock.ReadTime = DateTime.Now;
                stockManager.Add(stock);
            }
        }

        protected void btnFile1_Click(object sender, EventArgs e)
        {
            AddLog(1);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile3_Click(object sender, EventArgs e)
        {
            AddLog(3);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile2_Click(object sender, EventArgs e)
        {
            AddLog(2);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile4_Click(object sender, EventArgs e)
        {
            AddLog(4);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile5_Click(object sender, EventArgs e)
        {
            AddLog(5);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile6_Click(object sender, EventArgs e)
        {
            AddLog(6);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile7_Click(object sender, EventArgs e)
        {
            AddLog(7);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile8_Click(object sender, EventArgs e)
        {
            AddLog(8);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile9_Click(object sender, EventArgs e)
        {
            AddLog(9);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile10_Click(object sender, EventArgs e)
        {
            AddLog(10);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile11_Click(object sender, EventArgs e)
        {
            AddLog(11);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile12_Click(object sender, EventArgs e)
        {
            AddLog(12);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile13_Click(object sender, EventArgs e)
        {
            AddLog(13);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile14_Click(object sender, EventArgs e)
        {
            AddLog(14);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile15_Click(object sender, EventArgs e)
        {
            AddLog(15);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile17_Click(object sender, EventArgs e)
        {
            AddLog(17);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile18_Click(object sender, EventArgs e)
        {
            AddLog(18);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile20_Click(object sender, EventArgs e)
        {
            AddLog(20);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile21_Click(object sender, EventArgs e)
        {
            AddLog(21);
            Response.Redirect("EntryGuid.aspx?index=6");
        }
        protected void btnFile22_Click(object sender, EventArgs e)
        {
            AddLog(22);
            Response.Redirect("EntryGuid.aspx?index=6");
        }
        protected void btnFile23_Click(object sender, EventArgs e)
        {
            AddLog(23);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile19_Click(object sender, EventArgs e)
        {
            AddLog(19);
            Response.Redirect("EntryGuid.aspx?index=6");
        }

        protected void btnFile16_Click(object sender, EventArgs e)
        {
            AddLog(16);
            Response.Redirect("EntryGuid.aspx?index=6");
        }
    }
}
