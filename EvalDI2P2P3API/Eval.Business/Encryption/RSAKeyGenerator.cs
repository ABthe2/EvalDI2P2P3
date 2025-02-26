using System.Security.Cryptography;

namespace Eval.Business.Encryption;

public static class RSAKeyGenerator
{
    private const int KeySize = 4096;
    private static readonly string PrivateKeyPath = "Keys/rsa_private.pem";
    private static readonly string PublicKeyPath = "Keys/rsa_public.pem";

    public static void GenerateKeysIfNotExist()
    {
        // Vérifier si les clés existent déjà
        if (File.Exists(PrivateKeyPath) && File.Exists(PublicKeyPath))
        {
            Console.WriteLine("RSA keys already exist. No need to generate new ones.");
            return;
        }

        Console.WriteLine("Generating RSA keys...");
        Directory.CreateDirectory("Keys");

        using (var rsa = RSA.Create(KeySize))
        {
            string privateKey = rsa.ExportRSAPrivateKeyPem();
            string publicKey = rsa.ExportRSAPublicKeyPem();

            File.WriteAllText(PrivateKeyPath, privateKey);
            File.WriteAllText(PublicKeyPath, publicKey);
        }

        Console.WriteLine("RSA keys generated successfully.");
    }
}