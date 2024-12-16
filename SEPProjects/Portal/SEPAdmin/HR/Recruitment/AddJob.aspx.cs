using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPAdmin.HR.Recruitment
{
    public partial class AddJob : ESP.Web.UI.PageBase
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
                    ESP.HumanResource.Entity.JobInfo jobInfo = new ESP.HumanResource.BusinessLogic.JobManager().GetModel(id);
                    if (jobInfo != null)
                    {
                        Binding(jobInfo);
                    }
                }
            }
        }


        private void Binding(ESP.HumanResource.Entity.JobInfo jobInfo)
        {
            txtName.Text = jobInfo.JobName;
            ddlWorkingPlace.SelectedValue = jobInfo.WorkingPlace;
            txtResponsibilities.Text = jobInfo.Responsibilities;
            txtCreateTime.Text = jobInfo.CreateTime.ToString("yyyy-MM-dd");
            txtRequirements.Text = jobInfo.Requirements;
            txtSerToCustomer.Text = jobInfo.SerToCustomer;
            txtId.Text = jobInfo.JobId.ToString();
            txtCreater.Text = jobInfo.Creater.ToString();
            if (jobInfo.UrgentRecruitment)
            {
                radioIsEmergency.Checked = true;
                radioIsNotEmergency.Checked = false;
            }
            else
            {
                radioIsEmergency.Checked = false;
                radioIsNotEmergency.Checked = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (checkEmpty())
            {
                ESP.HumanResource.Entity.JobInfo jobInfo = new ESP.HumanResource.Entity.JobInfo
                {
                    JobName = txtName.Text.ToString(),
                    WorkingPlace = ddlWorkingPlace.SelectedValue,
                    Responsibilities = txtResponsibilities.Text.ToString(),
                    Requirements = txtRequirements.Text.ToString(),
                    SerToCustomer = txtSerToCustomer.Text,
                    UrgentRecruitment = radioIsEmergency.Checked ? true : false,
                    UpdateTime = DateTime.Now
                };
                ESP.HumanResource.BusinessLogic.JobManager jobManager = new ESP.HumanResource.BusinessLogic.JobManager();
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    jobInfo.Creater = int.Parse(CurrentUser.SysID);
                    jobInfo.CreateTime = DateTime.Now;
                    jobInfo.Ordinal = 1;
                    if (jobManager.Add(jobInfo) > -1)
                    {
                        Response.Write("<script>alert('成功添加一条培训活动！')</script>");
                        Server.Transfer("ListJob.aspx");
                    }
                }
                else
                {
                    jobInfo.JobId = int.Parse(txtId.Text);
                    jobInfo.CreateTime = DateTime.Parse(txtCreateTime.Text);
                    jobInfo.Creater = int.Parse(txtCreater.Text);
                    jobInfo.Ordinal = 1;
                    if (jobManager.Update(jobInfo) == 1)
                    {
                        Response.Write("<script>alert('成功修改一条岗位信息！')</script>");
                        Server.Transfer("ListJob.aspx");
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
            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('岗位名称不能为空!');", true);
                txtName.Focus();
                return false;
            }
            if (this.txtName.Text.Length > 50)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('最大输入50字符，岗位名称输入超限！');", true);
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtSerToCustomer.Text))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('服务客户不能为空!');", true);
                txtSerToCustomer.Focus();
                return false;
            }
            if (this.txtSerToCustomer.Text.Length > 50)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('最大输入50字符，服务客户输入超限！');", true);
                txtSerToCustomer.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtResponsibilities.Text))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('工作职责不能为空!');", true);
                txtResponsibilities.Focus();
                return false;
            }
            if (this.txtResponsibilities.Text.Length > 2000)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('最大输入2000字符，工作职责输入超限！');", true);
                txtResponsibilities.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtRequirements.Text))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('职位要求不能为空!');", true);
                txtRequirements.Focus();
                return false;
            }
            if (txtRequirements.Text.Length > 2000)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('最大输入2000字符，职位要求输入超限！');", true);
                txtRequirements.Focus();
                return false;
            }
            return true;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListJob.aspx");
        }
    }
}
