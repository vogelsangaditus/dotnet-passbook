namespace Passbook.Generator.Tags
{
    /// <summary>
    /// The name of the person the ticket grants admission to.
    /// </summary>
    public class AttendeeName(string value) : SemanticTagBaseValue("attendeeName", value)
    {
    }
}
