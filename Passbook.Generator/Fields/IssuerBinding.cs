namespace Passbook.Generator.Fields
{
    public class IssuerBinding
    {
        public IssuerBinding(string issuerBindingData, string learnMoreURL)
        {
            IssuerBindingData = issuerBindingData;
            LearnMoreURL = learnMoreURL;
        }

        public string IssuerBindingData { get; }

        public string LearnMoreURL { get; }
    }
}
