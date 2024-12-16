using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public class ITAssetCategoryInfo
    {
         public int Id { get; set; }
         public string Category { get; set; }
         public int  Sort{ get; set; }

    }
}
