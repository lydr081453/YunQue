using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.BusinessLogic
{
  public  class StockFileReaderManager
    {
      private ESP.HumanResource.DataAccess.StockFileReaderDataProvider dataProvider = new ESP.HumanResource.DataAccess.StockFileReaderDataProvider();

        public List<ESP.HumanResource.Entity.StockFileReaderInfo> GetList(string sqlWhere)
        {
            return dataProvider.GetList(sqlWhere);
        }

        public int Add(ESP.HumanResource.Entity.StockFileReaderInfo StockFileReaderInfo)
        {
            return dataProvider.Add(StockFileReaderInfo);
        }
    }
}
