using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminForm.Model
{
    public class WorkItemsInfo
    {
        public int WorkItemId { get; set; }
        public string WorkflowInstanceId { get; set; }
        public string WorkItemName { get; set; }
        public string WorkflowName { get; set; }
        public string ParticipantName { get; set; }
        public string Token { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int EntityId { get; set; }
        public int WebSiteId { get; set; }
        public string WebPage { get; set; }
        public int OperatorId { get; set; }
        public int ClosedBy { get; set; }
        public DateTime ClosedTime { get; set; }
        public DateTime Timeout { get; set; }


    }
}
