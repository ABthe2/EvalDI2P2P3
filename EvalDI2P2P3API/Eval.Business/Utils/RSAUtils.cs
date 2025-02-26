using System.Security.Cryptography;
using System.Text;
using Eval.Business.Encryption;

namespace Eval.Business.Utils;

public class RSAUtils : IEncryptionStrategy
{
    public string Encrypt(string text)
    {
        var publicKey = File.ReadAllText("Keys/rsa_public.pem");
        var rsa = RSA.Create();
        var base64PublicKey = ExtractBase64FromPublicPem(publicKey);
        rsa.ImportRSAPublicKey(Convert.FromBase64String(base64PublicKey), out _);
        byte[] encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.OaepSHA256);
        return Convert.ToBase64String(encryptedBytes);
    }

    public string Decrypt(string encryptedText)
    {
        var privateKey = File.ReadAllText("Keys/rsa_private.pem");
        var rsa = RSA.Create();
        var base64PrivateKey = ExtractBase64FromPrivatePem(privateKey);
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(base64PrivateKey), out _);
        byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(encryptedText), RSAEncryptionPadding.OaepSHA256);
        return Encoding.UTF8.GetString(decryptedBytes);
    }
    
    private static string ExtractBase64FromPublicPem(string pem)
    {
        const string header = "-----BEGIN RSA PUBLIC KEY-----";
        const string footer = "-----END RSA PUBLIC KEY-----";
        int start = pem.IndexOf(header, StringComparison.Ordinal) + header.Length;
        int end = pem.IndexOf(footer, start, StringComparison.Ordinal);
        string base64 = pem.Substring(start, end - start);
        return base64.Replace("\n", "").Replace("\r", "").Replace(" ", "");
    }

    private static string ExtractBase64FromPrivatePem(string pem)
    {
        const string header = "-----BEGIN RSA PRIVATE KEY-----";
        const string footer = "-----END RSA PRIVATE KEY-----";
        int start = pem.IndexOf(header, StringComparison.Ordinal) + header.Length;
        int end = pem.IndexOf(footer, start, StringComparison.Ordinal);
        string base64 = pem.Substring(start, end - start);
        return base64.Replace("\n", "").Replace("\r", "").Replace(" ", "");
    }
}