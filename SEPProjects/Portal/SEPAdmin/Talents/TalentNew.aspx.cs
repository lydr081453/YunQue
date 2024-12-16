using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.HumanResource.Entity;
using ESP.HumanResource.BusinessLogic;

namespace SEPAdmin.Talents
{
    public partial class TalentNew : ESP.Web.UI.PageBase
    {
        TalentInfo model = new TalentInfo();
        TalentManager talentManager = new TalentManager();
        TalentLogManager logManager = new TalentLogManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(SEPAdmin.Talents.TalentNew));

            if (!IsPostBack)
            {
                int talentId = 0;
                if (!string.IsNullOrEmpty(Request["talentid"]))
                {
                    talentId = int.Parse(Request["talentid"]);

                    BindModel(talentId);
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["talentid"]))
            {
                int talentId = int.Parse(Request["talentid"]);

                model = talentManager.GetModel(talentId);
            }

            model.NameCN = this.txtName.Text;

            string customer = string.Empty;
            string professional = string.Empty;

            for (int i = 0; i < this.chklist.Items.Count; i++)
            {
                if (chklist.Items[i].Selected == true)
                {
                    customer += chklist.Items[i].Text + ";";
                }
            }

            for (int i = 0; i < this.chkProfessional.Items.Count; i++)
            {
                if (chkProfessional.Items[i].Selected == true)
                {
                    professional += chkProfessional.Items[i].Text + ";";
                }
            }

            model.Customer = customer;
            model.Professional = professional;
            model.DeptShunya = txtArea.Text;
            model.Language = ddlLanguage.SelectedValue;
            model.BirthDay = DateTime.Parse(txtBirthday.Text);
            model.Education = ddlEducation.SelectedItem.Value;
            model.HRInterview = this.txtHR.Text;
            model.GroupInterview = txtGroup.Text;
            model.Mobile = this.txtMobile.Text;
            model.Position = this.txtPosition.Text;
            model.Resume = txtResume.Text;
            model.WorkBegin = string.IsNullOrEmpty(txtWorkBegin.Text) ? new DateTime(1753, 1, 1) : DateTime.Parse(txtWorkBegin.Text);
            model.CreateTime = DateTime.Now;
            model.CreatorId = CurrentUserID;


            string fileurl  = SaveFile();

            if (!string.IsNullOrEmpty(fileurl))
                model.ResumeFiles = fileurl;
            

            if (!string.IsNullOrEmpty(Request["talentid"]))
            {
                talentManager.Update(model);
            }
            else
            {
                talentManager.Add(model);
            }
            //add message
            if (!string.IsNullOrEmpty(txtMessage.Text))
            {
                TalentLogInfo log = new TalentLogInfo();
                log.TalentId = model.Id;
                log.AuditorId = CurrentUserID;
                log.AuditorName = CurrentUserName;
                log.Remark = txtMessage.Text;
                log.auditDate = DateTime.Now;

                logManager.Add(log);
            }

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交成功！');window.location.href='BackupList.aspx';", true);
        }

        private string SaveFile()
        {
            HttpPostedFile myFile = upFiles.PostedFile;

            if (myFile.FileName != null && myFile.ContentLength > 0)
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                string fn = "/HR/ResumeFiles/resume_" + Guid.NewGuid().ToString() + "_" + this.upFiles.FileName;

                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(fn));

                return fn;
            }
            else
            {
                return "";
            }

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("BackupList.aspx");
        }

        private void BindModel(int talentId)
        {
            model = talentManager.GetModel(talentId);

            if (!string.IsNullOrEmpty(model.Customer))
            {
                for (int i = 0; i < this.chklist.Items.Count; i++)
                {
                    if (model.Customer.IndexOf(chklist.Items[i].Text) >= 0)
                    {
                        chklist.Items[i].Selected = true;
                    }
                }
            }

            if (!string.IsNullOrEmpty(model.Professional))
            {
                for (int i = 0; i < this.chkProfessional.Items.Count; i++)
                {
                    if (model.Professional.IndexOf(chkProfessional.Items[i].Text) >= 0)
                    {
                        chkProfessional.Items[i].Selected = true;
                    }
                }
            }

            this.txtName.Text = model.NameCN;

            this.ddlEducation.SelectedValue = model.Education;
            this.txtGroup.Text = model.GroupInterview;
            this.txtHR.Text = model.HRInterview;
            this.txtMobile.Text = model.Mobile;
            this.txtPosition.Text = model.Position;
            txtArea.Text = model.DeptShunya;
            txtBirthday.Text = model.BirthDay==new DateTime(1,1,1) ? "" : model.BirthDay.ToString("yyyy-MM-dd");
            ddlLanguage.SelectedValue = model.Language;

            txtResume.Text = model.Resume;
            txtWorkBegin.Text = model.WorkBegin == new DateTime(1753, 1, 1) ? "" : model.WorkBegin.ToString("yyyy-MM-dd");

            var loglist = logManager.GetList(" talentId = " + model.Id);

            string logstr = string.Empty;

            foreach (var log in loglist)
            {
                logstr += log.AuditorName + ":" + log.Remark + "[" + log.auditDate.ToString() + "]<br/>";
            }
            lblMessage.Text = logstr;

            if (!string.IsNullOrEmpty(model.ResumeFiles))
            {
                hpFile.ToolTip = "下载附件：" + model.ResumeFiles;
                hpFile.ImageUrl = "/images/ico_04.gif";
                this.hpFile.NavigateUrl = model.ResumeFiles;
            }
            else
            {
                hpFile.Visible = false;
            }

        }
    }
}
