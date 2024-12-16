using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlow.Model;
using ModelTemplate;

namespace WorkFlowLibary
{
    public interface IWFActivity
    {
     void start();
     /**
      * 工作流活动激活
      * @throws NestedCheckedException
      */
     void active();
     /**
      * 工作流活动取消
      * @throws NestedCheckedException
      */
     void cancel();
     /**
      * 工作流活动加载
      * @throws NestedCheckedException
      */
     void load();
     /**
      * 任务角色
      * @throws NestedCheckedException
      */
     void taskrole();
     /**
      * 取得模板任务
      * @return ModelTask 模板任务对象
      */
     ModelTask getModelTask();
     /**
      * 取得工作项
      * @return WorkItem 工作项对象
      */
     WORKITEMS getWorkItem();

     void resume ();
    /**
     * 悬停
     * @throws NestedCheckedException
     */
    void suspend ();

    /**
     * 实行完成
     * @throws NestedCheckedException
     */
    void complete ();

    void persist();
    }
}
