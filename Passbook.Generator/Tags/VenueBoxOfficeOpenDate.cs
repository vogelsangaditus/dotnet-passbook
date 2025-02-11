using System;

namespace Passbook.Generator.Tags
{
    /// <summary>
    /// Time when the box office opens.
    /// </summary>
    public class VenueBoxOfficeOpenDate(DateTimeOffset value) : SemanticTagBaseValue("venueBoxOfficeOpenDate", value)
    {
    }
}
