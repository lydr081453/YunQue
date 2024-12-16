using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.ITIL.BusinessLogic.Maintenance
{
    /// <summary>
    /// 运维操作业务逻辑类
    /// 根据一个PR号或者流水号查询此PR包含的所有业务
    /// 1.接受PR号或者流水号
    /// 2.查出PR信息，显示其类别，并标明是否手填的项目号，如果是手填项目号要标明系统中是否存在此项目
    /// 3.查出全部收货信息
    /// 4.查出全部PN信息
    /// 5.查出全部PA信息
    /// 6.标明所有单据的下一步走向
    /// 注意：如果是媒体稿件费用或对私费用，需要将新生成的单据也要全部显示出来
    /// </summary>
    public partial class Operation
    {
        public static Entity.PrSnapshot GetPrSnapshot(int prId)
        {
            Entity.PrSnapshot snapshot = new ESP.ITIL.Entity.PrSnapshot();
            snapshot.BaseInfo = GetPrBaseInfo(prId);
            return snapshot;
        }

        #region 查询PR单信息
        internal static ESP.Purchase.Entity.GeneralInfo GetPrBaseInfo(int prId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 查询所有收货
        #endregion

        #region 查询P所有付款申请（PN）
        #endregion
    }
}
