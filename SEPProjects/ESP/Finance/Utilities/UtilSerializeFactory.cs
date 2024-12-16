using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.Linq;
using System.Reflection;
using ESP.Compatible;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ESP.Finance.Utility
{
  
    public static class UtilSerializeFactory
    {

        sealed class DeserializationBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                //Type typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
                //return typeToDeserialize;
                if (assemblyName.StartsWith("ESP"))
                    return Type.GetType(typeName + "," + assemblyName);

                string tmpTypeName = string.Empty;
                tmpTypeName = typeName.Substring(typeName.LastIndexOf('.') + 1 + 2);
                if (tmpTypeName.LastIndexOf("Info") < 0)
                    tmpTypeName += "Info";

                if (assemblyName.StartsWith("App_Code") || assemblyName.StartsWith("PurchaseWeb"))
                    return Type.GetType("ESP.Purchase.Entity." + tmpTypeName + ",ESP");

                if (assemblyName.StartsWith("ProjectManagement.Model"))
                    return Type.GetType("ESP.Finance.Entity." + tmpTypeName + ",ESP");

                 if (typeName.IndexOf("ProjectManagement.Model") >= 0)
                {
                    string tmpAssembly = typeName.Replace(", ProjectManagement.Model,", ", ESP,");
                    tmpAssembly = tmpAssembly.Replace("ProjectManagement.Model.", "ESP.Finance.Entity.");

                    string header = tmpAssembly.Substring(0, tmpAssembly.IndexOf("F_"));
                    string ends = tmpAssembly.Substring(tmpAssembly.IndexOf(','));
                    string middle = tmpAssembly.Substring(tmpAssembly.IndexOf("F_"), tmpAssembly.IndexOf(',') - tmpAssembly.IndexOf("F_"));
                    middle = middle.Substring(2);
                    if (middle.LastIndexOf("Info") < 0)
                        middle += "Info";
                    return Type.GetType(header + middle + ends);
                }
                return Type.GetType(typeName + "," + assemblyName);
            }
        }


        /// <summary>
        /// 序列化对象的方法
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        public static byte[] ObjectSerialize(this object pObj)
        {
            if (pObj == null)
                return null;
            System.IO.MemoryStream _memory = new System.IO.MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(_memory, pObj);
            _memory.Position = 0;
            byte[] read = new byte[_memory.Length];
            _memory.Read(read, 0, read.Length);
            _memory.Close();
            return read;
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pBytes"></param>
        /// <returns></returns>
        public static T ObjectDeserialize<T>(this byte[] pBytes)
        {
            T obj;
            if (pBytes == null || pBytes.Length == 0)
                return default(T);


            System.IO.MemoryStream _memory = new System.IO.MemoryStream(pBytes);
            _memory.Seek(0, SeekOrigin.Begin);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Binder = new DeserializationBinder();
            obj = (T)formatter.Deserialize(_memory);
            _memory.Close();
            return obj;
        }
    }

}
