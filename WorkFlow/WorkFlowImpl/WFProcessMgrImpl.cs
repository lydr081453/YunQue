using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowLibary;
using ModelTemplate.BLL;
using ModelTemplate;
using WorkFlow.Model;
using IWorkFlowDAO;
using WorkFlowDAO;

namespace WorkFlowImpl
{
    public class WFProcessMgrImpl : IWFProcessMgr
    {

        ModelManager modelManager = new ModelManager();
        private bool transaction_required = true;
        private bool synchronized_required = true;
        private IProcessInstanceDao processInstanceDao;
        private IWFActivityFactory activityFactory;

        private ModelProcess mp;

        public ModelManager ModelManager
        {
            get { return modelManager; }
            set { modelManager = value; }
        }

        public bool tran_required
        {
            get { return transaction_required; }
            set { transaction_required = value; }
        }

        public bool syn_required
        {
            get { return synchronized_required; }
            set { synchronized_required = value; }
        }

        public IWFActivityFactory WFActivityFactory
        {
            get
            {
                if (activityFactory == null)
                    activityFactory = new WFActivityFactoryImpl();
                return activityFactory;
            }
            set { activityFactory = value; }
        }

        public void create_process(IWFProcess process, WFUSERS[] initiators, String startTaskName)
        {
            process.start(initiators, startTaskName);

        }

        public IWFProcess preCreate_process(int processID, Hashtable context)
        {
            mp = modelManager.loadProcessModelByID(processID);
            if (mp == null) throw new Exception("the processid is :" + processID);
            IWFProcess process = loadWfProcess(new WFProcessImpl(mp, context, this), context, tran_required, syn_required);
            return process;
        }

        public IWFProcess load_process(int processID, String instanceid, Hashtable context)
        {
            return load_process(processID, instanceid, context, transaction_required, synchronized_required);
        }
        public IWFProcess load_process(int processID, String instanceid, Hashtable context, bool tran_required, bool syn_required)
        {
            ModelProcess mp = modelManager.loadProcessModelByID(processID);
            IWFProcess process = loadWfProcess(new WFProcessImpl(instanceid, context, mp, this), context, tran_required, syn_required);
            return process;
        }
  
        private IWFProcess loadWfProcess(WFProcessImpl process, Hashtable context, bool tran_required, bool syn_required)
        {
            IWFProcess retProcess = process;
            if (syn_required)
            {
                retProcess = new WfSynchronizedProcessProxy(this, retProcess, context);
            }
            if (tran_required)
            {
                retProcess = new WfTransactionProcessProxy(this, retProcess, context);
            }
            return retProcess;
        }

        public IProcessInstanceDao ProcessInstanceDao
        {
            get
            {
                if (processInstanceDao == null)
                    processInstanceDao = new ProcessInstanceDao();
                return processInstanceDao;
            }
            set { processInstanceDao = value; }
        }
    }
}
