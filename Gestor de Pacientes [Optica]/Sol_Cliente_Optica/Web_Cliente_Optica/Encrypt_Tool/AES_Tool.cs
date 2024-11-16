using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Web_Cliente_Optica.Encrypt_Tool
{
    public class AES_Tool
    {
        //Key y IV (Vector de Inicialización) en formato hexadecimal (para que tengan la longitud correcta [16 Bytes])

        private static readonly string HexKey = "3be377a88684544892a74658cc570451"; //16 Bytes
        private static readonly string HexIV = "d4f8a9b3c7e2461f5a7b9c3e8f1246da"; //16 Bytes

        private static byte[] Key = HexStringToByteArray(HexKey);
        private static byte[] IV = HexStringToByteArray(HexIV);

        public string Encrypt(string plainTextPassword)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Key;
                aesAlgorithm.IV = IV;

                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainTextPassword);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}