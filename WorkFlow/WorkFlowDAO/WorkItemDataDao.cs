using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IWorkFlowDAO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using WorkFlow.Model;
using WorkFlowLibary;
using System.Data;
using WorkFlowModel;

namespace WorkFlowDAO
{
   public class WorkItemDataDao:IWorkFlowDAO.IWorkItemDataDao
    {
       public List<WorkFlowModel.WorkItemData> getProcessDataList(string roleid,string type)
       {
           List<WorkFlowModel.WorkItemData> lists = new List<WorkFlowModel.WorkItemData>();

           Database db = DatabaseFactory.CreateDatabase();

           string sqlCommand = @"select we_workitemdata.ordercontent,p.workitemid,p.instanceid,p.processid,p.taskname from we_workitems as p 
                                inner join we_workitemdata on p.workitemid=we_workitemdata.workitemid 
                                inner join we_processinstances on p.instanceid=we_processinstances.instanceid
                                where roleid='" + roleid + "' and p.taskname like '" + type + "%' and p.state<100 and we_processinstances.processinstancestate<100";
             DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
           System.Data.SqlClient.SqlDataReader reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);

           while (reader.Read())
           {
               Object obj = null;
               obj = SerializeFactory.DeserializeObject((byte[])reader.GetValue(0));
               WorkFlowModel.WorkItemData item = new WorkItemData();
               item.WorkItemID = Convert.ToInt32(reader.GetValue(1));
               item.InstanceID = Convert.ToInt32(reader.GetValue(2));
               item.ProcessID = Convert.ToInt32(reader.GetValue(3));
               item.WorkItemName = reader.GetValue(4).ToString();
               item.ItemData = obj;
               lists.Add(item);
           }
           reader.Close();

           return lists;

       }

       public int insertItemData(int workitemid, int instanceid, byte[] data)
       {
           Database db = DatabaseFactory.CreateDatabase();
           string sqlCommand = "insert into we_workitemdata(WORKITEMID,INSTANCEID,INSERTDATE,ORDERTYPE,ORDERCONTENT) values(@WORKITEMID,@INSTANCEID,@INSERTDATE,@ORDERTYPE,@ORDERCONTENT)";
           DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@WORKITEMID", System.Data.DbType.Int64, workitemid);
           db.AddInParameter(dbCommand, "@INSTANCEID", System.Data.DbType.Int64, instanceid);
           db.AddInParameter(dbCommand, "@INSERTDATE", System.Data.DbType.DateTime, DateTime.Now);
           db.AddInParameter(dbCommand, "@ORDERTYPE", System.Data.DbType.String, "");
           db.AddInParameter(dbCommand, "@ORDERCONTENT", System.Data.DbType.Binary, data);
           return db.ExecuteNonQuery(dbCommand);;
       }

    }
}
