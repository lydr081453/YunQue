using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AdminForm.Data
{
    public class CBO
    {
        private static T CreateObject<T>(IDataReader dr)
        {
            return (T)CreateObject(typeof(T), dr);
        }

        private static object CreateObject(Type type, IDataReader dr)
        {
            object obj = Activator.CreateInstance(type);

            var properties = TypeDescriptor.GetProperties(obj);

            for (int i = dr.FieldCount - 1; i >= 0; i--)
            {
                var p = properties.Find(dr.GetName(i), true);
                if (p == null || p.IsReadOnly)
                    continue;

                if (dr.IsDBNull(i))
                {
                    //p.SetValue(obj, null);
                    continue;
                }

                var value = dr.GetValue(i);
                if (value is DateTime)
                {
                    DateTime dt = (DateTime)value;
                    value = new DateTime(dt.Ticks, DateTimeKind.Utc);
                }
                p.SetValue(obj, value);

            }
            return obj;
        }

        public static List<T> FillCollection<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            return FillCollection<T>(dr, list);
        }


        public static List<int> FillInt32Collection(IDataReader dr)
        {
            using (dr)
            {
                List<int> list = new List<int>();
                while (dr.Read())
                {
                    list.Add(dr.GetInt32(0));
                }
                return list;
            }
        }

        public static List<T> FillCollection<T>(IDataReader dr, List<T> listToFill)
        {
            using (dr)
            {
                while (dr.Read())
                {
                    T item = CreateObject<T>(dr);
                    listToFill.Add(item);
                }
                return listToFill;
            }
        }

        public static List<T> FillCollection<T>(IDataReader dr, List<T> listToFill, Action<T, List<T>> addedCallback)
        {
            using (dr)
            {
                while (dr.Read())
                {
                    T item = CreateObject<T>(dr);
                    listToFill.Add(item);
                    addedCallback(item, listToFill);
                }
                return listToFill;
            }
        }

        public static T FillObject<T>(IDataReader dr)
        {
            using (dr)
            {
                if (dr.Read())
                {
                    return CreateObject<T>(dr);
                }
                return default(T);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="item">无意义，仅用于编译器自动识别 T 类型。</param>
        /// <returns></returns>
        public static List<TAnonymous> FillAnonymousCollection<TAnonymous>(IDataReader dr, TAnonymous item)
        {
            Type t = typeof(TAnonymous);
            List<TAnonymous> list = new List<TAnonymous>();
            using (dr)
            {
                while (dr.Read())
                {
                    TAnonymous i = (TAnonymous)CreateAnonymousObject(t, dr);
                    list.Add(i);
                }

                return list;
            }
        }

        private static object CreateAnonymousObject(Type t, IDataReader dr)
        {
            var cstr = t.GetConstructors()[0];
            var paras = cstr.GetParameters();
            object[] paraValues = new object[paras.Length];

            for (int i = dr.FieldCount - 1; i >= 0; i--)
            {
                ParameterInfo p = null;
                int index = -1;
                string name = dr.GetName(i);

                for (int j = paras.Length - 1; j >= 0; j--)
                {
                    var pj = paras[j];
                    if (string.Compare(pj.Name, name, true) == 0)
                    {
                        p = pj;
                        index = j;
                        break;
                    }
                }

                if (p == null)
                    continue;

                if (dr.IsDBNull(i))
                {
                    continue;
                }

                var value = dr.GetValue(i);
                if (value is DateTime)
                {
                    DateTime dt = (DateTime)value;
                    value = new DateTime(dt.Ticks, DateTimeKind.Utc);
                }
                paraValues[index] = value;

            }
            return cstr.Invoke(paraValues);
        }
    }
}