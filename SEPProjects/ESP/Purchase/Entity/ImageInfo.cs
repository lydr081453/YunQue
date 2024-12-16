using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_Image 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class ImageInfo
    {
        public ImageInfo()
        { }

        #region Model
        private int _id;
        private int _supplier_id;
        private string _imagename;
        private string _imageurl;

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
        public int supplier_id
        {
            set { _supplier_id = value; }
            get { return _supplier_id; }
        }

        /// <summary>
        /// Gets or sets the imagename.
        /// </summary>
        /// <value>The imagename.</value>
        public string imagename
        {
            set { _imagename = value; }
            get { return _imagename; }
        }

        /// <summary>
        /// Gets or sets the imageurl.
        /// </summary>
        /// <value>The imageurl.</value>
        public string imageurl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
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
            if (null != r["supplier_id"] && r["supplier_id"].ToString() != "")
            {
                _supplier_id = int.Parse(r["supplier_id"].ToString());
            }
            _imagename = r["imagename"].ToString();
            _imageurl = r["imageurl"].ToString();
        }
    }
}