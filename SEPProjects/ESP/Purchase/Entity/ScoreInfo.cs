using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class ScoreInfo
    {
        public ScoreInfo()
        { }
        #region Model
        private int _scoreid;
        private string _scorename;
        private int _scores;
        private int _scorecontentid;
        private string _scorecontent;
        private bool _isneedremark;
        private string _description;
        /// <summary>
        /// 
        /// </summary>
        public int ScoreID
        {
            set { _scoreid = value; }
            get { return _scoreid; }
        }
        /// <summary>
        /// 优良中差等的文字说明
        /// </summary>
        public string ScoreName
        {
            set { _scorename = value; }
            get { return _scorename; }
        }
        /// <summary>
        /// 优良中差等对应的分值
        /// </summary>
        public int Scores
        {
            set { _scores = value; }
            get { return _scores; }
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
        /// 供应商评估项
        /// </summary>
        public string ScoreContent
        {
            set { _scorecontent = value; }
            get { return _scorecontent; }
        }
        /// <summary>
        /// 是否需要描述
        /// </summary>
        public bool IsNeedRemark
        {
            set { _isneedremark = value; }
            get { return _isneedremark; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion Model

    }
}
