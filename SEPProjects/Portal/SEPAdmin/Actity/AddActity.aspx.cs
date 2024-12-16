using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.Actity
{
    public partial class AddActity : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetRequestId();
        }

        private void GetRequestId()
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    ESP.HumanResource.Entity.ActityInfo actityInfo = new ESP.HumanResource.BusinessLogic.ActityManager().GetModel(id);
                    if (actityInfo!= null)
                    {
                        Binding(actityInfo);
                    }
                }
            }
        }

        private void Binding(ESP.HumanResource.Entity.ActityInfo actityInfo)
        {
            txtTitle.Text = actityInfo.ActityTitle;
            txtLecturer.Text = actityInfo.Lecturer;
            txtContent.Text = actityInfo.ActityContent;
            txtTime.Text = actityInfo.ActityTime.ToString("yyyy-MM-dd HH:mm");
            txtId.Text = actityInfo.Id.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (checkEmpty())
            {
                ESP.HumanResource.Entity.ActityInfo actityInfo = new ESP.HumanResource.Entity.ActityInfo
                {
                    ActityTitle = this.txtTitle.Text,
                    Lecturer = this.txtLecturer.Text,
                    ActityContent = this.txtContent.Text,
                    ActityTime = DateTime.Parse(this.txtTime.Text)
                };
                ESP.HumanResource.BusinessLogic.ActityManager actityManager = new ESP.HumanResource.BusinessLogic.ActityManager();
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    if (actityManager.Add(actityInfo) > -1)
                    {
                        Response.Write("<script>alert('成功添加一条培训活动！')</script>");
                        Server.Transfer("ListActity.aspx");
                    }
                }
                else
                {
                    actityInfo.Id = int.Parse(txtId.Text);
                    if (actityManager.Update(actityInfo)==1)
                    {
                        Response.Write("<script>alert('成功修改一条培训活动！')</script>");
                        Server.Transfer("ListActity.aspx");
                    }
                }
            }
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        /// <returns></returns>
        private bool checkEmpty()
        {
            if (string.IsNullOrEmpty(this.txtTitle.Text))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('培训标题不能为空!');", true);
                txtTitle.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtLecturer.Text))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('培训讲师不能为空!');", true);
                txtLecturer.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtContent.Text))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('培训内容不能为空!');", true);
                txtContent.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTime.Text))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('培训时间不能为空!');", true);
                return false;
            }
            else
            {
                DateTime selectDt = Convert.ToDateTime(txtTime.Text);
                if (selectDt <= DateTime.Now)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('您选择的培训时间已过期!');", true);
                    return false;
                }
            }
            return true;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListActity.aspx");
        }
    }
}
