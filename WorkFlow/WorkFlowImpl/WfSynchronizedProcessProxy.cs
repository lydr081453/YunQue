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
   public  class WfSynchronizedProcessProxy:MySubject,IWFProcess
    {
        private IWFProcess m_process;
        private WFProcessMgrImpl processMgr;
        private Hashtable context;

    public WfSynchronizedProcessProxy(WFProcessMgrImpl processMgr,IWFProcess process,Hashtable context) 
    {
        this.processMgr = processMgr;
        this.context = context;
        this.m_process = process;
    }

    public void start(WFUSERS[] initiators,String startTaskName)
    {
        m_process.start(initiators,startTaskName);
        this.NotifyStart(context);
    }
    public void terminate ()
    {
        m_process.terminate();
        this.NotifyTerminate(context);
    }
    public void abort ()
    {
        m_process.abort();
        this.NotifyAbort(context);
    }

    public void load_task(String workitemid,String taskName)
    {
        m_process.load_task(workitemid,taskName);
        this.NotifyLoad(context);
    }

    public void complete_task(String workitemid,String taskName,String actionName)
    {
         try
         {
            m_process.get_lastActivity().load();
            int state = processMgr.ProcessInstanceDao.get_workitem_state(workitemid);
            if( state > WfStateContants.TASKSTATE_CLOESED ) 
            {
                // 当前任务已经被完成
            	throw new Exception("the workitem is :" + workitemid+" had been finished.");
            }            
            m_process.complete_task(workitemid,taskName,actionName);
            this.NotifyTaskComplete(context);
        }
         catch(Exception ex) 
         {
             Console.WriteLine(ex.Message);
        }
         finally
         {

         }
        
    }

    public IWFActivity get_lastActivity()
    {
        return m_process.get_lastActivity();

    }


    public ModelProcess get_modelProcess()
    {
        return m_process.get_modelProcess();

    }
    public PROCESSINSTANCES get_instance_data()
    {
        return m_process.get_instance_data();
    }
    public void resume ()
    {
        m_process.resume();
        this.NotifyResume(context);
    }
    public void suspend ()
    {
        m_process.suspend();
        this.NotifySuspend(context);
    }
    public void complete ()
    {
        m_process.complete();
        this.NotifyComplete(context);
    }
    public void persist()
    {
        m_process.persist();
        this.NotifyPersist(context);
    }
    public void trycomplete()
    {
    	m_process.trycomplete();
        this.NotifyTryComplete(context);
        
    }

    public IWFActivity get_previousActivity() 
    {
    	return m_process.get_previousActivity();
    }
    }
}
