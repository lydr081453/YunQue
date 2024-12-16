using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_Image 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class ProjectRelationReporterPrivateInfo
    {
        public ProjectRelationReporterPrivateInfo()
        { }

        #region Model
        private int _id;
        private int _reporterPrivate_id;
        private int _project_id;

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
        /// Gets or sets the supplier_id.
        /// </summary>
        /// <value>The supplier_id.</value>
        public int ReporterPrivateID
        {
            set { _reporterPrivate_id = value; }
            get { return _reporterPrivate_id; }
        }

        /// <summary>
        /// Gets or sets the imagename.
        /// </summary>
        /// <value>The imagename.</value>
        public int ProjectID
        {
            set { _project_id = value; }
            get { return _project_id; }
        }

        #endregion Model

        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["id"] && r["id"].ToString() != "")
            {
                _id = int.Parse(r["id"].ToString());
            }
            if (null != r["ProjectID"] && r["ProjectID"].ToString() != "")
            {
                _project_id = int.Parse(r["ProjectID"].ToString());
            }
            if (null != r["ProjectID"] && r["ReporterPrivateID"].ToString() != "")
            {
                _reporterPrivate_id = int.Parse(r["ReporterPrivateID"].ToString());
            }
        }
    }
}