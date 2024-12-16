using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class PositionView
    {
        public int DepartmentPositionId { get; set; }
        public string DepartmentPositionName { get; set; }
        public int DepartmentId { get; set; }
        public int PositionLevel { get; set; }
        public string LevelName { get; set; }
        public decimal ChargeRate { get; set; }
        public decimal SalaryHigh { get; set; }
        public decimal SalaryLow { get; set; }

        public void PopupData(System.Data.IDataReader r)
        {
            if (r["departmentpositionid"].ToString() != "")
            {
                DepartmentPositionId = int.Parse(r["departmentpositionid"].ToString());
            }
            DepartmentPositionName = r["departmentpositionname"].ToString();
            if (r["DepartmentId"].ToString() != "")
            {
                DepartmentId = int.Parse(r["DepartmentId"].ToString());
            }
            if (r["PositionLevel"].ToString() != "")
            {
                PositionLevel = int.Parse(r["PositionLevel"].ToString());
            }
            LevelName = r["LevelName"].ToString();
            if (r["ChargeRate"].ToString() != "")
            {
                ChargeRate = decimal.Parse(r["ChargeRate"].ToString());
            }
            if (r["SalaryHigh"].ToString() != "")
            {
                SalaryHigh = decimal.Parse(r["SalaryHigh"].ToString());
            }
            if (r["SalaryLow"].ToString() != "")
            {
                SalaryLow = decimal.Parse(r["SalaryLow"].ToString());
            }
        }

    }
}
