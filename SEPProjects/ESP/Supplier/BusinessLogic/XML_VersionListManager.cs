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
    public class XML_VersionListManager
    {
        private readonly XML_VersionListDataProvider dal = new XML_VersionListDataProvider();
        public XML_VersionListManager()
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
        public int Add(XML_VersionList model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 停用或启用物料类别
        /// </summary>
        /// <param name="typeid">The typeid.</param>
        public void BlockUpOrUse(int typeid, int level, int updateStatus)
        {
            dal.Delete(typeid, level, updateStatus);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(XML_VersionList model)
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
        public XML_VersionList GetModel(int ID)
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
        public List<XML_VersionList> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<XML_VersionList> DataTableToList(DataTable dt)
        {
            List<XML_VersionList> modelList = new List<XML_VersionList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                XML_VersionList model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new XML_VersionList();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
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

                    model.BJAuditor = dt.Rows[n]["BJAuditor"].ToString();
                    model.SHAuditor = dt.Rows[n]["SHAuditor"].ToString();
                    model.GZAuditor = dt.Rows[n]["GZAuditor"].ToString();
                    if (dt.Rows[n]["ClassID"].ToString() != "")
                    {
                        model.ClassID = int.Parse(dt.Rows[n]["ClassID"].ToString());
                    }
                    if (dt.Rows[n]["BJAuditorId"].ToString() != "")
                    {
                        model.BJAuditorId = int.Parse(dt.Rows[n]["BJAuditorId"].ToString());
                    }
                    if (dt.Rows[n]["SHAuditorId"].ToString() != "")
                    {
                        model.SHAuditorId = int.Parse(dt.Rows[n]["SHAuditorId"].ToString());
                    }
                    if (dt.Rows[n]["GZAuditorId"].ToString() != "")
                    {
                        model.GZAuditorId = int.Parse(dt.Rows[n]["GZAuditorId"].ToString());
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
        public DataSet GetAllListUsed()
        {
            return dal.GetList(1);
        }

         //只取得supplier已有的所有的3级物料
        public List<XML_VersionList> GetChooseList(int sid)
        {
            return dal.GetChooseList(sid);
        }

        //只取得supplier已有的所有的3级物料
        public List<XML_VersionList> GetChooseList(int sid, int pid)
        {
            return dal.GetChooseList(sid,pid);
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
