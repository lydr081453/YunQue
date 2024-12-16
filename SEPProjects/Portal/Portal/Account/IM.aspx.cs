using System;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Portal.WebSite.Account
{
    public partial class IM : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request["type"] != null && Request["type"] == "0")
            //{
            //    BindingMSN();
            //}
            //if (!IsPostBack)
            //{
            //    string messenger = Twitter.Messenger.GetInstance().GetUserMessenger(UserID);
            //    if (!string.IsNullOrEmpty(messenger))
            //    {
            //        pnlReBinding.Visible = true;
            //        pnlBinding.Visible = false;
            //        pnlBtnCancel.Visible = false;
            //    }
            //    else
            //    {
            //        pnlReBinding.Visible = false;
            //        pnlBinding.Visible = true;
            //        pnlBtnCancel.Visible = false;
            //    }
            //}
        }

        /// <summary>
        /// 绑定MSN
        /// </summary>
        public void BindingMSN()
        {
            //try
            //{
            //    string messengerPath = ESP.Configuration.ConfigurationManager.SafeAppSettings["MessengerPath"];
            //    if(!System.IO.File.Exists(messengerPath))
            //    {
            //        throw new System.IO.FileNotFoundException(null, messengerPath);
            //    }

            //    Process pro = new Process();
            //    // 不显示窗口
            //    pro.StartInfo.CreateNoWindow = true;
            //    pro.StartInfo.UseShellExecute = false;
            //    //要调用的控制台程序
            //    pro.StartInfo.FileName = messengerPath;
            //    //给控制台程序的参数传递值
            //    pro.StartInfo.Arguments = Request.Form["address"].Trim() + " " + Request.Form["password"].Trim() +" " + UserID;
            //    pro.Start();
            //    //调用控制台程序的返回值
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}

            //btnsub.Disabled = true;
        }

        protected void btnRe_Click(object sender, EventArgs e)
        {
            //pnlReBinding.Visible = false;
            //pnlBinding.Visible = true;
            //pnlBtnCancel.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //pnlReBinding.Visible = true;
            //pnlBinding.Visible = false;
            //pnlBtnCancel.Visible = false;
        }
    }
}
