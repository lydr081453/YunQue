using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowLibary;
using ModelTemplate.BLL;
using ModelTemplate;
using WorkFlow.Model;
using IWorkFlowDAO;
using WorkFlowDAO;

namespace WorkFlowImpl
{
   public class WorkItemData
    {
       private WorkFlowDAO.WorkItemDataDao workitemdata = new WorkFlowDAO.WorkItemDataDao();
       public List<WorkFlowModel.WorkItemData> getProcessDataList(string roleid,string type)
       {
          return workitemdata.getProcessDataList(roleid,type);
       }

       public int insertItemData(int workitemid, int instanceid, byte[] data)
       {
           return workitemdata.insertItemData(workitemid, instanceid, data);
       }
    }
}
