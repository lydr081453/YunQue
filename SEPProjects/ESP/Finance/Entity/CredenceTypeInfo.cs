﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    [Serializable]
    public partial class CredenceTypeInfo
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }

    }
}
