using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Administrative.Entity;
using ESP.Administrative.DataAccess;
using System.Data;
using ESP.Administrative.Common;

namespace ESP.Administrative.BusinessLogic
{
    /// <summary>
    /// 门卡信息业务类
    /// </summary>
    public class CardNOManager
    {
        private readonly CardNODataProvider dal = new CardNODataProvider();
        public CardNOManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(CardNOInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(CardNOInfo model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CardNOInfo GetModel(int ID)
        {
            return dal.GetModel(ID);
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
        /// 通过用户系统编号获得门卡信息
        /// </summary>
        /// <param name="userid">用户系统编号</param>
        /// <returns>返回用户门卡信息</returns>
        public CardNOInfo GetModelByUserId(int userid)
        {
            string strwhere = " userid={0} and deleted=0";
            if (userid != 0)
            {
                strwhere = string.Format(strwhere, userid);
            }
            DataSet ds = dal.GetList(strwhere);
            ESP.Administrative.Entity.CardNOInfo usercardinfo = new ESP.Administrative.Entity.CardNOInfo();
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                usercardinfo.PopupData(ds.Tables[0].Rows[0]);
            }
            return usercardinfo;
        }

        /// <summary>
        /// 获得门卡信息
        /// </summary>
        /// <param name="cardno">门卡号</param>
        /// <param name="cardNoState">获取不同状体的门卡信息</param>
        /// <returns></returns>
        public IList<CardNOInfo> GetModelByCardNo(string cardno, CardNoState cardNoState)
        {
            IList<CardNOInfo> list = new List<CardNOInfo>();
            StringBuilder strbuild = new StringBuilder();
            strbuild.Append(" deleted=0 ");
            switch ((int)cardNoState)
            {
                case (int)CardNoState.AllState:
                    break;
                case (int)CardNoState.Enable:
                    strbuild.Append(" AND ISEnable=1");
                    break;
                case (int)CardNoState.UnEnable:
                    strbuild.Append(" AND ISEnable=0");
                    break;
                default:
                break;
            }
            DataSet ds = dal.GetList(strbuild.ToString());

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                CardNOInfo cardNoInfo = new CardNOInfo();
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    cardNoInfo.PopupData(dr);
                    list.Add(cardNoInfo);
                }
            }
            return list;
        }

        #endregion  成员方法
    }
}
