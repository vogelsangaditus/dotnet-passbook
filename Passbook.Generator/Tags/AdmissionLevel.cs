namespace Passbook.Generator.Tags
{
    /// <summary>
    /// The level of admission the ticket provides (e.g. General Admission, VIP, etc).
    /// </summary>
    public class AdmissionLevel(string value) : SemanticTagBaseValue("admissionLevel", value)
    {
    }
}
