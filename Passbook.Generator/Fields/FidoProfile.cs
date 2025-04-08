namespace Passbook.Generator.Fields
{
    public class FidoProfile
    {
        public FidoProfile(string relyingPartyIdentifier, string accountHash, string keyHash)
        {
            RelyingPartyIdentifier = relyingPartyIdentifier;
            AccountHash = accountHash;
            KeyHash = keyHash;
        }

        public string RelyingPartyIdentifier { get; }
        public string AccountHash { get; }
        public string KeyHash { get; }
    }
}