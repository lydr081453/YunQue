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
    public class AdminBLL
    {
        private AdminSQLProvider DAL = new AdminSQLProvider();

        public IList<AdminInfo> GetList(string condition)
        {
            return DAL.GetList(condition);
        }

        public bool IsExist(int id)
        {
            return DAL.Exists(id);
        }
    }
}