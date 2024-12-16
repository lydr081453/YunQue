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
    /// 工作任务工厂类，根据工作流模型生成相应的任务活动
    /// </summary>
    public class WFActivityFactoryImpl:IWFActivityFactory
    {
        /// <summary>
        /// 从模型库中获取当前工作流的活动任务
        /// </summary>
        /// <param name="model_task">模型库实例</param>
        /// <param name="context">当前上下文</param>
        /// <param name="processMgr">工作流管理类</param>
        /// <param name="process">工作流操作类</param>
        /// <returns>返回工作流的活动任务</returns>
        public IWFActivity create_wfActivity(ModelTask model_task, Hashtable context, IWFProcessMgr processMgr, IWFProcess process)
        {
             IWFActivity activity = null;

        switch(model_task.TaskType)
        {
            case WfStateContants.TASKTYPE_SERIALTASK:
                activity = new WfSerialActivityImpl(model_task, context, (WFProcessMgrImpl)processMgr, process);
                break;
            case WfStateContants.TASKTYPE_LOOPTASK:
                activity = new WfLoopActivityImpl(model_task,context,(WFProcessMgrImpl)processMgr,process);
                break;
            case WfStateContants.TASKTYPE_SYNNODE:
                activity = (new WfSynchroActivityImpl(model_task,context,(WFProcessMgrImpl)processMgr,process));
                break;
            case WfStateContants.TASKTYPE_SERVERTASK:
            	activity = new WfServerActivityImpl(model_task,context,(WFProcessMgrImpl)processMgr,process);
            	break;
            default://4  3  1
                activity = new WFActivityImpl(model_task, context, (WFProcessMgrImpl)processMgr, process);
                break;
        }
        //if (Type.GetType("activity").ToString()=="IWfNode")
        //{
            // 如果活动不是一个人机交互的活动，而是一个node，则调用抽取方法，抽取node背后的活动
            //
        //    activity = ((IWfNode)activity).ExtractorActivity();
        //}
        //return activity;
        return activity;
        }


        /// <summary>
        /// 从运行库中获取当前工作流的活动任务
        /// </summary>
        /// <param name="workitemid">活动任务ID</param>
        /// <param name="context">当前上下文</param>
        /// <param name="model_task">模型库实例</param>
        /// <param name="processMgr">工作流管理类</param>
        /// <param name="process">工作流类</param>
        /// <returns>从运行库中获取当前工作流的活动任务</returns>
        public IWFActivity create_wfActivity(String workitemid, Hashtable context, ModelTask model_task, IWFProcessMgr processMgr, IWFProcess process)
        {
            IWFActivity activity = null;
            switch (model_task.TaskType)
            {
                case WfStateContants.TASKTYPE_SERVERTASK:
                    activity = new WfServerActivityImpl(workitemid, context, model_task, (WFProcessMgrImpl)processMgr, process);
                    break;
                case WfStateContants.TASKTYPE_SYNNODE:
                    activity = (new WfSynchroActivityImpl(workitemid, context, model_task, (WFProcessMgrImpl)processMgr, process));
                    break;
                default:
                    activity = new WFActivityImpl(workitemid, context, model_task, (WFProcessMgrImpl)processMgr, process);
                    break;
            }
            return activity;
        }
    }
}
