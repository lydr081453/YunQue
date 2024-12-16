using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class TransferInfo
    {
        public TransferInfo()
        { }

        public int Id { get; set; }
        public int TransId { get; set; }
        public string TransCode { get; set; }
        public string TransName { get; set; }
        public int OldGroupId { get; set; }
        public string OldGroupName { get; set; }
        public int OldPositionId { get; set; }
        public string OldPositionName { get; set; }
        public int OldLevelId { get; set; }
        public string OldLevelName { get; set; }
        public int NewGroupId { get; set; }
        public string NewGroupName { get; set; }
        public int NewPositionId { get; set; }
        public string NewPositionName { get; set; }
        public int NewLevelId { get; set; }
        public string NewLevelName { get; set; }
        public decimal SalaryBase { get; set; }
        public decimal SalaryPromotion { get; set; }
        public DateTime TransInDate { get; set; }
        public DateTime TransOutDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public int CreaterId { get; set; }
        public string Creater { get; set; }
        public string Remark { get; set; }
        public int HeadCountId { get; set; }
    }
}
