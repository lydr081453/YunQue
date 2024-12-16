using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_FieldImgShunyaUserAdded
    {
        private int id;
        private int tempFieldId;
        private string largeimg;
        private string samllimg;
        private string imgdesc;
        private DateTime createdate;

        public int Id { get { return id; } set { id = value; } }
        public int TempFieldId { get; set; }
        public string LargeImg { get { return largeimg; } set { largeimg = value; } }
        public string SamllImg { get { return samllimg; } set { samllimg = value; } }
        public string ImgDesc { get { return imgdesc; } set { imgdesc = value; } }
        public DateTime CreateDate { get { return createdate; } set { createdate = value; } }
    }
}
