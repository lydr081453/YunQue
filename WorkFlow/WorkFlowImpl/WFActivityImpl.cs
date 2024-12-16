using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlow.Model;
using ModelTemplate;
using WorkFlowLibary;

namespace WorkFlowImpl
{
    /// <summary>
    /// 活动任务父类，继承了观察者模式的主题接口和活动任务的接口类
    /// </summary>
    public class WFActivityImpl :MySubject, IWFActivity
    {
        protected ModelTask model_task;

        protected WORKITEMS work_item;

        protected Hashtable context;

        protected WFProcessMgrImpl processMgr;

        protected IWFProcess process;

        /// <summary>
        /// 从模型库中初始化下一个任务，这时该任务的前项任务是从process中保存的lastactivity中获得的
        /// </summary>
        /// <param name="model_task">模型库的实例</param>
        /// <param name="context">当前上下文</param>
        /// <param name="processMgr">工作流管理类</param>
        /// <param name="process">工作流操作类实例</param>
        public WFActivityImpl(ModelTask model_task, Hashtable context,
                WFProcessMgrImpl processMgr, IWFProcess process)
        {

            work_item = new WORKITEMS();
            work_item.STATE=(WfStateContants.TASKSTATE_INACTIVE);
            work_item.PROCESSNAME = process.get_modelProcess().Processname;
            work_item.PROCESSID =Convert.ToInt64( process.get_modelProcess().ProcessID);
            work_item.TASKNAME = model_task.TaskName;
            work_item.TASKDISPLAYNAME = model_task.TaskName;
            work_item.TASKID = Convert.ToInt64(model_task.TaskID);
            work_item.TASKTYPE = Convert.ToInt32(model_task.TaskType);
            work_item.RoleID = model_task.RoleName;
          //  ModelRoleSet mrs = process.get_modelProcess().ModelRoleSet;

            //if (mrs == null)
            //{
            //    throw new Exception("roleset not found");
            //}
            work_item.STARTDATE = DateTime.Now;
            work_item.ReminderCount = 0;

           // work_item.setTaskinstructions(model_task.getTaskinstructions());

            if (process.get_previousActivity() != null)
            {
                PREVIOUSWORKITEMS pw = new PREVIOUSWORKITEMS();
                pw.PREPARTICIPANTID = (process.get_previousActivity().getWorkItem().PARTICIPANTID);
                pw.PREPARTICIPANTNAME = (process.get_previousActivity().getWorkItem().PARTICIPANTNAME);

                pw.INSTANCEID=(process.get_previousActivity().getWorkItem().PROCESSINSTANCES.Id.ToString());

                pw.PREPARTICIPANTNAME2=(process.get_previousActivity().getWorkItem().PARTICIPANTNAME2);

                pw.PREVIOUSWORKITERMID=(process.get_previousActivity().getWorkItem().Id);

                pw.PREVIOUSTASKNAME=(process.get_previousActivity().getWorkItem().TASKNAME);
                
                pw.WORKITEMS = work_item;

                IList<PREVIOUSWORKITEMS> pws = new List<PREVIOUSWORKITEMS>();
                pws.Add(pw);
                work_item.PREVIOUSWORKITEMSs = pws;
            }
            initialize(model_task, context, processMgr, process);

        }

        /// <summary>
        /// 在运行库中获得活动任务，这时前项任务是从运行库中提取的
        /// </summary>
        /// <param name="workitemid">活动任务ID</param>
        /// <param name="context">当前上下文</param>
        /// <param name="model_task">模型库实例</param>
        /// <param name="processMgr">工作流管理类</param>
        /// <param name="process">工作流操作类实例</param>
        public WFActivityImpl(String workitemid, Hashtable context, ModelTask model_task,
                WFProcessMgrImpl processMgr, IWFProcess process)
        {
            work_item = processMgr.ProcessInstanceDao.load_workitem(workitemid);
            initialize(model_task, context, processMgr, process);
        }

        private void initialize(ModelTask model_task, Hashtable context,
                WFProcessMgrImpl processMgr, IWFProcess process)
        {
            this.model_task = model_task;
            this.context = context;
            this.processMgr = processMgr;
            this.process = process;
        }

        /// <summary>
        /// 活动任务从暂停状态恢复运行
        /// </summary>
        public void resume()
        {
            if (this.getWorkItem().STATE != WfStateContants.TASKSTATE_SUSPENDED)
            {
                throw new Exception("the task state is not suspending!");
            }
            processMgr.ProcessInstanceDao.update_workitem_state(work_item.Id, WfStateContants.TASKSTATE_INACTIVE);
           // processMgr.ActivitySubjectContainer.TaskResumeSubject
           //     .notifyObservers(context);

            this.NotifyResume(context);
        }

        /// <summary>
        /// 活动任务暂停
        /// </summary>
        public void suspend()
        {
            if (this.getWorkItem().STATE > WfStateContants.TASKSTATE_SUSPENDED)
            {
                throw new Exception("the task state could not suspended!");
            }
            processMgr.ProcessInstanceDao.update_workitem_state(
                work_item.Id, WfStateContants.TASKSTATE_SUSPENDED);
        
            this.NotifySuspend(context);
        }

        /// <summary>
        /// 活动任务开始
        /// </summary>
        public void start()
        {
          //  processMgr.ActivitySubjectContainer.TaskInitializeSubject
          //      .notifyObservers(context);
            processMgr.ProcessInstanceDao.update_workitem_state(work_item.Id, WfStateContants.PROCESS_ACTIVE);
            this.NotifyStart(context);
        }

