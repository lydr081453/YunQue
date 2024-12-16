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
    public class SupporterBLL
    {
        private SupporterSQLProvider DAL = new SupporterSQLProvider();

        public SupporterInfo GetModelByID(int id)
        {
            return DAL.GetModel(id);
        }

        public int Add(SupporterInfo item)
        {
            return DAL.Add(item);
        }

        public bool Delete(int id)
        {
            DAL.Delete(id);
            return true;
        }

        public IList<SupporterInfo> GetList(string condition)
        {
            return DAL.GetList(condition);
        }

        public IList<SupporterInfo> GetTopList(string condition, int top)
        {
            return DAL.GetList(condition, top);
        }
    }
}