using System;
namespace ESP.Finance.Utility
{
    public static class StringHelper
    {
        public static string ReplaceQuote(string sourcestr)
        {
            return sourcestr.Replace("'", "''");
        }

        public static string SubString(string sourcestr, int length)
        {
            if (sourcestr.Length > length)
            {
                return sourcestr.Substring(0, length);
            }
            else
                return sourcestr;
        }

        public static string FormatPhoneLastChar(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                if (phone[phone.Length - 1] == '-')
                    phone = phone.Substring(0, phone.Length - 1);
            }
            return phone;
        }

        [Obsolete("使用该方法会降低性能，请使用 System.Int32.TryParse 替代。")]
        public static bool IsConvertInt(string pram)
        {
            try
            {
                int i = -1000000;
                i = int.Parse(pram);
                if (i != -1000000)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        [Obsolete("使用该方法会降低性能，请使用 System.Decimal.TryParse 替代。")]
        public static bool IsConvertDecimal(string pram)
        {
            try
            {
                decimal i = -1000000;
                i = decimal.Parse(pram);
                if (i != -1000000)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}