using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Portal.Common.Configuration.Task
{
    public class TaskConfigInfo
    {
        private DateTime _lastReadTaskConfigTime;
        public DateTime LastReadTaskConfigTime
        {
            get { return _lastReadTaskConfigTime; }
            set { _lastReadTaskConfigTime = value; }
        }

        private int _reloadMinutes;
        public int ReloadMinutes
        {
            get { return _reloadMinutes; }
            set { _reloadMinutes = value; }
        }

        private List<TaskConfigItemInfo> _items;
        public List<TaskConfigItemInfo> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public TaskConfigInfo(string configfilename)
        {
            _items = new List<TaskConfigItemInfo>();
            XmlDocument doc = new XmlDocument();
            doc.Load(configfilename);
            //设置Item
            XmlNodeList taskItemList = doc.GetElementsByTagName("Item");
            _reloadMinutes = int.Parse(doc.GetElementsByTagName("ReloadMinutes")[0].InnerText);
            _lastReadTaskConfigTime = DateTime.Now;
            //解析TaskConfigItemInfo
            foreach (XmlNode node in taskItemList)
            {
                Portal.Common.Configuration.Task.TaskConfigItemInfo item = new Portal.Common.Configuration.Task.TaskConfigItemInfo();
                item.Name = node["Name"].InnerText;
                item.Period = int.Parse(node["Period"].InnerText);
                item.Provider = node["Provider"].InnerText;
                item.Retry = int.Parse(node["Retry"].InnerText);
                _items.Add(item);
            }
        }
    }

    public class TaskConfigItemInfo
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _provider;
        public string Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        private int _period;
        public int Period
        {
            get { return _period; }
            set { _period = value; }
        }

        private int _retry;
        public int Retry
        {
            get { return _retry; }
            set { _retry = value; }
        }

        private int _sleepTime;
        public int SleepTime
        {
            get { return _sleepTime; }
            set { _sleepTime = value; }
        }
    }
}
