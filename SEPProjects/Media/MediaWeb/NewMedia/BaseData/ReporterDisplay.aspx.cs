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
using ESP.Compatible;
using System.Collections.Generic;
using ESP.MediaLinq.Entity;
using ESP.MediaLinq.BusinessLogic;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class ReporterDisplay : ESP.Web.UI.PageBase
    {
        private int mid = 0;
        private int id = 0;
        int alertvalue = 0;
        private int aid= 0;
        private int agency_mediaid = 0;
        /// <summary>
        /// Raises the <see cref="E:Init"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        override protected void OnInit(EventArgs e)
        {
            InitDataGridColumn();
            base.OnInit(e);
            int userid = CurrentUserID;
            if (string.IsNullOrEmpty(Request[RequestName.Alert]))
            {
                btnClose.Visible = false;
                btnClose1.Visible = false;
            }
            else
            {
                btnBack.Visible = false;
                btnBack1.Visible = false;
            }
            if (!string.IsNullOrEmpty(Request[RequestName.Alert]))
            {
                btnChangeMedia.Visible = false;
                alertvalue = int.Parse(Request[RequestName.Alert]);
                if (alertvalue > 1)
                {
                    btnBack.Visible = true;
                    btnBack1.Visible = true;
                }
            }
        }

        #region 绑定列头
        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitDataGridColumn()
        {
            string strColumn = "id#id#bankname#bankcardname#bankacountname#bankcardcode";
            string strHeader = "选择#私密信息状态#开户银行#银行卡姓名#开户姓名#账号";
            string strH = "center#center####";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, null, strH, this.dgList);
        }
        #endregion
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.ReporterID, Request[RequestName.ReporterID]);
            if (Request[RequestName.MediaID] != null)
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.MediaID, Request[RequestName.MediaID]);
            if (!string.IsNullOrEmpty(Request[RequestName.Alert]))
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.Alert, (Convert.ToInt32(Request[RequestName.Alert]) + 1).ToString());

            string sname = Guid.NewGuid().ToString();//DateTime.Now.ToShortTimeString();
            List<string> trunto = new List<string>();
            trunto.Add("ReporterDisplay.aspx");

            Session[sname] = trunto;

            if (Request[RequestName.BackUrl] != null)
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.BackUrl, Request[RequestName.BackUrl]);

            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Operate", "EDIT");
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "sname", sname);
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "truntocount", "0");
            Response.Redirect(string.Format("ReporterAddAndEdit.aspx?{0}", param));
        }

        int currentPrivateId = 0;
        int relationId = 0;
        int Pjid = 0;
        private void ListBind()
        {
            relationId = int.Parse(Request["relationId"]);
            Pjid = int.Parse(Request[RequestName.ProjectID]);
            //ProjectreporterrelationInfo relation = ProjectsManager.GetRelationReporterModel(int.Parse(Request["relationId"]));
            //if (relation != null)
            //{
            //    ReportersInfo reporter = ESP.Media.BusinessLogic.ReportersManager.GetModel(relation.Reporterid);

            //    int ReportId = reporter == null ? 0 : reporter.Reporterid;
            //    currentPrivateId = relation.Privateinfoid;
            if (!string.IsNullOrEmpty(Request["ReporterID"]))
            {
                DataTable dt = ESP.MediaLinq.BusinessLogic.PrivateManager.getListByReportId(int.Parse(Request["ReporterID"]));
                if (dt == null)
                    dt = new DataTable();
                dgList.DataSource = dt.DefaultView;
            }
            //}
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["type"]) && Request["type"] == "project")
                {
                    tbPrivacy.Visible = true;
                    ListBind();
                }
                if (Request[RequestName.ReporterID] != null)
                {
                    id = Convert.ToInt32(Request[RequestName.ReporterID]);
                    media_ReportersInfo mlReporter = ESP.MediaLinq.BusinessLogic.ReporterManager.GetModel(id);
                    if (mlReporter != null)
                    {
                        if (mlReporter.LastModifiedByUserID > 0)
                        {
                            labLastModifyUser.Text = new ESP.Compatible.Employee((int)mlReporter.LastModifiedByUserID).Name;
                        }
                        if (mlReporter.LastModifiedDate != null)
                        {
                            labLastModifyDate.Text = mlReporter.LastModifiedDate.ToString();
                        }
                        //媒体信息
                        mid = (int)mlReporter.Media_ID;
                        media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(mid);
                        if (media != null)
                        {
                            lnkMediaName.Text = media.MediaCName + " " + media.ChannelName + " " + media.TopicName;
                            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                            param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.MediaID, mid.ToString());
                            if (alertvalue > 0)
                            {
                                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.Alert, (alertvalue + 1).ToString());
                                //lnkMediaName.Attributes["href"] = string.Format("MediaDisplay.aspx?{0}", param);
                                lnkMediaName.Attributes["onclick"] = "javascript:return false;";
                            }
                            else
                            {
                                param = ESP.MediaLinq.Utilities.Global.AddParam(param, "visible", "false");
                                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.Alert, "1");
                                param = ESP.MediaLinq.Utilities.Global.AddParam(param, "enablededit", "0");
                                lnkMediaName.Attributes["onclick"] = string.Format("javascript:window.open('MediaDisplay.aspx?{0}','','{1}')", param, ESP.MediaLinq.Utilities.Global.OpenClass.Common);
                            }
                        }
                            //机构信息
                        aid = 0;
                            if(mlReporter.AgencyID != null)
                            aid = (int)mlReporter.AgencyID;
                        media_AgencyInfo agency = ESP.MediaLinq.BusinessLogic.AgencyManager.GetModel(aid);
                        if (agency != null)
                        {
                            agency_mediaid = (int)agency.MediaID;
                            lnkAgencyName.Text = agency.AgencyCName + " " + agency.AgencyEName;
                            string param2 = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                            param2 = ESP.MediaLinq.Utilities.Global.AddParam(param2, RequestName.MediaID, agency.MediaID.ToString());
                            //if (alertvalue > 0)
                            //{
                            //    param2 = ESP.MediaLinq.Utilities.Global.AddParam(param2, RequestName.Alert, (alertvalue + 1).ToString());
                            //    //lnkMediaName.Attributes["href"] = string.Format("MediaDisplay.aspx?{0}", param);
                            //    lnkAgencyName.Attributes["onclick"] = "javascript:return false;";
                            //}
                            //else
                            //{
                            //    param2 = ESP.MediaLinq.Utilities.Global.AddParam(param2, "visible", "false");
                            //    param2 = ESP.MediaLinq.Utilities.Global.AddParam(param2, RequestName.Alert, "1");
                            //    param2 = ESP.MediaLinq.Utilities.Global.AddParam(param2, "enablededit", "0");
                            //    lnkAgencyName.Attributes["onclick"] = string.Format("javascript:window.open('AgencyDisplay.aspx?{0}','','{1}')", param2, ESP.MediaLinq.Utilities.Global.OpenClass.Common);
                            //}
                            //labMediumSort.Text = media.Mediumsort;
                            //labIssueRegion.Text = media.Issueregion;
                            //labIndustry.Text = ESP.Media.BusinessLogic.IndustriesManager.GetModel(media.Industryid).Industryname;
                            //labheadquarter.Text = ESP.Media.BusinessLogic.CountryManager.GetModel(media.Countryid).Countryname + " " + ESP.Media.BusinessLogic.ProvinceManager.GetModel(media.Provinceid).Province_name + " " + ESP.Media.BusinessLogic.CityManager.GetModel(media.Cityid).City_name;
                            //labTelephoneExchange.Text = media.Telephoneexchange;
                        }
                        //基本信息
                        getFullImageUrl(mlReporter.Photo);
                        labName.Text = mlReporter.ReporterName;//姓名
                        labPenName.Text = mlReporter.PenName;//笔名
                        if (mlReporter.Sex == 1)//性别
                            labSex.Text = "男";
                        else if (mlReporter.Sex == 2)
                            labSex.Text = "女";
                        labBirthday.Text = mlReporter.Birthday.ToString().Trim().Split(' ')[0];//生日  
                        if (labBirthday.Text.Equals("1900-1-1"))
                        {
                            labBirthday.Text = "";
                        }
                        labPostCord.Text = mlReporter.Postcode_H;//家庭邮编 
                        labIdCard.Text = mlReporter.CardNumber;//身份证号 
                        labAddress.Text = mlReporter.Address_H;//住址
                        this.labQq.Text = mlReporter.QQ;
                        this.labMsn.Text = mlReporter.MSN;
                        labOtherMessageSoftware.Text = mlReporter.OtherMessageSoftware;
                        //联系信息
                        labOfficePhone.Text = mlReporter.Tel_O;//办公电话 
                        labHomePhone.Text = mlReporter.Tel_H;//家庭电话 
                        labUsualMobile.Text = mlReporter.UsualMobile;//常用手机 
                        labBackupMobile.Text = mlReporter.BackupMobile;//备用手机 

                        lblOfficeAddress.Text = mlReporter.OfficeAddress;
                        lblOfficePostID.Text = mlReporter.OfficePostID;
                        lblFax.Text = mlReporter.Fax;

                        this.labreporterposition.Text = mlReporter.ReporterPosition;
                        //labFax.Text = mlReporter.Fax.Trim();//传真 
                        //labQq.Text = mlReporter.Qq.Trim();//QQ 
                        //labMsn.Text = mlReporter.Msn.Trim();//MSN 
                        labEmailOne.Text = mlReporter.EmailOne;//E-mail1
                        labEmailTwo.Text = mlReporter.EmailTwo;//E-mail2
                        labEmailThree.Text = mlReporter.EmailThree;//E-mail3
                        //个人信息
                        //labAttention.Text = mlReporter.Attention.Trim();//职责
                        labHobby.Text = mlReporter.Hobby;//兴趣爱好
                        labCharacter.Text = mlReporter.Character;//性格特点
                        if (mlReporter.Marriage == 1)//婚姻状况
                            labMarriage.Text = "已婚";
                        else if (mlReporter.Marriage == 2)
                            labMarriage.Text = "未婚";
                        else
                            labMarriage.Text = "保密";
                        labFamily.Text = mlReporter.Family;//家庭成员
                        labWriting.Text = mlReporter.Writing;//主要作品

                        //负责领域
                        labresponsibledomain.Text = mlReporter.ResponsibleDomain;

                        if (UserInfo.UserID !=mlReporter.CreatedByUserID)
                        {
                            palPrivateInfo.Visible = false;
                        }
                        //if (Request[RequestName.ProjectID] != null && Request[RequestName.ProjectID].Length > 0)
                        //{
                        //    ESP.Media.Entity.ProjectreporterrelationInfo r = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporterModel(Convert.ToInt32(Request[RequestName.ProjectID]), mlReporter.Reporterid);
                        //    if (r != null)
                        //    {
                        //        //开户银行
                        //        labbankname.Text = r.Bankname;
                        //        //开户姓名
                        //        labbankacountname.Text = r.Bankacountname;
                        //        //帐号
                        //        labbankcardcode.Text = r.Bankcardcode;
                        //        //稿酬标准
                        //        labwritingfee.Text = r.Writingfee.ToString();
                        //        //付款方式
                        //        labpaymentmode.Text = r.Paymentmode == 0 ? "" : ESP.Media.Access.Utilities.Global.PaymentModeName[r.Paymentmode].ToString();
                        //        //备注
                        //        labPrivateRemark.Text = r.Privateremark;
                        //        //合作情况
                        //        labcooperatecircs.Text = r.Cooperatecircs;
                        //        palPrivateInfo.Visible = true;
                        //    }
                        //}
                        //else
                        //{
                        //    //开户银行
                        //    labbankname.Text = mlReporter.bankname;
                        //    //开户姓名
                        //    labbankacountname.Text = mlReporter.bankacountname;
                        //    //帐号
                        //    labbankcardcode.Text = mlReporter.bankcardcode;
                        //    //稿酬标准
                        //    labwritingfee.Text = mlReporter.writingfee.ToString();
                        //    //付款方式
                        //    labpaymentmode.Text = mlReporter.paymentmode != null && mlReporter.paymentmode == 0 ? "" : ESP.MediaLinq.Utilities.Global.PaymentModeName[(int)mlReporter.paymentmode].ToString();
                        //    //备注
                        //    labPrivateRemark.Text = mlReporter.PrivateRemark;
                        //    //合作情况
                        //    labcooperatecircs.Text = mlReporter.cooperatecircs;
                        //}

                        //教育信息
                        labEducation.Text = mlReporter.Education;//教育背景
                        //照片
                        //if (mlReporter.Photo != string.Empty)//照片
                        //{
                        //    imgReporter.ImageUrl = mlReporter.Photo;
                        //    imgTitleFull.ImageUrl = mlReporter.Photo.Replace(".jpg", "_full.jpg");
                        //    imgReporter.Height = 50;
                        //    imgReporter.Width = 50;
                        //}
                        //else
                        //{
                        //    imgReporter.Visible = false;
                        //}
                        hidMidId.Value = mid + "";
                        hidRidId.Value = id + "";

                        labHometown.Text = mlReporter.hometown;//籍贯
                        //工作信息
                        labWorkStory.Text = mlReporter.Experience;//职业经历
                        ListBind(mlReporter.Experience);
                    }
                }
                Watch();
            }
            if (!string.IsNullOrEmpty(Request["visible"]) && Request["visible"] == "false")
                ltOperate.Visible = false;
        }


        protected void dgList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = "<input type='radio' id='rad' name='rad' value='" + e.Row.Cells[0].Text + "'/>";
                e.Row.Cells[1].Text = e.Row.Cells[1].Text == currentPrivateId.ToString() ? "正在使用" : "";
                //e.Row.Cells[6].Text = string.Format("<a href='..\\Project\\ReporterPrivacy.aspx?{0}'><img src='{1}'></a>", "relationId=" + relationId + "&Pjid=" + Pjid + "&PrivateId=" + dgList.DataKeys[e.Row.RowIndex].Value.ToString(), ESP.Media.Access.Utilities.ConfigManager.EditIconPath);
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string errmeg = string.Empty;
            if (string.IsNullOrEmpty(Request["rad"]))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择记者私密信息！');</script>");
                return;
            }
           // ESP.Media.Entity.ProjectreporterrelationInfo relation = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporterModel(int.Parse(Request["relationId"]));
          //  relation.Privateinfoid = int.Parse(Request["rad"]);
         //   ESP.Media.BusinessLogic.ProjectsManager.SetReporterProviteMsg(relation, int.Parse(CurrentUser.SysID), out errmeg);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');window.close();", errmeg), true);
        }

        #region 绑定列表
        /// <summary>
        /// Lists the bind.
        /// </summary>
        /// <param name="xml">The XML.</param>
        private void ListBind(string xml)
        {
            //Media_skins_Experience.InitExperienceTable();
            //DataTable dt = Media_skins_Experience.ExperienceTable;
            //System.IO.StringReader sr = new System.IO.StringReader(xml);
            //dt.ReadXml(sr);
            //this.dgList.DataSource = dt.DefaultView;
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the btnViewMedia control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnViewMedia_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.ReporterID, Request[RequestName.ReporterID]);
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.MediaID, mid.ToString());
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Operate", "MediaView");
            if (alertvalue > 0)
            {
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.Alert, (alertvalue + 1).ToString());
            }
            string url = string.Format(@"MediaDisplay.aspx?{0}", param);
            Response.Redirect(url);
        }

        /// <summary>
        /// Handles the Click event of the btnChangeMedia control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnChangeMedia_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.ReporterID, Request[RequestName.ReporterID]);
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.MediaID, mid.ToString());
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Operate", "MediaSelect");
            if (alertvalue > 0)
            {
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.Alert, (alertvalue + 1).ToString());
            }
            string url = string.Format(@"ReporterSelectMediaList.aspx?{0}", param);
            Response.Redirect(url);
        }

        protected void btnAgencyLink_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.ReporterID, Request[RequestName.ReporterID]);
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.MediaID, agency_mediaid.ToString());
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Operate", "AgencySelect");
            if (alertvalue > 0)
            {
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.Alert, (alertvalue + 1).ToString());
            }
            string url = string.Format(@"ReporterSelectAgencyList.aspx?{0}", param);
            Response.Redirect(url);
        }

        #region 返回
        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (alertvalue == 0)
                Response.Redirect(string.IsNullOrEmpty(Request[RequestName.BackUrl]) ? "ReporterList.aspx" : Request[RequestName.BackUrl].ToString());
            else
            {
                //string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                //if (alertvalue > 0)
                //{
                //    param = ESP.Media.Access.Utilities.Global.AddParam(param, RequestName.Alert, (alertvalue - 1).ToString());
                //}
                //Response.Redirect(string.Format("/Project/ProjectSelectReporterList.aspx?{0}", param));
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.MediaID, Request[RequestName.MediaID]);
                if (alertvalue > 0)
                {
                    param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.Alert, (alertvalue - 1).ToString());
                }
                if (string.IsNullOrEmpty(Request[RequestName.BackUrl]))
                {
                    Response.Redirect(string.Format("/Project/ProjectSelectReporterList.aspx?{0}", param));
                }
                else
                {
                    Response.Redirect(string.Format("{0}?{1}", Request[RequestName.BackUrl], param));
                }
            }
        }
        #endregion

        /// <summary>
        /// Watches this instance.
        /// </summary>
        private void Watch()
        {
            if (Request[RequestName.ReporterID] != null)
            {
                //Response.Redirect(string.Format("ReporterContentsList.aspx?Rid={0}", Request["Cid"]));
                string ctrl = string.Empty;
                string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.ReporterID, Request[RequestName.ReporterID]);
                if (string.IsNullOrEmpty(Request[RequestName.Alert]))
                {
                    ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.open('ReportersContentsList.aspx?alert=1&Rid={0}','','{2}');\" value={1} class='widebuttons'/>", Request[RequestName.ReporterID], "历史查看", ESP.MediaLinq.Utilities.Global.OpenClass.Common);
                }
                else
                {
                    param = ESP.MediaLinq.Utilities.Global.AddParam(param, RequestName.Alert, (alertvalue + 1).ToString());
                    ctrl = string.Format("<input type='button' id='btnWatch' onclick=\"window.location = 'ReportersContentsList.aspx?{0}';\" value={1} class='widebuttons'/>", param, "历史查看");
                    if (alertvalue > 0)
                    {
                        //btnChangeMedia.Visible = false;
                    }
                    else
                    {
                        btnChangeMedia.Visible = true;
                    }
                }
                ltOperate.Text = ctrl;
            }
        }

        private void getFullImageUrl(string imgurl)
        {
            if (string.IsNullOrEmpty(imgurl))
            {
                this.imgPic.ImageUrl = ESP.Configuration.ConfigurationManager.SafeAppSettings["ImageUrl"].ToString() + ESP.Configuration.ConfigurationManager.SafeAppSettings["DefauleImgPath"].ToString().Replace(".jpg", "_full.jpg");
            }
            else
                this.imgPic.ImageUrl = imgurl.Replace(".jpg", "_full.jpg");
        }
    }
}
