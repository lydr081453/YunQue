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
    public class SC_EnquiryListManager
    {
        private static readonly SC_EnquiryListDataProvider dal = new SC_EnquiryListDataProvider();
        public SC_EnquiryListManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SC_EnquiryList model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static void Update(SC_EnquiryList model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static void Delete(int ID)
        {

            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SC_EnquiryList GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<SC_EnquiryList> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<SC_EnquiryList> DataTableToList(DataTable dt)
        {
            List<SC_EnquiryList> modelList = new List<SC_EnquiryList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SC_EnquiryList model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SC_EnquiryList();
                    model.Id = int.Parse(dt.Rows[n]["ID"].ToString());
                    model.TemplateID = int.Parse(dt.Rows[n]["TemplateID"].ToString());
                    model.TypeID = int.Parse(dt.Rows[n]["TypeID"].ToString());
                    model.UserID = int.Parse(dt.Rows[n]["UserID"].ToString());
                    model.MessageId = int.Parse(dt.Rows[n]["MessageId"].ToString());
                    model.Note = dt.Rows[n]["Note"].ToString();
                    model.PEID = int.Parse(dt.Rows[n]["PEID"].ToString());
                    if (dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList("");
        }

        public static void DeleteByTemplateID(int TemplateId)
        {
            dal.DeleteByTemplateID(TemplateId);
        }

        public static List<SC_EnquiryList> GetListByTemplateID(int TemplateId)
        {
            DataSet ds = GetList(string.Format(" TemplateId={0}",TemplateId));
            List<SC_EnquiryList> list = new List<SC_EnquiryList>();
            if (ds.Tables.Count > 0)
            {
                list = DataTableToList(ds.Tables[0]);
            }
            return list;
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
