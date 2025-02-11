using System;

namespace Passbook.Generator.Tags
{
    /// <summary>
    /// Time the fan zone opens.
    /// </summary>
    public class VenueFanZoneOpenDate(DateTimeOffset value) : SemanticTagBaseValue("venueFanZoneOpenDate", value)
    {
    }
}
