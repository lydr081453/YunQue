using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ESP.Security
{
    /// <summary>
    /// Passport认证票据类
    /// </summary>
    public class PassportAuthenticationTicket
    {
        const string VALIDATE_MARK = "ESP.AUTH";

        int _UserID;
        DateTime _Expired;
        string _UserName;

        /// <summary>
        /// 构造空的票据
        /// </summary>
        public PassportAuthenticationTicket()
        {
            _UserID = 0;
            _Expired = DateTime.MinValue;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expired
        {
            get { return _Expired; }
            set { _Expired = value; }
        }

        /// <summary>
        /// 用户名。
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        /// <summary>
        /// 从字节数组还原票据
        /// </summary>
        /// <param name="buf">包含票据信息的字节数组</param>
        /// <returns></returns>
        public static PassportAuthenticationTicket FromBytes(byte[] buf)
        {
            string cookieValue = System.Text.Encoding.UTF8.GetString(buf);
            if (cookieValue == null)
                return null;
            cookieValue = cookieValue.Trim();
            if (cookieValue.Length == 0)
                return null;

            string[] parts = cookieValue.Split(',');
            if (parts.Length != 5)
                return null;

            if (parts[1] != VALIDATE_MARK)
                return null;

            string userIdString = parts[2];
            string username = parts[3];
            string expiredString = parts[4];

            int userId;
            if (!int.TryParse(userIdString, out userId))
                return null;
            if (userId < 0)
                return null;


            DateTime expired;
            if (!DateTime.TryParse(expiredString, out expired))
                return null;
            expired = DateTime.SpecifyKind(expired, DateTimeKind.Utc);

            return new PassportAuthenticationTicket()
            {
                UserID = userId,
                UserName = username,
                Expired = expired
            };

        }

        /// <summary>
        /// 序列化为字节数组
        /// </summary>
        /// <returns>序列化后的字节数组</returns>
        public byte[] ToBytes()
        {
            byte[] rng = new byte[8];
            System.Security.Cryptography.RNGCryptoServiceProvider rngProvider = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rngProvider.GetBytes(rng);

            string b64Rng = Convert.ToBase64String(rng);

            string datetime = this._Expired.ToString("yyyy-MM-dd HH:mm:ss");

            string cookieValue = b64Rng + "," + VALIDATE_MARK + "," + _UserID.ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + _UserName + "," + datetime;

            byte[] utf8 = System.Text.Encoding.UTF8.GetBytes(cookieValue);

            return utf8;
        }

        /*
        /// <summary>
        /// 序列化为字节数组
        /// </summary>
        /// <returns>序列化后的字节数组</returns>
        public byte[] ToBytes()
        {
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            byte[] mark = System.Text.Encoding.UTF8.GetBytes(VALIDATE_MARK);
            //if (mark.Length > byte.MaxValue)
            //{
            //    byte[] tmp = new byte[byte.MaxValue];
            //    Array.Copy(mark, tmp, byte.MaxValue);
            //    mark = tmp;
            //}

            byte[] un = System.Text.Encoding.UTF8.GetBytes(_UserName ?? string.Empty);

            byte[] buf = new byte[1 + salt.Length + sizeof(int) + mark.Length + sizeof(int) + sizeof(int) + un.Length + sizeof(long)];

            int position = 0;
            int position2;

            buf[position] = (byte)salt.Length;
            position++;

            Array.Copy(salt, 0, buf, position, salt.Length);
            position += salt.Length;

            //buf[position] = (byte)mark.Length;
            //position++;

            int markLength = mark.Length;
            position2 = position + sizeof(int);
            while (position < position2)
            {
                buf[position] = (byte)(markLength & 0xFF);
                markLength = markLength >> 8;
                position++;
            }

            Array.Copy(mark, 0, buf, position, mark.Length);
            position += mark.Length;


            int u = _UserID;
            position2 = position + sizeof(int);
            while (position < position2)
            {
                buf[position] = (byte)(u & 0xFF);
                u = u >> 8;
                position++;
            }

            int unLength = un.Length;
            position2 = position + sizeof(int);
            while (position < position2)
            {
                buf[position] = (byte)(unLength & 0xFF);
                unLength = unLength >> 8;
                position++;
            }

            Array.Copy(un, 0, buf, position, un.Length);
            position += un.Length;

            long l = _Expired.Ticks;
            position2 = position + sizeof(long);
            while (position < position2)
            {
                buf[position] = (byte)(l & 0xFF);
                l = l >> 8;
                position++;
            }

            return buf;
        }

        /// <summary>
        /// 从字节数组还原票据
        /// </summary>
        /// <param name="buf">包含票据信息的字节数组</param>
        /// <returns></returns>
        public static PassportAuthenticationTicket FromBytes(byte[] buf)
        {
            int position = 0;
            int position2;

            if (position >= buf.Length)
                return null;

            int saltLen = buf[position++];

            position2 = position + saltLen;
            if (position2 > buf.Length)
                return null;

            byte[] salt = new byte[saltLen];
            Array.Copy(buf, position, salt, 0, saltLen);
            position = position2;

            if (position >= buf.Length)
                return null;



            //int markLen = buf[position++];
            if (position + sizeof(int) > buf.Length)
                return null;
            int markLen = 0;
            for (int i = 0; i < sizeof(int); i++)
            {
                int byteVal = buf[position++];
                byteVal = byteVal << 8 * i;
                markLen |= byteVal;
            }

            position2 = position + markLen;
            if (position2 > buf.Length)
                return null;

            byte[] mark = new byte[markLen];
            Array.Copy(buf, position, mark, 0, markLen);
            position = position2;


            try
            {
                string markString = System.Text.Encoding.UTF8.GetString(mark);
                if (markString != VALIDATE_MARK)
                    return null;
            }
            catch
            {
                return null;
            }

            if (position + sizeof(int) > buf.Length)
                return null;
            int userId = 0;
            for (int i = 0; i < sizeof(int); i++)
            {
                int byteVal = buf[position++];
                byteVal = byteVal << 8 * i;
                userId |= byteVal;
            }

            if (userId < 0)
                return null;

            if (position + sizeof(int) > buf.Length)
                return null;
            int unLen = 0;
            for (int i = 0; i < sizeof(int); i++)
            {
                int byteVal = buf[position++];
                byteVal = byteVal << 8 * i;
                unLen |= byteVal;
            }

            string username;
            if (unLen > 0)
            {
                position2 = position + unLen;
                if (position2 > buf.Length)
                    return null;

                byte[] un = new byte[unLen];
                Array.Copy(buf, position, un, 0, unLen);
                position = position2;

                try
                {
                    username = System.Text.Encoding.UTF8.GetString(un);
                }
                catch
                {
                    return null;
                }
            }
            else if (unLen == 0)
            {
                username = string.Empty;
            }
            else
            {
                return null;
            }

            if (position + sizeof(long) > buf.Length)
                return null;
            long ticks = 0;
            for (int i = 0; i < sizeof(long); i++)
            {
                long byteVal = buf[position++];
                byteVal = byteVal << 8 * i;
                ticks |= byteVal;
            }

            if (ticks < DateTime.MinValue.Ticks || ticks > DateTime.MaxValue.Ticks)
                return null;

            PassportAuthenticationTicket tiket = new PassportAuthenticationTicket();
            tiket._UserID = userId;
            tiket._UserName = username;
            tiket._Expired = new DateTime(ticks);

            return tiket;
        }
        */

        /// <summary>
        /// 空票据
        /// </summary>
        public static PassportAuthenticationTicket Empty
        {
            get
            {
                PassportAuthenticationTicket t = new PassportAuthenticationTicket();
                t._UserID = 0;
                t._UserName = string.Empty;
                t._Expired = DateTime.MaxValue;

                return t;
            }
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        /// <param name="seconds">过期时间</param>
        /// <returns>如果票据已过期返回true，否则返回false</returns>
        public bool IsExpired(int seconds)
        {
            return this._Expired < DateTime.UtcNow;
        }
    }
}