        /// <summary>
        /// 该活动任务的参与角色
        /// </summary>
        public void taskrole()
        {
         //   processMgr.ActivitySubjectContainer.TaskRoleSubject
         //       .notifyObservers(context);

            
        }

        /// <summary>
        /// 完成该活动任务
        /// </summary>
        public void complete()
        {
            int actionFlag = Convert.ToInt32(context[ContextConstants.SUBMIT_ACTION_TYPE]);
            WFUSERS wuser = (WFUSERS)context[ContextConstants.CURRENT_USER];
            if (wuser == null)
            {
                throw new Exception("no participant user!");
            }
            // 2008-3-18修改
            // 由于需要增加用户在首步骤提交的草稿箱功能，并且用户可以删除掉这些处于草稿状态的流程，因此在这里增加
            // 目前的判断逻辑是：
            // 1.
            // 如果用户提交的是草稿动作，并且，当前任务没有前项任务（说明是一个启动任务），并且这个任务的流程实例没有activePersonid（说明是用户通过手工启动的，不是通过接口调用的）
            // 对于符合这些逻辑的工作项，保存当前人信息，并且将工作项状态设置为草稿状态。
            // 这项功能待修改！
            if (actionFlag == WfStateContants.DRAFTACTION)
            {
                ArrayList set = (ArrayList)this.work_item.PREVIOUSWORKITEMSs;
                if (set == null || set.Count == 0)
                {
                    if (this.getWorkItem().PROCESSINSTANCES.ACTIVEPERSONID == 0)
                    {
                        work_item.PARTICIPANTID=(wuser.Id);
                        work_item.PARTICIPANTNAME=(wuser.USERNAME);
                        work_item.PARTICIPANTNAME2=(wuser.USERNAME2);
                        work_item.STATE=WfStateContants.TASKSTATE_DRAFT;
                    }
                }
            }
            // 如果不是保存草稿状态，则设置任务的完成标识，如果是草稿动作，则不设置这个完成标识。
            // 由于在待办中所有任务都是草稿状态的任务，因此去掉草稿箱功能
            // 草稿状态也不保存完成人
            //if (actionFlag != WfStateContants.DRAFTACTION)
            else
            {
                work_item.PARTICIPANTID=(wuser.Id);
                work_item.PARTICIPANTNAME=(wuser.USERNAME);
                work_item.PARTICIPANTNAME2 = (wuser.USERNAME2);
                work_item.STATE=WfStateContants.TASKSTATE_COMPLETED;
                work_item.ENDDATE=DateTime.Now;

                work_item.ActionTackenName=context[ContextConstants.SUBMIT_ACTION_NAME].ToString();
                work_item.PROCESSINSTANCES=this.process.get_instance_data();
                work_item.SubmitDisplayName = context[ContextConstants.SUBMIT_ACTION_DISPLAYNAME].ToString();
                work_item.PARTICIPANTTYPE=Convert.ToInt16(context[ContextConstants.PARTICIPANTTYPE]);
            }
         
            processMgr.ProcessInstanceDao.update_workitem(work_item);

            this.NotifyComplete(context);
        }

        /// <summary>
        /// 激活该活动任务状态从40到20
        /// </summary>
        public void active()
        {
            if (this.getWorkItem().STATE != WfStateContants.TASKSTATE_INACTIVE)
            {
                throw new Exception("the task state is not inactive!");
            }
            switch(work_item.TASKTYPE.ToString())
            {
                case WfStateContants.TASKTYPE_EXCLUSIVE:
                    processMgr.ProcessInstanceDao.updateXorWorkItemActive(work_item.INSTANCEID.ToString(),work_item.Id.ToString());
                    break;
                default:
                   processMgr.ProcessInstanceDao.update_workitem_state(work_item.Id, WfStateContants.PROCESS_ACTIVE);
                   break;
        }

            this.NotifyActive(context);
        }


        /// <summary>
        /// 读取活动项目，通知事件
        /// </summary>
        public void load()
        {
         //   processMgr.ActivitySubjectContainer.TaskLoadSubject
          //         .notifyObservers(context);

            this.NotifyLoad(context);
        }

        /// <summary>
        /// 持久化活动任务，保存到数据库
        /// </summary>
        public void persist()
        {
            work_item.Id=processMgr.ProcessInstanceDao.save_workitem(work_item);
         //   processMgr.ActivitySubjectContainer
         //        .TaskInitializeAfterPersistentSubject.notifyObservers(
          //               context);

            this.NotifyPersist(context);
        }

        /// <summary>
        /// 取消活动任务，从数据库中更改任务状态
        /// </summary>
        public void cancel()
        {
            processMgr.ProcessInstanceDao
            .update_workitem_state(work_item.Id,
                    WfStateContants.TASKSTATE_TERMINATED);

          //  processMgr.ActivitySubjectContainer.TaskCancelSubject
          //      .notifyObservers(context);

            this.NotifyCancel(context);
        }

        /// <summary>
        /// 获取当前活动任务所属的模板库实例
        /// </summary>
        /// <returns></returns>
        public ModelTask getModelTask()
        {
            return model_task;
        }

        /// <summary>
        /// 从数据库中获取数据实例
        /// </summary>
        /// <returns></returns>
        public WORKITEMS getWorkItem()
        {
            return this.work_item;
        }


        private Hashtable wrapContext()
        {
            return context;

        }
    }
}
