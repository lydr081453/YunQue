using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.Common;
using ESP.Purchase.DataAccess;
using ESP.Purchase.Entity;

namespace ESP.Purchase.BusinessLogic
{
    /// <summary>
    ///T_Log 的摘要说明
    /// </summary>
    public static class ProjectRelationReporterPrivateManager
    {
        private static ProjectRelationReporterPrivateDataProvider dal = new ProjectRelationReporterPrivateDataProvider();

        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Add(ProjectRelationReporterPrivateInfo model)
        {
            return ProjectRelationReporterPrivateDataProvider.Add(model);
        }

        /// <summary>
        /// Updates the log.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static int Update(ProjectRelationReporterPrivateInfo model)
        {
            return ProjectRelationReporterPrivateDataProvider.Update(model);
        }

        /// <summary>
        /// Gets a info.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static ProjectRelationReporterPrivateInfo GetModelByID(int id)
        {
            return ProjectRelationReporterPrivateDataProvider.GetModel(id);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public static DataSet GetList(string strWhere)
        {
            return ProjectRelationReporterPrivateDataProvider.GetList(strWhere);
        }

        /// <summary>
        /// Deletes the specified STR where.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        public static void Delete(string strWhere)
        {
            ProjectRelationReporterPrivateDataProvider.Delete(strWhere);
        }
    }
}