namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_PolicyFlow 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class PolicyFlowInfo
    {
        public PolicyFlowInfo()
        { }

        #region Model
        private int _id;
        private string _title;
        private string _synopsis;
        private string _contents;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        /// <value>The title.</value>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 简介
        /// </summary>
        /// <value>The synopsis.</value>
        public string synopsis
        {
            set { _synopsis = value; }
            get { return _synopsis; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        /// <value>The contents.</value>
        public string contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        #endregion Model
    }
}