using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Utilities
{
    public static class StringHelper
    {
        public static bool IsConvertInt(string pram)
        {
            try
            {
                int result = 0;
                return int.TryParse(pram, out result);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsConvertDecimal(string pram)
        {
            try
            {
                decimal result = 0;
                return decimal.TryParse(pram, out result);
            }
            catch
            {
                return false;
            }
        }
    }
}
