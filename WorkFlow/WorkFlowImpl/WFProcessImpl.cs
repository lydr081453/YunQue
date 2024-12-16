using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowLibary;
using ModelTemplate;
using WorkFlow.Model;
using System.Threading;

namespace WorkFlowImpl
{
    /// <summary>
    /// 该类继承自IWFProcess，由WFProcessMgrImpl类创建，工作流操作类
    /// </summary>
    public class WFProcessImpl :MySubject, IWFProcess
    {
        private ModelProcess modelData;
    private PROCESSINSTANCES process_instance;
    // 当后续环节有多个任务时，这里会同时包含多个WfActivity;
    private IWFActivity lastActivity;
    // 这里保存的前项任务是当一个环节向下推进后，当前环节被保存在这里，这种方式除非遇到同步结点，这里保存的是
    // 最后一项任务的结点。
    private IWFActivity previousActivity;
    public Hashtable context;
    public WFProcessMgrImpl processMgr;
    // 设置对于当前的流程实例对象是否检查完成状态
    // 因为在某项任务完成后，当这项任务发现没有后续任务后，会设置这个标识，
    // 当外界调用完成流程时，会检查这个状态
    // 当这个状态为true时，就去数据库中检查，否则不检查状态
    private bool isCheckProcessCompleteState = false;
    // 在这个流程对象中持续发现的服务器任务列表，这是一个缓存，在trycomplete中使用
    private ArrayList serverTaskList = new ArrayList();


 
        /// <summary>
        /// 在模型库中新构造一个流程实例时使用    
        /// </summary>
        /// <param name="modelData"></param>
        /// <param name="context"></param>
        /// <param name="processMgr"></param>
        public WFProcessImpl(ModelProcess modelData, Hashtable context, WFProcessMgrImpl processMgr)
        {
            process_instance = new PROCESSINSTANCES();
            process_instance.STARTDATE=(DateTime.Now);
            process_instance.PROCESSID=Convert.ToInt64(modelData.ProcessID);
            process_instance.PROCESSNAME=(modelData.Processname);
            process_instance.PROCESSINSTANCESTATE=(WfStateContants.PROCESS_ACTIVE);
            initialize(modelData, context, processMgr);
        }
        /// <summary>
        /// 在运行库中新构造一个流程实例时使用
        /// </summary>
        /// <param name="instanceid"></param>
        /// <param name="context"></param>
        /// <param name="modelData"></param>
        /// <param name="processMgr"></param>
        public WFProcessImpl(String instanceid, Hashtable context, ModelProcess modelData, WFProcessMgrImpl processMgr)
        {
            this.process_instance = processMgr.ProcessInstanceDao.load_processinstance(instanceid);
            initialize(modelData, context, processMgr);
        }

        private void initialize(ModelProcess modelData, Hashtable context, WFProcessMgrImpl processMgr)
        {
            this.modelData = modelData;
            this.context = context;
            this.processMgr = processMgr;
            context.Add(ContextConstants.PROCESS_IMPL, this);
        }

        /// <summary>
        /// 从暂停状态中恢复
        /// </summary>
        public void resume()
        {
            processMgr.ProcessInstanceDao.update_process_state(this.get_instance_data().Id, WfStateContants.PROCESS_ACTIVE);
           this.NotifyResume(context);
        }

       /// <summary>
       /// 暂停该工作流
       /// </summary>
        public void suspend()
        {
            processMgr.ProcessInstanceDao.update_process_state(this.get_instance_data().Id, WfStateContants.PROCESS_SUSPENDED);
          this.NotifySuspend(context);
        }

        /// <summary>
        /// 开始工作流
        /// </summary>
        /// <param name="initiators">流程发起者</param>
        /// <param name="startTaskName">流程名称</param>
        public void start(WFUSERS[] initiators, String startTaskName)
        {
            if (initiators.GetLength(0) > 0)
            {
                process_instance.INITIATORNAME=(initiators[0].USERNAME);
                process_instance.INITIATORID=(initiators[0].Id);
                process_instance.INITIATORNAME2=(initiators[0].USERNAME2);

            }
           // processMgr.ProcessSubjectContainer.ProcessStartSubject.notifyObservers(context);
            start_task(startTaskName);
            this.NotifyStart(context);
         
        }

