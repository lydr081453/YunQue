using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_OperationArea
    {
        public SC_OperationArea()
        { }
        #region Model
        private int _id;
        private int? _supplierid;
        private string _operationarea;
        private string _percentum;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? supplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string operationArea
        {
            set { _operationarea = value; }
            get { return _operationarea; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string percentum
        {
            set { _percentum = value; }
            get { return _percentum; }
        }
        #endregion Model
    }
}
