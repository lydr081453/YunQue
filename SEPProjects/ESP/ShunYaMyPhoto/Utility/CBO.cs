using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

namespace MyPhotoUtility
{
    public class CBO
    {
        private static T CreateObject<T>(IDataReader dr)
        {
            T obj = Activator.CreateInstance<T>();
            System.Collections.Hashtable properties = (System.Collections.Hashtable)GetPropertyInfo(obj.GetType());


            for (int i = 0; i < dr.FieldCount; i++)
            {
                var n = dr.GetName(i).ToLowerInvariant();
                PropertyInfo propertyInfo = (PropertyInfo)properties[n];
                if (propertyInfo != null)
                {
                    //   object nullValue = GetNull(propertyInfo.PropertyType);
                    if (dr.IsDBNull(i))
                    {
                        //     propertyInfo.SetValue(obj, nullValue, null);
                    }
                    else
                    {
                        try
                        {
                            propertyInfo.SetValue(obj, dr.GetValue(i), null);
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.Assert(true, e.ToString());
                        }
                    }
                }
            }
            return obj;
        }

        public static List<T> FillCollection<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                T item = CreateObject<T>(dr);
                list.Add(item);
            }
            if (dr != null)
            {
                dr.Close();
            }
            return list;
        }

        public static List<T> FillCollection<T>(IDataReader dr, int count)
        {
            List<T> list = new List<T>();
            while (dr.Read() && count > 0)
            {
                T item = CreateObject<T>(dr);
                list.Add(item);
                count--;
            }
            return list;
        }

        public static IList<T> FillCollection<T>(IDataReader dr, ref IList<T> listToFill)
        {
            while (dr.Read())
            {
                T item = CreateObject<T>(dr);
                listToFill.Add(item);
            }
            if (dr != null)
            {
                dr.Close();
            }
            return listToFill;
        }

        public static T FillObject<T>(IDataReader dr)
        {
            return FillObject<T>(dr, true);
        }

        public static T FillObject<T>(IDataReader dr, bool ManageDataReader)
        {
            bool flag;
            T obj;
            if (ManageDataReader)
            {
                flag = false;
                if (dr.Read())
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                obj = CreateObject<T>(dr);
            }
            else
            {
                obj = default(T);
            }
            if (ManageDataReader && (dr != null))
            {
                dr.Close();
            }
            return obj;
        }

        public static System.Collections.Hashtable GetPropertyInfo(Type objType)
        {
            string CacheKey = "BOProperties[" + objType.AssemblyQualifiedName + "]";

            System.Collections.Hashtable objObject = (System.Collections.Hashtable)HttpRuntime.Cache.Get(CacheKey);
            if (objObject == null)
            {
                objObject = new System.Collections.Hashtable();
                foreach (PropertyInfo info in objType.GetProperties())
                {
                    if (info.CanWrite && (info.PropertyType.Namespace.StartsWith("System") || info.PropertyType.IsEnum || info.PropertyType.IsPrimitive))
                        objObject.Add(info.Name.ToLowerInvariant(), info);
                }
                HttpRuntime.Cache.Insert(objType.FullName, objObject);
            }
            return objObject;
        }
    }
}