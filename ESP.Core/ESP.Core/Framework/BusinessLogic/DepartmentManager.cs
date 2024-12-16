
using System;
using System.Collections.Generic;

using System.Text;
using ESP.Framework.DataAccess;
using ESP.Framework.Entity;
using ESP.Framework;

namespace ESP.Framework.BusinessLogic
{
    /// <summary>
    /// 部门控制类
    /// </summary>
    public static class DepartmentManager
    {
        #region Private Members
        private static IDepartmentDataProvider GetProvider()
        {
            return ESP.Configuration.ProviderHelper<IDepartmentDataProvider>.Instance;
        }

        private static void PopulateChildren(Tree<DepartmentInfo> parent, DepartmentInfo parentDepartment, IList<DepartmentInfo> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                DepartmentInfo dep = list[i];
                if (dep.ParentID != parentDepartment.DepartmentID)
                    continue;

                Tree<DepartmentInfo> child = new Tree<DepartmentInfo>(dep.DepartmentID, dep.DepartmentName, dep);
                parent.AddChild(child);
                PopulateChildren(child, dep, list);
            }
        }

        #endregion

        /// <summary>
        /// 获取指定ID的部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>部门信息</returns>
        public static ESP.Framework.Entity.DepartmentInfo Get(int id)
        {
            return GetProvider().Get(id);
        }

        /// <summary>
        /// 获取指定名称的部门信息
        /// </summary>
        /// <param name="name">部门名称</param>
        /// <returns>部门信息</returns>
        public static IList<ESP.Framework.Entity.DepartmentInfo> Get(string name)
        {
            return GetProvider().Get(name);
        }

        /// <summary>
        /// 获取所有部门的信息列表
        /// </summary>
        /// <returns>所有部门列表</returns>
        public static IList<DepartmentInfo> GetAll()
        {
            return GetProvider().GetAll();
        }


        /// <summary>
        /// 获取部门所有子孙部门
        /// </summary>
        /// <param name="parentId">部门ID</param>
        /// <returns>所有部门列表</returns>
        public static IList<DepartmentInfo> GetChildrenRecursion(int parentId)
        {
            return GetProvider().GetChildrenRecursion(parentId);
        }

        public static IList<DepartmentInfo> GetChildrenRecursionByDesc(int descAreaId)
        {
            return GetProvider().GetChildrenRecursionByDesc(descAreaId);
        }

        /// <summary>
        /// 获取指定ID的部门的所有直接子级部门的信息列表
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>所有直接子级部门的信息列表</returns>
        public static IList<DepartmentInfo> GetChildren(int id)
        {
            return GetProvider().GetChildren(id);
        }

        /// <summary>
        /// 创建新的部门
        /// </summary>
        /// <param name="department">要创建的部门的信息</param>
        public static void Create(ESP.Framework.Entity.DepartmentInfo department)
        {
            GetProvider().Create(department);
        }

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="department">新的部门信息</param>
        public static void Update(ESP.Framework.Entity.DepartmentInfo department)
        {
            GetProvider().Update(department);
        }

        /// <summary>
        /// 删除具有指定ID的部门
        /// </summary>
        /// <param name="id">要删除的部门的ID</param>
        public static void Delete(int id)
        {
            GetProvider().Delete(id);
        }

        /// <summary>
        /// 获取树形结构的所有部门信息集合
        /// </summary>
        /// <returns>包含所有部门信息的ESP.Framework.Tree类型的树形结构</returns>
        public static Tree<DepartmentInfo> GetDepartmentTree()
        {
            Tree<DepartmentInfo> ret = new Tree<DepartmentInfo>(null, null, null);
            IList<DepartmentInfo> list = GetAll();
            for (int i = 0; i < list.Count; i++)
            {
                DepartmentInfo dep = list[i];
                if (dep.ParentID != 0)
                    continue;

                Tree<DepartmentInfo> t = new Tree<DepartmentInfo>(dep.DepartmentID, dep.DepartmentName, dep);
                PopulateChildren(t, dep, list);
                ret.AddChild(t);
            }

            return ret;
        }

        /// <summary>
        /// 根据指定ID递归方式获得此ID上级各个部门集合
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="depList">包含返回值的部门列表</param>
        /// <returns></returns>
        public static List<DepartmentInfo> GetDepartmentListByID(int id, List<DepartmentInfo> depList)
        {
            DepartmentInfo dep = Get(id);
            depList.Add(dep);
            if (dep.ParentID != 0)
            {
                GetDepartmentListByID(dep.ParentID, depList);
            }
            depList.Sort((p1, p2) => p1.DepartmentLevel.CompareTo(p2.DepartmentLevel));
            //depList.Sort(delegate(DepartmentInfo x, DepartmentInfo y) { return x.DepartmentID.CompareTo(y.DepartmentID); });
            return depList;
        }

        /// <summary>
        /// 根据部门获取员工
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>员工列表</returns>
        public static IList<EmployeeInfo> GetEmployeesByDepartment(int departmentId)
        {
            return GetProvider().GetEmployeesByDepartment(departmentId);
        }


        /// <summary>
        /// 根据部门代码获取部门信息
        /// </summary>
        /// <param name="code">部门代码</param>
        /// <returns>部门信息</returns>
        public static DepartmentInfo GetByCode(string code)
        {
            return GetProvider().GetByCode(code);
        }

        /// <summary>
        /// 检查部门代码是否已经存在
        /// </summary>
        /// <param name="code">部门代码</param>
        /// <returns>存在则返回 true，否则返回 false</returns>
        public static bool CodeExists(string code)
        {
            return GetProvider().CodeExists(code);
        }
    }
}
