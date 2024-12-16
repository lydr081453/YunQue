using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Vendee
    {
        public SC_Vendee()
        { }
        #region Model
        private int id;
        private int vendeecompanyID;
        private string companysystemUserID;
        private string realname;
        private string name;
        private string email;
        private bool isdelete;
        private bool isapproved;
        private bool islockedout;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        public int VendeeCompanyID
        {
            set { vendeecompanyID = value; }
            get { return vendeecompanyID; }
        }
        public string CompanySystemUserID
        {
            set { companysystemUserID = value; }
            get { return companysystemUserID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RealName
        {
            set { realname = value; }
            get { return realname; }
        }
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        public string Email
        {
            set { email = value; }
            get { return email; }
        }
        public bool IsDelete
        {
            set { isdelete = value; }
            get { return isdelete; }
        }
        public bool IsApproved
        {
            set { isapproved = value; }
            get { return isapproved; }
        }
        public bool IsLockedOut
        {
            set { islockedout = value; }
            get { return islockedout; }
        }
        #endregion Model

    }
}
