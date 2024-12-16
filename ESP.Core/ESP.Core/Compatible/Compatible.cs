using System;
using System.Collections.Generic;
using System.Text;
using ESP.Framework.Entity;
using System.Data;
using System.Xml.Xsl;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using EspDepartmentManager = ESP.Framework.BusinessLogic.DepartmentManager;
using EspEmployeeManager = ESP.Framework.BusinessLogic.EmployeeManager;
using EspDepartmentPositionManager = ESP.Framework.BusinessLogic.DepartmentPositionManager;
using EspUserManager = ESP.Framework.BusinessLogic.UserManager;
using EspModuleManager = ESP.Framework.BusinessLogic.ModuleManager;

namespace ESP.Compatible
{
    /// <summary>
    /// 员工
    /// </summary>
    [Obsolete("仅用于向前兼容，请使用更新的 ESP.Framework.Entity.UserInfo 或 ESP.Framework.Entity.EmployeeInfo 代替。")]
    public class Employee
    {
        /// <summary>
        /// 默认构造方法
        /// </summary>
        public Employee()
        {
        }

        /// <summary>
        /// 根据用户ID构造一个当前类实例
        /// </summary>
        /// <param name="uid">用户ID</param>
        public Employee(int uid)
        {
            EmployeeInfo e = EspEmployeeManager.Get(uid);
            if (e == null)
            {
                this.IntID = 0;
                return;
            }

            this.IntID = uid;
            this.SysID = uid.ToString();

            if (e != null)
            {
                ITCode = e.Username;
                Name = e.FullNameCN;
                EMail = e.Email;
                Mobile = e.MobilePhone;
                Telephone = e.Phone1;
                Address = e.Address;
                ID = e.Code;
            }
        }

        /// <summary>
        /// 唯一ID
        /// </summary>
        public int IntID { get; set; }

        /// <summary>
        /// 唯一ID
        /// </summary>
        public string SysID { get; set; }

        /// <summary>
        /// 代码(相当于ESP中的用户名)
        /// </summary>
        public string ITCode { get; set; }

        /// <summary>
        /// 名称(相当于ESP中的中文名)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get { return true; } }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string ID { get; set; }

        bool _isPositionLoaded = false;
        string _positionDescription = string.Empty;


        /// <summary>
        /// 
        /// </summary>
        public int DimissionStatus
        {
            get
            {
                string sql = @"SELECT status FROM sep_Employees WHERE userid=@userid";
                Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "userid", DbType.Int32, this.SysID);
                object status = db.ExecuteScalar(cmd);

                cmd = db.GetSqlStringCommand("select lastday from SEP_DimissionForm where userid=@userid and status not in(1,13)");
                db.AddInParameter(cmd, "userid", DbType.Int32, this.SysID);
                object lastday = db.ExecuteScalar(cmd);

                if (status == null)
                    return 0;
                else
                {
                    if (lastday != System.DBNull.Value && lastday != null)
                    {
                        //if (DateTime.Now > Convert.ToDateTime(lastday).AddDays(-1))
                        //{
                        return 8;
                        //}
                    }
                    return Convert.ToInt32(status);
                }
            }
        }
        /// <summary>
        /// 员工职位描述
        /// </summary>
        public string PositionDescription
        {
            get
            {
                // Try to load Employee position names
                if (_isPositionLoaded == false)
                {
                    lock (this)
                    {
                        if (_isPositionLoaded == false)
                        {
                            IList<EmployeePositionInfo> list = EspDepartmentPositionManager.GetEmployeePositions(this.IntID);
                            if (list != null && list.Count > 0)
                            {
                                _positionDescription = list[0].DepartmentPositionName;
                                for (int i = 1; i < list.Count; i++)
                                {
                                    _positionDescription += "," + list[i].DepartmentPositionName;
                                }
                            }

                            _isPositionLoaded = true;
                        }
                    }
                }

                return _positionDescription;
            }
        }

