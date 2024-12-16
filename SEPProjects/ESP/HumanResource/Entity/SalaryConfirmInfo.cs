using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class SalaryConfirmInfo
    {

            public int Id { get; set; }
            public int UserId { get; set; }
            public int SalaryType { get; set; }
            public DateTime ConfirmTime { get; set; }
            public string IPAddress { get; set; }
            public int SalaryYear { get; set; }
            public int SalaryMonth { get; set; }
    }
}
