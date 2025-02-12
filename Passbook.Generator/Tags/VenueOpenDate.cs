using System;

namespace Passbook.Generator.Tags
{
    /// <summary>
    /// Time when the venue opens. Use if none of the more specific values above apply.
    /// </summary>
    public class VenueOpenDate(DateTimeOffset value) : SemanticTagBaseValue("venueOpenDate", value)
    {
    }
}
