using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.HumanResource.Entity
{
   public  class TELInfo
    {
       public int Id { get; set; }
       public string Tel { get; set; }
       public int Status { get; set; }



       public void PopupData(IDataReader r)
       {
           Id = int.Parse(r["id"].ToString());
           if (r["tel"].ToString() != "")
           {
               Tel = r["tel"].ToString();
           }
         
           if (r["status"].ToString() != "")
           {
               Status = int.Parse(r["status"].ToString());
           }
       }
    }
}
