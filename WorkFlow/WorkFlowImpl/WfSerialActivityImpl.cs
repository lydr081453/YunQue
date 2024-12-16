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
    /// 如果前项任务仍然是一个串型任务，认为用户在穿行任务中循环，这时将userdrivenflag增加1，表示已经开始串行完成了任务
    /// 在角色解析时会知道何时执行到了最后一个，角色解析插件将标志设置为-1时，表示已经取到最后一个了。
    /// 如果用户提交的动作是-1，且动作类型是顺序动作时，会执行这个动作的所属连线
    /// 但如果所属连线仍然是一个串行的任务，则会重新开始新的一轮循环，如果用户上一次提交的不是一个串行动作，但是这个动作指向了一个
    /// 串行任务，这时，这个任务会重新开始记数即从第一个角色开始
    /// </summary>
    public class WfSerialActivityImpl:WFActivityImpl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model_task"></param>
        /// <param name="context"></param>
        /// <param name="processMgr"></param>
        /// <param name="process"></param>
        public WfSerialActivityImpl(ModelTask model_task, Hashtable context, WFProcessMgrImpl processMgr, IWFProcess process)
            : base(model_task, context, processMgr, process)
       {
           if (process.get_previousActivity() == null)
           {
               this.work_item.SERIALPOINTER = 1;
               return;
           }
        if( process.get_previousActivity().getModelTask().TaskType == WfStateContants.TASKTYPE_SERIALTASK) 
        {
        
        	int actionFlag = Convert.ToInt32(context[ContextConstants.SUBMIT_ACTION_TYPE]);
        	if( actionFlag != WfStateContants.SERIALACTION )
            {
                this.work_item.SERIALPOINTER = 1;
        	}else{
                this.work_item.SERIALPOINTER = process.get_previousActivity().getWorkItem().SERIALPOINTER + 1;        		
        	}
        }
        else{
            this.work_item.SERIALPOINTER = 1;	
        }
        
    }

        /// <summary>
        /// 在运行库中获得活动，这时前项任务是从运行库中提取的
        /// </summary>
        /// <param name="workitemid">活动任务ID</param>
        /// <param name="context">当前上下文</param>
        /// <param name="model_task">模型库实例</param>
        /// <param name="processMgr">工作流管理类实例</param>
        /// <param name="process">工作流实例</param>
        public WfSerialActivityImpl(String workitemid, Hashtable context, ModelTask model_task, WFProcessMgrImpl processMgr, IWFProcess process)
            : base(workitemid, context, model_task, processMgr, process)
        {

        }
    }
}
