using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminForm.Model
{
    public class WorkItemAssigneesInfo
    {
        public int WorkItemAssigneeId { get; set; }
        public int WorkItemId { get; set; }
        public int AssigneeId { get; set; }
        public bool NotifyOnly { get; set; }

    }
}
