using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_FieldImg
    {
        private int id;
        private int fieldId;
        private string largeimg;
        private string samllimg;
        private string imgdesc;

        public int Id { get { return id; } set { id = value; } }
        public int FieldId { get { return fieldId; } set { fieldId = value; } }
        public string LargeImg { get { return largeimg; } set { largeimg = value; } }
        public string SamllImg { get { return samllimg; } set { samllimg = value; } }
        public string ImgDesc { get { return imgdesc; } set { imgdesc = value; } }
    }
}
