namespace Passbook.Generator.Tags
{
    /// <summary>
    /// The long description of the entrance information. This contains whatever string you select such as "Apple gate 5".
    /// </summary>
    public class EntranceDescription(string value) : SemanticTagBaseValue("entranceDescription", value)
    {
    }
}
