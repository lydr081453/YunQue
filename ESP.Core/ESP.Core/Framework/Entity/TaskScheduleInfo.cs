using System;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///任务计划表
    ///</summary>
    [Serializable]
    public class TaskScheduleInfo
    {
        #region "variables"
        private Int32 _TaskScheduleID;
        private Int32 _WebSiteID;
        private String _TaskScheduleName;
        private String _Description;
        private Int32 _TaskScheduleType;
        private DateTime _StartTime;
        private DateTime _StopTime;
        private Int32 _Interval;
        private String _EventName;
        private Boolean _Enabled;
        private String _EntryClass;
        private Int32 _Creator;
        private DateTime _CreateTime;
        private DateTime _LastActivityTime;
        #endregion

		
        #region "Properties"
        ///<summary>
        ///标识列
        ///</summary>
        public Int32 TaskScheduleID
        {
            get{return _TaskScheduleID;}
            set{_TaskScheduleID = value;}
        }
        ///<summary>
        ///站点ID
        ///</summary>
        public Int32 WebSiteID
        {
            get{return _WebSiteID;}
            set{_WebSiteID = value;}
        }
        ///<summary>
        ///任务名字
        ///</summary>
        public String TaskScheduleName
        {
            get{return _TaskScheduleName;}
            set{_TaskScheduleName = value;}
        }
        ///<summary>
        ///描述
        ///</summary>
        public String Description
        {
            get{return _Description;}
            set{_Description = value;}
        }
        ///<summary>
        ///任务的类型
        ///</summary>
        public Int32 TaskScheduleType
        {
            get{return _TaskScheduleType;}
            set{_TaskScheduleType = value;}
        }
        ///<summary>
        ///第一次执行时间
        ///</summary>
        public DateTime StartTime
        {
            get{return _StartTime;}
            set{_StartTime = value;}
        }
        ///<summary>
        ///最后一次执行时间
        ///</summary>
        public DateTime StopTime
        {
            get{return _StopTime;}
            set{_StopTime = value;}
        }
        ///<summary>
        ///执行时间间隔
        ///</summary>
        public Int32 Interval
        {
            get{return _Interval;}
            set{_Interval = value;}
        }
        ///<summary>
        ///在哪些事件中执行（如Application_Start），多个事件用逗号分隔
        ///</summary>
        public String EventName
        {
            get{return _EventName;}
            set{_EventName = value;}
        }
        ///<summary>
        ///是否启用
        ///</summary>
        public Boolean Enabled
        {
            get{return _Enabled;}
            set{_Enabled = value;}
        }
        ///<summary>
        ///负责执行任务的类的程序集限定类名
        ///</summary>
        public String EntryClass
        {
            get{return _EntryClass;}
            set{_EntryClass = value;}
        }
        ///<summary>
        ///任务创建者
        ///</summary>
        public Int32 Creator
        {
            get{return _Creator;}
            set{_Creator = value;}
        }
        ///<summary>
        ///创建时间
        ///</summary>
        public DateTime CreateTime
        {
            get{return _CreateTime;}
            set{_CreateTime = value;}
        }
        ///<summary>
        ///最后一次执行时间
        ///</summary>
        public DateTime LastActivityTime
        {
            get{return _LastActivityTime;}
            set{_LastActivityTime = value;}
        }
        #endregion
    }
}
