using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowLibary
{
    public static class ContextConstants
    {
        // public const  String PROCESS_MODEL_DATA = "process_model_data";
        // public const  String PROCESS_TASK_DATA = "process_task_data";
        // public const  String PROCESS_INSTANCE_DATA = "process_instance_data";
        // public const  String PROCESS_WORKITEM_DATA = "process_workitem_data";
        /**
         * 重要修改：原来在context中设置的是基础数据模型，但是这些基础模型分配任务时包含一个一对多的关系
         * 因此这些关系只能从WfProcessImpl对象中获得，因此修改在context中保存WfProcessImpl对象
         *
         */
        public const String PROCESS_IMPL = "process_impl";

        public const String SUBMIT_ACTION_NAME = "submit_action_name";
        public const String SUBMIT_ACTION_DISPLAYNAME = "submit_action_displayname";
        //   保存用户提交的动作类型
        public const String SUBMIT_ACTION_TYPE = "action_type";

        public const String SUBMIT_DRIVEN_USER = "driven_user_name";

        // 环境变量在context中的值，这个变量是保存在运行容器中的键
        public const String CONTEXT_NAME = "activecontext";

        // 用户在程序中指定需要隐藏的ACTION的列表，这个列表在PREWORK中设定，在显示ACTION时做为一个条件判断
        // 在这个MAP中以action的标识索引字符“1”或“0”,表示程序控制这个动作是否显示，如果程序没有设置这个值，则使用设计器的设定值
        // 否则以程序设计值为准
        public const String HIDDEN_ACTION_HASHMAP = "actionvisibilitymap";

        // 当前用户保存的变量，这个变量存在工作流运行环境中，它实际就是当前登录人
        public const String CURRENT_USER = "current_user";

        // 在流程启动启动任务初始化时，任务的默认待办人保存在这个变量中，这时，任务的完成人是不从角色集中解析的
        // 因此仅仅在任务启动时在设置这个变量，目的是在角色解析时使用这个变量保存的值完成人员角色的解析
        public const String CURRENT_USER_ASSIGNMENT = "current_user_assignment";

        // 环境变量保存的前端http request
        public const String HTTP_REQUEST = "http_request";
        public const String HTTP_FORWARD_PATH = "http_forward_path";
        public const String HTTP_FORWARD_PATH_READONLY = "http_forward_path_readonly";
        // 这个参数是保存在http request中的USER，如果需要保存在CONTEXT中的user，应当从current_user中获得
        public const String HTTP_WF_USER = "httpwfuser";



        // 保存角色解析的历史数据对照表
        public const String ROLE_MAP = "role_map";
        public const String TASK_MAP = "task_map";

        // 在使用任务绝对跳转时，这个关键字索引的http request中保存了目标任务名称，如果任务名称没有找到则会抛出异常
        public const String ABSOLUTE_TASKNAME = "absoluttaskname";
        public const String PARTICIPANTTYPE = "openworkitemmode";

        public const String RESOURCE_FILE = "wfresources";


        public const String RUNNING_ROUTE = "running_route";


        public const String USER_SESSION_MONITOR = "USER_SESSION_MONITOR_KICKOUT";

        public const String USER_SESSION_MONITOR_KICKOUT_BYUSER = "1";

        public const String NOTIFY_LIST = "NotifyList";
    }
}
