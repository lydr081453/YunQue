using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_VendeeDepartmentTypeRelation
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int CompanyId { get; set; }

        private string companyDepartmentID;
        public string CompanyDepartmentID
        {
            get { return companyDepartmentID; }
            set { companyDepartmentID = value; }
        }

        public int TypeId { get; set; }

        public DateTime CreatTime { get; set; }

        public string CreatIP { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string LastUpdateIP { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
    }
}