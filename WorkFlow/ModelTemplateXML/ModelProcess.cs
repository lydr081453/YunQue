using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ModelTemplate.IDAL;

namespace ModelTemplate.XML
{
    public class ModelProcess : IModelProcess
    {
        private string xmlname;

        public string Xmlname
        {
            get { return xmlname; }
            set { xmlname = value; }
        }

        public  ModelTemplate.ModelProcess GetModelProcessByID(int processid)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "xml\\" + xmlname);
            XmlNode processnode = doc.SelectSingleNode("root/process-definition[@id='" + processid + "']");

           return initialize(doc, processnode, "root/process-definition[@id='" + processid + "']");

        }

        public  ModelTemplate.ModelProcess GetModelProcessByName(string processname)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory + "xml\\" + xmlname);
            XmlNode processnode = doc.SelectSingleNode("root/process-definition[@name='" + processname + "']");

            return initialize(doc, processnode, "root/process-definition[@name='" + processname + "']");

        }

        private ModelTemplate.ModelProcess initialize(XmlDocument doc, XmlNode processnode, string select)
        {
            ModelTemplate.ModelProcess process = new ModelTemplate.ModelProcess();

            process.ProcessID = processnode.Attributes["id"].Value;
            process.Processname = processnode.Attributes["name"].Value;
            process.DisPlayName = processnode.Attributes["displayname"].Value;
            process.Version = processnode.Attributes["version"].Value;
            process.Author = processnode.Attributes["author"].Value;

            XmlNodeList taskNode = doc.SelectNodes(select + "/node");

            IList<ModelTask> tasks = new List<ModelTask>();

            for (int i = 0; i < taskNode.Count; i++)
            {
                ModelTask mt = new ModelTask();
                mt.TaskID = taskNode[i].Attributes["id"].Value;
                mt.TaskName = taskNode[i].Attributes["name"].Value;
                mt.TaskType = taskNode[i].Attributes["type"].Value;
                mt.DisPlayName = taskNode[i].Attributes["displayname"].Value;
                mt.AutoExeActionName = taskNode[i].Attributes["autoexeactionname"].Value;
                mt.FormData = taskNode[i].Attributes["formdata"].Value;
                try
                {
                    if (taskNode[i].Attributes["deadlinequantity"].Value == string.Empty)
                        mt.DeadLineQuantity = 0;
                    else
                        mt.DeadLineQuantity = Convert.ToInt32(taskNode[i].Attributes["deadlinequantity"].Value);
                }
                catch
                {
                    mt.DeadLineQuantity = 0;
                }

                try
                {
                    if (taskNode[i].Attributes["opentype"].Value == string.Empty)
                        mt.OpenType = 0;
                    else
                        mt.OpenType = Convert.ToInt32(taskNode[i].Attributes["opentype"].Value);
                }
                catch
                {
                    mt.OpenType = 0;
                }
                mt.RoleName =taskNode[i].Attributes["rolename"].Value;

                XmlNodeList transnode = doc.SelectNodes(select + "/node[@name='" + mt.TaskName + "']" + "/transition");
                IList<Transition> trans = new List<Transition>();

                for (int j = 0; j < transnode.Count; j++)
                {
                    Transition t = new Transition();
                    t.TransitionName = transnode[j].Attributes["name"].Value;
                    t.TransitionTo = transnode[j].Attributes["to"].Value;
                    t.ScriptName = transnode[j].Attributes["scriptname"].Value;
                    trans.Add(t);

                }

                mt.Transations = trans;

                tasks.Add(mt);
            }

            process.ModelTaskList = tasks;

            return process;

        }

        public int ImportData(string processname, string displayname, string version, string author, List<ModelTemplate.ModelTask> tasks)
        {
            return 0;
        }
    }
}
