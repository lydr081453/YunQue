using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowModel;

namespace IWorkFlowDAO
{
   public interface IWorkItemDataDao
    {
       int insertItemData(int workitemid,int instanceid,byte[] data);
       List<WorkFlowModel.WorkItemData> getProcessDataList(string roleid,string type);
    }
}
