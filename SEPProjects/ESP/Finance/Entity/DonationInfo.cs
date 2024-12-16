using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
   public  class DonationInfo
    {

      public int DonationId{get;set;}
public int UserId {get;set;}
public string UserCode{get;set;}
public string UserName{get;set;}
public int DepartmentId { get; set; }
public string Department{get;set;}
public string BranchCode{get;set;}
public decimal Donation{get;set;}
public DateTime CommitDate{get;set;}
public string IP { get; set; }

    }
}
