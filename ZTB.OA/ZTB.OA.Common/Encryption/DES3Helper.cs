// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/7/12 13:37:12
// Update Time          :    2016/7/12 13:37:12
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZTB.OA.Common.Encryption
{
    public class DES3Helper
    {
        #region CBC模式**

        /// <summary>
        /// DES3 CBC模式加密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV</param>
        /// <param name="data">明文的byte数组</param>
        /// <returns>密文的byte数组</returns>
        public static byte[] Des3EncodeCBC(byte[] key, byte[] iv, byte[] data)
        {
            //复制于MSDN
            // Create a MemoryStream.
            MemoryStream mStream = new MemoryStream();

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.CBC;             //默认值
            tdsp.Padding = PaddingMode.PKCS7;       //默认值

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream cStream = new CryptoStream(mStream,
                tdsp.CreateEncryptor(key, iv),
                CryptoStreamMode.Write);

            // Write the byte array to the crypto stream and flush it.
            cStream.Write(data, 0, data.Length);
            cStream.FlushFinalBlock();

            // Get an array of bytes from the 
            // MemoryStream that holds the 
            // encrypted data.
            byte[] ret = mStream.ToArray();

            // Close the streams.
            cStream.Close();
            mStream.Close();

            // Return the encrypted buffer.
            return ret;

        }

        /// <summary>
        /// DES3 CBC模式解密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV</param>
        /// <param name="data">密文的byte数组</param>
        /// <returns>明文的byte数组</returns>
        internal static byte[] Des3DecodeCBC(byte[] key, byte[] iv, byte[] data)
        {

            // Create a new MemoryStream using the passed 
            // array of encrypted data.
            MemoryStream msDecrypt = new MemoryStream(data);

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.CBC;
            tdsp.Padding = PaddingMode.PKCS7;

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                tdsp.CreateDecryptor(key, iv),
                CryptoStreamMode.Read);

            // Create buffer to hold the decrypted data.
            byte[] fromEncrypt = new byte[data.Length];

            // Read the decrypted data out of the crypto stream
            // and place it into the temporary buffer.
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

            //Convert the buffer into a string and return it.
            return fromEncrypt;

        }

        #endregion

        #region ECB模式

        /// <summary>
        /// DES3 ECB模式加密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
        /// <param name="str">明文的byte数组</param>
        /// <returns>密文的byte数组</returns>
        public static byte[] Des3EncodeECB(byte[] key, byte[] iv, byte[] data)
        {

            // Create a MemoryStream.
            MemoryStream mStream = new MemoryStream();

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;
            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream cStream = new CryptoStream(mStream,
                tdsp.CreateEncryptor(key, iv),
                CryptoStreamMode.Write);

            // Write the byte array to the crypto stream and flush it.
            cStream.Write(data, 0, data.Length);
            cStream.FlushFinalBlock();

            // Get an array of bytes from the 
            // MemoryStream that holds the 
            // encrypted data.
            byte[] ret = mStream.ToArray();

            // Close the streams.
            cStream.Close();
            mStream.Close();

            // Return the encrypted buffer.
            return ret;

        }

        /// <summary>
        /// DES3 ECB模式解密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
        /// <param name="str">密文的byte数组</param>
        /// <returns>明文的byte数组</returns>
        public static byte[] Des3DecodeECB(byte[] key, byte[] iv, byte[] data)
        {
            // Create a new MemoryStream using the passed 
            // array of encrypted data.
            MemoryStream msDecrypt = new MemoryStream(data);

            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                tdsp.CreateDecryptor(key, iv),
                CryptoStreamMode.Read);

            // Create buffer to hold the decrypted data.
            byte[] fromEncrypt = new byte[data.Length];

            // Read the decrypted data out of the crypto stream
            // and place it into the temporary buffer.
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

            //Convert the buffer into a string and return it.
            return fromEncrypt;

        }

        #endregion

        const string DesKey = "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4";
        const string KeyIV = "diqngETQSqp=";
        static byte[] keyiv = { 6, 3, 8, 4, 5, 1, 8, 5 };

        public static string CodeToString(string Code)
        {

            Encoding utf8 = Encoding.UTF8;
            byte[] key = Convert.FromBase64String(DesKey);
            byte[] data = Convert.FromBase64String(Code);
            byte[] data1 = utf8.GetBytes(Code);
            byte[] str6 = Des3DecodeCBC(key, keyiv, data);
            string str = utf8.GetString(str6, 0, str6.Length).Replace("\0", "");
            return str;

        }

        public static string StringToCode(string Str)
        {

            Encoding utf8 = Encoding.UTF8;
            UnicodeEncoding Con = new UnicodeEncoding();
            byte[] key = Convert.FromBase64String(DesKey);
            byte[] data = utf8.GetBytes(Str);
            byte[] byte_Code = Des3EncodeCBC(key, keyiv, data);
            string Code = Convert.ToBase64String(byte_Code);
            return Code;

        }



        private const string sKey = "A3F2569DESJEIWBCJOTY45DYQWF68H1Y";   //矢量，矢量可以为空  
        private const string sIV = "qcDY6X+aPLw=";   //构造一个对称算法  
        private SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
        /// 输入的字符串  
        /// 加密后的字符串  
        public string EncryptString(string Value)
        {
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            mCSP.Key = Convert.FromBase64String(sKey);
            mCSP.IV = Convert.FromBase64String(sIV);    //指定加密的运算模式  
            mCSP.Mode = CipherMode.ECB;    //获取或设置加密算法的填充模式  
            mCSP.Padding = PaddingMode.PKCS7;
            ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);
            byt = Encoding.UTF8.GetBytes(Value);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