        /// <summary>
        /// 该工作流完成
        /// </summary>
        public void complete()
        {
            processMgr.ProcessInstanceDao.update_process_state(this.get_instance_data().Id, WfStateContants.PROCESS_COMPLETED);
            this.NotifyComplete(context);
        }

        /// <summary>
        /// 无论如何强行中止该工作流
        /// </summary>
        public void terminate()
        {
            processMgr.ProcessInstanceDao.update_process_state(this.get_instance_data().Id, WfStateContants.PROCESS_TERMINATED);
            this.NotifyTerminate(context);
        }

        /// <summary>
        /// 该工作流异常中止，作废
        /// </summary>
        public void abort()
        {
            processMgr.ProcessInstanceDao.update_process_state(this.get_instance_data().Id, WfStateContants.PROCESS_ABORTED);
            this.NotifyAbort(context);
        }

        /// <summary>
        /// 从模型库中重新构造一个任务实例
        /// </summary>
        /// <param name="taskName">任务名称</param>
        protected void start_task(String taskName)
        {
            IList<ModelTask> mts=modelData.ModelTaskList;
            ModelTask mt=null;

            for(int i=0;i<mts.Count;i++)
            {
               if(mts[i].TaskName==taskName)
               {
                   mt=mts[i];
                   break;
               }
            }

            if (mt == null)
            {
                throw new Exception("the task name is :" + taskName);
            }
            this.lastActivity = this.processMgr.WFActivityFactory.create_wfActivity(mt, context, (IWFProcessMgr)processMgr, this);
            this.lastActivity.start();
            
        }


        /// <summary>
        /// 在运行库中获得流程实例
        /// </summary>
        /// <param name="workitemid">任务ID</param>
        /// <param name="taskName">任务名称</param>
        public void load_task(String workitemid, String taskName)
        {
            loadTask(workitemid, taskName);
        }

        /// <summary>
        /// 根据参数完成某任务
        /// </summary>
        /// <param name="workitemid">任务ID</param>
        /// <param name="taskName">任务名称</param>
        /// <param name="actionName">操作名称</param>
        public void complete_task(String workitemid, String taskName, String actionName)
        {
            // System.out.println("#######################################the acural proxy begin .......");
            // 这个方法出入三个参数，前两个参数在本方法中没有用到，但在代理方法中会用到，因此不能删除
            this.lastActivity.complete();
            ArrayList arr = processMgr.ProcessInstanceDao.getNorifyList(Convert.ToInt32(this.lastActivity.getModelTask().TaskID),int.Parse(WfStateContants.TASKTYPE_NOTIFY));
            context.Remove(ContextConstants.NOTIFY_LIST);
            context.Add(ContextConstants.NOTIFY_LIST,arr);
            // 这里需要从context中重新获取actionName,因为用户可能在后处理逻辑中对这个actionName修改
            run(Convert.ToString(context[ContextConstants.SUBMIT_ACTION_NAME]));
            // System.out.println("#######################################the acural proxy end .......");
            if (isCheckProcessCompleteState)
            {
                this.trycomplete();
            }
        }
      

