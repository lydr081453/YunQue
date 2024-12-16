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
    public class LotteryBLL
    {
        private LotterySQLProvider DAL = new LotterySQLProvider();

        public IList<LotteryInfo> GetList(string condition)
        {
            return DAL.GetList(condition);
        }

        public int Add(LotteryInfo model)
        {
            return DAL.Add(model);
        }

        public bool IsExist(int id)
        {
            return DAL.Exists(id);
        }
    }
}