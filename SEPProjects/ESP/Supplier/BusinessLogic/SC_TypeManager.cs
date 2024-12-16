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
    public class SC_TypeManager
    {
        private readonly SC_TypeDataProvider dal = new SC_TypeDataProvider();
        public SC_TypeManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SC_Type model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_Type model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            dal.Delete(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_Type GetModel(int Id)
        {

            return dal.GetModel(Id);
        }
        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SC_Type> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SC_Type> DataTableToList(DataTable dt)
        {
            List<SC_Type> modelList = new List<SC_Type>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SC_Type model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SC_Type();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.TypeName = dt.Rows[n]["TypeName"].ToString();
                    model.TypeShortName = dt.Rows[n]["TypeShortName"].ToString();
                    model.Path = dt.Rows[n]["Path"].ToString();
                    if (dt.Rows[n]["parentId"].ToString() != "")
                    {
                        model.parentId = int.Parse(dt.Rows[n]["parentId"].ToString());
                    }
                    if (dt.Rows[n]["typelevel"].ToString() != "")
                    {
                        model.typelevel = int.Parse(dt.Rows[n]["typelevel"].ToString());
                    }
                    if (dt.Rows[n]["CreatTime"].ToString() != "")
                    {
                        model.CreatTime = DateTime.Parse(dt.Rows[n]["CreatTime"].ToString());
                    }
                    if (dt.Rows[n]["LastUpdateTime"].ToString() != "")
                    {
                        model.LastUpdateTime = DateTime.Parse(dt.Rows[n]["LastUpdateTime"].ToString());
                    }
                    if (dt.Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = int.Parse(dt.Rows[n]["Type"].ToString());
                    }
                    if (dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Rows[n]["BJAuditorId"].ToString() != "")
                    {
                        model.BJAuditorId = int.Parse(dt.Rows[n]["BJAuditorId"].ToString());
                    }
                    model.BJAuditor = dt.Rows[n]["BJAuditor"].ToString();
                    if (dt.Rows[n]["SHAuditorId"].ToString() != "")
                    {
                        model.SHAuditorId = int.Parse(dt.Rows[n]["SHAuditorId"].ToString());
                    }
                    model.SHAuditor = dt.Rows[n]["SHAuditor"].ToString();
                    if (dt.Rows[n]["GZAuditorId"].ToString() != "")
                    {
                        model.GZAuditorId = int.Parse(dt.Rows[n]["GZAuditorId"].ToString());
                    }
                    model.GZAuditor = dt.Rows[n]["GZAuditor"].ToString();
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

        //只取得supplier已有的所有的3级物料
        public List<SC_Type> GetSupplierL3TypeListBySupplierId(int sid)
        {
            
            DataSet ds = dal.GetSupplierL3TypeListBySupplierId(sid);
            DataTable dt = ds.Tables[0];
            List<SC_Type> modelList = new List<SC_Type>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SC_Type model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SC_Type();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.TypeName = dt.Rows[n]["TypeName"].ToString();
                    model.TypeShortName = dt.Rows[n]["TypeShortName"].ToString();
                    model.Path = dt.Rows[n]["Path"].ToString();
                    if (dt.Rows[n]["parentId"].ToString() != "")
                    {
                        model.parentId = int.Parse(dt.Rows[n]["parentId"].ToString());
                    }
                    if (dt.Rows[n]["typelevel"].ToString() != "")
                    {
                        model.typelevel = int.Parse(dt.Rows[n]["typelevel"].ToString());
                    }
                    if (dt.Rows[n]["CreatTime"].ToString() != "")
                    {
                        model.CreatTime = DateTime.Parse(dt.Rows[n]["CreatTime"].ToString());
                    }
                    if (dt.Rows[n]["LastUpdateTime"].ToString() != "")
                    {
                        model.LastUpdateTime = DateTime.Parse(dt.Rows[n]["LastUpdateTime"].ToString());
                    }
                    if (dt.Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = int.Parse(dt.Rows[n]["Type"].ToString());
                    }
                    if (dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Rows[n]["BJAuditorId"].ToString() != "")
                    {
                        model.BJAuditorId = int.Parse(dt.Rows[n]["BJAuditorId"].ToString());
                    }
                    model.BJAuditor = dt.Rows[n]["BJAuditor"].ToString();
                    if (dt.Rows[n]["SHAuditorId"].ToString() != "")
                    {
                        model.SHAuditorId = int.Parse(dt.Rows[n]["SHAuditorId"].ToString());
                    }
                    model.SHAuditor = dt.Rows[n]["SHAuditor"].ToString();
                    if (dt.Rows[n]["GZAuditorId"].ToString() != "")
                    {
                        model.GZAuditorId = int.Parse(dt.Rows[n]["GZAuditorId"].ToString());
                    }
                    model.GZAuditor = dt.Rows[n]["GZAuditor"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        #endregion  成员方法
    }
}
