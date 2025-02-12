using System;

namespace Passbook.Generator.Tags
{
    /// <summary>
    /// Time when the venue closes.
    /// </summary>
    public class VenueCloseDate(DateTimeOffset value) : SemanticTagBaseValue("venueCloseDate", value)
    {
    }
}
