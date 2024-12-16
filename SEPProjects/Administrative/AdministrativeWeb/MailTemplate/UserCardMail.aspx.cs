using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AdministrativeWeb.MailTemplate
{
    public partial class UserCardMail : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["flag"]))
                {
                    string flag = Request["flag"];
                    if (flag == "1")   // 启用新门卡
                    {
                        divEnable.Visible = true;
                        divUnEnable.Visible = false;
                        divChange.Visible = false;

                        initForm(Request["userid"], Request["cardno"]);
                    }
                    else if (flag == "2")   // 停用新门卡
                    {
                        divEnable.Visible = false;
                        divUnEnable.Visible = true;
                        divChange.Visible = false;
                        initForm(Request["userid"], Request["cardno"]);
                    }
                    else if (flag == "3")   // 更换门卡
                    {
                        divEnable.Visible = false;
                        divUnEnable.Visible = false;
                        divChange.Visible = true;
                        string cardno = "(原卡) " + Request["oldcardno"] + " ---- (新卡) " + Request["cardno"];
                        initForm(Request["userid"], cardno);
                    }
                }
            }
        }

        protected void initForm(string userid, string cardno)
        {
            imgs.ImageUrl = "http://" + Request.Url.Authority + "/images/mail_03.jpg";

            List<ESP.HumanResource.Entity.EmployeeBaseInfo> list = EmployeeBaseManager.GetModelList(" and a.userid=" + userid + "");
            if (list.Count > 0)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo info = list[0];
                if (cardno.Trim() != "0")
                {
                    info.Memo = cardno.Trim();
                }
                else
                {
                    info.Memo = "门禁绑定失败，请手动绑定门禁卡！";
                }

                UserName = info.FullNameCN;
                UserCode = info.Code;
                CompanyName = info.EmployeeJobInfo.companyName + " - " + info.EmployeeJobInfo.departmentName + " - " + info.EmployeeJobInfo.groupName;
                CardNoInfo = info.Memo;
            }
        }

        public void rptUserList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ESP.HumanResource.Entity.EmployeeBaseInfo model = (ESP.HumanResource.Entity.EmployeeBaseInfo)e.Item.DataItem;
            }
        }

        public string UserName
        {
            get
            {
                return this.ViewState["UserName"] as string;
            }
            set
            {
                this.ViewState["UserName"] = value;
            }
        }
        public string CompanyName
        {
            get
            {
                return this.ViewState["CompanyNameget"] as string;
            }
            set
            {
                this.ViewState["CompanyNameget"] = value;
            }   
        }
        public string UserCode
        {
            get
            {
                return this.ViewState["UserCode"] as string;
            }
            set
            {
                this.ViewState["UserCode"] = value;
            }
        }
        public string CardNoInfo
        {
            get
            {
                return this.ViewState["CardNoInfo"] as string;
            }
            set
            {
                this.ViewState["CardNoInfo"] = value;
            }
        }
    }
}