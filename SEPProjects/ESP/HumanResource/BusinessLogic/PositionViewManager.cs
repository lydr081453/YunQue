using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.BusinessLogic
{
    public class PositionViewManager
    {
        private static readonly ESP.HumanResource.DataAccess.PositionViewProvider dal = new ESP.HumanResource.DataAccess.PositionViewProvider();
        public PositionViewManager()
        { }
        public List<ESP.HumanResource.Entity.PositionView> GetModelList(int deptid)
        {
            return dal.GetModelList(deptid);
        }
    }
}
