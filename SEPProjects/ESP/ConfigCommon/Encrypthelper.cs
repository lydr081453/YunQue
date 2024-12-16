using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web;
using System.IO;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Configuration;
using System.Net;

namespace ESP.ConfigCommon
{
    public class EncryptHelper
    {


        #region SHA1

        private const int saltLen = 4;

        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static byte[] EncryptPassword(string input)
        {
            byte[] sha1Pwd;
            SHA1 sha1 = SHA1.Create();
            sha1Pwd = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
            sha1.Clear();
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltLen];
            rng.GetBytes(salt);

            return saltedDBPassword(sha1Pwd, salt);
        }


        /// <summary>
        /// Salteds the DB password.
        /// </summary>
        /// <param name="sha1Pwd">The sha1 PWD.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        private static byte[] saltedDBPassword(byte[] sha1Pwd, byte[] salt)
        {
            System.Diagnostics.Debug.Fail(Convert.ToBase64String(salt));
            int len = sha1Pwd.Length;
            byte[] plusPwd = new byte[len + saltLen];
            sha1Pwd.CopyTo(plusPwd, 0);
            salt.CopyTo(plusPwd, len);

            SHA1 sha1 = SHA1.Create();
            byte[] saltedPwd = sha1.ComputeHash(plusPwd);
            sha1.Clear();

            int len2 = saltedPwd.Length;
            byte[] DBPwd = new byte[len2 + saltLen];
            saltedPwd.CopyTo(DBPwd, 0);
            salt.CopyTo(DBPwd, len2);

            return DBPwd;
        }

        #endregion


        #region 
        private static SymmetricAlgorithm mCSP = new DESCryptoServiceProvider();
        /// <summary>
        /// Encrypts the specified value.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        public static string Encrypt(string Value)
        {
            byte[] mKey = { 0x22, 0x34, 0x33, 0xf3, 0x66, 0x66, 0xa3, 0x11 };
            byte[] mIV = { 0x24, 0x34, 0xd3, 0xa3, 0x76, 0x64, 0xd9, 0x17 };
            ICryptoTransform ct = mCSP.CreateEncryptor(mKey, mIV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                {
                    byte[] byt;
                    byt = Encoding.UTF8.GetBytes(Value);

                    cs.Write(byt, 0, byt.Length);
                    cs.FlushFinalBlock();

                    cs.Close();
                }

                return Convert.ToBase64String(ms.ToArray());
            }

        }

        /// <summary>
        /// Decrypts the specified value.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        public static string Decrypt(string Value)
        {
            try
            {
                byte[] mKey = { 0x22, 0x34, 0x33, 0xf3, 0x66, 0x66, 0xa3, 0x11 };
                byte[] mIV = { 0x24, 0x34, 0xd3, 0xa3, 0x76, 0x64, 0xd9, 0x17 };
                ICryptoTransform ct;

                ct = mCSP.CreateDecryptor(mKey, mIV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                    {
                        byte[] byt;
                        byt = Convert.FromBase64String(Value);

                        cs.Write(byt, 0, byt.Length);
                        cs.FlushFinalBlock();

                        cs.Close();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch { return ""; }
        }
        #endregion
    }
}
