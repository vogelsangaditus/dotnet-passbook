using System;

namespace Passbook.Generator.Tags
{
    /// <summary>
    /// Time the gates to the venue open.
    /// </summary>
    public class VenueGatesOpenDate(DateTimeOffset value) : SemanticTagBaseValue("venueGatesOpenDate", value)
    {
    }
}
