using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using ESP.Administrative.BusinessLogic;
using System.Configuration;

namespace StatisticService
{
    public partial class StatisticService : ServiceBase
    {

        #region 局部变量的定义
        /// <summary>
        ///  日志记录对象
        /// </summary>
        private LogManager logger = new LogManager();
        /// <summary>
        /// 计时器对象
        /// </summary>
        private System.Timers.Timer timerDelay;
        /// <summary>
        /// 是否刚刚启动的一个标识
        /// </summary>
        private int start = 0;
        #endregion

        public StatisticService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ESP.Configuration.ConfigurationManager.Create();
            logger.Add("StatisticService：统计考勤数据服务启动成功");
            timerDelay = new System.Timers.Timer();
            timerDelay.Elapsed += new System.Timers.ElapsedEventHandler(timerDelay_Elapsed);
            timerDelay.Start();
        }

        protected override void OnStop()
        {
            ESP.Configuration.ConfigurationManager.Dispose();
            logger.Add("StatisticService: 统计考勤数据服务停用成功");
        }

        void timerDelay_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // 计算下一次执行的时间
            // double sleeptime = double.Parse(ConfigurationManager.AppSettings["TimeInterval"]);
            DateTime nowdate = DateTime.Now;
            try
            {
                timerDelay.Enabled = false;
                timerDelay.Close();
                if (start == 0)
                {
                    DataExecute execute = new DataExecute();
                    execute.StatisticAttendanceInfos();
                    start = 1;
                }

                double sleeptime = CalNextTime();
                System.Timers.Timer t = new System.Timers.Timer(sleeptime);
                t.Elapsed += new System.Timers.ElapsedEventHandler(inData);    //到达时间的时候执行事件
                t.AutoReset = false;    //设置是执行一次（false）还是一直执行(true)；
                t.Enabled = true;       //是否执行System.Timers.Timer.Elapsed事件； 
                t.Start();
            }
            catch (Exception)
            {
                double sleeptime = CalNextTime();
                System.Timers.Timer t = new System.Timers.Timer(sleeptime);
                t.Elapsed += new System.Timers.ElapsedEventHandler(inData);    //到达时间的时候执行事件
                t.AutoReset = false;    //设置是执行一次（false）还是一直执行(true)；
                t.Enabled = true;       //是否执行System.Timers.Timer.Elapsed事件； 
                t.Start();
            }
        }

        /// <summary>
        /// 计算下一次执行的时间
        /// </summary>
        private double CalNextTime()
        {
            // 当前时间
            TimeSpan currtime = DateTime.Now.TimeOfDay;
            string[] BackUpTime = ConfigurationManager.AppSettings["BackUpTime"].Split(new char[] { ',' });
            double milliseconds = 0;
            int flag = 0;
            for (int i = 0; i < BackUpTime.Length; i++)
            {
                TimeSpan span = new TimeSpan(int.Parse(BackUpTime[i]), 0, 0);
                if (currtime > span)
                {
                    continue;
                }
                flag = 1;
                milliseconds = (span - currtime).TotalMilliseconds;
                break;
            }
            if (milliseconds == 0 && flag == 0)
            {
                TimeSpan temp1 = new TimeSpan(23, 59, 59);
                TimeSpan temp2 = new TimeSpan(0, 0, 0);
                TimeSpan span = new TimeSpan(int.Parse(BackUpTime[0]), 0, 0);
                milliseconds = ((temp1 - currtime) + (span - temp2)).TotalMilliseconds;
            }
            return milliseconds;
        }

        public void inData(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DataExecute execute = new DataExecute();
                execute.StatisticAttendanceInfos();
                timerDelay = new System.Timers.Timer();
                timerDelay.Elapsed += new System.Timers.ElapsedEventHandler(timerDelay_Elapsed);
                timerDelay.Start();
            }
            catch (Exception)
            {
                timerDelay = new System.Timers.Timer(60000);
                timerDelay.Elapsed += new System.Timers.ElapsedEventHandler(timerDelay_Elapsed);
                timerDelay.Start();
            }
        }
    }
}