        /// <summary>
        /// 从ESP.Framework.Entity.EmployeeInfo对象创建当前类实例
        /// </summary>
        /// <param name="info">ESP.Framework.Entity.EmployeeInfo对象</param>
        /// <returns>当前类实例</returns>
        public static Employee CreateFromEmployeeInfo(EmployeeInfo info)
        {
            if (info == null)
                return null;

            Employee u = new Employee();

            u.ITCode = info.Username;
            u.Name = info.FullNameCN;
            u.EMail = info.Email;
            u.Mobile = info.MobilePhone;
            u.Telephone = info.Phone1;
            u.Address = info.Address;
            u.ID = info.Code;

            u.IntID = info.UserID;
            u.SysID = info.UserID.ToString();
            return u;
        }

        private int[] _DepartmentIDs = null;
        private List<string> _DepartmentNames = null;

        private void TryLoadDepartments()
        {
            if (_DepartmentIDs == null)
            {
                lock (this)
                {
                    if (_DepartmentIDs == null)
                    {
                        IList<EmployeePositionInfo> list = EspDepartmentPositionManager.GetEmployeePositions(this.IntID);
                        List<int> idList = new List<int>(list.Count);
                        List<string> nameList = new List<string>(list.Count);
                        for (int i = 0; i < list.Count; i++)
                        {
                            EmployeePositionInfo ep = list[i];
                            int id = ep.DepartmentID;
                            string name = ep.DepartmentName;
                            if (idList.IndexOf(id) < 0)
                            {
                                idList.Add(id);
                                nameList.Add(name);
                            }
                        }

                        _DepartmentIDs = idList.ToArray();
                        _DepartmentNames = nameList;
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前员工所属部门的ID列表
        /// </summary>
        /// <returns>当前员工所属部门的ID列表</returns>
        public int[] GetDepartmentIDs()
        {
            TryLoadDepartments();
            return _DepartmentIDs;
        }

        /// <summary>
        /// 获取当前员工所属部门的名称列表
        /// </summary>
        /// <returns>当前员工所属部门的名称列表</returns>
        public IList<string> GetDepartmentNames()
        {
            TryLoadDepartments();
            return _DepartmentNames;
        }

        /// <summary>
        /// 根据ItCode得到用户中文名称
        /// </summary>
        /// <param name="userCode">用户ItCode</param>
        /// <returns>用户中文名称</returns>
        public static string GetName(string userCode)
        {
            return UserManager.GetUserName(userCode);
        }

        /// <summary>
        /// 获取指定SysID的员工所属的部门列表
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        public static IList<Department> GetDepartments(int sysId)
        {
            IList<EmployeePositionInfo> list = EspDepartmentPositionManager.GetEmployeePositions(sysId);
            if (list == null)
                return new List<Department>();

            List<Department> list2 = new List<Department>(list.Count);
            foreach (EmployeePositionInfo epi in list)
            {
                Department d = DepartmentManager.GetDepartmentByPK(epi.DepartmentID);
                list2.Add(d);
            }
            return list2;
        }


        /// <summary>
        /// 通过中文名模糊查询员工
        /// </summary>
        /// <param name="name">搜索关键字</param>
        /// <returns>匹配的员工</returns>
        public static List<Employee> GetDataSetByName(string name)
        {
            string sql = @"
SELECT u.Username, u.FirstNameCN, u.LastNameCN, u.FirstNameEN, u.LastNameEN, u.Email,
	e.*, et.TypeName
FROM sep_Employees e
	JOIN sep_Users u ON e.UserID = u.UserID
	LEFT JOIN sep_EmployeeTypes et ON e.TypeID=et.TypeID 
WHERE (u.LastNameCN + u.FirstNameCN) LIKE @SearchKeyword
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "SearchKeyword", DbType.String, name);
            IList<EmployeeInfo> list = null;
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                list = ESP.Framework.DataAccess.Utilities.CBO.LoadList<EmployeeInfo>(reader);
            }
            if (list == null)
                return new List<Employee>();

            List<Employee> ret = new List<Employee>(list.Count);
            foreach (EmployeeInfo ei in list)
            {
                ret.Add(Employee.CreateFromEmployeeInfo(ei));
            }
            return ret;
        }

        /// <summary>
        /// 根据关键字和部门ID查询员工
        /// </summary>
        /// <param name="keyValue">关键字</param>
        /// <param name="depids">部门ID数组</param>
        /// <returns>匹配的员工</returns>
        public static DataSet GetDataSetUserByKey(string keyValue, int[] depids)
        {
            return GetDataSetUserByKey(keyValue, depids, "");
        }

        /// <summary>
        /// 根据关键字和部门ID及附加过滤条件查询员工
        /// </summary>
        /// <param name="keyValue">关键字</param>
        /// <param name="depids">部门ID数组</param>
        /// <param name="additionalFilter">附加过滤条件</param>
        /// <returns>匹配的员工</returns>
        public static DataSet GetDataSetUserByKey(string keyValue, int[] depids, string additionalFilter)
        {

            if (additionalFilter == null)
                additionalFilter = "";

            string departmentFilter;
            if (depids != null && depids.Length > 0)
            {
                departmentFilter = " AND (ep.DepartmentID=" + depids[0];
                for (int i = 1; i < depids.Length; i++)
                {
                    departmentFilter += " OR ep.DepartmentID=" + depids[i];
                }
                departmentFilter += ")";
            }
            else
            {
                departmentFilter = "";
            }

            String sql = null;
            //SqlParameter[] paras = null;
            if (!string.IsNullOrEmpty(keyValue))
            {
                keyValue = "%" + keyValue + "%";

                sql = @"
SELECT 
SysUserID=u.UserID,
UserID=e.Code,
UserCode=u.Username,
EMail=u.Email,
UserName=u.LastNameCN + u.FirstNameCN,
Telephone=e.Phone1,
Mobile=e.MobilePhone,
PositionDescription=dp.DepartmentPositionName + ' (' + d.DepartmentName + ')', 
Status=e.Status
FROM sep_Employees e 
	JOIN sep_Users u ON e.UserID=u.UserID
	JOIN sep_EmployeesInPositions ep ON e.UserID=ep.UserID
	JOIN sep_DepartmentPositions dp ON ep.DepartmentPositionID=dp.DepartmentPositionID
	JOIN sep_Departments d ON ep.DepartmentID=d.DepartmentID
WHERE ISNULL(u.IsDeleted, 0)=0
    " + departmentFilter + @"
	AND (dp.DepartmentPositionName LIKE @Keyword
		OR (u.LastNameCN + u.FirstNameCN) LIKE @Keyword
		OR e.Code LIKE @Keyword
		OR u.Email LIKE @Keyword
		OR u.Username LIKE @Keyword
		OR e.Phone1 LIKE @Keyword
		OR e.MobilePhone LIKE @Keyword) and (e.status in(1,3))
    " + additionalFilter + @"

ORDER BY u.UserID
";
                //paras = new SqlParameter[] { new SqlParameter("@Keyword", keyValue) };
            }
            else
            {
                sql = @"
SELECT 
SysUserID=u.UserID,
UserID=e.Code,
UserCode=u.Username,
EMail=u.Email,
UserName=u.LastNameCN + u.FirstNameCN,
Telephone=e.Phone1,
Mobile=e.MobilePhone,
PositionDescription=dp.DepartmentPositionName + ' (' + d.DepartmentName + ')', 
Status=e.Status
FROM sep_Employees e 
	JOIN sep_Users u ON e.UserID=u.UserID
	JOIN sep_EmployeesInPositions ep ON e.UserID=ep.UserID
	JOIN sep_DepartmentPositions dp ON ep.DepartmentPositionID=dp.DepartmentPositionID
	JOIN sep_Departments d ON ep.DepartmentID=d.DepartmentID
WHERE ISNULL(u.IsDeleted, 0)=0 and  (e.status in(1,3))
    " + departmentFilter + @"
    " + additionalFilter + @"

ORDER BY u.UserID
";
            }

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Keyword", DbType.String, keyValue);
            return db.ExecuteDataSet(cmd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="depids"></param>
        /// <param name="additionalFilter"></param>
        /// <returns></returns>
        public static DataSet GetDataSetUserByKeyLevel(string keyValue, int[] depids, string additionalFilter)
        {

            if (additionalFilter == null)
                additionalFilter = "";

            string departmentFilter;
            if (depids != null && depids.Length > 0)
            {
                departmentFilter = " AND (ep.DepartmentID=" + depids[0];
                for (int i = 1; i < depids.Length; i++)
                {
                    departmentFilter += " OR ep.DepartmentID=" + depids[i];
                }
                departmentFilter += ")";
            }
            else
            {
                departmentFilter = "";
            }

            String sql = null;
            //SqlParameter[] paras = null;
            if (!string.IsNullOrEmpty(keyValue))
            {
                keyValue = "%" + keyValue + "%";

                sql = @"
SELECT 
SysUserID=u.UserID,
UserID=e.Code,
UserCode=u.Username,
EMail=u.Email,
UserName=u.LastNameCN + u.FirstNameCN,
Telephone=e.Phone1,
Mobile=e.MobilePhone,
PositionDescription=dp.DepartmentPositionName + ' (' + d.DepartmentName + ')', 
Status=e.Status
FROM sep_Employees e 
	JOIN sep_Users u ON e.UserID=u.UserID
	JOIN sep_EmployeesInPositions ep ON e.UserID=ep.UserID
	JOIN sep_DepartmentPositions dp ON ep.DepartmentPositionID=dp.DepartmentPositionID
	JOIN sep_Departments d ON ep.DepartmentID=d.DepartmentID
WHERE ISNULL(u.IsDeleted, 0)=0
    " + departmentFilter + @"
	AND (dp.DepartmentPositionName LIKE @Keyword
		OR (u.LastNameCN + u.FirstNameCN) LIKE @Keyword
		OR e.Code LIKE @Keyword
		OR u.Email LIKE @Keyword
		OR u.Username LIKE @Keyword
		OR e.Phone1 LIKE @Keyword
		OR e.MobilePhone LIKE @Keyword) and (e.status in(1,3)) and dp.positionLevel<=9 
    " + additionalFilter + @"

ORDER BY u.UserID
";
                //paras = new SqlParameter[] { new SqlParameter("@Keyword", keyValue) };
            }
            else
            {
                sql = @"
SELECT 
SysUserID=u.UserID,
UserID=e.Code,
UserCode=u.Username,
EMail=u.Email,
UserName=u.LastNameCN + u.FirstNameCN,
Telephone=e.Phone1,
Mobile=e.MobilePhone,
PositionDescription=dp.DepartmentPositionName + ' (' + d.DepartmentName + ')', 
Status=e.Status
FROM sep_Employees e 
	JOIN sep_Users u ON e.UserID=u.UserID
	JOIN sep_EmployeesInPositions ep ON e.UserID=ep.UserID
	JOIN sep_DepartmentPositions dp ON ep.DepartmentPositionID=dp.DepartmentPositionID
	JOIN sep_Departments d ON ep.DepartmentID=d.DepartmentID
WHERE ISNULL(u.IsDeleted, 0)=0 and  (e.status in(1,3)) and dp.positionLevel<=9 
    " + departmentFilter + @"
    " + additionalFilter + @"

ORDER BY u.UserID
";
            }

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Keyword", DbType.String, keyValue);
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 根据关键字和部门ID查询员工
        /// </summary>
        /// <param name="keyValue">关键字</param>
        /// <param name="depid">部门ID</param>
        /// <returns>匹配的员工</returns>
        public static DataSet GetDataSetUserByKey_Department(string keyValue, int depid)
        {
            return GetDataSetUserByKey(keyValue, new int[] { depid }, "");
        }
    }

    /// <summary>
    /// 配置管理
    /// </summary>
    [Obsolete("仅用于向前兼容。")]
    public static class ConfigManager
    {
        /// <summary>
        /// 最大记录数
        /// </summary>
        public static int MaxRecordCount = 20;

        /// <summary>
        /// 登录Url
        /// </summary>
        public static string SiteLogonUrl
        {
            get
            {
                return ESP.Security.PassportAuthentication.GetLogoutUrl("/");
            }
        }

        /// <summary>
        /// LSF框架站点 出错页面
        /// </summary>
        private static string _defaultErrorPage = "ErrorMsg.aspx";

        /// <summary>
        /// 获取默认的错误页面名称
        /// </summary>
        public static string DefaultSiteErrorPage
        {
            get
            {
                return "/public/page/" + _defaultErrorPage;
            }
        }
    }

    /// <summary>
    /// 部门信息
    /// </summary>
    [Obsolete("仅用于向前兼容，请使用更新的 ESP.Framework.Entity.DepartmentInfo 代替。")]
    public class Department
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public int UniqID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string NodeName { get { return DepartmentName; } }

        /// <summary>
        /// 部门描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 部门级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 部门排序
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 部门状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 父级部门ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 根据一个ESP.Framework.Entity.DepartmentInfo对象构造一个当前类实例
        /// </summary>
        /// <param name="info"></param>
        public Department(DepartmentInfo info)
        {
            if (info != null)
            {
                UniqID = info.DepartmentID;
                DepartmentName = info.DepartmentName;
                Description = info.Description;
                Level = info.DepartmentLevel;
                Sort = info.Ordinal.ToString();
                Status = info.Status;
                Type = info.DepartmentLevel == 3 ? 1 : 0;
                ParentID = info.ParentID;
            }
        }

        /// <summary>
        /// 默认构造方法
        /// </summary>
        public Department()
        {
        }


        private Department _Parent = null;

        /// <summary>
        /// 父级部门对象
        /// </summary>
        public Department Parent
        {
            get
            {
                if (this.ParentID <= 0)
                    return null;

                if (_Parent != null)
                    return _Parent;
                lock (this)
                {
                    if (_Parent != null)
                        return _Parent;

                    DepartmentInfo pInfo = EspDepartmentManager.Get(ParentID);
                    if (pInfo != null)
                    {
                        _Parent = new Department(pInfo);
                    }

                    return _Parent;
                }
            }
        }
    }

    /// <summary>
    /// 部门管理
    /// </summary>
    [Obsolete("仅用于向前兼容，请使用更新的 ESP.BusinessControlling.EspDepartmentManager 代替。")]
    public static class DepartmentManager
    {
        /// <summary>
        /// 获取指定ID的部门
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>部门信息</returns>
        public static Department GetDepartmentByPK(int departmentId)
        {
            DepartmentInfo info = EspDepartmentManager.Get(departmentId);
            if (info == null)
                return null;

            Department dep = new Department(info);
            return dep;
        }

        /// <summary>
        /// 获取部门的所有直接子部门
        /// </summary>
        /// <param name="parentId">部门ID</param>
        /// <returns>所有直接子部门</returns>
        public static List<Department> GetByParent(int parentId)
        {
            IList<DepartmentInfo> list = EspDepartmentManager.GetChildren(parentId);
            List<Department> ret = new List<Department>();
            foreach (DepartmentInfo info in list)
            {
                ret.Add(new Department(info));
            }

            return ret;
        }

        /// <summary>
        /// 获取部门的所有直接子部门的ID和名称的对应列表，该方法用于AJAX加载数据
        /// </summary>
        /// <param name="parentId">部门ID</param>
        /// <returns>所有直接子部门的ID和名称的对应列表</returns>
        public static List<List<string>> GetListForAJAX(int parentId)
        {
            IList<DepartmentInfo> list = EspDepartmentManager.GetChildren(parentId);
            if (list == null)
                return new List<List<string>>();
            List<List<string>> ret = new List<List<string>>(list.Count);
            foreach (DepartmentInfo d in list)
            {
                ret.Add(new List<string>(new string[] { d.DepartmentID.ToString(), d.DepartmentName }));
            }

            return ret;
        }

        /// <summary>
        /// 获取部门的所有直接子部门的ID和名称的对应列表，该方法用于AJAX加载数据
        /// </summary>
        /// <param name="parentId">部门ID</param>
        /// <returns>所有直接子部门的ID和名称的对应列表</returns>
        public static List<List<string>> GetListForAJAXReports(int parentId)
        {
            IList<DepartmentInfo> list = EspDepartmentManager.GetChildren(parentId);
            if (list == null)
                return new List<List<string>>();
            List<List<string>> ret = new List<List<string>>(list.Count);
            foreach (DepartmentInfo d in list)
            {
                string ProjectAdministrativeIDs = System.Configuration.ConfigurationManager.AppSettings["ProjectAdministrativeIDs"];
                if (ProjectAdministrativeIDs.IndexOf("," + d.DepartmentID.ToString() + ",") < 0)
                    ret.Add(new List<string>(new string[] { d.DepartmentID.ToString(), d.DepartmentName }));
            }

            return ret;
        }

        /// <summary>
        ///  对字符串中的XML特殊字符做处理
        /// </summary>
        /// <param name="inStr">需要处理的字符串</param>
        /// <returns>处理结果</returns>
        public static string XmlEncode(string inStr)
        {
            if (inStr == "" || inStr == null)
            {
                return "";
            }

            string retStr = "";

            for (int i = 0; i < inStr.Length; i++)
            {
                switch (inStr.Substring(i, 1))
                {
                    case "&":
                        retStr += @"&amp;";
                        break;
                    case "\"":
                        retStr += @"&quot;";
                        break;
                    case "'":
                        retStr += @"&apos;";
                        break;
                    case "<":
                        retStr += @"&lt;";
                        break;
                    case ">":
                        retStr += @"&gt;";
                        break;
                    default:
                        retStr += inStr.Substring(i, 1);
                        break;
                }
            }

            return retStr;
        }

        private static void WriteDepartmentToXml(ESP.Tree<DepartmentInfo> tree, StringBuilder writer)
        {

            DepartmentInfo d = tree.Value;
            writer.Append("<row ");
            writer.Append("positionID=\"").Append(d.DepartmentID).Append("\" ");
            writer.Append("privilegeID=\"").Append(d.DepartmentID).Append("\" ");
            writer.Append("parentID=\"").Append(d.ParentID).Append("\" ");
            writer.Append("key=\"").Append(d.DepartmentID).Append("\" ");
            writer.Append("level=\"").Append(d.DepartmentLevel).Append("\" ");
            writer.Append("name=\"").Append(XmlEncode(d.DepartmentName)).Append("\" ");
            writer.Append("description=\"").Append(XmlEncode(d.Description)).Append("\" ");
            writer.Append("type=\"").Append(d.DepartmentTypeID).Append("\" ");
            writer.Append("sort=\"").Append(d.Ordinal).Append("\" ");
            writer.Append("enable=\"true\" ");
            writer.AppendLine(">");

            foreach (ESP.Tree<DepartmentInfo> child in tree)
            {
                WriteDepartmentToXml(child, writer);
            }

            writer.AppendLine("</row>");
        }

        /// <summary>
        /// 获取部门树的XML
        /// </summary>
        /// <returns>部门树的XML</returns>
        public static string GetXml()
        {
            ESP.Tree<DepartmentInfo> tree = EspDepartmentManager.GetDepartmentTree();

            StringBuilder writer = new StringBuilder();

            writer.AppendLine("<?xml version='1.0'?>");
            writer.AppendLine("<recordset level='0'>");
            foreach (ESP.Tree<DepartmentInfo> d in tree)
            {
                WriteDepartmentToXml(d, writer);
            }
            writer.AppendLine("</recordset>");

            return writer.ToString();

        }

        /// <summary>
        /// 获取部门树的HTML
        /// </summary>
        /// <param name="url">XSLT文件的Url</param>
        /// <returns>部门树的HTML</returns>
        public static string GetHtml(string url)
        {
            string xml = GetXml();

            XslTransform xtf = new XslTransform();
            xtf.Load(url);
            using (System.IO.StringWriter destStream = new System.IO.StringWriter())
            {
                using (System.IO.StringReader srcStream = new System.IO.StringReader(xml))
                {
                    xtf.Transform(new System.Xml.XPath.XPathDocument(srcStream), null, destStream);
                }
                return destStream.ToString();
            }
        }
    }


    /// <summary>
    /// 用户管理
    /// </summary>
    [Obsolete("仅用于向前兼容，请使用更新的 ESP.BusinessControlling.EspUserManager 代替。")]
    public class UserManager
    {
        /// <summary>
        /// 根据用户登录名获取用户中文名
        /// </summary>
        /// <param name="userCode">用户登录名</param>
        /// <returns>用户中文名</returns>
        public static string GetUserName(string userCode)
        {
            ESP.Framework.Entity.UserInfo u = EspUserManager.Get(userCode);
            return u == null ? null : u.FullNameCN;
        }

        /// <summary>
        /// 根据用户唯一ID获取用户的中文名
        /// </summary>
        /// <param name="sysId">唯一ID</param>
        /// <returns>用户中文名</returns>
        public static string GetUserName(int sysId)
        {
            ESP.Framework.Entity.UserInfo u = EspUserManager.Get(sysId);
            return u == null ? null : u.FullNameCN;
        }

        //public static string GetLogonToken(string itcode, string ip)
        //{
        //    return null;
        //}

    }

    //[Obsolete("仅用于向前兼容，请使用更新的 ESP.BusinessControlling.EspUserManager 代替。")]
    //public class SessionManager
    //{
    //    public static Employee GetUser(System.Web.SessionState.HttpSessionState session)
    //    {
    //        int uid = EspUserManager.GetCurrentUserID();
    //        return new Employee(uid);
    //    }

    //    public static void SetSignOut(System.Web.SessionState.HttpSessionState session)
    //    {
    //    }
    //}

    /// <summary>
    /// 用户权限管理
    /// </summary>
    [Obsolete("仅用于向前兼容，请使用更新的 ESP.BusinessControlling.EspModuleManager 代替。")]
    public class UserPrivilegeManager
    {
        /// <summary>
        /// 获取用户可访问的导航列表
        /// </summary>
        /// <param name="itcode">用户的ITCode</param>
        /// <returns>用户可访问的导航列表</returns>
        public static DataTable GetNavigateTreeDT(string itcode)
        {
            UserInfo u = EspUserManager.Get(itcode);
            int sysId = u == null ? 0 : u.UserID;

            return GetNavigateTreeDT(sysId);
        }

        /// <summary>
        /// 获取用户可访问的导航列表
        /// </summary>
        /// <param name="sysId">用户的SysID</param>
        /// <returns>用户可访问的导航列表</returns>
        public static DataTable GetNavigateTreeDT(int sysId)
        {
            string root = HttpRuntime.AppDomainAppVirtualPath;
            if (root.Length == 0 || root[root.Length - 1] != '/')
                root += '/';

            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("parentId", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("link", typeof(string));
            dt.Columns.Add("nodesort", typeof(string));
            IList<ModuleInfo> list = EspModuleManager.GetByUser(ESP.Configuration.ConfigurationManager.WebSiteID, sysId);
            foreach (ModuleInfo m in list)
            {
                DataRow item = dt.NewRow();
                item["id"] = m.ModuleID;
                item["parentId"] = m.ParentID;
                item["name"] = m.ModuleName;
                item["link"] = m.NodeType == ModuleType.Module ? root + m.DefaultPageUrl : String.Empty;
                item["nodesort"] = m.Ordinal;
                dt.Rows.Add(item);
            }
            return dt;
        }
    }
}
