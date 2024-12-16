using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowLibary
{
    public static class WfStateContants {
    // 任务状态定义(task state)
    /**
     * 本工作项的状态，小于50是需要做的
       描述活动状态，状态分为：
       runing < 50
           20 active-->已分配，用户打开了表单（在此执行preWork，若是服务器类型任务直接执行）
           40 inactive-->已分配，但用户未打开（在initWork返回True之后）
       50 < not runing < 100
           60 suspended-->（在initWork返回False之后或子流程任务等待子流程结束）
           70 draft--->用户使改项工作未不活动状态，即放入了自己的草稿箱中，但是虽然这项工作在
                       草稿箱中，与其相关联的所有调度程序仍在运行
       closed > 100
           110 completed-->正常完成
           120 terminated-->被外界强行停止或取消（外界强行取消）
           130 aborted-->出现异常情况
     */
    public const int TASKSTATE_RUNNING = 80;
    public const int TASKSTATE_CLOESED = 100;
    public const int TASKSTATE_ACTIVE = 20;
    public const int TASKSTATE_INACTIVE = 40;
    public const int TASKSTATE_NOTRUNNING = 50;
    public const int TASKSTATE_SUSPENDED = 60;
    public const int TASKSTATE_DRAFT = 70;
    public const int TASKSTATE_SUBMITED = 80;
    public const int TASKSTATE_COMPLETED = 110;
    public const int TASKSTATE_TERMINATED = 120;
    public const int TASKSTATE_ABORTED = 130;



    // 活动类型定义
    public  const string TASKTYPE_COMMTASK = "1";
    public const string TASKTYPE_SERVERTASK = "2";
    public const string TASKTYPE_STARTTASK = "3";
    public const string TASKTYPE_CONNTASK = "4";
    public const string TASKTYPE_LOOPTASK = "5";
    public const string TASKTYPE_SYNNODE = "6";
    public const string TASKTYPE_SERIALTASK = "7";
    public const string TASKTYPE_EXCLUSIVE = "8";
    public const string TASKTYPE_NOTIFY = "9";

    public const short TASK_OPENMODE_OPENACCEPT = 1;
    public const short TASK_OPENMODE_SUBMITACCEPT = 2;
    // 流程实例状态定义
    /**
     *描述活动状态，状态分为：
     runing < 50
          20 active-->正常运行
     not runing < 100
          60 suspended-->流程被挂起
     closed > 100
          110 completed-->正常完成
          120 teminated-->被外界强行停止或取消（外界强行取消）
          130 aborted-->出现异常情况而停止
     */
    public const short PROCESS_ACTIVE = 20;
    public const short PROCESS_NOTRUNNING = 50;
    public const short PROCESS_SUSPENDED = 60;
    public const short PROCESS_COMPLETED = 110;
    public const short PROCESS_TERMINATED = 120;
    public const short PROCESS_ABORTED    = 130;    
    
    // 动作类型
    // 对于动作而言，某一种动作抽象了一种使用场景，一个场景仅仅需要确定两件事 1. 人员如何确定 2. 任务如何确定 
    public const int  COMMACTION     = 1;  // 普通
    public const int  ASSIGNACTION   = 2;  // 指派 角色用指派人员
    public const int  CONSULTACTION  = 3;  // 咨询 角色用指派人员
    public const int  NOMINATEACTION = 4;  //  获取任务仍然使用下图形解析，角色用指派人员
    public const int  CONSULTRETURNACTION = 7; // 咨询返回动作，
                                                // 当遇到这个动作时任务分派仍然取当前任务，角色分派取指派人员
    // 任务回退在拓补实现上有问题，因此目前回退仍然使用删除工作项的方式完成
    public const int  TASKROLLBACKACTION = 8;  // 任务回退动作，当用户点击了这个动作时，认为当前流程自动转回上一步的完成人，以及上一步的完成动作
    public const int  FREEFLOWACTION = 9;      // 自由流动作，这个动作自动跳转到任何一个步骤并解析那个步骤的完成人
    public const int  DRAFTACTION=10; // 草稿动作，这类动作不驱动任务流转，仅仅执行依次动作的后处理
    public const int  SERIALACTION=11; // 配置给这个动作的角色所代表的人员串行完成这个动作之后才继续执行后面的线段表示的逻辑
}
}
