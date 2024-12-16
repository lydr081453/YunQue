using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Media.DataAccess;
using ESP.Media.Entity;
using System.Data;

namespace ESP.Media.BusinessLogic
{
    public class ReporterEvaluationManager
    {
        public static readonly ReporterEvaluationDataProvider DataProvider = new ReporterEvaluationDataProvider();

        public static void Insert(ReporterEvaluation model)
        {
            DataProvider.Insert(model);
        }

        public static ReporterEvaluation Get(int id)
        {
            return DataProvider.Get(id);
        }

        public static DataSet GetReporterEvaluation(int reporterId)
        {
            return DataProvider.GetReporterEvaluation(reporterId);
        }

        public static DataSet GetReporterEvaluation(int reporterId,string userName)
        {
            return DataProvider.GetReporterEvaluation(reporterId,userName);
        }

        public static DataSet GetReporterEvaluation(string logIds)
        {
            return DataProvider.GetReporterEvaluation(logIds);
        }
    }
}
