using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelTemplate
{
    public class ModelTask
    {
        private string _taskname;
        private string _tasktype;
        private IList<Transition> _transation;
        private string _taskid;
        private int _openttype;
        private int _taskenddatecount;
        private string _formname;
        private int _Deadlinequant;
        private string _autoexeactionname;
        private string _displayname;
        private string _rolename;
        private string _formdata;
        private int modelprocessid;
        private Role role;

        public Role Role
        {
            get { return role; }
            set { role = value; }
        }
        public int ModelProcessID
        {
            get { return modelprocessid; }
            set { modelprocessid = value; }
        }
        public string FormData
        {
            get { return _formdata; }
            set { _formdata = value; }
        }

        public string RoleName
        {
            get { return _rolename; }
            set { _rolename = value; }
        }

        public string DisPlayName
        {
            get { return _displayname; }
            set { _displayname = value; }
        }

        public string AutoExeActionName
        {
            get { return _autoexeactionname; }
            set { _autoexeactionname = value; }
        }

        public int DeadLineQuantity
        {
            get { return _Deadlinequant; }
            set { _Deadlinequant = value; }
        }

        public string FormName
        {
            get { return _formname; }
            set {  _formname=value ; }
        }

        public int TaskEndDateCount
        {
            get { return _taskenddatecount;  }
            set { _taskenddatecount = value; }
        }

        public int OpenType
        {
            get { return _openttype; }
            set { _openttype = value; }
        }

        public string TaskID
        {
            get { return _taskid; }
            set { _taskid = value; }
        }

        public string TaskName
        {
            get { return _taskname; }
            set { _taskname = value; }
        }

        public string TaskType
        {
            get { return _tasktype; }
            set { _tasktype = value; }
        }

        public IList<Transition> Transations
        {
            get {
                if (_transation == null)
                    _transation = new List<Transition>();
                return _transation; }
            set { _transation = value; }
        }

    }
}
