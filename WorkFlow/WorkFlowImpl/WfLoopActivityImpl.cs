using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowLibary;
using ModelTemplate;
using WorkFlow.Model;

namespace WorkFlowImpl
{
    /// <summary>
    /// 循环模型中的活动任务
    /// 只要不是同步结点，所有任务都只有一向前项任务,此处LOOPactivity是重载基本的activity的保存方法，按照解析出的人员保存工作项。
    /// </summary>
   public class WfLoopActivityImpl:WFActivityImpl
    {
       /// <summary>
       /// 重用基类的构造函数，从模型库中创建活动任务
       /// </summary>
        /// <param name="model_task">模型库实例</param>
       /// <param name="context">当前上下文</param>
       /// <param name="processMgr">工作流管理类实例</param>
       /// <param name="process">工作流实例</param>
        public WfLoopActivityImpl(ModelTask model_task,Hashtable context,WFProcessMgrImpl processMgr,IWFProcess process):base(model_task,context,processMgr,process)
        {
        }

       /// <summary>
        /// 重用基类的构造函数，从运行库中创建活动任务
       /// </summary>
       /// <param name="workitemid">活动任务ID</param>
        /// <param name="context">当前上下文</param>
        /// <param name="model_task">模型库实例</param>
        /// <param name="processMgr">工作流管理类实例</param>
        /// <param name="process">工作流实例</param>
    public WfLoopActivityImpl(String workitemid,Hashtable context,ModelTask model_task,WFProcessMgrImpl processMgr,IWFProcess process):base(workitemid,context,model_task,processMgr,process)
    {
    }
    
    
      /// <summary>
      /// 持久化到数据库
      /// </summary>
    public new void persist()
    {
        IList<TASKFINISHER> taskfinisher = this.work_item.TASKFINISHERs;
        

        for (int i = 0; i < taskfinisher.Count; i++)
        {
            WORKITEMS wi = new WORKITEMS();
            TASKFINISHER tf = taskfinisher[i];
            IList<TASKFINISHER> arr = new List<TASKFINISHER>();
            arr.Add(tf);
            wi.TASKFINISHERs = arr;
            wi.PROCESSINSTANCES = work_item.PROCESSINSTANCES;
            wi.PREVIOUSWORKITEMSs = this.get_previous_workitem(work_item.PREVIOUSWORKITEMSs, wi);
            tf.WORKITEMS = wi;
          //  processMgr.ActivitySubjectContainer.TaskInitializeAfterPersistentSubject.notifyObservers(context);

            this.NotifyPersist(context);
        }
           
    }
    private IList<PREVIOUSWORKITEMS> get_previous_workitem(IList<PREVIOUSWORKITEMS> pw, WORKITEMS wi)
    {
        IList<PREVIOUSWORKITEMS> hs = new List<PREVIOUSWORKITEMS>();
        PREVIOUSWORKITEMS pt ;
        for(int i=0;i<pw.Count;i++)
        {
            pt=pw[i];
            pt.WORKITEMS = wi;
            hs.Add(pt);
            
        }
        return hs;
    }
    }
}
