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
    /// 执行同步逻辑，如果同步成功，则返回同步后的活动，否则返回同步节点
    /// </summary>
   public class WfSynchroActivityImpl:WFActivityImpl,IWfNode
    {
       /// <summary>
       /// 调用基类的构造函数，从模型库创建活动任务
       /// </summary>
       /// <param name="model_task">模型库实例</param>
       /// <param name="context">当前上下文</param>
       /// <param name="processMgr">工作流管理类</param>
       /// <param name="process">工作流实例</param>
       public WfSynchroActivityImpl(ModelTask model_task,Hashtable context,WFProcessMgrImpl processMgr,IWFProcess process):base(model_task,context,processMgr,process)
       {
    }
       /// <summary>
       /// 调用基类的构造函数，从运行库创建活动任务
       /// </summary>
       /// <param name="workitemid">活动任务ID</param>
       /// <param name="context">当前上下文</param>
       /// <param name="model_task">模型库实例</param>
       /// <param name="processMgr">工作流管理类</param>
       /// <param name="process">工作流实例</param>
       public WfSynchroActivityImpl(String workitemid, Hashtable context, ModelTask model_task, WFProcessMgrImpl processMgr, IWFProcess process)
           : base(workitemid, context, model_task, processMgr, process)
    {
    }

       /// <summary>
       /// 持久化数据到数据库
       /// </summary>
    public new void persist()
    {
        processMgr.ProcessInstanceDao.save_synchroactivity(getLastSynchronizedActivity());
    }



       /// <summary>
       /// 由于最后一个同步任务需要在这个节点持久化时才保存，这里先手动加入
       /// </summary>
       /// <returns></returns>
    public IWFActivity ExtractorActivity() 
    {
        Hashtable synTaskMap = (Hashtable)this.context[SynchronizeTaskConstants.SYNCHRONIZE_TASKMAP];
        ArrayList alreadyFinishedSynchroActivity = null;
        if (synTaskMap != null)
        {
            alreadyFinishedSynchroActivity = (ArrayList)synTaskMap[model_task.TaskName];
        }
        // 由于最后一个同步任务需要在这个节点持久化时才保存，这里先手动加入
        if (alreadyFinishedSynchroActivity == null)
        {
            alreadyFinishedSynchroActivity = new ArrayList();
        }
        alreadyFinishedSynchroActivity.Add(getLastSynchronizedActivity());
        int nFinished = alreadyFinishedSynchroActivity.Count;
        bool synchro_succeed = false;
        if (this.model_task.OpenType == 1)
        {
            //用户在设计期指定前面需要同步的节点数目，保存在TASKENDDATEQUAN中
            if (nFinished == model_task.TaskEndDateCount)
            {
                synchro_succeed = true;
            }
        }
        else
        {
            // 用户在运行时确定节点数目，用户需要注册一个对象，此对象保存在FORMNAME中
            String beanName = this.model_task.FormName;
            try
            {
                //Class clazz = Class.forName(beanName);
                //BeanWrapperImpl bw = new BeanWrapperImpl(clazz);
                //SynchroActivityCaculator sc =
                //    (SynchroActivityCaculator)bw.getWrappedInstance();
                //if (sc.doCaculate(this.context, alreadyFinishedSynchroActivity, this.model_task))
                //{
                //    synchro_succeed = true;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception( "synchronode could not create the calculator!"+ex.ToString());
            }
        }
        if (synchro_succeed)
        {
            // 同步成功，删除在同步表中记录的本项同步节点的所有同步记录
            processMgr.ProcessInstanceDao.
                delete_synchroactivitylist(process.get_instance_data().Id.ToString(), model_task.TaskName);
            // ROLENAME 保存下项动作名称 2007-06-12修改保存taskid
            // String nextTaskName = this.model_task.getRoleName();
            // ModelTask mt = (ModelTask)process.get_modelProcess().getModelTaskList.get(nextTaskName);

            ModelTask mt = processMgr.ModelManager.loadProcessTask(process.get_modelProcess().ProcessID, this.model_task.DeadLineQuantity);

            IWFActivity activity = this.processMgr.WFActivityFactory.create_wfActivity(mt, context, (IWFProcessMgr)processMgr, process);
            // 设置同步任务后下项任务的前项工作
            SavePreviousWorkItem(activity.getWorkItem(), alreadyFinishedSynchroActivity);
            return activity;
        }
        else
        {
            return this;
        }
    }

    private WORKITEMS getLastSynchronizedActivity()
    {

        WORKITEMS previousWorkItem = process.get_previousActivity().getWorkItem();

        WORKITEMS kwpt = new WORKITEMS();
        kwpt.INSTANCEID= (process.get_instance_data().Id);
        kwpt.Id=(previousWorkItem.Id);
        kwpt.PARTICIPANTID=(previousWorkItem.PARTICIPANTID);
        kwpt.PARTICIPANTNAME=(previousWorkItem.PARTICIPANTNAME);
        kwpt.PARTICIPANTNAME2=(previousWorkItem.PARTICIPANTNAME2);
        kwpt.TASKNAME=(previousWorkItem.TASKNAME);
        kwpt.SyTaskName=(this.model_task.TaskName);

        return kwpt;

    }

    private void SavePreviousWorkItem(WORKITEMS parentWorkItem,ArrayList alreadyFinishedSynchroActivity) 
    {
        IList<PREVIOUSWORKITEMS> hs = new List<PREVIOUSWORKITEMS>();
        if (alreadyFinishedSynchroActivity != null && alreadyFinishedSynchroActivity.Count != 0)
        {
            for (int i = 0; i < alreadyFinishedSynchroActivity.Count; i++)
            {
                WORKITEMS kwpt = (WORKITEMS)alreadyFinishedSynchroActivity[i];

                PREVIOUSWORKITEMS pw = new PREVIOUSWORKITEMS();
                pw.PREPARTICIPANTID=(kwpt.PARTICIPANTID);
                pw.PREPARTICIPANTNAME=(kwpt.PARTICIPANTNAME);
                pw.PREPARTICIPANTNAME2=(kwpt.PARTICIPANTNAME2);
                pw.PREVIOUSWORKITERMID=(kwpt.Id);
                pw.PREVIOUSTASKNAME=(kwpt.TASKNAME);
                pw.WORKITEMS=(parentWorkItem);
                hs.Add(pw);
            }

            parentWorkItem.PREVIOUSWORKITEMSs=(hs);
        }
       
    }
    }
}
