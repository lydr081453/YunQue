using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Core
{
    public interface ITask 
    {
        Portal.Model.TaskInfo Info { get; set; }
        void Init(object obj);
        void Execute();
        void Complete();
        void Error(Exception e);
    }
}
