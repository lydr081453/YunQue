using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlow.Model;
using ModelTemplate;

namespace WorkFlowLibary
{
    public interface  IWFProcess
    {

    void start(WFUSERS[] initiators,String startTaskName);
    /**
     * 终止流程
     * @throws NestedCheckedException
     */
    void terminate ();
    /**
     * 异常中断流程
     * @throws NestedCheckedException
     */
    void abort ();
    /**
     * 试图结束工作流 具体状态由自身隐形判断
     * @throws NestedCheckedException
     */
    void trycomplete();
    
    /**
     * 读取任务
     * @param workitemid 工作项ID
     * @param taskName 任务名称
     * @throws NestedCheckedException
     */
    void load_task(String workitemid,String taskName);
    
    /**
     * 完成任务
     * @param workitemid 工作项ID
     * @param taskName 任务名称
     * @param actionName ACTION名称
     * @throws NestedCheckedException
     */
    void complete_task(String workitemid,String taskName,String actionName);
    /**
     * 取得最后一个处理的工作流活动
     * @return WfActivity 流程活动接口
     */
    IWFActivity get_lastActivity();
    /**
     * 取得上一个处理的工作流活动
     * @return WfActivity 流程活动接口
     */
    IWFActivity get_previousActivity();
    /**
     * 取得模板流程
     * @return ProcessInstance 流程实例对象
     */
    ModelProcess get_modelProcess();
    /**
     * 取得实例数据
     * @return ProcessInstance 流程实例对象
     */
    PROCESSINSTANCES get_instance_data();

    void resume();
    /**
     * 悬停
     * @throws NestedCheckedException
     */
    void suspend();

    /**
     * 实行完成
     * @throws NestedCheckedException
     */
    void complete();

    void persist();
    }
}
