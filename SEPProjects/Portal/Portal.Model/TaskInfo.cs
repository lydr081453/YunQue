using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Model
{
    public class TaskInfo
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private object _instance;
        public object Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }

        private DateTime _lastErrorTime;
        public DateTime LastErrorTime
        {
            get { return _lastErrorTime; }
            set { _lastErrorTime = value; }
        }

        private DateTime _lastExcuteTime;
        public DateTime LastExcuteTime
        {
            get { return _lastExcuteTime; }
            set { _lastExcuteTime = value; }
        }

        private object _state;
        public object State
        {
            get { return _state; }
            set { _state = value; }
        }

        private bool _isSuccess;
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        }

        private int _errorCount;
        public int ErrorCount
        {
            get { return _errorCount; }
            set { _errorCount = value; }
        }

        private string _lastError;
        public string LastError
        {
            get { return _lastError; }
            set { _lastError = value; }
        }

        public TaskInfo()
        {
            _name = "";
            _instance = null;
            _state = true;
            _lastExcuteTime = DateTime.Now;
            _lastErrorTime = DateTime.Now;
            _isSuccess = true;
            _errorCount = 0;
            _lastError = "";
        }

        public void Sleep(int sleepTime)
        {
            _state = true;
            _isSuccess = true;
            _errorCount = 0;
            _lastExcuteTime = DateTime.Now.AddSeconds(sleepTime);
        }
    }
}
