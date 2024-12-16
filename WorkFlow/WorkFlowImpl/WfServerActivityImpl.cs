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
    /// 服务器任务的实现类，对于服务器任务只由基类执行两个
    /// </summary>
    public class WfServerActivityImpl:WFActivityImpl
    {
        /// <summary>
        /// 调用基类的构造函数实现，创建活动任务实例
        /// </summary>
        /// <param name="model_task">模型库实例</param>
        /// <param name="context">当前上下文</param>
        /// <param name="processMgr">工作流管理类实例</param>
        /// <param name="process">工作流实例</param>
        public WfServerActivityImpl(ModelTask model_task,Hashtable context,WFProcessMgrImpl processMgr,IWFProcess process): base(model_task,context,processMgr,process)
        {
       
    }
        /// <summary>
        /// 从数据库中创建活动任务
        /// </summary>
        /// <param name="workitemid">活动任务ID</param>
        /// <param name="context">当前上下文</param>
        /// <param name="model_task">模型库实例</param>
        /// <param name="processMgr">工作流管理类实例</param>
        /// <param name="process">工作流实例</param>
    public WfServerActivityImpl(String workitemid,Hashtable context,ModelTask model_task,WFProcessMgrImpl processMgr,IWFProcess process):base(workitemid,context,model_task,processMgr,process)
    {
        
    }

        /// <summary>
        /// 完成当前活动任务
        /// </summary>
    public new void complete()
    {
        work_item.STATE=(Convert.ToInt32(WfStateContants.TASKSTATE_COMPLETED));
        work_item.ENDDATE = DateTime.Now;

     //   processMgr.ActivitySubjectContainer.TaskCompleteSubject.notifyObservers(context);

       

        String actionName = context[ContextConstants.SUBMIT_ACTION_NAME].ToString();
      
        if( actionName == null ||actionName=="" ) 
        {
    		//actionName = base.model_task.getAutoexeactionname();
        }

        context.Add(ContextConstants.SUBMIT_ACTION_NAME,actionName);
      //  ModelAction ma = (ModelAction)model_task.getModelActionList().get(actionName);
        work_item.PARTICIPANTNAME=(actionName);
        work_item.PROCESSINSTANCES=(this.process.get_instance_data());
       // work_item.TASKDISPLAYNAME=(ma.getActiondisplayname());
        processMgr.ProcessInstanceDao.update_workitem(work_item);

        this.NotifyComplete(context);
    }
    }
}
