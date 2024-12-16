using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelTemplate
{
   public class Transition
    {
       private string _tname;
       private string _tto;
       private string _sname;
       public ITransfer _transfer;
       private int taskid;
       private int transid;

       public int TransitionID
       {
           get { return transid; }
           set { transid = value; }
       }

       public int ModelTaskID
       {
           get { return taskid; }
           set { taskid = value; }
       }
       public string TransitionName
       {
           get { return _tname; }
           set { _tname = value; }
       }

       public string TransitionTo
       {
           get { return _tto; }
           set { _tto = value; }
       }

       public string ScriptName
       {
           get { return _sname; }
           set { _sname = value; }
       }

       public ITransfer Transfer
       {
           get { return TransferFactory.CreateTransfer(ScriptName); }
       }
    }
}
