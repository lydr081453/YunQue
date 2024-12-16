using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using ESP.Compatible;

namespace FrameSite.Web.include.page
{
	/// <summary>
	/// Header 的摘要说明。
	/// </summary>
	public partial class Header : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
            {
                ESP.Framework.Entity.UserInfo currentuser = ESP.Framework.BusinessLogic.UserManager.Get();
                lblCaption.Text = "当前用户：";
                if (currentuser != null)
                    lblCaption.Text += currentuser.FullNameCN;
            }
		}

        /// <summary>
        /// 用户中文名称
        /// </summary>
        public string userName
        {
            get
            {
                ESP.Framework.Entity.UserInfo currentuser = ESP.Framework.BusinessLogic.UserManager.Get();
                return currentuser.FullNameCN;
            }
        }
	}
}
