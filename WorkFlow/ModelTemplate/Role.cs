using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelTemplate
{
   public class Role
    {
        private int roleid;

        public int RoleID
        {
            get { return roleid; }
            set { roleid = value; }
        }
        private int rolesetid;

        public int RoleSetID
        {
            get { return rolesetid; }
            set { rolesetid = value; }
        }
        private int modeltaskid;

        public int ModelTaskID
        {
            get { return modeltaskid; }
            set { modeltaskid = value; }
        }

       private string rolename;

        public string RoleName
        {
            get { return rolename; }
            set { rolename = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

    }
}
