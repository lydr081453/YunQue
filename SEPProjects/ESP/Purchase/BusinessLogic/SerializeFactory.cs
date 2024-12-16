using System.Runtime.Serialization.Formatters.Binary;

namespace ESP.Purchase.BusinessLogic
{
    public static class SerializeFactory
    {
        /// <summary>
        /// Serializes the specified p obj.
        /// </summary>
        /// <param name="pObj">The p obj.</param>
        /// <returns></returns>
        public static byte[] Serialize(object pObj)
        {
            if (pObj == null)
            {
                return null;
            }
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
        /// Deserializes the object.
        /// </summary>
        /// <param name="pBytes">The p bytes.</param>
        /// <returns></returns>
        public static object DeserializeObject(byte[] pBytes)
        {
            object _newOjb = null;
            if (pBytes == null)
            {
                return _newOjb;
            }
            System.IO.MemoryStream _memory = new System.IO.MemoryStream(pBytes);
            _memory.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            _newOjb = formatter.Deserialize(_memory);
            _memory.Close();
            return _newOjb;
        }
    }
}