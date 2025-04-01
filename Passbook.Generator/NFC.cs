namespace Passbook.Generator
{
    public class Nfc
    {
        public Nfc(string message, string encryptionPublicKey, bool? requiresAuthentication = null)
        {
            Message = message;
            EncryptionPublicKey = encryptionPublicKey;
        }

        public string Message { get; }

        public string EncryptionPublicKey { get; }

        public bool? RequiresAuthentication { get; }
    }
}
