using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类ScoreRecordInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class ScoreRecordInfo
    {
        public ScoreRecordInfo()
        { }
        #region Model
        private int _recordid;
        private int _prid;
        private string _prno;
        private int _supplierid;
        private string _suppliername;
        private int _scorecontentid;
        private string _scorecontent;
        private int _scoreid;
        private string _scorename;
        private int _scores;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int RecordID
        {
            set { _recordid = value; }
            get { return _recordid; }
        }
        /// <summary>
        /// PR单ID
        /// </summary>
        public int PRID
        {
            set { _prid = value; }
            get { return _prid; }
        }
        /// <summary>
        /// PR单号
        /// </summary>
        public string PRNO
        {
            set { _prno = value; }
            get { return _prno; }
        }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public int SupplierID
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        /// <summary>
        /// 评估项的ID
        /// </summary>
        public int ScoreContentID
        {
            set { _scorecontentid = value; }
            get { return _scorecontentid; }
        }
        /// <summary>
        /// 评估项
        /// </summary>
        public string ScoreContent
        {
            set { _scorecontent = value; }
            get { return _scorecontent; }
        }
        /// <summary>
        /// 评分ID
        /// </summary>
        public int ScoreID
        {
            set { _scoreid = value; }
            get { return _scoreid; }
        }
        /// <summary>
        /// 评分级别
        /// </summary>
        public string ScoreName
        {
            set { _scorename = value; }
            get { return _scorename; }
        }
        /// <summary>
        /// 评分级别对应的分值
        /// </summary>
        public int Scores
        {
            set { _scores = value; }
            get { return _scores; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        #endregion Model

    }
}
