using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelTemplate.BLL
{
   public class ModelManager
    {
       private static readonly ModelTemplate.IDAL.IModelProcess dal = ModelProcessFactory.CreateModelProcess();

       public ModelProcess loadProcessModelByID(int processid)
       {
            return dal.GetModelProcessByID(processid);
       }

       public ModelTask loadProcessTask(string ProcessID, int Deadlinequant)
       {
           ModelTask retm = null;
           ModelProcess mp = null;

           mp = dal.GetModelProcessByName(ProcessID);


           IList<ModelTask> tasks = mp.ModelTaskList;
           for (int i = 0; i < tasks.Count; i++)
           {
               if (tasks[i].DeadLineQuantity == Deadlinequant)
               {
                   retm = tasks[i];
                   break;
               }
           }
           return retm;
       }

       public ModelProcess loadProcessModelByName(string processname)
       {
           return dal.GetModelProcessByName(processname);
       }

       public int ImportData(string processname, string displayname, string version, string author, List<ModelTemplate.ModelTask> tasks)
       {
           return dal.ImportData(processname,displayname,version,author,tasks);
       }
    }
}
