using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowLibary
{
    public interface IWfNode
    {
        IWFActivity ExtractorActivity();
    }
}
