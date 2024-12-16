using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlow.Model
{
   public class ROLELIST
    {
       protected long _rolelistid;
       protected long _roleid;
       protected long _userid;
       protected ROLES _role;

       public ROLES ROLES
       {
           get { return _role; }
           set { _role = value; }
       }

       public long ROLELISTID
       {
           get { return _rolelistid; }
           set { _rolelistid = value; }
       }

       public long ROLEID
       {
           get { return _roleid; }
           set { _roleid = value; }
       }

       public long USERID
       {
           get { return _userid; }
           set { _userid = value; }
       }
    }
}
