using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelTemplate
{
  public  class ModelRoleSet
    {
        private int rolesetid;

        public int RolesetID
        {
            get { return rolesetid; }
            set { rolesetid = value; }
        }
        private int modelprocessid;

        public int ModelProcessID
        {
            get { return modelprocessid; }
            set { modelprocessid = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private IList<Role> roles;

        public IList<Role> Roles
        {
            get {
                if (roles == null)
                    roles = new List<Role>();
                return roles; }
            set { roles = value; }
        } 

    }
}
