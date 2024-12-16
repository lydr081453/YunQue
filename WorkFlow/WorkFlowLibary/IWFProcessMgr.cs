using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlow.Model;
using System.Collections;

namespace WorkFlowLibary
{
    public interface  IWFProcessMgr
    {
        void create_process(IWFProcess process,WFUSERS[] initiators,String startTaskName);
    /**
     * 读取流程
     * @param processID 流程ID
     * @param instanceid 流程实例ID
     * @param context 流程实例上下文
     * @return WfProcess 流程对象
     * @throws NestedCheckedException
     */
        IWFProcess load_process(int processID, String instanceid, Hashtable context);
    /**
     * 设置流程提交时是否使用事务
     * @param transaction_required
     */
   // void setTransaction_required(bool transaction_required);
    /**
     * 设置流程提交时是否做串行控制
     * @param synchronized_required
     */
   // void setSynchronized_required(bool synchronized_required);
    }
}
