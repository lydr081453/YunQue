using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminForm.Model
{
    public class WorkflowInstancesInfo
    {
        public string InstanceId { get; set; }
        public string Definition { get; set; }
        public string DynamicAssembly { get; set; }
        public string DynamicNamespace { get; set; }
        public int NextThreadId { get; set; }
        public string Threads { get; set; }
        public string DataFeilds { get; set; }
        public string EventQueues { get; set; }
        public int Status { get; set; }
        public bool IsLocked { get; set; }
        public int InitiatorId { get; set; }
        public DateTime InitiatedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public string Users { get; set; }

    }
}
