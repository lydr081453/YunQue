using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowModel
{
  public  class WorkItemData
    {
      private int workitemid;
      private int instanceid;
      private int processid;
      private Object itemdata;
      private string itemname;

      public string WorkItemName
      {
          get { return itemname; }
          set { itemname = value; }
      }
      public int WorkItemID
      {
          get { return workitemid; }
          set { workitemid = value; }
      }
      public int InstanceID
      {
          get { return instanceid; }
          set { instanceid = value; }
      }

      public int ProcessID
      {
          get { return processid; }
          set { processid = value; }
      }

      public Object ItemData
      {
          get { return itemdata; }
          set { itemdata = value; }
      }
    }
}
