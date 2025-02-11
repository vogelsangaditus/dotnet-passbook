using System;

namespace Passbook.Generator.Tags
{
    /// <summary>
    /// Time the doors to the venue open.
    /// </summary>
    public class VenueDoorsOpenDate(DateTimeOffset value) : SemanticTagBaseValue("venueDoorsOpenDate", value)
    {
    }
}
