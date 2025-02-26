namespace Eval.Business.Encryption;

public class EncryptionContext
{
    private readonly IEncryptionStrategy _encryptionStrategy;
    
    public EncryptionContext(IEncryptionStrategy encryptionStrategy)
    {
        _encryptionStrategy = encryptionStrategy;
    }
    
    public string Encrypt(string text)
    {
        return _encryptionStrategy.Encrypt(text);
    }
    
    public string Decrypt(string encryptedText)
    {
        return _encryptionStrategy.Decrypt(encryptedText);
    }
}