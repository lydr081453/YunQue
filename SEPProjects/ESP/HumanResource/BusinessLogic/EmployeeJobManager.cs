/*********************************************************
 * 类中文名称：员工基本信息之工作合同、工资操作类
 * 类详细描述：
 * 
 * 
 * 
 * 
 * 主要制作人：zhouqi
 ********************************************************/
using System;
using System.Data;
using System.Collections.Generic;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using ESP.HumanResource.DataAccess;

namespace ESP.HumanResource.BusinessLogic
{    
    public class EmployeeJobManager
    {
        private static EmployeeJobDataProvider dal = new EmployeeJobDataProvider();
        public EmployeeJobManager()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条员工工作合同、工资数据
        /// </summary>
        public static int Add(EmployeeJobInfo model, SqlTransaction trans)
        {
            return dal.Add(model, trans);
        }

        /// <summary>
        /// 更新一条员工工作合同、工资数据
        /// </summary>
        public static void Update(EmployeeJobInfo model, SqlTransaction trans)
        {
            dal.Update(model, trans);
        }

        /// <summary>
        /// 删除一条员工工作合同、工资数据
        /// </summary>
        public static void Delete(int id, SqlTransaction trans)
        {

            dal.Delete(id, trans);
        }

        /// <summary>
        /// 根据用户ID删除员工工作合同、工资数据
        /// </summary>
        public static void DeleteBySysUserID(int sysid, SqlTransaction trans)
        {

            dal.Delete(sysid, trans);
        }

        /// <summary>
        /// 得到一个员工工作合同、工资对象实体
        /// </summary>
        public static EmployeeJobInfo GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 通过条件获得员工工作合同、工资数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 通过条件获得员工工作合同、工资数据列表
        /// </summary>
        public static List<EmployeeJobInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<EmployeeJobInfo> modelList = new List<EmployeeJobInfo>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                EmployeeJobInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new EmployeeJobInfo();
                    if (ds.Tables[0].Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[n]["id"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["sysid"].ToString() != "")
                    {
                        model.sysid = int.Parse(ds.Tables[0].Rows[n]["sysid"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["companyid"].ToString() != "")
                    {
                        model.companyid = int.Parse(ds.Tables[0].Rows[n]["companyid"].ToString());
                    }
                    model.companyName = ds.Tables[0].Rows[n]["companyName"].ToString();
                    if (ds.Tables[0].Rows[n]["departmentid"].ToString() != "")
                    {
                        model.departmentid = int.Parse(ds.Tables[0].Rows[n]["departmentid"].ToString());
                    }
                    model.departmentName = ds.Tables[0].Rows[n]["departmentName"].ToString();
                    if (ds.Tables[0].Rows[n]["groupid"].ToString() != "")
                    {
                        model.groupid = int.Parse(ds.Tables[0].Rows[n]["groupid"].ToString());
                    }
                    model.groupName = ds.Tables[0].Rows[n]["groupName"].ToString();
                    model.workPlace = ds.Tables[0].Rows[n]["workPlace"].ToString();
                    if (ds.Tables[0].Rows[n]["joinDate"].ToString() != "")
                    {
                        model.joinDate = DateTime.Parse(ds.Tables[0].Rows[n]["joinDate"].ToString());
                    }

                    model.joinJob = ds.Tables[0].Rows[n]["joinJob"].ToString();
                    model.directorName = ds.Tables[0].Rows[n]["directorName"].ToString();
                    model.directorJob = ds.Tables[0].Rows[n]["directorJob"].ToString();
                    model.joinjobID = int.Parse(ds.Tables[0].Rows[n]["joinjobID"].ToString());
                    model.memo = ds.Tables[0].Rows[n]["memo"].ToString();

                    model.selfIntroduction = ds.Tables[0].Rows[n]["selfIntroduction"].ToString();
                    model.objective = ds.Tables[0].Rows[n]["objective"].ToString();
                    model.workingExperience = ds.Tables[0].Rows[n]["workingExperience"].ToString();
                    model.educationalBackground = ds.Tables[0].Rows[n]["educationalBackground"].ToString();
                    model.languagesAndDialect = ds.Tables[0].Rows[n]["languagesAndDialect"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得全部员工工作合同、工资数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 通过SysId到入职信息
        /// </summary>
        /// <param name="sysid"></param>
        /// <returns></returns>
        public static EmployeeJobInfo getModelBySysId(int sysid)
        {
            return dal.getModelBySysId(sysid);
        }



        #endregion  成员方法
    }
}
