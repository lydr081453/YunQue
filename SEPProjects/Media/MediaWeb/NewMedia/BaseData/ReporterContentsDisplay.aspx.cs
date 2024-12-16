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
using ESP.MediaLinq.Utilities;
using System.Collections.Generic;
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class ReporterContentsDisplay : ESP.Web.UI.PageBase
    {
        private int mid = 0;
        private int id = 0;
        int alertvalue = 0;

        /// <summary>
        /// Raises the <see cref="E:Init"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        override protected void OnInit(EventArgs e)
        {
            InitDataGridColumn();
            base.OnInit(e);
            int userid = CurrentUserID;
            if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"] != "1")
            {
                btnBack.Visible = true;
                alertvalue = int.Parse(Request["alert"]);
            }
        }

        #region 绑定列头
        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitDataGridColumn()
        {
            //string strColumn = "单位类型#单位名称#职位#工作起始时间#工作截止时间#专兼职#单位所属行业#单位描述";
            //string strHeader = "单位类型#单位名称#职位#工作起始时间#工作截止时间#专兼职#单位所属行业#单位描述";
            //string sort = "#######";
            //MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort, this.dgList);
        }
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Hisid"] != null)
            {
                int hisID = Convert.ToInt32(Request["Hisid"]);
                id = Convert.ToInt32(Request["Rid"]);
                ESP.MediaLinq.Entity.media_reportersHistInfo mlReporter = ESP.MediaLinq.BusinessLogic.ReportersHistManager.GetModel(hisID);
                if (mlReporter != null)
                {
                    if (mlReporter.LastModifiedByUserID > 0)
                    {
                        labLastModifyUser.Text = new ESP.Compatible.Employee(Convert.ToInt32(mlReporter.LastModifiedByUserID)).Name;
                    }
                    if (mlReporter.LastModifiedDate != null)
                    {
                        labLastModifyDate.Text = mlReporter.LastModifiedDate.ToString();
                    }
                    //媒体信息
                    mid = Convert.ToInt32(mlReporter.Media_ID);
                    media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(mid);
                    if (media != null)
                    {
                        lnkMediaName.Text = media.MediaCName + " " + media.ChannelName + " " + media.TopicName;
                        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
                        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Mid", mid.ToString());
                        if (alertvalue > 0)
                        {
                            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue + 1).ToString());
                            //lnkMediaName.Attributes["href"] = string.Format("MediaDisplay.aspx?{0}", param);
                            lnkMediaName.Attributes["onclick"] = "javascript:return false;";
                        }
                        else
                        {
                            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", "1");
                            param = ESP.Media.Access.Utilities.Global.AddParam(param, "visible", "false");
                            lnkMediaName.Attributes["onclick"] = string.Format("javascript:window.open('MediaDisplay.aspx?{0}','','{1}')", param, ESP.Media.Access.Utilities.Global.OpenClass.Common);
                        }

                        //labMediumSort.Text = media.Mediumsort;
                        //labIssueRegion.Text = media.Issueregion;
                        //labIndustry.Text = ESP.Media.BusinessLogic.IndustriesManager.GetModel(media.Industryid).Industryname;
                        //labheadquarter.Text = ESP.Media.BusinessLogic.CountryManager.GetModel(media.Countryid).Countryname + " " + ESP.Media.BusinessLogic.ProvinceManager.GetModel(media.Provinceid).Province_name + " " + ESP.Media.BusinessLogic.CityManager.GetModel(media.Cityid).City_name;
                        //labTelephoneExchange.Text = media.Telephoneexchange;
                    }
                    //基本信息
                    labVersion.Text = mlReporter.version + "";
                    labName.Text = mlReporter.ReporterName;//姓名
                    labPenName.Text = mlReporter.PenName;//笔名
                    if (mlReporter.Sex == 1)//性别
                        labSex.Text = "男";
                    else if (mlReporter.Sex == 2)
                        labSex.Text = "女";
                    labBirthday.Text = mlReporter.Birthday.ToString().Split(' ')[0];//生日  
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
                    //教育信息
                    labEducation.Text = mlReporter.Education;//教育背景
                    //照片
                    if (mlReporter.Photo != string.Empty)//照片
                    {
                        imgReporter.ImageUrl = mlReporter.Photo;
                        imgReporter.Height = 50;
                        imgReporter.Width = 50;
                    }
                    else
                    {
                        imgReporter.Visible = false;
                    }
                    labHometown.Text = mlReporter.hometown;//籍贯
                    //工作信息
                    labWorkStory.Text = mlReporter.Experience;//职业经历

                    if (!CurrentUser.SysID.Equals(mlReporter.CreatedByUserID.ToString()))
                    {
                        palPrivateInfo.Visible = false;
                    }
                    if (Request[RequestName.ProjectID] != null && Request[RequestName.ProjectID].Length > 0)
                    {
                        ESP.Media.Entity.ProjectreporterrelationInfo r = ESP.Media.BusinessLogic.ProjectsManager.GetRelationReporterModel(Convert.ToInt32(Request[RequestName.ProjectID]), mlReporter.ReporterID);
                        //开户银行
                        labbankname.Text = r.Bankname;
                        //开户姓名
                        labbankacountname.Text = r.Bankacountname;
                        //帐号
                        labbankcardcode.Text = r.Bankcardcode;
                        //稿酬标准
                        labwritingfee.Text = r.Writingfee.ToString();
                        //付款方式
                        labpaymentmode.Text = r.Paymentmode == 0 ? "" : ESP.Media.Access.Utilities.Global.PaymentModeName[r.Paymentmode].ToString();
                        //备注
                        labPrivateRemark.Text = r.Privateremark;
                        //合作情况
                        labcooperatecircs.Text = r.Cooperatecircs;
                        palPrivateInfo.Visible = true;
                    }
                    else
                    {
                        //开户银行
                        labbankname.Text = mlReporter.bankname;
                        //开户姓名
                        labbankacountname.Text = mlReporter.bankacountname;
                        //帐号
                        labbankcardcode.Text = mlReporter.bankcardcode;
                        //稿酬标准
                        labwritingfee.Text = mlReporter.writingfee.ToString();
                        //付款方式
                        labpaymentmode.Text = mlReporter.paymentmode == 0 ? "" : ESP.Media.Access.Utilities.Global.PaymentModeName[Convert.ToInt32(mlReporter.paymentmode)].ToString();
                        //备注
                        labPrivateRemark.Text = mlReporter.PrivateRemark;
                        //合作情况
                        labcooperatecircs.Text = mlReporter.cooperatecircs;
                    }
                }
            }
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

        /// <summary>
        /// Handles the RowDataBound event of the dgList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text == "1")
                    e.Row.Cells[0].Text = "媒体";
                else if (e.Row.Cells[0].Text == "2")
                    e.Row.Cells[0].Text = "非媒体";

                if (e.Row.Cells[5].Text == "1")
                    e.Row.Cells[5].Text = "专职";
                else if (e.Row.Cells[5].Text == "2")
                    e.Row.Cells[5].Text = "兼职";
            }
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the btnViewMedia control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnViewMedia_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MediaDisplay.aspx?Mid={0}&Operate={1}&Rid={2}", mid, "MediaView", id));
        }

        /// <summary>
        /// Handles the Click event of the btnChangeMedia control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnChangeMedia_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ReporterSelectMediaList.aspx?Mid={0}&Operate={1}&Rid={2}", mid, "MediaSelect", id));
        }

        #region 返回
        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query;
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Rid", id.ToString());
            if (alertvalue > 0)
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue - 1).ToString());
            Response.Redirect(string.Format("ReporterContentsList.aspx?{0}", param));
        }
        #endregion
    }
}
