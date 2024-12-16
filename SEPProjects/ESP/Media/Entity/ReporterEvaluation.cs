using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Media.Entity
{
    public class ReporterEvaluation
    {
        public int ID { get; set; }
        public int ReporterId { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Evaluation { get; set; }
        public string Reason { get; set; }
    }
}
