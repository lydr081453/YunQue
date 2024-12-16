using System;
using System.Data;

namespace ESP.Supplier.Entity
{
    public class SC_DangerWord
    {
        #region Model
        private int _id;
        private string _word;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public string Word
        {
            set { _word = value; }
            get { return _word; }
        }
        #endregion Model

        public void PopupData(IDataReader r)
        {            
            if (null != r["id"] && r["id"].ToString() != "")
            {
                ID = int.Parse(r["id"].ToString());
            }

            Word = r["Word"].ToString();
        }
    }
}
