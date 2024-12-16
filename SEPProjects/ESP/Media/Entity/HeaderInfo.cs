using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Media.Entity
{

    public class HeaderInfo
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string altname;

        public string AltName
        {
            get { return altname; }
            set { altname = value; }
        }


        public HeaderInfo(string name, string altname)
        {
            this.name = name;
            this.altname = altname;
        }

    }
}
