using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ESP.HumanResource.Common
{
    /// <summary>
    /// 对称加密算法类
    /// </summary>
    public class SymmetricCrypto
    {
        private SymmetricAlgorithm mobjCryptoService;
        private string Key;
        /// <summary>
        /// 对称加密类的构造函数
        /// </summary>
        public SymmetricCrypto()
        {
            mobjCryptoService = new RijndaelManaged();
            Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
        }

        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            string sTemp = Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIV()
        {
            string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public string EncrypString(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Source">待解密的串</param>
        /// <returns>经过解密的串</returns>
        public string DecrypString(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }


    public class HashAlgorithm
    {
        /// <summary>
        /// Computes the hash by SH a1.
        /// </summary>
        /// <param name="textToHash">The text to hash.</param>
        /// <returns></returns>
        public static string ComputeHashBySHA1(string textToHash)
        {
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(textToHash);
            byte[] byteHash = SHA1.ComputeHash(byteValue);
            SHA1.Clear();

            return Convert.ToBase64String(byteHash);
        }

        /// <summary>
        /// Computes the hash by SH a512.
        /// </summary>
        /// <param name="textToHash">The text to hash.</param>
        /// <returns></returns>
        public static string ComputeHashBySHA512(string textToHash)
        {
            System.Security.Cryptography.SHA512Managed SHA512 = new SHA512Managed();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(textToHash);
            byte[] byteHash = SHA512.ComputeHash(byteValue);
            SHA512.Clear();
            return Convert.ToBase64String(byteHash);
        }
    }

    public sealed class DPAPIAlgorithm
    {
        // use local machine or user to encrypt and decrypt the data
        public enum Store
        {
            Machine,
            User
        }

        // const values
        private class Consts
        {
            // specify an entropy so other DPAPI applications can't see the data
            public readonly static byte[] EntropyData = ASCIIEncoding.ASCII.GetBytes("B0D125B7-967E-4f94-9305-A6F9AF56A19A");
        }

        // static class
        private DPAPIAlgorithm()
        {
        }

        // public methods

        // encrypt the data using DPAPI, returns a base64-encoded encrypted string
        public static string Encrypt(string data, Store store)
        {
            // holds the result string
            string result = "";

            // blobs used in the CryptProtectData call
            Win32.DATA_BLOB inBlob = new Win32.DATA_BLOB();
            Win32.DATA_BLOB entropyBlob = new Win32.DATA_BLOB();
            Win32.DATA_BLOB outBlob = new Win32.DATA_BLOB();

            try
            {
                // setup flags passed to the CryptProtectData call
                int flags = Win32.CRYPTPROTECT_UI_FORBIDDEN |
                    (int)((store == Store.Machine) ? Win32.CRYPTPROTECT_LOCAL_MACHINE : 0);

                // setup input blobs, the data to be encrypted and entropy blob
                SetBlobData(ref inBlob, ASCIIEncoding.ASCII.GetBytes(data));
                SetBlobData(ref entropyBlob, Consts.EntropyData);

                // call the DPAPI function, returns true if successful and fills in the outBlob
                if (Win32.CryptProtectData(ref inBlob, "", ref entropyBlob, IntPtr.Zero, IntPtr.Zero, flags, ref outBlob))
                {
                    byte[] resultBits = GetBlobData(ref outBlob);
                    if (resultBits != null)
                        result = Convert.ToBase64String(resultBits);
                }
            }
            catch
            {
                // an error occurred, return an empty string
            }
            finally
            {
                // clean up
                if (inBlob.pbData.ToInt32() != 0)
                    Marshal.FreeHGlobal(inBlob.pbData);

                if (entropyBlob.pbData.ToInt32() != 0)
                    Marshal.FreeHGlobal(entropyBlob.pbData);
            }

            return result;
        }

        // decrypt the data using DPAPI, data is a base64-encoded encrypted string
        public static string Decrypt(string data, Store store)
        {
            // holds the result string
            string result = "";

            // blobs used in the CryptUnprotectData call
            Win32.DATA_BLOB inBlob = new Win32.DATA_BLOB();
            Win32.DATA_BLOB entropyBlob = new Win32.DATA_BLOB();
            Win32.DATA_BLOB outBlob = new Win32.DATA_BLOB();

            try
            {
                // setup flags passed to the CryptUnprotectData call
                int flags = Win32.CRYPTPROTECT_UI_FORBIDDEN |
                    (int)((store == Store.Machine) ? Win32.CRYPTPROTECT_LOCAL_MACHINE : 0);

                // the CryptUnprotectData works with a byte array, convert string data
                byte[] bits = Convert.FromBase64String(data);

                // setup input blobs, the data to be decrypted and entropy blob
                SetBlobData(ref inBlob, bits);
                SetBlobData(ref entropyBlob, Consts.EntropyData);

                // call the DPAPI function, returns true if successful and fills in the outBlob
                if (Win32.CryptUnprotectData(ref inBlob, null, ref entropyBlob, IntPtr.Zero, IntPtr.Zero, flags, ref outBlob))
                {
                    byte[] resultBits = GetBlobData(ref outBlob);
                    if (resultBits != null)
                        result = ASCIIEncoding.ASCII.GetString(resultBits);
                }
            }
            catch
            {
                // an error occurred, return an empty string
            }
            finally
            {
                // clean up
                if (inBlob.pbData.ToInt32() != 0)
                    Marshal.FreeHGlobal(inBlob.pbData);

                if (entropyBlob.pbData.ToInt32() != 0)
                    Marshal.FreeHGlobal(entropyBlob.pbData);
            }

            return result;
        }


        // internal methods

        #region Data Protection API

        private class Win32
        {
            public const int CRYPTPROTECT_UI_FORBIDDEN = 0x1;
            public const int CRYPTPROTECT_LOCAL_MACHINE = 0x4;

            [StructLayout(LayoutKind.Sequential)]
            public struct DATA_BLOB
            {
                public int cbData;
                public IntPtr pbData;
            }

            [DllImport("crypt32", CharSet = CharSet.Auto)]
            public static extern bool CryptProtectData(ref DATA_BLOB pDataIn, string szDataDescr, ref DATA_BLOB pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref DATA_BLOB pDataOut);

            [DllImport("crypt32", CharSet = CharSet.Auto)]
            public static extern bool CryptUnprotectData(ref DATA_BLOB pDataIn, StringBuilder szDataDescr, ref DATA_BLOB pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref DATA_BLOB pDataOut);

            [DllImport("kernel32")]
            public static extern IntPtr LocalFree(IntPtr hMem);
        }

        #endregion

        // helper method that fills in a DATA_BLOB, copies 
        // data from managed to unmanaged memory
        private static void SetBlobData(ref Win32.DATA_BLOB blob, byte[] bits)
        {
            blob.cbData = bits.Length;
            blob.pbData = Marshal.AllocHGlobal(bits.Length);
            Marshal.Copy(bits, 0, blob.pbData, bits.Length);
        }

        // helper method that gets data from a DATA_BLOB, 
        // copies data from unmanaged memory to managed
        private static byte[] GetBlobData(ref Win32.DATA_BLOB blob)
        {
            // return an empty string if the blob is empty
            if (blob.pbData.ToInt32() == 0)
                return null;

            // copy information from the blob
            byte[] data = new byte[blob.cbData];
            Marshal.Copy(blob.pbData, data, 0, blob.cbData);
            Win32.LocalFree(blob.pbData);

            return data;
        }
    }
}
