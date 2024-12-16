using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPhotoInfo;
using MyPhotoSQLServerDAL;
using MyPhotoUtility;
using System.Data;
using System.Data.SqlClient;

namespace MyPhotoBLL
{
    public class PhotoBLL
    {
        private PhotoSQLProvider DAL = new PhotoSQLProvider();

        public PhotoInfo GetModelByID(int id)
        {
            return DAL.GetModel(id);
        }

        public int Add(PhotoInfo item)
        {
            return DAL.Add(item);
        }

        public int Update(PhotoInfo item)
        {
            return DAL.Update(item);
        }

        public bool Delete(int id)
        {
            DAL.Delete(id);
            return true;
        }

        public IList<PhotoInfo> GetList(string condition)
        {
            return DAL.GetList(condition);
        }

        public IList<PhotoInfo> GetList(string condition, string orderby)
        {
            return DAL.GetList(condition, orderby);
        }

        public DataSet GetDataSetList(string condition, string orderby)
        {
            return DAL.GetDataSetList(condition, orderby);
        }
        public DataSet GetDataSetList(string sql)
        {
            return DAL.GetDataSetList(sql);
        }
        public DataSet GetDataSetListForTop(string sql, int top)
        {
            return DAL.GetDataSetListForSelecedTop(sql, top);
        }

        public DataSet GetDataSetListForSelectedTop(string sql, int top)
        {
            return DAL.GetDataSetListForTop(sql, top);
        }
        public DataSet GetDataSetListForExp(string sql)
        {
            return DAL.GetDataSetListForExp(sql);
        }

        public IList<PhotoInfo> GetTopList(string condition, int top)
        {
            return DAL.GetList(condition, top);
        }
    }
}
