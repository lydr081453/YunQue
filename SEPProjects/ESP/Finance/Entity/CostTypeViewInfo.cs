using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class CostTypeViewInfo
    {
        private int _typeid;
        private string _typename;
        private int _parentid;
        private string _parentTypeName;
        private int _typelevel;

     


        /// <summary>
        /// 
        /// </summary>
        public int TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }
        public int ParentID
        {
            get { return _parentid; }
            set { _parentid = value; }
        }
        public string ParentTypeName
        {
            get { return _parentTypeName; }
            set { _parentTypeName = value; }
        }
         public int TypeLevel
        {
            get { return _typelevel; }
            set { _typelevel = value; }
        }

    }
}
