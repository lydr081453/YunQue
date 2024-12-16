using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using ESP.Compatible;
using ESP.Media.Entity;

public partial class NewMedia_System_Default : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int quarter=0;
        if (System.DateTime.Now.Month % 3 > 0)
            quarter = System.DateTime.Now.Month / 3 + 1;
        else
            quarter = System.DateTime.Now.Month / 3;
        BirthdayTips();
        postList();
        SetLastFaceToFace();
        meeting();
        MediaView();
        integralTOP10(1, System.DateTime.Now.Month);
        integralTOP10(2, quarter);
        integralTOP10(3, System.DateTime.Now.Year);
    }
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
        int userid = CurrentUserID;
    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = string.Empty;
        string strHeader = string.Empty;
     }
    #endregion


    bool checkRole(int[] source, List<int> des)
    {
        for (int i = 0; i < source.Length; i++)
        {
            for (int j = 0; j < des.Count; j++)
            {
                if (source[i] == des[j])
                    return false;
            }
        }
        return true;
    }

    public void integralTOP10(int flg, int values)
    {
        List<ESP.Media.Entity.CounterInfo> lists = null;
        String[] sItems;
        Int32[] iValue;
         switch (flg)
        {
            case 1:

                lists = ESP.Media.BusinessLogic.CounterManager.GetMonthList(Page.Application, System.DateTime.Now.Year, values);
                if (lists == null || lists.Count == 0) return;
                sItems = new String[lists.Count];
                iValue = new Int32[lists.Count];
                for (int i = 0; i < lists.Count; i++)
                {
                    sItems[i] = lists[i].Username;
                    iValue[i] = lists[i].Counts;

                }
                dngchart.YAxisValues = iValue;
                dngchart.YAxisItems = sItems;
                dngchart.ChartTitle = "<b>积分排名TOP10</b>";
                dngchart.XAxisTitle = "";
                break;
            case 2:
                lists = ESP.Media.BusinessLogic.CounterManager.GetSeasonList(Page.Application, System.DateTime.Now.Year, values);
                if (lists == null || lists.Count == 0) return;
                sItems = new String[lists.Count];
                iValue = new Int32[lists.Count];
                for (int i = 0; i < lists.Count; i++)
                {
                    sItems[i] = lists[i].Username;
                    iValue[i] = lists[i].Counts;

                }
                this.quarterChart.YAxisValues = iValue;
                quarterChart.YAxisItems = sItems;
                quarterChart.ChartTitle = "<b>积分排名TOP10</b>";
                quarterChart.XAxisTitle = "";
                break;
            case 3:
                lists = ESP.Media.BusinessLogic.CounterManager.GetYearList(Page.Application, values);
                if (lists == null || lists.Count == 0) return;
                sItems = new String[lists.Count];
                iValue = new Int32[lists.Count];
                for (int i = 0; i < lists.Count; i++)
                {
                    sItems[i] = lists[i].Username;
                    iValue[i] = lists[i].Counts;

                }
                this.yearChart.YAxisValues = iValue;
                yearChart.YAxisItems = sItems;
                yearChart.ChartTitle = "<b>积分排名TOP10</b>";
                yearChart.XAxisTitle = "";
                break;
            default:
                lists = ESP.Media.BusinessLogic.CounterManager.GetMonthList(Page.Application, System.DateTime.Now.Year, values);
                if (lists == null || lists.Count == 0) return;
                sItems = new String[lists.Count];
                iValue = new Int32[lists.Count];
                for (int i = 0; i < lists.Count; i++)
                {
                    sItems[i] = lists[i].Username;
                    iValue[i] = lists[i].Counts;

                }
                dngchart.YAxisValues = iValue;
                dngchart.YAxisItems = sItems;
                dngchart.ChartTitle = "<b>积分排名TOP10</b>";
                dngchart.XAxisTitle = "";
                break;
        }


    }

    private void BirthdayTips()
    {
        #region 生日提醒

        DateTime sTime = DateTime.Now;
        DateTime eTime = sTime.AddDays(14);

        DataTable dtBirthday = ESP.Media.BusinessLogic.UsersManager.GetReportersByBirthday(CurrentUserID, sTime, eTime);


        if (dtBirthday == null || dtBirthday.Rows.Count==0)
        {
            Label l = new Label();
            l.ForeColor = System.Drawing.Color.Black;
            l.Text = "没有符合条件的记者生日信息.";
            phBirthday.Controls.Add(l);
        }
        else
        {
            //this.dgBirthday.DataSource = dtBirthday.DefaultView;

            //phBirthday绑定
            Table tbb = new Table();
            tbb.Attributes["style"] = "width: 85%; border:0; padding:0";
            tbb.Attributes["align"] = "center";
            tbb.BorderWidth = 0;
            tbb.CellPadding = 0;
            TableRow trb = null;
            for (int i = 0; i < dtBirthday.Rows.Count; i++)
            {
                if ((i % 2) == 0)
                {
                    trb = new TableRow();
                    trb.CssClass = "align:left";
                    tbb.Controls.Add(trb);
                }

                TableCell tcb = new TableCell();
                tcb.Text = string.Format(" <li></li><span class='span1'>{0} {1}</span>", dtBirthday.Rows[i]["ReporterName"], dtBirthday.Rows[i]["MediaName"]);


                TableCell tcbv = new TableCell();
                tcbv.Text = string.Format("<span class='span1'>生日：{0}</span>",Convert.ToDateTime( dtBirthday.Rows[i]["Birthday"]).ToString("MM-dd"));
                trb.Controls.Add(tcb);
                trb.Controls.Add(tcbv);

            }
            phBirthday.Controls.Add(tbb);
        }
        #endregion

    }
    private void postList()
    {

        #region 公告信息

        DataTable dtPosts = ESP.Media.BusinessLogic.PostsManager.GetCommonMsgTopN(10);
        DataTable dtUnAudit = ESP.Media.BusinessLogic.MediaitemsManager.GetUnAuditList(null, null);
        if (dtPosts == null)
        {
            dtPosts = new DataTable();
        }
        else
        {
            //phPosts绑定
            Table tbn = new Table();
            tbn.Attributes["style"] = "width: 85%; border:0; padding:0";
            tbn.Attributes["align"] = "center";
            tbn.BorderWidth = 0;
            tbn.CellPadding = 0;
            TableRow trtitle = null;
            TableRow trbody = null;
            if (dtUnAudit.Rows.Count != 0)//存在未审核媒体
            {
                trtitle = new TableRow();
                trbody = new TableRow();
                trtitle.CssClass = "oddrow";


                TableCell tcrelease = new TableCell();
                tcrelease.Text = string.Format("<span class='span1'>发布人：{0}</span>", "系统");


                TableCell tcsubj = new TableCell();
                tcsubj.Text = string.Format("<span class='span1'>主题：</span>&nbsp;&nbsp;<a href='/Media/AuditMediaList.aspx'>当前有<span class='span1'>{0}</span>个媒体未审核.</a>", dtUnAudit.Rows.Count.ToString());
                trtitle.Controls.Add(tcsubj);
                trtitle.Controls.Add(tcrelease);
                TableCell tcbody = new TableCell();
                tcbody.ColumnSpan = 2;
                tcbody.Text = string.Format("<a href='/Media/AuditMediaList.aspx'>当前有<span class='span1'>{0}</span>个媒体未审核.</a>", dtUnAudit.Rows.Count.ToString());
                trbody.Controls.Add(tcbody);

                tbn.Controls.Add(trtitle);
                tbn.Controls.Add(trbody);
            }

            for (int i = 0; i < dtPosts.Rows.Count; i++)//循环添加媒介公告
            {

                trtitle = new TableRow();
                trbody = new TableRow();
                trtitle.CssClass = "oddrow";


                TableCell tcrelease = new TableCell();
                tcrelease.Text = string.Format("<span class='span1'>发布人：{0}</span>", new ESP.Compatible.Employee(Convert.ToInt32(dtPosts.Rows[i]["userid"].ToString())).Name);

                TableCell tcsubj = new TableCell();
                tcsubj.Text = string.Format("<span class='span1'>主题：{0}</span>&nbsp;&nbsp;<span class='span1'>({1})</span>", dtPosts.Rows[i]["subject"], dtPosts.Rows[i]["issuedate"]);
                trtitle.Controls.Add(tcsubj);
                trtitle.Controls.Add(tcrelease);

                TableCell tcbody = new TableCell();
                tcbody.ColumnSpan = 2;
                tcbody.Text = string.Format("<span class='span1'>{0}</span>", Server.HtmlDecode(dtPosts.Rows[i]["body"].ToString()));
                trbody.Controls.Add(tcbody);

                tbn.Controls.Add(trtitle);
                tbn.Controls.Add(trbody);

            }
            phPosts.Controls.Add(tbn);

        }
        #endregion

    }

    #region 媒介例会
    private void meeting()
    {
        MeetingsInfo meeting = ESP.Media.BusinessLogic.MeetingsManager.GetNew();

        Table tbn = new Table();
        tbn.Attributes["style"] = "width: 85%; border:0; padding:0";
        tbn.Attributes["align"] = "center";
        tbn.BorderWidth = 0;
        tbn.CellPadding = 0;
        TableRow trtitle = null;
        TableRow trbody = null;

        trtitle = new TableRow();
        trtitle.CssClass = "oddrow";
        
        TableCell tcsubj = new TableCell();
        tcsubj.Attributes["style"] = "width: 30%;align:left;";
        tcsubj.Attributes["align"] = "center";
        tcsubj.Text = "最新发布:  " + meeting.Subject + string.Format("<a href='#' onclick=\"window.location='{0}'\">{1}</a>", meeting.Path.Substring(meeting.Path.IndexOf('~') + 1), "[下载]"); 

        TableCell tctime = new TableCell();
        tctime.Attributes["style"] = "width: 10%;";
        tctime.Attributes["align"] = "center";
        tctime.Text = "<span style=\"font-size:9px\">["+meeting.Createdate+"]    第"+meeting.Cycle.ToString()+"期</span>";

        trbody = new TableRow();

        TableCell tcremark = new TableCell();
        tcremark.Attributes["align"] = "left";
        tcremark.Text = string.Format("<span class='span1'>{0}</span>", Server.HtmlDecode(meeting.Remark));
        trbody.Controls.Add(tcremark);

        TableCell tcdown = new TableCell();
        tcdown.Attributes["style"] = "width: 10%;";
        tcdown.Attributes["align"] = "center";
        tcdown.Text = "<a href=\"/System/MediaMeetingAdd.aspx\">往期回顾</a>";

        trtitle.Controls.Add(tcsubj);
        trtitle.Controls.Add(tctime);

        trbody.Controls.Add(tcremark);
        trbody.Controls.Add(tcdown);

        tbn.Controls.Add(trtitle);
        tbn.Controls.Add(trbody);

        MeetingHolder.Controls.Add(tbn);

    }
    #endregion

    #region 媒介视线
    private void MediaView()
    {
        DataTable dtCommon = ESP.Media.BusinessLogic.PostsManager.GetBlogMsg();
        Table tbn = new Table();
        tbn.Attributes["style"] = "width: 85%; border:0; padding:0";
        tbn.Attributes["align"] = "center";
        tbn.BorderWidth = 0;
        tbn.CellPadding = 0;
        TableRow trtitle = null;
        TableRow trbody = null;
        trtitle = new TableRow();
        TableCell tcnew = new TableCell();
        tcnew.Text = string.Format("<a href='{0}'>{1}</a>", "/Message/MsgList.aspx", "往期回顾");
        trtitle.Controls.Add(tcnew);
        tbn.Controls.Add(trtitle);
        //for (int i = 0; i < dtCommon.Rows.Count; i++)//循环添加媒介视线
        //{
        if (dtCommon.Rows.Count > 0)
        {
            trtitle = new TableRow();
            trbody = new TableRow();
            trtitle.CssClass = "oddrow";


            TableCell tcrelease = new TableCell();
            tcrelease.Text = string.Format("<span class='span1'>发布人：{0}</span>", new ESP.Compatible.Employee(Convert.ToInt32(dtCommon.Rows[0]["userid"].ToString())).Name);

            TableCell tcsubj = new TableCell();
            tcsubj.Text = string.Format("<span class='span1'>主题：{0}</span>&nbsp;&nbsp;<span class='span1'>({1})</span>", dtCommon.Rows[0]["subject"], dtCommon.Rows[0]["issuedate"]);

            trtitle.Controls.Add(tcsubj);
            trtitle.Controls.Add(tcrelease);

            TableCell tcbody = new TableCell();
            tcbody.ColumnSpan = 2;
            tcbody.Text = string.Format("<span class='span1'>{0}</span>", Server.HtmlDecode(dtCommon.Rows[0]["body"].ToString()));
            trbody.Controls.Add(tcbody);

            tbn.Controls.Add(trtitle);
            tbn.Controls.Add(trbody);

        }
        phMediaView.Controls.Add(tbn);

    }
    #endregion


    #region 媒体面对面
    private void SetLastFaceToFace()
    {
        FacetofaceInfo face = ESP.Media.BusinessLogic.FacetofaceManager.GetNew();
      
        
        Table tbn = new Table();
        tbn.Attributes["style"] = "width: 85%; border:0; padding:0";
        tbn.Attributes["align"] = "center";
        tbn.BorderWidth = 0;
        tbn.CellPadding = 0;
        TableRow trtitle = null;
        TableRow trbody = null;
        trtitle = new TableRow();
        trtitle.CssClass = "oddrow";

        TableCell tcsubj = new TableCell();
        tcsubj.Attributes["style"] = "width: 30%;align:left;";
        tcsubj.Attributes["align"] = "center";
        tcsubj.Text = "最新发布:  " + face.Subject + string.Format("<a href='#' onclick=\"window.location='{0}'\">{1}</a>", face.Path.Substring(face.Path.IndexOf('~') + 1), "[下载]");

        TableCell tctime = new TableCell();
        tctime.Attributes["style"] = "width: 10%;";
        tctime.Attributes["align"] = "center";
        tctime.Text = "<span style=\"font-size:9px\">[" + face.Createdate + "]    第" + face.Cycle.ToString() + "期</span>";

        trbody = new TableRow();

        TableCell tcremark = new TableCell();
        tcremark.Attributes["align"] = "left";
        tcremark.Text = string.Format("<span class='span1'>{0}</span>", Server.HtmlDecode(face.Remark));
        trbody.Controls.Add(tcremark);

        TableCell tcdown = new TableCell();
        tcdown.Attributes["style"] = "width: 10%;";
        tcdown.Attributes["align"] = "center";
        tcdown.Text = "<a href=\"/System/MediaFaceToFaceAdd.aspx\">往期回顾</a> ";

        trtitle.Controls.Add(tcsubj);
        trtitle.Controls.Add(tctime);

        trbody.Controls.Add(tcremark);
        trbody.Controls.Add(tcdown);

        tbn.Controls.Add(trtitle);
        tbn.Controls.Add(trbody);

        face2faceholder.Controls.Add(tbn);

    }
    #endregion

    protected void dgBirthday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Split(' ')[0];
        }
    }
    protected void dgIntegral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text.Trim().Length > 0)
                e.Row.Cells[0].Text = new ESP.Compatible.Employee(Convert.ToInt32(e.Row.Cells[0].Text)).Name;
        }
    }
    protected void dgPosts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text.Trim().Length > 0)
                e.Row.Cells[0].Text = new ESP.Compatible.Employee(Convert.ToInt32(e.Row.Cells[0].Text)).Name;
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Split(' ')[0];
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Split(' ')[0];
        }
    }

    protected void dgNoUploadReporterList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
     
    }
}