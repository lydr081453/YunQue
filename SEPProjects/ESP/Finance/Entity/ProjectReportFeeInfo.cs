using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class ProjectReportFeeInfo
    {
      public int Id { get; set; }
      public string ProjectCode { get; set; }
      public int Year { get; set; }
      public int Month { get; set; }
      public decimal Fee { get; set; }
      public int DeptId { get; set; }
      public string ProjectType { get; set; }
    }
}
