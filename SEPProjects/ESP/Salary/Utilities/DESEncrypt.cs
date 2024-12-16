using System;
using System.Security.Cryptography;  
using System.Text;
namespace ESP.Salary.Utility
{
	/// <summary>
	/// DES加密/解密类。
	/// </summary>
	public class DESEncrypt
	{

        //默认密钥向量
        private static byte[] IV = { 0x45, 0x73, 0x61, 0x89, 0x4F, 0x35, 0x2D, 0x89, 0x13, 0x4E, 0x1F, 0x87, 0x1D, 0x69, 0x3E, 0x1F };

		public DESEncrypt() 
		{			
		}


        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptString">要加密的字符</param>
        /// <param name="encryptKey">对应的密钥(不可为中文) 32位</param>
        /// <returns></returns>
        public static string Encode(string encryptString)
        {
            if (string.IsNullOrEmpty(encryptString))
            {
                return "";
            }
            else
            {
                encryptString = encryptString.Replace(",", "");
            }
            //encryptKey = Utils.GetSubString(encryptKey, 16, "");
            //encryptKey = encryptKey.PadRight(16, ' ');
            string Keys = ESP.Salary.Utility.Common.Salary_Keys;
            byte[] keyByteArray = System.Text.Encoding.Default.GetBytes(Keys);

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = keyByteArray;
            rijndaelProvider.IV = IV;
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

            byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }


        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptString">要解密的字符（该字符必须是已经加密过的字符）</param>
        /// <param name="decryptKey">解密的密钥，该密钥要和加密的密钥一致 32位</param>
        /// <returns></returns>
        public static string Decode(string decryptString)
        {
            if (string.IsNullOrEmpty(decryptString))
            {
                return "0";
            }
            try
            {
                string Keys = ESP.Salary.Utility.Common.Salary_Keys;
                byte[] keyByteArray = System.Text.Encoding.Default.GetBytes(Keys);
                RijndaelManaged rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.Key = keyByteArray;
                rijndaelProvider.IV = IV;//Encoding.UTF8.GetBytes(decryptKey);
                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                if (string.IsNullOrEmpty(Encoding.UTF8.GetString(decryptedData)))
                {
                    return "0";
                }
                else {
                    return Encoding.UTF8.GetString(decryptedData).Replace(",","");
                }
            }
            catch(Exception ex)
            {
                string str = ex.Message;
                return "0";
            }

        }

	}
}
