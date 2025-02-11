namespace Passbook.Generator.Tags
{
    /// <summary>
    /// The name of the city or hosting region of the venue.
    /// </summary>
    public class VenueRegionName(string value) : SemanticTagBaseValue("venueRegionName", value)
    {
    }
}
