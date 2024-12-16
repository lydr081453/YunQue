using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

namespace ESP.HumanResource.Utilities
{
    public static class CBO
    {

        /// <summary>
        /// 根据DataRow得到一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static T CreateObject<T>(DataRow dr)
        {
            T obj = Activator.CreateInstance<T>();
            System.Collections.Hashtable properties = (System.Collections.Hashtable)GetPropertyInfo(obj.GetType());


            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = (PropertyInfo)properties[dr.Table.Columns[i].ColumnName.ToUpper()];
                if (propertyInfo != null)
                {
                    object nullValue = null;//NullValue.GetNull(propertyInfo);
                    if (dr[i] == DBNull.Value)
                    {
                        propertyInfo.SetValue(obj, nullValue, null);
                    }
                    else
                    {
                        try
                        {
                            propertyInfo.SetValue(obj, dr[i], null);
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


        public static List<T> FillCollection<T>(DataSet ds)
        {
            List<T> list = new List<T>();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0] == null) return list;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                T item = CreateObject<T>(dr);
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// 根据一个DataTable得到一个对象的List的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> FillCollection<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T item = CreateObject<T>(dr);
                list.Add(item);
            }

            return list;
        }

        public static IList<T> FillCollection<T>(DataTable dt, ref IList<T> listToFill)
        {
            foreach (DataRow dr in dt.Rows)
            {
                T item = CreateObject<T>(dr);
                listToFill.Add(item);
            }

            return listToFill;
        }

        public static T FillObject<T>(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return FillObject<T>(ds.Tables[0].Rows[0]);
            }
            return default(T);
        }

        public static T FillObject<T>(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return FillObject<T>(dt.Rows[0]);
            }
            return default(T);
        }

        public static T FillObject<T>(DataRow dr)
        {
            return FillObject<T>(dr, true);
        }

        public static T FillObject<T>(DataRow dr, bool ManageDataReader)
        {
            bool flag;
            T obj;
            if (ManageDataReader)
            {
                flag = false;
                if (dr != null)
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

            return obj;
        }


        /// <summary>
        /// 得到一个类中的属性
        /// </summary>
        /// <param name="objType"></param>
        /// <returns></returns>
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
                        objObject.Add(info.Name.ToUpper(), info);
                }
                HttpRuntime.Cache.Insert(objType.FullName, objObject);
            }
            return objObject;
        }
    }
}