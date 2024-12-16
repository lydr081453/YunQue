using System;
using System.Xml;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Text;

using Portal.Common;
using Portal.Model;

namespace Portal.Core
{
    /// <summary>
    /// 任务调度——单例
    /// 1.取任务的配置信息
    /// 2.每60秒触发一次任务调度，在任务配置文件中设置的Period是以任务调度时间（60秒）为单位的等待次数
    ///   例如：Period设置为2，则每等待两个任务调度周期执行一次
    /// 3.取所有任务的执行间隔
    /// 4.到达执行时间的任务交给ThreadPool去执行，并设置任务执行状态为启动，结束后要设置执行状态为结束
    /// 5.任务执行过程中出现错误的处理机制交给任务自身，但是任务需要向Runtime提交异常信息，Runtime记录
    ///   任务提交的最后一次异常信息，并累计异常次数，如果连续异常次数超过重试次数设定值，则将停止执行此
    ///   任务若干时间，停止的时间也由配置程序的设置决定
    /// </summary>
    public class Runtime
    {
        private static Runtime _instance;
        private static object locker = new object();
        private Timer t;
        private Dictionary<string, TaskInfo> tasksState;
        private Portal.Common.Configuration.Task.TaskConfigInfo taskConfig;
        private static object visitor_locker = new object();

        private Runtime()
        {
            PreInitTask();
            InitTask();
            StartTask();
        }

