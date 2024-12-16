using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;

namespace ESP.Supplier.BusinessLogic
{
    public class SC_VendeeDepartmentTypeRelationManager
    {
        private static readonly SC_VendeeDepartmentTypeRelationDataProvider dal = new SC_VendeeDepartmentTypeRelationDataProvider();

        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SC_VendeeDepartmentTypeRelation model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SC_VendeeDepartmentTypeRelation model, SqlTransaction trans, SqlConnection conn)
        {
            return dal.Add(model,trans,conn);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(SC_VendeeDepartmentTypeRelation model)
        {
            dal.Update(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int CompanyDepartmentID)
        {
            dal.Delete(CompanyDepartmentID);
        }
        /// <summary>
        /// 根据供应商ID和得分类型删除一组数据
        /// </summary>
        public static void DeleteByCompanyDepartmentID(int CompanyDepartmentID, int TypeId)
        {
            dal.DeleteByCompanyDepartmentID(CompanyDepartmentID,TypeId);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SC_VendeeDepartmentTypeRelation GetModel(int Id)
        {
            return dal.GetModel(Id);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<SC_VendeeDepartmentTypeRelation> GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        public static List<SC_VendeeDepartmentTypeRelation> GetList(string strWhere, SqlParameter[] parameters)
        {
            return dal.GetList(strWhere, parameters);
        }
        public static List<SC_VendeeDepartmentTypeRelation> GetListByDepartmentId(int cid)
        {
            return dal.GetListByDepartmentId(cid);
        }
        public static List<SC_VendeeDepartmentTypeRelation> GetListBySupplierId(int cid, int typeid)
        {
            return dal.GetListBySupplierId(cid,typeid);
        }
        #endregion  成员方法
    }
}
