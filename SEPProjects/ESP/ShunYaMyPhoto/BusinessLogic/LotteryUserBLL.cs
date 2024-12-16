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
    public class LotteryUserBLL
    {
        private LotteryUserSQLProvider DAL = new LotteryUserSQLProvider();

        public IList<LotteryUserInfo> GetList()
        {
            return DAL.GetList();
        }
    }
}