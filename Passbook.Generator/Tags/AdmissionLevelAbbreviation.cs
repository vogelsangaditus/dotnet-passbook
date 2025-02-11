namespace Passbook.Generator.Tags
{
    /// <summary>
    /// The abbreviation of the level of admission the ticket provides (GA, VIP, etc).
    /// </summary>
    public class AdmissionLevelAbbreviation(string value) : SemanticTagBaseValue("admissionLevelAbbreviation", value)
    {
    }
}
