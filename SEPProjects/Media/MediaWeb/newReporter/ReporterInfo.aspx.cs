using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Media.Entity;
using System.Data;

namespace MediaWeb.newReporter
{
    public partial class ReporterInfo : ESP.Web.UI.PageBase
    {
        int reporterId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request[RequestName.ReporterID]))
                reporterId = int.Parse(Request[RequestName.ReporterID]);

            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private void BindInfo()
        {
            ReportersInfo mlReporter = ESP.Media.BusinessLogic.ReportersManager.GetModel(reporterId);
            if (mlReporter != null)
            {
                //媒体信息
                int mid = mlReporter.Media_id;
                MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(mid);
                if (media != null)
                {
                    lnkMediaName.Text = media.Mediacname + " " + media.Channelname + " " + media.Topicname;
                    string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, RequestName.MediaID, mid.ToString());
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "visible", "false");
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, RequestName.Alert, "1");
                    param = ESP.Media.Access.Utilities.Global.AddParam(param, "enablededit", "0");
                    lnkMediaName.Attributes["onclick"] = string.Format("javascript:window.open('/Media/MediaDisplay.aspx?{0}','','{1}')", param, ESP.Media.Access.Utilities.Global.OpenClass.Common);
                }
                //基本信息
                getFullImageUrl(mlReporter.Photo);
                labName.Text = mlReporter.Reportername.Trim();//姓名
                if (mlReporter.Sex == 1)//性别
                    labSex.Text = "男";
                else if (mlReporter.Sex == 2)
                    labSex.Text = "女";
                this.labQq.Text = mlReporter.Qq.Trim();
                this.labMsn.Text = mlReporter.Msn.Trim();
                labOtherMessageSoftware.Text = mlReporter.Othermessagesoftware.Trim();
                //联系信息
                labOfficePhone.Text = mlReporter.Tel_o.Trim();//办公电话 
                labUsualMobile.Text = mlReporter.Usualmobile.Trim();//常用手机 

                lblOfficeAddress.Text = mlReporter.OfficeAddress;
                lblFax.Text = mlReporter.Fax;

                this.labreporterposition.Text = mlReporter.Reporterposition;
                labEmailOne.Text = mlReporter.Emailone.Trim();//E-mail1

                //负责领域
                labresponsibledomain.Text = mlReporter.Responsibledomain;

                DataSet ds = ESP.Media.BusinessLogic.ReporterEvaluationManager.GetReporterEvaluation(int.Parse(Request[RequestName.ReporterID]));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtEvaluation.Text = labEvaluation.Text = ds.Tables[0].Rows[0]["evaluation"].ToString();
                    labEditInfo.Text = "本信息由" + ds.Tables[0].Rows[0]["username"].ToString() + "最后修订于" + DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:ss") + "。";
                    labEditInfo.Text += "&nbsp;<a href='#' onclick=\"window.open('EvaluationLogList.aspx?Rid=" + Request[RequestName.ReporterID] + "');\">查看历史</a>";
                }

               DataTable dtHist = ESP.Media.BusinessLogic.ReportersManager.GetHistFullInfoByClientID(reporterId);
               if (dtHist != null && dtHist.Rows.Count > 0 && dtHist.Rows.Count > 0)
               {
                   ESP.Compatible.Employee emp = new ESP.Compatible.Employee(int.Parse(dtHist.Rows[0]["createdbyuserid"].ToString()));
                   lblHist.Text = "本信息由" + emp.Name + "最后修订于" + DateTime.Parse(dtHist.Rows[0]["CreatedDate"].ToString()).ToString("yyyy-MM-dd HH:ss") + "。";
                   lblHist.Text += "&nbsp;<a href='#' onclick=\"window.open('ReporterHist.aspx?Rid=" + Request[RequestName.ReporterID] + "');\">查看历史</a>";
               }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ESP.Media.Entity.ReporterEvaluation model = new ESP.Media.Entity.ReporterEvaluation();
            model.CreateDate = DateTime.Now;
            model.Evaluation = txtEvaluation.Text.Trim();
            model.ReporterId = int.Parse(Request[RequestName.ReporterID]);
            model.UserID = int.Parse(CurrentUser.SysID);
            model.UserName = CurrentUser.Name;
            model.Reason = txtReason.Text.Trim();

            ESP.Media.BusinessLogic.ReporterEvaluationManager.Insert(model);
            Response.Redirect("ReporterInfo.aspx?Rid="+Request[RequestName.ReporterID]);
        }

        private void getFullImageUrl(string imgurl)
        {
            if (string.IsNullOrEmpty(imgurl))
            {
                this.imgPic.ImageUrl = "/images/head-e.jpg";
            }
            else
                this.imgPic.ImageUrl = imgurl;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReporterList.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReporterEdit.aspx?" + RequestName.ReporterID + "=" + reporterId);
        }
    }
}
