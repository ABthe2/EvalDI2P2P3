using System.Security.Cryptography;
using System.Text;
using Eval.Business.Encryption;

namespace Eval.Business.Utils;

public class AESUtils : IEncryptionStrategy
{
    private static readonly string Key = "7cc571067883c3b31d6c72be1e49324f";
    private static readonly string IV = "FJENCH475YTUIROP";
    
    public string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                cs.Write(plainBytes, 0, plainBytes.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
    
    public string Decrypt(string encryptedText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedText)))
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                byte[] decryptedBytes = new byte[ms.Length];
                int bytesRead = cs.Read(decryptedBytes, 0, decryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes, 0, bytesRead);
            }
        }
    }
}