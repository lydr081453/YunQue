using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelTemplate;

namespace ModelTemplate.IDAL
{
    public interface IModelProcess
    {
         ModelTemplate.ModelProcess GetModelProcessByID(int processid);
         ModelTemplate.ModelProcess GetModelProcessByName(string processname);
        /// <summary>
        /// import model process from business layer
        /// </summary>
        /// <param name="processname">process name</param>
        /// <param name="displayname">display name</param>
        /// <param name="version">version</param>
        /// <param name="author">authority user</param>
        /// <param name="tasks">work items list</param>
        /// <returns></returns>
         int ImportData(string processname, string displayname,string version,string author,List<ModelTemplate.ModelTask> tasks);
         
    }
}
