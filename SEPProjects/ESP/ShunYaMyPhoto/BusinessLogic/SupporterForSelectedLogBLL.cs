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
    public class SupporterForSelectedBLL
    {
        private SupporterForSelectedSQLProvider DAL = new SupporterForSelectedSQLProvider();

        public SupporterForSelectedInfo GetModelByID(int id)
        {
            return DAL.GetModel(id);
        }

        public int Add(SupporterForSelectedInfo item)
        {
            return DAL.Add(item);
        }

        public bool Delete(int id)
        {
            DAL.Delete(id);
            return true;
        }

        public IList<SupporterForSelectedInfo> GetList(string condition)
        {
            return DAL.GetList(condition);
        }

        public IList<SupporterForSelectedInfo> GetTopList(string condition, int top)
        {
            return DAL.GetList(condition, top);
        }
    }
}