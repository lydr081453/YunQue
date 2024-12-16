using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_ChangDiSupplierRemark
    {
        public SC_ChangDiSupplierRemark()
        { }
        #region Model
        private int _ID;
        private int _SupplierID;
        private DateTime _CreatedDate;
        private string _Remark;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SupplierID
        {
            set { _SupplierID = value; }
            get { return _SupplierID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate
        {
            set { _CreatedDate = value; }
            get { return _CreatedDate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }
        #endregion Model

    }
}