        /// <summary>
        /// Runtime开始执行
        /// </summary>
        public static void Start()
        {
            Portal.Data.TestLogger.Log("HttpRuntime->Start", "被访问");
            if (_instance == null)
            {
                lock (locker)
                {
                    if (_instance == null)
                    {
                        try
                        {
                            //创建Runtime实例
                            Portal.Data.TestLogger.Log("HttpRuntime->Start", "创建实例");
                            _instance = new Runtime();
                            Portal.Data.TestLogger.Log("HttpRuntime->Start", "实例创建完毕");
                        }
                        catch (Exception e)
                        {
                            Portal.Data.TestLogger.Log("HttpRuntime->Start", string.Format("创建实例出现异常，异常信息：{0}", e.ToString()));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 重新启动Runtime
        /// </summary>
        public static void Restart()
        {
            Portal.Data.TestLogger.Log("HttpRuntime->Restart", "被访问");
            if (_instance != null)
            {
                Portal.Data.TestLogger.Log("HttpRuntime->Restart", "调用实例的StopTask函数");
                _instance.StopTask();
                Portal.Data.TestLogger.Log("HttpRuntime->Restart", "调用实例的StopTask函数结束");
            }
            else
            {
                Portal.Data.TestLogger.Log("HttpRuntime->Restart", "调用实例的Start函数");
                Start();
                Portal.Data.TestLogger.Log("HttpRuntime->Restart", "调用实例的Start函数结束");
                return;
            }
            bool sign = true;
            int count = 0;
            //等待所有任务结束
            Portal.Data.TestLogger.Log("HttpRuntime->Restart", "等待所有任务结束");
            lock (_instance.tasksState)
            {
                Portal.Data.TestLogger.Log("HttpRuntime->Restart", "锁定实例的taskState");
                while (true)
                {
                    Portal.Data.TestLogger.Log("HttpRuntime->Restart", "等待任务结束循环开始");
                    foreach (string key in _instance.tasksState.Keys)
                    {
                        Portal.Data.TestLogger.Log("HttpRuntime->Restart", string.Format("任务主键：{0}，开始", key));
                        if (_instance.tasksState[key].State == null)
                        {
                            _instance.tasksState[key].State = true;
                        }
                        Portal.Data.TestLogger.Log("HttpRuntime->Restart", string.Format("任务主键：{0}，状态", _instance.tasksState[key].State));
                        sign = (bool)(_instance.tasksState[key].State) && sign;
                    }
                    //如果所有任务都完成或超时则退出
                    if (!sign && count <= Portal.Common.Global.TASK_STOP_MAX_WAIT_TIMES)
                    {
                        Portal.Data.TestLogger.Log("HttpRuntime->Restart", string.Format("有任务没有完成，等待次数{0}", count));
                        Thread.Sleep(Portal.Common.Global.TASK_STOP_WAIT_TIME);
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
                Portal.Data.TestLogger.Log("HttpRuntime->Restart", string.Format("等待所有任务结束退出"));
            }
            //重新读取任务配置文件
            Portal.Data.TestLogger.Log("HttpRuntime->Restart", string.Format("调用实例的PreInitTask函数"));
            _instance.PreInitTask();
            Portal.Data.TestLogger.Log("HttpRuntime->Restart", string.Format("调用实例的InitTask函数"));
            _instance.InitTask();
            Portal.Data.TestLogger.Log("HttpRuntime->Restart", string.Format("调用实例的StartTask函数"));
            _instance.StartTask();
        }

        /// <summary>
        /// 预初始化任务
        /// 读取配置文件
        /// </summary>
        private void PreInitTask()
        {
            Portal.Data.TestLogger.Log("HttpRuntime->PreInitTask", string.Format("被调用"));
            //创建任务状态列表
            tasksState = new Dictionary<string, TaskInfo>();
            //任务配置文件名称
            string taskConfigFileName = System.Web.HttpRuntime.AppDomainAppPath + "task.config";
            //任务配置文件结构
            Portal.Data.TestLogger.Log("HttpRuntime->PreInitTask", string.Format("调用TaskConfigInfo函数读取配置文件，配置文件名称：{0}", taskConfigFileName));
            taskConfig = new Portal.Common.Configuration.Task.TaskConfigInfo(taskConfigFileName);
            Portal.Data.TestLogger.Log("HttpRuntime->PreInitTask", string.Format("退出"));
        }

        /// <summary>
        /// 初始化任务
        /// 设置任务初始数据
        /// 如果任务数据已经存在则保留——为避免覆盖任务的执行状态
        /// </summary>
        /// <param name="taskConfig"></param>
        private void InitTask()
        {
            Portal.Data.TestLogger.Log("HttpRuntime->InitTask", string.Format("被调用"));
            for (int i = 0; i < taskConfig.Items.Count; ++i)
            {
                Portal.Data.TestLogger.Log("HttpRuntime->InitTask", string.Format("初始化第{0}个任务", i));
                if (!tasksState.ContainsKey(taskConfig.Items[i].Name))
                {
                    Portal.Data.TestLogger.Log("HttpRuntime->InitTask", string.Format("当前taskState中不包含任务{0}", taskConfig.Items[i].Name));
                    tasksState[taskConfig.Items[i].Name] = new TaskInfo();
                    tasksState[taskConfig.Items[i].Name].Name = taskConfig.Items[i].Name;
                    tasksState[taskConfig.Items[i].Name].LastExcuteTime = DateTime.Now.AddMilliseconds(-(double)(Portal.Common.Global.TASK_TIMER_PERIOD * taskConfig.Items[i].Period));
                    try
                    {
                        tasksState[taskConfig.Items[i].Name].Instance = (ITask)Activator.CreateInstance(Type.GetType(taskConfig.Items[i].Provider));
                    }
                    catch (Exception e)
                    {
                        Portal.Data.TestLogger.Log("HttpRuntime->InitTask", string.Format("创建第{0}个任务实例失败，异常信息：{1}", i, e.ToString()));
                    }
                    Portal.Data.TestLogger.Log("HttpRuntime->InitTask", string.Format("创建第{0}个任务成功", i));
                }
                Portal.Data.TestLogger.Log("HttpRuntime->InitTask", string.Format("当前taskConfig中已经包含任务{0}", taskConfig.Items[i].Name));
            }
            Portal.Data.TestLogger.Log("HttpRuntime->InitTask", string.Format("初始化任务结束"));
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        private void StartTask()
        {
            Portal.Data.TestLogger.Log("HttpRuntime->StartTask", string.Format("被调用，开始启动Timer"));
            //启动任务Timer
            try
            {
                t = new Timer(new TimerCallback(ExcuteTask), taskConfig, 0, Portal.Common.Global.TASK_TIMER_PERIOD);
            }
            catch(Exception e)
            {
                Portal.Data.TestLogger.Log("HttpRuntime->StartTask", string.Format("启动Timer失败，异常信息：{0}", e.ToString()));
            }
            Portal.Data.TestLogger.Log("HttpRuntime->StartTask", string.Format("Timer启动成功，回调函数为ExcuteTask，调用结束"));
        }

        /// <summary>
        /// 定时重新读取任务配置文件
        /// 通过Restart实现
        /// </summary>
        /// <param name="taskConfig"></param>
        /// <returns></returns>
        public bool ReloadTaskConfig()
        {
            Portal.Data.TestLogger.Log("HttpRuntime->ReloadTaskConfig", string.Format("被调用"));
            TimeSpan t1 = DateTime.Now - taskConfig.LastReadTaskConfigTime;
            TimeSpan t2 = new TimeSpan(0, taskConfig.ReloadMinutes, 0);
            if (t1.Ticks >= t2.Ticks)
            {
                Portal.Data.TestLogger.Log("HttpRuntime->ReloadTaskConfig", string.Format("调用PreInitTask"));
                PreInitTask();
            }
            Portal.Data.TestLogger.Log("HttpRuntime->ReloadTaskConfig", string.Format("调用结束"));
            return false;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="obj"></param>
        public void ExcuteTask(object obj)
        {
        //    Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("被调用"));
            DateTime EntryTime = DateTime.Now;
            Portal.Common.Configuration.Task.TaskConfigInfo taskConfig = null;
        //    Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("将传入参数obj转换为Portal.Common.Configuration.Task.TaskConfigInfo"));
            try
            {
                taskConfig = obj as Portal.Common.Configuration.Task.TaskConfigInfo;
            }
            catch (Exception e)
            {
                Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("转换失败，异常退出，异常信息：{0}", e.ToString()));
                return;
            }
            //if (ReloadTaskConfig())
            //{
            //    return;
            //}
        //    Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("开始执行任务"));
            foreach (Portal.Common.Configuration.Task.TaskConfigItemInfo item in taskConfig.Items)
            {
               // Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("开始执行任务"));
                if (tasksState.ContainsKey(item.Name))
                {
                  //  Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("开始执行任务{0}", item.Name));
                    lock (tasksState[item.Name].State)
                    {
                    //    Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("锁定tasksState[{0}].State", item.Name));
                        //上一次任务是否已经执行完毕
                        if ((bool)tasksState[item.Name].State)
                        {
                       //     Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("上一次任务{0}已经执行完毕", item.Name));
                            ///判断任务是否可以执行，如果连续错误计数超过任务允许的Retry次数，则Sleep任务；
                            ///Sleep任务就是将上一次执行时间向后设置SleepTime（在任务配置文件中设置这个值）
                            if (tasksState[item.Name].ErrorCount >= item.Retry)
                            {
                            //    Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("任务{0}重试次数已经超过设定值，休眠任务，并重置重试次数为0", item.Name));
                                tasksState[item.Name].Sleep(item.SleepTime);
                                tasksState[item.Name].ErrorCount = 0;
                                break;
                            }
                            //计算任务是否处于可以执行的时间
                         //   Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("计算任务{0}是否在可执行时间", item.Name));
                            TimeSpan period = new TimeSpan(0, 0, 0, 0, Portal.Common.Global.TASK_TIMER_PERIOD * item.Period);
                            TimeSpan interval = DateTime.Now - tasksState[item.Name].LastExcuteTime;
                            if (interval.Ticks >= period.Ticks)
                            {
                         //       Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("任务{0}在可执行时间", item.Name));
                                tasksState[item.Name].LastExcuteTime = EntryTime;
                         //       Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("执行任务{0}", item.Name));
                                try
                                {
                                    ThreadPool.QueueUserWorkItem(new WaitCallback(((ITask)tasksState[item.Name].Instance).Init), tasksState[item.Name]);
                                }
                                catch (Exception e)
                                {
                                    Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("任务{0}执行失败，异常信息{1}", item.Name, e.ToString()));
                                }
                         //       ESP.Logging.Logger.Add(string.Format("{0} Excute : {1}, state : {2}", item.Name, DateTime.Now.ToLongTimeString(), tasksState[item.Name].State.ToString()), "Portal Task");
                            }
                            else
                            {
                       //         Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("任务{0}还未达到可执行时间", item.Name));
                            }
                        }
                        else
                        {
                            //记录未能执行的时间
                      //      Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("任务{0}未能执行", item.Name));
                        }
                    }
                }
                else
                {
                 //   Portal.Data.TestLogger.Log("HttpRuntime->ExcuteTask", string.Format("任务{0}不再tasksState中", item.Name));
                }
            }
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public void StopTask()
        {
            Portal.Data.TestLogger.Log("HttpRuntime->StopTask", "StopTask被访问");
            try
            {
                if (t != null)
                {
                    t.Dispose();
                    t = null;
                }
            }
            catch (Exception e)
            {
                Portal.Data.TestLogger.Log("HttpRuntime->StopTask", string.Format("StopTask发生异常，异常信息：{0}", e.ToString()));
            }
        }

        /// <summary>
        /// 停止Runtime
        /// </summary>
        public static void Stop()
        {
            Portal.Data.TestLogger.Log("HttpRuntime->StopTask", "Stop被访问");
            try
            {
                if (_instance != null)
                {
                    _instance.StopTask();
                    _instance = null;
                }
            }
            catch(Exception e)
            {
                Portal.Data.TestLogger.Log("HttpRuntime->StopTask", string.Format("Stop发生异常，异常信息：{0}", e.ToString()));
            }
        }

        /// <summary>
        /// 获取Task运行状态数据
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, TaskInfo> GetTaskRuntimeInfo()
        {
            Portal.Data.TestLogger.Log("HttpRuntime->StopTask", "GetTaskRuntimeInfo被访问");
            return _instance.tasksState;
        }
    }
}
