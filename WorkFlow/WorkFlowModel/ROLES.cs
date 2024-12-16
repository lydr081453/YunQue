using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlow.Model
{
    public class ROLES
    {
        protected long _roleid;
        protected string _rolename;
        protected IList<ROLELIST> _rolelist;

        public IList<ROLELIST> ROLELIST
        {
            get { return _rolelist; }
            set { _rolelist = value; }
        }

        public long ROLEID
        {
            get { return _roleid; }
            set { _roleid = value; }
        }

        public string ROLENAME
        {
            get { return _rolename; }
            set { _rolename = value; }
        }
    }
}
