namespace Passbook.Generator.Tags
{
    /// <summary>
    /// Boolean value indicating whether tailgating is allowed at the venue.
    /// </summary>
    public class TailgatingAllowed(bool value) : SemanticTagBaseValue("tailgatingAllowed", value)
    {
    }
}
