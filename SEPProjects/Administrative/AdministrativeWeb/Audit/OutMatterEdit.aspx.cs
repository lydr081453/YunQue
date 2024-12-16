using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;

namespace AdministrativeWeb.Audit
{
    public partial class OutMatterEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Show();
                // 判断是否有选择时间
                if (Request["selectdatetime"] != null)
                {
                    SelectDateTime = Request["selectdatetime"];
                }
                
                if (Request["userid"] != null)
                {
                    MatterUserID = Request["userid"];
                }
                // 判断是否有返回页面信息
                if (Request["backurl"] != null)
                {
                    BackUrl = Request["backurl"];
                }
                else
                {
                    BackUrl = "OutAuditList.aspx?userid=" + MatterUserID;
                }
                if (Request["tabtype"] != null)
                {
                    int index = int.Parse(Request["tabtype"]);
                    switch (index)
                    {
                        case 8:
                            tabMatters.SelectedTab = tabMatters.Tabs[0];
                            MultiPage1.SelectedIndex = 0;
                            matOut.BackUrl = BackUrl;
                            break;
                        case 10:
                            tabMatters.SelectedTab = tabMatters.Tabs[1];
                            MultiPage1.SelectedIndex = 1;
                            matOther.BackUrl = BackUrl;
                            break;
                    }
                }
                matOut.SelectDateTime = SelectDateTime;                
                matOut.BackUrl = BackUrl;                
            }
        }

        protected void Show()
        {
            TabStripTab tab = new TabStripTab();
            if (!string.IsNullOrEmpty(Request["tabtype"]))
            {
                int index = int.Parse(Request["tabtype"]);
               // MultiPage1.SelectedIndex = index;
                switch (index)
                {                    
                    case 8:
                        matOut.Visible = true;
                        MultiPage1.SelectedIndex = 0;
                        break;                    
                    case 10:
                        matOther.Visible = true;
                        MultiPage1.SelectedIndex = 1;
                        break;
                }
            }
            else
            {
                matOut.Visible = true;                
                matOther.Visible = true;
            }

            tab = new TabStripTab();
            tab.ClientTemplateId = "TabTemplate";
            tab.ID = "0";
            tab.Text = "外出";
            tabMatters.Tabs.Add(tab);

            tab = new TabStripTab();
            tab.ClientTemplateId = "TabTemplate";
            tab.ID = "1";
            tab.Text = "其他";
            tabMatters.Tabs.Add(tab);
            
        }

        /// <summary>
        /// 用户选择的日期
        /// </summary>
        private string _selectDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 用户选择的日期
        /// </summary>
        public string SelectDateTime
        {
            get
            {
                return _selectDateTime;
            }
            set
            {
                _selectDateTime = value;
            }
        }

        private string _userid = "";
        public string MatterUserID
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
            }
        }

        /// <summary>
        /// 返回URL
        /// </summary>
        public string BackUrl
        {
            get
            {
                return this.ViewState["BackUrl"] as string;
            }
            set
            {
                this.ViewState["BackUrl"] = value;
            }
        }
    }
}
