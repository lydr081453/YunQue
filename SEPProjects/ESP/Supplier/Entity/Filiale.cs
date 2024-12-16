using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class Filiale
    {
        private string _address;
        private string _linker;
        private string _tel;
        private string _fax;
        private string _fname;

        public string address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string linker
        {
            get { return _linker; }
            set { _linker = value; }
        }
        public string tel
        {
            get { return _tel; }
            set { _tel = value; }
        }
        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public string fname
        {
            get { return _fname;}
            set { _fname = value; }
        }
    }
}