        protected void run(String actionName)
        {
            this.previousActivity = this.lastActivity;
            this.lastActivity = null;
            int actionFlag = Convert.ToInt32(context[ContextConstants.SUBMIT_ACTION_TYPE]);
            if (actionFlag == WfStateContants.DRAFTACTION)
            {
                // 如果是草稿动作则不进行流转
                return;
            }
            else if (actionFlag == WfStateContants.SERIALACTION)
            {
                if (this.previousActivity.getWorkItem().SERIALPOINTER== 0)
                {
                    /**
                     * 0表示在角色解析到最后一个角色时，将这个标志设置为0，表示一次串行流转结束，这时重新从模型库运行流程，如果这次解析没有
                     * 结束，则继续本次串行流转，这里存在的一个问题是，如果是串行动作但是它又指向了一个顺序任务，这时会开始下一次顺序执行
                     */
                    runTaskFromModel(actionName);
                }
                else
                {
                    ModelTask mt = this.previousActivity.getModelTask();
                    queueNext(mt.TaskName);
                }
            }
            else if (actionFlag == WfStateContants.NOMINATEACTION || actionFlag == WfStateContants.CONSULTACTION
                   || actionFlag == WfStateContants.CONSULTRETURNACTION)
            {
                // 如果是在条件中判断的三种动作，则下项任务仍然是当前任务，至于任务的完成人由角色解析插件完成
                ModelTask mt = this.previousActivity.getModelTask();
                queueNext(mt.TaskName);
                this.lastActivity.getWorkItem().UserDrivenFlag = (actionFlag);
                WFUSERS user = (WFUSERS)context[ContextConstants.CURRENT_USER];
                this.lastActivity.getWorkItem().UserDrivenPersonID=Convert.ToInt32(user.Id);
            }
            else if (actionFlag == WfStateContants.TASKROLLBACKACTION)
            {
                // 回退任务
                IList<PREVIOUSWORKITEMS> preset = this.previousActivity.getWorkItem().PREVIOUSWORKITEMSs;
                if (preset == null || preset.Count > 1 || preset.Count==0)
                {
                    throw new Exception("roll back error for the set ");
                }
                PREVIOUSWORKITEMS pw = preset[0];
                queueNext(pw.PREVIOUSTASKNAME);
            }
            else if (actionFlag == WfStateContants.FREEFLOWACTION)
            {
                // 绝对跳转任务
                String tName = context[ContextConstants.ABSOLUTE_TASKNAME].ToString();
                if (tName == null || tName=="") throw new Exception("no task name!");
                queueNext(tName);
            }
            else
            {
                runTaskFromModel(actionName);
            }

        }
        
        private void runTaskFromModel(String actionName)
        {
            ModelTask mt = this.previousActivity.getModelTask();
            IList<Transition> trs = mt.Transations;

            if (trs == null || trs.Count == 0 || trs[0].TransitionTo == "end")
            {
                isCheckProcessCompleteState = true;
            }
            else
            {
                for (int i = 0; i < trs.Count; i++)
                {
                    if (trs[i].ScriptName==null || trs[i].ScriptName.Trim() == string.Empty)
                        queueNext(trs[i].TransitionTo);
                    else
                    {
                        queueNext(trs[i].Transfer.transferto());
                    }
                }
            }
          
        }
        protected void queueNext(String nextTaskName)
        {
            //PREVIOUSWORKITEMS pre = new PREVIOUSWORKITEMS();
            //pre.PREVIOUSWORKITERMID = this.previousActivity.getWorkItem().Id;
            //pre.PREVIOUSTASKNAME = this.previousActivity.getWorkItem().TASKNAME;
            //pre.PREPARTICIPANTID = this.previousActivity.getWorkItem().PARTICIPANTID;
            //pre.PREPARTICIPANTNAME = this.previousActivity.getWorkItem().PARTICIPANTNAME;
            //pre.PREPARTICIPANTNAME2 = this.previousActivity.getWorkItem().PARTICIPANTNAME2;


            switch(this.previousActivity.getModelTask().TaskType)
            {

                case  WfStateContants.TASKTYPE_SERVERTASK:
                    start_task(nextTaskName);

                    this.lastActivity.getWorkItem().PROCESSINSTANCES = (this.process_instance);
                   // this.lastActivity.getWorkItem().PREVIOUSWORKITEMSs.Add(pre);
                    this.lastActivity.persist();

                // 如果是服务器端任务则开启一个线程，在服务器端运行
                          serverTaskList.Add(new ServerTaskRunner(this,this.get_modelProcess(),
                          this.get_instance_data(),
                          this.get_lastActivity().getModelTask(),
                          this.get_lastActivity().getWorkItem().Id.ToString()));
                break;
                case WfStateContants.TASKTYPE_SYNNODE:

                int active = processMgr.ProcessInstanceDao.get_workitem_activity(this.previousActivity.getWorkItem().PROCESSINSTANCES.Id);
                if (active == 0)
                {
                    start_task(nextTaskName);

                    this.lastActivity.getWorkItem().PROCESSINSTANCES = (this.process_instance);
                   // this.lastActivity.getWorkItem().PREVIOUSWORKITEMSs.Add(pre);
                    this.lastActivity.persist();
                }
                else
                    return;
                break;

                case WfStateContants.TASKTYPE_SERIALTASK:
                   start_task(nextTaskName);

                   (this.lastActivity).getWorkItem().PROCESSINSTANCES = (this.process_instance);
                  // this.lastActivity.getWorkItem().PREVIOUSWORKITEMSs.Add(pre);
                   (this.lastActivity).persist();
                    
                break;

                case WfStateContants.TASKTYPE_EXCLUSIVE:
                    start_task(nextTaskName);

                    (this.lastActivity).getWorkItem().PROCESSINSTANCES = (this.process_instance);
                  //  this.lastActivity.getWorkItem().PREVIOUSWORKITEMSs.Add(pre);
                    (this.lastActivity).persist();
                break;
            }

        }

