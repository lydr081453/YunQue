using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ModelTemplate;

namespace WorkFlowLibary
{
    public interface IWFActivityFactory
    {
        IWFActivity create_wfActivity(ModelTask model_task, Hashtable context, IWFProcessMgr processMgr, IWFProcess process);
    // 从运行库构造任务
        IWFActivity create_wfActivity(String workitemid, Hashtable context, ModelTask mt, IWFProcessMgr processMgr, IWFProcess process);
    }
}
