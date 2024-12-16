using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class CredenceInfo
    {
        public int Id{get;set;}
        public string Code {get;set;}
        public int UserId{get;set;}
        public string UserName{get;set;}
        public string Remark { get; set; }
	
    }
}