        public void persist()
        {
           int instanceid= processMgr.ProcessInstanceDao.save_processinstance(this.process_instance);
           if (instanceid == 0)
               throw new Exception("process instance persist failed");
           this.process_instance.Id = Convert.ToInt64(instanceid);
            get_lastActivity().getWorkItem().PROCESSINSTANCES=(this.process_instance);
            get_lastActivity().persist();
        }
        /// <summary>
        /// 获取当前工作流的当前任务
        /// </summary>
        /// <returns>返回工作任务</returns>
        public IWFActivity get_lastActivity()
        {
            return this.lastActivity;
        }

        /// <summary>
        /// 获取上一个任务
        /// </summary>
        /// <returns></returns>
        public IWFActivity get_previousActivity()
        {
            return this.previousActivity;
        }

        protected void loadTask(String workitemid, String taskName)
        {
            IList<ModelTask> mts = modelData.ModelTaskList;
            ModelTask mt=null;
            for (int i = 0; i < mts.Count; i++)
            {
                if (mts[i].TaskName == taskName)
                {
                    mt = mts[i];
                    break;
                }
            }
            this.lastActivity =
                this.processMgr.WFActivityFactory.create_wfActivity(workitemid, context, mt,(IWFProcessMgr)processMgr, (IWFProcess)this);
        }
        
        /// <summary>
        /// 从模型库中获取该工作流的模板内容
        /// </summary>
        /// <returns></returns>
        public ModelProcess get_modelProcess()
        {
            return this.modelData;
        }

        /// <summary>
        /// 从运行库中获取数据库中的工作流内容
        /// </summary>
        /// <returns></returns>
        public PROCESSINSTANCES get_instance_data()
        {
            return this.process_instance;
        }

        /// <summary>
        /// 尝试完成工作流
        /// </summary>
        public void trycomplete()
        {
            int totalcount;
            if (isCheckProcessCompleteState)
            {
                totalcount = this.get_modelProcess().ModelTaskList.Count;

                int totals = processMgr.ProcessInstanceDao.get_workitem_total_activity(this.process_instance.Id);

                if (totals == totalcount)
                {
                    int count = processMgr.ProcessInstanceDao.get_workitem_activity(this.process_instance.Id);
                    
                    if (count == 0)
                    {
                        // 说明当前任务没有活动的流程，调用流程正常结束接口
                        this.complete();
                    }
                }
               
            }
        }



  
        internal class ServerTaskRunner 
        {
    	private ModelTask task_model;
    	private ModelProcess process_model;
    	private PROCESSINSTANCES instance_data;
    	private String workitemid;
        private WFProcessImpl wfprocess;
        public ServerTaskRunner(WFProcessImpl wfp, ModelProcess pm, PROCESSINSTANCES pi, ModelTask mt, String wiid)
        {
            wfprocess = wfp;
    		task_model = mt;
    		process_model = pm;
    		instance_data = pi;
    		workitemid = wiid;

            Thread th=new Thread( new ThreadStart(DoWork));
            
    	}
        public void DoWork()
        {
            Hashtable hm = new Hashtable();   

    		// 后台驱动的动作统一为普通类型
    		hm.Add(ContextConstants.SUBMIT_ACTION_TYPE,WfStateContants.COMMACTION.ToString());
            wfprocess.context = hm;
    		try {
                IWFProcess np = wfprocess.processMgr.load_process(Convert.ToInt32(process_model.ProcessID), instance_data.Id.ToString(),
    					hm);
        		np.load_task(workitemid, task_model.TaskName);
        		// 这个方法放到了任务提交的实例同步代理中了
        		// np.get_lastActivity().load();
                np.complete_task(workitemid, task_model.TaskName, task_model.AutoExeActionName);
        		np.trycomplete();	
			} 
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
				
			}			
    	}
    }

}

}