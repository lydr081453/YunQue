using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierSubsidiaryUsers
    {
        public int ID { get; set; }
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string Name_en { get; set; }
        public string LogName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDel { get; set; }
        public bool IsEffective { get; set; }
        public int Gender { get; set; }
        public string Departments { get; set; }
        public string Duties { get; set; }
        public int Ages { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int Types { get; set; }
    }
}
