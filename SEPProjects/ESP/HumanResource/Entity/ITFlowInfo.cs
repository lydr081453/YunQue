using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class ITFlowInfo
    {
        public int Id { get; set; }
        public string FlowName { get; set; }
        public int DirectorId { get; set; }
        public string DirectorName { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public int Status { get; set; }

    }
}
