using System;

namespace Passbook.Generator.Tags
{
    /// <summary>
    /// Time the parking lots open.
    /// </summary>
    public class VenueParkingLotsOpenDate(DateTimeOffset value) : SemanticTagBaseValue("venueParkingLotsOpenDate", value)
    {
    }
}
