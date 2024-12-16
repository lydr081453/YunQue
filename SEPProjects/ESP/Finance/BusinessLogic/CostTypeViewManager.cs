using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ESP.Finance.BusinessLogic
{
   public static class CostTypeViewManager
    {

        private static ESP.Finance.IDataAccess.ICostTypeViewDataProvider DataProvider{get{return ESP.Configuration.ProviderHelper<ESP.Finance.IDataAccess.ICostTypeViewDataProvider>.Instance;}}
        //private const string _dalProviderName = "CostTypeDALProviderViewInfo";

        


        #region  成员方法

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ESP.Finance.Entity.CostTypeViewInfo GetModel(int costtypeId)
        {

            return DataProvider.GetModel(costtypeId);
        }

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.CostTypeViewInfo> GetAllList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.CostTypeViewInfo> GetList(string term)
        {
            return GetList(term, null);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static IList<ESP.Finance.Entity.CostTypeViewInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            return DataProvider.GetList(term, param);
        }

        public static IList<ESP.Finance.Entity.CostTypeViewInfo> GetLevel1List(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            if (string.IsNullOrEmpty(term))
            {
                term = " 1= 1 ";
            }
            if (param == null)
            {
                param = new List<System.Data.SqlClient.SqlParameter>();
            }
            term += " and a.typelevel = 1";
            return GetList(term, param);
        }

        public static Hashtable GetLevel2List(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            IList<ESP.Finance.Entity.CostTypeViewInfo> listall =GetList(term, param);
            Hashtable Items = new Hashtable();
            IEnumerable<ESP.Finance.Entity.CostTypeViewInfo> list_level_1 = from obj in listall where obj.TypeLevel == 1 select obj;
            IEnumerable<ESP.Finance.Entity.CostTypeViewInfo> list_level_2 = from obj in listall where obj.TypeLevel == 2 select obj;

            foreach (Entity.CostTypeViewInfo item in list_level_1)
            {
                IEnumerable<ESP.Finance.Entity.CostTypeViewInfo> list = from obj in list_level_2 where obj.ParentID == item.TypeID select obj;
                List<ESP.Finance.Entity.CostTypeViewInfo> itemlist = list.ToList<ESP.Finance.Entity.CostTypeViewInfo>();
                Items.Add(item.TypeName,itemlist) ;
            }
            return Items;
        }



        /// <summary>
        /// 根据关键字取得列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Level1List"> 返回一级物料列表</param>
        /// <returns></returns>
        public static Hashtable GetLevel2List(string key,ref List<ESP.Finance.Entity.CostTypeViewInfo> Level1List)
        {
            Level1List = new List<ESP.Finance.Entity.CostTypeViewInfo>();
            IList<ESP.Finance.Entity.CostTypeViewInfo> listall = GetAllList();
            Hashtable Items = new Hashtable();
            IEnumerable<ESP.Finance.Entity.CostTypeViewInfo> list_level_1 = from obj in listall where obj.TypeLevel == 1 select obj;

            IEnumerable<ESP.Finance.Entity.CostTypeViewInfo> list_level_2 = null;
            if (!string.IsNullOrEmpty(key))
            {
                list_level_2 = from obj in listall where obj.TypeLevel == 2 && obj.TypeName.IndexOf(key) != -1 select obj;
            }
            else
            {
                list_level_2 = from obj in listall where obj.TypeLevel == 2 select obj;
            }

            foreach (Entity.CostTypeViewInfo item in list_level_1)
            {
                IEnumerable<ESP.Finance.Entity.CostTypeViewInfo> list = from obj in list_level_2 where obj.ParentID == item.TypeID select obj;
                if (list != null && list.Count() > 0)
                {
                    List<ESP.Finance.Entity.CostTypeViewInfo> itemlist = list.ToList<ESP.Finance.Entity.CostTypeViewInfo>();
                    Items.Add(item.TypeName, itemlist);
                    Level1List.Add(item);
                }
            }
            return Items;
        }


        #endregion 获得数据列表
        #endregion  成员方法
    }
}
