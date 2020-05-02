using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Frame.Common
{
    /// <summary>
    /// 加密解密类
    /// </summary>
    public class EncryptDecode
    {
        #region SHA256加密算法

        /// <summary>
        /// SHA256函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果(返回长度为44字节的字符串)</returns>
        public static string SHA256(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed sha256 = new SHA256Managed();
            byte[] result = sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(result);
        }

        #endregion

        #region Base64加密解密

        /// <summary>
        /// Base64是一種使用64基的位置計數法。它使用2的最大次方來代表僅可列印的ASCII 字元。
        /// 這使它可用來作為電子郵件的傳輸編碼。在Base64中的變數使用字元A-Z、a-z和0-9 ，
        /// 這樣共有62個字元，用來作為開始的64個數字，最後兩個用來作為數字的符號在不同的
        /// 系統中而不同。
        /// Base64加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Encrypt(string str)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64(string str)
        {
            byte[] data = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(data);
        }

        #endregion

        #region MD5

        /// <summary>
        /// 获得32位的MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5_32(string str)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获得16位的MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5_16(string str)
        {
            return GetMD5_32(str).Substring(8,16);
        }

        /// <summary>
        /// 获得8位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_8(string str)
        {
            return GetMD5_32(str).Substring(8, 8);
        }
        /// <summary>
        /// 获得4位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_4(string str)
        {
            return GetMD5_32(str).Substring(8, 4);
        }

        public static string MD5EncryptHash(string str)
        {
            System.Security.Cryptography.MD5 mD5 = new MD5CryptoServiceProvider();
            byte[] data = mD5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            char[] temp = new char[data.Length];
            Array.Copy(data, temp, data.Length);
            return new string(temp);
        }

        #endregion
    }
}
