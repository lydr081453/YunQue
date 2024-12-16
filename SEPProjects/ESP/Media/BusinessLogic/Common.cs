using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using ESP.Compatible;

namespace MediaLib.Model
{
    public class Media_Headers
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string altname;

        public string AltName
        {
            get { return altname; }
            set { altname = value; }
        }

        public Media_Headers(string name, string altname)
        {
            this.name = name;
            this.altname = altname;
        }
    }

    public class Media_Employee
    {
        public Media_Employee(string sysid)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(Convert.ToInt32(sysid));
            this._sysid = emp.SysID;
            this._userid = emp.ID;
            this._username = emp.Name;

            emp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        string _sysid;

        public string Sysid
        {
            get { return _sysid; }
            set { _sysid = value; }
        }
        string _userid;

        public string Userid
        {
            get { return _userid; }
            set { _userid = value; }
        }
        string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
    }

    public class Media_Department
    {
        public Media_Department()
        {

        }
        [AjaxPro.AjaxMethod]
        public static  List<Media_Department> getDepartmentList(string leaderid)
        {
            List<Media_Department> lists = new List<Media_Department>();

            IList<Department> dt = ESP.Compatible.Employee.GetDepartments(Convert.ToInt32(leaderid));

            for (int i = 0; i < dt.Count; i++)
            {
                Media_Department dept = new Media_Department();
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

namespace ESP.Media.BusinessLogic
{
    public class CommonManager
    {
        public CommonManager()
        {
        }

        public static int GetLastVersion(string tablename,int id,SqlTransaction ts)
        {
            string sql = string.Format("select Max(Version) from Media_{0}sHist where {0}ID={1}",tablename,id);
            DataTable dt;
            if (ts == null)
                dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql);
            else
                dt = ESP.Media.Access.Utilities.clsSelect.QueryBySql(sql,ts);
            if (dt.Rows[0][0] != DBNull.Value)
                return (int)(dt.Rows[0][0]) + 1;
            else
                return 1;
        }

        public static int GetLastVersion(string tablename, int id)
        {
            return GetLastVersion(tablename, id, null);
        }

        public static string GetCntentsXml(string tablename, object obj, params string[] fields)
        {
            string xml = "<" + tablename + "Info><BaseInfo>";
            Type type = obj.GetType();
            foreach (string field in fields)
            {
                System.Reflection.PropertyInfo info = type.GetProperty(field);
                try
                {
                    string v = string.Format("<{0}>{1}</{0}>", field, info.GetValue(obj, null).ToString());
                    xml += v;
                }
                catch
                {
                    continue;
                }
            }
            xml += "</BaseInfo></" + tablename + "Info>";
            return xml;
        }

        public static string SaveFile(string path ,string name, byte[] data, bool checkdic)
        {
            string fname = string.Empty;//name;
            fname = name.Substring(name.LastIndexOf('\\') + 1);
            string serverpath = name.Substring(0, name.LastIndexOf('\\'));
            if (checkdic)
            {
                if (!Directory.Exists(serverpath))
                    Directory.CreateDirectory(serverpath);
            }
            if (File.Exists(name))
            {
                fname = Guid.NewGuid().ToString() + fname;
            }
            FileStream fs = new FileStream(serverpath + "\\" + fname, FileMode.CreateNew);
            fs.Write(data, 0, data.Length);
            fs.Close();
           
            return path+fname;
        }

        public static string[] GetDepartmentStrings(DataTable dt, int count)
        {
            List<string> temp = new List<string>();
            int tlen = 0;
            foreach (DataRow row in dt.Rows)
            {
                string s = row["userdepartmentname"].ToString() + "/";
                if (!temp.Contains(s))
                {
                    temp.Add(s);
                    tlen += s.Length;
                }
            }
            int len = (int)(tlen / count);
            string[] res = new string[count];
            int i = 0;
            int l = 0;
            foreach (string s in temp)
            {
                res[i] += s;
                l = res[i].Length;
                if (l > len)
                {
                    res[i] = res[i].Trim('/');
                    i++;
                }
            }
            return res;
        }
    }
}
