using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.IO;
using System.Threading;
using ESP.Administrative.BusinessLogic;

namespace AdministrativeService
{
    public partial class AdministrativeService : ServiceBase
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

        public AdministrativeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            logger.Add("AdministrativeService：服务启动成功");

            logger.Add("AdministrativeService: 启动计时器");
            timerDelay = new System.Timers.Timer();
            timerDelay.Elapsed += new System.Timers.ElapsedEventHandler(timerDelay_Elapsed);
            timerDelay.Start();
        }

        protected override void OnStop()
        {
            logger.Add("AdministrativeService: 服务停止成功");
        }

        void timerDelay_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timerDelay.Enabled = false;
            timerDelay.Close();

            DateTime nowdate = DateTime.Now;

            if (start == 0)
            {
                DataExChange exchange = new DataExChange();
                exchange.StartDateExChange();
                start = 1;
            }

            // 计算下一次执行的时间
            logger.Add("AdministrativeService: 获得下一次执行的时间");
            double sleeptime = double.Parse(ConfigurationManager.AppSettings["TimeInterval"]);
            System.Timers.Timer t = new System.Timers.Timer(sleeptime);
            t.Elapsed += new System.Timers.ElapsedEventHandler(inData);    //到达时间的时候执行事件
            t.AutoReset = false;    //设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;       //是否执行System.Timers.Timer.Elapsed事件； 
            t.Start();
        }

        public void inData(object source, System.Timers.ElapsedEventArgs e)
        {
            logger.Add("AdministrativeService: 开始导入打卡时间信息");
            DataExChange exchange = new DataExChange();
            exchange.StartDateExChange();
            timerDelay = new System.Timers.Timer();
            timerDelay.Elapsed += new System.Timers.ElapsedEventHandler(timerDelay_Elapsed);
            timerDelay.Start();
        }
    }
}
