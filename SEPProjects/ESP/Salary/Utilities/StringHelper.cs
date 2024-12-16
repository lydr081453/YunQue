using System;
namespace ESP.Salary.Utility
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

        public static int ConvertStringToInt(string pram)
        {
            try
            {
                int result = 0;
                int.TryParse(pram, out result);
                return result;

            }
            catch
            {
                return 0;
            }
        }

        public static decimal ConvertStringToDecimal(string pram)
        {
            try
            {
                decimal result = 0;
                decimal.TryParse(pram, out result);
                return result;
            }
            catch
            {
                return 0;
            }
        }
    }
}