namespace Passbook.Generator.Tags
{
    /// <summary>
    /// A flexible field for critical ticket attributes that will show on the footer of the main poster. 
    /// This is meant for critical, ticket-specific information that cannot be reasonably located elsewhere.
    /// </summary>
    public class AdditionalTicketAttributes(string value) : SemanticTagBaseValue("additionalTicketAttributes", value)
    {
    }
}
