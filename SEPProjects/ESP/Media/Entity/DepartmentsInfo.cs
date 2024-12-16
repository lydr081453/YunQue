using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Media.Entity
{

    public class DepartmentsInfo
    {

        public DepartmentsInfo()
        {

        }
        [AjaxPro.AjaxMethod]
        public static List<DepartmentsInfo> getDepartmentList(string leaderid)
        {
            List<DepartmentsInfo> lists = new List<DepartmentsInfo>();

            IList<ESP.Compatible.Department> dt = ESP.Compatible.Employee.GetDepartments(Convert.ToInt32(leaderid));

            for (int i = 0; i < dt.Count; i++)
            {
                DepartmentsInfo dept = new DepartmentsInfo();
                dept.Deptid = dt[i].UniqID.ToString();
                dept.Deptname = dt[i].NodeName;
                dept.Description = dt[i].Description;
                lists.Add(dept);
            }
            return lists;
        }
        string _deptid;

        public string Deptid
        {
            get { return _deptid; }
            set { _deptid = value; }
        }
        string _deptname;

        public string Deptname
        {
            get { return _deptname; }
            set { _deptname = value; }
        }
        string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }




    }
}
