using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class SalaryPermissionsInfo
    {
        public SalaryPermissionsInfo()
        { }
        #region Model
        private int _id;
        private decimal _salarypermissions = 0;
        private string _description;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 查看额度
        /// </summary>
        public decimal SalaryPermissions
        {
            set { _salarypermissions = value; }
            get { return _salarypermissions; }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        #endregion Model

    }
}
