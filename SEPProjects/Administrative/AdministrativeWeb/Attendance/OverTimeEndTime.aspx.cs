using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Administrative.BusinessLogic;
using ESP.Administrative.Entity;

namespace AdministrativeWeb.Attendance
{
    /// <summary>
    /// 导出OT结束时间和下班打卡时间不一致的OT单信息
    /// </summary>
    public partial class OverTimeEndTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /// <summary>
            /// 用户打卡记录信息
            /// </summary>
            Dictionary<int, Dictionary<long, DateTime>> clockInDic = null;
            // 获得用户打卡记录信息
            ClockInManager clockInManager = new ClockInManager();
            clockInDic = clockInManager.GetClockInTimes(2009, 11, 0);

            SingleOvertimeManager singleManager = new SingleOvertimeManager();
            //List<SingleOvertimeInfo> singleOvertimeList = singleManager.GetSingleOvertimeByDepid(107);
            List<SingleOvertimeInfo> list = new List<SingleOvertimeInfo>();
            //if (singleOvertimeList != null && singleOvertimeList.Count > 0)
            //{
            //    foreach (SingleOvertimeInfo model in singleOvertimeList)
            //    {
            //        int singleUserid = model.UserID;
            //        Dictionary<long, DateTime> dictime = clockInDic[singleUserid];
            //        long day = model.BeginTime.Day;
            //        if (dictime.ContainsKey(-day))
            //        {
            //            DateTime time = dictime[-day];
            //            if (model.EndTime > time)
            //            {
            //                model.CreateTime = time;
            //                list.Add(model);
            //            }
            //        }
            //    }
            //}

            Grid1.DataSource = list;
            Grid1.DataBind();
        }
    }
}
