using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowLibary
{
    public abstract class MySubject
    {
        public delegate void StartHandler(Hashtable context);
        public delegate void ResumeHandler(Hashtable context);
        public delegate void SuspendHandler(Hashtable context);
        public delegate void AbortHandler(Hashtable context);
        public delegate void CompleteHandler(Hashtable context);
        public delegate void ActiveHandler(Hashtable context);
        public delegate void TerminateHandler(Hashtable context);
        public delegate void PersistHandler(Hashtable context);
        public delegate void CreateHandler(Hashtable context);
        public delegate void CancelHandler(Hashtable context);
        public delegate void LoadHandler(Hashtable context);
        public delegate void TrycompleteHandler(Hashtable context);
        public delegate void TaskCompleteHandler(Hashtable context);

        public event StartHandler StartEvent;
        public event ResumeHandler ResumeEvent;
        public event SuspendHandler SuspendEvent;
        public event AbortHandler AbortEvent;
        public event CompleteHandler CompleteEvent;
        public event ActiveHandler ActiveEvent;
        public event TerminateHandler TerminateEvent;
        public event PersistHandler PersistEvent;
        public event CreateHandler CreateEvent;
        public event CancelHandler CancelEvent;
        public event LoadHandler LoadEvent;
        public event TrycompleteHandler TryCompleteEvent;
        public event TaskCompleteHandler TaskCompleteEvent;

        public void NotifyResume(Hashtable context)
        {
            if (ResumeEvent != null) ResumeEvent(context);
        }

        public void NotifyPersist(Hashtable context)
        {
            if (PersistEvent != null) PersistEvent(context);
        }

        public void NotifyStart(Hashtable context)
        {
            if (StartEvent != null) StartEvent(context);
        }

        public void NotifySuspend(Hashtable context)
        {
            if (SuspendEvent != null) SuspendEvent(context);
        }

        public void NotifyAbort(Hashtable context)
        {
            if (AbortEvent != null) AbortEvent(context);
        }

        public void NotifyComplete(Hashtable context)
        {
            if (CompleteEvent != null) CompleteEvent(context);
        }

        public void NotifyActive(Hashtable context)
        {
            if (ActiveEvent != null) ActiveEvent(context);
        }

        public void NotifyTerminate(Hashtable context)
        {
            if (TerminateEvent != null) TerminateEvent(context);
        }

        public void NotifyLoad(Hashtable context)
        {
            if (LoadEvent != null) LoadEvent(context);
        }

        public void NotifyCreate(Hashtable context)
        {
            if (CreateEvent != null)
            {
                CreateEvent(context);
            }
        }

        public void NotifyTryComplete(Hashtable context)
        {
            if (TryCompleteEvent != null)
            {
                TryCompleteEvent(context);
            }
        }

        public void NotifyTaskComplete(Hashtable context)
        {
            if (TaskCompleteEvent != null)
            {
                TaskCompleteEvent(context);
            }
        }


        public void NotifyCancel(Hashtable context)
        {
            if (CancelEvent != null) CancelEvent(context);
        }
    }
}
