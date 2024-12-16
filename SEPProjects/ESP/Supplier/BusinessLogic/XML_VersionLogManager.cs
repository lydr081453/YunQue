using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Entity;
using ESP.Supplier.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.BusinessLogic
{
    public class XML_VersionLogManager
    {
        private readonly XML_VersionLogDataProvider dal = new XML_VersionLogDataProvider();
        public XML_VersionLogManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XML_VersionLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(XML_VersionLog model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XML_VersionLog GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<XML_VersionLog> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<XML_VersionLog> DataTableToList(DataTable dt)
        {
            List<XML_VersionLog> modelList = new List<XML_VersionLog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                XML_VersionLog model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new XML_VersionLog();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["VersionID"].ToString() != "")
                    {
                        model.VersionID = int.Parse(dt.Rows[n]["VersionID"].ToString());
                    }
                    model.Name = dt.Rows[n]["Name"].ToString();
                    model.TableName = dt.Rows[n]["TableName"].ToString();
                    model.Url = dt.Rows[n]["Url"].ToString();
                    model.Content = dt.Rows[n]["Content"].ToString();
                    model.Version = dt.Rows[n]["Version"].ToString();
                    model.InsertUser = dt.Rows[n]["InsertUser"].ToString();
                    model.InsertTime = dt.Rows[n]["InsertTime"].ToString();
                    model.InsertIP = dt.Rows[n]["InsertIP"].ToString();
                    model.UpdateUser = dt.Rows[n]["UpdateUser"].ToString();
                    model.UpdateTime = dt.Rows[n]["UpdateTime"].ToString();
                    model.UpdateIP = dt.Rows[n]["UpdateIP"].ToString();
                    if (dt.Rows[n]["ClassID"].ToString() != "")
                    {
                        model.ClassID = int.Parse(dt.Rows[n]["ClassID"].ToString());
                    }
                    model.XML = dt.Rows[n]["XML"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  成员方法
    }
}
