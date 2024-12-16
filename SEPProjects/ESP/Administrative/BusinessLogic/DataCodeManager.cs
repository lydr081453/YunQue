using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.DataAccess;
using ESP.Administrative.Entity;
using System.Data;

namespace ESP.Administrative.BusinessLogic
{
    public class DataCodeManager
    {
        private readonly DataCodeDataProvider dal = new DataCodeDataProvider();
        public DataCodeManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DataCodeID)
        {
            return dal.Exists(DataCodeID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DataCodeInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(DataCodeInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int DataCodeID)
        {
            dal.Delete(DataCodeID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataCodeInfo GetModel(int DataCodeID)
        {
            return dal.GetModel(DataCodeID);
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
        public DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// 获得一种类型的数据信息
        /// </summary>
        /// <param name="type">类型名称</param>
        /// <returns>返回一个类型的集合</returns>
        public List<DataCodeInfo> GetDataCodeByType(string type)
        {
            List<DataCodeInfo> list = new List<DataCodeInfo>();
            DataSet ds = this.GetList(" type= '" + type + "' and deleted='false'");
            if (ds != null && ds.Tables != null && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataCodeInfo datacode = new DataCodeInfo();
                    datacode.PopupData(dr);
                    list.Add(datacode);
                }
            }
            return list;
        }
        #endregion  成员方法
    }
}