using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ModelTemplate
{
   public class ModelProcess
    {
       private string _processid;
       private string _processname;
       private ModelRoleSet mrs;
       private string _displayname;
       private string _version;
       private string _author;
       private IList<ModelTask> _tasks;
       #region structure from xml file

       #endregion

       
       public string Version
       {
           get { return _version; }
           set { _version=value;}
       }

       public string Author
       {
           get { return _author; }
           set { _author = value; }
       }

       public ModelRoleSet ModelRoleSet
       {
           get { return mrs; }
           set { mrs = value; }
       }

       public string ProcessID
       {
           get {
               return _processid;
           }
           set {
               _processid = value;
           }
       }

       public string Processname
       {
           get {
               return _processname;
           }
           set {
               _processname = value;
           }
       }

       public string DisPlayName
       {
           get { return _displayname; }
           set { _displayname = value; }
       }

       public IList<ModelTask> ModelTaskList
       {
           get
           {
               if (_tasks == null)
                   _tasks = new List<ModelTask>();
               return _tasks;
           }
           set
           {
               _tasks = value;
           }
     
       }
    }
}
