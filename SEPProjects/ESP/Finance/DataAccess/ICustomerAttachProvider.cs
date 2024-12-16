using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.IDataAccess
{
    [ESP.Configuration.Provider]
    public interface ICustomerAttachDataProvider
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int AttachID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Entity.CustomerAttachInfo model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(Entity.CustomerAttachInfo model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int AttachID);

        Entity.CustomerAttachInfo GetModel(int AttachID);


        /// <summary>
        /// 获得数据列表
        /// </summary>
        IList<Entity.CustomerAttachInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param);

    }
}
