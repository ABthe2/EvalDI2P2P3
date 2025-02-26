namespace Eval.Business.Encryption;

public interface IEncryptionStrategy
{
    string Encrypt(string text);
    string Decrypt(string encryptedText);
}