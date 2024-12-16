using System;
using System.Data;
using System.Reflection;

namespace ESP.Salary.Utility
{
    public class NullValue
    {
        public static short NullShort
        {
            get { return -1; }
        }

        public static int NullInteger
        {
            get { return -1; }
        }

        public static byte NullByte
        {
            get { return 255; }
        }

        public static Single NullSingle
        {
            get { return Single.MinValue; }
        }

        public static double NullDouble
        {
            get { return double.MinValue; }
        }

        public static decimal NullDecimal
        {
            get { return decimal.MinValue; }
        }

        public static DateTime NullDate
        {
            get { return DateTime.MinValue; }
        }

        public static string NullString
        {
            get { return ""; }
        }

        public static bool NullBoolean
        {
            get { return false; }
        }

        public static Guid NullGuid
        {
            get { return Guid.Empty; }
        }

        // sets a field to an application encoded null value ( used in BLL layer )
        public static object SetNull(object objValue, object objField)
        {
            object rtn = null;
            if (objValue == DBNull.Value)
            {
                if (objField is Int16)
                    rtn = NullShort;
                else if (objField is Byte)
                    rtn = NullByte;
                else if (objField is Int32)
                    rtn = NullInteger;
                else if (objField is Single)
                    rtn = NullSingle;
                else if (objField is Double)
                    rtn = NullDouble;
                else if (objField is Decimal)
                    rtn = NullDecimal;
                else if (objField is DateTime)
                    rtn = NullDate;
                else if (objField is string)
                    rtn = NullString;
                else if (objField is Boolean)
                    rtn = NullBoolean;
                else if (objField is Guid)
                    rtn = NullGuid;
                else // complex object
                    rtn = null;
            }
            else
            {
                // return value
                rtn = objValue;
            }
            return rtn;
        }

        // sets a field to an application encoded null value ( used in BLL layer )
        public static object SetNull(PropertyInfo objPropertyInfo)
        {
            object rtn = null;
            switch (objPropertyInfo.PropertyType.ToString())
            {
                case "System.Int16":
                    rtn = NullShort;
                    break;
                case "System.Int32":
                case "System.Int64":
                    rtn = NullInteger;
                    break;
                case "system.Byte":
                    rtn = NullByte;
                    break;
                case "System.Single":
                    rtn = NullSingle;
                    break;
                case "System.Double":
                    rtn = NullDouble;
                    break;
                case "System.Decimal":
                    rtn = NullDecimal;
                    break;
                case "System.DateTime":
                    rtn = NullDate;
                    break;
                case "System.String":
                case "System.Char":
                    rtn = NullString;
                    break;
                case "System.Boolean":
                    rtn = NullBoolean;
                    break;
                case "System.Guid":
                    rtn = NullGuid;
                    break;
                default:
                    // Enumerations default to the first entry
                    Type pType = objPropertyInfo.PropertyType;
                    if (pType.BaseType.Equals(typeof(System.Enum)))
                    {
                        System.Array objEnumValues = System.Enum.GetValues(pType);
                        Array.Sort(objEnumValues);
                        rtn = System.Enum.ToObject(pType, objEnumValues.GetValue(0));
                    }
                    else // complex object
                    {
                        rtn = null;
                    }
                    break;
            }
            return rtn;
        }

