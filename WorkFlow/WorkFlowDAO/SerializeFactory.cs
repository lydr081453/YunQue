using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace WorkFlowDAO
{
    sealed class Temporary_PurchaseAppCodeWrapper : System.Runtime.Serialization.SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if(assemblyName.StartsWith("ESP"))
                return Type.GetType(typeName + "," + assemblyName);

            string tmpTypeName = string.Empty;
            tmpTypeName = typeName.Substring(typeName.LastIndexOf('.') + 1 + 2) ;
            if (tmpTypeName.LastIndexOf("Info") < 0)
                tmpTypeName += "Info";

            if (assemblyName.StartsWith("App_Code") || assemblyName.StartsWith("PurchaseWeb"))
                return Type.GetType("ESP.Purchase.Entity." + tmpTypeName + ",ESP");
            
            if (assemblyName.StartsWith("ProjectManagement.Model"))
                return Type.GetType("ESP.Finance.Entity." + tmpTypeName + ",ESP");

            //System.Collections.Generic.List`1[[ProjectManagement.Model.F_ProjectMember, ProjectManagement.Model, Version=1.0.3342.22845, Culture=neutral, PublicKeyToken=null]]。
            if (typeName.IndexOf("ProjectManagement.Model") >= 0)
            {
                string tmpAssembly = typeName.Replace(", ProjectManagement.Model,", ", ESP,");
                tmpAssembly = tmpAssembly.Replace("ProjectManagement.Model.", "ESP.Finance.Entity.");

                string header = tmpAssembly.Substring(0, tmpAssembly.IndexOf("F_") );
                string ends = tmpAssembly.Substring(tmpAssembly.IndexOf(','));
                string middle = tmpAssembly.Substring(tmpAssembly.IndexOf("F_"), tmpAssembly.IndexOf(',') - tmpAssembly.IndexOf("F_"));
                middle = middle.Substring(2);
                if (middle.LastIndexOf("Info") < 0)
                    middle += "Info";
                return Type.GetType(header +middle+ ends );
            }
            return Type.GetType(typeName + "," + assemblyName);
        }
    }

    public static class SerializeFactory
    {
        public static byte[] Serialize(object pObj)
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
        public static object DeserializeObject(byte[] pBytes)
        {
            object _newOjb = null;
            if (pBytes == null)
                return _newOjb;
            System.IO.MemoryStream _memory = new System.IO.MemoryStream(pBytes);
            _memory.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Binder = new Temporary_PurchaseAppCodeWrapper();
            _newOjb = formatter.Deserialize(_memory);
            _memory.Close();
            return _newOjb;
        }

    }
}
