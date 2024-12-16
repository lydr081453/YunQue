using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
     [Serializable]
    public class TransferDetailsInfo
    {

         public TransferDetailsInfo()
        { }

        #region Model
         public int Id { get;set;}
         public int TransferId { get; set; }
         public int UserId { get; set; }
         public string UserName { get; set; }
         public int FormId { get; set; }
         public string FormCode { get; set; }
         public string FormType { get; set; }
         public int ProjectId { get; set; }
         public string ProjectCode { get; set; }
         public string Description { get; set; }
         public decimal TotalPrice { get; set; }
         public int FormStatus { get; set; }
         public int ReceiverId { get; set; }
         public string ReceiverName { get; set; }
         public int ReceiverDepartmentId { get; set; }
         public string ReceiverDepartmentName { get; set; }
         public int Status { get; set; }
         public string Remark { get; set; }
         public DateTime CreateTime { get; set; }
         public DateTime? ReceiverTime { get; set; }
         public string WebSite { get; set; }
         public string Url { get; set; }
         public int UpdateStatus { get; set; }
         public int TransGroup { get; set; }

        #endregion Model

    }
}