        // sets a field to an application encoded null value ( used in BLL layer )
        public static object GetNull(PropertyInfo objPropertyInfo)
        {
            object rtn = null;
            switch (objPropertyInfo.PropertyType.ToString())
            {
                case "System.Int16":
                    rtn = NullShort;
                    break;
                case "System.Int32":
                    rtn = NullInteger;
                    break;
                case "System.Int64":
                    rtn = NullInteger;
                    break;
                case "system.Byte":
                    rtn = NullByte;
                    break;
                case "System.Single":
                    rtn = NullSingle;
                    break;
                case "System.Double":
                    rtn = NullDouble;
                    break;
                case "System.Decimal":
                    rtn = NullDecimal;
                    break;
                case "System.DateTime":
                    rtn = NullDate;
                    break;
                case "System.String":
                    rtn = NullString;
                    break;
                case "System.Char":
                    rtn = NullString;
                    break;
                case "System.Boolean":
                    rtn = NullBoolean;
                    break;
                case "System.Guid":
                    rtn = NullGuid;
                    break;
                default:
                    // Enumerations default to the first entry
                    Type pType = objPropertyInfo.PropertyType;
                    if (pType.BaseType.Equals(typeof(System.Enum)))
                    {
                        System.Array objEnumValues = System.Enum.GetValues(pType);
                        Array.Sort(objEnumValues);
                        rtn = System.Enum.ToObject(pType, objEnumValues.GetValue(0));
                    }
                    else // complex object
                    {
                        rtn = null;
                    }
                    break;
            }
            return rtn;
        }

        // convert an application encoded null value to a database null value ( used in DAL )
        public static object GetNull(object objField, object objDBNull)
        {
            object rtn = objField;
            if (objField == null)
            {
                rtn = objDBNull;
            }
            else if (objField is Byte)
            {
                if (Convert.ToByte(objField) == NullByte)
                    rtn = objDBNull;
            }
            else if (objField is Int16)
            {
                if (Convert.ToInt16(objField) == NullShort)
                    rtn = objDBNull;
            }
            else if (objField is Int32)
            {
                if (Convert.ToInt32(objField) == NullInteger)
                    rtn = objDBNull;
            }
            else if (objField is Single)
            {
                if (Convert.ToSingle(objField) == NullSingle)
                    rtn = objDBNull;
            }
            else if (objField is Double)
            {
                if (Convert.ToDouble(objField) == NullDouble)
                    rtn = objDBNull;
            }
            else if (objField is Decimal)
            {
                if (Convert.ToDecimal(objField) == NullDecimal)
                    rtn = objDBNull;
            }
            else if (objField is DateTime)
            {
                // compare the Date part of the DateTime with the DatePart of the NullDate ( this avoids subtle time differences )
                if (Convert.ToDateTime(objField).Date == NullDate.Date)
                    rtn = objDBNull;
            }
            else if (objField is String)
            {
                if (objField == null)
                    rtn = objDBNull;
                else
                    if (objField.ToString() == NullString)
                        rtn = objDBNull;
            }
            else if (objField is Boolean)
            {
                if (Convert.ToBoolean(objField) == NullBoolean)
                    rtn = objDBNull;
            }
            else if (objField is Guid)
            {
                if ((Guid)objField == NullGuid)
                    rtn = objDBNull;
            }
            return rtn;
        }

        // checks if a field contains an application encoded null value
        public static bool IsNull(object objField)
        {
            bool rtn = false;
            if (objField != null)
            {
                if (objField is Int32)
                    rtn = objField.Equals(NullInteger);
                else if (objField is Int16)
                    rtn = objField.Equals(NullShort);
                else if (objField is Byte)
                    rtn = objField.Equals(NullByte);
                else if (objField is Single)
                    rtn = objField.Equals(NullSingle);
                else if (objField is Double)
                    rtn = objField.Equals(NullDouble);
                else if (objField is Decimal)
                    rtn = objField.Equals(NullDecimal);
                else if (objField is DateTime)
                {
                    DateTime objDate = (DateTime)objField;
                    rtn = objDate.Date.Equals(NullDate.Date);
                }
                else if (objField is String)
                    rtn = objField.Equals(NullString);
                else if (objField is Boolean)
                    rtn = objField.Equals(NullBoolean);
                else if (objField is Guid)
                    rtn = objField.Equals(NullGuid);
                else // complex object
                    rtn = false;
            }
            else
                rtn = true;
            return rtn;
        }
    }
}
