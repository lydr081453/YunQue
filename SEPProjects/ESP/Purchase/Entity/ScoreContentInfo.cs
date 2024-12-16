using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类ScoreContentInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class ScoreContentInfo
    {
        public ScoreContentInfo()
        { }
        #region Model
        private int _scorecontentid;
        private string _description;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int ScoreContentID
        {
            set { _scorecontentid = value; }
            get { return _scorecontentid; }
        }
        /// <summary>
        /// 供应商评估项
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
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
