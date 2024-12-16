using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.DataAccess;
using ESP.HumanResource.Entity;
using System.Data;

namespace ESP.HumanResource.BusinessLogic
{
    public class EmailClosingManager
    {
        public readonly static EmailClosingProvider Provider = new EmailClosingProvider();

        public static int Add(EmailClosingInfo model)
        {
            return Provider.Add(model);
        }

        public static bool Update(EmailClosingInfo model)
        {
            return Provider.Update(model);
        }

        /// <summary>
        /// delete by userid
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns></returns>
        public static bool Delete(int userId)
        {
            return Provider.Delete(userId);
        }
        /// <summary>
        /// get model by userid
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns></returns>
        public static EmailClosingInfo GetModel(int userId)
        {
            return Provider.GetModel(userId);
        }

        public static List<EmailClosingInfo> GetList(string strWhere)
        {
            return Provider.GetList(strWhere);
        }
    }
}
