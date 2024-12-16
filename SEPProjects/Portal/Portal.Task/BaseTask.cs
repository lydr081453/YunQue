using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Task
{
    public class BaseTask : Portal.Core.ITask
    {
        private Portal.Model.TaskInfo _info;
        public Portal.Model.TaskInfo Info
        {
            get { return _info; }
            set { _info = value; }
        }

        public BaseTask() { }

        #region ITask 成员

        /// <summary>
        /// 初始化任务
        /// </summary>
        /// <param name="obj"></param>
        public void Init(object obj)
        {
            Portal.Data.TestLogger.Log("BaseTask->Init", string.Format("被调用"));
            if (obj != null)
            {
                Portal.Data.TestLogger.Log("BaseTask->Init", string.Format("传入数据不为空"));
                try
                {
                    _info = obj as Portal.Model.TaskInfo;
                    _info.State = false;
                    Portal.Data.TestLogger.Log("BaseTask->Init", string.Format("调用Execute函数"));
                    Execute();
                    Portal.Data.TestLogger.Log("BaseTask->Init", string.Format("调用Complete函数"));
                    Complete();
                }
                catch (Exception e)
                {
                    Portal.Data.TestLogger.Log("BaseTask->Init", string.Format("初始化失败，异常信息为：{0}", e.ToString()));
                    Error(e);
                }
            }
        }

        /// <summary>
        /// 任务逻辑
        /// </summary>
        public virtual void Execute()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("BaseTask");
#endif
        }

        /// <summary>
        /// 任务执行完毕
        /// 连续错误计数清零
        /// 设置执行成功标志为true
        /// </summary>
        public virtual void Complete()
        {
            Portal.Data.TestLogger.Log("BaseTask->Complete", string.Format("被调用"));
            _info.State = true;
            _info.ErrorCount = 0;
            _info.IsSuccess = true;
#if DEBUG
            System.Diagnostics.Debug.WriteLine(string.Format("{2} Complete : {0}, state : {1}", DateTime.Now.ToLongTimeString(), _info.State.ToString(), _info.Name));
#endif
        }

        /// <summary>
        /// 错误处理函数
        /// 记录异常信息
        /// 累计连续错误次数
        /// 执行成功标志设置为false
        /// </summary>
        /// <param name="e"></param>
        public virtual void Error(Exception e)
        {
            Portal.Data.TestLogger.Log("BaseTask->Error", string.Format("被调用"));
            if (_info != null)
            {
                _info.IsSuccess = false;
                _info.ErrorCount++;
                _info.LastErrorTime = DateTime.Now;
                _info.LastError = e.ToString();
                _info.State = true;
            }
            else
            {
                throw new Exception("未知错误，无法转换TaskInfo");
            }
        }

        #endregion
    }
}
