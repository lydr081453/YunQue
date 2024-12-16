using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;
using ESP.Administrative.Common;

namespace AdministrativeWeb.Attendance
{
    public partial class MattersEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Show();
                // 判断是否有选择时间
                if (Request["selectdatetime"] != null)
                {
                    DateTime selectDateTime = DateTime.Parse(Request["selectdatetime"]);
                    CommuterTimeManager commuterTimeManager = new CommuterTimeManager();
                    CommuterTimeInfo commuterTimeModel = commuterTimeManager.GetCommuterTimes(commuterTimeManager.GetCommuterTimeByUserId(UserID), selectDateTime.Date);
                    SelectDateTime = DateTime.Parse(Request["selectdatetime"]).Date.Add(commuterTimeModel.GoWorkTime.TimeOfDay).ToString("yyyy-MM-dd HH:mm:ss");
                }
                // 判断是否有返回页面信息
                if (Request["backurl"] != null)
                {
                    BackUrl = Request["backurl"];
                }
                if (Request["tabtype"] != null)
                { 
                    int index = int.Parse(Request["tabtype"]);
                    //tabMatters.SelectedTab = tabMatters.Tabs[index];
                    //MultiPage1.SelectedIndex = index;
                    //tabMatters.SelectedTab = tabMatters.Tabs[0];
                    //MultiPage1.SelectedIndex = 0;
                }

                matLeave.SelectDateTime = SelectDateTime;
                matOut.SelectDateTime = SelectDateTime;
                //matOverTime.SelectDateTime = SelectDateTime;
                matTavel.SelectDateTime = SelectDateTime;
                //matOffTune.SelectDateTime = SelectDateTime;
                //matOther.SelectDateTime = SelectDateTime;
                //matOTLate.SelectDateTime = SelectDateTime;

                matLeave.BackUrl = BackUrl;
                matOut.BackUrl = BackUrl;
                //matOverTime.BackUrl = BackUrl;
                matTavel.BackUrl = BackUrl;
               //matOffTune.BackUrl = BackUrl;
                //matOther.BackUrl = BackUrl;
               //matOTLate.BackUrl = BackUrl;
                
            }
            GetClockInTimes();
        }

        /// <summary>
        /// 显示选项卡内容信息
        /// </summary>
        protected void Show()
        {
            TabStripTab tab = new TabStripTab();
            if (!string.IsNullOrEmpty(Request["tabtype"]))
            {
                int index = int.Parse(Request["tabtype"]);
                MultiPage1.SelectedIndex = index;
                switch (index)
                {
                    case 0:
                        matLeave.Visible = true;
                        break;
                    case 1:
                        matOut.Visible = true;
                        break;
                    case 2:
                        matTavel.Visible = true;
                        break;
                    //case 3:
                    //    matOffTune.Visible = true;
                    //    break;
                    //case 4:
                    //    matOTLate.Visible = true;
                    //    break;
                }
            }
            else
            {
                matLeave.Visible = true;
                //matOverTime.Visible = true;
                matOut.Visible = true;
                matTavel.Visible = true;
                //matOffTune.Visible = true;
                //matOther.Visible = true;
                //matOTLate.Visible = true;
            }

            tab.ClientTemplateId = "TabTemplate";
            tab.ID = "0";
            tab.Text = "请假";
            tabMatters.Tabs.Add(tab);

            tab = new TabStripTab();
            tab.ClientTemplateId = "TabTemplate";
            tab.ID = "1";
            tab.Text = "外出";
            tabMatters.Tabs.Add(tab);

            tab = new TabStripTab();
            tab.ClientTemplateId = "TabTemplate";
            tab.ID = "2";
            tab.Text = "出差";
            tabMatters.Tabs.Add(tab);

            //tab = new TabStripTab();
            //tab.ClientTemplateId = "TabTemplate";
            //tab.ID = "3";
            //tab.Text = "调休";
            //tabMatters.Tabs.Add(tab);

            //tab = new TabStripTab();
            //tab.ClientTemplateId = "TabTemplate";
            //tab.ID = "4";
            //tab.Text = "晚到申请";
            //tabMatters.Tabs.Add(tab);
        }
        
        /// <summary>
        /// 获得前后三天的上下班时间
        /// </summary>
        public void GetClockInTimes()
        {
            Dictionary<int, DateTime> dic = new ClockInManager().GetClockInTimes(DateTime.Parse(SelectDateTime), UserID);
            string[] clockins = new string[3] { "无", "无", "无" };
            string[] clockouts = new string[3] { "无", "无", "无" };
            if (dic != null && dic.Count > 0)
            {
                DateTime beginTime = DateTime.Parse(SelectDateTime).AddDays(-1);

                for (int i = 0; i < 3; i++)
                {
                    dic.ContainsKey(beginTime.Day);
                    DateTime time = new DateTime();
                    DateTime time1 = new DateTime();
                    if (!dic.TryGetValue(beginTime.Day, out time))
                    {
                        time = new DateTime(1900, 1, 1, 0, 0, 0);
                    }
                    if (!dic.TryGetValue(-beginTime.Day, out time1))
                    {
                        time1 = new DateTime(1900, 1, 1, 0, 0, 0);
                    }

                    if (time == null || (time.ToString("yyyy-MM-dd")) == Status.EmptyTime)
                        clockins[i] = "无";
                    else
                        clockins[i] = time.ToString("HH:mm");

                    if (time1 == null || (time1.ToString("yyyy-MM-dd")) == Status.EmptyTime)
                        clockouts[i] = "无";
                    else
                        clockouts[i] = time1.ToString("HH:mm");
                    beginTime = beginTime.AddDays(1);
                }

            }
            ClockIn = clockins;
            ClockOut = clockouts;
        }

        /// <summary>
        /// 前后三天的上班时间
        /// </summary>
        private string[] _clockIn;
        public string[] ClockIn
        {
            get
            {
                return _clockIn;
            }
            set
            {
                _clockIn = value;
            }
        }
        /// <summary>
        /// 前后三天的下班时间
        /// </summary>
        private string[] _clockOut;
        public string[] ClockOut
        {
            get
            {
                return _clockOut;
            }
            set
            {
                _clockOut = value;
            }
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

        private string _backUrl = "MattersEdit.aspx";
        /// <summary>
        /// 返回URL
        /// </summary>
        public string BackUrl
        {
            get
            {
                return _backUrl;
            }
            set
            {
                _backUrl = value;
            }
        }
    }
}
