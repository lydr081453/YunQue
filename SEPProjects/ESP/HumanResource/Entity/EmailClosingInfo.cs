using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    /// <summary>
    /// IT关闭邮箱提醒实体类(sep_emailclosing)
    /// </summary>
    public class EmailClosingInfo
    {

        public int Id { get; set; }
        public int UserId { get; set; }

        public string NameCN { get; set; }
        public string UserCode { get; set; }
        public string DeptName { get; set; }
        public string Postion { get; set; }
        public string Email { get; set; }
        public int OperatorId { get; set; }
        public DateTime KeepDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int Status { get; set; }


    }
}
