using System;
using System.Security.Cryptography;

namespace ESP.Utilities
{
    /// <summary>
    /// 加密辅助类
    /// </summary>
    public static class CryptoUtility
    {
        /// <summary>
        /// 生成高度随机字节序列
        /// </summary>
        /// <returns>高度随机字节序列</returns>
        public static byte[] GetRandomSequence()
        {
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return data;
        }

        /// <summary>
        /// 用AES算法加密数据
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后的数据</returns>
        public static byte[] EncryptData(byte[] data, string key)
        {
            return EncryptDataInternal(data, key, false);
        }

        /// <summary>
        /// 用AES算法解密数据
        /// </summary>
        /// <param name="data">要解密的数据</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的数据</returns>
        public static byte[] DecryptData(byte[] data, string key)
        {
            return EncryptDataInternal(data, key, true);
        }

        private static byte[] EncryptDataInternal(byte[] data, string key, bool isDecrypt)
        {
            if (data == null || data.Length == 0)
                return null;

            if (key == null || key.Length == 0)
                return null;


            string[] kniv = key.Split(',');

            if (kniv.Length != 2)
                return null;

            try
            {
                byte[] k = Convert.FromBase64String(kniv[0]);
                byte[] iv = Convert.FromBase64String(kniv[1]);
                AesCryptoServiceProvider provider = new AesCryptoServiceProvider();
                provider.KeySize = 128;
                provider.BlockSize = 128;
                provider.Padding = PaddingMode.Zeros;
                provider.Mode = CipherMode.CBC;
                ICryptoTransform trans = isDecrypt ? provider.CreateDecryptor(k, iv) : provider.CreateEncryptor(k, iv);
                byte[] outputBuf = trans.TransformFinalBlock(data, 0, data.Length);

                return outputBuf;
            }
            catch
            {
                return null;
            }
        }

        ///// <summary>
        ///// 生成AES加密密钥
        ///// </summary>
        ///// <returns>AES加密密钥</returns>
        //public static byte[] GenerateKey()
        //{
        //    TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
        //    provider.GenerateKey();
        //    return provider.Key;
        //}

        /// <summary>
        /// 将字节数组转换成Int32值。
        /// </summary>
        /// <param name="bytes">包含Int32值的数组</param>
        /// <param name="start">bytes中的起启位置，操作成功后，该参数返回下一个字节的位置</param>
        /// <returns>转换成的Int32值</returns>
        public static Int32 Int32FromBytes(byte[] bytes, ref int start)
        {
            if (bytes.Length - start < sizeof(Int32))
                throw new IndexOutOfRangeException();

            int value = 0;
            for (int i = 0; i < sizeof(Int32); i++)
            {
                int byteVal = bytes[start++];
                byteVal = byteVal << 8 * i;
                value |= byteVal;
            }

            return value;
        }

        /// <summary>
        /// 将字节数组转换成Int64值。
        /// </summary>
        /// <param name="bytes">包含Int64值的数组</param>
        /// <param name="start">bytes中的起启位置，操作成功后，该参数返回下一个字节的位置</param>
        /// <returns>转换成的Int64值</returns>
        public static Int64 Int64FromBytes(byte[] bytes, ref int start)
        {
            if (bytes.Length - start < sizeof(Int64))
                throw new IndexOutOfRangeException();

            long value = 0;
            for (int i = 0; i < sizeof(Int64); i++)
            {
                long byteVal = bytes[start++];
                byteVal = byteVal << 8 * i;
                value |= byteVal;
            }

            return value;
        }

        /// <summary>
        /// 将Int32值转换成字节数组。
        /// </summary>
        /// <param name="value">Int32值</param>
        /// <returns>转换成的字节数组</returns>
        public static byte[] Int32ToBytes(int value)
        {
            byte[] buf = new byte[sizeof(Int32)];
            for (int i = 0; i < sizeof(Int32); i++)
            {
                buf[i] = (byte)((value >> 8 * i) & 0xFF);
            }

            return buf;
        }

        /// <summary>
        /// 将Int64值转换成字节数组。
        /// </summary>
        /// <param name="value">Int64值</param>
        /// <returns>转换成的字节数组</returns>
        public static byte[] Int64ToBytes(long value)
        {
            byte[] buf = new byte[sizeof(Int64)];
            for (int i = 0; i < sizeof(Int64); i++)
            {
                buf[i] = (byte)((value >> 8 * i) & 0xFF);
            }

            return buf;
        }

    }
}
